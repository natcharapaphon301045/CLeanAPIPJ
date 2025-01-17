using CleanAPIPJ.Domain.Entities;

namespace CleanAPIPJ.Domain.Interfaces
{
    public interface IPokedexRepository
    {
        void Add(Pokedex pokemon);
        Pokedex GetById(int id);
        void Update(Pokedex pokemon);
        void Remove(int id);
        List<Pokedex> GetAll();
    }
}
