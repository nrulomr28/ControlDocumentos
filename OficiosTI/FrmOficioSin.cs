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
            InicializarGrid(1);
            dataGridOficios.CellDoubleClick += dataGridOficios_CellDoubleClick;
            btnCrearOficio.Enabled = false;
        }

        // Agregamos el parámetro para saber qué oficina queremos cargar
        private void InicializarGrid(int idOficinaDeseada)
        {
            dataGridOficios.AutoGenerateColumns = true;

            if (_context != null)
            {
                // Usamos la misma sintaxis de JOIN que ya dominas
                var historialOficios = (from resp in _context.OficioRespuesta
                                        join num in _context.NumOficio
                                        on resp.RespuestaId equals num.OficioId

                                        // 1. Filtramos por la oficina en la tabla NumOficio
                                        where num.Oficinas_Id == idOficinaDeseada

                                        // 2. Mantenemos tu filtro para oficios independientes (sin ticket)
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

                // Ocultamos la columna del ID interno para que la tabla se vea limpia
                if (dataGridOficios.Columns.Contains("OficioRespuestaId"))
                {
                    dataGridOficios.Columns["OficioRespuestaId"].Visible = false;
                }
            }
        }
        /*
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
                    .Where(o =>o.Numticket==0)
                  
                    .ToList();
                dataGridOficios.DataSource = historialOficios;
                 if (dataGridOficios.Columns.Contains("OficioRespuestaId"))
                {
                    dataGridOficios.Columns["OficioRespuestaId"].Visible = false;
                }
            }
        }
        */

        private void btnCrearOficio_Click(object sender, EventArgs e)
        {
            FrmOficioNuevo frmNuevo = new FrmOficioNuevo(_context, null);

            if (frmNuevo.ShowDialog() == DialogResult.OK)
            {
                InicializarGrid(1);
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
                    InicializarGrid(1);
                }
            }
        }
    }

 }

