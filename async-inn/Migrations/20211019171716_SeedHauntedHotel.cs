using Microsoft.EntityFrameworkCore.Migrations;

namespace async_inn.Migrations
{
    public partial class SeedHauntedHotel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Hotels",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Haunted Hotel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Hotels",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
