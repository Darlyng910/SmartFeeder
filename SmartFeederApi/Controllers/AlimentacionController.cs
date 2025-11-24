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
    public class AlimentacionController : ControllerBase
    {
        private readonly AlimentadorContext _context;

        public AlimentacionController(AlimentadorContext context)
        {
            _context = context;
        }

        // POST: api/alimentacion/registrar
        [HttpPost("registrar")]
        public async Task<ActionResult<EventoAlimentacion>> Registrar(EventoAlimentacion evento)
        {
            evento.FechaHora = DateTime.Now;
            _context.EventosAlimentacion.Add(evento);
            await _context.SaveChangesAsync();

            return Ok(evento);
        }

        // GET: api/alimentacion/historial/5
        [HttpGet("historial/{mascotaId}")]
        public async Task<ActionResult<IEnumerable<EventoAlimentacion>>> Historial(int mascotaId)
        {
            return await _context.EventosAlimentacion
                .Where(e => e.MascotaId == mascotaId)
                .OrderByDescending(e => e.FechaHora)
                .ToListAsync();
        }
    }
}