using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class HiloTicket
    {
        public int HiloTicketId { get; set; }

        public DateTime HiloTicketFecha { get; set; }

        public string HiloTicketAccion { get; set; }

        public string HiloTicketMensaje { get; set; }

        public decimal UsuarioId { get; set; }

        public int TicketId { get; set; }
    }
}
