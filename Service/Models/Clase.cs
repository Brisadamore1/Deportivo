using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Clase
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Seleccione un día.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DayOfWeek DiaSemana { get; set; }

        [Required(ErrorMessage = "Ingrese la hora de inicio.")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraInicio { get; set; }

        [Required(ErrorMessage = "Ingrese la hora de finalización.")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraFin { get; set; }

        [Range(1, 100, ErrorMessage = "El cupo debe ser entre 1 y 100.")]
        public int CupoMaximo { get; set; } 
        public bool Activa { get; set; } //Si ese horario se suspende temporalmente se desactiva.

        [Required(ErrorMessage = "Seleccione una actividad.")]
        [Range(1, int.MaxValue, ErrorMessage = "Seleccione una actividad.")]
        public int ActividadId { get; set; }
        public Actividad? Actividad { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}
