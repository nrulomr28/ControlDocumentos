using Microsoft.EntityFrameworkCore;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
using OficiosTI.Documents;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace OficiosTI
{
    public partial class FrmTicketDetalle : Form
    {
        private readonly Ticket _ticket;
        private readonly OficiosContext _context;
     // private readonly Oficinas _oficinas;

        public FrmTicketDetalle(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();
            _ticket = ticket;
            _context = context;
            CargarDatosTicket();
            CargarHilo();
            CargarTicketsR();
        }

        private void CargarDatosTicket()
        {
            lblTicketId.Text = $"Ticket #{_ticket.TicketId}";
            txtPersona.Text = _ticket.TicketPersona;
            txtAsunto.Text = _ticket.TicketMensaje;
            txtMensaje.Text = _ticket.TicketAsunto;
        }

        private void CargarHilo()
        {
            var hilo = _context.HiloTicket
                .Where(h => h.TicketId == _ticket.TicketId)
                .OrderBy(h => h.HiloTicketFecha)
                .ToList();
            dataGridHilo.DataSource = hilo;
        }
        private async Task CargarTicketsR()   ////// TICKETS RELACIONADOS 
        {
            try
            {
                if (_ticket?.id_of == null || _ticket.id_of == 0)
                {
                    dataGridRelacion.DataSource = null;
                    return;
                }
                var ticketsRelacionados = await (from t in _context.Ticket
                                                 join o in _context.Oficinas on t.OficinasId equals o.OficinasId
                                                 join s in _context.Cat_TicketStatus on t.Cat_TicketStatusId equals s.Cat_TicketStatusId
                                                 join x in _context.OficioRespuesta on t.TicketId equals x.TicketId into respuestas
                                                 from r in respuestas.DefaultIfEmpty()
                                                 where t.id_of == _ticket.id_of && t.TicketId != _ticket.TicketId
                                                 orderby t.TicketId
                                                 select new
                                                 {
                                                     Ticket = t.TicketId,
                                                     Remitente = t.TicketPersona,
                                                     Asunto = t.TicketMensaje,
                                                     Oficina = o.OficinasNombre,
                                                     Estado = s.Cat_TicketStatusStatus,
                                                     Oficio = r != null ? r.NumeroOficio : "SIN OFICIO",
                                                 }).ToListAsync();
                dataGridRelacion.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridRelacion.DataSource = ticketsRelacionados;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar los tickets relacionados: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     /*   private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
        {
            /// BUSCAR EL ESTADO DEL TICKET 
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
            bool cerrarTicket = chkCerrarTicket.Checked;   /// CHECKBOX SIN OFICIO ///
            var hilo = new HiloTicket
            {
                TicketId = _ticket.TicketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = cerrarTicket ? "CERRADO" : "RESPUESTA",
                HiloTicketMensaje = txtRespuesta.Text,
                UsuarioId = 0
            };

            _context.HiloTicket.Add(hilo);
            if (cerrarTicket)
            {
                var SinOficio = new Ticket_SinOficio
                {
                    TicketId = _ticket.TicketId,
                    Cat_TicketStatusId = 1,
                    Cat_TicketStatusStatus  ="TICKET SIN OFICIO", 
                    Fecha = DateTime.Now,
                }; 
                _ticket.Cat_TicketStatusId = 3;    //// CERRADO SIN OFICIO    
                _context.Ticket.Update(_ticket);
            }

            _context.SaveChanges();
            if (cerrarTicket)
            {
                MessageBox.Show("Respuesta guardada y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            else
            {
                txtRespuesta.Clear();
                CargarHilo();
                MessageBox.Show("Respuesta guardada.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }*/

        private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
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

            bool cerrarTicket = chkCerrarTicket.Checked;
            var hilo = new HiloTicket
            {
                TicketId = _ticket.TicketId,
                HiloTicketFecha = DateTime.Now,
                HiloTicketAccion = cerrarTicket ? "CERRADO" : "RESPUESTA",
                HiloTicketMensaje = txtRespuesta.Text,
                UsuarioId = 0 
            };

            try
            {
                _context.HiloTicket.Add(hilo);
                if (cerrarTicket)     //// GUARDAR EN LA TABLA DE TICKETS SIN OFICIO 
                {
                    var sinOficio = new Ticket_SinOficio
                    {
                        TicketId = _ticket.TicketId,
                        Cat_TicketStatusId = 3,
                        Cat_TicketStatusStatus = "CERRADO - SIN OFICIO",
                        Fecha = DateTime.Now,
                    };
                    _context.Add(sinOficio);
                    _ticket.Cat_TicketStatusId = 3; // ESTADO CERRADO EN LA TABLA DE TICKETS 
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

        /*     private void BtnGuardarRespuesta_Click(object sender, EventArgs e)
             {
                 // 1. Validar que la respuesta no esté vacía
                 if (string.IsNullOrWhiteSpace(txtRespuesta.Text))
                 {
                     MessageBox.Show("Por favor, escribe una respuesta.", "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                     return;
                 }

                 // 2. Preguntar al usuario qué acción desea realizar
                 DialogResult decision = MessageBox.Show(
                     "¿Deseas cerrar el ticket definitivamente con esta respuesta?\n\n" +
                     "SÍ = Guardar respuesta y CERRAR el ticket.\n" +
                     "NO = Guardar respuesta y mantener el ticket ABIERTO.",
                     "Opciones de Guardado",
                     MessageBoxButtons.YesNoCancel,
                     MessageBoxIcon.Question);

                 // 3. Si el usuario presiona Cancelar, detenemos el proceso
                 if (decision == DialogResult.Cancel)
                 {
                     return;
                 }

                 bool cerrarTicket = (decision == DialogResult.Yes);

                 // 4. Registrar la respuesta en el hilo
                 var hilo = new HiloTicket
                 {
                     TicketId = _ticket.TicketId,
                     HiloTicketFecha = DateTime.Now,
                     HiloTicketAccion = cerrarTicket ? "CERRADO" : "RESPUESTA",
                     HiloTicketMensaje = txtRespuesta.Text,
                     UsuarioId = 0 // Ajustar al ID de usuario en sesión
                 };

                 _context.HiloTicket.Add(hilo);

                 // 5. Si eligió cerrar, actualizamos el estatus del ticket a 3 (Cerrado)
                 if (cerrarTicket)
                 {
                     _ticket.Cat_TicketStatusId = 3;
                     _context.Ticket.Update(_ticket);
                 }

                 // 6. Guardar cambios en la base de datos
                 _context.SaveChanges();

                 // 7. Acciones finales dependiendo de la decisión del usuario
                 if (cerrarTicket)
                 {
                     MessageBox.Show("Respuesta guardada y ticket cerrado correctamente.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     Close(); // Cierra el formulario
                 }
                 else
                 {
                     txtRespuesta.Clear();
                     CargarHilo();
                     MessageBox.Show("Respuesta guardada. El ticket sigue abierto.", "Proceso Completado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                     // Al no llamar a Close(), el usuario puede seguir trabajando
                 }
             }
        */
        /// BOTON PARA VER EL OFICIO DE SOLICITUD  
        private void BtnGenerarOficio_Click(object sender, EventArgs e)
        {
            var archivoBd = _context.Archivo.FirstOrDefault(p => p.OficioId == _ticket.id_of);
            if (archivoBd == null || archivoBd.ArchivoArchivo == null || archivoBd.ArchivoArchivo.Length == 0)
            {
                MessageBox.Show("El documento no existe o está vacío.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
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


   
    }
}
