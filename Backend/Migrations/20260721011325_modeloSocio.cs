using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class modeloSocio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Socios",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 1,
                column: "EdadRecomendada",
                value: "Adulto");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 2,
                column: "EdadRecomendada",
                value: "Adulto");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 3,
                column: "EdadRecomendada",
                value: "Adulto");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 4,
                column: "EdadRecomendada",
                value: "Adulto");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 5,
                column: "EdadRecomendada",
                value: "Adulto");

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaNacimiento",
                value: null);

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaNacimiento",
                value: null);

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaNacimiento",
                value: null);

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaNacimiento",
                value: null);

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaNacimiento",
                value: null);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "FechaNacimiento",
                table: "Socios",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 1,
                column: "EdadRecomendada",
                value: "Adultos");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 2,
                column: "EdadRecomendada",
                value: "Adultos");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 3,
                column: "EdadRecomendada",
                value: "Adultos");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 4,
                column: "EdadRecomendada",
                value: "Adultos");

            migrationBuilder.UpdateData(
                table: "Actividades",
                keyColumn: "Id",
                keyValue: 5,
                column: "EdadRecomendada",
                value: "Adultos");

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaNacimiento",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaNacimiento",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaNacimiento",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaNacimiento",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Socios",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaNacimiento",
                value: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
