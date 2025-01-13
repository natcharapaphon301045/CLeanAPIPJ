using CleanAPIPJ.Domain.Entities;
using CleanAPIPJ.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

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

        // เพิ่ม Pokemon ใหม่
        public void Add(Pokedex pokemon)
        {
            _context.Pokemons.Add(pokemon); // เพิ่ม Pokemon ลงใน DbSet
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลงลงในฐานข้อมูล
        }

        // ค้นหาหมายเลข Pokemon โดยใช้ ID
        public Pokedex GetById(int id)
        {
            return _context.Pokemons.FirstOrDefault(p => p.PokemonID == id);
        }

        // อัปเดตข้อมูล Pokemon
        public void Update(Pokedex pokemon)
        {
            _context.Pokemons.Update(pokemon); // อัปเดตข้อมูลใน DbSet
            _context.SaveChanges(); // บันทึกการเปลี่ยนแปลง
        }

        // ลบ Pokemon ตาม ID
        public void Remove(int id)
        {
            var pokemon = _context.Pokemons.Find(id); // ค้นหา Pokemon จาก ID
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon); // ลบ Pokemon
                _context.SaveChanges(); // บันทึกการเปลี่ยนแปลง
            }
        }

        // ดึงรายการ Pokemon ทั้งหมด
        public List<Pokedex> GetAll()
        {
            return _context.Pokemons.ToList(); // ดึงข้อมูลทั้งหมดจาก DbSet
        }
    }
}
