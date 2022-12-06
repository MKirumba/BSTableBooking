using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class qtysessionupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnitPrice",
                table: "BookingInfo");

            migrationBuilder.AddColumn<int>(
                name: "Qty",
                table: "BookingInfo",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qty",
                table: "BookingInfo");

            migrationBuilder.AddColumn<double>(
                name: "UnitPrice",
                table: "BookingInfo",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
