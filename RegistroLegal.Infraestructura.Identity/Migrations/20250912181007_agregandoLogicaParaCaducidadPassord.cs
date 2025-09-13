using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RegistroLegal.Infraestructura.Identity.Migrations
{
    /// <inheritdoc />
    public partial class agregandoLogicaParaCaducidadPassord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastPasswordChangedDate",
                schema: "Identity",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastPasswordChangedDate",
                schema: "Identity",
                table: "Users");
        }
    }
}
