using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Documents;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OficiosTI
{
    public partial class FrmTicketDetalle : Form
    {
        private readonly Ticket _ticket;
        private readonly OficiosContext _context;

        public FrmTicketDetalle(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();

            _ticket = ticket;
            _context = context;

            CargarDatosTicket();
            CargarHilo();
        }

        private void CargarDatosTicket()
        {
            lblTicketId.Text = $"Ticket #{_ticket.TicketId}";
            txtPersona.Text = _ticket.TicketPersona;
            txtAsunto.Text = _ticket.TicketAsunto;
            txtMensaje.Text = _ticket.TicketMensaje;
        }

        private void CargarHilo()
        {
            var hilo = _context.HiloTicket
                .Where(h => h.TicketId == _ticket.TicketId)
                .OrderBy(h => h.HiloTicketFecha)
                .ToList();

            dataGridHilo.DataSource = hilo;
        }

        private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
                return;

            var hilo = new HiloTicket
            {
                TicketId = _ticket.TicketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = "Respuesta",
                HiloTicketMensaje = txtRespuesta.Text,
                UsuarioId = 1
            };

            _context.HiloTicket.Add(hilo);
            _context.SaveChanges();

            txtRespuesta.Clear();

            CargarHilo();
        }

        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            var modelo = new OficioModel
            {
                NumeroOficio = "SSP/OM/DTI/0273/2026",

                Asunto = $"Respuesta a ticket {_ticket.TicketId}",

                Fecha = DateTime.Now,

                Destinatario = _ticket.TicketPersona,

                CargoDestinatario = "",

                Cuerpo = _ticket.TicketMensaje,

                DirectorNombre = "Ing. Luis Felipe Ramírez Flores",

                DirectorCargo = "Director de Tecnologías de la Información",

                Copias = "C.C.P. Archivo."
            };

            var documento = new OficioRespuestaPdf(modelo);

            string ruta = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                $"Oficio_Ticket_{_ticket.TicketId}.pdf");

            documento.GeneratePdf(ruta);

            MessageBox.Show("Oficio generado:\n" + ruta);

            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = ruta,
                UseShellExecute = true
            });
        }
    }
}
