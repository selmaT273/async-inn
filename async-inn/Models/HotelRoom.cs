using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace async_inn.Models
{
    public class HotelRoom
    {
        [Required]
        public int HotelId { get; set; }

        [Required]
        public int RoomNumber { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        [Column(TypeName="money")]
        public decimal Rate { get; set; }

        [Required]
        public bool PetFriendly { get; set; }

        // Navigation Properties
        public Hotel Hotel { get; set; }

        public Room Room { get; set; }
    }
}
