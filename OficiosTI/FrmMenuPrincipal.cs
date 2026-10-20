using OficiosTI.Data;
using OficiosTI.Data.Entities;
using System;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace OficiosTI
{
    public partial class FrmMenuPrincipal : Form
    {
        private readonly OficiosContext _context;
        private readonly UsuarioContexto _usuarioActual;

        public FrmMenuPrincipal(OficiosContext context, UsuarioContexto usuarioActual)
        {
            InitializeComponent();
            _context = context;
            _usuarioActual = usuarioActual;
            this.Text = "Menú Principal " +
                        "- Sistema de Oficios TI";
            lblBienvenida.Text = $"Hola {_usuarioActual.NombreMostrar}, Oficina: {_usuarioActual.UnidadOrganizativa}";
            if (_usuarioActual.EsGlobal)
            {
                btnCapturarOficio.Visible = true;
                btnCrearTicket.Visible = true;
            }
            else
            {
                btnCapturarOficio.Visible = false;
                btnCrearTicket.Visible = false;
            }
            ResaltarOficios();
            InicializarGrid();
            CargarEstado();
            CargarTipoCaptura();
            DataGridOficios.DataBindingComplete += DataGridOficios_DataBindingComplete;
            DataGridOficios.CellDoubleClick += DataGridOficios_CellDoubleClick;
            this.Font = new Font("Gotham", 10F, FontStyle.Regular, GraphicsUnit.Point);
        }
        private void ActualizarIndicadores()
        {
            int total = _context.CapturaOficios
                                .Count(resp => resp.Activo == 1);
            int cerrados = _context.CapturaOficios.Count(resp => resp.Activo == 1 && resp.Estatus == "CERRADO");
            int abiertos = _context.CapturaOficios.Count(resp => resp.Activo == 1 && resp.Estatus == "ABIERTO");
            int proceso =  _context.CapturaOficios.Count(resp => resp.Activo == 1 && resp.Estatus == "EN PROCESO");
            lblAbiertosTotal.Text = $"Total: {abiertos}";
            lblCerradosTotal.Text = $"Cerrados: {cerrados}";
            lblEnProcesoTotal.Text = $"Proceso: {proceso}";
        }
        private void DataGridOficios_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            Font boldFont = new Font(DataGridOficios.Font, FontStyle.Bold);
            foreach (DataGridViewRow row in DataGridOficios.Rows)
            {
                if (row.IsNewRow) continue;
                string oficio = row.Cells["NumOficio"].Value?.ToString();
                string estado = row.Cells["Estado"].Value?.ToString();
                string Ticket = row.Cells["RequiereTicket"].Value?.ToString();
                row.DefaultCellStyle.BackColor = Color.Honeydew;
                if (!string.IsNullOrWhiteSpace(oficio))
                {
                    row.Cells["NumOficio"].Style.ForeColor = Color.DarkGreen;
                    row.Cells["NumOficio"].Style.Font = boldFont;
                }

                if (!string.IsNullOrWhiteSpace(estado))
                {
                    switch (estado.ToUpper())
                    {
                        case "ABIERTO":
                            row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                            row.Cells["NumOficio"].Style.ForeColor = Color.CornflowerBlue;
                            break;

                        case "CERRADO":
                            row.Cells["NumOficio"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["Estado"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["Estado"].Style.Font = boldFont;
                            break;
                    }
                }

                if (!string.IsNullOrWhiteSpace(Ticket))
                {
                    switch (Ticket.ToUpper())
                    {
                        case "NO":
                            row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                            row.Cells["RequiereTicket"].Style.ForeColor = Color.Black;
                            break;
                        case "SI":
                            row.Cells["RequiereTicket"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["RequiereTicket"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["RequiereTicket"].Style.Font = boldFont;
                            break;
                    }
                }
            }
        }


        private void CargarEstado()
        {
            var listaEstado = new[]
                {
                new { Id = -1, Nombre = "TODOS" },
                new { Id =  0, Nombre = "ABIERTO" },
                new { Id =  1, Nombre = "CERRADO" },
                new { Id =  2, Nombre = "EN PROCESO" }
                }.ToList();
            cmbEstado.DataSource = listaEstado;
            cmbEstado.DisplayMember = "Nombre";
            cmbEstado.ValueMember = "Id";
            cmbEstado.SelectedIndex = 0;
        }

        private void CargarTipoCaptura()
        {
            var listaCaptura = new[]
            {
            new { Id = -1, Nombre = "TODOS" },
            new { Id =  0, Nombre = "RECIBIDO" },
            new { Id =  1, Nombre = "ENVIADO" }
            }.ToList();
                cmbTipoCaptura.DataSource = listaCaptura;
                cmbTipoCaptura.DisplayMember = "Nombre";
                cmbTipoCaptura.ValueMember = "Id";
                cmbTipoCaptura.SelectedIndex = 0;
        }

        private void btnCapturarOficio_Click(object sender, EventArgs e)
        {
            using (FrmCapOficios frmCaptura = new FrmCapOficios(_context, _usuarioActual))
            {
                frmCaptura.ShowDialog();
                frmCaptura.Size = new System.Drawing.Size(1030, 567);

            }
        }

        private void btnPanelAdmin_Click(object sender, EventArgs e)
        {
            using (FrmTickets frmPanel = new FrmTickets(_context))
            {
                frmPanel.ShowDialog();

            }
        }

       private void btnSalir_Click(object sender, EventArgs e)
       {
            Application.Exit();
       }
    
        /*   private void InicializarGrid()    /// OFICIOS CAPTURADOS POR DOCUMENTACION 
                {
                    DataGridOficios.AutoGenerateColumns = true;
                    if (_context != null)
                    {
                        var historialOficios = (from resp in _context.CapturaOficios
                                                join remi in _context.Remitente
                                                on resp.Remitente equals remi.RemitenteId
                                                join desti in _context.Destinatario
                                                on resp.Destinatario equals desti.DestinatarioId


                                                join ticket in _context.Ticket
                                                on resp.CapturaOficioId equals ticket.id_of into remiGroup
                                                from remiLeft in remiGroup.DefaultIfEmpty()

                                                where resp.Activo == 1
                                                // where resp.Oficinas_Id == 3
                                                //  where resp.TicketId == 0 || resp.TicketId == null
                                                orderby resp.CapturaOficioId descending
                                                select new
                                                {
                                                    CapOficio = resp.CapturaOficioId,
                                                    TipoCaptura = resp.TipoCaptura,
                                                    Estado = resp.Estatus,
                                                    NumOficio = resp.NumeroOficio,
                                                    TipoDocumento = resp.TipoDocumento,
                                                    Asunto = resp.Asunto,
                                                    Peticion = resp.Peticion,
                                                    NumeroControl = resp.FolioInicio,
                                                    Remitente = remi.RemitenteNombre,
                                                    Destinatario = desti.DestinatarioNombre,
                                                    RequiereTicket = resp.RequiereTicket,
                                                    OficioRespuesta = resp.OficioRespuestaId

                                                })
                                                // .OrderByDescending(resp => resp.CapOficio)
                                                .ToList();
                        DataGridOficios.DataSource = historialOficios;
                        if (DataGridOficios.Columns.Contains("CapOficioId"))
                        {
                            DataGridOficios.Columns["CapOficioId"].Visible = false;
                        }

                    }
                    ConfigurarGrid();
                    ActualizarIndicadores();
                }
             */
        /*     private void InicializarGrid()
             {
                 DataGridOficios.AutoGenerateColumns = true;
                 if (_context != null)
                 {

                     var consultaBD = (from resp in _context.CapturaOficios
                                       join remi in _context.Remitente
                                       on resp.Remitente equals remi.RemitenteId
                                       join desti in _context.Destinatario
                                       on resp.Destinatario equals desti.DestinatarioId                               
                                       join ticket in _context.Ticket
                                       on resp.CapturaOficioId equals ticket.id_of into ticketGroup
                                       where resp.Activo == 1
                                       orderby resp.CapturaOficioId descending
                                       select new
                                       {
                                           CapOficio = resp.CapturaOficioId,
                                           TipoCaptura = resp.TipoCaptura,
                                           Estado = resp.Estatus,
                                           NumOficio = resp.NumeroOficio,
                                           TipoDocumento = resp.TipoDocumento,
                                           Asunto = resp.Asunto,
                                           Peticion = resp.Peticion,
                                           NumeroControl = resp.FolioInicio,
                                           Remitente = remi.RemitenteNombre,
                                           Destinatario = desti.DestinatarioNombre,
                                           RequiereTicket = resp.RequiereTicket,
                                           OficioRespuesta = resp.OficioRespuestaId,


                                           ListaTickets = ticketGroup.Select(t => t.id_of)
                                       }).ToList();

                     var historialOficios = consultaBD.Select(x => new
                     {
                         x.CapOficio,
                         x.TipoCaptura,
                         x.Estado,
                         x.NumOficio,
                         x.TipoDocumento,
                         x.Asunto,
                         x.Peticion,
                         x.NumeroControl,
                         x.Remitente,
                         x.Destinatario,
                         x.RequiereTicket,
                         x.OficioRespuesta,                   
                         Tickets = x.ListaTickets.Any() ? string.Join(", ", x.ListaTickets) : "Sin Ticket"

                     }).ToList();

                     DataGridOficios.DataSource = historialOficios;

                     if (DataGridOficios.Columns.Contains("CapOficio"))
                     {
                         DataGridOficios.Columns["CapOficio"].Visible = false;
                     }
                 }

                 ConfigurarGrid();
                 ActualizarIndicadores();
             }
     */


        private void InicializarGrid()     /// OFICIOS CAPTURADOS POR DOCUMENTACION 
        {
            DataGridOficios.AutoGenerateColumns = true;
            if (_context != null)
            {
                var consultaBD = (from resp in _context.CapturaOficios
                                  join remi in _context.Remitente
                                  on resp.Remitente equals remi.RemitenteId
                                  join desti in _context.Destinatario
                                  on resp.Destinatario equals desti.DestinatarioId
                                  join ticket in _context.Ticket
                                  on resp.CapturaOficioId equals ticket.id_of into ticketGroup
                                  where resp.Activo == 1 
                                  orderby resp.CapturaOficioId descending
                                  select new
                                  {
                                      CapOficio = resp.CapturaOficioId,
                                      TipoCaptura = resp.TipoCaptura,
                                      Estado = resp.Estatus,
                                      NumOficio = resp.NumeroOficio,
                                      TipoDocumento = resp.TipoDocumento,
                                      Asunto = resp.Asunto,
                                      Peticion = resp.Peticion,
                                      NumeroControl = resp.FolioInicio, 
                                      Remitente = remi.RemitenteNombre,
                                      Destinatario = desti.DestinatarioNombre,
                                      RequiereTicket = resp.RequiereTicket,
                                    //OficioRespuesta = resp.OficioRespuestaId,
                                      ListaTickets = ticketGroup.Select(t => t.TicketId)
                                  }).ToList();

                var historialOficios = consultaBD.Select(x =>
                {
                    bool requiere = x.RequiereTicket != null && x.RequiereTicket.ToString().Trim().ToUpper() == "SI";    /// SE MUESTRAN LOS TICKETS DEL OFICIO       
                    int totalPendientes = consultaBD.Count(x =>
                         x.RequiereTicket != null &&
                         x.RequiereTicket.ToString().Trim().ToUpper() == "SI" &&
                         !x.ListaTickets.Any()
                     );

                    lblTicketFaltante.Text = $"Generar Tickets: {totalPendientes}";
                    string textoTickets = "";
                    int ticketspendientes = 0;
                    if (requiere)
                    {
                        if (x.ListaTickets.Any())
                        {
                            textoTickets = string.Join(", ", x.ListaTickets);
                        }
                        else
                        {
                            textoTickets = "Pendiente";
                            ticketspendientes = x.ListaTickets.Count();
                        }
                    }
                    else
                    {
                        textoTickets = "No requiere"; // SIN TICKET
                    }
                    return new
                    {
                        x.CapOficio,
                        x.TipoCaptura,
                        x.Estado,
                        x.NumOficio,
                        x.TipoDocumento,
                        x.Asunto,
                        x.Peticion,
                        x.NumeroControl,
                        x.Remitente,
                        x.Destinatario,
                        x.RequiereTicket,
                     //   x.OficioRespuesta,
                        Tickets = textoTickets
                    };
                }).ToList();

                DataGridOficios.DataSource = historialOficios;
                if (DataGridOficios.Columns.Contains("CapOficio"))
                {
                    DataGridOficios.Columns["CapOficio"].Visible = false;
                }
            }

            ConfigurarGrid();
            ActualizarIndicadores();
        }

        private void DataGridOficios_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex >= 0 && DataGridOficios.Columns[e.ColumnIndex].Name == "Tickets")
            {
                if (e.Value != null && e.Value.ToString() == "Pendiente")
                {
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(DataGridOficios.Font, FontStyle.Bold);
                    //    e.CellStyle.BackColor = Color.Orange;
                    //    e.CellStyle.ForeColor = Color.White; 
                }
            }
        }

        private void ConfigurarGrid()
        {
            DataGridOficios.SuspendLayout();
            DataGridOficios.DefaultCellStyle.Font = new Font("Gotham", 9, FontStyle.Regular);
            DataGridOficios.ColumnHeadersDefaultCellStyle.Font = new Font("Gotham", 11, FontStyle.Bold);
            DataGridOficios.EnableHeadersVisualStyles = false;
            DataGridOficios.ColumnHeadersDefaultCellStyle.BackColor = Color.MidnightBlue; // Color de fondo
            DataGridOficios.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;        // Color de letra
            DataGridOficios.ColumnHeadersDefaultCellStyle.Font = new Font(DataGridOficios.Font, FontStyle.Bold);
            foreach (DataGridViewColumn col in DataGridOficios.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.Automatic;
            }
            SetColumnHeader("TipoCaptura", "Tipo Captura");
            SetColumnHeader("Estado", "Estatus", Color.DarkOrange, Color.White);
            SetColumnHeader("NumOficio", "Numero Oficio");
            SetColumnHeader("TipoDocumento", "Tipo Documento");
            SetColumnHeader("Asunto", "Asunto");
            SetColumnHeader("Peticion", "Petición");
            SetColumnHeader("NumeroControl", "Numero de Control");
            SetColumnHeader("Remitente", "Remitente");
            SetColumnHeader("Destinatario", "Destinatario");
            SetColumnHeader("OficioRespuestaId", "Oficio Respuesta");
            SetColumnHeader("RequiereTicket", "Ticket");

            if (DataGridOficios.Columns.Count > 0)
            {
                var col0 = DataGridOficios.Columns[0];
                col0.ReadOnly = true;
                col0.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            DataGridOficios.ResumeLayout();
            ResaltarOficios();
        }


        private void SetColumnHeader(string columnName, string headerText, Color? backColor = null, Color? foreColor = null)
        {
            if (DataGridOficios.Columns.Contains(columnName))
            {
                DataGridViewColumn col = DataGridOficios.Columns[columnName];
                col.HeaderText = headerText;
                if (backColor.HasValue)
                {
                    col.HeaderCell.Style.BackColor = backColor.Value;
                }

                if (foreColor.HasValue)
                {
                    col.HeaderCell.Style.ForeColor = foreColor.Value;
                }
            }
        }

      /*  private void SetColumnHeader(string columnName, string headerText)
        {
            if (DataGridOficios.Columns.Contains(columnName))
            {
                DataGridOficios.Columns[columnName].HeaderText = headerText;
            }
        }
      */

        private void ResaltarOficios()
        {

            Font boldFont = new Font(DataGridOficios.Font, FontStyle.Bold);

            foreach (DataGridViewRow row in DataGridOficios.Rows)
            {
                if (row.IsNewRow) continue;
                string oficio = row.Cells["NumOficio"].Value?.ToString();
                string estado = row.Cells["Estado"].Value?.ToString();
                string Ticket = row.Cells["RequiereTicket"].Value?.ToString();

                row.DefaultCellStyle.BackColor = Color.Honeydew;
                if (!string.IsNullOrWhiteSpace(oficio))
                {
                    row.Cells["NumOficio"].Style.ForeColor = Color.DarkGreen;
                    row.Cells["NumOficio"].Style.Font = boldFont;
                }
                if (!string.IsNullOrWhiteSpace(estado))
                {
                    switch (estado.ToUpper())
                    {
                        case "ABIERTO":
                            row.DefaultCellStyle.BackColor = Color.WhiteSmoke;
                            row.Cells["NumOficio"].Style.ForeColor = Color.CornflowerBlue;
                            break;

                        case "CERRADO":
                            row.Cells["NumOficio"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["Estado"].Style.ForeColor = Color.CornflowerBlue;
                            row.Cells["Estado"].Style.Font = boldFont;
                            break;
                    }
                }
            }
        }

        private void DataGridOficios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {

                if (e.RowIndex >= 0)
                {
                    var valorCelda = DataGridOficios.Rows[e.RowIndex].Cells["CapOficio"].Value;
                    if (valorCelda != null)
                    {
                        int idOficio = Convert.ToInt32(valorCelda);
                        using (FrmCapOficios frmDetalle = new FrmCapOficios(_context, _usuarioActual, idOficio))
                        {
                            frmDetalle.ShowDialog();
                        }

                        InicializarGrid();
                    }
                    else
                    {
                        MessageBox.Show("La celda CapOficio está vacía.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (ArgumentException)
            {
                MessageBox.Show("No se encontró una columna llamada 'CapOficio' en el Grid.", "Error de Columna", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al intentar abrir el detalle:\n\n{ex.Message}\n\nRevisa el ID.",
                                "Error General",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }
        }

        private void btnbuscar_Click(object sender, EventArgs e)
        {
            string texto = txtBuscar.Text.Trim();
            string estadoSeleccionado = cmbEstado.Text.Trim();
            string tipoCaptura = cmbTipoCaptura.Text.Trim();

            if (_context != null)
            {
                var consultaBD = (from resp in _context.CapturaOficios
                                  join remi in _context.Remitente
                                  on resp.Remitente equals remi.RemitenteId
                                  join desti in _context.Destinatario
                                  on resp.Destinatario equals desti.DestinatarioId
                                  join ticket in _context.Ticket
                                  on resp.CapturaOficioId equals ticket.id_of into ticketGroup
                                  where resp.Activo == 1 &&
                                        (string.IsNullOrEmpty(texto) ||
                                         resp.NumeroOficio.Contains(texto) ||
                                         resp.FolioInicio.Contains(texto) ||
                                         resp.FolioInicio.Contains(texto) ||
                                         resp.Asunto.Contains(texto) ||
                                         resp.Peticion.Contains(texto)) &&

                                        (string.IsNullOrEmpty(estadoSeleccionado) ||
                                         estadoSeleccionado == "TODOS" ||
                                         resp.Estatus == estadoSeleccionado) &&

                                         (string.IsNullOrEmpty(tipoCaptura) ||
                                         tipoCaptura == "TODOS" ||
                                         resp.TipoCaptura == tipoCaptura)
                                  orderby resp.CapturaOficioId descending
                                  select new
                                  {
                                      CapOficio = resp.CapturaOficioId,
                                      TipoCaptura = resp.TipoCaptura,
                                      Estado = resp.Estatus,
                                      NumOficio = resp.NumeroOficio,
                                      TipoDocumento = resp.TipoDocumento,
                                      Asunto = resp.Asunto,
                                      Peticion = resp.Peticion,
                                      NumeroControl = resp.FolioInicio,
                                      Remitente = remi.RemitenteNombre,
                                      Destinatario = desti.DestinatarioNombre,
                                      RequiereTicket = resp.RequiereTicket,
                                      OficioRespuesta = resp.OficioRespuestaId,
                                      ListaTickets = ticketGroup.Select(t => t.TicketId)
                                  }).ToList();


                var historialOficios = consultaBD.Select(x =>
                {
                    bool requiere = x.RequiereTicket != null && x.RequiereTicket.ToString().Trim().ToUpper() == "SI";
                    string textoTickets = "";
                    if (requiere)
                    {
                        textoTickets = x.ListaTickets.Any()
                                       ? string.Join(", ", x.ListaTickets)
                                       : "Pendiente";
                    }
                    else
                    {
                        textoTickets = "No requiere";
                    }
                    return new
                    {
                        x.CapOficio,
                        x.TipoCaptura,
                        x.Estado,
                        x.NumOficio,
                        x.TipoDocumento,
                        x.Asunto,
                        x.Peticion,
                        x.NumeroControl,
                        x.Remitente,
                        x.Destinatario,
                        x.RequiereTicket,
                        x.OficioRespuesta,
                        Tickets = textoTickets
                    };
                }).ToList();
                DataGridOficios.DataSource = historialOficios;

                if (DataGridOficios.Columns.Contains("CapOficio"))
                {
                    DataGridOficios.Columns["CapOficio"].Visible = false;
                }

                ConfigurarGrid();
            }
        }

        private void btnCrearTicket_Click(object sender, EventArgs e)
        {
            using (FrmCrearTicket frmTicket = new FrmCrearTicket(_context, _usuarioActual))
            {
                frmTicket.ShowDialog();
                //  frmTicket.IdOficioVinculado = idOficioGuardado;
                //  frmTicket.AsuntoPredefinido = TextAsunto.Text.Trim();
                //  frmTicket.Instruccion = TextPeticion.Text.Trim();

            }
        }

        /* 
        private void btnbuscar_Click(object sender, EventArgs e)
        {
                string texto = txtBuscar.Text.Trim();
                string estado = cmbEstado.Text;                
                if (_context != null)
                {               
                    var consultaBD = (from resp in _context.CapturaOficios
                                      join remi in _context.Remitente
                                      on resp.Remitente equals remi.RemitenteId
                                      join desti in _context.Destinatario
                                      on resp.Destinatario equals desti.DestinatarioId                                
                                      join ticket in _context.Ticket
                                      on resp.CapturaOficioId equals ticket.id_of into ticketGroup                            
                                      where resp.Activo == 1 &&
                                           (resp.NumeroOficio.Contains(texto) ||
                                            resp.Asunto.Contains(texto) ||
                                            resp.Peticion.Contains(texto) ||
                                            resp.Estatus.Contains(estado))                                    
                                      orderby resp.CapturaOficioId descending
                                      select new
                                      {
                                          CapOficio = resp.CapturaOficioId,
                                          TipoCaptura = resp.TipoCaptura,
                                          Estado = resp.Estatus,
                                          NumOficio = resp.NumeroOficio,
                                          TipoDocumento = resp.TipoDocumento,
                                          Asunto = resp.Asunto,
                                          Peticion = resp.Peticion,
                                          NumeroControl = resp.FolioInicio,
                                          Remitente = remi.RemitenteNombre,
                                          Destinatario = desti.DestinatarioNombre,
                                          RequiereTicket = resp.RequiereTicket,
                                          OficioRespuesta = resp.OficioRespuestaId,

                                          ListaTickets = ticketGroup.Select(t => t.TicketId)
                                      }).ToList();


                    var historialOficios = consultaBD.Select(x =>
                    {
                        bool requiere = x.RequiereTicket != null && x.RequiereTicket.ToString().Trim().ToUpper() == "SI";
                        string textoTickets = "";
                        if (requiere)
                        {
                            textoTickets = x.ListaTickets.Any()
                                           ? string.Join(", ", x.ListaTickets)
                                           : "Pendiente";
                        }
                        else
                        {
                            textoTickets = "No requiere";
                        }
                        return new
                        {
                            x.CapOficio,
                            x.TipoCaptura,
                            x.Estado,
                            x.NumOficio,
                            x.TipoDocumento,
                            x.Asunto,
                            x.Peticion,
                            x.NumeroControl,
                            x.Remitente,
                            x.Destinatario,
                            x.RequiereTicket,
                            x.OficioRespuesta,
                            Tickets = textoTickets
                        };
                    }).ToList();              
                    DataGridOficios.DataSource = historialOficios;
                    if (DataGridOficios.Columns.Contains("CapOficio"))
                    {
                        DataGridOficios.Columns["CapOficio"].Visible = false;
                    }
                    ConfigurarGrid();
                }
      }
     */
    }




}
 
