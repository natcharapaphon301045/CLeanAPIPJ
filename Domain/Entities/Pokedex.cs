namespace CleanAPIPJ.Domain.Entities
{
    public class Pokedex
    {
        public int PokemonID {get; set;}
    public string PokemonName {get; set;}
    public string PokemonDescription {get; set;}

        public Pokedex(int pokemonID, string pokemonName, string pokemonDescription)
        {
            PokemonID = pokemonID;
            PokemonName = pokemonName;
            PokemonDescription = pokemonDescription;
        }
    }
}
