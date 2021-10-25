using System;
namespace async_inn.Models
{
    public class RoomAmenity
    {
        public int AmenityId { get; set; }
        public int RoomId { get; set; }


        public Amenity Amenity { get; set; }
        public Room Room { get; set; }
    }
}
