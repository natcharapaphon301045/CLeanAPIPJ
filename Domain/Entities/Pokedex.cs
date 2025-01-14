using System.ComponentModel.DataAnnotations;

namespace CleanAPIPJ.Domain.Entities
{
    public class Pokedex
    {
        [Key]
        [Required]
        public int PokemonID { get; set; }  // Primary Key
        public string PokemonName { get; set; }
        public string PokemonDescription { get; set; }

        // คอลเลคชัน PokemonTypeRelation เพื่อรองรับความสัมพันธ์หลาย-หลาย
        public ICollection<PokemonTypeRelation> PokemonType { get; set; }

        public Pokedex(int pokemonID, string pokemonName, string pokemonDescription)
        {
            PokemonID = pokemonID;
            PokemonName = pokemonName;
            PokemonDescription = pokemonDescription;
            PokemonType = new List<PokemonTypeRelation>();
        }
    }
}
