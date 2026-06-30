using OficiosTI.Aplicacion.Tickets.DTOs;
using OficiosTI.Data.Entities;

namespace OficiosTI.Aplicacion.Tickets.Services
{
    public class TicketDashboardService
    {
        private readonly TicketDashboardRepository _queryService;

        public TicketDashboardService(TicketDashboardRepository queryService)
        {
            _queryService = queryService;
        }

        public List<TicketGridModel> ObtenerPendientes()
        {
            return _queryService
                .QueryTicketsBase()
                .Where(t => t.NumeroOficio == null)
                .OrderByDescending(t => t.TicketFecha)
                .ToList();
        }

        public DashboardIndicadoresDto ObtenerIndicadores(IEnumerable<TicketGridModel> tickets)
        {
            return _queryService.ObtenerIndicadores(tickets);
        }
    }
}
