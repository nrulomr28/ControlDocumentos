using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using System.Runtime.InteropServices;

namespace OficiosTI.Services
{
    public class OficioRespuestaService
    {
        private readonly OficiosContext _context;
        private NumOficio _oficioAnterior; 
     //   private Oficio1 _oficioAnterior;     ///// OFICIOREFRENCIA

        public OficioRespuestaService(OficiosContext context)
        {
            _context = context;
        }

        public bool NumeroOficioExiste(string numeroOficio)
        {
            return _context.OficioRespuesta
                .Any(x => x.NumeroOficio == numeroOficio);
        }

        public int NumeroTicketExiste(string numeroOficio)
        {


            /// OBTENER PRIMERO EL ID DEL OFICIO PARA BUSCARLO EN LA TABLA DE OficioRespuesta
            var ofin = _context.NumOficio
               .Where(x => x.NumeroConsecutivo == numeroOficio)
               .OrderByDescending(x => x.OficioId)
               .FirstOrDefault();

       /*     var idTickets = _context.OficioRespuesta
               .Where(x => x.RespuestaId === ofin)
               .OrderByDescending(x => x.OficioId)
               .FirstOrDefault(); 
       */

            /*     buscar el int en la tabla de numoficio
                     obtener el id de la tabla el oficioID
                     y buscarla en la tabla de OficioRespuesta
            */
            //  return idTickets;
            return 0;
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

      /*  public string ObtenerOficioAntes (int IdOf)
        {
            var OficioAntes = _context.
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();
      *
        }*/

        /*
        public OficioRespuesta CrearOficio(
            int ticketId,
            string numeroOficio,
            string oficioReferencia,
            string asunto,
            string destinatario,
            string cargo,
            string respuesta,
            string copias,
            int firmanteid)
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

                Anio = (short)DateTime.Now.Year,

                FirmanteId = firmanteid
            };

            _context.OficioRespuesta.Add(oficio);

            _context.SaveChanges();

            return oficio;
        }
        */

        public OficioRespuesta CrearOficio(
            int ticketId,
            string numeroOficio,
            string oficioReferencia,
            string asunto,
            string destinatario,
            string cargo,
            string respuesta,
            string copias,
            int? firmanteid,
            int?OficioId) 
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
                Anio = (short)DateTime.Now.Year,
                OficioId = OficioId,         
                FirmanteId = firmanteid
            };

            
            _context.OficioRespuesta.Add(oficio);
            _context.SaveChanges();

            return oficio;
        }

        /// ASIGNAR EL NUMERO CONSECUTIVO EN LA TABLA NUMOFICIO

        public NumOficio CrearNumero(string NumCon,
            string Bis,
            int Oficinas_Id,
            int Tipo,
            int Anio,
            int TicketId)
        {
            var nuevoNumOficio = new NumOficio
            {
                NumeroConsecutivo = NumCon,
                Bis = Bis, 
                Oficinas_Id = Oficinas_Id,
                Tipo = Tipo,
                Anio = Anio,
                FechaCaptura=DateTime.Now,
                TicketId= TicketId

            };
            _context.NumOficio.Add(nuevoNumOficio);
            _context.SaveChanges();        
            return nuevoNumOficio;
        }

        /// ASIGNAR NUMERO DE OFICIO EN LA TABLA DE NUMOFICIO

     
        public OficioRespuesta? ObtenerOficioPorTicket(int ticketId)
        {
            return _context.OficioRespuesta
                .Where(x => x.TicketId == ticketId)
                .OrderByDescending(x => x.FechaCaptura)
                .FirstOrDefault();
        }


        /*
        public OficioRespuesta ConvertirTicketEnBorrador(Ticket ticket)
        { 
            var firmante = new Firmante();
            var Oficio1 = new Oficio1();
            var hilo = _context.HiloTicket
                .Where(x => x.TicketId == ticket.TicketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();
        

            return new OficioRespuesta
            {
                TicketId = ticket.TicketId,
                Asunto = ticket.TicketMensaje,
                Destinatario = ticket.TicketPersona,
              //  CuerpoRespuesta = hilo?.HiloTicketMensaje ?? "",
                FechaOficio = DateTime.Now,
                FechaCaptura = DateTime.Now,
                Anio = (short)DateTime.Now.Year,
                FirmanteId = firmante.FirmanteId,
           //     OficioId =Oficio1.OficioNoControl
              
            };
        }
        */


        public OficioRespuesta ConvertirTicketEnBorrador(Ticket ticket)
        {
            var firmante = new Firmante();

            var hilo = _context.HiloTicket
                .Where(x => x.TicketId == ticket.TicketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();

            var registroOficio1 = _context.Oficio1
                .Where(y => y.OficioId == ticket.id_of)
                .FirstOrDefault();

            return new OficioRespuesta
            {
                TicketId = ticket.TicketId,
                Asunto = ticket.TicketMensaje,
                Destinatario = ticket.TicketPersona,
                CuerpoRespuesta = hilo?.HiloTicketMensaje ?? string.Empty,
                FechaOficio = DateTime.Now,
                FechaCaptura = DateTime.Now,
                Anio = (short)DateTime.Now.Year,
                OficioReferencia = $"{registroOficio1.OficioNoOficio}",
               /// OficioReferencia = registroOficio1?.OficioNoControl ?? string.Empty,
                FirmanteId = firmante?.FirmanteId,
                OficioId = registroOficio1?.OficioId
            };
        }

      /*  public OficioRespuesta ConvertirTicketEnBorrador(Ticket ticket)
        {
            var firmante = new Firmante();
            var hilo = _context.HiloTicket
                .Where(x => x.TicketId == ticket.TicketId)
                .OrderByDescending(x => x.HiloTicketFecha)
                .FirstOrDefault();

            var registroOficio1 = _context.Oficio1
                .Where(y => y.OficioId == ticket.id_of)
                .FirstOrDefault();


            return new OficioRespuesta
            {
                TicketId = ticket.TicketId,
                Asunto = ticket.TicketMensaje,
                Destinatario = ticket.TicketPersona,
                CuerpoRespuesta = hilo?.HiloTicketMensaje ?? string.Empty,
                FechaOficio = DateTime.Now,
                FechaCaptura = DateTime.Now,
                Anio = (short)DateTime.Now.Year,
            //    NumeroOficio = registroOficio1?.OficioNoControl ?? string.Empty,       
                OficioReferencia = registroOficio1.OficioNoControl, 

                FirmanteId = firmante?.FirmanteId
            };
        }

        */


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
                HiloTicketAccion = "CERRADO",
                HiloTicketMensaje = $"Oficio {numeroOficio} emitido como respuesta.",
                UsuarioId = 0 
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