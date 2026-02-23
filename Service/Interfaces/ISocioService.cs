using Service.DTOs;
using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISocioService: IGenericService<Socio> 
    {
        //Firma del método para obtener actividades con filtro
        public Task<List<Socio>?> GetWithFilterAsync(FilterSocioDTO filter);
    }
}
