using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroLegal.Infraestructura.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class mejoraBorrado : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfraccionCarpetas_Carpetas_CarpetaId",
                table: "InfraccionCarpetas");

            migrationBuilder.AddForeignKey(
                name: "FK_InfraccionCarpetas_Carpetas_CarpetaId",
                table: "InfraccionCarpetas",
                column: "CarpetaId",
                principalTable: "Carpetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InfraccionCarpetas_Carpetas_CarpetaId",
                table: "InfraccionCarpetas");

            migrationBuilder.AddForeignKey(
                name: "FK_InfraccionCarpetas_Carpetas_CarpetaId",
                table: "InfraccionCarpetas",
                column: "CarpetaId",
                principalTable: "Carpetas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
