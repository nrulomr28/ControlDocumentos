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
    public DbSet<Oficio1> Oficio1 { get; set; }   //// tabla anterior de oficios capturados 
    public DbSet<NumOficio> NumOficio { get; set; }  /// nueva tabla 
    public DbSet<Cat_TicketStatus> Cat_TicketStatus { get; set; } 
    public DbSet<Cat_copias> Cat_Copias { get; set; }   /// nueva tabla  
    public DbSet<Archivo> Archivo { get; set; }   /// tabla donde se guarda el oficio escaneado 
    public DbSet<Ticket_SinOficio>Ticket_SinOficio { get; set; }   /// nueva tabla donde se guarda nuevo estado ticket sin oficio de respuesta 
    public DbSet<CapturaOficios> CapturaOficios { get; set; }   /// nueva tabla de captura de oficios 
    public DbSet<Cat_TipoDocumento> Cat_TipoDocumento { get; set; }  // TIPOS DE DOCUMENTOS QUE SE CAPTURAN 
    public DbSet<Remitente> Remitente {  get; set; }
    public DbSet<Destinatario> Destinatario { get; set; }
    public DbSet <TSeguimiento> TSeguimiento { get; set; }   // SEGUIMIENTO DE LOS OFICIOS 
    public DbSet<Dependencia> Dependencia { get; set; }   /// CARGOS Y DEPENDENCIAS 
    public DbSet<CatClasificacion> CatClasificacion  {  get; set; } // Clasificacion archivatoria 
    public DbSet<Cat_Status> Cat_Status { get; set; } // catalogo de estados del oficio 
    public DbSet<Usuarios>Usuarios { get; set; } /// nueva tabla de usuarios 
}