using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using async_inn.Data;
using async_inn.Models;
using async_inn.Models.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace async_inn.Services
{
    public class DatabaseHotelRepository : IHotelRepository
    {
        private readonly AsyncInnDbContext _context;

        public DatabaseHotelRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task CreateHotel(Hotel hotel)
        {
            _context.Hotels.Add(hotel);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Hotel>> GetAll()
        {
            return await _context.Hotels.ToListAsync();
        }

        public async Task<Hotel> GetById(int id)
        {
            return await _context.Hotels.FindAsync(id);
        }

        public async Task<bool> RemoveHotel(int id)
        {
            var hotel = await _context.Hotels.FindAsync(id);
            if (hotel == null)
            {
                return false;
            }

            _context.Hotels.Remove(hotel);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateHotel(int id, Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return false;
            }

            _context.Entry(hotel).State = EntityState.Modified;

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
