using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Application.Services;
using CleanAPIPJ.Domain.Interfaces;
using CleanAPIPJ.Infrastructure;
using CleanAPIPJ.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using CleanAPIPJ.Application.Requests;

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
    var pokemon = service.GetPokemonById(id); // เรียกใช้ service
    return pokemon is not null 
        ? Results.Ok(pokemon) // หากพบ Pokémon จะคืนค่าด้วย 200 OK พร้อมข้อมูล
        : Results.NotFound(pokemon); // หากไม่พบ Pokémon จะคืนค่าด้วย 404 Not Found พร้อมข้อความ
});


// Create a new Pokemon
// POST: /api/pokemon - Create a new Pokemon
app.MapPost("/api/pokemon", (CreatePokemonRequest request, PokedexService service) =>
{
    try
    {
        // ตรวจสอบว่า PokemonID ซ้ำหรือไม่
        var existingPokemon = service.GetPokemonById(request.PokemonID); 

        // แปลง CreatePokemonRequest เป็น Pokedex
        var pokemon = new Pokedex(request.PokemonID, request.PokemonName, request.PokemonDescription);

        // ส่ง typeIds ไปด้วย
        var result = service.CreatePokemon(pokemon, request.TypeIds);

        return Results.Created($"/api/pokemon/{pokemon.PokemonID}", result);
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message); // ส่งกลับข้อความผิดพลาดหากเกิดข้อผิดพลาด
    }
});



// Update an existing Pokemon
app.MapPut("/api/pokemon", (Pokedex pokemon, PokedexService service) =>
{
    service.Update(pokemon);
    return Results.NoContent();
});

// Delete a Pokemon by ID
app.MapDelete("/api/pokemon/{id}", (int id, PokedexService service) =>
{
    service.Delete(id);
    return Results.NoContent();
});

// Get all Pokemon
app.MapGet("/api/pokemon", (PokedexService service) =>
{
    var pokemons = service.GetAllPokemons();
    return Results.Ok(pokemons);
});

app.Run();
