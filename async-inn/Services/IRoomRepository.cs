using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Models;
using async_inn.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Services
{
    public interface IRoomRepository
    {
        Task<List<RoomDTO>> GetAll();
        Task<ActionResult<Room>> GetById(int id);
        Task CreateRoom(Room room);
        Task<bool> UpdateRoom(int id, Room room);
        Task<ActionResult<bool>> RemoveRoom(int id);
        Task AddAmenityToRoom(int roomId, int amenityId);
        Task RemoveAmenityFromRoom(int id, int amenityId);
    }
}
