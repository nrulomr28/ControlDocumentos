using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public string TicketPersona { get; set; }

        public string TicketTel { get; set; }

        public string TicketAsunto { get; set; }

        public string TicketMensaje { get; set; }

        public string TicketPrioridad { get; set; }

        public DateTime TicketFecha { get; set; }

        public int Cat_TicketStatusId { get; set; }

        public int TicketUUsuario { get; set; }

        //public string UsuarioNombre { get; set; }
        public int OficinasId { get; set; }

        public int id_of { get; set; }
    }
}
