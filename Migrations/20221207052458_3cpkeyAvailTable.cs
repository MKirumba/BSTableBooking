using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BSTableBooking.Migrations
{
    public partial class _3cpkeyAvailTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables");

            migrationBuilder.AlterColumn<string>(
                name: "BookingSession",
                table: "BookingInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "AvailTables",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables",
                columns: new[] { "ProductId", "BookDay", "Session" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables");

            migrationBuilder.DropColumn(
                name: "Session",
                table: "AvailTables");

            migrationBuilder.AlterColumn<string>(
                name: "BookingSession",
                table: "BookingInfo",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AvailTables",
                table: "AvailTables",
                columns: new[] { "ProductId", "BookDay" });
        }
    }
}
