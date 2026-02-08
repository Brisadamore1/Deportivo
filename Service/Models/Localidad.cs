using System.ComponentModel.DataAnnotations;

namespace Service.Models
{
    public class Localidad
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } = false;

        public override string ToString()
        {
            return Nombre;
        }
    }
}
