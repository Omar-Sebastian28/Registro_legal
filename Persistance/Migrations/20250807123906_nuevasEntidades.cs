using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroLegal.Infraestructura.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class nuevasEntidades : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Carpetas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carpetas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InfraccionCarpetas",
                columns: table => new
                {
                    CarpetaId = table.Column<int>(type: "int", nullable: false),
                    InfraccionId = table.Column<int>(type: "int", nullable: false),
                    FechaAgregado = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InfraccionCarpetas", x => new { x.InfraccionId, x.CarpetaId });
                    table.ForeignKey(
                        name: "FK_InfraccionCarpetas_Carpetas_CarpetaId",
                        column: x => x.CarpetaId,
                        principalTable: "Carpetas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InfraccionCarpetas_Ilicito_InfraccionId",
                        column: x => x.InfraccionId,
                        principalTable: "Ilicito",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InfraccionCarpetas_CarpetaId",
                table: "InfraccionCarpetas",
                column: "CarpetaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InfraccionCarpetas");

            migrationBuilder.DropTable(
                name: "Carpetas");
        }
    }
}
