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

namespace async_inn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomRepository rooms;
        private readonly AsyncInnDbContext _context;

        public RoomsController(IRoomRepository rooms, AsyncInnDbContext context)
        {
            this.rooms = rooms;
            _context = context;
        }

        // GET: api/Rooms
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Room>>> GetRooms()
        {
            return await rooms.GetAll();
        }

        // GET: api/Rooms/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> GetRoom(int id)
        {
            // TODO: Handle if id is not found

            return await rooms.GetById(id);
        }

        // PUT: api/Rooms/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRoom(int id, Room room)
        {
            if (id != room.Id)
            {
                return BadRequest();
            }

            if (!await rooms.UpdateRoom(id, room))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Rooms
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task PostRoom(Room room)
        {
            await rooms.CreateRoom(room);

        }

        // DELETE: api/Rooms/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteRoom(int id)
        {
            //TODO: Refactor to return BadRequest() or NotFound() or NoContent()
            return await rooms.RemoveRoom(id);
        }

        [HttpPost]
        [Route("{id}/Amenity/{amenityId}")]
        public async Task<IActionResult> AddAmenity(int id, int amenityId)
        {
            await rooms.AddAmenityToRoom(id, amenityId);
            return NoContent();
        }

        [HttpDelete]
        [Route("{id}/Amenity/{amenityId}")]
        public async Task<IActionResult> RemoveAmenity(int id, int amenityId)
        {
            await rooms.RemoveAmenityFromRoom(id, amenityId);
            return NoContent();
        }

        private bool RoomExists(int id)
        {
            return _context.Rooms.Any(e => e.Id == id);
        }
    }
}
