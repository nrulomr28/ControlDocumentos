using Microsoft.EntityFrameworkCore;
using OficiosTI.Aplicacion.DTOs;
using OficiosTI.Aplicacion.Tickets.DTOs;
using OficiosTI.Data;
using OficiosTI.Data.Entities;

namespace OficiosTI.Aplicacion.Tickets.Services;

public class TicketDashboardRepository
{
    private readonly OficiosContext _context;

    public TicketDashboardRepository(OficiosContext context)
    {
        _context = context;
    }

    public IQueryable<Ticket> BaseQuery()
    {
        return _context.Ticket
            .Where(t => t.OficinasId == 3);
    }

    public IQueryable<TicketGridModel> QueryTicketsBase()
    {
        var fechaLimite = DateTime.Now.AddMonths(-8);

        return
            from t in BaseQuery()
            join o in _context.OficioRespuesta
                on t.TicketId equals o.TicketId into oficios
            from o in oficios.DefaultIfEmpty()
            where t.TicketFecha >= fechaLimite
            select new TicketGridModel
            {
                TicketId = t.TicketId,
                TicketPersona = t.TicketPersona,
                TicketAsunto = t.TicketAsunto,
                TicketPrioridad = t.TicketPrioridad,
                TicketFecha = t.TicketFecha,
                Cat_TicketStatusId = t.Cat_TicketStatusId,
                NumeroOficio = o.NumeroOficio
            };
    }

    public List<TicketGridModel> Mapear(IQueryable<Ticket> query)
    {
        return query
            .Select(t => new TicketGridModel
            {
                TicketId = t.TicketId,
                TicketPersona = t.TicketPersona,
                TicketAsunto = t.TicketAsunto,
                TicketPrioridad = t.TicketPrioridad,
                TicketFecha = t.TicketFecha,

                NumeroOficio = _context.OficioRespuesta
                    .Where(o => o.TicketId == t.TicketId)
                    .Select(o => o.NumeroOficio)
                    .FirstOrDefault()
            })
            .OrderByDescending(t => t.TicketFecha)
            .ToList();
    }

    public DashboardIndicadoresDto ObtenerIndicadores(IEnumerable<TicketGridModel> tickets)
    {
        var lista = tickets.ToList();

        int total = lista.Count;

        int respondidos = lista.Count(t =>
            !string.IsNullOrWhiteSpace(t.NumeroOficio));

        return new DashboardIndicadoresDto
        {
            Total = total,
            Respondidos = respondidos,
            Pendientes = total - respondidos
        };
    }

}