using MyProject.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MyProject.Application.Services
{
    public class PokedexService
    {
        private readonly List<Pokedex> _pokedexList = new List<Pokedex>();

        // Create: Add a new Pokémon to the list
        public void CreatePokemon(Pokedex pokemon)
        {
            _pokedexList.Add(pokemon);
        }

        // Read: Get Pokémon by ID
        public Pokedex GetPokemonById(int pokemonID)
        {
            return _pokedexList.FirstOrDefault(p => p.PokemonID == pokemonID);
        }

        // Update: Modify an existing Pokémon's details
        public void UpdatePokemon(Pokedex pokemon)
        {
            var existingPokemon = _pokedexList.FirstOrDefault(p => p.PokemonID == pokemon.PokemonID);
            if (existingPokemon != null)
            {
                existingPokemon.PokemonName = pokemon.PokemonName;
                existingPokemon.PokemonDescription = pokemon.PokemonDescription;
            }
        }

        // Delete: Remove a Pokémon by ID
        public void DeletePokemon(int pokemonID)
        {
            var pokemon = _pokedexList.FirstOrDefault(p => p.PokemonID == pokemonID);
            if (pokemon != null)
            {
                _pokedexList.Remove(pokemon);
            }
        }

        // List all Pokémon
        public List<Pokedex> GetAllPokemons()
        {
            return _pokedexList;
        }
    }
}
