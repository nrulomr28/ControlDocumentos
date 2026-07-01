using OficiosTI.Aplicacion.Tickets.DTOs;
using OficiosTI.Data;
using OficiosTI.Data.Entities;

namespace OficiosTI.Aplicacion.Tickets.Services
{
    public class TicketDashboardService
    {
        private readonly TicketDashboardRepository _repository;

        public TicketDashboardService(OficiosContext context)
        {
            _repository = new TicketDashboardRepository(context);
        }

        public List<TicketGridModel> ObtenerPendientes()
        {
            return _repository
                .QueryTicketsBase()
                .Where(t => t.NumeroOficio == null)
                .OrderByDescending(t => t.TicketFecha)
                .ToList();
        }

        public DashboardIndicadoresDto ObtenerIndicadores(
                IEnumerable<TicketGridModel> tickets)
        {
            return _repository.ObtenerIndicadores(tickets);
        }

        public Ticket? ObtenerTicket(int ticketId)
        {
            return _repository.ObtenerTicket(ticketId);
        }

        public List<TicketGridModel> ObtenerCerradosSinOficio()
        {
            return _repository.ObtenerCerradosSinOficio();
        }

        public List<TicketGridModel> ObtenerTodos()
        {
            return _repository.ObtenerTodos();
        }

    }
}
