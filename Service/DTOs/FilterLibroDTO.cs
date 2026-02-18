using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class FilterActividadDTO
    {
        public string SearchText { get; set; } = "";
        public bool ForNombre { get; set; } = false;
        public bool ForProfesor { get; set; } = false;
        public bool ForEditorial { get; set; } = false;
        public bool ForGenero { get; set; } = false;
        public bool ForSinopsis { get; set; } = false;
    }
}
