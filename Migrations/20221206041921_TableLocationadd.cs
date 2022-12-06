using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class TableLocationadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TableLocation",
                table: "BookingInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TableLocation",
                table: "BookingInfo");
        }
    }
}
