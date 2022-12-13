using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class availtableascore9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailTables_AvalBook",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AvalBook",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "AvalBook",
                table: "Booking");

            migrationBuilder.AddForeignKey(
                name: "FK_AvailTables_Booking_SessionID",
                table: "AvailTables",
                column: "SessionID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AvailTables_Booking_SessionID",
                table: "AvailTables");

            migrationBuilder.AddColumn<int>(
                name: "AvalBook",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AvalBook",
                table: "Booking",
                column: "AvalBook",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailTables_AvalBook",
                table: "Booking",
                column: "AvalBook",
                principalTable: "AvailTables",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
