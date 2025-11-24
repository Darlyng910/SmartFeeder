using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartFeederApi.Models
{
    public class Mascota
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Especie { get; set; }
        public int Edad { get; set; }
    }

}