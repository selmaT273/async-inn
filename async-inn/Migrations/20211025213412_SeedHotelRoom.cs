using Microsoft.EntityFrameworkCore.Migrations;

namespace async_inn.Migrations
{
    public partial class SeedHotelRoom : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.InsertData(
            //    table: "HotelRooms",
            //    columns: new[] { "HotelId", "RoomId" },
            //    values: new object[] { 2, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DeleteData(
            //    table: "HotelRooms",
            //    keyColumns: new[] { "HotelId", "RoomId" },
            //    keyValues: new object[] { 2, 1 });
        }
    }
}
