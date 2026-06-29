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
        private readonly Oficinas _oficinas; 

        public FrmTicketDetalle(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();

            _ticket = ticket;
            _context = context;
            

            CargarDatosTicket();
            CargarHilo();
            CargarTicketsR();
        }

        private void CargarDatosTicket()
        {
            lblTicketId.Text = $"Ticket #{_ticket.TicketId}";
            txtPersona.Text = _ticket.TicketPersona;
            txtAsunto.Text = _ticket.TicketMensaje ;
            txtMensaje.Text = _ticket.TicketAsunto;
        }

        private void CargarHilo()
        {
            var hilo = _context.HiloTicket
                .Where(h => h.TicketId == _ticket.TicketId)
                .OrderBy(h => h.HiloTicketFecha)
                .ToList();
            dataGridHilo.DataSource = hilo;
        }


        private void CargarTicketsR()
        {
            if (_ticket == null || _ticket.id_of == 0 || _ticket.id_of == null)
            {
                dataGridRelacion.DataSource = null;
                return;
            }
            var ticketsRelacionados = (from t in _context.Ticket
                                       join o in _context.Oficinas
                                       on t.OficinasId equals o.OficinasId
                                       join s in _context.Cat_TicketStatus
                                       on t.Cat_TicketStatusId equals s.Cat_TicketStatusId
                                       where t.id_of == _ticket.id_of
                                       where t.TicketId != _ticket.TicketId
                                       orderby t.TicketId
                                       select new
                                       {
                                           Ticket = t.TicketId,
                                           Remitente = t.TicketPersona,
                                           Asunto = t.TicketMensaje,
                                           Oficina = o.OficinasNombre,
                                           Estado = s.Cat_TicketStatusStatus
                                       }).ToList();

            dataGridRelacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridRelacion.DataSource = ticketsRelacionados;
        }

        private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
                return;

            var hilo = new HiloTicket
            {
                TicketId = _ticket.TicketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = "RESPUESTA",
                HiloTicketMensaje = txtRespuesta.Text,
                UsuarioId = 0
            };

            _context.HiloTicket.Add(hilo);
            _context.SaveChanges();
            txtRespuesta.Clear();
            CargarHilo();
        }

        /// BOTON DE GENERAR OFICIO PERO DEENTRO DEL ROW 
       /* private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            var modelo = new OficioModel
            {
            //    NumeroOficio = { _oficioActual. = },
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
        }*/
    }
}
