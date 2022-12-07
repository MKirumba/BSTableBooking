using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class cpkeyAvailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables",
                columns: new[] { "ProductId", "BookDay" });

            migrationBuilder.CreateIndex(
                name: "IX_AvailTables_ProductId",
                table: "AvailTables",
                column: "ProductId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables");

            migrationBuilder.DropIndex(
                name: "IX_AvailTables_ProductId",
                table: "AvailTables");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables",
                column: "ProductId");
        }
    }
}
