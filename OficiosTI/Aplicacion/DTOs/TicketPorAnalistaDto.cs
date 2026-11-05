using OficiosTI.Data.Entities;
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
        public string? Analista { get; set; }
        public string? Oficina { get; set; }
        public int Total { get; set; }
        public int Urgentes { get; set; }
        public int Terminados { get; set; }
        public List<Ticket> Tickets { get; set; } = new List<Ticket>();     
        public string NumerosDeTickets => string.Join(", ", Tickets.Select(t => t.TicketId));
    }
}
