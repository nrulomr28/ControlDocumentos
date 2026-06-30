using Microsoft.VisualBasic.ApplicationServices;
using OficiosTI.Aplicacion.Services;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.UI;
using System.DirectoryServices;
using System.DirectoryServices.ActiveDirectory;


namespace OficiosTI
{
    public partial class FrmTickets : Form
    {
        private readonly OficiosContext _context;

        private OficioRespuestaService _service;
        public FrmTickets(OficiosContext context)
        {
            _context = context;
            _service = new OficioRespuestaService(_context);
            InitializeComponent();
          
            InicializarGrid();
            CargarTickets();
            DataGridTickets.DataBindingComplete += DataGridTickets_DataBindingComplete;
            DataGridTickets.CellClick += DataGridTickets_CellClick;
            DataGridTickets.CellFormatting += DataGridTickets_CellFormatting;
            CargarUser();
            CargarDominio();
           
            ObtenerOrganizacion();
            string org =    _service.ObtenerUnidadOrganizativa();
            int idOficina = _service.ObtenerUnidadOrgId(org);
            ObtenerSegmentoDeRed();
            // Aplicamos los permisos a los botones
            AplicarPermisosInterfaz(idOficina, org);

        }

        private void AplicarPermisosInterfaz(int oficinaId, string nombreOU)
        {
            string ou = nombreOU?.Trim().ToUpper() ?? "";
          if (ou == "DESARROLLO DIGITAL" || ou == "JEFATURA")
         //   if (ou ==  "JEFATURA")
            {
                btnAsignar.Visible = true;
                btnSinTicket.Enabled = true;            
            }
            else
            {       
                btnAsignar.Visible = false;
                btnSinTicket.Visible = false;
       
            }
        }
       

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            XPTheme.ApplyForm(this);
            XPTheme.ApplyStartButton(BtnAdjuntos);
        }
        private void CargarUser()
        {
            lbUser.Text = $"Usuario: {_service.ObtenerNombreUsuarioRed()}";
        }

        private void CargarDominio()
        {
            lbDominio.Text = $"USUARIO Y DOMINIO: {_service.ObtenerDominioYUsuario()}";
        }

        private void ObtenerSegmentoDeRed()
        {
            lbSegmento.Text = $"Segmento: {_service.ObtenerSegmentoDeRed()}";
          //  lbSegmento.Text = $"Segmento: {_service.ObtenerUnidadOrgId()}";
            
        }

        private void ObtenerOrganizacion()
        {
           // lbOrganizacion.Text = $"Departamento: {_service.ObtenerUnidadOrganizativa()}";
            LabelTitulo.Text =  $"Tickets - {_service.ObtenerUnidadOrganizativa()} TI";
        }
        /*     public void ExtraerInfoBasicaAD()
                 {
                     using (PrincipalContext contexto = new PrincipalContext(ContextType.Domain))
                     {
                         // Buscamos al usuario actual de Windows
                         UserPrincipal usuarioAD = UserPrincipal.FindByIdentity(contexto, Environment.UserName);

                         if (usuarioAD != null)
                         {
                             // Propiedades directas que te da C#
                             string nombreMostrar = usuarioAD.DisplayName;      
                             string correo = usuarioAD.EmailAddress;            
                             string telefono = usuarioAD.VoiceTelephoneNumber;  
                             string numEmpleado = usuarioAD.EmployeeId;          
                             string nombrePila = usuarioAD.GivenName;         
                             string apellidos = usuarioAD.Surname;              
                         }
                     }
                 }
        */
        private void InicializarGrid()
        {
            DataGridTickets.AutoGenerateColumns = true;
        }

     /* private void CargarTickets()
        {

            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            var query = _context.Ticket.AsQueryable();
            if (_service.EsUsuarioGlobal())
            {

                query = query.Where(t => t.id_of != null);
            }
            else
            {

                query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null);
            }

            DataGridTickets.DataSource = query.ToList();
        }
     */

        /// MODIFICAR PARA CARGA DE TICKETS
        /// FALTA PONER EL ESTADO Y LA OFICINA

        private void CargarTickets()
        {
            string textoBuscar = TextBuscar.Text.Trim();
            string prioridad = ComboPrioridad.Text;
            string status = ComboStatus.Text;
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            string org = _service.ObtenerUnidadOrganizativa();
            int idorg = _service.ObtenerUnidadOrgId(org);
            //// OBTENER EL ID DEL DEPARTAMENTO 
            ///
            /*  var oficinaEncontrada = _context.Oficinas
               .FirstOrDefault(q => q.OficinasNombre == org);*/
            var query = _context.Ticket.AsQueryable();
        //  var query = _context.Ticket

            if (_service.EsUsuarioGlobal())
            {
                query = query.Where(t => t.id_of != null);
            }
            else
            {
                query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null);
            }
           //  .Where(t => t.OficinasId == idorg && t.id_of != null);   /// 1 REDES, 2 CONTROL Y RESGUARDO, 3 DESARROLLO


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
                    OficinasNombre = _context.Oficinas
                    .Where(o => o.OficinasId == t.OficinasId)
                    .Select(o =>o.OficinasNombre)
                    .FirstOrDefault(),                                   
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
            string org = _service.ObtenerUnidadOrganizativa();
            int idorg = _service.ObtenerUnidadOrgId(org);
            //var oficinaEncontrada = _context.Oficinas
            //  .FirstOrDefault(q => q.OficinasNombre == org);

            var query = _context.Ticket
                .Where(t => t.OficinasId == idorg);    /// 1 REDES, 2 CONTROL Y RESGUARDO, 3 DESARROLLO
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
             DataGridTickets.Columns["OficinasNombre"].HeaderText = "Oficina";

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
          //  FrmPanelTickets formAsignar = new FrmPanelTickets();
            //formAsignar.ShowDialog();
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
        /*
            private void btnSinTicket_Click_1(object sender, EventArgs e)
            {
             var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;
             Ticket ticketSeleccionado = _context.Ticket.Find(gridItem.TicketId);
             FrmOficioTicket FormAsignar = new FrmOficioTicket(ticketSeleccionado, _context);
             FormAsignar.ShowDialog();
            }

            private void btnSinTicket_Click_1(object sender, EventArgs e)
            {
                var gridItem = (TicketGridModel)DataGridTickets.CurrentRow.DataBoundItem;
                Ticket ticketSeleccionado = _context.Ticket.Find(gridItem.TicketId);
                FrmOficioTicket FormAsignar = new FrmOficioTicket(ticketSeleccionado, _context);
                FormAsignar.ShowDialog();
            }
        */

        private void btnSinTicket_Click_1(object sender, EventArgs e)
        {
            Ticket ticketNulo = null;

            FrmOficioTicket formAsignar = new FrmOficioTicket(ticketNulo, _context);
            formAsignar.ShowDialog();
        }
    }
}
