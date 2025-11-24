using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartFeederApi.Models;

namespace SmartFeederApi.Data
{
    public class AlimentadorContext : DbContext
    {
        public AlimentadorContext(DbContextOptions<AlimentadorContext> options)
            : base(options)
        {
        }

        public DbSet<Mascota> Mascotas { get; set; }
        public DbSet<HorarioAlimentacion> HorariosAlimentacion { get; set; }
        public DbSet<EventoAlimentacion> EventosAlimentacion { get; set; }
    }
}