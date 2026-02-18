using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Utils
{
    public static class ApiEndpoints
    {
        public static string Actividad { get; set; } = "actividades";
        public static string Asistencia { get; set; } = "asistencias";
        public static string Clase { get; set; } = "clases";
        public static string Localidad { get; set; } = "localidades";
        public static string Profesor { get; set; } = "profesores";
        public static string Socio { get; set; } = "socios";
        public static string SocioActividad { get; set; } = "sociosactividades";
        public static string Usuario { get; set; } = "usuarios";
        public static string Gemini { get; set; } = "gemini";
        public static string Login { get; set; } = "auth";

        public static string GetEndpoint(string name)
        {
            return name switch
            {
                nameof(Actividad) => Actividad,
                nameof(Asistencia) => Asistencia,
                nameof(Clase) => Clase,
                nameof(Localidad) => Localidad,
                nameof(Profesor) => Profesor,
                nameof(Socio) => Socio,
                nameof(SocioActividad) => SocioActividad,
                nameof(Usuario) => Usuario,
                nameof(Gemini) => Gemini,
                nameof(Login) => Login,
                _ => throw new ArgumentException($"Endpoint '{name}' no está definido.")
            };
        }
    }
}
