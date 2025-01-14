using CleanAPIPJ.Domain.Entities;

public class PokemonTypeRelation
{
    public int PokemonID { get; set; }
    public required Pokedex Pokedex { get; set; }

    public int TypeID { get; set; }
    public required PokemonType PokemonType { get; set; }
}
