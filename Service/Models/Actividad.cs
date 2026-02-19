using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class Actividad
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;
        public string Imagen { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "text")]
        public string Descripcion { get; set; } = string.Empty;
        public string? EdadRecomendada { get; set; }

        [Column(TypeName = "text")]
        public string? Beneficios { get; set; }

        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return Nombre;
        }

    }
}
