using Microsoft.EntityFrameworkCore.Migrations;

namespace Prodavnica2.Migrations
{
    public partial class Porudzbine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    PorudzbinaID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(nullable: false),
                    Adresa1 = table.Column<string>(nullable: false),
                    Adresa2 = table.Column<string>(nullable: true),
                    ImeGrada = table.Column<string>(nullable: false),
                    Drzava = table.Column<string>(nullable: false),
                    PostanskiBroj = table.Column<string>(nullable: false),
                    Poklon = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Porudzbine", x => x.PorudzbinaID);
                });

            migrationBuilder.CreateTable(
                name: "KorpaElement",
                columns: table => new
                {
                    KorpaElementID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProizvodID = table.Column<int>(nullable: true),
                    Kolicina = table.Column<int>(nullable: false),
                    PorudzbinaID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaElement", x => x.KorpaElementID);
                    table.ForeignKey(
                        name: "FK_KorpaElement_Porudzbine_PorudzbinaID",
                        column: x => x.PorudzbinaID,
                        principalTable: "Porudzbine",
                        principalColumn: "PorudzbinaID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorpaElement_Proizvodi_ProizvodID",
                        column: x => x.ProizvodID,
                        principalTable: "Proizvodi",
                        principalColumn: "ProizvodID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorpaElement_PorudzbinaID",
                table: "KorpaElement",
                column: "PorudzbinaID");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaElement_ProizvodID",
                table: "KorpaElement",
                column: "ProizvodID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KorpaElement");

            migrationBuilder.DropTable(
                name: "Porudzbine");
        }
    }
}
