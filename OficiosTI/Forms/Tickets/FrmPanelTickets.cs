
using OficiosTI.Aplicacion.Tickets.Services;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.UI;

namespace OficiosTI
{
    public partial class FrmPanelTickets : Form
    {
        private readonly OficiosContext _context;
        private readonly TicketDashboardService _dashboardService;       
        public FrmPanelTickets(OficiosContext context)
        {
            InitializeComponent();
            _context = context;
            
            _dashboardService = new TicketDashboardService(_context);
            tabControlMain.SelectedIndexChanged += tabControlMain_SelectedIndexChanged;            
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

            if (DataGridTickets.CurrentRow.DataBoundItem is not TicketGridModel gridItem)
                return null;

            return _dashboardService.ObtenerTicket(gridItem.TicketId);
        }

        private void ActualizarIndicadores(List<TicketGridModel> tickets)
        {
            var indicadores = _dashboardService.ObtenerIndicadores(tickets);

            LabelTotalTickets.Text = $"Tickets derivados: {indicadores.Total}";
            LabelRespondidos.Text = $"Respondidos: {indicadores.Respondidos}";
            LabelPendientes.Text = $"Pendientes: {indicadores.Pendientes}";
        }
        
        private void CargarPendientes()
        {
            var tickets = _dashboardService.ObtenerPendientes();

            gridPendientes.DataSource = tickets;

            ActualizarIndicadores(tickets);
        }

        private void CargarCerradosSinOficio()
        {
            gridCerrados.DataSource =
                       _dashboardService.ObtenerCerradosSinOficio();
        }
        
        private void CargarTodos()
        {
            gridTodos.DataSource =
                    _dashboardService.ObtenerTodos();
        }
        
    }
}
