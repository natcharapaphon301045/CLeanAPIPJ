using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Application.Services;
using CleanAPIPJ.Domain.Interfaces;
using CleanAPIPJ.Infrastructure;
using CleanAPIPJ.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext to use SQL Server
builder.Services.AddDbContext<PokemonDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IPokedexRepository, PokedexRepository>(); // Register Repository
builder.Services.AddScoped<PokedexService>(); // Register Service

var app = builder.Build();

// Define Minimal API Endpoints here

// Get a specific Pokemon by ID
app.MapGet("/api/pokemon/{id}", (int id, PokedexService service) =>
{
    var pokemon = service.GetPokemonById(id);
    return pokemon is not null ? Results.Ok(pokemon) : Results.NotFound();
});

// Create a new Pokemon
app.MapPost("/api/pokemon", (Pokedex pokemon, PokedexService service) =>
{
    service.CreatePokemon(pokemon);
    return Results.Created($"/api/pokemon/{pokemon.PokemonID}", pokemon);
});

// Update an existing Pokemon
app.MapPut("/api/pokemon", (Pokedex pokemon, PokedexService service) =>
{
    service.UpdatePokemon(pokemon);
    return Results.NoContent();
});

// Delete a Pokemon by ID
app.MapDelete("/api/pokemon/{id}", (int id, PokedexService service) =>
{
    service.DeletePokemon(id);
    return Results.NoContent();
});

// Get all Pokemon
app.MapGet("/api/pokemon", (PokedexService service) =>
{
    var pokemons = service.GetAllPokemons();
    return Results.Ok(pokemons);
});

app.Run();
