using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Profesor
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
        public bool IsDeleted { get; set; } = false;
        public override string ToString()
        {
            return Nombre;
        }
    }
}
