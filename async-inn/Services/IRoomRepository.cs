using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Models;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Services
{
    public interface IRoomRepository
    {
        Task<List<Room>> GetAll();
        Task<ActionResult<Room>> GetById(int id);
        Task CreateRoom(Room room);
        Task<bool> UpdateRoom(int id, Room room);
        Task<ActionResult<bool>> RemoveRoom(int id);
        Task AddAmenityToRoom(int amenityId, int roomId);
    }
}
