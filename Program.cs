using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Application.Services;
using CleanAPIPJ.Domain.Interfaces;
using CleanAPIPJ.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSingleton<IPokedexRepository, PokedexRepository>(); // Register Repository
builder.Services.AddSingleton<PokedexService>(); // Register Service

var app = builder.Build();

// Define Minimal API Endpoints here...

app.Run();
