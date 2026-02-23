namespace Service.DTOs
{
    public class FilterSocioDTO
    {
        // Texto de búsqueda por nombre
        public string SearchText { get; set; } = string.Empty;
        public bool ForDni { get; set; } = false;
    }
}