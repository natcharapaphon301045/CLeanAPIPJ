using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Domain.Interfaces;

namespace CleanAPIPJ.Application.Services
{
    public class PokedexService
    {
        private readonly IPokedexRepository _repository;

        public PokedexService(IPokedexRepository repository)
        {
            _repository = repository;
        }

        public void CreatePokemon(Pokedex pokemon) => _repository.Add(pokemon);
        public Pokedex GetPokemonById(int id) => _repository.GetById(id);
        public void UpdatePokemon(Pokedex pokemon) => _repository.Update(pokemon);
        public void DeletePokemon(int id) => _repository.Remove(id);
        public List<Pokedex> GetAllPokemons() => _repository.GetAll();
    }
}
