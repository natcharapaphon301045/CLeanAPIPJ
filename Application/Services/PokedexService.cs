using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using CleanAPIPJ.Infrastructure;


namespace CleanAPIPJ.Application.Services
{
    public class PokedexService
    {
        private readonly IPokedexRepository _repository;
        private readonly PokemonDbContext _dbContext;

        public PokedexService(IPokedexRepository repository,PokemonDbContext dbContext)
        {
            _repository = repository;
            _dbContext = dbContext;
        }
/*------------------------------------------------API POST-----------------------------------------------------*/  
   public object CreatePokemon(Pokedex pokemon, List<int> typeIds)
    {
        // ตรวจสอบว่า PokemonID ซ้ำหรือไม่
        var existingPokemon = _repository.GetById(pokemon.PokemonID);
        if (existingPokemon != null)
        {
            return new { message = "PokemonID นี้มีอยู่แล้วในระบบ" };
        }
        // เพิ่ม Pokémon และความสัมพันธ์
        _repository.AddWithTypes(pokemon, typeIds);
        return new { message = "เพิ่มข้อมูลสำเร็จ", pokemon };
    }
/*------------------------------------UPDATE---------------------------------------------------*/
        public void Update(Pokedex pokemon) => _repository.Update(pokemon);
/*-----------------------------------DELETE----------------------------------------------------*/
        public void Delete(int id)
        {
            var pokemon = _dbContext.Pokedex.Find(id);
            if (pokemon == null)
            {
                throw new KeyNotFoundException($"ไม่พบ Pokémon ที่จะลบ ด้วย ID {id}");
            }

            _dbContext.Pokedex.Remove(pokemon); // ลบ Pokemon
            _dbContext.SaveChanges(); // บันทึกการเปลี่ยนแปลง
        }
/*--------------------------------------GET-------------------------------------------------*/
        public List<object> GetAllPokemons()
        {
            // ดึงข้อมูล Pokémon
            var pokemons = _dbContext.Pokedex
                .Include(p => p.PokemonType) // Include PokemonTypeRelation
                .ThenInclude(pt => pt.PokemonType) // Include PokemonType เพื่อเข้าถึง TypeName
                .Select(p => new
            {
                PokemonID = p.PokemonID,
                PokemonName = p.PokemonName,
                PokemonDescription = p.PokemonDescription,
                PokemonType = p.PokemonType.Select(pt => pt.PokemonType.TypeName) // ดึง TypeName จาก PokemonType
            })
            .ToList<object>();

            // ตรวจสอบว่าไม่มีข้อมูล Pokémon หรือไม่
            if (pokemons.Count == 0)
            {
                // ถ้าไม่มีข้อมูล Pokémon ให้แสดงข้อความ "ไม่มีข้อมูลจ้าา"
                return new List<object> { "ไม่มีข้อมูลจ้าา" };
            }
            // ถ้ามีข้อมูล Pokémon ให้คืนค่าตามปกติ
            return pokemons;
        }
/*---------------------------GET by id------------------------------------------------------------*/
        public object GetPokemonById(int id) 
        {
            var pokemon = _dbContext.Pokedex
            .Where(p => p.PokemonID == id)
            .Include(p => p.PokemonType)
            .ThenInclude( pt => pt.PokemonType)
            .Select(p=> new{
                PokemonID = p.PokemonID,
                PokemonName = p.PokemonName,
                PokemonDescription = p.PokemonDescription,
                PokemonType = p.PokemonType.Select(pt => pt.PokemonType.TypeName)
            })
            .FirstOrDefault();
             if (pokemon == null)
                {
                    return Results.NotFound(new { message = "หาไม่เจอจ้า" });  // คืนค่า null เมื่อไม่พบ Pokémon
                }
                return pokemon; 
        }
    }
}
