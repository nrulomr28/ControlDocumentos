using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OficiosTI
{
    public partial class FrmCrearTicket : Form
    {
        private OficiosContext _context;
        private OficioRespuesta _oficioActual;
        private OficioRespuestaService _service;
        private UsuarioContexto _usuarioActual;
        public int IdOficioVinculado { get; set; }
        public string AsuntoPredefinido { get; set; }
    //  public int RemitentePredefinido { get; set; }
        public string Instruccion { get; set; }
        public FrmCrearTicket(OficiosContext context, UsuarioContexto usuarioActual, OficioRespuesta OficioResp = null)
        {
            InitializeComponent();
            _context = context;
            _usuarioActual = usuarioActual;
            _oficioActual = OficioResp;
            _service = new OficioRespuestaService(_context);
            this.Text = $"Captura de Tickets - {_usuarioActual.NombreMostrar} ({_usuarioActual.UnidadOrganizativa})";
            CargarPrioridad();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);           
            var Ofinum = _context.CapturaOficios.FirstOrDefault(y => y.CapturaOficioId == this.IdOficioVinculado);
            int idRemitente = Ofinum?.Remitente ?? 0;
            var remitente = _context.Remitente.FirstOrDefault(y => y.RemitenteId == idRemitente);
            string nombreRemitente = remitente?.RemitenteNombre ?? "";
            if (!string.IsNullOrEmpty(AsuntoPredefinido))
            {
                txtAsunto.Text = AsuntoPredefinido;
            }
            txtNombre.Text = nombreRemitente;

            if (!string.IsNullOrEmpty(Instruccion))
            {
                textInstruccion.Text = Instruccion;
            }
        }

        private void CargarPrioridad()
        {
            var listaPrioridad = new[]
            {
             new { Id =  "1",  Nombre = "NORMAL" },
             new { Id =  "2", Nombre = "URGENTE" },
            }.ToList();
            cmbPrioridad.DataSource = listaPrioridad;
            cmbPrioridad.DisplayMember = "Nombre";
            cmbPrioridad.ValueMember = "Id";
            cmbPrioridad.SelectedIndex = -1;
            cmbPrioridad.DropDownStyle = ComboBoxStyle.DropDown;
            cmbPrioridad.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbPrioridad.AutoCompleteSource = AutoCompleteSource.ListItems;
            cmbPrioridad.SelectedIndex = -1;
        }

        private void btnCrearTicket_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Falta el nombre de quien genera el ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtAsunto.Text))
            {
                MessageBox.Show("Falta el asunto del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAsunto.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(cmbPrioridad.Text))
            {
                MessageBox.Show("Falta la prioridad del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbPrioridad.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(textInstruccion.Text))
            {
                MessageBox.Show("Falta el mensaje del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textInstruccion.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtExtension.Text))
            {
                MessageBox.Show("Falta la extensión del usuario", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textInstruccion.Focus();
                return;
            }                       

            List<int> oficinasSeleccionadas = new List<int>();
            if (chcOficina1.Checked) oficinasSeleccionadas.Add(1); // REDES
            if (chcOficina2.Checked) oficinasSeleccionadas.Add(2); // CONTROL Y RESGUARDO
            if (chcOficina3.Checked) oficinasSeleccionadas.Add(3); // DESARROLLO
            if (chcOficina4.Checked) oficinasSeleccionadas.Add(4); // GESTION ADMINISTRATIVA
            if (chcOficina5.Checked) oficinasSeleccionadas.Add(5); // JEFATURA
            if (chcOficina6.Checked) oficinasSeleccionadas.Add(6); // DISEÑO

            if (oficinasSeleccionadas.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos una oficina a la que va dirigido el ticket.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {                        
                        var Ofinum = _context.CapturaOficios.FirstOrDefault(y => y.CapturaOficioId == this.IdOficioVinculado);
                        int idRemitente = Ofinum?.Remitente ?? 0;
                        var Remitente = _context.Remitente.FirstOrDefault(y => y.RemitenteId == idRemitente);
                        string nombrePersona = !string.IsNullOrWhiteSpace(txtNombre.Text)
                                               ? txtNombre.Text.Trim()
                                               : (Remitente?.RemitenteNombre ?? "");
                        string usuariodominio =  _service.ObtenerDominioYUsuario();                         
                        int idusuario = _context.Usuarios
                                                        .Where(y => y.UsuarioDominio == usuariodominio)  /// USUARIO QUE CAPTURA EL TICKET 
                                                        .Select(y => y.UsuarioAnterior)
                                                        .FirstOrDefault();
                        foreach (int idOficina in oficinasSeleccionadas)
                        {
                            int idJefeOficina = _context.Usuarios
                                                        .Where(y => y.IdOficina == idOficina && y.Jefe == 1)    /// JEFE DE OFICINA 
                                                        .Select(y => y.UsuarioAnterior)
                                                        .FirstOrDefault();
                            Ticket nuevoticket = new Ticket
                            {
                                TicketPersona = nombrePersona,
                                TicketTel = txtTel.Text.Trim(),
                                TicketAsunto = txtAsunto.Text.Trim(), 
                                TicketMensaje = textInstruccion.Text.Trim(),
                                TicketPrioridad = cmbPrioridad.Text.Trim(),                               
                                TicketFecha = DateTime.Now,
                                Cat_TicketStatusId = 1, // ESTADO CREADO
                                OficinasId = idOficina,
                                id_of = this.IdOficioVinculado,
                                TicketUUsuario = idJefeOficina,   /// se asigna al jefe 
                                TicketExt = txtExtension.Text
                              //  TicketExt = int.Parse(txtExtension.Text),
                            };
                            _context.Ticket.Add(nuevoticket);
                            _context.SaveChanges();                                                
                            var hilo = new HiloTicket
                            {
                                TicketId = nuevoticket.TicketId,
                                HiloTicketFecha = DateTime.Now,
                                HiloTicketAccion = "CREADO",
                                HiloTicketMensaje = textInstruccion.Text.Trim(),
                                UsuarioId = idusuario,   /// quien capturo el ticket 
                            };
                            _context.HiloTicket.Add(hilo);
                            _context.SaveChanges();                            
                            var hilo2 = new HiloTicket
                            {
                                TicketId = nuevoticket.TicketId,    /// ASIGNADO AL DEPARTAMENTO 
                                HiloTicketFecha = DateTime.Now,
                                HiloTicketAccion =  "ASIGNADO",
                                HiloTicketMensaje = "ASIGNADO AUTOMATICAMENTE",
                                UsuarioId = idJefeOficina       //// ID DEL JEFE 
                            };
                            _context.HiloTicket.Add(hilo2);
                            _context.SaveChanges();
                        }
                        transaction.Commit();
                        MessageBox.Show($"Se crearon {oficinasSeleccionadas.Count} ticket(s) correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);                      
                        this.DialogResult = DialogResult.OK;
                        Close();
                    }
                    catch (Exception exBD)
                    {
                        transaction.Rollback();
                        throw exBD;
                    }
                }
            }
            catch (Exception exSeg)
            {
                string errorReal = exSeg.InnerException?.Message ?? exSeg.Message;
                MessageBox.Show("Hubo un error al registrar en la base de datos. Ningún ticket fue guardado.\n\nMotivo: " + errorReal,
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

     /* private void btnCrearTicket_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Falta el nombre de quien genera el ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           

            if (string.IsNullOrWhiteSpace(txtAsunto.Text))
            {
                MessageBox.Show("Falta el asunto del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(cmbPrioridad.Text))
            {
                MessageBox.Show("Falta la prioridad del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(textInstruccion.Text))
            {
                MessageBox.Show("Falta el mensaje del ticket", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
       
            List<int> oficinasSeleccionadas = new List<int>();            
            if (chcOficina1.Checked) oficinasSeleccionadas.Add(1); // REDES
            if (chcOficina2.Checked) oficinasSeleccionadas.Add(2); // CONTROL Y RESGUARDO
            if (chcOficina3.Checked) oficinasSeleccionadas.Add(3); // DESARROLLO
            if (chcOficina4.Checked) oficinasSeleccionadas.Add(4); // GESTION ADMINISTRATIVA
            if (chcOficina5.Checked) oficinasSeleccionadas.Add(5); // JEFATURA
            if (chcOficina6.Checked) oficinasSeleccionadas.Add(6); // DISEÑO

            if (oficinasSeleccionadas.Count == 0)
            {
                MessageBox.Show("Por favor, seleccione al menos una oficina a la que va dirigido el ticket.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        var Ofinum = _context.CapturaOficios.FirstOrDefault(y => y.CapturaOficioId == this.IdOficioVinculado);
                        int idRemitente = Ofinum?.Remitente ?? 0;
                        var Remitente =_context.Remitente.FirstOrDefault(y => y.RemitenteId == idRemitente);
                        string nombreRemitente = Remitente.RemitenteNombre ?? null; 

                        foreach (int idOficina in oficinasSeleccionadas)
                        {
                            Ticket nuevoticket = new Ticket
                            {
                              //TicketPersona = txtNombre.Text,
                                TicketPersona = nombreRemitente,   // crear ticket 
                                TicketTel = txtTel.Text,
                                TicketAsunto = txtAsunto.Text,
                                TicketMensaje = textInstruccion.Text,
                                TicketPrioridad = cmbPrioridad.Text,
                                TicketFecha = DateTime.Now,
                                Cat_TicketStatusId = 1, /// ESTADO CREADO
                                TicketUUsuario = 0,
                                OficinasId = idOficina,
                                id_of = this.IdOficioVinculado,
                            };
                            _context.Ticket.Add(nuevoticket);
                            _context.SaveChanges();
                            var hilo = new HiloTicket
                            {
                                TicketId = nuevoticket.TicketId,
                                HiloTicketFecha = DateTime.Now,
                                HiloTicketAccion = "CREADO",
                                HiloTicketMensaje = textInstruccion.Text,
                                UsuarioId = 0,
                            };
                            _context.HiloTicket.Add(hilo);
                            _context.SaveChanges();

                         
                            int idJefeOficina = _context.Usuarios
                                                        .Where(y => y.IdOficina == idOficina && y.Jefe == 1)
                                                        .Select(y => y.UsuarioId)
                                                        .FirstOrDefault();

                           
                            var hilo2 = new HiloTicket
                            {
                                TicketId = nuevoticket.TicketId,
                                HiloTicketFecha = DateTime.Now,
                                HiloTicketAccion = "ASIGNADO",
                                HiloTicketMensaje = "ASIGNADO AUTOMATICAMENTE",
                                UsuarioId = idJefeOficina 
                            };                          

                            _context.HiloTicket.Add(hilo2);
                            _context.SaveChanges();
                        }

                        transaction.Commit();
                        MessageBox.Show($"Se crearon {oficinasSeleccionadas.Count} ticket(s) correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                    catch (Exception exBD)
                    {
                        transaction.Rollback();

                        throw exBD;
                    }
                }
            }
            catch (Exception exSeg)
            {
                string errorReal = exSeg.InnerException?.Message ?? exSeg.Message;
                MessageBox.Show("Hubo un error al registrar en la base de datos. Ningún ticket fue guardado.\n\nMotivo: " + errorReal,
                                "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
     */

    }
}





