using System.ComponentModel.DataAnnotations;

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
        public override string ToString()
        {
            return Nombre;
        }
    }
}
