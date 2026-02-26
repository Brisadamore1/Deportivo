namespace Service.DTOs
{
    public class FilterSocioDTO
    {
        // Texto de búsqueda por nombre
        public string SearchText { get; set; } = string.Empty;
        public bool ForNombre { get; set; } = false;
        public bool ForDni { get; set; } = false;
        public bool ForLocalidad { get; set; } = false; 
        public bool ForActividad { get; set; } = false;
    }
}