using OficiosTI.Data;
using OficiosTI.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OficiosTI
{
    public partial class FrmAdjuntos : Form
    {
        private readonly int _ticketId;
        private readonly OficiosContext _context;

        public FrmAdjuntos(int ticketId, OficiosContext context)
        {
            InitializeComponent();

            _ticketId = ticketId;
            _context = context;
            
            dataGridArchivos.CellMouseEnter += (s, e) =>
            {
                if (e.RowIndex >= 0)
                    dataGridArchivos.Cursor = Cursors.Hand;
            };

            CargarArchivos();
            dataGridArchivos.DataBindingComplete += DataGridArchivos_DataBindingComplete;
        }

        private void DataGridArchivos_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            if (dataGridArchivos.Columns.Count == 0)
                return;

            if (dataGridArchivos.Columns["TicketArchivoId"] != null)
                dataGridArchivos.Columns["TicketArchivoId"].Visible = false;

            if (dataGridArchivos.Columns["TipoMime"] != null)
                dataGridArchivos.Columns["TipoMime"].Visible = false;

            if (dataGridArchivos.Columns["NombreArchivo"] != null)
                dataGridArchivos.Columns["NombreArchivo"].HeaderText = "Archivo";

            if (dataGridArchivos.Columns["Extension"] != null)
            {
                dataGridArchivos.Columns["Extension"].HeaderText = "Tipo";
                dataGridArchivos.Columns["Extension"].Width = 80;
            }

            if (dataGridArchivos.Columns["FechaCarga"] != null)
            {
                dataGridArchivos.Columns["FechaCarga"].HeaderText = "Fecha";
                dataGridArchivos.Columns["FechaCarga"].Width = 150;
                dataGridArchivos.Columns["FechaCarga"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";
            }
        }

        private void CargarArchivos()
        {
            var lista = _context.TicketArchivo
                .Where(x => x.TicketId == _ticketId)
                .Select(x => new
                {
                    x.TicketArchivoId,
                    x.NombreArchivo,
                    x.Extension,
                    x.TipoMime,
                    x.FechaCarga
                })
                .OrderByDescending(x => x.FechaCarga)
                .ToList();

            dataGridArchivos.DataSource = lista;
        }

        private void BtnSubir_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog();

            if (ofd.ShowDialog() != DialogResult.OK)
                return;

            byte[] bytes = File.ReadAllBytes(ofd.FileName);

            var archivo = new TicketArchivo
            {
                TicketId = _ticketId,
                NombreArchivo = Path.GetFileName(ofd.FileName),
                Extension = Path.GetExtension(ofd.FileName),
                TipoMime = ObtenerMime(ofd.FileName),
                Archivo = bytes, 
                FechaCarga = DateTime.Now
            };

            _context.TicketArchivo.Add(archivo);
            _context.SaveChanges();

            CargarArchivos();
        }

        private void dataGridArchivos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            int id = (int)dataGridArchivos.Rows[e.RowIndex].Cells["TicketArchivoId"].Value;

            var archivo = _context.TicketArchivo.Find(id);

            if (archivo == null)
                return;

            string tempPath = Path.Combine(Path.GetTempPath(), archivo.NombreArchivo);

            File.WriteAllBytes(tempPath, archivo.Archivo);

            Process.Start(new ProcessStartInfo
            {
                FileName = tempPath,
                UseShellExecute = true
            });
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dataGridArchivos.CurrentRow == null)
                return;

            int id = (int)dataGridArchivos.CurrentRow.Cells["TicketArchivoId"].Value;

            var archivo = _context.TicketArchivo.Find(id);

            if (archivo == null)
                return;

            var r = MessageBox.Show("¿Eliminar archivo?", "Confirmar", MessageBoxButtons.YesNo);

            if (r == DialogResult.No)
                return;

            _context.TicketArchivo.Remove(archivo);
            _context.SaveChanges();

            CargarArchivos();
        }

        private string ObtenerMime(string file)
        {
            string ext = Path.GetExtension(file).ToLower();

            return ext switch
            {
                ".pdf" => "application/pdf",
                ".docx" => "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                ".xlsx" => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                ".jpg" => "image/jpeg",
                ".png" => "image/png",
                _ => "application/octet-stream"
            };
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
