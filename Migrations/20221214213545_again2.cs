using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class again2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_AvailTables_SessionID",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_SessionID",
                table: "Booking");

            migrationBuilder.CreateTable(
                name: "AvailTablesBooking",
                columns: table => new
                {
                    BookingID = table.Column<int>(type: "int", nullable: false),
                    SessionID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AvailTablesBooking", x => new { x.BookingID, x.SessionID });
                    table.ForeignKey(
                        name: "FK_AvailTablesBooking_AvailTables_SessionID",
                        column: x => x.SessionID,
                        principalTable: "AvailTables",
                        principalColumn: "SessionID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AvailTablesBooking_Booking_BookingID",
                        column: x => x.BookingID,
                        principalTable: "Booking",
                        principalColumn: "BookingID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AvailTablesBooking_SessionID",
                table: "AvailTablesBooking",
                column: "SessionID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AvailTablesBooking");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_SessionID",
                table: "Booking",
                column: "SessionID",
                unique: true,
                filter: "[SessionID] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_AvailTables_SessionID",
                table: "Booking",
                column: "SessionID",
                principalTable: "AvailTables",
                principalColumn: "SessionID");
        }
    }
}
