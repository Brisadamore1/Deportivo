using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class Socio
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        [Required]
        public string Dni { get; set; } = string.Empty;
        [Required]
        public string Domicilio { get; set; } = string.Empty;
        [Required]
        public string Telefono { get; set; } = string.Empty;
        [Required]
        public bool Activo { get; set; } = true;

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        public int LocalidadId { get; set; }
        public Localidad? Localidad { get; set; }

        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        virtual public string Actividades
        {
            get
            {
                if (SocioActividades == null || SocioActividades.Count == 0)
                    return string.Empty;
                var textActividad = SocioActividades.Count > 1 ? "Actividades: " : "Actividad: ";
                return textActividad + string.Join(", ", SocioActividades.Where(lg => lg.Actividad != null).Select(lg => lg.Actividad!.Nombre));
            }
        }
        virtual public ICollection<SocioActividad> SocioActividades { get; set; } = new List<SocioActividad>();

        public override string ToString()
        {
            return Nombre;
        }
    }
}
