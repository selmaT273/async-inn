using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Data;
using async_inn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace async_inn.Services
{
    public class DatabaseAmenitiyRepository : IAmenityRepository
    {
        private readonly AsyncInnDbContext _context;

        public DatabaseAmenitiyRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<Amenity>>> GetAll()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<ActionResult<Amenity>> GetById(int id)
        {
            return await _context.Amenities.FindAsync(id);
        }
    }
}