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
                .AsNoTracking()
                .Where(a => a.Nombre.Contains(filtro))
                .ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Socio>>> GetDeletedsSocios()
        {
            return await _context.Socios
                .Include(s => s.SocioActividades)
                    .ThenInclude(sa => sa.Actividad)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted).ToListAsync();
        }

        // GET: api/Socios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Socio>> GetSocio(int id)
        {
            var socio = await _context.Socios
                .Include(s => s.SocioActividades)
                    .ThenInclude(sa => sa.Actividad)
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
                .AsNoTracking()
                .AsQueryable();


            if (filter == null || string.IsNullOrWhiteSpace(filter.SearchText))
            {
                return await query.ToListAsync();
            }

            var text = filter.SearchText!.ToUpperInvariant();

            // Si el DTO indica que se debe filtrar por DNI, usar el texto como filtro de DNI,
            // en caso contrario filtrar por nombre (case-insensitive).
            if (filter.ForDni)
            {
                query = query.Where(a => a.Dni.Contains(filter.SearchText));
            }
            else
            {
                query = query.Where(a => a.Nombre.ToUpper().Contains(text));
            }

            return await query.ToListAsync();
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
