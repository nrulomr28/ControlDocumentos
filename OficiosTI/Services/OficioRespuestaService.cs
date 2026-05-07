using OficiosTI.Data;
using OficiosTI.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace OficiosTI.Services
{
    public class OficioRespuestaService
    {
        private readonly OficiosContext _context;

        public OficioRespuestaService(OficiosContext context)
        {
            _context = context;
        }

        public bool NumeroOficioExiste(string numeroOficio)
        {
            return _context.OficioRespuesta
                .Any(x => x.NumeroOficio == numeroOficio);
        }

        public string ObtenerUltimoHilo(int ticketId)
        {
            var hilo = _context.HiloTicket
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();

            if (hilo == null)
                return "";

            return hilo.HiloTicketMensaje;
        }

        public OficioRespuesta CrearOficio(
            int ticketId,
            string numeroOficio,
            string oficioReferencia,
            string asunto,
            string destinatario,
            string cargo,
            string respuesta,
            string copias)
        {
            var oficio = new OficioRespuesta
            {
                TicketId = ticketId,

                NumeroOficio = numeroOficio,

                OficioReferencia = oficioReferencia,

                Asunto = asunto,

                Destinatario = destinatario,

                CargoDestinatario = cargo,

                CuerpoRespuesta = respuesta,

                Copias = copias,

                FechaOficio = DateTime.Now,

                FechaCaptura = DateTime.Now,

                Anio = (short)DateTime.Now.Year
            };

            _context.OficioRespuesta.Add(oficio);

            _context.SaveChanges();

            return oficio;
        }

        public List<string> ObtenerDestinatariosFrecuentes()
        {
            return _context.OficioRespuesta
                .Select(x => x.Destinatario)
                .Distinct()
                .OrderBy(x => x)
                .ToList();
        }

        public OficioRespuesta? ObtenerOficioPorTicket(int ticketId)
        {
            return _context.OficioRespuesta
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.FechaCaptura)
                .FirstOrDefault();
        }

        public OficioRespuesta ConvertirTicketEnBorrador(Ticket ticket)
        {
            var hilo = _context.HiloTicket
                .Where(x => x.TicketId == ticket.TicketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();

            return new OficioRespuesta
            {
                TicketId = ticket.TicketId,
                Asunto = ticket.TicketAsunto,
                CuerpoRespuesta = hilo?.HiloTicketMensaje ?? "",
                FechaOficio = DateTime.Now,
                FechaCaptura = DateTime.Now,
                Anio = (short)DateTime.Now.Year
            };
        }

        public void ActualizarOficio(OficioRespuesta oficio)
        {
            _context.OficioRespuesta.Update(oficio);
            _context.SaveChanges();
        }

        public void RegistrarHiloOficio(int ticketId, string numeroOficio)
        {
            var hilo = new HiloTicket
            {
                TicketId = ticketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = "OFICIO",
                HiloTicketMensaje = $"Oficio {numeroOficio} emitido como respuesta.",
                UsuarioId = 0 // puedes cambiarlo si manejas usuarios
            };

            _context.HiloTicket.Add(hilo);

            _context.SaveChanges();
        }

        public bool HiloYaExiste(int ticketId, string descripcion)
        {
            return _context.HiloTicket
                .Any(h => h.TicketId == ticketId && h.HiloTicketMensaje == descripcion);
        }
    }
}