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
                    EdadRecomendada = "Adultos",
                    Nivel = "Principiante",
                    ProfesorId = 1,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 2,
                    Nombre = "Pilates",
                    Imagen = "portada2.jpg",
                    Descripcion = "Clase de pilates para fortalecer el core y mejorar la postura.",
                    EdadRecomendada = "Adultos",
                    Nivel = "Principiante",
                    ProfesorId = 2,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 3,
                    Nombre = "Zumba",
                    Imagen = "portada3.jpg",
                    Descripcion = "Clase de zumba para quemar calorías y divertirse bailando.",
                    EdadRecomendada = "Adultos",
                    Nivel = "Principiante",
                    ProfesorId = 3,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 4,
                    Nombre = "Spinning",
                    Imagen = "portada4.jpg",
                    Descripcion = "Clase de spinning para mejorar la resistencia cardiovascular.",
                    EdadRecomendada = "Adultos",
                    Nivel = "Principiante",
                    ProfesorId = 4,
                    IsDeleted = false
                },
                new Actividad
                {
                    Id = 5,
                    Nombre = "CrossFit",
                    Imagen = "portada5.jpg",
                    Descripcion = "Clase de CrossFit para un entrenamiento intenso y variado.",
                    EdadRecomendada = "Adultos",
                    Nivel = "Principiante",
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

            #region datos semillas de 5 profesores
            modelBuilder.Entity<Profesor>().HasData( 
                new Profesor { Id = 1, Nombre = "Juan Perez", Activo = true, IsDeleted = false },
                new Profesor { Id = 2, Nombre = "Carlos Gomez", Activo = true, IsDeleted = false },
                new Profesor { Id = 3, Nombre = "Pablo Garcia", Activo = true, IsDeleted = false},
                new Profesor { Id = 4, Nombre = "Maria Lopez", Activo = true, IsDeleted = false },
                new Profesor { Id = 5, Nombre = "Camila Fernandez", Activo = true, IsDeleted = false }
                );
            #endregion

            #region datos semillas de 5 socios
            modelBuilder.Entity<Socio>().HasData(
                new Socio { Id = 1, Nombre = "Sofia Martinez", LocalidadId = 1, IsDeleted = false },
                new Socio { Id = 2, Nombre = "Luca Rodriguez", LocalidadId = 2, IsDeleted = false },
                new Socio { Id = 3, Nombre = "Valentina Gomez", LocalidadId = 3, IsDeleted = false },
                new Socio { Id = 4, Nombre = "Mateo Fernandez", LocalidadId = 4, IsDeleted = false },
                new Socio { Id = 5, Nombre = "Camila Sanchez", LocalidadId = 5, IsDeleted = false }
            );
            #endregion

            #region datos semillas de 5 socioactividades
            modelBuilder.Entity<SocioActividad>().HasData(
                new SocioActividad { Id = 1, SocioId = 1, ActividadId = 1, IsDeleted = false },
                new SocioActividad { Id = 2, SocioId = 2, ActividadId = 2, IsDeleted = false },
                new SocioActividad { Id = 3, SocioId = 3, ActividadId = 3, IsDeleted = false },
                new SocioActividad { Id = 4, SocioId = 4, ActividadId = 4, IsDeleted = false },
                new SocioActividad { Id = 5, SocioId = 5, ActividadId = 5, IsDeleted = false }
            );
            #endregion

            #region datos semillas de 5 usuarios
            modelBuilder.Entity<Usuario>().HasData(
               new Usuario { Id = 1, Nombre = "Juan Pérez", Email = "juan1@mail.com", Password = "pass1", TipoRol = Service.Enums.TipoRolEnum.Administrativo, FechaRegistracion = new DateTimeOffset(2023, 1, 1, 0, 0, 0, TimeSpan.Zero), Dni = "10000001", Domicilio = "Calle 1", Telefono = "1111111", Observacion = "", IsDeleted = false },
               new Usuario { Id = 2, Nombre = "Ana Gómez", Email = "ana2@mail.com", Password = "pass2", TipoRol = Service.Enums.TipoRolEnum.Profesor, FechaRegistracion = new DateTimeOffset(2023, 1, 2, 0, 0, 0, TimeSpan.Zero), Dni = "10000002", Domicilio = "Calle 2", Telefono = "2222222", Observacion = "", IsDeleted = false },
               new Usuario { Id = 3, Nombre = "Luis Torres", Email = "luis3@mail.com", Password = "pass3", TipoRol = Service.Enums.TipoRolEnum.Administrativo, FechaRegistracion = new DateTimeOffset(2023, 1, 3, 0, 0, 0, TimeSpan.Zero), Dni = "10000003", Domicilio = "Calle 3", Telefono = "3333333", Observacion = "", IsDeleted = false },
               new Usuario { Id = 4, Nombre = "María López", Email = "maria4@mail.com", Password = "pass4", TipoRol = Service.Enums.TipoRolEnum.Profesor, FechaRegistracion = new DateTimeOffset(2023, 1, 4, 0, 0, 0, TimeSpan.Zero), Dni = "10000004", Domicilio = "Calle 4", Telefono = "4444444", Observacion = "", IsDeleted = false },
               new Usuario { Id = 5, Nombre = "Pedro Ruiz", Email = "pedro5@mail.com", Password = "pass5", TipoRol = Service.Enums.TipoRolEnum.Profesor, FechaRegistracion = new DateTimeOffset(2023, 1, 5, 0, 0, 0, TimeSpan.Zero), Dni = "10000005", Domicilio = "Calle 5", Telefono = "5555555", Observacion = "", IsDeleted = false }
               );
            #endregion

            //configuramos los query filters para que no trigan los registros marcados como eliminados. Son los mecanimos por el cual se indica que un registro esta eliminado sin borrarlo fisicamente de la base de datos.
            modelBuilder.Entity<Actividad>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Asistencia>().HasQueryFilter(a => !a.IsDeleted);
            modelBuilder.Entity<Clase>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Localidad>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<Profesor>().HasQueryFilter(p => !p.IsDeleted);
            modelBuilder.Entity<Socio>().HasQueryFilter(s => !s.IsDeleted);
            modelBuilder.Entity<SocioActividad>().HasQueryFilter(sa => !sa.IsDeleted);
            modelBuilder.Entity<Usuario>().HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
