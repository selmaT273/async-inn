using System;
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
    public class DatabaseRoomRepository : IRoomRepository
    {
        private readonly AsyncInnDbContext _context;

        public DatabaseRoomRepository(AsyncInnDbContext context)
        {
            _context = context;
        }

        public async Task AddAmenityToRoom(int roomId, int amenityId)
        {
            var roomAmenity = new RoomAmenity
            {
                RoomId = roomId,
                AmenityId = amenityId,
            };

            _context.RoomAmenities.Add(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task CreateRoom(Room room)
        {
            _context.Rooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task<List<RoomDTO>> GetAll()
        {
            List<RoomDTO> result = await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .Select(room => new RoomDTO
                {
                    Id = room.Id,
                    Name = room.Name,
                    Layout = room.Layout,
                    Amenities = room.RoomAmenities
                        .Select(ra => new AmenityDTO
                        {
                            Id = ra.AmenityId,
                            Name = ra.Amenity.Name
                        })
                        .ToList(),
                })
                .ToListAsync();

            return result;
        }

        public async Task<ActionResult<Room>> GetById(int id)
        {
            Room room = await _context.Rooms
                .Include(r => r.RoomAmenities)
                .ThenInclude(ra => ra.Amenity)
                .FirstOrDefaultAsync(r => r.Id == id);
            
            return room;
        }

        public async Task RemoveAmenityFromRoom(int roomId, int amenityId)
        {
            var roomAmenity = await _context.RoomAmenities
                .FirstOrDefaultAsync(ra =>
                    ra.RoomId == roomId &&
                    ra.AmenityId == amenityId);

            _context.RoomAmenities.Remove(roomAmenity);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<bool>> RemoveRoom(int id)
        {
            var room = await _context.Rooms.FindAsync(id);
            if (room == null)
            {
                return false;
            }

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return false;
            }

            _context.Entry(room).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Rooms.Any(e => e.Id == id))
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
