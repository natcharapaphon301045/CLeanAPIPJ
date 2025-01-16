using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Domain.Interfaces;

namespace CleanAPIPJ.Infrastructure.Repositories
{
    public class PokedexRepository : IPokedexRepository
    {
        private readonly PokemonDbContext _context;

        // เปลี่ยนจาก DbContext เป็น PokemonDbContext
        public PokedexRepository(PokemonDbContext context)
        {
            _context = context;
        }
/*--------------------------POST---------------------------------------*/
        // เพิ่ม Pokemon ใหม่
        public void AddWithTypes(Pokedex pokemon, List<int> typeIds)
        {
            // เพิ่ม Pokémon
            _context.Pokedex.Add(pokemon);

            // ตรวจสอบว่า TypeID ทั้งหมดมีอยู่ในระบบหรือไม่
            var validTypes = new List<PokemonType>();
            foreach (var typeId in typeIds)
            {
                var type = _context.PokemonType.Find(typeId);
                if (type != null)
                {
                    validTypes.Add(type); // เก็บ Type ที่ถูกต้อง
                }
                else
                {
                    // ถ้าพบ TypeID ที่ไม่ถูกต้อง ให้เพิ่มข้อความผิดพลาด
                    throw new InvalidOperationException($"TypeID {typeId} ไม่พบในระบบ");
                }
            }

            // สร้างความสัมพันธ์ระหว่าง Pokémon กับ TypeID
            foreach (var type in validTypes)
            {
                var relation = new PokemonTypeRelation
                {
                    PokemonID = pokemon.PokemonID,
                    TypeID = type.TypeID,
                    Pokedex = pokemon,
                    PokemonType = type
                };
                _context.PokemonTypeRelations.Add(relation);
            }

            // บันทึกข้อมูลทั้งหมดลงในฐานข้อมูล
            _context.SaveChanges();
        }
/*------------------------------Get by id ------------------------------------------------*/
        // ค้นหาหมายเลข Pokemon โดยใช้ ID
        public Pokedex? GetById(int id)
        {
            return _context.Pokedex.FirstOrDefault(p => p.PokemonID == id);
        }

/*---------------------------put ---------------------------------------*/
        // อัปเดตข้อมูล Pokemon
        public void Update(Pokedex pokemon)
        {
            _context.Pokedex.Update(pokemon); // อัปเดตข้อมูลใน DbSet
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลง
        }
/*----------------------------------delete--------------------------------*/
        // ลบ Pokemon ตาม ID
        public void Remove(int id)
        {
            var pokemon = _context.Pokedex.Find(id); // ค้นหา Pokemon จาก ID
            if (pokemon != null)
            {
                _context.Pokedex.Remove(pokemon); // ลบ Pokemon
                _context.SaveChanges(); // บันทึกการเปลี่ยนแปลง
            }
        }
/*-------------------------------------------get all----------------------------------------*/
        // ดึงรายการ Pokemon ทั้งหมด
        public List<Pokedex> GetAll()
        {
            return _context.Pokedex.ToList(); // ดึงข้อมูลทั้งหมดจาก DbSet
        }

        public void Add(Pokedex pokemon)
        {
            throw new NotImplementedException();
        }
    }
}
