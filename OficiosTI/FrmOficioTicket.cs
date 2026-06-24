using DocumentFormat.OpenXml.InkML;
using Microsoft.EntityFrameworkCore;
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

namespace OficiosTI
{
    public partial class FrmOficioTicket : Form
    {
        private OficiosContext _context;
        private readonly Ticket _ticket;
        private OficioRespuestaService _service;
        private NumOficio _OficioRespuesta;   /// tabla de NumOficio

        /*  public FrmOficioTicket(Ticket ticket, OficiosContext context)
          {
              InitializeComponent();
              _context = context;
              _ticket = ticket;       
              CargarOficinas();
              CargarTiposD();
              CargarDatosTicket();

              _service = new OficioRespuestaService(_context);

              var tieneOficio = _context.NumOficio.Any(x => x.TicketId == _ticket.TicketId);

              AsignarF.Enabled = !tieneOficio;

              var oficioExistente = _context.OficioRespuesta
                                    .FirstOrDefault(x => x.TicketId == _ticket.TicketId);

              var OfSin = _context.NumOficio
                            .FirstOrDefault(x => x.TicketId == _ticket.TicketId);

              AsignarF.Enabled = !tieneOficio;

              if (tieneOficio)
              {
                  txtNumOf.Text = oficioExistente.NumeroOficio;          

                  txtNumOf.ReadOnly = true;

                  cmbOficinas.SelectedValue = OfSin.Oficinas_Id;



              }
          }*/

        /*       public FrmOficioTicket(Ticket ticket, OficiosContext context)
               {
                   InitializeComponent();
                   _context = context;
                   _ticket = ticket;
                   CargarOficinas();
                   CargarTiposD();
                   var tieneOficio = _context.NumOficio.Any(x => x.TicketId == _ticket.TicketId);

                   if (tieneOficio != null)
                   {
                       CargarDatosTicket();
                       var NuCons = _context.NumOficio
                  .Where(x => x.TicketId == _ticket.TicketId)
                  .FirstOrDefault();
                       AsignarF.Enabled = !tieneOficio;

                       if (tieneOficio)
                       {
                           txtNumOf.Text = NuCons.NumeroConsecutivo;
                           cmbOficinas.SelectedValue = Convert.ToInt32(NuCons.Oficinas_Id);
                           cmbTipos.SelectedValue = Convert.ToInt32(NuCons.Tipo);
                           txtNumOf.ReadOnly = true;
                       }

                   }
                   _service = new OficioRespuestaService(_context);



               }*/


        public FrmOficioTicket(Ticket ticket, OficiosContext context)
        {
            InitializeComponent();
            _context = context;
            _ticket = ticket;
            CargarOficinas();
            CargarTiposD();
            _service = new OficioRespuestaService(_context);

            if (_ticket != null)
            {
                CargarDatosTicket();

                var oficioExistente = _context.NumOficio.FirstOrDefault(x => x.TicketId == _ticket.TicketId);

                if (oficioExistente != null)
                {
                    AsignarF.Enabled = false; 
                    txtNumOf.Text = oficioExistente.NumeroConsecutivo;
                    cmbOficinas.SelectedValue = Convert.ToInt32(oficioExistente.Oficinas_Id);
                    cmbTipos.SelectedValue = Convert.ToInt32(oficioExistente.Tipo);
                    txtNumOf.ReadOnly = true;
                }
                else
                {
                    AsignarF.Enabled = true;
                    txtNumOf.ReadOnly = false;
                }
            }
            else
            {
                AsignarF.Enabled = true;
                txtNumOf.ReadOnly = false;
                txtNumOf.Clear();

                
            }
        }

        private void CargarOficinas()
        {
            var Oficinas = _context.Oficinas
                .Where(x => x.OficinasId == 1)     //// 1 REDES, 2 CONTROL Y REGUARDO DE LA INFORMACION, 3 DESARROLLO
                .ToList();
            cmbOficinas.DataSource = Oficinas;
            cmbOficinas.DisplayMember = "OficinasNombre";
            cmbOficinas.ValueMember = "OficinasId";
          //cmbOficinas.SelectedIndex = 2;
        }

        private void CargarTiposD()
        {
            var listaTipos = new[]
            {
             new { Id = 1, Nombre = "OFICIOS" },
             new { Id = 2, Nombre = "TARJETA" }
            }.ToList();

            cmbTipos.DataSource = listaTipos;
            cmbTipos.DisplayMember = "Nombre";
            cmbTipos.ValueMember = "Id";
            cmbTipos.SelectedIndex = 0;
        }

        private void CargarDatosTicket()
        {
            TicketTitulo.Text = $"Ticket #{_ticket.TicketId}";

        }
        private void txtNumOf_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void AsignarF_Click(object sender, EventArgs e)
        {
            if (cmbOficinas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una oficina.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!txtNumOf.Text.Trim().All(char.IsDigit))
            {
                MessageBox.Show("El campo solo acepta caracteres numéricos.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNumOf.Focus();
                return;
            }
            if (string.IsNullOrWhiteSpace(txtNumOf.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string conse = txtNumOf.Text.Trim();
            int anioActual = DateTime.Now.Year;
            if (_ticket != null)
            {
                bool yaTieneOficio = _context.NumOficio.Any(x => x.TicketId == _ticket.TicketId);
                if (yaTieneOficio)
                {
                    MessageBox.Show("Este ticket ya cuenta con un oficio asignado y no puede generar otro.",
                                    "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            var oficioExistente = _context.NumOficio.FirstOrDefault(x => x.NumeroConsecutivo == conse && x.Anio == anioActual);
            if (oficioExistente != null)
            {
                DialogResult respuesta = MessageBox.Show(
                    $"El número de oficio '{conse}' ya está asignado actualmente al Ticket ID: {oficioExistente.TicketId}.\n\n¿Desea continuar y usar este número de todos modos?",
                    "Número ya registrado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                int? idOficio = _ticket?.id_of;
                var registroOficio1 = idOficio.HasValue
                    ? _context.Oficio1.FirstOrDefault(x => x.OficioId == idOficio.Value)
                    : null;
                string bis = "";
                int oficinasId = Convert.ToInt32(cmbOficinas.SelectedValue);
                int tipo = Convert.ToInt32(cmbTipos.SelectedValue);
                int ticketId = _ticket != null ? _ticket.TicketId : 0;
                var nuevoNumeroCreado = _service.CrearNumero(conse, bis, oficinasId, tipo, anioActual, ticketId);
                var registroRespuesta = new OficioRespuesta
                {
                    TicketId = _ticket != null ? ticketId : 0,
                    NumeroOficio = $"SSP/OM/DTI/{nuevoNumeroCreado.NumeroConsecutivo}/{anioActual}",
                    Asunto = _ticket != null ? _ticket.TicketMensaje : string.Empty,
                    Destinatario = _ticket != null ? _ticket.TicketPersona : string.Empty,
                    CuerpoRespuesta = "",
                    Copias = "C.C.P. Lic. Andrés Augusto Rosaldo García. - Oficial Mayor de la SSP. - Para su superior conocimiento. – Presente.",
                    FechaOficio = DateTime.Now,
                    FechaCaptura = DateTime.Now,
                    Anio = (short)anioActual,
                    OficioReferencia = $"{registroOficio1?.OficioNoOficio}",
                 // OficioReferencia = registroOficio1.OficioNoControl ?? int.Empty,
                    FirmanteId = 0,
                    OficioId = registroOficio1?.OficioId,
                    RespuestaId = nuevoNumeroCreado.OficioId,
                };

                _context.OficioRespuesta.Add(registroRespuesta);
                _context.SaveChanges();

                MessageBox.Show("Guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                {
                    errorReal = errorReal.InnerException;
                }

                MessageBox.Show("Error de Base de Datos:\n\n" + errorReal.Message,
                                "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

       /* private void AsignarF_Click(object sender, EventArgs e)
        {
            bool yaTieneOficio = _context.NumOficio.Any(x => x.TicketId == _ticket.TicketId);
            if (yaTieneOficio)
            {
                MessageBox.Show("Este ticket ya cuenta con un oficio asignado y no puede generar otro.",
                                "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (cmbOficinas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una oficina.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumOf.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string conse = txtNumOf.Text.Trim(); 
            int anioActual = DateTime.Now.Year;

            var oficioExistente = _context.NumOficio.FirstOrDefault(x => x.NumeroConsecutivo == conse && x.Anio == anioActual);

            if (oficioExistente != null)
            {
                DialogResult respuesta = MessageBox.Show(
                    $"El número de oficio '{conse}' ya está asignado actualmente al Ticket ID: {oficioExistente.TicketId}.\n\n¿Desea continuar y usar este número de todos modos?",
                    "Número ya registrado",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (respuesta == DialogResult.No)
                {
                    return;
                }
            }

            try
            {
                var registroOficio1 = _context.Oficio1.FirstOrDefault(x => x.OficioId == _ticket.id_of);
                string bis = "";
                int oficinasId = Convert.ToInt32(cmbOficinas.SelectedValue);
                int tipo = Convert.ToInt32(cmbTipos.SelectedValue);
                int ticketId = _ticket.TicketId;
                var nuevoNumeroCreado = _service.CrearNumero(conse, bis, oficinasId, tipo, anioActual, ticketId);
                var registroRespuesta = new OficioRespuesta
                {
                    TicketId = ticketId,
                    NumeroOficio = $"SSP/OM/DTI/{nuevoNumeroCreado.NumeroConsecutivo}/{anioActual}",
                   
                    Asunto = _ticket.TicketMensaje,
                    Destinatario = _ticket.TicketPersona,
                    CuerpoRespuesta = "",
                    FechaOficio = DateTime.Now,
                    FechaCaptura = DateTime.Now,
                    Anio = (short)anioActual,
                    OficioReferencia = registroOficio1?.OficioNoControl ?? string.Empty,
                    FirmanteId = 0,
                    OficioId = registroOficio1?.OficioId,
                    RespuestaId = nuevoNumeroCreado.OficioId,
                };

                _context.OficioRespuesta.Add(registroRespuesta);
                _context.SaveChanges();
                MessageBox.Show("Guardado con éxito.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Close();
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                {
                    errorReal = errorReal.InnerException;
                }

                MessageBox.Show("Error de Base de Datos:\n\n" + errorReal.Message,
                                "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
       */

        /*
        private void AsignarF_Click(object sender, EventArgs e)
        {

            bool yaTieneOficio = _context.NumOficio.Any(x => x.TicketId == _ticket.TicketId);

            if (yaTieneOficio)
            {
                MessageBox.Show("Este ticket ya cuenta con un oficio asignado y no puede generar otro.",
                                "Acción no permitida", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return; 
            }

            var registroOficio1 = _context.Oficio1
            .FirstOrDefault(x => x.OficioId == _ticket.id_of);


            if (cmbOficinas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione una oficina.");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNumOf.Text))
            {
                MessageBox.Show("Debe capturar el número de oficio.");
                return;
            }

            string conse = txtNumOf.Text;
            int anioActual = DateTime.Now.Year;

            bool yaExiste = _context.NumOficio.Any(x => x.NumeroConsecutivo == conse && x.Anio == anioActual);


            if (yaExiste)
            {
                MessageBox.Show("El número de oficio '" + conse + "' ya ha sido registrado para este año al ticket.'"+ yaTieneOficio + "'",
                                "Oficio Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; 
            }

            try
            {
                registroOficio1 = _context.Oficio1.FirstOrDefault(x => x.OficioId == _ticket.id_of);            
                string numCon = txtNumOf.Text;
                string bis = "";
                int oficinasId = Convert.ToInt32(cmbOficinas.SelectedValue);
                int tipo = Convert.ToInt32(cmbTipos.SelectedValue);
                int anio = DateTime.Now.Year;
                int ticketId = _ticket.TicketId;

                var nuevoNumeroCreado = _service.CrearNumero(numCon, bis, oficinasId, tipo, anio, ticketId);

                var registroRespuesta = new OficioRespuesta
                {
                
                    TicketId = ticketId,
                    NumeroOficio = "SSP/OM/DTI/" + nuevoNumeroCreado.NumeroConsecutivo + "/"+ anio,
                    Asunto = _ticket.TicketMensaje,
                    Destinatario = _ticket.TicketPersona,
                    CuerpoRespuesta = "",
                    FechaOficio = DateTime.Now,
                    FechaCaptura = DateTime.Now,
                    Anio = (short)DateTime.Now.Year,                  
                    OficioReferencia = registroOficio1?.OficioNoControl ?? string.Empty,
                    FirmanteId = 0,
                    OficioId = registroOficio1?.OficioId, 
                    RespuestaId = nuevoNumeroCreado.OficioId,
                };       

                _context.OficioRespuesta.Add(registroRespuesta);
                _context.SaveChanges();

                MessageBox.Show("Guardado con éxito.");
                Close(); 
            }
            catch (Exception ex)
            {
                Exception errorReal = ex;
                while (errorReal.InnerException != null)
                {
                    errorReal = errorReal.InnerException;
                }

                MessageBox.Show("Error de Base de Datos:\n\n" + errorReal.Message,
                                "Error Detallado", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        */
   
    }
}


//private void AsignarF_Click(object sender, EventArgs e)
//{
//    if (cmbOficinas.SelectedIndex == -1)
//    {
//        MessageBox.Show("Por favor seleccione una oficina.");
//        return;
//    }
//    if (string.IsNullOrWhiteSpace(txtNumOf.Text))
//    {
//        MessageBox.Show("Debe capturar el número de oficio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
//        return;
//    }

//    try
//    {
//        string numCon = txtNumOf.Text;
//        string bis = "";
//        int oficinasId = Convert.ToInt32(cmbOficinas.SelectedValue);              
//        int tipo = Convert.ToInt32(cmbTipos.SelectedValue);
//        int anio = DateTime.Now.Year;
//        int TicketId = _ticket.TicketId;

//        var nuevoNumeroCreado = _service.CrearNumero(numCon, bis, oficinasId, tipo, anio, TicketId);

//        int idDelNumeroNuevo = nuevoNumeroCreado.OficioId; 

//        // Creamos el objeto para la OTRA tabla (ejemplo: RegistroAuditoria o Seguimiento)
//        var registroRespuesta = new OficioRespuesta
//        {
//            RespuestaId = idDelNumeroNuevo,                    
//            TicketId = TicketId
//        };

//        // Agregamos al contexto y guardamos
//        _context.OficioRespuesta.Add(registroRespuesta);
//        _context.SaveChanges();

//    );

//        MessageBox.Show("Guardado con éxito.");
//    }
//    catch (Exception ex)
//    {
//        Exception errorReal = ex;
//        while (errorReal.InnerException != null)
//        {
//            errorReal = errorReal.InnerException;
//        }

//        MessageBox.Show("Error de Base de Datos:\n\n" + errorReal.Message,
//                        "Error Detallado",
//                        MessageBoxButtons.OK,
//                        MessageBoxIcon.Error);
//    }
//    Close();
//}



