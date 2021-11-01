using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Models;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Services
{
    public interface IHotelRoomRepository
    {
        Task<ActionResult<IEnumerable<HotelRoom>>> GetAll(int hotelId);
        Task<ActionResult<HotelRoom>> GetByRoomNumber(int hotelId, int roomNumber);
    }
}
