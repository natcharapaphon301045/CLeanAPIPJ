namespace CleanAPIPJ.Application.Requests
{
    public class CreatePokemonRequest
    {
        public int PokemonID { get; set; }
        public string PokemonName { get; set; }
        public string PokemonDescription { get; set; }
        public List<int> TypeIds { get; set; }
    }
}
