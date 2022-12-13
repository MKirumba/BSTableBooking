using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class availtableascore5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailTables_AvailTablesSessionID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AvailTablesSessionID",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SessionID",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "AvailTablesSessionID",
                table: "Booking",
                newName: "AvalBook");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailTables_AvalBook",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_AvalBook",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "AvalBook",
                table: "Booking",
                newName: "AvailTablesSessionID");

            migrationBuilder.AddColumn<int>(
                name: "SessionID",
                table: "Booking",
                type: "int",
                nullable: true);

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
    }
}
