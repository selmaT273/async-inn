using System.Collections.Generic;
using System.Linq;
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

        public async Task CreateAmenity(Amenity amenity)
        {
            _context.Amenities.Add(amenity);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Amenity>>> GetAll()
        {
            return await _context.Amenities.ToListAsync();
        }

        public async Task<ActionResult<Amenity>> GetById(int id)
        {
            return await _context.Amenities.FindAsync(id);
        }

        public async Task<ActionResult<bool>> RemoveAmenity(int id)
        {
            var amenity = await _context.Amenities.FindAsync(id);
            if (amenity == null)
            {
                return false;
            }

            _context.Amenities.Remove(amenity);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateAmenity(int id, Amenity amenity)
        {
            if (id != amenity.Id)
            {
                return false;
            }

            _context.Entry(amenity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Hotels.Any(e => e.Id == id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }
    }
}