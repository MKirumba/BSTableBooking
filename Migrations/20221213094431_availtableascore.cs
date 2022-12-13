using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class availtableascore : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Session_Booking_BookingID",
                table: "Session");

            migrationBuilder.DropIndex(
                name: "IX_Session_BookingID",
                table: "Session");

            migrationBuilder.DropColumn(
                name: "BookingID",
                table: "Session");

            migrationBuilder.AddColumn<int>(
                name: "AvailTablesSessionID",
                table: "Booking",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_AvailTablesSessionID",
                table: "Booking",
                column: "AvailTablesSessionID");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailTables_AvailTablesSessionID",
                table: "Booking",
                column: "AvailTablesSessionID",
                principalTable: "AvailTables",
                principalColumn: "SessionID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailTables_AvailTablesSessionID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AvailTablesSessionID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "AvailTablesSessionID",
                table: "Booking");

            migrationBuilder.AddColumn<int>(
                name: "BookingID",
                table: "Session",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Session_BookingID",
                table: "Session",
                column: "BookingID",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Booking_BookingID",
                table: "Session",
                column: "BookingID",
                principalTable: "Booking",
                principalColumn: "BookingID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
