using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Prodavnica.Migrations
{
    public partial class Porudzbine : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Porudzbine",
                columns: table => new
                {
                    PorudzbinaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
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
                    table.PrimaryKey("PK_Porudzbine", x => x.PorudzbinaId);
                });

            migrationBuilder.CreateTable(
                name: "KorpaElement",
                columns: table => new
                {
                    KorpaElementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ProizvodId = table.Column<int>(nullable: true),
                    Kolicina = table.Column<int>(nullable: false),
                    PorudzbinaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KorpaElement", x => x.KorpaElementId);
                    table.ForeignKey(
                        name: "FK_KorpaElement_Porudzbine_PorudzbinaId",
                        column: x => x.PorudzbinaId,
                        principalTable: "Porudzbine",
                        principalColumn: "PorudzbinaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KorpaElement_Proizvodi_ProizvodId",
                        column: x => x.ProizvodId,
                        principalTable: "Proizvodi",
                        principalColumn: "ProizvodId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KorpaElement_PorudzbinaId",
                table: "KorpaElement",
                column: "PorudzbinaId");

            migrationBuilder.CreateIndex(
                name: "IX_KorpaElement_ProizvodId",
                table: "KorpaElement",
                column: "ProizvodId");
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
