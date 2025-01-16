using CleanAPIPJ.Domain.Entities;


namespace CleanAPIPJ.Domain.Interfaces
{
    public interface IPokedexRepository
    {
        Pokedex GetById(int id);
        void Update(Pokedex pokemon);
        void Remove(int id);
        List<Pokedex> GetAll();

        void AddWithTypes(Pokedex pokemon, List<int> typeIds);
    }
}
