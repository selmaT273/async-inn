using System;
using System.ComponentModel.DataAnnotations;

namespace async_inn.Models.DTO
{
    public class AmenityDTO
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
