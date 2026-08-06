using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OficiosTI.Data.Entities
{
    public class Ticket_SinOficio
    {
        [Key] public int SinOId { get; set; }
        public int TicketId { get; set; }
        public string? Cat_TicketStatusStatus { get; set; }
        public int Cat_TicketStatusId { get; set; }
        public DateTime Fecha { get; set; }
    }
}
