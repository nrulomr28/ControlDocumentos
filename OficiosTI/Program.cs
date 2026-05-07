using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;

namespace OficiosTI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            //ApplicationConfiguration.Initialize();
            //Application.Run(new FrmTickets());

            var options = new DbContextOptionsBuilder<OficiosContext>()
                        .UseSqlServer("Server=10.8.3.115;Database=OficiosTI;User Id=usrOficiosTI;Password=tyNmYDb3Vk;TrustServerCertificate=True")
                        .Options;

            var context = new OficiosContext(options);
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;
            Application.Run(new FrmTickets(context));
        }
    }
}