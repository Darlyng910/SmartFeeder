using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using  SmartFeederApi.Models;
using SmartFeederApi.Data;

namespace SmartFeederApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MascotasController : ControllerBase
    {
        private readonly AlimentadorContext _context;

        public MascotasController(AlimentadorContext context)
        {
            _context = context;
        }

        // GET: api/mascotas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Mascota>>> GetMascotas()
        {
            return await _context.Mascotas.ToListAsync();
        }

        // POST: api/mascotas
        [HttpPost]
        public async Task<ActionResult<Mascota>> CrearMascota(Mascota mascota)
        {
            _context.Mascotas.Add(mascota);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMascotas), new { id = mascota.Id }, mascota);
        }
    }
}