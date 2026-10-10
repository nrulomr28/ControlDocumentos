using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Destinatario
    {
        [Key] public int? DestinatarioId { get; set; }
        public string? DestinatarioNombre { get; set; }
        public int? Cat_CentroTrabajoId { get; set; }

    }
}
