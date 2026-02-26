using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.DTOs;
using Service.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ActividadesController : ControllerBase
    {
        private readonly DeportivoContext _context;

        public ActividadesController(DeportivoContext context)
        {
            _context = context;
        }

        // GET: api/Actividades
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetActividades([FromQuery] string filtro="")
        {
            // Incluir siempre al profesor para que la app pueda mostrar su nombre
            return await _context.Actividades
                .AsNoTracking()
                .Include(a => a.Profesor)
                .Where(a => a.Nombre.Contains(filtro))
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        // POST: api/Actividades/withfilter
        [HttpPost("withfilter")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetActividadesWithFilter([FromBody] FilterActivityDTO? filter)
        {
            var query = _context.Actividades.AsNoTracking().AsQueryable();

            // Incluir siempre al profesor para que la app pueda mostrar su nombre
            query = query.Include(a => a.Profesor);

            if (filter == null || string.IsNullOrWhiteSpace(filter.SearchText))
            {
                return await query.OrderBy(a => a.Nombre).ToListAsync();
            }

            var text = filter.SearchText.ToUpperInvariant();

            // Nota: la entidad Profesor ya fue incluida arriba para asegurar que
            // la propiedad Profesor esté poblada cuando se devuelvan las actividades.

            query = query.Where(a =>
                (filter.ForNombre && a.Nombre.ToUpper().Contains(text)) ||
                (filter.ForNivel && ((a.Nivel ?? string.Empty).ToUpper().Contains(text))) ||
                (filter.ForProfesor && a.Profesor != null && a.Profesor.Nombre.ToUpper().Contains(text))
            );

            return await query.OrderBy(a => a.Nombre).ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Actividad>>> GetDeletedsActividades()
        {
            return await _context.Actividades
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted)
                .OrderBy(a => a.Nombre)
                .ToListAsync();
        }

        // GET: api/Actividades/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Actividad>> GetActividad(int id)
        {
            var actividad = await _context.Actividades.AsNoTracking().FirstOrDefaultAsync(a=>a.Id.Equals(id));

            if (actividad == null)
            {
                return NotFound();
            }

            return actividad;
        }

        // PUT: api/Actividades/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActividad(int id, Actividad actividad)
        {
            if (id != actividad.Id)
            {
                return BadRequest();
            }

            _context.Entry(actividad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActividadExists(id))
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

        // POST: api/Actividades
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Actividad>> PostActividad(Actividad actividad)
        {
            _context.Actividades.Add(actividad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetActividad", new { id = actividad.Id }, actividad);
        }

        // DELETE: api/Actividades/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActividad(int id)
        {
            var actividad = await _context.Actividades.FindAsync(id);
            if (actividad == null)
            {
                return NotFound();
            }
            actividad.IsDeleted=true;
            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreActividad(int id)
        {
            var actividad = await _context.Actividades.IgnoreQueryFilters().FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (actividad == null)
            {
                return NotFound();
            }
            actividad.IsDeleted=false;
            _context.Actividades.Update(actividad);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ActividadExists(int id)
        {
            return _context.Actividades.Any(e => e.Id == id);
        }
    }
}
