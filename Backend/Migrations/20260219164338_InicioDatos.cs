using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Backend.Migrations
{
    /// <inheritdoc />
    public partial class InicioDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Localidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Localidades", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Profesores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profesores", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Email = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TipoRol = table.Column<int>(type: "int", nullable: false),
                    FechaRegistracion = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: false),
                    Dni = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Observacion = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Socios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Dni = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Domicilio = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Telefono = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Activo = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LocalidadId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Socios_Localidades_LocalidadId",
                        column: x => x.LocalidadId,
                        principalTable: "Localidades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Actividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Imagen = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ProfesorId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actividades_Profesores_ProfesorId",
                        column: x => x.ProfesorId,
                        principalTable: "Profesores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    HoraFin = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    CupoMaximo = table.Column<int>(type: "int", nullable: false),
                    Activa = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clases_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SocioActividades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    ActividadId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocioActividades", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SocioActividades_Actividades_ActividadId",
                        column: x => x.ActividadId,
                        principalTable: "Actividades",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SocioActividades_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Asistencias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Fecha = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Presente = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    SocioId = table.Column<int>(type: "int", nullable: false),
                    ClaseId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asistencias", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asistencias_Clases_ClaseId",
                        column: x => x.ClaseId,
                        principalTable: "Clases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asistencias_Socios_SocioId",
                        column: x => x.SocioId,
                        principalTable: "Socios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Localidades",
                columns: new[] { "Id", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, false, "San justo" },
                    { 2, false, "Videla" },
                    { 3, false, "Gobernador Crespo" },
                    { 4, false, "San Martín Norte" },
                    { 5, false, "Ramayón" }
                });

            migrationBuilder.InsertData(
                table: "Profesores",
                columns: new[] { "Id", "Activo", "IsDeleted", "Nombre" },
                values: new object[,]
                {
                    { 1, true, false, "Juan Perez" },
                    { 2, true, false, "Carlos Gomez" },
                    { 3, true, false, "Pablo Garcia" },
                    { 4, true, false, "Maria Lopez" },
                    { 5, true, false, "Camila Fernandez" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Dni", "Domicilio", "Email", "FechaRegistracion", "IsDeleted", "Nombre", "Observacion", "Password", "Telefono", "TipoRol" },
                values: new object[,]
                {
                    { 1, "10000001", "Calle 1", "juan1@mail.com", new DateTimeOffset(new DateTime(2023, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Juan Pérez", "", "pass1", "1111111", 0 },
                    { 2, "10000002", "Calle 2", "ana2@mail.com", new DateTimeOffset(new DateTime(2023, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Ana Gómez", "", "pass2", "2222222", 1 },
                    { 3, "10000003", "Calle 3", "luis3@mail.com", new DateTimeOffset(new DateTime(2023, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Luis Torres", "", "pass3", "3333333", 0 },
                    { 4, "10000004", "Calle 4", "maria4@mail.com", new DateTimeOffset(new DateTime(2023, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "María López", "", "pass4", "4444444", 1 },
                    { 5, "10000005", "Calle 5", "pedro5@mail.com", new DateTimeOffset(new DateTime(2023, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), false, "Pedro Ruiz", "", "pass5", "5555555", 1 }
                });

            migrationBuilder.InsertData(
                table: "Actividades",
                columns: new[] { "Id", "Descripcion", "Imagen", "IsDeleted", "Nombre", "ProfesorId" },
                values: new object[,]
                {
                    { 1, "Clase de yoga para mejorar la flexibilidad y el bienestar general.", "portada1.jpg", false, "Yoga", 1 },
                    { 2, "Clase de pilates para fortalecer el core y mejorar la postura.", "portada2.jpg", false, "Pilates", 2 },
                    { 3, "Clase de zumba para quemar calorías y divertirse bailando.", "portada3.jpg", false, "Zumba", 3 },
                    { 4, "Clase de spinning para mejorar la resistencia cardiovascular.", "portada4.jpg", false, "Spinning", 4 },
                    { 5, "Clase de CrossFit para un entrenamiento intenso y variado.", "portada5.jpg", false, "CrossFit", 5 }
                });

            migrationBuilder.InsertData(
                table: "Socios",
                columns: new[] { "Id", "Activo", "Dni", "Domicilio", "FechaNacimiento", "IsDeleted", "LocalidadId", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, "Sofia Martinez", "" },
                    { 2, true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, "Luca Rodriguez", "" },
                    { 3, true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, "Valentina Gomez", "" },
                    { 4, true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4, "Mateo Fernandez", "" },
                    { 5, true, "", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, "Camila Sanchez", "" }
                });

            migrationBuilder.InsertData(
                table: "Clases",
                columns: new[] { "Id", "Activa", "ActividadId", "CupoMaximo", "DiaSemana", "HoraFin", "HoraInicio", "IsDeleted" },
                values: new object[,]
                {
                    { 1, true, 1, 20, 1, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), false },
                    { 2, true, 2, 15, 3, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 19, 0, 0, 0), false },
                    { 3, true, 3, 25, 5, new TimeSpan(0, 18, 0, 0, 0), new TimeSpan(0, 17, 0, 0, 0), false },
                    { 4, true, 4, 20, 2, new TimeSpan(0, 19, 0, 0, 0), new TimeSpan(0, 18, 0, 0, 0), false },
                    { 5, true, 5, 15, 4, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 19, 0, 0, 0), false }
                });

            migrationBuilder.InsertData(
                table: "SocioActividades",
                columns: new[] { "Id", "ActividadId", "IsDeleted", "SocioId" },
                values: new object[,]
                {
                    { 1, 1, false, 1 },
                    { 2, 2, false, 2 },
                    { 3, 3, false, 3 },
                    { 4, 4, false, 4 },
                    { 5, 5, false, 5 }
                });

            migrationBuilder.InsertData(
                table: "Asistencias",
                columns: new[] { "Id", "ClaseId", "Fecha", "IsDeleted", "Presente", "SocioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1 },
                    { 2, 1, new DateTime(2026, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 2 },
                    { 3, 2, new DateTime(2024, 2, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 1 },
                    { 4, 2, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, true, 3 },
                    { 5, 3, new DateTime(2026, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actividades_ProfesorId",
                table: "Actividades",
                column: "ProfesorId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_ClaseId",
                table: "Asistencias",
                column: "ClaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Asistencias_SocioId",
                table: "Asistencias",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Clases_ActividadId",
                table: "Clases",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_SocioActividades_ActividadId",
                table: "SocioActividades",
                column: "ActividadId");

            migrationBuilder.CreateIndex(
                name: "IX_SocioActividades_SocioId",
                table: "SocioActividades",
                column: "SocioId");

            migrationBuilder.CreateIndex(
                name: "IX_Socios_LocalidadId",
                table: "Socios",
                column: "LocalidadId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asistencias");

            migrationBuilder.DropTable(
                name: "SocioActividades");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Clases");

            migrationBuilder.DropTable(
                name: "Socios");

            migrationBuilder.DropTable(
                name: "Actividades");

            migrationBuilder.DropTable(
                name: "Localidades");

            migrationBuilder.DropTable(
                name: "Profesores");
        }
    }
}
