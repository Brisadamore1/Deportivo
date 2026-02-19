using Service.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Service.Models
{
    public class Profesor : IEntityIdNombre
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
