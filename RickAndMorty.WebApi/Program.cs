using Elastic.Apm.NetCoreAll;

using Microsoft.EntityFrameworkCore;

using RickAndMorty.Shared.Helpers;
using RickAndMorty.Shared.Services;
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

// Configure Serilog
builder.Host.UseSerilog((hostingContext, loggerConfiguration) =>
{
    loggerConfiguration
        .ReadFrom.Configuration(hostingContext.Configuration)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RickAndMortyContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<ILocationService, LocationService>();

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
    app.MapGet("/characters", async (ICharacterService characterService, string? planet) =>
    {
        var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Get All Characters", "GET");

        CharacterResponse[] characters = [];
        try
        {
            characters = await characterService.GetAllCharactersAsync(planet);
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
    app.MapGet("/planets", async (ILocationService locationService) =>
    {
        var apmTransaction = Elastic.Apm.Agent.Tracer.StartTransaction("Get All Planets", "GET");

        LocationResponse[] planets = [];
        try
        {
            planets = await locationService.GetAllPlanetsAsync();
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