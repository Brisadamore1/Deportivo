using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Asistencia
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Fecha { get; set; }
        public bool Presente { get; set; }
        public int SocioId { get; set; }
        public Socio? Socio { get; set; }
        public int ClaseId { get; set; }
        public Clase? Clase { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
