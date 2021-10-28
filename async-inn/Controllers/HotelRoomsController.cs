using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using async_inn.Data;
using async_inn.Models;
using async_inn.Services;
using Microsoft.AspNetCore.Authorization;

namespace async_inn.Controllers
{
    [Authorize(Roles = "District Manager, Property Manager, Agent")]
    [Route("api/Hotels/{hotelId}/Rooms")]
    [ApiController]
    public class HotelRoomsController : ControllerBase
    {
        private readonly IHotelRoomRepository hotelRooms;
        private readonly AsyncInnDbContext _context;

        public HotelRoomsController(IHotelRoomRepository hotelRooms, AsyncInnDbContext context)
        {
            this.hotelRooms = hotelRooms;
            _context = context;
        }

        // GET: api/HotelRoom
        [Authorize(Roles = "District Manager, Property Manager")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HotelRoom>>> GetHotelRooms(int hotelId)
        {
            return await hotelRooms.GetAll(hotelId);
        }

        // GET: api/HotelRoom/5
        [HttpGet("{roomNumber}")]
        public async Task<ActionResult<HotelRoom>> GetHotelRoom(int hotelId, int roomNumber)
        {
             return await hotelRooms.GetByRoomNumber(hotelId, roomNumber);
        }

        // PUT: api/HotelRoom/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotelRoom(int id, HotelRoom hotelRoom)
        {
            if (id != hotelRoom.HotelId)
            {
                return BadRequest();
            }

            _context.Entry(hotelRoom).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HotelRoomExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/HotelRoom
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize(Roles = "District Manager, Property Manager")]
        [HttpPost]
        public async Task<ActionResult<HotelRoom>> PostHotelRoom(HotelRoom hotelRoom)
        {
            _context.HotelRooms.Add(hotelRoom);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (HotelRoomExists(hotelRoom.HotelId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetHotelRoom", new { id = hotelRoom.HotelId }, hotelRoom);
        }

        // DELETE: api/HotelRoom/5
        [Authorize(Roles = "District Manager")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotelRoom(int id)
        {
            var hotelRoom = await _context.HotelRooms.FindAsync(id);
            if (hotelRoom == null)
            {
                return NotFound();
            }

            _context.HotelRooms.Remove(hotelRoom);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HotelRoomExists(int id)
        {
            return _context.HotelRooms.Any(e => e.HotelId == id);
        }
    }
}
