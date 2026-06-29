using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using System.DirectoryServices.AccountManagement;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.InteropServices;

using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;

namespace OficiosTI.Services
{
    public class OficioRespuestaService
    {
        private readonly OficiosContext _context;
        private NumOficio _oficioAnterior; 
     // private Oficio1 _oficioAnterior;     ///// OFICIOREFRENCIA

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
        public string ObtenerNombreUsuarioRed()
        {
            string nombreUsuarioCorto = Environment.UserName;
            string nombreMostrar = "Usuario Desconocido";

            try
            {

                using (PrincipalContext contexto = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal usuarioAD = UserPrincipal.FindByIdentity(contexto, nombreUsuarioCorto);

                    if (usuarioAD != null)
                    {
                        nombreMostrar = usuarioAD.DisplayName;
                    }
                }
            }
            catch
            {
                nombreMostrar = nombreUsuarioCorto;
            }

            return nombreMostrar;
        }

        public string ObtenerDominioYUsuario()
        {
         //string dominioCompleto = IPGlobalProperties.GetIPGlobalProperties().DomainName;
           string dominio = Environment.UserDomainName;
           string usuario = Environment.UserName;

           return $"{dominio}\\{usuario}";
         //   return $"{dominioCompleto}\\{usuario}";
        }

        public string ObtenerSegmentoDeRed()
        {
            foreach (NetworkInterface adaptador in NetworkInterface.GetAllNetworkInterfaces())
            {
                 if (adaptador.OperationalStatus == OperationalStatus.Up &&
                    adaptador.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                {
                    IPInterfaceProperties propiedades = adaptador.GetIPProperties();

                    foreach (UnicastIPAddressInformation ipInfo in propiedades.UnicastAddresses)
                    {
                        if (ipInfo.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            string ip = ipInfo.Address.ToString();
                            string mascara = ipInfo.IPv4Mask.ToString();

                            string segmentoBase = CalcularDireccionDeRed(ipInfo.Address, ipInfo.IPv4Mask);

                            return $": {segmentoBase} (IP: {ip} / Máscara: {mascara})";
                        }
                    }
                }
            }
            return "Red no detectada";
        }

        private string CalcularDireccionDeRed(IPAddress ipAddress, IPAddress subnetMask)
        {
            byte[] ipBytes = ipAddress.GetAddressBytes();
            byte[] maskBytes = subnetMask.GetAddressBytes();
            byte[] networkBytes = new byte[ipBytes.Length];

            for (int i = 0; i < networkBytes.Length; i++)
            {
                networkBytes[i] = (byte)(ipBytes[i] & maskBytes[i]);
            }
            return new IPAddress(networkBytes).ToString();
        }



        public string ObtenerUnidadOrganizativa()
        {
            string nombreOU = "Sin UO";

            try
            {
                using (PrincipalContext contexto = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal usuario = UserPrincipal.FindByIdentity(contexto, Environment.UserName);
                    if (usuario != null && !string.IsNullOrEmpty(usuario.DistinguishedName))
                    {
                        string[] partesDistinguishedName = usuario.DistinguishedName.Split(',');
                        foreach (string parte in partesDistinguishedName)
                        {
                            if (parte.Trim().StartsWith("OU="))
                            {
                                nombreOU = parte.Replace("OU=", "").Trim();

                                break;
                            }
                        }
                    }
                }
            }
            catch
            {
                nombreOU = "Fuera de dominio";
            }

            return nombreOU;
        }
      
        public int ObtenerUnidadOrgId(string nombreOU)
        {   //// OBTENER EL ID DEL DEPARTAMENTO          
          string org  =  ObtenerUnidadOrganizativa();

            var oficina =  _context.Oficinas
                                  .FirstOrDefault(q => q.OficinasNombre == nombreOU);
            return oficina?.OficinasId ?? 0;
        }


        public bool EsUsuarioGlobal()
        {
            string org = ObtenerUnidadOrganizativa();
            var globales = new List<string> { "DESARROLLO DIGITAL", "JEFATURA", "REDES", "CONTROL Y RESGUARDO DE LA INFORMACION" };
            return globales.Contains(org?.Trim().ToUpper() ?? "");
        }


        /*  public int NumeroTicketExiste(string numeroOficio)
            {
                /// OBTENER PRIMERO EL ID DEL OFICIO PARA BUSCARLO EN LA TABLA DE OficioRespuesta
                var ofin = _context.NumOficio
                   .Where(x => x.NumeroConsecutivo == numeroOficio)
                   .OrderByDescending(x => x.OficioId)
                   .FirstOrDefault();


                return 0;
            }
        */

        /*    private int ObtenerUnidadOrgId()
                  {
                      string org = ObtenerUnidadOrganizativa();

                      var oficina = _context.Oficinas
                                            .FirstOrDefault(q => q.OficinasNombre == org);

                      // Devolvemos el ID si se encontró, o 0 si no existe
                      return oficina != null ? oficina.OficinasId : 0;
                  }
        */

        /*    private void ObtenerUnidaOrgId()
                  {   //// OBTENER EL ID DEL DEPARTAMENTO            
                         string org = ObtenerUnidadOrganizativa();
                         var oficinaEncontrada = _context.Oficinas
                          .FirstOrDefault(q => q.OficinasNombre == org);
                         return  oficinaEncontrada; 
                  }

       */

        /*  public string ObtenerOficioAntes (int IdOf)
          {
              var OficioAntes = _context.
                  .Where(x => x.TicketId == ticketId)
                  .OrderByDescending(x => x.HiloTicketFecha)
                  .FirstOrDefault();
       
          }
       */

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
                UsuarioId = ObtenerNombreUsuarioRed(),
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
            int TicketId,
            string UsuarioId)
        {
            var nuevoNumOficio = new NumOficio
            {
                NumeroConsecutivo = NumCon,
                Bis = Bis, 
                Oficinas_Id = Oficinas_Id,
                Tipo = Tipo,
                Anio = Anio,
                FechaCaptura=DateTime.Now,
                TicketId= TicketId,
                UsuarioId = UsuarioId

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

        /*
        public void RegistrarHiloOficio(int ticketId, string numeroOficio)
        {
            var hilo = new HiloTicket
            {
                TicketId = ticketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = "CERRADO",
                HiloTicketMensaje = numeroOficio,
                UsuarioId = 0 
            };

            _context.HiloTicket.Add(hilo);

            _context.SaveChanges();
        }
        */
   
        public void RegistrarHiloOficio(int ticketId, string numeroOficio, string accion = "CERRADO")
        {
            var hilo = new HiloTicket
            {
                TicketId = ticketId,
                HiloTicketFecha = DateTime.Now,

                HiloTicketAccion = accion,

                HiloTicketMensaje = numeroOficio,
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