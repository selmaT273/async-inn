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
    }
}
