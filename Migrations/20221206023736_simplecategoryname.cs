using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class simplecategoryname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionStartTime",
                table: "BookingInfo");

            migrationBuilder.AddColumn<string>(
                name: "CategoryName",
                table: "BookingInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryName",
                table: "BookingInfo");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionStartTime",
                table: "BookingInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
