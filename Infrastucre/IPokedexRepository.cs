using CleanAPIPJ.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace CleanAPIPJ.Infrastructure.Repositories
{
    public class PokedexRepository
    {
        private readonly List<Pokedex> _pokedexList = new List<Pokedex>();

        public void Add(Pokedex pokemon) => _pokedexList.Add(pokemon);
        public Pokedex GetById(int id) => _pokedexList.FirstOrDefault(p => p.PokemonID == id);
        public void Update(Pokedex pokemon)
        {
            var existingPokemon = _pokedexList.FirstOrDefault(p => p.PokemonID == pokemon.PokemonID);
            if (existingPokemon != null)
            {
                existingPokemon.PokemonName = pokemon.PokemonName;
                existingPokemon.PokemonDescription = pokemon.PokemonDescription;
            }
        }
        public void Remove(int id) => _pokedexList.RemoveAll(p => p.PokemonID == id);
        public List<Pokedex> GetAll() => _pokedexList;
    }
}
