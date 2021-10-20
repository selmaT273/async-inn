using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Models.Services
{
    public interface IHotelRepository
    {
        Task<List<Hotel>> GetAll();

        Task<Hotel> GetById(int id);
    }
}
