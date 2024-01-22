using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SIDOK.Migrations
{
    /// <inheritdoc />
    public partial class AddTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Poli",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Lokasi = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Poli", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Spesialisasi",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gelar = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spesialisasi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dokter",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nama = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NIK = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TanggalLahir = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TempatLahir = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JenisKelamin = table.Column<int>(type: "int", nullable: false),
                    SpesialisasiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dokter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dokter_Spesialisasi_SpesialisasiId",
                        column: x => x.SpesialisasiId,
                        principalTable: "Spesialisasi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jadwal_Jaga",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hari = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PoliId = table.Column<int>(type: "int", nullable: false),
                    DokterId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jadwal_Jaga", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Jadwal_Jaga_Dokter_DokterId",
                        column: x => x.DokterId,
                        principalTable: "Dokter",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Jadwal_Jaga_Poli_PoliId",
                        column: x => x.PoliId,
                        principalTable: "Poli",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dokter_SpesialisasiId",
                table: "Dokter",
                column: "SpesialisasiId");

            migrationBuilder.CreateIndex(
                name: "IX_Jadwal_Jaga_DokterId",
                table: "Jadwal_Jaga",
                column: "DokterId");

            migrationBuilder.CreateIndex(
                name: "IX_Jadwal_Jaga_PoliId",
                table: "Jadwal_Jaga",
                column: "PoliId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jadwal_Jaga");

            migrationBuilder.DropTable(
                name: "Dokter");

            migrationBuilder.DropTable(
                name: "Poli");

            migrationBuilder.DropTable(
                name: "Spesialisasi");
        }
    }
}
