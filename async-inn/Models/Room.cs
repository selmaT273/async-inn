using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace async_inn.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public LayoutType Layout { get; set; }

        public List<RoomAmenity> RoomAmenities { get; set; }

        public enum LayoutType
        { 
            Studio,
            OneBedroom,
            TwoBedroom
        }
    }
}
