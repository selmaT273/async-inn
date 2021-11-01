using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using async_inn.Data;
using async_inn.Models;
using async_inn.Models.DTO;
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

        public async Task<ActionResult<IEnumerable<AmenityDTO>>> GetAll()
        {
            List<AmenityDTO> result = await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    Id = amenity.Id,
                    Name = amenity.Name
                })
                .ToListAsync();

            return result;
        }

        public async Task<ActionResult<AmenityDTO>> GetById(int id)
        {
            AmenityDTO result = await _context.Amenities
                .Select(amenity => new AmenityDTO
                {
                    Id = amenity.Id,
                    Name = amenity.Name,
                })
                .FirstOrDefaultAsync(amenity => amenity.Id == id);

            return result;
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