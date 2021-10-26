using System;
using System.Collections.Generic;
using static async_inn.Models.Room;

namespace async_inn.Models.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public LayoutType Layout { get; set; }
        public List<AmenityDTO> Amenities { get; set; }
    }
}
