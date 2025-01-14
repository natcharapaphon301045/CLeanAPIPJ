using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CleanAPIPJ.Infrastructure;

namespace CleanAPIPJ.Application.Services
{
    public class PokedexService
    {
        private readonly IPokedexRepository _repository;
        private readonly PokemonDbContext _dbContext;

        public PokedexService(IPokedexRepository repository,PokemonDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }

        public void CreatePokemon(Pokedex pokemon) => _repository.Add(pokemon);
        public Pokedex GetPokemonById(int id) => _repository.GetById(id);
        public void UpdatePokemon(Pokedex pokemon) => _repository.Update(pokemon);
        public void DeletePokemon(int id) => _repository.Remove(id);
        public List<object> GetAllPokemons()
        {
            // ดึงข้อมูล Pokémon
            var pokemons = _dbContext.Pokedex
                .Include(p => p.PokemonType) // Include PokemonTypeRelation
                .ThenInclude(pt => pt.PokemonType) // Include PokemonType เพื่อเข้าถึง TypeName
                .Select(p => new
            {
                PokemonID = p.PokemonID,
                PokemonName = p.PokemonName,
                PokemonDescription = p.PokemonDescription,
                PokemonType = p.PokemonType.Select(pt => pt.PokemonType.TypeName) // ดึง TypeName จาก PokemonType
            })
            .ToList<object>();

            // ตรวจสอบว่าไม่มีข้อมูล Pokémon หรือไม่
            if (pokemons.Count == 0)
            {
                // ถ้าไม่มีข้อมูล Pokémon ให้แสดงข้อความ "ไม่มีข้อมูลจ้าา"
                return new List<object> { "ไม่มีข้อมูลจ้าา" };
            }
            // ถ้ามีข้อมูล Pokémon ให้คืนค่าตามปกติ
            return pokemons;
        }
    }
}
