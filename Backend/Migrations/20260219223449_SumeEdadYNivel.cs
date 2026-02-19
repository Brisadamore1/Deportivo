using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class SumeEdadYNivel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Beneficios",
                table: "Actividades",
                type: "text",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "EdadRecomendada",
                table: "Actividades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Nivel",
                table: "Actividades",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Beneficios", "EdadRecomendada", "Nivel" },
                values: new object[] { null, "Adultos", "Principiante" });

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Beneficios", "EdadRecomendada", "Nivel" },
                values: new object[] { null, "Adultos", "Principiante" });

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Beneficios", "EdadRecomendada", "Nivel" },
                values: new object[] { null, "Adultos", "Principiante" });

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Beneficios", "EdadRecomendada", "Nivel" },
                values: new object[] { null, "Adultos", "Principiante" });

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "Beneficios", "EdadRecomendada", "Nivel" },
                values: new object[] { null, "Adultos", "Principiante" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Beneficios",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "EdadRecomendada",
                table: "Actividades");

            migrationBuilder.DropColumn(
                name: "Nivel",
                table: "Actividades");
        }
    }
}
