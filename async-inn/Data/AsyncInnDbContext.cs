using System;
using async_inn.Models;
using Microsoft.EntityFrameworkCore;

namespace async_inn.Data
{
    public class AsyncInnDbContext : DbContext
    {
        // Allow context to be configured by magic
        public AsyncInnDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<HotelRoom> HotelRooms { get; set; }

        public DbSet<Amenity> Amenities { get; set; }

        public DbSet<RoomAmenity> RoomAmenities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelRoom>()
                .HasKey(h => new { h.HotelId, h.RoomNumber });

            modelBuilder.Entity<RoomAmenity>()
                .HasKey(r => new { r.AmenityId, r.RoomId });

            modelBuilder.Entity<HotelRoom>()
                .HasData(
                    new HotelRoom { HotelId = 2, RoomNumber = 1, RoomId = 1, Rate = 99.00m, PetFriendly = true }
                );
        }
    }
}
