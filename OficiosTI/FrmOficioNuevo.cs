using DocumentFormat.OpenXml.InkML;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Documents;
using OficiosTI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmOficioNuevo : Form
    {
        private OficiosContext _context;
        private OficioRespuesta _oficioActual;   ///OFICIORESPUESTA 

        private NumOficio _oficioconse;
        private OficioRespuestaService _service;

     /*   public FrmOficioNuevo(OficiosContext context, OficioRespuesta OficioResp)
        {
            InitializeComponent();
            _context = context;
            _oficioActual = OficioResp;
            CargarFirma();

            if (txtCcp.Text == string.Empty)
            {
                txtCcp.Text = ObtenerCopiasDefault();
            }

            CargarResp(OficioResp);
        }*/

  
        public FrmOficioNuevo(OficiosContext context, OficioRespuesta OficioResp = null)
        {
            InitializeComponent();
            _context = context;
            _oficioActual = OficioResp;
            CargarFirma();
            //CargarOficios(3);
            if (string.IsNullOrEmpty(txtCcp.Text))
            {
                txtCcp.Text = ObtenerCopiasDefault();
            }

            if (_oficioActual != null)
            {
                CargarResp(_oficioActual);
            }
        }

        private void CargarFirma()
        {
            var firmantes = _context.Firmante
                .Where(x => x.Activo)
                .ToList();

            cmbFirmas.DataSource = firmantes;
            cmbFirmas.DisplayMember = "NombreCompleto";
            cmbFirmas.ValueMember = "FirmanteId";
            cmbFirmas.SelectedIndex = -1;
        }


        /*   private void CargarOficios(int idOficinaDeseada)
           {
               if (_context == null) return;

               var of_conse = _context.NumOficio
                   .Where(x => x.Oficinas_Id == idOficinaDeseada)
                   .OrderByDescending(x => x.OficioId)
                   .ToList();

               cmbOf.DataSource = of_conse;
               cmbOf.DisplayMember = "NumeroConsecutivo";
               cmbOf.ValueMember = "OficioId";
           }
        */
      /*  private void CargarOficios(int idOficinaDeseada)
        {
            if (_context == null) return;

            var of_conse = (from num in _context.NumOficio
                            join resp in _context.OficioRespuesta
                            on num.OficioId equals resp.RespuestaId
                            where num.Oficinas_Id == idOficinaDeseada
                            where resp.TicketId == 0 || resp.TicketId == null
                            orderby num.OficioId descending
                            select new
                            {
                                oficioRespuestaId = resp.OficioRespuestaId,
                                NumeroOficio = resp.NumeroOficio
                            }).ToList();

            cmbOf.DataSource = of_conse;
            cmbOf.DisplayMember = "NumeroOficio";
            cmbOf.ValueMember = "oficioRespuestaId";

            cmbOf.SelectedIndex = -1;
        }

        */
        /*
        private void CargarOficios(int idOficinaDeseada)
        {
            if (_context == null) return;

            // Usamos LINQ Join para buscar en ambas tablas al mismo tiempo
            var of_conse = (from num in _context.NumOficio
                            join resp in _context.OficioRespuesta
                            on num.OficioId equals resp.RespuestaId // Comparamos las llaves de relación
                            where num.Oficinas_Id == idOficinaDeseada
                            orderby num.OficioId descending
                            select new
                            {
                                OficioId = num.OficioId,
                                NumeroConsecutivo = num.NumeroConsecutivo
                            }).ToList();

            cmbOf.DataSource = of_conse;
            cmbOf.DisplayMember = "NumeroOficio";
            cmbOf.ValueMember = "oficioRespuestaId";
            cmbOf.SelectedIndex = -1;
        }

        /*     private void CargarOficios()
             {
                 var of_conse = _context.NumOficio
                     .Where(x => x.Oficinas_Id)
                     .Tolist();

                 cmbOf.DataSource = of_conse;
                 cmbOf.DisplayMember = "NumeroConsecutivo";
                 cmbOf.ValueMember = "OficioId";
             }
        */


        /*    private void GuardarOficio()
              {
                  try
                  {
                      bool esRegistroNuevo = (_oficioActual == null);

                      if (esRegistroNuevo)
                      {
                          _oficioActual = new OficioRespuesta();
                          _oficioActual.FechaOficio = DateTime.Now; 
                      }

                      _oficioActual.NumeroOficio = txtNof.Text.Trim();
                      _oficioActual.Asunto = txtAsunto.Text.Trim();
                      _oficioActual.Destinatario = txtDest.Text.Trim();
                      _oficioActual.CargoDestinatario = txtCrgo.Text.Trim();
                      _oficioActual.CuerpoRespuesta = txtRespC.Text.Trim();
                      _oficioActual.Copias = txtCcp.Text.Trim();

                      if (cmbFirmas.SelectedValue != null)
                      {
                          _oficioActual.FirmanteId = (int)cmbFirmas.SelectedValue;
                      }

                      if (esRegistroNuevo)
                      {
                          _context.OficioRespuesta.Add(_oficioActual);
                      }

                      _context.SaveChanges();

                      MessageBox.Show("El oficio se ha guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                      this.DialogResult = DialogResult.OK;
                      this.Close();
                  }
                  catch (Exception ex)
                  {
                      MessageBox.Show($"Ocurrió un error al guardar en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                  }
              }

         */

        private void btnGuardarNu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNof.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtAsunto.Text))
            {
                MessageBox.Show("Debe capturar el Asunto del Oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtDest.Text))
            {
                MessageBox.Show("Debe capturar el Destinatario.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCrgo.Text))
            {
                MessageBox.Show("Debe capturar el cargo.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRespC.Text))
            {
                MessageBox.Show("Debe capturar la Respuesta del Oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCcp.Text))
            {
                MessageBox.Show("Debe capturar las ccp.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbFirmas.Text))
            {
                MessageBox.Show("Debe seleccionar quien firma.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            GuardarOficio();

        }

        private void GuardarOficio()
        {
            try
            {
                bool esRegistroNuevo = (_oficioActual == null);

                if (esRegistroNuevo)
                {
                    _oficioActual = new OficioRespuesta();
                    _oficioActual.FechaOficio = DateTime.Now;
                }
                _oficioActual.TicketId = 0;
                _oficioActual.NumeroOficio = txtNof.Text.Trim();
                _oficioActual.Asunto = txtAsunto.Text.Trim();
                _oficioActual.Destinatario = txtDest.Text.Trim();
                _oficioActual.CargoDestinatario = txtCrgo.Text.Trim();
                _oficioActual.CuerpoRespuesta = txtRespC.Text.Trim();
                _oficioActual.Copias = txtCcp.Text.Trim();

                if (cmbFirmas.SelectedValue != null)
                {
                    _oficioActual.FirmanteId = (int)cmbFirmas.SelectedValue;
                }

                if (esRegistroNuevo)
                {
                    _context.OficioRespuesta.Add(_oficioActual);
                }
                else
                {
                    _context.OficioRespuesta.Update(_oficioActual);
                }

                _context.SaveChanges();

                MessageBox.Show("El oficio se ha guardado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar en la base de datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnWord_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;


                string textoCombo = cmbFirmas.Text;

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
                    NumeroOficio = txtNof.Text,
                    Asunto = txtAsunto.Text,
                    OficioReferencia = "",
                    Destinatario = txtDest.Text,
                    CargoDestinatario = txtCrgo.Text,
                    FundamentoLegal = ObtenerFundamentoLegal(cargoExtraido),
                    Cuerpo = txtRespC.Text,
                    Copias = txtCcp.Text,
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
            /*   catch (Exception ex)
               {
                   MessageBox.Show($"Ocurrió un error al previsualizar el oficio:\n\n{ex.Message}",
                                   "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
               }*/

            catch (Exception ex)
            {
                // 1. Excavamos hasta encontrar la excepción original (el error real)
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                {
                    errorReal = errorReal.InnerException;
                }

                // 2. Armamos un mensaje detallado con el error y DÓNDE ocurrió
                string mensajeError = $"Ocurrió un error al previsualizar el oficio.\n\n" +
                                      $"❌ MOTIVO EXACTO:\n{errorReal.Message}\n\n" +
                                      $"📍 UBICACIÓN DEL ERROR (StackTrace):\n{errorReal.StackTrace}";

                // 3. Mostramos el mensaje (usamos MessageBox con texto expandido)
                MessageBox.Show(mensajeError, "Error Detallado de Word Interop", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }

        }

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

        private string ObtenerCopiasDefault()
        {
            return "C.C.P. Lic. Andrés Augusto Rosaldo García. - Oficial Mayor de la SSP. - Para su superior conocimiento. – Presente.";
        }

        /*
        private void GuardarOficio()
        {
   
          //  bool esNuevo = _oficioconse.OficioId == 0;

            int? firmanteSeleccionado = cmbFirmas.SelectedValue as int?;


            var registroRespuesta = new OficioRespuesta
            {

                TicketId = 0,
                NumeroOficio = txtNof.Text,
                OficioReferencia = "",
                Asunto = txtAsunto.Text,
                Destinatario = txtDest.Text,
                CargoDestinatario = txtCrgo.Text,
                CuerpoRespuesta = txtRespC.Text,
                Copias = txtCcp.Text,
                FechaOficio = DateTime.Now,
                FechaCaptura = DateTime.Now,
                Anio = (short)DateTime.Now.Year,
               
                FirmanteId = firmanteSeleccionado,
                OficioId =0,
                RespuestaId =0,
             //   OficioId = registroOficio1?.OficioId,
             //   RespuestaId = nuevoNumeroCreado.OficioId,
            };


            _context.OficioRespuesta.Add(registroRespuesta);
            _context.SaveChanges();


            MessageBox.Show("Oficio generado.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();

        }
        */

        private void CargarResp(OficioRespuesta oficio)
        {
            _oficioActual = oficio ?? new OficioRespuesta();

            txtNof.Text = _oficioActual.NumeroOficio ?? string.Empty;       
            txtAsunto.Text = _oficioActual.Asunto ?? string.Empty;
            txtDest.Text = _oficioActual.Destinatario ?? string.Empty;
            txtCrgo.Text = _oficioActual.CargoDestinatario ?? string.Empty;
            txtRespC.Text = _oficioActual.CuerpoRespuesta ?? string.Empty;
            txtCcp.Text = _oficioActual.Copias ?? string.Empty;

            if (_oficioActual.FirmanteId != null)
            {
                cmbFirmas.SelectedValue = _oficioActual.FirmanteId;
            }
            else
            {
                cmbFirmas.SelectedIndex = -1;
            }
        }
    }
}
