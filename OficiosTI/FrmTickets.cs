using DocumentFormat.OpenXml.Drawing.Charts;
using Microsoft.VisualBasic.ApplicationServices;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Services;
using OficiosTI.UI;

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

            // Suscripción de eventos (barato, sin BD)
            DataGridTickets.DataBindingComplete += DataGridTickets_DataBindingComplete;
            DataGridTickets.CellClick += DataGridTickets_CellClick;
            DataGridTickets.CellFormatting += DataGridTickets_CellFormatting;
        }

        private async void FrmTickets_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UseWaitCursor = true;

                // 1) Estructura visual primero (rápido, sin BD)
                InicializarGrid();
                this.Font = new Font("Gotham", 10F, FontStyle.Regular, GraphicsUnit.Point);

                // 2) Combos y catálogos
                CargarUser();
                CargarDominio();
                CargarStatus();
                //CargarPrioridad();
                CargarComboSuplente();

                // 3) Datos dependientes de sesión / organización
                ObtenerOrganizacion();
                ObtenerSegmentoDeRed();

                string org = _service.ObtenerUnidadOrganizativa();
                int idDepartamento = _service.ObtenerUnidadOrgId(org);

                // 4) Permisos (depende de org)
                AplicarPermisosInterfaz(idDepartamento, org);

                // 5) Visibilidad de suplente (depende de usuario logueado)
                ConfigurarVisibilidadSuplente();

                // 6) Datos pesados al final
                CargarTickets();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Ocurrió un error al inicializar el formulario:\n\n" + ex.Message,
                    "Error de inicialización",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            finally
            {
                UseWaitCursor = false;
                Cursor = Cursors.Default;
            }
        }

        private void ConfigurarVisibilidadSuplente()
        {
            string usuarioDominioLogueado = _service.ObtenerDominioYUsuario();

            var usuarioActual = _context.Usuarios
                .Where(y => y.UsuarioDominio == usuarioDominioLogueado)
                .Select(y => new
                {
                    y.Jefe,
                    y.SuplenteJefe
                })
                .FirstOrDefault();

            bool esJefeOSuplente =
                usuarioActual != null &&
                (usuarioActual.Jefe == 1 || usuarioActual.SuplenteJefe == 1);

            cmbSuplente.Visible = esJefeOSuplente;
            lblSuplente.Visible = esJefeOSuplente;
            btnSuplente.Visible = esJefeOSuplente;
            lblSuplenteAsignado.Visible = esJefeOSuplente;
        }      

        private void AplicarPermisosInterfaz(int oficinaId, string nombreOU)    
        {   /// permisos del boton para asignar los oficios de los demas departamentos 
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            bool tienePermiso = _context.Oficinas
                                       .Any(x => x.OficinasId == miOficinaId && x.Permiso == 1);
            btnAsignar.Visible = tienePermiso;    /// numeros de oficio para respuestas con origen  =1
            btnSinTicket.Visible = tienePermiso;  /// numeros de oficio para respuestas sin origen  =1
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SigelicXPTheme.ApplyForm(this);
            SigelicXPTheme.ApplyStartButton(BtnAdjuntos);
        }

        private void CargarUser()
        {
            lbUser.Text = $"Usuario: {_service.ObtenerNombreUsuarioRed()}";
        }

        private void CargarDominio()
        {
            lbDominio.Text = $"USUARIO: {_service.ObtenerDominioYUsuario()}";
        }

        private void ObtenerSegmentoDeRed()
        {
            lbSegmento.Text = $"Segmento: {_service.ObtenerSegmentoDeRed()}";
        }
        private void ObtenerOrganizacion()
        {
            // lbOrganizacion.Text = $"Departamento: {_service.ObtenerUnidadOrganizativa()}";
            LabelTitulo.Text = $"Tickets - {_service.ObtenerUnidadOrganizativa()} TI";
        }

        private void InicializarGrid()
        {
            DataGridTickets.AutoGenerateColumns = true;
        }
        private void CargarStatus()
        {
            var status = _context.Cat_TicketStatus
                .Where(x => x.Cat_TicketStatusId > 1 && x.Cat_TicketStatusId <= 3)
                .ToList();
            ComboStatus.DataSource = status;
            ComboStatus.DisplayMember = "Cat_TicketStatusStatus";
            ComboStatus.ValueMember = "Cat_TicketStatusId";
            ComboStatus.SelectedIndex = -1;
        }        


        public class ItemComboUsuario
        {
            public string NombreCompleto { get; set; }
            public int? SuplenteJefe { get; set; }
            public int UsuarioAnterior { get; set; }
            public int? Jefe { get; set; }         
        }
        private void CargarComboSuplente()
        {
            try
            {
                int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());               
                var usuariosSuplentes = _context.Usuarios
                    .Where(x => x.IdOficina == miOficinaId &&
                                x.Activo == 1 &&
                                x.Jefe == 0)
                    .Select(x => new ItemComboUsuario 
                    {
                        NombreCompleto = x.NombreCompleto,
                        SuplenteJefe = x.SuplenteJefe,
                        UsuarioAnterior = x.UsuarioAnterior,
                        Jefe = x.Jefe
                    })
                    .ToList();               
                var listaCombo = new List<ItemComboUsuario>();   
                listaCombo.Add(new ItemComboUsuario 
                {
                    NombreCompleto = "-- SIN SUPLENTE --",
                    SuplenteJefe = 0,
                    UsuarioAnterior = 0,
                    Jefe = 0
                });

                listaCombo.AddRange(usuariosSuplentes);                
                cmbSuplente.DisplayMember = "NombreCompleto";
                cmbSuplente.ValueMember = "UsuarioAnterior";
                cmbSuplente.DataSource = listaCombo;

                var idsuplente = usuariosSuplentes.FirstOrDefault(x => x.SuplenteJefe == 1);
                if (idsuplente != null)
                {
                    cmbSuplente.SelectedValue = idsuplente.UsuarioAnterior;
                    lblSuplenteAsignado.Text = $"Suplente: {Environment.NewLine} {idsuplente.NombreCompleto}";
                }
                else
                {
                    cmbSuplente.SelectedIndex = 0;
                    lblSuplenteAsignado.Text = "No hay Suplente";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los analistas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void CargarTickets()
        {
            string textoBuscar = TextBuscar.Text.Trim();
            
            string status = ComboStatus.SelectedValue?.ToString() ?? "";
            int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
            bool esUsuarioGlobal = _service.EsUsuarioGlobal();
            DateTime fechaLimite = DateTime.Now.AddMonths(-2);
            string usuarioDominioLogueado = _service.ObtenerDominioYUsuario();
            // var usuarioActual = _context.Usuarios
            //                               .FirstOrDefault(y => y.UsuarioDominio == usuarioDominioLogueado);
            var usuarioActual = _context.Usuarios
                               .Where(y => y.UsuarioDominio == usuarioDominioLogueado)
                               .Select(y => new
                               {
                                   y.UsuarioAnterior,
                                   y.Jefe,
                                   y.SuplenteJefe
                               })
                               .FirstOrDefault();
            int idUsuarioActual = 0;
            bool esJefe = false;
            if (usuarioActual != null)
            {
                idUsuarioActual = usuarioActual.UsuarioAnterior;
                //  JEFE SI:  titular (1)  o suplente
                esJefe = (usuarioActual.Jefe == 1) || (usuarioActual.SuplenteJefe == 1);
                if (idUsuarioActual == 0)
                {
                    MessageBox.Show($"Usuario encontrado, pero 'UsuarioAnterior' en la BD es 0.\nRevisa el registro de: {usuarioDominioLogueado}", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show($"No se encontró el usuario en la BD. \nTexto buscado exactamente: '{usuarioDominioLogueado}'", "Error de Sesión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var query = _context.Ticket.AsQueryable();
            if (esUsuarioGlobal)
            {
                query = query.Where(t => t.Cat_TicketStatusId >= 1);
            }
            else if (esJefe)
            {
                //  El jefe o el suplente ve todo lo de su Departamento
                query = query.Where(t => t.OficinasId == miOficinaId);
            }
            else
            {
                // ANALISTA
                //    string idBuscado = idUsuarioActual.ToString();
                query = query.Where(t => t.OficinasId == miOficinaId
                               && t.TicketUUsuario == idUsuarioActual);
            }
            query = query.Where(t => t.TicketFecha >= fechaLimite);

            if (!string.IsNullOrWhiteSpace(textoBuscar))
            {
                query = query.Where(t => t.TicketPersona.Contains(textoBuscar) ||
                                         t.TicketAsunto.Contains(textoBuscar));
            }            

            if (!string.IsNullOrWhiteSpace(status) && int.TryParse(status, out int statusId))
            {
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
                    /// PRIMER TABLA DE ESTADOS 
                    EstadoTicket = _context.Cat_TicketStatus
                        .Where(o => o.Cat_TicketStatusId == t.Cat_TicketStatusId)
                        .Select(o => o.Cat_TicketStatusStatus)
                        .FirstOrDefault(),
                    DepartamentoNombre = _context.Oficinas
                        .Where(o => o.OficinasId == t.OficinasId)
                        .Select(o => o.OficinasNombre)
                        .FirstOrDefault(),
                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault(),
                    /// TABLA DE TICKETS SIN OFICIOS 
                    EstadoSinOficio = _context.Ticket_SinOficio
                        .Where(s => s.TicketId == t.TicketId)
                        .Select(s => s.Cat_TicketStatusStatus)
                        .FirstOrDefault()
                })
                // Ordenamiento personalizado
                .OrderBy(t =>
                    (t.Cat_TicketStatusId == 1) ? 1 :                          // PRIMERO ABIERTOS
                    (t.Cat_TicketStatusId == 2) ? 2 :                          // LUEGO EN PROCESO
                    (t.Cat_TicketStatusId == 3 && string.IsNullOrEmpty(t.NumeroOficio)) ? 3 : // CERRADOS SIN OFICIO
                    (t.Cat_TicketStatusId == 3) ? 4 :                          // CERRADOS CON OFICIO
                    5)                                                          // OTROS AL FINAL
                .ThenByDescending(t => t.Cat_TicketStatusId)
                .ToList();
            DataGridTickets.DataSource = null;
            DataGridTickets.DataSource = new BindingSource { DataSource = tickets };
            ConfigurarGrid();
            ActualizarIndicadores(tickets);
        }
        private void DataGridTickets_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // 1. Validar que el evento sea válido (no encabezados)
            if (e.RowIndex < 0 || e.ColumnIndex < 0)
                return;

            // 2. Validar que el grid no sea null (defensivo)
            if (DataGridTickets?.Rows == null || e.RowIndex >= DataGridTickets.Rows.Count)
                return;

            try
            {
                // 3. Obtener el bound item de forma segura
                var row = DataGridTickets.Rows[e.RowIndex];
                if (row?.DataBoundItem is not TicketGridModel gridItem)
                    return;

                // 4. Validar el Id antes de usarlo
                if (gridItem.TicketId <= 0)
                {
                    MessageBox.Show("El ticket seleccionado no tiene un identificador válido.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 5. Validar contexto (por si el form se está cerrando / disposing)
                if (_context == null)
                {
                    MessageBox.Show("El contexto de datos no está disponible.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 6. Buscar el ticket
                var ticket = _context.Ticket.Find(gridItem.TicketId);
                if (ticket == null)
                {
                    MessageBox.Show("No se encontró el ticket. Es posible que haya sido eliminado.",
                        "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    // Opcional: refrescar el grid para reflejar el estado real
                    // CargarTickets();
                    return;
                }

                // 7. Evitar múltiples aperturas simultáneas (anti doble-click)
                using var frm = new FrmTicketDetalle(ticket, _context);
                frm.ShowDialog(this);
            }
            catch (ObjectDisposedException)
            {
                // El contexto o el form ya fueron liberados
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show($"No se pudo abrir el ticket: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error inesperado al abrir el ticket:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Aquí podrías loguear: Logger.Error(ex);
            }
        }

        private void BtnAbrirTicket_Click(object sender, EventArgs e)
        {
            var ticket = ObtenerTicketSeleccionado();
            if (ticket == null)
                return;
            new FrmTicketDetalle(ticket, _context).ShowDialog();
            CargarTickets();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)   ////  BUSCAR POR GLOBAL O USUARIO NORMAL 
        {
            string texto = TextBuscar.Text.Trim();            
            string status = ComboStatus.SelectedValue?.ToString() ?? "";
            string org = _service.ObtenerUnidadOrganizativa();
            int idorg = _service.ObtenerUnidadOrgId(org);
            var query = _context.Ticket.AsQueryable();
            bool esUsuarioGlobal = _service.EsUsuarioGlobal();
            if (esUsuarioGlobal)
            {
                query = query.Where(t => t.id_of != null && t.Cat_TicketStatusId >= 1); /// USUARIO GLOBAL VE TODOS LOS TICKETS PARA CERRARLOS SI SE EQUIVOCAN
            }
            else
            {
             // query = query.Where(t => t.OficinasId == miOficinaId && t.id_of != null && t.Cat_TicketStatusId > 1);
                query = query.Where(t => t.OficinasId == idorg);
            }
            // query = query.Where(t => t.TicketFecha >= fechaLimite);
            // query = query.Where(t => t.OficinasId == idorg);
            if (!string.IsNullOrWhiteSpace(texto))
            {
                query = query.Where(t =>
                    t.TicketPersona.Contains(texto) ||
                    t.TicketAsunto.Contains(texto) ||
                    t.TicketId.ToString().Contains(texto));
            }

            if (!string.IsNullOrWhiteSpace(status) && int.TryParse(status, out int statusId))
            {
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
                    // PRIMER TABLA DE ESTADOS
                    EstadoTicket = _context.Cat_TicketStatus
                        .Where(o => o.Cat_TicketStatusId == t.Cat_TicketStatusId)
                        .Select(o => o.Cat_TicketStatusStatus)
                        .FirstOrDefault(),
                    DepartamentoNombre = _context.Oficinas
                        .Where(o => o.OficinasId == t.OficinasId)
                        .Select(o => o.OficinasNombre)
                        .FirstOrDefault(),

                    NumeroOficio = _context.OficioRespuesta
                        .Where(o => o.TicketId == t.TicketId)
                        .Select(o => o.NumeroOficio)
                        .FirstOrDefault(),

                    /// TABLA DE ESTADOS SIN OFICIO 
                    EstadoSinOficio = _context.Ticket_SinOficio
                        .Where(s => s.TicketId == t.TicketId)
                        .Select(s => s.Cat_TicketStatusStatus)
                        .FirstOrDefault()
                })
                .OrderByDescending(t => t.TicketFecha)
                .ToList();
            DataGridTickets.DataSource = null;
            DataGridTickets.DataSource = new BindingSource { DataSource = tickets };
            ConfigurarGrid();
        }

        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridTickets.CurrentRow == null)
                {
                    MessageBox.Show("No hay ninguna fila seleccionada en el DataGrid.", "Depuración");
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
                MessageBox.Show($"Error crítico:\n\n{ex.Message}\n\nDetalles técnicos:\n{ex.StackTrace}",
                    "Error grave",
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
            DataGridTickets.Columns["EstadoTicket"].Visible = false;
            DataGridTickets.Columns["EstadoSinOficio"].Visible = false;
            DataGridTickets.Columns["TicketId"].HeaderText = "Ticket";
            DataGridTickets.Columns["TicketPersona"].HeaderText = "Envía";
            DataGridTickets.Columns["TicketAsunto"].HeaderText = "Asunto";
            DataGridTickets.Columns["TicketPrioridad"].HeaderText = "Prioridad";
            DataGridTickets.Columns["TicketFecha"].HeaderText = "Fecha";
            DataGridTickets.Columns["NumeroOficio"].HeaderText = "Oficio";
            DataGridTickets.Columns["DepartamentoNombre"].HeaderText = "Departamento";
            ResaltarOficios();
            //  AgregarColumnaOficio();    
            var col0 = DataGridTickets.Columns[0];
            col0.ReadOnly = true;
            col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            //  col0.DefaultCellStyle.SelectionBackColor = DataGridTickets.DefaultCellStyle.BackColor;
        }
        private void ResaltarOficios()
        {
            foreach (DataGridViewRow row in DataGridTickets.Rows)
            {
                if (row.IsNewRow) continue;

                var oficio = row.Cells["NumeroOficio"].Value;
                var estado = row.Cells["Cat_TicketStatusId"].Value;
             //   var estnom = row.Cells["Estado"].Value;

                if (oficio != null && !string.IsNullOrWhiteSpace(oficio.ToString()))
                {
                    row.Cells["NumeroOficio"].Style.ForeColor = Color.DarkGreen;
                    row.Cells["NumeroOficio"].Style.Font = new Font(DataGridTickets.Font, FontStyle.Bold);
                }
                if (estado != null && int.TryParse(estado.ToString(), out int estadoId))
                {
                    if (estadoId == 2)
                    {  ///// EN PROCESO            
                        row.Cells["NumeroOficio"].Style.ForeColor = Color.CornflowerBlue;
                        row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                    }
                    if (estadoId == 3)
                    {  /////// CERRADO 
                        row.DefaultCellStyle.BackColor = Color.Honeydew;
                        row.Cells["NumeroOficio"].Style.ForeColor = Color.CornflowerBlue;
                        row.Cells["Estado"].Style.ForeColor = Color.CornflowerBlue;
                        row.Cells["Cat_TicketStatusId"].Style.ForeColor = Color.CornflowerBlue;
                        row.Cells["Cat_TicketStatusId"].Style.Font = new Font(DataGridTickets.Font, FontStyle.Bold);
                        row.Cells["Estado"].Style.Font = new Font(DataGridTickets.Font, FontStyle.Bold);
                        row.Cells["TicketId"].Style.ForeColor = Color.CornflowerBlue;
                        row.Cells["TicketId"].Style.Font = new Font(DataGridTickets.Font, FontStyle.Bold);
                    }
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
            ComboStatus.SelectedIndex = -1;
            CargarTickets();
        }
        private void ActualizarIndicadores(List<TicketGridModel> tickets)
        {
            int total = tickets.Count;
            int respondidos = tickets.Count(t =>
                !string.IsNullOrEmpty(t.NumeroOficio));
            int pendientes = total - respondidos;  /// TICKETS PENDIENTES SON LOS QUE NO TIENEN OFICIO, INCLUYENDO LOS CERRADOS Y EN PROCESO. 

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
          //  var formOficioSin = new FrmPanelTickets(_context);
          //  formOficioSin.ShowDialog();
        }

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

        private void btnSinTicket_Click_1(object sender, EventArgs e)
        {
            Ticket ticketNulo = null;
            FrmOficioTicket formAsignar = new FrmOficioTicket(ticketNulo, _context);
            formAsignar.ShowDialog();
        }

        private void btnSuplente_Click(object sender, EventArgs e)
        {
            if (cmbSuplente.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione una opción de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
                int idSeleccionado = Convert.ToInt32(cmbSuplente.SelectedValue);             
                var suplenteActual = _context.Usuarios
                    .FirstOrDefault(x => x.IdOficina == miOficinaId &&
                                         x.Activo == 1 &&
                                         x.SuplenteJefe == 1);
                if (idSeleccionado == 0)
                {
                    if (suplenteActual != null)
                    {
                        suplenteActual.SuplenteJefe = 0;
                        _context.SaveChanges();

                        lblSuplenteAsignado.Text = "No hay Suplente";
                        MessageBox.Show("El suplente ha sido removido exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("No hay ningún suplente asignado actualmente.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }              
                if (suplenteActual != null && suplenteActual.UsuarioAnterior == idSeleccionado)
                {
                    MessageBox.Show("Este usuario ya es el suplente actual.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (suplenteActual != null)
                {
                    suplenteActual.SuplenteJefe = 0;
                }
                
                var nuevoSuplente = _context.Usuarios
                    .FirstOrDefault(x => x.IdOficina == miOficinaId &&
                                         x.Activo == 1 &&
                                         x.UsuarioAnterior == idSeleccionado);

                if (nuevoSuplente != null)
                {
                    nuevoSuplente.SuplenteJefe = 1;
                  
                    _context.SaveChanges();
                 
                    lblSuplenteAsignado.Text = $"Suplente: {Environment.NewLine} {nuevoSuplente.NombreCompleto}";
                    MessageBox.Show("El suplente ha sido asignado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("No se encontró el usuario seleccionado en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar los cambios: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
