using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.DTOs
{
    public sealed class ActivityImageExtractionRequestDTO
    {
        public IFormFile? Image { get; set; }

    }
}
