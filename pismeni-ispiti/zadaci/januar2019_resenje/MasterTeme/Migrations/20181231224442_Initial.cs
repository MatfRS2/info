using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MasterTeme.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Nastavnici",
                columns: table => new
                {
                    NastavnikId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Nastavnici", x => x.NastavnikId);
                });

            migrationBuilder.CreateTable(
                name: "Studenti",
                columns: table => new
                {
                    StudentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Ime = table.Column<string>(nullable: true),
                    Prezime = table.Column<string>(nullable: true),
                    Indeks = table.Column<string>(nullable: true),
                    Smer = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Studenti", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "MasterTeme",
                columns: table => new
                {
                    MasterTemaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Naziv = table.Column<string>(nullable: true),
                    StudentId = table.Column<int>(nullable: true),
                    MentorNastavnikId = table.Column<int>(nullable: true),
                    DatumNNV = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTeme", x => x.MasterTemaId);
                    table.ForeignKey(
                        name: "FK_MasterTeme_Nastavnici_MentorNastavnikId",
                        column: x => x.MentorNastavnikId,
                        principalTable: "Nastavnici",
                        principalColumn: "NastavnikId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MasterTeme_Studenti_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Studenti",
                        principalColumn: "StudentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "KomisijaElement",
                columns: table => new
                {
                    KomisijaElementId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NastavnikId = table.Column<int>(nullable: true),
                    MasterTemaId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KomisijaElement", x => x.KomisijaElementId);
                    table.ForeignKey(
                        name: "FK_KomisijaElement_MasterTeme_MasterTemaId",
                        column: x => x.MasterTemaId,
                        principalTable: "MasterTeme",
                        principalColumn: "MasterTemaId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_KomisijaElement_Nastavnici_NastavnikId",
                        column: x => x.NastavnikId,
                        principalTable: "Nastavnici",
                        principalColumn: "NastavnikId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_KomisijaElement_MasterTemaId",
                table: "KomisijaElement",
                column: "MasterTemaId");

            migrationBuilder.CreateIndex(
                name: "IX_KomisijaElement_NastavnikId",
                table: "KomisijaElement",
                column: "NastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTeme_MentorNastavnikId",
                table: "MasterTeme",
                column: "MentorNastavnikId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterTeme_StudentId",
                table: "MasterTeme",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "KomisijaElement");

            migrationBuilder.DropTable(
                name: "MasterTeme");

            migrationBuilder.DropTable(
                name: "Nastavnici");

            migrationBuilder.DropTable(
                name: "Studenti");
        }
    }
}
