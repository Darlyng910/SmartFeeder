using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartFeederApi.Models;
using SmartFeederApi.Data;

namespace SmartFeederApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HorariosController : ControllerBase
    {
        private readonly AlimentadorContext _context;

        public HorariosController(AlimentadorContext context)
        {
            _context = context;
        }

        // GET: api/horarios/mascota/5
        [HttpGet("mascota/{mascotaId}")]
        public async Task<ActionResult<IEnumerable<HorarioAlimentacion>>> GetHorarios(int mascotaId)
        {
            return await _context.HorariosAlimentacion
                .Where(h => h.MascotaId == mascotaId)
                .ToListAsync();
        }

        // POST: api/horarios
        [HttpPost]
        public async Task<ActionResult<HorarioAlimentacion>> CrearHorario(HorarioAlimentacion horario)
        {
            _context.HorariosAlimentacion.Add(horario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHorarios),
                new { mascotaId = horario.MascotaId },
                horario);
        }

        // DELETE: api/horarios/10
        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarHorario(int id)
        {
            var horario = await _context.HorariosAlimentacion.FindAsync(id);

            if (horario == null)
                return NotFound();

            _context.HorariosAlimentacion.Remove(horario);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}