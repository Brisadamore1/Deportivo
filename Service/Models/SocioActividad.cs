namespace Service.Models
{
    public class SocioActividad
    {
        public int Id { get; set; }
        public int SocioId { get; set; }
        public Socio? Socio { get; set; }
        public int ActividadId { get; set; }
        public Actividad? Actividad { get; set; }
        public bool IsDeleted { get; set; } = false;

    }
}
