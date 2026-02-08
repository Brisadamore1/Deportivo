using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Actividad
    {
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; } = string.Empty;

        public int ProfesorId { get; set; }
        public Profesor? Profesor { get; set; }

        public override string ToString()
        {
            return Nombre;
        }

    }
}
