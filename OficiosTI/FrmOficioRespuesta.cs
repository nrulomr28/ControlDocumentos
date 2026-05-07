using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Documents;
using OficiosTI.Models;
using OficiosTI.Services;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OfficeCore = Microsoft.Office.Core;
using Word = Microsoft.Office.Interop.Word;
using WordInterop = Microsoft.Office.Interop.Word;

namespace OficiosTI
{
    public partial class FrmOficioRespuesta : Form
    {
        private Ticket _ticket;
        private OficiosContext _context;
        private OficioRespuestaService _service;
        private OficioRespuesta? _oficioActual;


        private DestinatarioService _destinatarioService;
        private List<DestinatarioItem> _destinatariosCache;
        private bool _autocompletando = false;

        private ComboBox cmbFirmante;
        private CheckBox chkFirmaPorAusencia;
        public FrmOficioRespuesta(Ticket ticket, OficiosContext context)
        {
            //InitializeComponent();

            _ticket = ticket;
            _context = context;

            _service = new OficioRespuestaService(_context);

            var oficio = _service.ObtenerOficioPorTicket(ticket.TicketId);

            if (oficio != null)
            {
                // Ya existe oficio → se carga para edición
                CargarOficio(oficio);
            }
            else
            {
                // No existe → se crea borrador usando ticket + último hilo
                var borrador = _service.ConvertirTicketEnBorrador(ticket);
                CargarOficio(borrador);
            }
            if (txtCopias.Text == string.Empty)
            {
                txtCopias.Text = ObtenerCopiasDefault();
            }

            _destinatarioService = new DestinatarioService(_context);
            _destinatariosCache = _destinatarioService.ObtenerCatalogo();

            ConfigurarAutoComplete();

            InicializarFirmantes();
            CargarFirmantes();
        }

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

        private string ObtenerCopiasDefault()
        {
            return "C.C.P. Lic. Andrés Augusto Rosaldo García.- Oficial Mayor de la SSP.- Para su superior conocimiento. – Presente.";
        }

        private void BtnPreview_Click(object sender, EventArgs e)
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

        private void BtnGuardar_Click(object sender, EventArgs e)
        {
            GuardarOficio();
        }

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

        private string ObtenerFundamentoLegal()
        {
            return @"De conformidad con lo dispuesto en los artículos 1, 10 y 13 de la Ley Orgánica del Poder Ejecutivo del Estado de Veracruz de Ignacio de la Llave; 3 segundo párrafo del Código de Procedimientos Administrativos; 186 fracción III del Código Financiero del Estado; así como los artículos 2, 3, 6 fracción XIII, 11 fracciones VI y VIII y 65 del Reglamento Interior vigente de la Secretaría de Seguridad Pública del Estado de Veracruz;";
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
        

        private void CargarFirmantes()
        {
            var firmantes = _context.Firmantes
                .Where(x => x.Activo)
                .ToList();

            cmbFirmante.DataSource = firmantes;
            cmbFirmante.DisplayMember = "Nombre";
            cmbFirmante.ValueMember = "FirmanteId";
        }

        private void cmbFirmante_SelectedIndexChanged(object sender, EventArgs e)
        {
            var firmante = (Firmante)cmbFirmante.SelectedItem;

            txtDestinatario.Text = firmante.Nombre;
            txtCargo.Text = firmante.Cargo;

            if (chkFirmaPorAusencia.Checked)
            {
                txtCargo.Text = $"Por ausencia del titular\n{firmante.Cargo}";
            }
        }

        private void InicializarFirmantes()
        {
            // 🔹 Label sección
            Label lblSeccionFirma = new Label
            {
                Text = "Firmante",
                Left = 20,
                Top = txtCargo.Bottom + 15,
                Font = new Font("Tahoma", 9F, FontStyle.Bold)
            };

            // 🔹 Combo
            cmbFirmante = new ComboBox
            {
                Left = 20,
                Top = lblSeccionFirma.Bottom + 5,
                Width = 260,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            // 🔹 Check
            chkFirmaPorAusencia = new CheckBox
            {
                Left = 20,
                Top = cmbFirmante.Bottom + 5,
                Text = "Firmar por ausencia del titular"
            };

            panelFormulario.Controls.Add(lblSeccionFirma);
            panelFormulario.Controls.Add(cmbFirmante);
            panelFormulario.Controls.Add(chkFirmaPorAusencia);

            cmbFirmante.SelectedIndexChanged += cmbFirmante_SelectedIndexChanged;
        }
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