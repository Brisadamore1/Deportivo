using Backend.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Service.Models;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AsistenciasController : ControllerBase
    {
        private readonly DeportivoContext _context;

        public AsistenciasController(DeportivoContext context)
        {
            _context = context;
        }

        // GET: api/Asistencias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetAsistencias([FromQuery] string filtro="")
        {
            return await _context.Asistencias
                .Include(a => a.Socio)
                .Include(a => a.Clase!)
                .ThenInclude(c => c.Actividad)
                .AsNoTracking()
                .ToListAsync();
        }

        [HttpGet("deleteds")]
        public async Task<ActionResult<IEnumerable<Asistencia>>> GetDeletedsAsistencias()
        {
            return await _context.Asistencias
                .Include(a => a.Socio)
                .Include(a => a.Clase!)
                .ThenInclude(c => c.Actividad)
                .AsNoTracking()
                .IgnoreQueryFilters()
                .Where(a => a.IsDeleted)
                .ToListAsync();
        }

        // GET: api/Asistencias/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Asistencia>> GetAsistencia(int id)
        {
            var asistencia = await _context.Asistencias
                .Include(a => a.Socio)
                .Include(a => a.Clase!)
                .ThenInclude(c => c.Actividad)
                .AsNoTracking()
                .FirstOrDefaultAsync(a=>a.Id.Equals(id));

            if (asistencia == null)
            {
                return NotFound();
            }

            return asistencia;
        }

        // PUT: api/Asistencias/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsistencia(int id, Asistencia asistencia)
        {
            if (id != asistencia.Id)
            {
                return BadRequest();
            }

            _context.Entry(asistencia).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AsistenciaExists(id))
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

        // POST: api/Asistencia
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Asistencia>> PostAsistencia(Asistencia asistencia)
        {
            if (asistencia.SocioId == 0 || asistencia.ClaseId == 0)
            {
                return BadRequest("Debe indicar un socio y una clase.");
            }

            var fechaInicio = asistencia.Fecha.Date;
            var fechaFin = fechaInicio.AddDays(1);

            var yaExiste = await _context.Asistencias.AnyAsync(a =>
                a.SocioId == asistencia.SocioId &&
                a.ClaseId == asistencia.ClaseId &&
                a.Fecha >= fechaInicio &&
                a.Fecha < fechaFin);

            if (yaExiste)
            {
                return Conflict(
                    "Ya existe una asistencia para este socio, clase y fecha.");
            }

            asistencia.Fecha = fechaInicio;

            _context.Asistencias.Add(asistencia);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                "GetAsistencia",
                new { id = asistencia.Id },
                asistencia);
        }

        // DELETE: api/Asistencias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.FindAsync(id);
            if (asistencia == null)
            {
                return NotFound();
            }
            asistencia.IsDeleted=true;
            _context.Asistencias.Update(asistencia);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("restore/{id}")]
        public async Task<IActionResult> RestoreAsistencia(int id)
        {
            var asistencia = await _context.Asistencias.IgnoreQueryFilters().FirstOrDefaultAsync(a=>a.Id.Equals(id));
            if (asistencia == null)
            {
                return NotFound();
            }
            asistencia.IsDeleted=false;
            _context.Asistencias.Update(asistencia);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool AsistenciaExists(int id)
        {
            return _context.Asistencias.Any(e => e.Id == id);
        }
    }
}
