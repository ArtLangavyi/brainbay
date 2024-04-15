using Amazon.Runtime.Internal.Util;

using Elastic.Apm.NetCoreAll;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

using RickAndMorty.Shared.Helpers;
using RickAndMorty.Shared.Services;
using RickAndMorty.Shared.Services.Abstractions;
using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Core.Services.Abstractions;
using RickAndMorty.WebApi.Core.Services.Character;
using RickAndMorty.WebApi.Data.Context;
using RickAndMorty.WebApi.Data.Repositories;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;
using RickAndMorty.WebApi.Models.Requests.Characters;
using RickAndMorty.WebApi.Models.Responses.Characters;
using RickAndMorty.WebApi.Models.Responses.Locations;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

builder.Services.AddMemoryCache();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RickAndMortyContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<ILocationService, LocationService>();

builder.Services.AddTransient<ICacheService, CacheService>(); 

var logger = LogService.AddLogger(builder.Configuration, "RickAndMorty_WebApi");
Log.Logger = logger.CreateLogger();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (builder.Configuration.GetSection("ElasticApm").GetValue<bool>("Enabled"))
{
    app.UseHttpsRedirection();

    app.UseAllElasticApm(builder.Configuration);
}


app.UseSerilogRequestLogging();


CreateRequestMapForCharacters(app);

CreateRequestMapForLocations(app);


app.Run();


static void CreateRequestMapForCharacters(WebApplication app)
{
    app.MapGet("/characters", async (ICharacterService characterService, ICacheService cacheService, string? planet) =>
    {
        var cacheKey = "characters";
        
        if(!string.IsNullOrEmpty(planet))
        {
            cacheKey = $"characters_{planet}";
        }

        var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Get All Characters", "GET");

        CharacterResponse[]? characters = null;

        try
        {
            characters = cacheService.GetObjectFromCache<CharacterResponse[]>(cacheKey);

            if (characters == null)
            {
                characters = await characterService.GetAllCharactersAsync(planet);

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromSeconds(30))
                        .SetAbsoluteExpiration(TimeSpan.FromHours(1));

                cacheService.SetObjectInCache<CharacterResponse[]>(cacheKey, 30, 1, characters);
            }
            
        }
        catch (Exception ex)
        {
            apmTransaction.CaptureException(ex);
        }
        finally
        {
            apmTransaction.End();
        }

        return TypedResults.Ok(characters);
    })
    .WithName("Get All Characters")
    .WithOpenApi(operation => new(operation)
    {
        Tags = [new() { Name = "Characters" }],
        Summary = "Get All Characters",
        Description = "Get All Characters"
    })
    .Produces<IEnumerable<CharacterResponse>>();

    app.MapPost("/characters", async (ICharacterService characterService, AddCharactersRequest request) =>
    {
        var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Add Character", "POST");

        int? addedCharacterId = default;
        try
        {
            addedCharacterId = await characterService.AddCharacterAsync(request);
        }
        catch (Exception ex)
        {
            apmTransaction.CaptureException(ex);
        }
        finally
        {
            apmTransaction.End();
        }
        return TypedResults.Ok(addedCharacterId);
    })
    .WithName("Add Character")
    .WithOpenApi(operation => new(operation)
    {
        Tags = [new() { Name = "Characters" }],
        Summary = "Add Character",
        Description = "Add Character"
    });
}

static void CreateRequestMapForLocations(WebApplication app)
{
    app.MapGet("/planets", async (ILocationService locationService, ICacheService cacheService) =>
    {
        var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Get All Planets", "GET");

        LocationResponse[]? planets = [];
        try
        {
            const string cacheKey = "planets";

            planets = cacheService.GetObjectFromCache<LocationResponse[]>(cacheKey);

            if (planets == null)
            {
                planets = await locationService.GetAllPlanetsAsync();

                cacheService.SetObjectInCache<LocationResponse[]>(cacheKey, 30, 1, planets);
            }

          
        }
        catch (Exception ex)
        {
            apmTransaction.CaptureException(ex);
        }
        finally
        {
            apmTransaction.End();
        }
        return TypedResults.Ok(planets);
    })
    .WithName("Get All Planets")
    .WithOpenApi(operation => new(operation)
    {
        Tags = [new() { Name = "Locations" }],
        Summary = "Get All Planets",
        Description = "Get All Planets"
    })
    .Produces<LocationResponse[]>();
}