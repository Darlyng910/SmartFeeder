using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFeederApi.Models
{
    public class HorarioAlimentacion
    {
        public int Id { get; set; }

        public int MascotaId { get; set; }
        public Mascota? Mascota { get; set; }

        public TimeOnly Hora { get; set; }   
        public int CantidadGramos { get; set; }
    }
}