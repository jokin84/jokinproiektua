using Microsoft.EntityFrameworkCore.Migrations;

namespace proiektua2.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Erabiltzailea",
                table: "BezeroaEskaera");

            migrationBuilder.AddColumn<int>(
                name: "Erabiltzaileaid",
                table: "BezeroaEskaera",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Erabiltzaileaid",
                table: "BezeroaEskaera");

            migrationBuilder.AddColumn<string>(
                name: "Erabiltzailea",
                table: "BezeroaEskaera",
                type: "text",
                nullable: true);
        }
    }
}
