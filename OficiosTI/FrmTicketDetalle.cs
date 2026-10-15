using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.ApplicationServices;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Services;
using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmTicketDetalle : Form
    {
        private readonly Ticket _ticket;
        private readonly OficiosContext _context;
        private readonly OficioRespuestaService _service;

        public FrmTicketDetalle(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();
            _ticket = ticket;
            _context = context;
            _service = new OficioRespuestaService(_context);
            this.Font = new Font("Gotham", 10F, FontStyle.Regular, GraphicsUnit.Point);
            this.Load += FrmTicketDetalle_Load;
        }

        private void FrmTicketDetalle_Load(object sender, EventArgs e)
        {
            CargarDatosTicket();
            CargarHilo();
            CargarComboAnalista();
            CargarTicketsR();
        }

        private void CargarDatosTicket()
        {
            lblTicketId.Text = $"Ticket #{_ticket.TicketId}";
            txtPersona.Text = _ticket.TicketPersona;
            txtAsunto.Text = _ticket.TicketMensaje;
            txtMensaje.Text = _ticket.TicketAsunto;
            txtExtension.Text = _ticket.TicketExt?.ToString() ?? "";
            txtPrioridad.Text = _ticket.TicketPrioridad;
        }

        private void CargarComboAnalista()
        {
            try
            {
                int miOficinaId = _service.ObtenerUnidadOrgId(_service.ObtenerUnidadOrganizativa());
                string usuariodominio = _service.ObtenerDominioYUsuario();
                int idusuario = _context.Usuarios
                                                        .Where(y => y.UsuarioDominio == usuariodominio)  /// USUARIO QUE CAPTURA EL TICKET 
                                                        .Select(y => y.UsuarioAnterior)
                                                        .FirstOrDefault();

                var status = _context.Usuarios
                    .Where(x => x.IdOficina == miOficinaId && x.Activo == 1 && x.UsuarioAnterior != idusuario)
                    .ToList();

                cmbAnalistas.DisplayMember = "NombreCompleto";
                cmbAnalistas.ValueMember = "UsuarioAnterior";
                cmbAnalistas.DataSource = status;
                cmbAnalistas.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los analistas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarHilo()
        {
            try
            {
                var hilo = (from h in _context.HiloTicket
                            where h.TicketId == _ticket.TicketId
                            join u in _context.Usuarios on h.UsuarioId equals u.UsuarioAnterior into usuarios
                            from usr in usuarios.DefaultIfEmpty()
                            orderby h.HiloTicketFecha
                            select new
                            {
                                Fecha = h.HiloTicketFecha,
                                Accion = h.HiloTicketAccion,
                                Mensaje = h.HiloTicketMensaje,
                                Usuario = usr != null ? (usr.Nombre + " " + usr.Paterno + " " + usr.Materno).Trim() : "--"
                            }).ToList();

                dataGridHilo.DataSource = hilo;
                dataGridHilo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar el historial del ticket: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void CargarTicketsR()
        {
            try
            {
                if (_ticket?.id_of == null || _ticket.id_of == 0)
                {
                    dataGridRelacion.DataSource = null;
                    return;
                }

                var ticketsRelacionados = (from t in _context.Ticket
                                           join o in _context.Oficinas on t.OficinasId equals o.OficinasId
                                           join s in _context.Cat_TicketStatus on t.Cat_TicketStatusId equals s.Cat_TicketStatusId
                                           join x in _context.OficioRespuesta on t.TicketId equals x.TicketId into respuestas
                                           from r in respuestas.DefaultIfEmpty()
                                           where t.id_of == _ticket.id_of && t.TicketId != _ticket.TicketId
                                           orderby t.TicketId
                                           select new
                                           {
                                               Ticket = t.TicketId,
                                               //  Remitente = t.TicketPersona,
                                               //    Asunto = t.TicketMensaje,
                                               Oficina = o.OficinasNombre,
                                               Estado = s.Cat_TicketStatusStatus,
                                               Oficio = r != null ? r.NumeroOficio : "SIN OFICIO",
                                           }).ToList();

                dataGridRelacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridRelacion.DataSource = ticketsRelacionados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los tickets relacionados: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAsignarTicket_Click(object sender, EventArgs e)
        {
            if (_ticket.Cat_TicketStatusId == 3)
            {
                MessageBox.Show("Este ticket ya se encuentra cerrado. No se puede asignar.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int? idanalista = null;

            if (cmbAnalistas.SelectedValue != null && int.TryParse(cmbAnalistas.SelectedValue.ToString(), out int idParsed))
            {
                idanalista = idParsed;
            }

            if (idanalista == null)
            {
                MessageBox.Show("Por favor, selecciona un analista.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtInstruccion.Text))
            {
                MessageBox.Show("Por favor, agregue una indicación.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var hilo = new HiloTicket
            {
                TicketId = _ticket.TicketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = "ASIGNADO",
                HiloTicketMensaje = txtInstruccion.Text.Trim(),
                UsuarioId = idanalista
            };

            try
            {
                _context.HiloTicket.Add(hilo);
                _ticket.TicketUUsuario = idanalista.Value;   /// asignar al analista
                _ticket.Cat_TicketStatusId = 2;              //// cambiar el estado del ticket a en proceso   
                _context.Ticket.Update(_ticket);
                _context.SaveChanges();
                txtInstruccion.Clear();
                cmbAnalistas.SelectedIndex = -1;
                CargarHilo();

                MessageBox.Show("Ticket asignado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al asignar el ticket: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       
        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            try
            {
                var archivoBd = _context.Archivo.FirstOrDefault(p => p.OficioId == _ticket.id_of);

                if (archivoBd == null || archivoBd.ArchivoArchivo == null || archivoBd.ArchivoArchivo.Length == 0)
                {
                    MessageBox.Show("El documento no existe o está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string nombreArchivo = $"Oficio_{archivoBd.ArchivoId}.pdf";
                string rutaTemporal = System.IO.Path.Combine(System.IO.Path.GetTempPath(), nombreArchivo);
                System.IO.File.WriteAllBytes(rutaTemporal, archivoBd.ArchivoArchivo);
                var psi = new System.Diagnostics.ProcessStartInfo
                {
                    FileName = rutaTemporal,
                    UseShellExecute = true
                };
                System.Diagnostics.Process.Start(psi);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al abrir el PDF: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


       
        private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
        {
            bool cerrarYsinOficio = chkCerrarTicket.Checked;
            ProcesarRespuesta(cerrarTicket: cerrarYsinOficio, esSinOficio: cerrarYsinOficio);
        }

   
        private void btnGuardaryCerrar_Click(object sender, EventArgs e)
        {
          
            ProcesarRespuesta(cerrarTicket: true, esSinOficio: false);
        }

     
        private void ProcesarRespuesta(bool cerrarTicket, bool esSinOficio)
        {
            if (_ticket.Cat_TicketStatusId == 3)
            {
                MessageBox.Show("Este ticket ya se encuentra cerrado. No se pueden agregar más respuestas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
            {
                MessageBox.Show("Por favor, escribe una respuesta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.Enabled = false;

            try
            {
                string usuariodominio = _service.ObtenerDominioYUsuario();
                int idanalista = _context.Usuarios
                    .Where(y => y.UsuarioDominio == usuariodominio)
                    .Select(y => y.UsuarioAnterior)
                    .FirstOrDefault();

                var hilo = new HiloTicket
                {
                    TicketId = _ticket.TicketId,
                    HiloTicketFecha = DateTime.Now,
                    HiloTicketAccion = cerrarTicket ? "CERRADO" : "RESPUESTA",
                    HiloTicketMensaje = txtRespuesta.Text.Trim(),
                    UsuarioId = idanalista
                };

                _context.HiloTicket.Add(hilo);

               
                if (cerrarTicket)
                {
                   
                    if (esSinOficio)
                    {
                        var sinOficio = new Ticket_SinOficio
                        {
                            TicketId = _ticket.TicketId,
                            Cat_TicketStatusId = 3,
                            Cat_TicketStatusStatus = "CERRADO - SIN OFICIO",
                            Fecha = DateTime.Now,
                        };
                        _context.Add(sinOficio);
                    }

                 
                    _ticket.Cat_TicketStatusId = 3;
                    _context.Ticket.Update(_ticket);
                }

                _context.SaveChanges();

                if (cerrarTicket)
                {
                    MessageBox.Show("Respuesta guardada y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                else
                {
                    txtRespuesta.Clear();
                    chkCerrarTicket.Checked = false;
                    CargarHilo();
                    MessageBox.Show("Respuesta guardada.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ocurrió un error al guardar la respuesta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Enabled = true;
            }
        }


        /*  private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
          {
              if (_ticket.Cat_TicketStatusId == 3)
              {
                  MessageBox.Show("Este ticket ya se encuentra cerrado. No se pueden agregar más respuestas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

              if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
              {
                  MessageBox.Show("Por favor, escribe una respuesta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }
              string usuariodominio = _service.ObtenerDominioYUsuario();
              int idanalista = _context.Usuarios
                                                      .Where(y => y.UsuarioDominio == usuariodominio)  /// USUARIO QUE CAPTURA EL TICKET 
                                                      .Select(y => y.UsuarioAnterior)
                                                      .FirstOrDefault();

              bool cerrarTicket = chkCerrarTicket.Checked;


              var hilo = new HiloTicket
              {
                  TicketId = _ticket.TicketId,
                  HiloTicketFecha = DateTime.Now,
                  HiloTicketAccion = cerrarTicket ? "CERRADO" : "RESPUESTA",
                  HiloTicketMensaje = txtRespuesta.Text,
                  UsuarioId = idanalista
              };

              try
              {
                  _context.HiloTicket.Add(hilo);

                  if (cerrarTicket)
                  {
                      var sinOficio = new Ticket_SinOficio
                      {
                          TicketId = _ticket.TicketId,
                          Cat_TicketStatusId = 3,
                          Cat_TicketStatusStatus = "CERRADO - SIN OFICIO",
                          Fecha = DateTime.Now,
                      };
                      _context.Add(sinOficio);
                      _ticket.Cat_TicketStatusId = 3;
                      _context.Ticket.Update(_ticket);
                  }

                  _context.SaveChanges();

                  if (cerrarTicket)
                  {
                      MessageBox.Show("Respuesta guardada y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                      this.Close();
                  }
                  else
                  {
                      txtRespuesta.Clear();
                      CargarHilo();
                      MessageBox.Show("Respuesta guardada.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  }
              }
              catch (Exception ex)
              {
                  MessageBox.Show($"Ocurrió un error al guardar la respuesta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
          }

          private void btnGuardaryCerrar_Click(object sender, EventArgs e)
          {
              if (_ticket.Cat_TicketStatusId == 3)
              {
                  MessageBox.Show("Este ticket ya se encuentra cerrado. No se pueden agregar más respuestas.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

              if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
              {
                  MessageBox.Show("Por favor, escribe una respuesta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                  return;
              }

              btnGuardaryCerrar.Enabled = false;

              try
              {
                  string usuariodominio = _service.ObtenerDominioYUsuario();
                  int idanalista = _context.Usuarios
                      .Where(y => y.UsuarioDominio == usuariodominio)
                      .Select(y => y.UsuarioAnterior)
                      .FirstOrDefault();

                  var hilo = new HiloTicket
                  {
                      TicketId = _ticket.TicketId,
                      HiloTicketFecha = DateTime.Now,
                      HiloTicketAccion = "CERRADO",
                      HiloTicketMensaje = txtRespuesta.Text.Trim(),
                      UsuarioId = idanalista
                  };

                  _context.HiloTicket.Add(hilo);


                  _ticket.Cat_TicketStatusId = 3;
                  _context.Ticket.Update(_ticket);

                  _context.SaveChanges();

                  MessageBox.Show("Respuesta guardada y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                  this.Close();
              }
              catch (Exception ex)
              {
                  MessageBox.Show($"Ocurrió un error al guardar la respuesta: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
              }
              finally
              {
                  btnGuardaryCerrar.Enabled = true;
              }
          }
        */
    }
}