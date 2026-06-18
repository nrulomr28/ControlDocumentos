using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.UI;


namespace OficiosTI
{
    public partial class FrmTickets : Form
    {
        private readonly OficiosContext _context;


        public FrmTickets(OficiosContext context)
        {
            InitializeComponent();
            _context = context;

            InicializarGrid();
            CargarTickets();
            DataGridTickets.DataBindingComplete += DataGridTickets_DataBindingComplete;
            DataGridTickets.CellClick += DataGridTickets_CellClick;
            DataGridTickets.CellFormatting += DataGridTickets_CellFormatting;
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            SigelicXPTheme.ApplyForm(this);
            SigelicXPTheme.ApplyStartButton(BtnAdjuntos);


        }

        private void InicializarGrid()
        {
            DataGridTickets.AutoGenerateColumns = true;
        }

        private void CargarTickets()
        {
            string textoBuscar = TextBuscar.Text.Trim();
            string prioridad = ComboPrioridad.Text;
            string status = ComboStatus.Text;


            var query = _context.Ticket

           .Where(t => t.OficinasId == 3 && t.id_of != null);

            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(t =>
                    t.TicketPersona.Contains(textoBuscar) ||
                    t.TicketAsunto.Contains(textoBuscar));
            }

            if (!string.IsNullOrWhiteSpace(prioridad) && prioridad != "Todos")
            {
                query = query.Where(t => t.TicketPrioridad == prioridad);
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "Todos")
            {
                int statusId = int.Parse(status);
                query = query.Where(t => t.Cat_TicketStatusId == statusId);
            }

            var tickets = query
                .Select(t => new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    Cat_TicketStatusId = t.Cat_TicketStatusId,

                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault()
                })
                .Where(t => t.TicketFecha >= DateTime.Now.AddMonths(-2))
                .OrderByDescending(t => t.TicketFecha)
                .ToList();

            DataGridTickets.DataSource = null;

            var bs = new BindingSource();
            bs.DataSource = tickets;

            DataGridTickets.DataSource = bs;

            ConfigurarGrid();
            ActualizarIndicadores(tickets);
        }

        private void DataGridTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var gridItem = (TicketGridModel)DataGridTickets.Rows[e.RowIndex].DataBoundItem as TicketGridModel;

            if (gridItem == null)
                return;

            var ticket = _context.Ticket.Find(gridItem.TicketId);

            if (ticket == null)
            {
                MessageBox.Show("No se encontró el ticket.");
                return;
            }

            var frm = new FrmTicketDetalle(ticket, _context);

            frm.ShowDialog();

            CargarTickets();

        }

        private void BtnAbrirTicket_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();

            if (ticket == null)
                return;

            new FrmTicketDetalle(ticket, _context).ShowDialog();

            CargarTickets();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            string texto = TextBuscar.Text.Trim();
            string prioridad = ComboPrioridad.Text;
            string status = ComboStatus.Text;

            var query = _context.Ticket
                .Where(t => t.OficinasId == 3);

            if (!string.IsNullOrWhiteSpace(texto))
            {
                query = query.Where(t =>
                    t.TicketPersona.Contains(texto) ||
                    t.TicketAsunto.Contains(texto) ||
                    t.TicketId.ToString().Contains(texto));
            }

            if (!string.IsNullOrWhiteSpace(prioridad) && prioridad != "Todos")
            {
                query = query.Where(t => t.TicketPrioridad == prioridad);
            }

            if (!string.IsNullOrWhiteSpace(status) && status != "Todos")
            {
                int statusId = int.Parse(status);
                query = query.Where(t => t.Cat_TicketStatusId == statusId);
            }

            var tickets = query
                .Select(t => new TicketGridModel
                {
                    TicketId = t.TicketId,
                    TicketPersona = t.TicketPersona,
                    TicketAsunto = t.TicketAsunto,
                    TicketPrioridad = t.TicketPrioridad,
                    TicketFecha = t.TicketFecha,
                    Cat_TicketStatusId = t.Cat_TicketStatusId,
                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault()
                })
                .OrderByDescending(t => t.TicketFecha)
                .ToList();

            var bs = new BindingSource();
            bs.DataSource = tickets;

            DataGridTickets.DataSource = bs;

            ConfigurarGrid();
        }

        //private void BtnGenerarOficio_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (DataGridTickets.CurrentRow == null)
        //            return;

        //        var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

        //        if (gridItem == null)
        //            return;

        //        if (!string.IsNullOrEmpty(gridItem.NumeroOficio))
        //        {
        //            var r = MessageBox.Show(
        //                "Este ticket ya tiene oficio. ¿Desea editarlo?",
        //                "Oficio existente",
        //                MessageBoxButtons.YesNo);

        //            if (r == DialogResult.No)
        //                return;
        //        }

        //        var ticket = ObtenerTicketSeleccionado();

        //        if (ticket == null)
        //            return;

        //        new FrmOficioRespuesta(ticket, _context).ShowDialog();

        //        CargarTickets();
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridTickets.CurrentRow == null)
                {
                    MessageBox.Show("Falla 1: No hay ninguna fila seleccionada en el DataGrid.", "Depuración");
                    return;
                }

                var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;


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



                new FrmOficioRespuesta(ticket, _context).ShowDialog();

                CargarTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"ERROR GRAVE:\n\n{ex.Message}\n\nDetalles técnicos:\n{ex.StackTrace}",
                    "Error detectado",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            foreach (DataGridViewColumn col in DataGridTickets.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }

            DataGridTickets.Columns["TicketId"].HeaderText = "Ticket";
            DataGridTickets.Columns["TicketPersona"].HeaderText = "Persona";
            DataGridTickets.Columns["TicketAsunto"].HeaderText = "Asunto";
            DataGridTickets.Columns["TicketPrioridad"].HeaderText = "Prioridad";
            DataGridTickets.Columns["TicketFecha"].HeaderText = "Fecha";
            DataGridTickets.Columns["NumeroOficio"].HeaderText = "Oficio";

            //ResaltarOficios();

            AgregarColumnaOficio();


            // Blindar columna 0
            var col0 = DataGridTickets.Columns[0];
            col0.ReadOnly = true;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            col0.DefaultCellStyle.SelectionBackColor = DataGridTickets.DefaultCellStyle.BackColor;
        }

        private void ResaltarOficios()
        {
            foreach (DataGridViewRow row in DataGridTickets.Rows)
            {
                var oficio = row.Cells["NumeroOficio"].Value;

                if (oficio != null && oficio.ToString() != "")
                {
                    row.Cells["NumeroOficio"].Style.ForeColor = Color.DarkGreen;
                    row.Cells["NumeroOficio"].Style.Font =
                        new Font(DataGridTickets.Font, FontStyle.Bold);

                    row.DefaultCellStyle.BackColor = Color.Honeydew;
                }
            }
        }

        private Ticket ObtenerTicketSeleccionado()
        {
            if (DataGridTickets.CurrentRow == null)
                return null;

            var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

            return _context.Ticket.Find(gridItem.TicketId);
        }

        private void DataGridTickets_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ResaltarOficios();
        }


        private void AgregarColumnaOficio()
        {
            if (DataGridTickets.Columns["AbrirOficio"] != null)
                return;

            var col = new DataGridViewButtonColumn();

            col.Name = "AbrirOficio";
            col.HeaderText = "Documento";
            col.Text = "📄";
            col.UseColumnTextForButtonValue = true;
            col.Width = 60;

            DataGridTickets.Columns.Add(col);
        }

        private void DataGridTickets_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            if (e.ColumnIndex < 0)
                return;

            if (e.ColumnIndex == 0)
                return;

            if (DataGridTickets.Columns[e.ColumnIndex].Name != "AbrirOficio")
                return;

            var gridItem = DataGridTickets.Rows[e.RowIndex].DataBoundItem as TicketGridModel;
            if (gridItem == null)
                return;

            if (string.IsNullOrEmpty(gridItem.NumeroOficio))
            {
                MessageBox.Show("Este ticket no tiene oficio generado.");
                return;
            }
            var ticket = _context.Ticket.Find(gridItem.TicketId);

            if (ticket == null)
                return;

            var frm = new FrmOficioRespuesta(ticket, _context);

            frm.ShowDialog();

            CargarTickets();
        }

        private void DataGridTickets_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (DataGridTickets.Columns[e.ColumnIndex].Name == "AbrirOficio")
            {
                var row = DataGridTickets.Rows[e.RowIndex];
                var oficio = row.Cells["NumeroOficio"].Value;

                if (oficio == null || oficio.ToString() == "")
                {
                    e.Value = "";
                }
            }
        }
        private void BtnLimpiar_Click(object sender, EventArgs e)
        {
            TextBuscar.Text = "";
            ComboPrioridad.SelectedIndex = -1;
            ComboStatus.SelectedIndex = -1;

            CargarTickets();
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

        private void BtnAdjuntos_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();

            if (ticket == null)
                return;

            var frm = new FrmAdjuntos(ticket.TicketId, _context);

            frm.ShowDialog();

            CargarTickets();
        }


        private void BtnAbrirTicket_Click_1(object sender, EventArgs e)
        {

        }


        /*
                private void button1_Click(object sender, EventArgs e)
                {
                    FrmOficioTicket FormAsignar = new FrmOficioTicket();
                    FormAsignar.ShowDialog();

                    var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;
                }*/


        private void button1_Click(object sender, EventArgs e)
        {
            if (DataGridTickets.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione un ticket de la lista primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 2. Obtener el item del Grid PRIMERO
            var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;

            Ticket ticketSeleccionado = _context.Ticket.Find(gridItem.TicketId);

            FrmOficioTicket FormAsignar = new FrmOficioTicket(ticketSeleccionado, _context);

            FormAsignar.ShowDialog();
        }
      
        private void btnOficios_Click(object sender, EventArgs e)
        {
            FrmOficioSin FormOficiosin = new FrmOficioSin(_context);
            FormOficiosin.ShowDialog();

        }
    }
}
