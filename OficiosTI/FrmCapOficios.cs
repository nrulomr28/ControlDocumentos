using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Services;
using System;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmCapOficios : Form
    {
        private OficiosContext _context;
      
        private OficioRespuesta _oficioActual;    /// OFICIO ORIGEN 
        private OficioRespuestaService _service;
        private UsuarioContexto _usuarioActual;
        private readonly Ticket _ticket;        ///  TICKETS
        private int? _idOficioActual;  /// edicion o captura 
        public FrmCapOficios( OficiosContext context, UsuarioContexto usuarioActual)
        {
            InitializeComponent();              
            _context = context;
            _usuarioActual = usuarioActual;
            _service = new OficioRespuestaService(_context);
            this.Text = $"Captura de Oficios - {_usuarioActual.NombreMostrar} ({_usuarioActual.UnidadOrganizativa})";
           // CATALOGOS 
            CargarEjercicio();
            CargarReqTicket();
            CargarPrioridad();
            ConfigurarCalendario();
            CargarTipoCaptura();
            CargarDocumento();
            CargarRemitentes();
            CargarDestinatarios();
            CargarSeRecibe();
            CargarArchivo();
            CargarDepedencias();
            CargoDestinatario();
            CargarEstadosO();
            DateRecepcion.Checked = false;
            DateDocumento.Checked = false;
            btnQuitarAdjunto.Visible = false;
            btnVerArchivo.Visible = false;
            txtSeguimiento.Visible= false;
            lblhistorial.Visible = false;
            dataGridTickets.Visible = false;
            cmbEstatus.Visible = false;
            lblTickets.Visible = false;
            lblEstado.Visible = false;
            this.Font = new Font("Gotham", 10F, FontStyle.Regular, GraphicsUnit.Point);          
        }

       public FrmCapOficios(OficiosContext context, UsuarioContexto usuarioActual, int idOficio, OficioRespuesta OficioResp = null)
                 : this(context, usuarioActual)
       {
            _oficioActual = OficioResp;
            _idOficioActual = idOficio;
            CargarDatosDelOficio(idOficio);
            CargarTicketsR(idOficio);
       }
   
  
        private void CargarTicketsR(int idOficio) ////// TICKETS RELACIONADOS DEL OFICIO 
        {
            try
            {
               
                if (idOficio <= 0)
                {
                    dataGridTickets.DataSource = null;
                    return;
                }
                var ticketsRelacionados = (from c in _context.CapturaOficios
                                           join t in _context.Ticket on c.CapturaOficioId equals t.id_of
                                           join o in _context.Oficinas on t.OficinasId equals o.OficinasId
                                           join s in _context.Cat_TicketStatus on t.Cat_TicketStatusId equals s.Cat_TicketStatusId
                                           where t.id_of == idOficio
                                           orderby t.TicketId
                                           select new
                                           {
                                               Ticket = t.TicketId,
                                               Remitente = t.TicketPersona,
                                               Asunto = t.TicketMensaje,
                                               Oficina = o.OficinasNombre,
                                               Estado = s.Cat_TicketStatusStatus,
                                           }).ToList();
                dataGridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridTickets.DataSource = ticketsRelacionados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los tickets relacionados: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // CARGAR DATOS DEL OFICIO POR ID 

        private void CargarDatosDelOficio(int id)
        {
            txtSeguimiento.Visible = true;
            lblhistorial.Visible = true;
            dataGridTickets.Visible = true;
            lblTickets.Visible = true;
            cmbEstatus.Visible = true;
            lblEstado.Visible = true;

            var oficio = _context.CapturaOficios.Find(id);

            if (oficio != null)
            {                
                AjustarTamanioPorTipo(oficio.TipoCaptura);
                CargarTicketsR(id);
                var seguimientoDatos = _context.TSeguimiento
                 .Where(s => s.OficioId == id)
                 .Select(s => new
                 {
                     s.TSeguimientoFecha,
                     s.TSeguimientoNombreUsuario,
                     s.TSeguimientoSeguimiento
                 })
                .ToList();

                var seguimientoTodo = seguimientoDatos.Select(s =>
                $"[{s.TSeguimientoFecha:dd/MM/yyyy}] {s.TSeguimientoNombreUsuario}: {s.TSeguimientoSeguimiento}");

                CmbTipo.Text = oficio.TipoCaptura;
                CmbDocumento.Text = oficio.TipoDocumento;
                CmbPrioridad.Text = oficio.Prioridad;
                CmbTicket.Text = oficio.RequiereTicket;
                CmbClasif.Text = oficio.Clasificacion;
                TxtNumOficio.Text = oficio.NumeroOficio;
                CmbSeRecibe.Text = oficio.TipoSeRecibe;
                TextAsunto.Text = oficio.Asunto;
                TextPeticion.Text = oficio.Peticion;
                TextUbicacion.Text = oficio.UbicacionFisica;
                TextFolioIni.Text = oficio.FolioInicio?.ToString();
                TextFolioFin.Text = oficio.FolioFin?.ToString();
                cmbEstatus.Text = oficio.Estatus;
                txtSeguimiento.Text = string.Join(Environment.NewLine, seguimientoTodo); /// TODO EL SEGUIMIENTO DEL OFICIO 
                if (oficio.FechaRecepcion.HasValue)
                {
                    DateRecepcion.Value = oficio.FechaRecepcion.Value;
                    DateRecepcion.Checked = true;
                }
                else
                {
                    DateRecepcion.Checked = false;
                }

                if (oficio.FechaDocumento.HasValue)
                {
                    DateDocumento.Value = oficio.FechaDocumento.Value;
                    DateDocumento.Checked = true;
                }
                else
                {
                    DateDocumento.Checked = false;
                }

                if (oficio.Remitente != null)
                {
                    CmbRemitente.SelectedValue = oficio.Remitente;
                }
                if (oficio.CargoRemitente != null)
                {
                    cmbCargoRemitente.SelectedValue = oficio.CargoRemitente;
                }
                if (oficio.Destinatario != null)
                {
                    CmbDestinatario.SelectedValue = oficio.Destinatario;
                }
                if (oficio.CargoDestinatario != null)
                {
                    cmbCargoDestinatario.SelectedValue = oficio.CargoDestinatario;
                }

                var archivoAdjunto = _context.Archivo.FirstOrDefault(a => a.OficioId == id);
                if (archivoAdjunto != null && !string.IsNullOrWhiteSpace(archivoAdjunto.ArchivoNombre))
                {
                    _rutaArchivoTemporal = archivoAdjunto.ArchivoNombre;
                    _nombreArchivo = archivoAdjunto.ArchivoNombre;
                    btnAdjuntar.Text = "📎 " + _nombreArchivo;
                    btnQuitarAdjunto.Visible = true;
                    btnVerArchivo.Visible = true;
                }
                else
                {
                    _rutaArchivoTemporal = string.Empty;
                    _nombreArchivo = string.Empty;
                    btnAdjuntar.Text = "Adjuntar Oficio";
                    btnQuitarAdjunto.Visible = false;
                    btnVerArchivo.Visible = false;
                }
                this.Text = $"Detalle de Oficio - Número: {oficio.NumeroOficio}";
            }
        }

       
        private void AjustarTamanioPorTipo(string tipoCaptura)
        {
            switch (tipoCaptura)
            {
                case "Oficio":
                    this.Size = new System.Drawing.Size(1030, 842);
                    break;
              
                default:
                    this.Size = new System.Drawing.Size(1030, 842);
                    break;
            }
        }

        /*     private void CargarDatosDelOficio(int id)
             {
                 txtSeguimiento.Visible = true;
                 lblhistorial.Visible = true;
                 dataGridTickets.Visible = true;
                 lblTickets.Visible = true;


                 var oficio = _context.CapturaOficios.Find(id);             

                 if (oficio != null)
                 {
                     CargarTicketsR(id); 

                     var seguimientoDatos = _context.TSeguimiento
                      .Where(s => s.OficioId == id)
                      .Select(s => new
                       {
                         s.TSeguimientoFecha,
                         s.TSeguimientoNombreUsuario,
                         s.TSeguimientoSeguimiento
                     })
                     .ToList();
                     var seguimientoTodo = seguimientoDatos.Select(s =>
                     $"[{s.TSeguimientoFecha:dd/MM/yyyy}] {s.TSeguimientoNombreUsuario}: {s.TSeguimientoSeguimiento}");
                     CmbTipo.Text =        oficio.TipoCaptura;
                     CmbDocumento.Text =   oficio.TipoDocumento;
                     CmbPrioridad.Text =   oficio.Prioridad;
                     CmbTicket.Text =      oficio.RequiereTicket;
                     CmbClasif.Text =      oficio.Clasificacion;
                     TxtNumOficio.Text =   oficio.NumeroOficio;
                     TextAsunto.Text =     oficio.Asunto;
                     TextPeticion.Text =   oficio.Peticion;
                     TextUbicacion.Text =  oficio.UbicacionFisica;
                     TextFolioIni.Text =   oficio.FolioInicio?.ToString();
                     TextFolioFin.Text =   oficio.FolioFin?.ToString();
                     cmbEstatus.Text =     oficio.Estatus;
                     txtSeguimiento.Text = string.Join(Environment.NewLine, seguimientoTodo); /// TODO EL SEGUIMIENTO DEL OFICIO 
                     if (oficio.FechaRecepcion.HasValue)
                     {
                         DateRecepcion.Value = oficio.FechaRecepcion.Value;
                         DateRecepcion.Checked = true;
                     }
                     else
                     {
                         DateRecepcion.Checked = false;
                     }

                     if (oficio.FechaDocumento.HasValue)
                     {
                         DateDocumento.Value = oficio.FechaDocumento.Value;
                         DateDocumento.Checked = true;
                     }
                     else
                     {
                         DateDocumento.Checked = false;
                     }

                     if (oficio.Remitente != null)
                     {
                         CmbRemitente.SelectedValue = oficio.Remitente;
                     }
                     if (oficio.CargoRemitente != null)
                     {
                         cmbCargoRemitente.SelectedValue = oficio.CargoRemitente;
                     }
                     if (oficio.Destinatario != null)
                     {
                         CmbDestinatario.SelectedValue = oficio.Destinatario;
                     }
                     if (oficio.CargoDestinatario != null)
                     {
                         cmbCargoDestinatario.SelectedValue = oficio.CargoDestinatario;
                     }                     
                     var archivoAdjunto = _context.Archivo.FirstOrDefault(a => a.OficioId == id);
                     if (archivoAdjunto != null && !string.IsNullOrWhiteSpace(archivoAdjunto.ArchivoNombre))
                     {                 
                         _rutaArchivoTemporal = archivoAdjunto.ArchivoNombre;
                         _nombreArchivo = archivoAdjunto.ArchivoNombre;                 
                         btnAdjuntar.Text = "📎 " + _nombreArchivo;
                         btnQuitarAdjunto.Visible = true;                 
                         btnVerArchivo.Visible = true;
                     }
                     else
                     {                   
                         _rutaArchivoTemporal = string.Empty;
                         _nombreArchivo = string.Empty;
                         btnAdjuntar.Text = "Adjuntar Oficio";
                         btnQuitarAdjunto.Visible = false;
                         btnVerArchivo.Visible = false;
                     }
                     this.Text = $"Detalle de Oficio - Número: {oficio.NumeroOficio}";
                 }
             }
             // CARGAR DATOS DEL OFICIO POR ID 
        */
        /// BOTON PARA VER EL ARCHIVO CARGADO 
        private void btnVerArchivo_Click(object sender, EventArgs e)
        {
            if (_idOficioActual.HasValue)
            {
                var archivoAdjunto = _context.Archivo.FirstOrDefault(a => a.OficioId == _idOficioActual.Value);
                if (archivoAdjunto != null && archivoAdjunto.ArchivoArchivo != null)
                {                 
                    string rutaTemporal = Path.Combine(Path.GetTempPath(), archivoAdjunto.ArchivoNombre);

                    File.WriteAllBytes(rutaTemporal, archivoAdjunto.ArchivoArchivo);

                    System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo()
                    {
                        FileName = rutaTemporal,
                        UseShellExecute = true
                    });
                }
            }
        }

        // --- catálogo tipos de documentos que se reciben ---
        private void CargarSeRecibe()
        {

            var listaArchivo = new[]
            {
                new { Id = 1, Nombre = "ORIGINAL" },
                new { Id = 2, Nombre = "COPIAS"   },
                new { Id = 3, Nombre = "SOBRE"    }

            }.ToList();
            CmbSeRecibe.DataSource = listaArchivo;
            CmbSeRecibe.DisplayMember = "Nombre";
            CmbSeRecibe.ValueMember = "Id";
            CmbSeRecibe.DropDownStyle = ComboBoxStyle.DropDown;
            CmbSeRecibe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbSeRecibe.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbSeRecibe.SelectedIndex = -1;
        }


        ///--- COMBO CLASIFICACION DEL ARCHIVO ---
        private List<CatClasificacion> _Archivistas;
        private void CargarArchivo()
        {
            _Archivistas = _context.CatClasificacion.ToList();
            CmbClasif.DataSource = _Archivistas;
            CmbClasif.DisplayMember = "Codigo";
            CmbClasif.ValueMember = "ClasificacionId";
            CmbClasif.DropDownStyle = ComboBoxStyle.DropDown;
            CmbClasif.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbClasif.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbClasif.SelectedIndex = -1;
        }

   
        ///  -- COMBO ESTATUS DEL OFICIO 
        private List<Cat_Status> _estatus;
        private void CargarEstadosO()
        {
            _estatus = _context.Cat_Status.ToList();
            cmbEstatus.DataSource = _estatus;
            cmbEstatus.DisplayMember = "Cat_StatusStatus";
            cmbEstatus.ValueMember = "Cat_StatusId";
            cmbEstatus.DropDownStyle = ComboBoxStyle.DropDown;
            cmbEstatus.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbEstatus.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbEstatus.SelectedIndex = -1;
        }


        /// --- COMBO REMITENTES ----
        private List<Remitente> _todosLosRemitentes;
        private void CargarRemitentes()
        {

            _todosLosRemitentes = _context.Remitente.ToList();
            CmbRemitente.DataSource = _todosLosRemitentes;
            CmbRemitente.DisplayMember = "RemitenteNombre";
            CmbRemitente.ValueMember = "RemitenteId";
            CmbRemitente.SelectedIndex = -1;
            CmbRemitente.DropDownStyle = ComboBoxStyle.DropDown;
            CmbRemitente.AutoCompleteMode = AutoCompleteMode.None;
            CmbRemitente.TextUpdate -= CmbRemitente_TextUpdate;
            CmbRemitente.TextUpdate += CmbRemitente_TextUpdate;
            CmbRemitente.KeyPress += CmbRemitente_KeyPress;

            CmbRemitente.Validating -= CmbRemitente_Validating;
            CmbRemitente.Validating += CmbRemitente_Validating;
        }

        private void CmbRemitente_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string textoIngresado = CmbRemitente.Text.Trim();

            if (string.IsNullOrWhiteSpace(textoIngresado))
            {
                return;
            }

            bool existe = _todosLosRemitentes.Any(r =>
                r.RemitenteNombre.Equals(textoIngresado, StringComparison.OrdinalIgnoreCase));

            if (!existe)
            {
                MessageBox.Show("El remitente ingresado no existe. Por favor, seleccione uno válido de la lista.",
                                "Remitente no válido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                CmbRemitente.Text = string.Empty;
                CmbRemitente.SelectedIndex = -1;


            }
        }
        private void CmbRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

       
        private void CmbRemitente_TextUpdate(object sender, EventArgs e)
        {
            CmbRemitente.TextUpdate -= CmbRemitente_TextUpdate;

            string textoBusqueda = CmbRemitente.Text;
            int cursorPosition = CmbRemitente.SelectionStart;

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CmbRemitente.DataSource = _todosLosRemitentes.ToList();
                CmbRemitente.DroppedDown = false;
            }
            else
            {
                var filtrado = _todosLosRemitentes
                   .Where(r => r.RemitenteNombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                   .ToList();

                if (filtrado.Count > 0)
                {
                    CmbRemitente.DataSource = filtrado;
                    CmbRemitente.DroppedDown = true;
                }
                else
                {
                    CmbRemitente.DroppedDown = false;
                }
            }

            CmbRemitente.Text = textoBusqueda;
            CmbRemitente.SelectionStart = cursorPosition;
            CmbRemitente.SelectionLength = 0;
            CmbRemitente.TextUpdate += CmbRemitente_TextUpdate;
        }
        /// --- COMBO REMITENTES ----


        /// ---- COMBO DEPDENDENCIAS ----

        private List<Dependencia> _listaDependencias;
        private void CargarDepedencias()
        {

            _listaDependencias = _context.Dependencia.ToList();
            cmbCargoRemitente.DataSource = _listaDependencias;
            cmbCargoRemitente.DisplayMember = "DependenciaNombre";
            cmbCargoRemitente.ValueMember = "DependenciaId";
            cmbCargoRemitente.SelectedIndex = -1;

            cmbCargoRemitente.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCargoRemitente.AutoCompleteMode = AutoCompleteMode.None;
            cmbCargoRemitente.TextUpdate -= cmbCargoRemitente_TextUpdate;
            cmbCargoRemitente.TextUpdate += cmbCargoRemitente_TextUpdate;
            cmbCargoRemitente.KeyPress += cmbCargoRemitente_KeyPress;
            cmbCargoRemitente.Validating -= cmbCargoRemitente_Validating;
            cmbCargoRemitente.Validating += cmbCargoRemitente_Validating;
        }
        private void cmbCargoRemitente_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string textDependencia = cmbCargoRemitente.Text.Trim();

            if (string.IsNullOrWhiteSpace(textDependencia))
            {
                return;
            }

            bool existe = _listaDependencias.Any(r =>
                r.DependenciaNombre.Equals(textDependencia, StringComparison.OrdinalIgnoreCase));

            if (!existe)
            {
                MessageBox.Show("La dependencia ingresada no existe. Por favor, seleccione una válida de la lista.",
                                "Dependencia no válida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                // e.Cancel = true; 


                this.BeginInvoke(new Action(() =>
                {
                    cmbCargoRemitente.DataSource = _listaDependencias;
                    cmbCargoRemitente.SelectedIndex = -1;
                    cmbCargoRemitente.Text = string.Empty;
                }));
            }
        }

        private void cmbCargoRemitente_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void cmbCargoRemitente_TextUpdate(object sender, EventArgs e)
        {
            cmbCargoRemitente.TextUpdate -= cmbCargoRemitente_TextUpdate;

            string textoBusquedaD = cmbCargoRemitente.Text;
            int cursorPositionD = cmbCargoRemitente.SelectionStart;

            if (string.IsNullOrWhiteSpace(textoBusquedaD))
            {
                cmbCargoRemitente.DataSource = _listaDependencias;
                cmbCargoRemitente.DroppedDown = false;
            }
            else
            {
                var filtradoD = _listaDependencias
                   .Where(r => r.DependenciaNombre.IndexOf(textoBusquedaD, StringComparison.OrdinalIgnoreCase) >= 0)
                   .ToList();


                if (filtradoD.Count > 0)
                {
                    cmbCargoRemitente.DataSource = filtradoD;
                    cmbCargoRemitente.DroppedDown = true;
                }
                else
                {

                    cmbCargoRemitente.DroppedDown = false;
                }
            }

            cmbCargoRemitente.Text = textoBusquedaD;
            cmbCargoRemitente.SelectionStart = cursorPositionD;
            cmbCargoRemitente.SelectionLength = 0;
            cmbCargoRemitente.TextUpdate += cmbCargoRemitente_TextUpdate;
        }

        ///  COMBO DEPENDENCIAS 


        // COMBO DESTINATARIOS 
        private List<Destinatario> _todosLosDestinatarios;

        private void CargarDestinatarios()
        {

            _todosLosDestinatarios = _context.Destinatario.ToList();

            CmbDestinatario.DataSource = _todosLosDestinatarios;
            CmbDestinatario.DisplayMember = "DestinatarioNombre";
            CmbDestinatario.ValueMember = "DestinatarioId";
            CmbDestinatario.SelectedIndex = -1;

            CmbDestinatario.DropDownStyle = ComboBoxStyle.DropDown;
            CmbDestinatario.AutoCompleteMode = AutoCompleteMode.None;

            CmbDestinatario.KeyPress += CmbDestinatario_KeyPress;
            CmbDestinatario.TextUpdate -= CmbDestinatario_TextUpdate;
            CmbDestinatario.TextUpdate += CmbDestinatario_TextUpdate;

            CmbDestinatario.Validating -= CmbDestinatario_Validating;
            CmbDestinatario.Validating += CmbDestinatario_Validating;
        }
        private void CmbDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }
        private void CmbDestinatario_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string textDependencia = CmbDestinatario.Text.Trim();

            if (string.IsNullOrWhiteSpace(textDependencia))
            {
                return;
            }

            bool existe = _todosLosDestinatarios.Any(r =>
                r.DestinatarioNombre.Equals(textDependencia, StringComparison.OrdinalIgnoreCase));

            if (!existe)
            {
                MessageBox.Show("El destinatario ingresado no existe. Por favor, seleccione uno válido de la lista.",
                                "Destinatario no válido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);


                // e.Cancel = true; 


                this.BeginInvoke(new Action(() =>
                {
                    CmbDestinatario.DataSource = _todosLosDestinatarios;
                    CmbDestinatario.SelectedIndex = -1;
                    CmbDestinatario.Text = string.Empty;
                }));
            }
        }
        private void CmbDestinatario_TextUpdate(object sender, EventArgs e)
        {
            CmbDestinatario.TextUpdate -= CmbDestinatario_TextUpdate;

            string textoBusqueda = CmbDestinatario.Text;
            int cursorPosition = CmbDestinatario.SelectionStart;

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                CmbDestinatario.DataSource = _todosLosDestinatarios;
                CmbDestinatario.DroppedDown = false;
            }
            else
            {
                var filtrados = _todosLosDestinatarios
                    .Where(d => d.DestinatarioNombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                if (filtrados.Count > 0)
                {
                    CmbDestinatario.DataSource = filtrados;
                    CmbDestinatario.DroppedDown = true;
                }
                else
                {
                    CmbDestinatario.DroppedDown = false;
                }
            }

            CmbDestinatario.Text = textoBusqueda;
            CmbDestinatario.SelectionStart = cursorPosition;
            CmbDestinatario.SelectionLength = 0;

            CmbDestinatario.TextUpdate += CmbDestinatario_TextUpdate;
        }

        // COMBO DESTINATARIOS 




        // COMBO CARGOS DESTINATARIOS 

        private void CargoDestinatario()
        {
            // Usamos .ToList() para que no se vincule con el combo de Remitente
            cmbCargoDestinatario.DataSource = _listaDependencias.ToList();
            cmbCargoDestinatario.DisplayMember = "DependenciaNombre";
            cmbCargoDestinatario.ValueMember = "DependenciaId";
            cmbCargoDestinatario.SelectedIndex = -1;

            cmbCargoDestinatario.DropDownStyle = ComboBoxStyle.DropDown;
            cmbCargoDestinatario.AutoCompleteMode = AutoCompleteMode.None;

            // Suscripción segura
            cmbCargoDestinatario.TextUpdate -= cmbCargoDestinatario_TextUpdate;
            cmbCargoDestinatario.TextUpdate += cmbCargoDestinatario_TextUpdate;
            cmbCargoDestinatario.KeyPress -= cmbCargoDestinatario_KeyPress;
            cmbCargoDestinatario.KeyPress += cmbCargoDestinatario_KeyPress;
            cmbCargoDestinatario.Validating -= cmbCargoDestinatario_Validating;
            cmbCargoDestinatario.Validating += cmbCargoDestinatario_Validating;
        }

        private void cmbCargoDestinatario_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string textDependencia = cmbCargoDestinatario.Text.Trim();

            if (string.IsNullOrWhiteSpace(textDependencia))
            {
                return;
            }

            bool existe = _listaDependencias.Any(r =>
                r.DependenciaNombre.Equals(textDependencia, StringComparison.OrdinalIgnoreCase));

            if (!existe)
            {
                MessageBox.Show("La dependencia ingresada no existe. Por favor, seleccione una válida de la lista.",
                                "Dependencia no válida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                this.BeginInvoke(new Action(() =>
                {
                    // Restauramos con .ToList() por la misma razón del CurrencyManager
                    cmbCargoDestinatario.DataSource = _listaDependencias.ToList();
                    cmbCargoDestinatario.SelectedIndex = -1;
                    cmbCargoDestinatario.Text = string.Empty;
                }));
            }
        }

        private void cmbCargoDestinatario_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void cmbCargoDestinatario_TextUpdate(object sender, EventArgs e)
        {
            cmbCargoDestinatario.TextUpdate -= cmbCargoDestinatario_TextUpdate;

            string textoBusqueda = cmbCargoDestinatario.Text;
            int cursorPosition = cmbCargoDestinatario.SelectionStart;

            if (string.IsNullOrWhiteSpace(textoBusqueda))
            {
                cmbCargoDestinatario.DataSource = _listaDependencias.ToList();
                cmbCargoDestinatario.DroppedDown = false;
            }
            else
            {
                var filtrado = _listaDependencias
                   .Where(r => r.DependenciaNombre.IndexOf(textoBusqueda, StringComparison.OrdinalIgnoreCase) >= 0)
                   .ToList();

                if (filtrado.Count > 0)
                {
                    cmbCargoDestinatario.DataSource = filtrado;
                    cmbCargoDestinatario.DroppedDown = true;
                }
                else
                {
                    cmbCargoDestinatario.DroppedDown = false;
                }
            }

            cmbCargoDestinatario.Text = textoBusqueda;
            cmbCargoDestinatario.SelectionStart = cursorPosition;
            cmbCargoDestinatario.SelectionLength = 0;
            cmbCargoDestinatario.TextUpdate += cmbCargoDestinatario_TextUpdate;
        }

        /// COMBO CARGO DESTINATARIOS 


        /// COMBO DE TIPO DE DOCUMENTOS 

        private void CargarDocumento()
        {
            var Tipodoc = _context.Cat_TipoDocumento
             .ToList();

            CmbDocumento.DataSource = Tipodoc;
            CmbDocumento.DisplayMember = "Cat_TipoDocumentoTipo";
            CmbDocumento.ValueMember = "Cat_TipoDocumentoId";
            CmbDocumento.SelectedIndex = -1;

            CmbDocumento.DropDownStyle = ComboBoxStyle.DropDown;
            CmbDocumento.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbDocumento.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbDocumento.SelectedIndex = -1;
        }

        // COMBO REQUUIERE TICKET 
        private void CargarReqTicket()
        {
            var listaticket = new[]
            {
             new { Id =  "1", Nombre = "SI" },
             new { Id =  "0", Nombre = "NO" },

            }.ToList();

            CmbTicket.DataSource = listaticket;
            CmbTicket.DisplayMember = "Nombre";
            CmbTicket.ValueMember = "Id";
            CmbTicket.SelectedIndex = -1;
            CmbTicket.KeyPress += CmbTicket_KeyPress;
            CmbTicket.DropDownStyle = ComboBoxStyle.DropDown;
            CmbTicket.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbTicket.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbTicket.SelectedIndex = -1;
        }


        private void CmbTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = char.ToUpper(e.KeyChar);
        }

        private void CargarTipoCaptura()
        {
            var listacap = new[]
            {
             new { Id =  "RECIBIDO", Nombre = "RECIBIDO" },
             new { Id =  "ENVIADO", Nombre = "ENVIADO" },


            }.ToList();

            CmbTipo.DataSource = listacap;
            CmbTipo.DisplayMember = "Nombre";
            CmbTipo.ValueMember = "Id";
            CmbTipo.SelectedIndex = -1;
            CmbTipo.DropDownStyle = ComboBoxStyle.DropDown;
            CmbTipo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbTipo.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbTipo.SelectedIndex = -1;
        }



        // COMBO AÑO DE CAPTURA 
        private void CargarEjercicio()
        {
            int añoActual = DateTime.Now.Year;
            var listaejercicio = Enumerable.Range(añoActual - 2, 3)
                                           .OrderByDescending(año => año)
                                           .Select(año => new { Id = año, Nombre = año.ToString() })
                                           .ToList();

            CmbEjercicio.DataSource = listaejercicio;
            CmbEjercicio.DisplayMember = "Nombre";
            CmbEjercicio.ValueMember = "Id";

            if (CmbEjercicio.Items.Count > 0)
            {
                CmbEjercicio.SelectedIndex = 0;
            }
        }


        // COMBO PRIORIDAD DEL DOCUMENTO 
        private void CargarPrioridad()
        {
            var listaPrioridad = new[]
            {
             new { Id =  "1",  Nombre = "NORMAL" },
             new { Id =  "2", Nombre = "URGENTE" },

            }.ToList();

            CmbPrioridad.DataSource = listaPrioridad;
            CmbPrioridad.DisplayMember = "Nombre";
            CmbPrioridad.ValueMember = "Id";
            CmbPrioridad.SelectedIndex = -1;
            CmbPrioridad.DropDownStyle = ComboBoxStyle.DropDown;
            CmbPrioridad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            CmbPrioridad.AutoCompleteSource = AutoCompleteSource.ListItems;
            CmbPrioridad.SelectedIndex = -1;
        }

        private void ConfigurarCalendario()   /////////// VALIDACION DEL CALENDARIO PARA MAXIMO DIA DE HOY 
        {
            DateRecepcion.MaxDate = DateTime.Now;
            int añoActual = DateTime.Now.Year;
            DateRecepcion.MinDate = new DateTime(añoActual, 1, 1);
            DateRecepcion.MaxDate = DateTime.Today;
            DateDocumento.MaxDate = DateTime.Now;
            DateDocumento.MinDate = new DateTime(añoActual, 1, 1);
            DateDocumento.MaxDate = DateTime.Today;
        }
        private void CmbTipo_SelectedIndexChanged(object sender, EventArgs e)    ///  CUANDO EL OFICIO SEA ENVIADO SE DESHABILITA EL COMBO PARA TICKET
        {
            CmbTicket.Enabled = (CmbTipo.Text != "ENVIADO");
        }



      /*  private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_rutaArchivoTemporal))
            {
                MessageBox.Show("Falta adjuntar el oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbTipo.Text))
            {
                MessageBox.Show("Seleccione el tipo de captura", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DateRecepcion.Checked)
            {
                MessageBox.Show("Por favor, marca la casilla y selecciona una fecha de recepción.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DateRecepcion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(CmbRemitente.Text))
            {
                MessageBox.Show("Seleccione el remitente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbRemitente.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(TxtNumOficio.Text))
            {
                MessageBox.Show("Escriba el número de oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtNumOficio.Focus();
                return;
            }
            if (!DateDocumento.Checked)
            {
                MessageBox.Show("Por favor, marca la casilla y selecciona una fecha de documento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DateDocumento.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbPrioridad.Text))
            {
                MessageBox.Show("Debe seleccionar la prioridad del oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (CmbTipo.Text == "RECIBIDO")
            {
                if (string.IsNullOrWhiteSpace(CmbTicket.Text))
                {
                    MessageBox.Show("Requiere Ticket????.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    CmbTicket.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(TextPeticion.Text))
            {
                MessageBox.Show("Escriba la petición del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextAsunto.Text))
            {
                MessageBox.Show("Escriba el asunto del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(CmbClasif.Text))
            {
                MessageBox.Show("Escriba la clasificación del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(TextFolioIni.Text))
            {
                MessageBox.Show("Escriba el Folio del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!TextFolioIni.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("El campo de Folio inicial solo acepta caracteres numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextFolioIni.Focus();
                return;
            }

            int FolioIni;
            if (!int.TryParse(TextFolioIni.Text, out FolioIni))
            {
                MessageBox.Show("Por favor, ingrese un número válido en el folio inicial.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextFolioIni.Focus();
                return;
            }

            if (!string.IsNullOrWhiteSpace(TextFolioFin.Text))
            {
                if (!TextFolioFin.Text.Trim().All(char.IsDigit))
                {
                    MessageBox.Show("El campo de Folio final solo acepta caracteres numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextFolioFin.Focus();
                    return;
                }

                int FolioFin;
                if (!int.TryParse(TextFolioFin.Text, out FolioFin))
                {
                    MessageBox.Show("El folio final debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    TextFolioFin.Focus();
                    return;
                }

                if (FolioIni > FolioFin)
                {
                    MessageBox.Show("El folio inicial no puede ser mayor al final.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextFolioIni.Focus();
                    return;
                }
            }

            if (string.IsNullOrWhiteSpace(TextUbicacion.Text))
            {
                MessageBox.Show("Escriba la ubicación física del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CapOficios nuevoOficio = new CapOficios
            {
                TipoCaptura = CmbTipo.Text,
                Anio = CmbEjercicio.SelectedValue != null ? (int)CmbEjercicio.SelectedValue : (int?)null,
                FechaRecepcion = DateRecepcion.Checked ? DateRecepcion.Value.Date : (DateTime?)null,
                TipoDocumento = CmbDocumento.Text,
                NumOficio = TxtNumOficio.Text.Trim(),
                FechaDocumento = DateDocumento.Checked ? DateDocumento.Value.Date : (DateTime?)null,
                TipoSeRecibe = CmbSeRecibe.Text,
                // Remitente = CmbRemitente.Text.Trim(),
                Remitente = CmbRemitente.SelectedValue != null ? (int)CmbRemitente.SelectedValue : (int?)null,
                CargoRemitente = cmbCargoRemitente.SelectedValue != null ? (int)CmbRemitente.SelectedValue : (int?)null,
                Destinatario = CmbDestinatario.SelectedValue != null ? (int)CmbDestinatario.SelectedValue : (int?)null,
                CargoDestinatario = cmbCargoDestinatario.SelectedValue != null ? (int)CmbDestinatario.SelectedValue : (int?)null,
                // Destinatario = CmbDestinatario.Text.Trim(),
                RequiereTicket = CmbTicket.Text,
                Prioridad = CmbPrioridad.Text,
                Peticion = TextPeticion.Text.Trim(),
                Asunto = TextAsunto.Text.Trim(),
                FolioInicio = string.IsNullOrWhiteSpace(TextFolioIni.Text) ? null : TextFolioIni.Text.Trim(),
                FolioFin = string.IsNullOrWhiteSpace(TextFolioFin.Text) ? null : TextFolioFin.Text.Trim(),
                Clasificacion = CmbClasif.Text.Trim(),
                UsuarioCap = _service.ObtenerDominioYUsuario(),
                UbicacionFisica = TextUbicacion.Text.Trim(),
                Estatus = "ABIERTO",
                FechaCap = DateTime.Now,
                Activo = 1
            };

            try
            {

                _context.CapOficios.Add(nuevoOficio);
                _context.SaveChanges();
                if (!string.IsNullOrEmpty(_rutaArchivoTemporal))
                {
                    try
                    {

                        byte[] bytes = System.IO.File.ReadAllBytes(_rutaArchivoTemporal);
                        var archivo = new Archivo
                        {
                            ArchivoNombre = System.IO.Path.GetFileName(_rutaArchivoTemporal),
                            ArchivoArchivo = bytes,
                            ArchivoFecha = DateTime.Now,
                            OficioId = nuevoOficio.CapOficioId
                        };

                        _context.Archivo.Add(archivo);
                        _context.SaveChanges();
                    }
                    catch (Exception exArchivo)
                    {

                        string errorReal = exArchivo.InnerException?.Message ?? exArchivo.Message;

                        MessageBox.Show("El registro se guardó, pero el archivo fue rechazado por la base de datos.\n\nMotivo: " + errorReal,
                                        "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }


                // GUARDADO DEL SEGUIMIENTO DEL OFICIO 
                var nuevoseguimiento = new TSeguimiento
                {
                    TSeguimientoSeguimiento = "CAPTURA DEL OFICIO",
                    OficioId = nuevoOficio.CapOficioId,
                    TSeguimientoFecha = DateTime.Now,
                    TSeguimientoNombreUsuario = _service.ObtenerDominioYUsuario(),
                    TSeguimientoUsuarioId = 0,
                };

                try
                {
                    _context.TSeguimiento.Add(nuevoseguimiento);
                    _context.SaveChanges();
                }
                catch (Exception exSeg)
                {
                    string errorReal = exSeg.InnerException?.Message ?? exSeg.Message;
                    MessageBox.Show("El oficio y archivo se guardaron, pero hubo un error al registrar el seguimiento.\n\nMotivo: " + errorReal,
                                    "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;

                }
                MessageBox.Show("El oficio y su archivo se guardaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (CmbTicket.Text.Trim().ToUpper() == "SI")
                {
                    FrmCrearTicket frmTicket = new FrmCrearTicket(_context, _usuarioActual);
                    frmTicket.IdOficioVinculado = nuevoOficio.CapOficioId;
                    frmTicket.AsuntoPredefinido = nuevoOficio.Asunto;
                    // frmTicket.RemitentePredefinido = nuevoOficio.Remitente;
                    frmTicket.Instruccion = nuevoOficio.Peticion;
                    frmTicket.ShowDialog();
                }
                Close();
                FrmMenuPrincipal frmMenu = new FrmMenuPrincipal(_context, _usuarioActual);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar: " + ex.InnerException?.Message ?? ex.Message, "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
      */
        
        // BOTON PARA GUARDAR EL OFICIO O ACTUALIZARLO 
        private void btnGuardar_Click(object sender, EventArgs e)
        {           
            if (string.IsNullOrWhiteSpace(_rutaArchivoTemporal))
            { 
                MessageBox.Show("Falta adjuntar el oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbTipo.Text)) 
            { 
                MessageBox.Show("Seleccione el tipo de captura", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (!DateRecepcion.Checked)
            { 
                MessageBox.Show("Por favor, marca la casilla y selecciona una fecha de recepción.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                DateRecepcion.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbRemitente.Text))
            { 
                MessageBox.Show("Seleccione el remitente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbRemitente.Focus(); 
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbCargoRemitente.Text))
            {
                MessageBox.Show("Seleccione el cargo del remitente", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargoRemitente.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbDestinatario.Text))
            {
                MessageBox.Show("Seleccione el destinatario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbDestinatario.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbCargoDestinatario.Text))
            {
                MessageBox.Show("Seleccione el cargo del destinatario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbCargoDestinatario.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(TxtNumOficio.Text))
            {
                MessageBox.Show("Escriba el número de oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                TxtNumOficio.Focus(); 
                return; 
            }
            if (!DateDocumento.Checked)
            {
                MessageBox.Show("Por favor, marca la casilla y selecciona una fecha de documento.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DateDocumento.Focus(); 
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbPrioridad.Text))
            {
                MessageBox.Show("Debe seleccionar la prioridad del oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (CmbTipo.Text == "RECIBIDO" && string.IsNullOrWhiteSpace(CmbTicket.Text)) 
            { 
                MessageBox.Show("Requiere Ticket????.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                CmbTicket.Focus(); 
                return;
            }
            if (string.IsNullOrWhiteSpace(TextPeticion.Text))
            { 
                MessageBox.Show("Escriba la petición del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            if (string.IsNullOrWhiteSpace(TextAsunto.Text)) 
            { 
                MessageBox.Show("Escriba el asunto del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }
            if (string.IsNullOrWhiteSpace(CmbClasif.Text)) 
            { 
                MessageBox.Show("Escriba la clasificación del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(TextFolioIni.Text)) 
            { 
                MessageBox.Show("Escriba el Folio del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }
            if (!TextFolioIni.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("El campo de Folio inicial solo acepta caracteres numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TextFolioIni.Focus();
                return;
            }
            int FolioIni;
            if (!int.TryParse(TextFolioIni.Text, out FolioIni)) 
            { 
                MessageBox.Show("Por favor, ingrese un número válido en el folio inicial.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error);
                TextFolioIni.Focus();
                return;
            }
            int? folioFinNullable = null; 
            if (!string.IsNullOrWhiteSpace(TextFolioFin.Text))
            {
                if (!TextFolioFin.Text.Trim().All(char.IsDigit)) 
                { 
                    MessageBox.Show("El campo de Folio final solo acepta caracteres numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                    TextFolioFin.Focus(); 
                    return; 
                }
                int FolioFin;
                if (!int.TryParse(TextFolioFin.Text, out FolioFin))
                { 
                    MessageBox.Show("El folio final debe ser un número válido.", "Error de formato", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                    TextFolioFin.Focus(); 
                    return; 
                }
                if (FolioIni > FolioFin)
                { 
                    MessageBox.Show("El folio inicial no puede ser mayor al final.", "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    TextFolioIni.Focus();
                    return; 
                }

                folioFinNullable = FolioFin; 
            }
            if (string.IsNullOrWhiteSpace(TextUbicacion.Text))
            { 
                MessageBox.Show("Escriba la ubicación física del oficio", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }
            try
            {
                int idOficioGuardado = 0; 

                if (_idOficioActual.HasValue && _idOficioActual.Value > 0)
                {
                    // actualizar el oficio 
                    var oficioEditar = _context.CapturaOficios.Find(_idOficioActual.Value);
                    if (oficioEditar != null)
                    {
                        oficioEditar.TipoCaptura = CmbTipo.Text.ToUpper();
                        if (CmbTipo.Text == "ENVIADO")
                        {
                            oficioEditar.RequiereTicket = "";
                        }
                        else
                        {
                            oficioEditar.RequiereTicket = CmbTicket.Text.Trim().ToUpper();
                        }
                        oficioEditar.Anio = CmbEjercicio.SelectedValue != null ? (int)CmbEjercicio.SelectedValue : (int?)null;
                        oficioEditar.FechaRecepcion = DateRecepcion.Checked ? DateRecepcion.Value.Date : (DateTime?)null;
                        oficioEditar.TipoDocumento = CmbDocumento.Text;
                        oficioEditar.NumeroOficio = TxtNumOficio.Text.Trim().ToUpper();
                        oficioEditar.FechaDocumento = DateDocumento.Checked ? DateDocumento.Value.Date : (DateTime?)null;
                        oficioEditar.TipoSeRecibe = CmbSeRecibe.Text.Trim();
                        oficioEditar.Remitente = CmbRemitente.SelectedValue != null ? (int)CmbRemitente.SelectedValue : (int?)null;
                        oficioEditar.CargoRemitente = cmbCargoRemitente.SelectedValue != null ? (int)cmbCargoRemitente.SelectedValue : (int?)null;
                        oficioEditar.Destinatario = CmbDestinatario.SelectedValue != null ? (int)CmbDestinatario.SelectedValue : (int?)null;
                        oficioEditar.CargoDestinatario = cmbCargoDestinatario.SelectedValue != null ? (int)cmbCargoDestinatario.SelectedValue : (int?)null;                       
                        oficioEditar.Prioridad = CmbPrioridad.Text;
                        oficioEditar.Peticion = TextPeticion.Text.Trim().ToUpper();
                        oficioEditar.Asunto = TextAsunto.Text.Trim().ToUpper(); ;
                        oficioEditar.FolioInicio = FolioIni.ToString();
                        oficioEditar.FolioFin = folioFinNullable?.ToString();                            
                        oficioEditar.Clasificacion = CmbClasif.Text.Trim();
                        oficioEditar.UbicacionFisica = TextUbicacion.Text.Trim().ToUpper();
                        oficioEditar.Estatus = cmbEstatus.Text.Trim();
                        _context.SaveChanges();                     
                        idOficioGuardado = oficioEditar.CapturaOficioId;  
                     /* var segMod = new TSeguimiento 
                        { 
                            TSeguimientoSeguimiento = "ACTUALIZACIÓN DE OFICIO", 
                            OficioId = idOficioGuardado, 
                            TSeguimientoFecha = DateTime.Now, 
                            TSeguimientoNombreUsuario = _service.ObtenerDominioYUsuario(), 
                            TSeguimientoUsuarioId = 0 
                        };
                        _context.TSeguimiento.Add(segMod);
                        _context.SaveChanges();             
                     */
                    }
                }
                else
                {
                    // guardar uno nuevo 
                    CapturaOficios nuevoOficio = new CapturaOficios
                    {  
                        TipoCaptura = CmbTipo.Text.ToUpper(),
                        Anio = CmbEjercicio.SelectedValue != null ? (int)CmbEjercicio.SelectedValue : (int?)null,
                        FechaRecepcion = DateRecepcion.Checked ? DateRecepcion.Value.Date : (DateTime?)null,
                        TipoDocumento = CmbDocumento.Text.ToUpper(),
                        NumeroOficio = TxtNumOficio.Text.Trim().ToUpper(),
                        FechaDocumento = DateDocumento.Checked ? DateDocumento.Value.Date : (DateTime?)null,
                        TipoSeRecibe = CmbSeRecibe.Text.ToUpper(),
                        Remitente = CmbRemitente.SelectedValue != null ? (int)CmbRemitente.SelectedValue : (int?)null,
                        CargoRemitente = cmbCargoRemitente.SelectedValue != null ? (int)cmbCargoRemitente.SelectedValue : (int?)null,
                        Destinatario = CmbDestinatario.SelectedValue != null ? (int)CmbDestinatario.SelectedValue : (int?)null,
                        CargoDestinatario = cmbCargoDestinatario.SelectedValue != null ? (int)cmbCargoDestinatario.SelectedValue : (int?)null,
                        RequiereTicket = CmbTicket.Text.Trim().ToUpper(),
                        Prioridad = CmbPrioridad.Text.ToUpper(),
                        Peticion = TextPeticion.Text.Trim().ToUpper(),
                        Asunto = TextAsunto.Text.Trim().ToUpper(),
                        FolioInicio = FolioIni.ToString(),
                        FolioFin = folioFinNullable?.ToString(),
                        Clasificacion = CmbClasif.Text.Trim().ToUpper(),
                        UsuarioCaptura = _service.ObtenerDominioYUsuario(),
                        UbicacionFisica = TextUbicacion.Text.Trim().ToUpper(),
                        Estatus = "ABIERTO",
                        FechaCaptura = DateTime.Now,
                        Activo = 1
                    };
                    _context.CapturaOficios.Add(nuevoOficio);
                    _context.SaveChanges();
                    idOficioGuardado = nuevoOficio.CapturaOficioId;
                    string usuariodominio = _service.ObtenerDominioYUsuario();

                    int idusuario = _context.Usuarios
                                                    .Where(y => y.UsuarioDominio == usuariodominio)
                                                    .Select(y => y.UsuarioId)
                                                    .FirstOrDefault();
                    // Seguimiento de oficio
                    var nuevoseguimiento = new TSeguimiento 
                    { 
                        TSeguimientoSeguimiento = "CAPTURA DEL OFICIO",
                        OficioId = idOficioGuardado,
                        TSeguimientoFecha = DateTime.Now,
                        TSeguimientoNombreUsuario = usuariodominio,                        
                        TSeguimientoUsuarioId = idusuario
                    };
                    _context.TSeguimiento.Add(nuevoseguimiento);
                    _context.SaveChanges();
                }
                
                if (!string.IsNullOrEmpty(_rutaArchivoTemporal) && System.IO.File.Exists(_rutaArchivoTemporal))
                {
                    try
                    {
                        byte[] bytes = System.IO.File.ReadAllBytes(_rutaArchivoTemporal);
                        var archivoExistente = _context.Archivo.FirstOrDefault(a => a.OficioId == idOficioGuardado);

                        if (archivoExistente != null)
                        {                            
                            archivoExistente.ArchivoNombre = System.IO.Path.GetFileName(_rutaArchivoTemporal);
                            archivoExistente.ArchivoArchivo = bytes;
                            archivoExistente.ArchivoFecha = DateTime.Now;
                        }
                        else
                        {                           
                            var archivo = new Archivo
                            {
                                ArchivoNombre = System.IO.Path.GetFileName(_rutaArchivoTemporal),
                                ArchivoArchivo = bytes, 
                                ArchivoFecha = DateTime.Now, 
                                OficioId = idOficioGuardado
                            };
                            _context.Archivo.Add(archivo);
                        }
                        _context.SaveChanges();
                    }
                    catch (Exception exArchivo)
                    {
                        string errorReal = exArchivo.InnerException?.Message ?? exArchivo.Message;
                        MessageBox.Show("El registro se guardó, pero el archivo fue rechazado por la base de datos.\n\nMotivo: " + errorReal, "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                             
                MessageBox.Show("El oficio y su archivo se guardaron correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                if (CmbTicket.Text.Trim().Equals("SI", StringComparison.OrdinalIgnoreCase))
                {
                    
                    using (FrmCrearTicket frmTicket = new FrmCrearTicket(_context, _usuarioActual))
                    {
                        frmTicket.IdOficioVinculado = idOficioGuardado; 
                        frmTicket.AsuntoPredefinido = TextAsunto.Text.Trim();
                        frmTicket.Instruccion = TextPeticion.Text.Trim();
                        frmTicket.ShowDialog();
                    }
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al guardar: " + (ex.InnerException?.Message ?? ex.Message), "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        ///  GUARDAR EL PDF 
        private string _rutaArchivoTemporal = string.Empty;
        private string _nombreArchivo = string.Empty;
        private void btnAdjuntar_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                // ofd.Filter = "Documentos (*.pdf;*.docx;*.jpg;*.png)|*.pdf;*.docx;*.jpg;*.png|Todos los archivos (*.*)|*.*";
                /// Solo documentos PDF 
                ofd.Filter = "Documentos (*.pdf)|*.pdf;";
                ofd.Title = "Seleccione el documento a adjuntar";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    _rutaArchivoTemporal = ofd.FileName;
                    _nombreArchivo = System.IO.Path.GetFileName(ofd.FileName);
                    btnAdjuntar.Text = "📎 " + _nombreArchivo;
                    btnQuitarAdjunto.Visible = true;
                    MessageBox.Show("Archivo seleccionado. Se subirá al sistema cuando presione 'Guardar'.", "Archivo adjunto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        // BORRAR EL ARCHIVO PDF 
        private void btnQuitarAdjunto_Click(object sender, EventArgs e)
        {
            _rutaArchivoTemporal = string.Empty;
            _nombreArchivo = string.Empty;
            btnAdjuntar.Text = "Adjuntar archivo";
            btnQuitarAdjunto.Visible = false;
        }
    }
}