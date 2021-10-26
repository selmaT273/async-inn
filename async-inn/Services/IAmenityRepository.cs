using System.Collections.Generic;
using System.Threading.Tasks;
using async_inn.Models;
using async_inn.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace async_inn.Services
{
    public interface IAmenityRepository
    {
        Task<ActionResult<IEnumerable<AmenityDTO>>> GetAll();
        Task<ActionResult<AmenityDTO>> GetById(int id);
        Task<bool> UpdateAmenity(int id, Amenity amenity);
        Task CreateAmenity(Amenity amenity);
        Task<ActionResult<bool>> RemoveAmenity(int id);
    }
}