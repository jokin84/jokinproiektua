using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace proiektua2.Migrations
{
    public partial class error1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "dirua",
                table: "Erosketa",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "pasahitzaAldatu",
                columns: table => new
                {
                    erabiltzeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PaswordOld = table.Column<string>(type: "text", nullable: true),
                    Pasword1 = table.Column<string>(type: "text", nullable: true),
                    Pasword2 = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PasaitzaAldatu", x => x.erabiltzeId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "pasahitzaAldatu");

            migrationBuilder.DropColumn(
                name: "dirua",
                table: "Erosketa");
        }
    }
}
