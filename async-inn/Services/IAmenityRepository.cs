using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Models;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Services
{
    public interface IAmenityRepository
    {
        Task<ActionResult<IEnumerable<Amenity>>> GetAll();
        Task<ActionResult<Amenity>> GetById(int id);
        Task<bool> UpdateAmenity(int id, Amenity amenity);
        Task CreateAmenity(Amenity amenity);
    }
}