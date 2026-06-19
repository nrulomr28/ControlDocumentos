using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Documents;
using OficiosTI.Models;
using OficiosTI.Services;
using System.Data;
using System.Diagnostics;

namespace OficiosTI
{
    public partial class FrmOficioRespuesta : Form
    {
        private Ticket _ticket;
        private OficiosContext _context;
        private OficioRespuestaService _service;
        private OficioRespuesta _oficioActual;   ///OFICIORESPUESTA 
        private Oficio1 _oficioAntes;           ////// OFICIO DE DONDE VIENE EL TICKET
        private DestinatarioService _destinatarioService;
        private List<DestinatarioItem> _destinatariosCache;
        private bool _autocompletando = false;

      //  private CheckBox chkFirmaPorAusencia;

        public FrmOficioRespuesta(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();

            _ticket = ticket;        
            _context = context;
         
            _service = new OficioRespuestaService(_context);
            var oficio = _service.ObtenerOficioPorTicket(ticket.TicketId);

            if (oficio != null)
            {
                CargarOficio(oficio);
            }
            else
            {
                var borrador = _service.ConvertirTicketEnBorrador(ticket);
                CargarOficio(borrador);
            }
            if (txtCopias.Text == string.Empty)
            {
                txtCopias.Text = ObtenerCopiasDefault();
            }

            _destinatarioService = new DestinatarioService(_context);
            _destinatariosCache = _destinatarioService.ObtenerCatalogo();

            //    ConfigurarAutoComplete();
            //     InicializarFirmantes();

           CargarFirmantes();

           if (oficio?.FirmanteId.HasValue == true)
            {
                comboBox1.SelectedValue = (int)oficio.FirmanteId.Value;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }
               
        }


        /*
        private void ConfigurarAutoComplete()
        {
            var nombres = new AutoCompleteStringCollection();
            var cargos = new AutoCompleteStringCollection();

            nombres.AddRange(_destinatariosCache.Select(x => x.Nombre).Distinct().ToArray());
            cargos.AddRange(_destinatariosCache.Select(x => x.Cargo).Distinct().ToArray());

            txtDestinatario.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtDestinatario.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtDestinatario.AutoCompleteCustomSource = nombres;

            txtCargo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtCargo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtCargo.AutoCompleteCustomSource = cargos;
        }
        */
        private string ObtenerCopiasDefault()
        {
            return "C.C.P. Lic. Andrés Augusto Rosaldo García. - Oficial Mayor de la SSP. - Para su superior conocimiento. – Presente.";
        }



        private void BtnPreview_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string textoCombo = comboBox1.Text;
                string nombreExtraido = textoCombo;
                string cargoExtraido = "Sin Cargo";

                if (!string.IsNullOrEmpty(textoCombo) && textoCombo.Contains("-"))
                {
                      var partes = textoCombo.Split('-', StringSplitOptions.TrimEntries);

                    if (partes.Length >= 2)
                    {
                        nombreExtraido = partes[0]; // NOMBRE DEL TITULAR
                        cargoExtraido = partes[1];  // CARGO DEL TITULAR
                    }
                }

                var model = new OficioModel
                {
                    NumeroOficio = txtNumeroOficio.Text,
                    Asunto = txtAsunto.Text,
                    OficioReferencia = txtOficioReferencia.Text,
                    Destinatario = txtDestinatario.Text,
                    CargoDestinatario = txtCargo.Text,
                    FundamentoLegal = ObtenerFundamentoLegal(cargoExtraido),
                    Cuerpo = txtRespuesta.Text,
                    Copias = txtCopias.Text,
                    Fecha = DateTime.Now,
                    DirectorNombre = nombreExtraido,
                    DirectorCargo = cargoExtraido,
                   
                };

                var servicio = new OficioWordInteropService();
                string ruta = servicio.Generar(model);
                Process.Start(new ProcessStartInfo
                {
                    FileName = ruta,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al previsualizar el oficio:\n\n{ex.Message}",
                                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /* private void BtnPreview_Click(object sender, EventArgs e)
         {
             //var model = new OficioModel
             //{
             //    NumeroOficio = txtNumeroOficio.Text,

             //    Asunto = txtAsunto.Text,

             //    OficioReferencia = txtOficioReferencia.Text,

             //    Destinatario = txtDestinatario.Text,

             //    CargoDestinatario = txtCargo.Text,

             //    FundamentoLegal = ObtenerFundamentoLegal(),

             //    Cuerpo = txtRespuesta.Text,

             //    Copias = txtCopias.Text,

             //    Fecha = DateTime.Now,

             //    DirectorNombre = "Ing. Luis Felipe Ramírez Flores",
             //    DirectorCargo = "Director de Tecnologías de la Información"
             //};

             ////var pdf = new OficioRespuestaWord(model);
             //var word = new OficioRespuestaWord(model);
             //string ruta = Path.Combine(Path.GetTempPath(), "preview_oficio.pdf");

             ////pdf.GeneratePdf(ruta);
             //var bytes = word.Generar();
             //Process.Start(new ProcessStartInfo
             //{
             //    FileName = ruta,
             //    UseShellExecute = true
             //});
             var model = new OficioModel
             {
                 NumeroOficio = txtNumeroOficio.Text,
                 Asunto = txtAsunto.Text,
                 OficioReferencia = txtOficioReferencia.Text,
                 Destinatario = txtDestinatario.Text,
                 CargoDestinatario = txtCargo.Text,
                 FundamentoLegal = ObtenerFundamentoLegal(),
                 Cuerpo = txtRespuesta.Text,
                 Copias = txtCopias.Text,
                 Fecha = DateTime.Now,
                 DirectorNombre = "Ing. Luis Felipe Ramírez Flores",
                 DirectorCargo = "Director de Tecnologías de la Información"
             };

             var servicio = new OficioWordInteropService();
             string ruta = servicio.Generar(model);

             Process.Start(new ProcessStartInfo
             {
                 FileName = ruta,
                 UseShellExecute = true
             });


         }

         */

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            GuardarOficio();
        }

        private void GuardarOficio()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroOficio.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool esNuevo = _oficioActual.OficioRespuestaId == 0;

            if ((esNuevo || _oficioActual.NumeroOficio != txtNumeroOficio.Text)
                && _service.NumeroOficioExiste(txtNumeroOficio.Text))
            {
                MessageBox.Show("Ese número de oficio ya existe. Por favor, asigne otro folio.", "Folio Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            OficioRespuesta snapshotAnterior = null;

            if (!esNuevo)
            {
                snapshotAnterior = new OficioRespuesta
                {
                    NumeroOficio = _oficioActual.NumeroOficio,
                    OficioReferencia = _oficioActual.OficioReferencia,
                    Asunto = _oficioActual.Asunto,
                    Destinatario = _oficioActual.Destinatario,
                    CargoDestinatario = _oficioActual.CargoDestinatario,
                    CuerpoRespuesta = _oficioActual.CuerpoRespuesta,
                    Copias = _oficioActual.Copias,                
                    Firmante = _oficioActual.Firmante
                };

 
            }

            int? firmanteSeleccionado = comboBox1.SelectedValue as int?;
            ///OBTENER EL ID DEL OFICIO DE REFERENCIA /////
            var Ofinum = _context.Oficio1
                 .Where(y => y.OficioId == _ticket.id_of)
                 .FirstOrDefault();

            int idOficioAnterior = Ofinum?.OficioId ?? 0;
            ///// OFICIO DE REFERENCIA /////

            ///// OBTENER EL ID DEL OFICIO DE NUMCONSECUTIVO 
     
         var NumCons = _context.NumOficio
                .Where(x => x.OficioId == _oficioActual.RespuestaId)
                .FirstOrDefault();

            if (esNuevo)
            {
                    _oficioActual = _service.CrearOficio(
                    _ticket.TicketId,
                    txtNumeroOficio.Text,
                    txtOficioReferencia.Text,
                    txtAsunto.Text,
                    txtDestinatario.Text,
                    txtCargo.Text,
                    txtRespuesta.Text,
                    txtCopias.Text,
                    firmanteSeleccionado,
                    idOficioAnterior
                );
            }
            else
            {
                _oficioActual.NumeroOficio = txtNumeroOficio.Text;
                _oficioActual.OficioReferencia = txtOficioReferencia.Text;
                _oficioActual.Asunto = txtAsunto.Text;
                _oficioActual.Destinatario = txtDestinatario.Text;
                _oficioActual.CargoDestinatario = txtCargo.Text;
                _oficioActual.CuerpoRespuesta = txtRespuesta.Text;
                _oficioActual.Copias = txtCopias.Text;
                _oficioActual.FirmanteId = firmanteSeleccionado;

                _service.ActualizarOficio(_oficioActual);
            }

            bool huboCambios = esNuevo || HayCambios(snapshotAnterior, _oficioActual);

            if (huboCambios)
            {
                string descripcion = $"{txtNumeroOficio.Text} emitido como respuesta al asunto: {txtAsunto.Text}";

                if (!_service.HiloYaExiste(_ticket.TicketId, descripcion))
                {
                    _service.RegistrarHiloOficio(
                        _ticket.TicketId,
                        descripcion
                    );
                }
            }

            if (esNuevo)
            {
                _ticket.Cat_TicketStatusId = 3;
                _service.ActualizarOficio(_oficioActual);

            }

            MessageBox.Show("Oficio generado y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }

        /*
        private void GuardarOficio()
        {
            if (string.IsNullOrWhiteSpace(txtNumeroOficio.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.");
                return;
            }

            // Validar duplicado solo si es nuevo o cambió el número
            if ((_oficioActual == null || _oficioActual.NumeroOficio != txtNumeroOficio.Text)
                && _service.NumeroOficioExiste(txtNumeroOficio.Text))
            {
                MessageBox.Show("Ese número de oficio ya existe.");
                return;
            }

            bool esNuevo = _oficioActual == null;

            // 🔥 Snapshot FUERTE (sin dynamic)
            OficioRespuesta snapshotAnterior = null;

            if (!esNuevo)
            {
                snapshotAnterior = new OficioRespuesta
                {
                    NumeroOficio = _oficioActual.NumeroOficio,
                    OficioReferencia = _oficioActual.OficioReferencia,
                    Asunto = _oficioActual.Asunto,
                    Destinatario = _oficioActual.Destinatario,
                    CargoDestinatario = _oficioActual.CargoDestinatario,
                    CuerpoRespuesta = _oficioActual.CuerpoRespuesta,
                    Copias = _oficioActual.Copias
                };
            }

            if (esNuevo)
            {
                _oficioActual = _service.CrearOficio(
                    _ticket.TicketId,
                    txtNumeroOficio.Text,
                    txtOficioReferencia.Text,
                    txtAsunto.Text,
                    txtDestinatario.Text,
                    txtCargo.Text,
                    txtRespuesta.Text,
                    txtCopias.Text
                );
            }
            else
            {
                _oficioActual.NumeroOficio = txtNumeroOficio.Text;
                _oficioActual.OficioReferencia = txtOficioReferencia.Text;
                _oficioActual.Asunto = txtAsunto.Text;
                _oficioActual.Destinatario = txtDestinatario.Text;
                _oficioActual.CargoDestinatario = txtCargo.Text;
                _oficioActual.CuerpoRespuesta = txtRespuesta.Text;
                _oficioActual.Copias = txtCopias.Text;

                _service.ActualizarOficio(_oficioActual);
            }

            // 🔥 Detectar cambios reales
            bool huboCambios = esNuevo || HayCambios(snapshotAnterior, _oficioActual);

            if (huboCambios)
            {
                string descripcion = $"{txtNumeroOficio.Text} emitido como respuesta al asunto: {txtAsunto.Text}";

                // 🔥 Evitar duplicados en historial
                if (!_service.HiloYaExiste(_ticket.TicketId, descripcion))
                {
                    _service.RegistrarHiloOficio(
                        _ticket.TicketId,
                        descripcion
                    );
                }
            }

            MessageBox.Show("Oficio guardado correctamente.");
            Close();
        }
        */
        private bool HayCambios(OficioRespuesta anterior, OficioRespuesta actual)
        {
            if (anterior == null) return true;

            return
                Normalizar(anterior.NumeroOficio) != Normalizar(actual.NumeroOficio) ||
                Normalizar(anterior.OficioReferencia) != Normalizar(actual.OficioReferencia) ||
                Normalizar(anterior.Asunto) != Normalizar(actual.Asunto) ||
                Normalizar(anterior.Destinatario) != Normalizar(actual.Destinatario) ||
                Normalizar(anterior.CargoDestinatario) != Normalizar(actual.CargoDestinatario) ||
                Normalizar(anterior.CuerpoRespuesta) != Normalizar(actual.CuerpoRespuesta) ||
                Normalizar(anterior.Copias) != Normalizar(actual.Copias);
        }

        private string Normalizar(string valor)
        {
            return (valor ?? string.Empty).Trim();
        }

        /*
        private void CargarOficio(OficioRespuesta oficio)
        {
            _oficioActual = oficio;

            txtNumeroOficio.Text = oficio.NumeroOficio;
            txtOficioReferencia.Text = oficio.OficioReferencia;
            txtAsunto.Text = oficio.Asunto;
            txtDestinatario.Text = oficio.Destinatario;
            txtCargo.Text = oficio.CargoDestinatario;
            txtRespuesta.Text = oficio.CuerpoRespuesta;
            txtCopias.Text = oficio.Copias;
        }
        */
        /*
        public OficioRespuesta ObtenerOficioPorId(int idBuscado)
        {
            using (var Oficio1 = new Oficio1()) 
            {
                var oficio = dbContext.Oficios
                                      .FirstOrDefault(o => o.OficioId == idBuscado);

                /* NOTA: Si "Firmante" está en OTRA tabla y necesitas traerlo al mismo tiempo (JOIN),
                usarías .Include():
                var oficio = dbContext.Oficios
                                      .Include(o => o.Firmante)
                                      .FirstOrDefault(o => o.OficioId == idBuscado);
                */
        /*
                return oficio;
            }
        }*/

        private void CargarOficio(OficioRespuesta oficio)
        {
            _oficioActual = oficio ?? new OficioRespuesta();

            // 2. Asignación directa usando string.Empty
            txtNumeroOficio.Text = _oficioActual.NumeroOficio ?? string.Empty;
            txtOficioReferencia.Text = _oficioActual.OficioReferencia ?? string.Empty;
            txtAsunto.Text = _oficioActual.Asunto ?? string.Empty;
            txtDestinatario.Text = _oficioActual.Destinatario ?? string.Empty;
            txtCargo.Text = _oficioActual.CargoDestinatario ?? string.Empty;
            txtRespuesta.Text = _oficioActual.CuerpoRespuesta ?? string.Empty;
            txtCopias.Text = _oficioActual.Copias ?? string.Empty;

            if (_oficioActual.FirmanteId != null)
            {
                comboBox1.SelectedValue = _oficioActual.FirmanteId;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }
        }
        /*
        private void CargarOficio(OficioRespuesta oficio)
        {

            if (oficio == null)
            {
                _oficioActual = new OficioRespuesta();

                return;
            }

            _oficioActual = oficio;

            if (oficio != null && oficio.OficioId != null)
            {
                // Convertimos el ID a texto para poder mostrarlo en el TextBox
                txtOficioReferencia.Text = oficio.OficioId.ToString();
            }
            else
            {
           
                txtOficioReferencia.Text = string.Empty; 
            }

            txtNumeroOficio.Text = oficio.NumeroOficio ?? "";
           // txtNumeroOficio.Text = _oficioAnterior.;
            txtOficioReferencia.Text = oficio.OficioReferencia ?? "";
            txtAsunto.Text = oficio.Asunto ?? "";
            txtDestinatario.Text = oficio.Destinatario ?? "";
            txtCargo.Text = oficio.CargoDestinatario ?? "";
            txtRespuesta.Text = oficio.CuerpoRespuesta ?? "";
            txtCopias.Text = oficio.Copias ?? "";
            if (oficio.FirmanteId != null)
            {
                comboBox1.SelectedValue = oficio.FirmanteId;
            }
            else
            {
                comboBox1.SelectedIndex = -1;
            }

        }*/

       /*   private string ObtenerFundamentoLegal()
           {
          
               return @"De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo del Estado de Veracruz de Ignacio de la Llave; 3 segundo párrafo del Código de Procedimientos Administrativos; 186 fracción III del Código Financiero del Estado; así como los artículos 2, 3, 6 fracción XIII, 11 fracciones VI y VIII y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz;";

           }*/

        private string ObtenerFundamentoLegal(string cargoFirmante)
        {
            string cargoNormalizado = cargoFirmante.Trim();

            switch (cargoNormalizado)
            {
                case "Director de Tecnologías de la Información":
                    return @"De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo del Estado de Veracruz de Ignacio de la Llave; 3 segundo párrafo del Código de Procedimientos Administrativos; 186 fracción III del Código Financiero del Estado; así como los artículos 2, 3, 6 fracción XIII, 11 fracciones VI y VIII y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz;";

                case "Jefe del Departamento de Seguridad de Redes en la Dirección de Tecnologías de la Información":
                    return "De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo; 3, segundo párrafo, del Código de Procedimientos Administrativos, todos del Estado de Veracruz de Ignacio de la Llave; así como 2, 3, 6 fracción XIII, 11 fracciones VI y VIII, 40 fracciones VII y XXVIII, y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz, demás disposiciones legales aplicables, y por instrucciones del Ing. Luis Felipe Ramírez Flores, Director de Tecnologías de la Información,";

                case "Jefe del Departamento de Desarrollo Digital en la Dirección de Tecnologías de la Información":
                    return "De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo; 3, segundo párrafo, del Código de Procedimientos Administrativos, todos del Estado de Veracruz de Ignacio de la Llave; así como 2, 3, 6 fracción XIII, 11 fracciones VI y VIII, 40 fracciones VII y XXVIII, y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz, demás disposiciones legales aplicables, y por instrucciones del Ing. Luis Felipe Ramírez Flores, Director de Tecnologías de la Información,";

                default:
                    return @"De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo del Estado de Veracruz de Ignacio de la Llave; 3 segundo párrafo del Código de Procedimientos Administrativos; 186 fracción III del Código Financiero del Estado; así como los artículos 2, 3, 6 fracción XIII, 11 fracciones VI y VIII y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz;";

            }
        }

        private void txtDestinatario_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtDestinatario.Text))
                return;

            var match = _context.OficioRespuesta
                .Where(x => x.Destinatario == txtDestinatario.Text)
                .OrderByDescending(x => x.OficioRespuestaId) // el más reciente
                .FirstOrDefault();

            if (match != null)
            {
                txtCargo.Text = match.CargoDestinatario;
            }
        }

        private void txtCargo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCargo.Text))
                return;

            var match = _context.OficioRespuesta
                .Where(x => x.CargoDestinatario == txtCargo.Text)
                .OrderByDescending(x => x.OficioRespuestaId)
                .FirstOrDefault();

            if (match != null)
            {
                txtDestinatario.Text = match.Destinatario;
            }
        }

        private void txtDestinatario_TextChanged(object sender, EventArgs e)
        {
            if (_autocompletando) return;

            if (_destinatariosCache == null) return;

            var texto = txtDestinatario.Text.Trim().ToLower();

            if (string.IsNullOrWhiteSpace(texto))
            {
                txtCargo.Text = "";
                return;
            }

            var match = _destinatariosCache
                .FirstOrDefault(x => x.Nombre.ToLower().Contains(texto));

            if (match != null)
            {
                _autocompletando = true;
                txtCargo.Text = match.Cargo;
                _autocompletando = false;
            }
        }

        private void CargarFirmantes()
        {
            var firmantes = _context.Firmante
                .Where(x => x.Activo)
                .ToList();

            comboBox1.DataSource = firmantes;
            comboBox1.DisplayMember = "NombreCompleto";
            comboBox1.ValueMember = "FirmanteId";

            comboBox1.SelectedIndex = -1;

        }

        /*    private void txtDestinatario_TextChanged(object sender, EventArgs e)
            {
                if (_autocompletando) return;

                var texto = txtDestinatario.Text.Trim().ToLower();

                var match = _destinatariosCache
                    .FirstOrDefault(x => x.Nombre.ToLower().Contains(texto));

                if (match != null)
                {
                    _autocompletando = true;
                    txtCargo.Text = match.Cargo;
                    _autocompletando = false;
                }
            }
      private void txtCargo_TextChanged(object sender, EventArgs e)
         {
             if (_autocompletando) return;

             var texto = txtCargo.Text.Trim().ToLower();

             var match = _destinatariosCache
                 .FirstOrDefault(x => x.Cargo.ToLower().Contains(texto));

             if (match != null)
             {
                 _autocompletando = true;
                 txtDestinatario.Text = match.Nombre;
                 _autocompletando = false;
             }
         }
         private void txtCargo_TextChanged(object sender, EventArgs e)
            {
                if (_autocompletando || _destinatariosCache == null) return;

                var texto = txtCargo.Text.Trim().ToLower();

                if (string.IsNullOrEmpty(texto) || texto.Length < 3)
                {
                    return;
                }

                var match = _destinatariosCache
                    .FirstOrDefault(x => x.Cargo != null && x.Cargo.ToLower().Contains(texto));

                if (match != null && string.IsNullOrEmpty(txtDestinatario.Text))
                {
                    _autocompletando = true;
                    txtDestinatario.Text = match.Nombre;
                    _autocompletando = false;
                }
            }*/







    }
}


// Versión 1.0
//private void GuardarOficio()
//{
//    if (string.IsNullOrWhiteSpace(txtNumeroOficio.Text))
//    {
//        MessageBox.Show("Debe capturar el número de oficio.");
//        return;
//    }

//    // validar duplicado solo si es nuevo o cambió el número
//    if ((_oficioActual == null || _oficioActual.NumeroOficio != txtNumeroOficio.Text)
//        && _service.NumeroOficioExiste(txtNumeroOficio.Text))
//    {
//        MessageBox.Show("Ese número de oficio ya existe.");
//        return;
//    }

//    bool esNuevo = _oficioActual == null;

//    if (esNuevo)
//    {
//        _oficioActual = _service.CrearOficio(
//            _ticket.TicketId,
//            txtNumeroOficio.Text,
//            txtOficioReferencia.Text,
//            txtAsunto.Text,
//            txtDestinatario.Text,
//            txtCargo.Text,
//            txtRespuesta.Text,
//            txtCopias.Text
//        );
//    }
//    else
//    {
//        _oficioActual.NumeroOficio = txtNumeroOficio.Text;
//        _oficioActual.OficioReferencia = txtOficioReferencia.Text;
//        _oficioActual.Asunto = txtAsunto.Text;
//        _oficioActual.Destinatario = txtDestinatario.Text;
//        _oficioActual.CargoDestinatario = txtCargo.Text;
//        _oficioActual.CuerpoRespuesta = txtRespuesta.Text;
//        _oficioActual.Copias = txtCopias.Text;

//        _service.ActualizarOficio(_oficioActual);
//    }

//    // SOLO registrar hilo cuando es nuevo
//    //if (esNuevo)
//    //{
//        _service.RegistrarHiloOficio(
//            _ticket.TicketId,
//            $"{txtNumeroOficio.Text} emitido como respuesta al asunto: {txtAsunto.Text}");
//     //}

//MessageBox.Show("Oficio guardado correctamente.");

//    Close();
//}