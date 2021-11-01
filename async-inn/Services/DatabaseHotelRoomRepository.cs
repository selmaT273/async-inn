using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using async_inn.Data;
using async_inn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace async_inn.Services
{
    public class DatabaseHotelRoomRepository : IHotelRoomRepository
    {
        private readonly AsyncInnDbContext _context;

        public DatabaseHotelRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetAll(int hotelId)
        {
            return await _context.HotelRooms
               .Where(hr => hr.HotelId == hotelId)
               .Include(hr => hr.Room)
               .ThenInclude(r => r.RoomAmenities)
               .ToListAsync();
        }

        public async Task<ActionResult<HotelRoom>> GetByRoomNumber(int hotelId, int roomNumber)
        {
            var hotelRoom = await _context.HotelRooms
                .Where(hr => hr.RoomNumber == roomNumber)
                .Include(hr => hr.Room)
                .ThenInclude(r => r.RoomAmenities)
                .FirstOrDefaultAsync();

            return hotelRoom;
        }
    }
}
