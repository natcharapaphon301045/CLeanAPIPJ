using Microsoft.EntityFrameworkCore;
using CleanAPIPJ.Domain.Entities;

namespace CleanAPIPJ.Infrastructure
{
    public class PokemonDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<Pokedex> Pokedex { get; set; }
        public DbSet<PokemonType> PokemonType { get; set; }
        public DbSet<PokemonTypeRelation> PokemonTypeRelations { get; set; }

        
        public PokemonDbContext(DbContextOptions<PokemonDbContext> options)
            : base(options)
        {
            Pokedex = Set<Pokedex>();
            PokemonType = Set<PokemonType>();
            PokemonTypeRelations = Set<PokemonTypeRelation>();

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // กำหนด Primary Key ให้กับ Pokedex
            modelBuilder.Entity<Pokedex>()
                .HasKey(p => p.PokemonID);

            modelBuilder.Entity<PokemonType>()
                .HasKey(pt => pt.TypeID); // กำหนด Primary Key ให้กับ PokemonType

            // Configuring the junction table for many-to-many relationship
            modelBuilder.Entity<PokemonTypeRelation>()
                .HasKey(ptr => new { ptr.PokemonID, ptr.TypeID });

            modelBuilder.Entity<PokemonTypeRelation>();
                modelBuilder.Entity<PokemonTypeRelation>()
                .HasOne(ptr => ptr.Pokedex)
                .WithMany(p => p.PokemonType)
                .HasForeignKey(ptr => ptr.PokemonID)
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
