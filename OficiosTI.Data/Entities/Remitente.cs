using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Remitente
    {      
        [Key] public int? RemitenteId { get; set; }
        public string? RemitenteNombre { get; set; }
       // public DateTime ? RemitenteDateTime { get; set; }
        public int? Cat_CentroTrabajoId { get; set; }

    }
}
