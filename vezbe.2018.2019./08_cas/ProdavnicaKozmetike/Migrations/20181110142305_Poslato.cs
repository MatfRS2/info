using Microsoft.EntityFrameworkCore.Migrations;

namespace ProdavnicaKozmetike.Migrations
{
    public partial class Poslato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Poslato",
                table: "Porudzbine",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Poslato",
                table: "Porudzbine");
        }
    }
}
