using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Services;
using System;
using System.Windows.Forms;

namespace OficiosTI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

         // ==========================================
         // CADENA DE CONEXIÓN
         // ==========================================
         // const string ConnectionString = "Server=10.8.3.115;Database=OficiosTI;User Id=usrOficiosTI;Password=tyNmYDb3Vk;TrustServerCertificate=True";
            const string ConnectionString = "Server=CSOSAG-PC\\SQLEXPRESS01;Database=OficiosTI;User Id=prueba;Password=s1st3m40MS$P;TrustServerCertificate=True";
            var options = new DbContextOptionsBuilder<OficiosContext>()
                .UseSqlServer(ConnectionString)
                .Options;
            using var context = new OficiosContext(options);            
            UsuarioContexto contextoUsuario;
            try
            {
               
                contextoUsuario = ValidarUsuarioYConstruirPerfil(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el perfil de usuario: {ex.Message}", "Error de inicio", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Cierra la aplicación si hay un error crítico
            }
                      
          //   Application.Run(new FrmMenuPrincipal(context, contextoUsuario));

        //  Application.Run(new FrmCrearTicket(context, contextoUsuario));
        //  USUARIO GLOBAL  VE LA CAPTURA DE LOS OFICIOS Y EL PANEL DE TICKETS DE TODOS 

        if (contextoUsuario.EsGlobal)
               {             
                      Application.Run(new FrmMenuPrincipal(context, contextoUsuario));               
               }
            else
               {
                      // Usuario NORMAL SOLO VE SUS TICKETS
                      Application.Run(new FrmTickets(context));
               }
      

        }

       
    /*    private static UsuarioContexto ValidarUsuarioYConstruirPerfil(DbContextOptions<OficiosContext> options)
        {
            using (var dbLocal = new OficiosContext(options))
            {
                var _oficioService = new OficioRespuestaService(dbLocal);
                string nombreOU = _oficioService.ObtenerUnidadOrganizativa();

                return new UsuarioContexto
                {
                    NombreMostrar = _oficioService.ObtenerNombreUsuarioRed(),
                    DominioYUsuario = _oficioService.ObtenerDominioYUsuario(),
                    SegmentoRed = _oficioService.ObtenerSegmentoDeRed(),
                    UnidadOrganizativa = nombreOU,
                    OficinaId = _oficioService.ObtenerUnidadOrgId(nombreOU),
                    EsGlobal = _oficioService.EsUsuarioGlobal()
                };
            }
        }
    */


        /// Validar el usuario para ingresar al sistema 
        private static UsuarioContexto ValidarUsuarioYConstruirPerfil(DbContextOptions<OficiosContext> options)
        {
            using (var dbLocal = new OficiosContext(options))
            {
                var _oficioService = new OficioRespuestaService(dbLocal);
              
                string dominioUsuario = _oficioService.ObtenerDominioYUsuario();
              
                var usuarioValido = dbLocal.Usuarios
                                           .FirstOrDefault(u => u.UsuarioDominio == dominioUsuario && u.Activo == 1);               
                if (usuarioValido == null)
                {
                    throw new UnauthorizedAccessException("Tu usuario de red no está registrado o se encuentra inactivo. Contacta al administrador.");
                }
                               
                string nombreOU = _oficioService.ObtenerUnidadOrganizativa();
                return new UsuarioContexto
                {                    
                    NombreMostrar = usuarioValido.NombreCompleto ?? _oficioService.ObtenerNombreUsuarioRed(),
                    DominioYUsuario = dominioUsuario,
                    SegmentoRed = _oficioService.ObtenerSegmentoDeRed(),
                    UnidadOrganizativa = nombreOU,
                    OficinaId = _oficioService.ObtenerUnidadOrgId(nombreOU),                    
                    EsGlobal = usuarioValido.UsuarioGlobal == 1
                };
            }
        }

    }

   
    
    public class UsuarioContexto
    {
        public string NombreMostrar { get; set; }
        public string DominioYUsuario { get; set; }
        public string SegmentoRed { get; set; }
        public string UnidadOrganizativa { get; set; }
        public int OficinaId { get; set; }
        public bool EsGlobal { get; set; }
    }




   
}