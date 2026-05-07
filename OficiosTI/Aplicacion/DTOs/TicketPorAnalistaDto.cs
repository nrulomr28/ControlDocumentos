using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Aplicacion.DTOs
{
    public class TicketPorAnalistaDto
    {
        public int UsuarioId { get; set; }
        public string Analista { get; set; }
        public int Total { get; set; }
        public int Urgentes { get; set; }
        public int ConAdjunto { get; set; }
    }
}
