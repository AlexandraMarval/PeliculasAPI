using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

#nullable disable

namespace PeliculasAPI.Migrations
{
    /// <inheritdoc />
    public partial class SalaDeCineUbicacion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalaDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropIndex(
                name: "IX_PeliculasSalasDeCines_SalaDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.AddColumn<Point>(
                name: "Ubicacion",
                table: "SalasDeCine",
                type: "geography",
                nullable: false);

            migrationBuilder.AddColumn<int>(
                name: "SalasDeCineId",
                table: "PeliculasSalasDeCines",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCines_SalasDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalasDeCineId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalasDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalasDeCineId",
                principalTable: "SalasDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalasDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropIndex(
                name: "IX_PeliculasSalasDeCines_SalasDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.DropColumn(
                name: "Ubicacion",
                table: "SalasDeCine");

            migrationBuilder.DropColumn(
                name: "SalasDeCineId",
                table: "PeliculasSalasDeCines");

            migrationBuilder.CreateIndex(
                name: "IX_PeliculasSalasDeCines_SalaDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineId");

            migrationBuilder.AddForeignKey(
                name: "FK_PeliculasSalasDeCines_SalasDeCine_SalaDeCineId",
                table: "PeliculasSalasDeCines",
                column: "SalaDeCineId",
                principalTable: "SalasDeCine",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
