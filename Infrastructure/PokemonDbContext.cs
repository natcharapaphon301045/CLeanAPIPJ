using Microsoft.EntityFrameworkCore;
using CleanAPIPJ.Domain.Entities;

namespace CleanAPIPJ.Infrastructure
{
    public class PokemonDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Pokedex> Pokedex { get; set; }

        // ใช้ชื่อ PokemonDbContext แทน DbContext
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
            : base(options)
        {
        }
    }
}
