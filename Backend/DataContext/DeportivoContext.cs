using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Backend.DataContext
{
    public class DeportivoContext : DbContext
    {
        public DeportivoContext()
        {
        }

        public DeportivoContext(DbContextOptions<DeportivoContext> options) : base(options)
        {
        }

        public virtual DbSet<Actividad> Actividades { get; set; }
        public virtual DbSet<Asistencia> Asistencias { get; set; }
        public virtual DbSet<Clase> Clases { get; set; }
        public virtual DbSet<Localidad> Localidades { get; set; }
        public virtual DbSet<Profesor> Profesores { get; set; }
        public virtual DbSet<Socio> Socios { get; set; }
        public virtual DbSet<SocioActividad> SocioActividades { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
            var cadenaConexion = configuration.GetConnectionString("mysqlRemoto");

            optionsBuilder.UseMySql(cadenaConexion, ServerVersion.AutoDetect(cadenaConexion));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region datos semillas de 5 actividades
            modelBuilder.Entity<Actividad>().HasData(
                new Actividad
                {
                    Id = 1,
                    Nombre = "Yoga",
                    Imagen = "portada1.jpg",
                    Descripcion = "Clase de yoga para mejorar la flexibilidad y el bienestar general.",
                    ProfesorId = 1,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 2,
                    Nombre = "Pilates",
                    Imagen = "portada2.jpg",
                    Descripcion = "Clase de pilates para fortalecer el core y mejorar la postura.",
                    ProfesorId = 2,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 3,
                    Nombre = "Zumba",
                    Imagen = "portada3.jpg",
                    Descripcion = "Clase de zumba para quemar calorías y divertirse bailando.",
                    ProfesorId = 3,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 4,
                    Nombre = "Spinning",
                    Imagen = "portada4.jpg",
                    Descripcion = "Clase de spinning para mejorar la resistencia cardiovascular.",
                    ProfesorId = 4,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 5,
                    Nombre = "CrossFit",
                    Imagen = "portada5.jpg",
                    Descripcion = "Clase de CrossFit para un entrenamiento intenso y variado.",
                    ProfesorId = 5,
                    IsDeleted = false
                }
            );
            #endregion

            #region datos semillas de 5 asistencias
            modelBuilder.Entity<Asistencia>().HasData(
                new Asistencia {
                    Id = 1,
                    Fecha = new DateTime(2026, 01, 28),
                    Presente = true,
                    SocioId = 1,
                    ClaseId = 1,
                    IsDeleted = false },
                new Asistencia {
                    Id = 2,
                    Fecha = new DateTime(2026, 01, 26),
                    Presente = false,
                    SocioId = 2,
                    ClaseId = 1,
                    IsDeleted = false },
                new Asistencia {
                    Id = 3,
                    Fecha = new DateTime(2024, 02, 6),
                    Presente = true,
                    SocioId = 1,
                    ClaseId = 2,
                    IsDeleted = false },
                new Asistencia {
                    Id = 4,
                    Fecha = new DateTime(2026, 02, 2),
                    Presente = true,
                    SocioId = 3,
                    ClaseId = 2,
                    IsDeleted = false },
                new Asistencia {
                    Id = 5,
                    Fecha = new DateTime(2026, 02, 2),
                    Presente = false,
                    SocioId = 2,
                    ClaseId = 3,
                    IsDeleted = false }
            );
            #endregion

            #region datos semillas de 5 clases
            modelBuilder.Entity<Clase>().HasData(
               new Clase {
                   Id = 1,
                   DiaSemana = DayOfWeek.Monday,
                   HoraInicio = new TimeSpan(18, 0, 0),
                   HoraFin = new TimeSpan(19, 0, 0),
                   CupoMaximo = 20,
                   Activa = true,
                   ActividadId = 1,
                   IsDeleted = false },
               new Clase {
                    Id = 2,
                    DiaSemana = DayOfWeek.Wednesday,
                    HoraInicio = new TimeSpan(19, 0, 0),
                    HoraFin = new TimeSpan(20, 0, 0),
                    CupoMaximo = 15,
                    Activa = true,
                    ActividadId = 2,
                    IsDeleted = false },
               new Clase {
                    Id = 3,
                    DiaSemana = DayOfWeek.Friday,
                    HoraInicio = new TimeSpan(17, 0, 0),
                    HoraFin = new TimeSpan(18, 0, 0),
                    CupoMaximo = 25,
                    Activa = true,
                    ActividadId = 3,
                    IsDeleted = false },
               new Clase {
                    Id = 4,
                    DiaSemana = DayOfWeek.Tuesday,
                    HoraInicio = new TimeSpan(18, 0, 0),
                    HoraFin = new TimeSpan(19, 0, 0),
                    CupoMaximo = 20,
                    Activa = true,
                    ActividadId = 4,
                    IsDeleted = false },
               new Clase {
                    Id = 5,
                    DiaSemana = DayOfWeek.Thursday,
                    HoraInicio = new TimeSpan(19, 0, 0),
                    HoraFin = new TimeSpan(20, 0, 0),
                    CupoMaximo = 15,
                    Activa = true,
                    ActividadId = 5,
                    IsDeleted = false }
            );
            #endregion

            #region datos semillas de 5 localidades
            modelBuilder.Entity<Localidad>().HasData(
                new Localidad { Id = 1, Nombre = "San justo", IsDeleted = false },
                new Localidad { Id = 2, Nombre = "Videla", IsDeleted = false },
                new Localidad { Id = 3, Nombre = "Gobernador Crespo", IsDeleted = false },
                new Localidad { Id = 4, Nombre = "San Martín Norte", IsDeleted = false },
                new Localidad { Id = 5, Nombre = "Ramayón", IsDeleted = false }
            );
            #endregion
        }
    }
}
