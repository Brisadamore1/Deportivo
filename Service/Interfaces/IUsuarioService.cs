using Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUsuarioService: IGenericService<Usuario> 
    {
        public Task<Usuario?> GetByEmailAsync(string email); //Email devuelve un solo usuario o null si no se encuentra
        public Task<bool> LoginInSystem(string email, string password);
    }
}
