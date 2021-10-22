using System;
using System.ComponentModel.DataAnnotations;

namespace async_inn.Models
{
    public class HotelRoom
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomId { get; set; }

        // Navigation Properties
        public Hotel Hotel { get; set; }

        public Room Room { get; set; }
    }
}
