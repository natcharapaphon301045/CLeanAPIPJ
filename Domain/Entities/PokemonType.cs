using CleanAPIPJ.Domain.Entities;
using System.ComponentModel.DataAnnotations; // Add this line

public class PokemonType
{
    [Required]
    public int TypeID { get; set; }
    [Required]
    public string TypeName { get; set; }

    public ICollection<PokemonTypeRelation> PokemonRelation { get; set; }
    public PokemonType(string typeName)
    {
        TypeName = typeName;
        PokemonRelation = new List<PokemonTypeRelation>(); // เริ่มต้นคอลเลกชัน
    }

}
