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
        public string Estado
        {
            get
            {
                if (!string.IsNullOrEmpty(EstadoSinOficio))    ///SE MUESTRA SOLO 1 ESTADO 
                {
                    return EstadoSinOficio;
                }
             
                return EstadoTicket;
            }
        }
        public int Cat_TicketStatusId { get; set; }
        public string EstadoTicket { get; set; }    /// ESTADO EN LA TABLA PRINCIPAL CAT_TICKETSTATUS

        public string EstadoSinOficio { get; set; }  //// ESTADO DE LA TABLA DONDE SOLO HAY TICKETS SIN OFICIOS 

        public string NumeroOficio { get; set; }

        public string OficinasNombre { get; set; }

    }
}
