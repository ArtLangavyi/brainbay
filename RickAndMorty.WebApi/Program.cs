using Microsoft.EntityFrameworkCore;

using RickAndMorty.WebApi.Core.Mappers;
using RickAndMorty.WebApi.Core.Services.Abstractions;
using RickAndMorty.WebApi.Core.Services.Character;
using RickAndMorty.WebApi.Data.Context;
using RickAndMorty.WebApi.Data.Repositories;
using RickAndMorty.WebApi.Data.Repositories.Abstractions;
using RickAndMorty.WebApi.Models.Requests.Characters;
using RickAndMorty.WebApi.Models.Responses.Characters;
using RickAndMorty.WebApi.Models.Responses.Locations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RickAndMortyContext>(options =>
          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Singleton);

builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

builder.Services.AddScoped<ILocationRepository, LocationRepository>();
builder.Services.AddScoped<ICharacterRepository, CharacterRepository>();

builder.Services.AddTransient<ICharacterService, CharacterService>();
builder.Services.AddTransient<ILocationService, LocationService>(); 

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

CreateRequestMapForCharacters(app);

CreateRequestMapForLocations(app);


app.Run();


static void CreateRequestMapForCharacters(WebApplication app)
{
    app.MapGet("/characters", async (ICharacterService characterService, string? planet) =>
    {
        var characters = await characterService.GetAllCharactersAsync(planet);
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
        var id = await characterService.AddCharacterAsync(request);
        return TypedResults.Ok(id);
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
        var planets = await locationService.GetAllPlanetsAsync();
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