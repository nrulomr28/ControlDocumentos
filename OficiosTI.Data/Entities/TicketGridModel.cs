using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class TicketGridModel
    {
        public int TicketId { get; set; }

        public string TicketPersona { get; set; }

        public string TicketAsunto { get; set; }

        public string TicketPrioridad { get; set; }

        public DateTime TicketFecha { get; set; }

        public int Cat_TicketStatusId { get; set; }

        public string NumeroOficio { get; set; }

        public string OficinasNombre { get; set; }

      
    }
}
