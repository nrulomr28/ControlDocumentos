using DocumentFormat.OpenXml.InkML;
using OficiosTI;
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
    public partial class FrmOficioSin : Form
    {
        private OficiosContext _context;
        int oficina = 3;
      
        /*public FrmOficioSin(OficiosContext context)
        {         
            InitializeComponent();
            _context = context;
            InicializarGrid(oficina);
            dataGridOficios.CellDoubleClick += dataGridOficios_CellDoubleClick;
            btnCrearOficio.Enabled = false;
        }*/
        public FrmOficioSin(OficiosContext context)
        {
            InitializeComponent();

            _context = context;

            var service = new OficioRespuestaService(_context);
         
            int miOficinaId = service.ObtenerUnidadOrgId(service.ObtenerUnidadOrganizativa());

            InicializarGrid(miOficinaId);

            dataGridOficios.CellDoubleClick += dataGridOficios_CellDoubleClick;
            btnCrearOficio.Enabled = false;
        }
        private void InicializarGrid(int idOficinaDeseada)
        {
            dataGridOficios.AutoGenerateColumns = true;

            if (_context != null)
            {
                var historialOficios = (from resp in _context.OficioRespuesta
                                        join num in _context.NumOficio
                                        on resp.RespuestaId equals num.OficioId
                                        where num.Oficinas_Id == idOficinaDeseada
                                        where resp.TicketId == 0 || resp.TicketId == null
                                        orderby resp.OficioRespuestaId descending
                                        select new
                                        {
                                            OficioRespuestaId = resp.OficioRespuestaId,
                                            No_Oficio = resp.NumeroOficio,
                                            Asunto = resp.Asunto,
                                            Destinatario = resp.Destinatario,
                                            Cargo = resp.CargoDestinatario,
                                            Respuesta = resp.CuerpoRespuesta,
                                            Fecha = resp.FechaOficio,
                                            Numticket = resp.TicketId
                                        }).ToList();

                 dataGridOficios.DataSource = historialOficios;

                if (dataGridOficios.Columns.Contains("OficioRespuestaId"))
                {
                    dataGridOficios.Columns["OficioRespuestaId"].Visible = false;
                }
            }
        }
     
        private void btnCrearOficio_Click(object sender, EventArgs e)
        {
            FrmOficioNuevo frmNuevo = new FrmOficioNuevo(_context, null);

            if (frmNuevo.ShowDialog() == DialogResult.OK)
            {
                InicializarGrid(oficina);
            }
        }

        private void dataGridOficios_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            int oficioRespuestaId = Convert.ToInt32(dataGridOficios.Rows[e.RowIndex].Cells["OficioRespuestaId"].Value);
            var oficioSeleccionado = _context.OficioRespuesta.Find(oficioRespuestaId);
            if (oficioSeleccionado != null)
            {
                FrmOficioNuevo frmDetalle = new FrmOficioNuevo(_context, oficioSeleccionado);
                if (frmDetalle.ShowDialog() == DialogResult.OK)
                {
                    InicializarGrid(oficina);
                }
            }
        }
    }

 }

