using DocumentFormat.OpenXml.InkML;
using OficiosTI;
using OficiosTI.Data;
using OficiosTI.Data.Entities;
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

        public FrmOficioSin(OficiosContext context)
        {
            InitializeComponent();
            _context = context;
            InicializarGrid();
            dataGridOficios.CellDoubleClick += dataGridOficios_CellDoubleClick;
        }

        private void InicializarGrid()
        {
            dataGridOficios.AutoGenerateColumns = true;

            if (_context != null)
            {
                var historialOficios = _context.OficioRespuesta
                    .OrderByDescending(o => o.OficioRespuestaId)
                    .Select(o => new
                    {
                        OficioRespuestaId = o.OficioRespuestaId,
                        No_Oficio = o.NumeroOficio,
                        Asunto = o.Asunto,
                        Destinatario = o.Destinatario,
                        Cargo = o.CargoDestinatario,
                        Respuesta = o.CuerpoRespuesta,
                        Fecha = o.FechaOficio,
                        Numticket = o.TicketId
                    })
                    .ToList();

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
                InicializarGrid();
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
                    InicializarGrid();
                }
            }
        }
    }

 }

