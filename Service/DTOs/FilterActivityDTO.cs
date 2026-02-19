using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public class FilterActivityDTO
    {
        public string SearchText { get; set; } = "";
        public bool ForNombre { get; set; } = false;
        public bool ForProfesor { get; set; } = false;
        public bool ForNivel { get; set; } = false;
    }
}
