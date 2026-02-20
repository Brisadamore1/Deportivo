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
    public class ProfesoresController : ControllerBase
    {
        private readonly DeportivoContext _context;

        public ProfesoresController(DeportivoContext context)
        {
            _context = context;
        }

        // GET: api/Profesores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesores([FromQuery] string filtro="")
        {
            return await _context.Profesores.AsNoTracking().Where(a=>a.Nombre.Contains(filtro)).ToListAsync();
        }

        // POST: api/Profesores/withfilter
        [HttpPost("withfilter")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetProfesoresWithFilter([FromBody] FilterActivityDTO? filter)
        {
            var query = _context.Profesores.AsNoTracking().AsQueryable();

            if (filter == null || string.IsNullOrWhiteSpace(filter.SearchText))
            {
                return await query.ToListAsync();
            }

            var text = filter.SearchText.ToUpperInvariant();

            // Only 'ForNombre' is applicable for profesores; if set, filter by Nombre
            if (filter.ForNombre)
            {
                query = query.Where(p => p.Nombre.ToUpper().Contains(text));
            }

            return await query.ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Profesor>>> GetDeletedsProfesores()
        {
            return await _context.Profesores
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted).ToListAsync();
        }

        // GET: api/Profesores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Profesor>> GetProfesor(int id)
        {
            var profesor = await _context.Profesores.AsNoTracking().FirstOrDefaultAsync(a=>a.Id.Equals(id));

            if (profesor == null)
            {
                return NotFound();
            }

            return profesor;
        }

        // PUT: api/Profesores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProfesor(int id, Profesor profesor)
        {
            if (id != profesor.Id)
            {
                return BadRequest();
            }

            _context.Entry(profesor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfesorExists(id))
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

        // POST: api/Profesores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Profesor>> PostProfesor(Profesor profesor)
        {
            _context.Profesores.Add(profesor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProfesor", new { id = profesor.Id }, profesor);
        }

        // DELETE: api/Profesores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProfesor(int id)
        {
            var profesor = await _context.Profesores.FindAsync(id);
            if (profesor == null)
            {
                return NotFound();
            }
            profesor.IsDeleted=true;
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreProfesor(int id)
        {
            var profesor = await _context.Profesores.IgnoreQueryFilters().FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (profesor == null)
            {
                return NotFound();
            }
            profesor.IsDeleted=false;
            _context.Profesores.Update(profesor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ProfesorExists(int id)
        {
            return _context.Profesores.Any(e => e.Id == id);
        }
    }
}
