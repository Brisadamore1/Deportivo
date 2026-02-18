using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.ExtentionMethods;
using Service.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SocioActividadesController : ControllerBase
    {
        private readonly DeportivoContext _context;

        public SocioActividadesController(DeportivoContext context)
        {
            _context = context;
        }

        // GET: api/SocioActividades
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SocioActividad>>> GetSocioActividades([FromQuery] string filtro="")
        {
            return await _context.SocioActividades
                .Include(l=>l.Socio)
                .Include(l=>l.Actividad)
                .AsNoTracking()
                .Where(l=>l.Socio.Nombre.ToUpper().Contains(filtro.ToUpper())||
                       l.Actividad.Nombre.ToUpper().Contains(filtro.ToUpper())).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<SocioActividad>>> GetDeletedsSocioActividades()
        {
            return await _context.SocioActividades
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(l => l.IsDeleted).ToListAsync();
        }

        // GET: api/SocioActividades/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SocioActividad>> GetSocioActividad(int id)
        {
            var socioActividad = await _context.SocioActividades.AsNoTracking().FirstOrDefaultAsync(l=>l.Id.Equals(id));

            if (socioActividad == null)
            {
                return NotFound();
            }

            return socioActividad;
        }

        // PUT: api/SocioActividades/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSocioActividad(int id, SocioActividad socioActividad)
        {
            _context.TryAttach(socioActividad?.Socio);
            _context.TryAttach(socioActividad?.Actividad?.Profesor);
            _context.TryAttach(socioActividad?.Actividad);
            if (id != socioActividad.Id)
            {
                return BadRequest();
            }

            _context.Entry(socioActividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SocioActividadExists(id))
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

        // POST: api/SocioActividades
        [HttpPost]
        public async Task<ActionResult<SocioActividad>> PostSocioActividad(SocioActividad socioActividad)
        {
            _context.TryAttach(socioActividad?.Socio);
            _context.TryAttach(socioActividad?.Actividad?.Profesor);
            _context.TryAttach(socioActividad?.Actividad);
            _context.SocioActividades.Add(socioActividad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSocioActividad", new { id = socioActividad.Id }, socioActividad);
        }

        // DELETE: api/SocioActividades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSocioActividad(int id)
        {
            var socioActividad = await _context.SocioActividades.FindAsync(id);
            if (socioActividad == null)
            {
                return NotFound();
            }
            socioActividad.IsDeleted=true;
            _context.SocioActividades.Update(socioActividad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreSocioActividad(int id)
        {
            var socioActividad = await _context.SocioActividades.IgnoreQueryFilters().FirstOrDefaultAsync(l=>l.Id.Equals(id));
            if (socioActividad == null)
            {
                return NotFound();
            }
            socioActividad.IsDeleted=false;
            _context.SocioActividades.Update(socioActividad);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool SocioActividadExists(int id)
        {
            return _context.SocioActividades.Any(e => e.Id == id);
        }
    }
}
