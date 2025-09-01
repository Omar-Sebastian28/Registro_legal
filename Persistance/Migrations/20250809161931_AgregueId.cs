using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroLegal.Infraestructura.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AgregueId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InfraccionCarpetas",
                table: "InfraccionCarpetas");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "InfraccionCarpetas",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfraccionCarpetas",
                table: "InfraccionCarpetas",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_InfraccionCarpetas_InfraccionId",
                table: "InfraccionCarpetas",
                column: "InfraccionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_InfraccionCarpetas",
                table: "InfraccionCarpetas");

            migrationBuilder.DropIndex(
                name: "IX_InfraccionCarpetas_InfraccionId",
                table: "InfraccionCarpetas");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "InfraccionCarpetas");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InfraccionCarpetas",
                table: "InfraccionCarpetas",
                columns: new[] { "InfraccionId", "CarpetaId" });
        }
    }
}
