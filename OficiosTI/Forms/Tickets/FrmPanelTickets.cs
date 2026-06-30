using OficiosTI.Aplicacion.DTOs;
using OficiosTI.Aplicacion.Tickets.Services;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmPanelTickets : Form
    {
        private readonly OficiosContext _context;
        private readonly TicketDashboardQueryService _dashboardQueryService;
        public FrmPanelTickets(OficiosContext context)
        {
            InitializeComponent();
            _context = context;
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;
            _dashboardQueryService = new TicketDashboardQueryService(_context);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            XPTheme.ApplyForm(this);
            XPTheme.ApplyStartButton(btnAdjuntos);
            CargarPendientes(); 

        }

        private void tabControlMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (tabControlMain.SelectedIndex)
            {
                case 0:
                    CargarPendientes();
                    break;

                case 1:
                    CargarCerradosSinOficio();
                    break;

                case 2:
                  //  CargarPorAnalista();
                    break;

                case 3:
                    CargarTodos();
                    break;
            }
        }

        private void BtnAdjuntos_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();

            if (ticket == null)
                return;

            var frm = new FrmAdjuntos(ticket.TicketId, _context);

            frm.ShowDialog();            
        }

        private void BtnAbrirTicket_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();

            if (ticket == null)
                return;

            new FrmTicketDetalle(ticket, _context).ShowDialog();            
        }

        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridTickets.CurrentRow == null)
                    return;

                var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

                if (gridItem == null)
                    return;

                if (!string.IsNullOrEmpty(gridItem.NumeroOficio))
                {
                    var r = MessageBox.Show(
                        "Este ticket ya tiene oficio. ¿Desea editarlo?",
                        "Oficio existente",
                        MessageBoxButtons.YesNo);

                    if (r == DialogResult.No)
                        return;
                }
                var ticket = ObtenerTicketSeleccionado();
                if (ticket == null)
                    return;
                new FrmOficioRespuesta(ticket, _context).ShowDialog();                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        
        private Ticket ObtenerTicketSeleccionado()
        {
            if (DataGridTickets.CurrentRow == null)
                return null;

            var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

            return _context.Ticket.Find(gridItem.TicketId);
        }

        private void ActualizarIndicadores(List<TicketGridModel> tickets)
        {
            int total = tickets.Count;

            int respondidos = tickets.Count(t =>
                !string.IsNullOrEmpty(t.NumeroOficio));

            int pendientes = total - respondidos;

            LabelTotalTickets.Text = $"Tickets derivados: {total}";
            LabelRespondidos.Text = $"Respondidos: {respondidos}";
            LabelPendientes.Text = $"Pendientes: {pendientes}";
        }


        

        private void CargarPendientes()
        {
            var tickets = _dashboardQueryService
                        .QueryTicketsBase()
                        .Where(t => t.NumeroOficio == null)
                        .OrderByDescending(t => t.TicketFecha)
                        .ToList();

            gridPendientes.DataSource = tickets;
            ActualizarIndicadores(tickets);
        }

        private void CargarCerradosSinOficio()
        {
            var query = BaseQuery()
                .Where(t =>
                    t.Cat_TicketStatusId == 3 && // ajusta si cambia
                    !_context.OficioRespuesta.Any(o => o.TicketId == t.TicketId));

            gridCerrados.DataSource =
                _dashboardQueryService.Mapear(query);
        }

        
        private IQueryable<Ticket> BaseQuery()
        {
            return _context.Ticket
                .Where(t => t.OficinasId == 3);    //// 1 REDES, 2 CONTROL Y RESGUARDO, DESARRROLLO
        }

        private void CargarTodos()
        {
            var query = BaseQuery();

            gridTodos.DataSource = 
                _dashboardQueryService.Mapear(query);
        }

        [Obsolete("Movido a TicketDashboardQueryService")]
        private IQueryable<TicketGridModel> QueryTicketsBase()
        {
            return _dashboardQueryService.QueryTicketsBase();
        }
    }
}
