namespace OficiosTI
{
    partial class FrmAdjuntos
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitulo;

        private DataGridView dataGridArchivos;

        private Panel panelBotones;
        private Button btnSubir;
        private Button btnEliminar;
        private Button btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle headerStyle = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle cellStyle = new System.Windows.Forms.DataGridViewCellStyle();

            this.panelHeader = new Panel();
            this.lblTitulo = new Label();

            this.dataGridArchivos = new DataGridView();

            this.panelBotones = new Panel();
            this.btnSubir = new Button();
            this.btnEliminar = new Button();
            this.btnCerrar = new Button();

            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridArchivos)).BeginInit();
            this.panelBotones.SuspendLayout();
            this.SuspendLayout();

            // ================= HEADER =================
            this.panelHeader.BackColor = Color.FromArgb(0, 120, 215);
            this.panelHeader.Dock = DockStyle.Top;
            this.panelHeader.Height = 45;
            this.panelHeader.Controls.Add(this.lblTitulo);

            this.lblTitulo.AutoSize = true;
            this.lblTitulo.ForeColor = Color.White;
            this.lblTitulo.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            this.lblTitulo.Location = new Point(15, 12);
            this.lblTitulo.Text = "Adjuntos del Ticket";

            // ================= GRID =================
            this.dataGridArchivos.Dock = DockStyle.Fill;
            this.dataGridArchivos.BackgroundColor = Color.White;
            this.dataGridArchivos.BorderStyle = BorderStyle.FixedSingle;
            this.dataGridArchivos.ReadOnly = true;
            this.dataGridArchivos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dataGridArchivos.MultiSelect = false;
            this.dataGridArchivos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridArchivos.RowHeadersVisible = false;

            // Header style
            headerStyle.BackColor = Color.FromArgb(0, 120, 215);
            headerStyle.ForeColor = Color.White;
            headerStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            this.dataGridArchivos.ColumnHeadersDefaultCellStyle = headerStyle;
            this.dataGridArchivos.EnableHeadersVisualStyles = false;

            // Cell style
            cellStyle.SelectionBackColor = Color.FromArgb(200, 220, 240);
            cellStyle.SelectionForeColor = Color.Black;
            this.dataGridArchivos.DefaultCellStyle = cellStyle;

            // EVENTOS (sin lambdas)
            this.dataGridArchivos.CellDoubleClick += new DataGridViewCellEventHandler(this.dataGridArchivos_CellDoubleClick);

            // ================= BOTONES =================
            this.panelBotones.Dock = DockStyle.Bottom;
            this.panelBotones.Height = 55;
            this.panelBotones.BackColor = Color.WhiteSmoke;

            // Subir
            this.btnSubir.Text = "Subir";
            this.btnSubir.Size = new Size(100, 30);
            this.btnSubir.Location = new Point(20, 12);
            this.btnSubir.Click += new EventHandler(this.BtnSubir_Click);

            // Eliminar
            this.btnEliminar.Text = "Eliminar";
            this.btnEliminar.Size = new Size(100, 30);
            this.btnEliminar.Location = new Point(140, 12);
            this.btnEliminar.Click += new EventHandler(this.BtnEliminar_Click);

            // Cerrar (SIN lambda)
            this.btnCerrar.Text = "Cerrar";
            this.btnCerrar.Size = new Size(100, 30);
            this.btnCerrar.Location = new Point(260, 12);
            this.btnCerrar.Click += new EventHandler(this.BtnCerrar_Click);

            this.panelBotones.Controls.Add(this.btnSubir);
            this.panelBotones.Controls.Add(this.btnEliminar);
            this.panelBotones.Controls.Add(this.btnCerrar);

            // ================= FORM =================
            this.ClientSize = new Size(700, 400);
            this.Controls.Add(this.dataGridArchivos);
            this.Controls.Add(this.panelBotones);
            this.Controls.Add(this.panelHeader);

            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Adjuntos";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;

            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridArchivos)).EndInit();
            this.panelBotones.ResumeLayout(false);
            this.ResumeLayout(false);
        }
    }
}