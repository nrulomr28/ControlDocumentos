using Microsoft.EntityFrameworkCore;
using OficiosTI.Data.Entities;
using System.Net.Sockets;

namespace OficiosTI.Data;

public class OficiosContext : DbContext
{
    public OficiosContext(DbContextOptions<OficiosContext> options)
        : base(options)
    {
    }
    public DbSet<Ticket> Ticket { get; set; }
    public DbSet<HiloTicket> HiloTicket { get; set; }
    public DbSet<Oficinas> Oficinas { get; set; }
    public DbSet<OficioRespuesta> OficioRespuesta { get; set; }
    public DbSet<TicketArchivo> TicketArchivo { get; set; }
    public DbSet<Firmante> Firmante { get; set; }
    public DbSet<Oficio1> Oficio1 { get; set; }
    public DbSet<NumOficio> NumOficio { get; set; }
    public DbSet<Cat_TicketStatus> Cat_TicketStatus { get; set; }
}