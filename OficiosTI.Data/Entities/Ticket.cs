using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public string TicketPersona { get; set; }     /// REMITENTE

        public string TicketTel { get; set; }    /// CAMPO DE TEL SIEMPRE VACIO 

        public string TicketAsunto { get; set; }     //// NUMERO DE OFICIO DEL TICKET 

        public string TicketMensaje { get; set; }    //// ASUNTO DEL TICKET

        public string TicketPrioridad { get; set; }   /// PRIORIDAD DEL TICKET 

        public DateTime TicketFecha { get; set; }   /// FECHA DE CAPTURA DEL TICKET

        public int Cat_TicketStatusId { get; set; }   /// ESTADO DEL TICKET 

        public int TicketUUsuario { get; set; }    /// CAPTURADO POR 

        //public string UsuarioNombre { get; set; }
        public int OficinasId { get; set; }
        [ForeignKey("id_of")]
        public int? id_of { get; set; }
    }
}



