using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;
using Service.DTOs;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SociosController : ControllerBase
    {
        private readonly DeportivoContext _context;

        public SociosController(DeportivoContext context)
        {
            _context = context;
        }

        // GET: api/Socios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Socio>>> GetSocios([FromQuery] string filtro="")
        {
            return await _context.Socios
                .Include(s => s.SocioActividades)
                    .ThenInclude(sa => sa.Actividad)
                .Include(s => s.Localidad)
                .AsNoTracking()
                .Where(a => a.Nombre.Contains(filtro))
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Socio>>> GetDeletedsSocios()
        {
            return await _context.Socios
                .Include(s => s.SocioActividades)
                    .ThenInclude(sa => sa.Actividad)
                .Include(s => s.Localidad)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted)
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        // GET: api/Socios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Socio>> GetSocio(int id)
        {
            var socio = await _context.Socios
                .Include(s => s.SocioActividades)
                    .ThenInclude(sa => sa.Actividad)
                .Include(s => s.Localidad)
                .AsNoTracking()
                .FirstOrDefaultAsync(a=>a.Id.Equals(id));

            if (socio == null)
            {
                return NotFound();
            }

            return socio;
        }

        // PUT: api/Socios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocio(int id, Socio socio)
        {
            if (id != socio.Id)
            {
                return BadRequest();
            }

            _context.Entry(socio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Socios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Socio>> PostSocio(Socio socio)
        {
            _context.Socios.Add(socio);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocio", new { id = socio.Id }, socio);
        }

        // POST: api/Socios/withfilter
        [HttpPost("withfilter")]
        public async Task<ActionResult<IEnumerable<Socio>>> GetSociosWithFilter([FromBody] FilterSocioDTO? filter)
        {
            var query = _context.Socios
                .Include(s => s.SocioActividades)
                .ThenInclude(sa => sa.Actividad)
                .Include(s => s.Localidad)
                .AsNoTracking()
                .AsQueryable();

            // Si no se envió filtro devolvemos todo ordenado
            if (filter == null)
            {
                return await query.OrderBy(a => a.Nombre).ToListAsync();
            }

            var text = (filter.SearchText ?? string.Empty).ToUpperInvariant();

            // Si no se filtró por estado, aplicar búsqueda por texto combinando opciones (OR)
            if (!string.IsNullOrWhiteSpace(text))
            {
                query = query.Where(a =>
                    (filter.ForDni && a.Dni != null && a.Dni.ToUpper().Contains(text)) ||
                    (filter.ForNombre && a.Nombre.ToUpper().Contains(text)) ||
                    (filter.ForLocalidad && a.Localidad != null && a.Localidad.Nombre.ToUpper().Contains(text)) ||
                    (filter.ForActividad && a.SocioActividades.Any(sa => sa.Actividad != null && sa.Actividad.Nombre.ToUpper().Contains(text)))
                );
            }

            return await query.OrderBy(a => a.Nombre).ToListAsync();
        }

        // DELETE: api/Socios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocios(int id)
        {
            var socio = await _context.Socios.FindAsync(id);
            if (socio == null)
            {
                return NotFound();
            }
            socio.IsDeleted=true;
            _context.Socios.Update(socio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreSocio(int id)
        {
            var socio = await _context.Socios.IgnoreQueryFilters().FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (socio == null)
            {
                return NotFound();
            }
            socio.IsDeleted=false;
            _context.Socios.Update(socio);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool SocioExists(int id)
        {
            return _context.Socios.Any(e => e.Id == id);
        }
    }
}
