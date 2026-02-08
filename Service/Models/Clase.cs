using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Clase
    {
        public int Id { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DayOfWeek DiaSemana { get; set; }

        [Required]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraInicio { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:hh\\:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan HoraFin { get; set; }

        [Range(1, 100)]
        public int CupoMaximo { get; set; } 
        public bool Activa { get; set; } //Si ese horario se suspende temporalmente se desactiva.
        public int ActividadId { get; set; }
        public Actividad? Actividad { get; set; } 
        public bool IsDeleted { get; set; } = false;
    }
}
