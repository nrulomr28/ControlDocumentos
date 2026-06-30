using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;

namespace OficiosTI
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Configuración estándar de WinForms (.NET 6+)
            ApplicationConfiguration.Initialize();

            // ==========================================
            // CADENA DE CONEXIÓN
            // ==========================================

            const string ConnectionString =
                "Server=10.8.3.115;Database=OficiosTI;User Id=usrOficiosTI;Password=tyNmYDb3Vk;TrustServerCertificate=True";

            // Desarrollo local
            // const string ConnectionString =
            //     "Server=CSOSAG-PC\\SQLEXPRESS01;Database=OficiosTI;User Id=prueba;Password=s1st3m40MS$P;TrustServerCertificate=True";

            var options = new DbContextOptionsBuilder<OficiosContext>()
                .UseSqlServer(ConnectionString)
                .Options;

            // Se libera automáticamente al cerrar la aplicación
            using var context = new OficiosContext(options);

            // Configuración de QuestPDF 
            // Migrado a ReportingServices
            //QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            // ==========================================
            // FORMULARIO INICIAL
            // ==========================================

            Application.Run(new FrmTickets(context));

            // Alternativas para pruebas
            // Application.Run(new FrmPanelTickets(context));
        }
    }
}