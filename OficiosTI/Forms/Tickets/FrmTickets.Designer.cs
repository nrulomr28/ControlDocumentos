namespace OficiosTI
{
    partial class FrmTickets
    {
        private System.ComponentModel.IContainer components = null;

        private Panel PanelHeader;
        private Label LabelTitulo;

        private Panel PanelFiltros;
        private Label LabelBuscar;
        private TextBox TextBuscar;
        private Label LabelPrioridad;
        private ComboBox ComboPrioridad;
        private Label LabelStatus;
        private ComboBox ComboStatus;
        private Button BtnBuscar;

        private DataGridView DataGridTickets;

        private Panel PanelAcciones;
        private Button BtnAbrirTicket;
        private Button BtnAdjuntos;
        private Button BtnGenerarOficio;
        private Button BtnLimpiar;

        private Panel PanelIndicadores;

        private Label LabelTotalTickets;
        private Label LabelRespondidos;
        private Label LabelPendientes;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            PanelHeader = new Panel();
            lbSegmento = new Label();
            lbDominio = new Label();
            lbUser = new Label();
            LabelTitulo = new Label();
            PanelFiltros = new Panel();
            LabelBuscar = new Label();
            TextBuscar = new TextBox();
            LabelPrioridad = new Label();
            ComboPrioridad = new ComboBox();
            LabelStatus = new Label();
            ComboStatus = new ComboBox();
            BtnBuscar = new Button();
            BtnLimpiar = new Button();
            DataGridTickets = new DataGridView();
            PanelAcciones = new Panel();
            btnSinTicket = new Button();
            btnOficios = new Button();
            btnAsignar = new Button();
            BtnAbrirTicket = new Button();
            BtnAdjuntos = new Button();
            BtnGenerarOficio = new Button();
            PanelIndicadores = new Panel();
            LabelTotalTickets = new Label();
            LabelRespondidos = new Label();
            LabelPendientes = new Label();
            PanelHeader.SuspendLayout();
            PanelFiltros.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridTickets).BeginInit();
            PanelAcciones.SuspendLayout();
            PanelIndicadores.SuspendLayout();
            SuspendLayout();
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.FromArgb(31, 58, 95);
            PanelHeader.Controls.Add(lbSegmento);
            PanelHeader.Controls.Add(lbDominio);
            PanelHeader.Controls.Add(lbUser);
            PanelHeader.Controls.Add(LabelTitulo);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 40);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(1000, 50);
            PanelHeader.TabIndex = 3;
            // 
            // lbSegmento
            // 
            lbSegmento.AutoSize = true;
            lbSegmento.ForeColor = Color.White;
            lbSegmento.Location = new Point(831, 32);
            lbSegmento.Name = "lbSegmento";
            lbSegmento.Size = new Size(38, 15);
            lbSegmento.TabIndex = 3;
            lbSegmento.Text = "label1";
            // 
            // lbDominio
            // 
            lbDominio.AutoSize = true;
            lbDominio.ForeColor = Color.White;
            lbDominio.Location = new Point(500, 32);
            lbDominio.Name = "lbDominio";
            lbDominio.Size = new Size(38, 15);
            lbDominio.TabIndex = 2;
            lbDominio.Text = "label1";
            // 
            // lbUser
            // 
            lbUser.AutoSize = true;
            lbUser.ForeColor = Color.White;
            lbUser.Location = new Point(3, 0);
            lbUser.Name = "lbUser";
            lbUser.Size = new Size(38, 15);
            lbUser.TabIndex = 1;
            lbUser.Text = "label1";
            // 
            // LabelTitulo
            // 
            LabelTitulo.AutoSize = true;
            LabelTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            LabelTitulo.ForeColor = Color.White;
            LabelTitulo.Location = new Point(3, 26);
            LabelTitulo.Name = "LabelTitulo";
            LabelTitulo.Size = new Size(73, 21);
            LabelTitulo.TabIndex = 0;
            LabelTitulo.Text = "Tickets -";
            // 
            // PanelFiltros
            // 
            PanelFiltros.BackColor = Color.FromArgb(244, 246, 249);
            PanelFiltros.Controls.Add(LabelBuscar);
            PanelFiltros.Controls.Add(TextBuscar);
            PanelFiltros.Controls.Add(LabelPrioridad);
            PanelFiltros.Controls.Add(ComboPrioridad);
            PanelFiltros.Controls.Add(LabelStatus);
            PanelFiltros.Controls.Add(ComboStatus);
            PanelFiltros.Controls.Add(BtnBuscar);
            PanelFiltros.Controls.Add(BtnLimpiar);
            PanelFiltros.Dock = DockStyle.Left;
            PanelFiltros.Location = new Point(0, 90);
            PanelFiltros.Name = "PanelFiltros";
            PanelFiltros.Size = new Size(220, 460);
            PanelFiltros.TabIndex = 1;
            // 
            // LabelBuscar
            // 
            LabelBuscar.AutoSize = true;
            LabelBuscar.Location = new Point(15, 20);
            LabelBuscar.Name = "LabelBuscar";
            LabelBuscar.Size = new Size(42, 15);
            LabelBuscar.TabIndex = 0;
            LabelBuscar.Text = "Buscar";
            // 
            // TextBuscar
            // 
            TextBuscar.Location = new Point(15, 40);
            TextBuscar.Name = "TextBuscar";
            TextBuscar.Size = new Size(180, 23);
            TextBuscar.TabIndex = 1;
            // 
            // LabelPrioridad
            // 
            LabelPrioridad.AutoSize = true;
            LabelPrioridad.Location = new Point(15, 80);
            LabelPrioridad.Name = "LabelPrioridad";
            LabelPrioridad.Size = new Size(55, 15);
            LabelPrioridad.TabIndex = 2;
            LabelPrioridad.Text = "Prioridad";
            // 
            // ComboPrioridad
            // 
            ComboPrioridad.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboPrioridad.Location = new Point(15, 100);
            ComboPrioridad.Name = "ComboPrioridad";
            ComboPrioridad.Size = new Size(180, 23);
            ComboPrioridad.TabIndex = 3;
            // 
            // LabelStatus
            // 
            LabelStatus.AutoSize = true;
            LabelStatus.Location = new Point(15, 140);
            LabelStatus.Name = "LabelStatus";
            LabelStatus.Size = new Size(39, 15);
            LabelStatus.TabIndex = 4;
            LabelStatus.Text = "Status";
            // 
            // ComboStatus
            // 
            ComboStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboStatus.Location = new Point(15, 160);
            ComboStatus.Name = "ComboStatus";
            ComboStatus.Size = new Size(180, 23);
            ComboStatus.TabIndex = 5;
            // 
            // BtnBuscar
            // 
            BtnBuscar.Location = new Point(15, 210);
            BtnBuscar.Name = "BtnBuscar";
            BtnBuscar.Size = new Size(180, 35);
            BtnBuscar.TabIndex = 6;
            BtnBuscar.Text = "Buscar";
            BtnBuscar.Click += BtnBuscar_Click;
            // 
            // BtnLimpiar
            // 
            BtnLimpiar.Location = new Point(15, 255);
            BtnLimpiar.Name = "BtnLimpiar";
            BtnLimpiar.Size = new Size(180, 35);
            BtnLimpiar.TabIndex = 5;
            BtnLimpiar.Text = "Limpiar filtros";
            BtnLimpiar.UseVisualStyleBackColor = true;
            BtnLimpiar.Click += BtnLimpiar_Click;
            // 
            // DataGridTickets
            // 
            DataGridTickets.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridTickets.BackgroundColor = Color.White;
            DataGridTickets.BorderStyle = BorderStyle.None;
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = Color.FromArgb(31, 58, 95);
            dataGridViewCellStyle1.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle1.ForeColor = Color.White;
            dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = DataGridViewTriState.True;
            DataGridTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Window;
            dataGridViewCellStyle2.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = Color.FromArgb(46, 134, 193);
            dataGridViewCellStyle2.SelectionForeColor = Color.White;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
            DataGridTickets.DefaultCellStyle = dataGridViewCellStyle2;
            DataGridTickets.Dock = DockStyle.Fill;
            DataGridTickets.EnableHeadersVisualStyles = false;
            DataGridTickets.Location = new Point(220, 90);
            DataGridTickets.MultiSelect = false;
            DataGridTickets.Name = "DataGridTickets";
            DataGridTickets.ReadOnly = true;
            DataGridTickets.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            DataGridTickets.Size = new Size(780, 460);
            DataGridTickets.TabIndex = 0;
            DataGridTickets.CellClick += DataGridTickets_CellClick;
            DataGridTickets.CellDoubleClick += DataGridTickets_CellDoubleClick;
            DataGridTickets.DataBindingComplete += DataGridTickets_DataBindingComplete;
            // 
            // PanelAcciones
            // 
            PanelAcciones.BackColor = Color.WhiteSmoke;
            PanelAcciones.Controls.Add(btnSinTicket);
            PanelAcciones.Controls.Add(btnOficios);
            PanelAcciones.Controls.Add(btnAsignar);
            PanelAcciones.Controls.Add(BtnAbrirTicket);
            PanelAcciones.Controls.Add(BtnAdjuntos);
            PanelAcciones.Controls.Add(BtnGenerarOficio);
            PanelAcciones.Dock = DockStyle.Bottom;
            PanelAcciones.Location = new Point(0, 550);
            PanelAcciones.Name = "PanelAcciones";
            PanelAcciones.Size = new Size(1000, 50);
            PanelAcciones.TabIndex = 2;
            // 
            // btnSinTicket
            // 
            btnSinTicket.Location = new Point(540, 13);
            btnSinTicket.Name = "btnSinTicket";
            btnSinTicket.Size = new Size(121, 28);
            btnSinTicket.TabIndex = 5;
            btnSinTicket.Text = "Numeros Oficio";
            btnSinTicket.UseVisualStyleBackColor = true;
            btnSinTicket.Click += btnSinTicket_Click_1;
            // 
            // btnOficios
            // 
            btnOficios.Location = new Point(667, 13);
            btnOficios.Name = "btnOficios";
            btnOficios.Size = new Size(106, 28);
            btnOficios.TabIndex = 4;
            btnOficios.Text = "OFicios";
            btnOficios.UseVisualStyleBackColor = true;
            btnOficios.Click += btnOficios_Click;
            // 
            // btnAsignar
            // 
            btnAsignar.Location = new Point(271, 12);
            btnAsignar.Name = "btnAsignar";
            btnAsignar.Size = new Size(121, 28);
            btnAsignar.TabIndex = 3;
            btnAsignar.Text = "Asignar Oficio";
            btnAsignar.UseVisualStyleBackColor = true;
            btnAsignar.Click += button1_Click;
            // 
            // BtnAbrirTicket
            // 
            BtnAbrirTicket.Location = new Point(15, 10);
            BtnAbrirTicket.Name = "BtnAbrirTicket";
            BtnAbrirTicket.Size = new Size(120, 30);
            BtnAbrirTicket.TabIndex = 0;
            BtnAbrirTicket.Text = "Abrir Ticket";
            BtnAbrirTicket.Click += BtnAbrirTicket_Click_1;
            // 
            // BtnAdjuntos
            // 
            BtnAdjuntos.Location = new Point(145, 10);
            BtnAdjuntos.Name = "BtnAdjuntos";
            BtnAdjuntos.Size = new Size(120, 30);
            BtnAdjuntos.TabIndex = 1;
            BtnAdjuntos.Text = "Adjuntos";
            BtnAdjuntos.Click += BtnAdjuntos_Click;
            // 
            // BtnGenerarOficio
            // 
            BtnGenerarOficio.Location = new Point(398, 11);
            BtnGenerarOficio.Name = "BtnGenerarOficio";
            BtnGenerarOficio.Size = new Size(140, 30);
            BtnGenerarOficio.TabIndex = 2;
            BtnGenerarOficio.Text = "Generar Oficio";
            BtnGenerarOficio.Click += BtnGenerarOficio_Click;
            // 
            // PanelIndicadores
            // 
            PanelIndicadores.BackColor = Color.White;
            PanelIndicadores.Controls.Add(LabelTotalTickets);
            PanelIndicadores.Controls.Add(LabelRespondidos);
            PanelIndicadores.Controls.Add(LabelPendientes);
            PanelIndicadores.Dock = DockStyle.Top;
            PanelIndicadores.Location = new Point(0, 0);
            PanelIndicadores.Name = "PanelIndicadores";
            PanelIndicadores.Size = new Size(1000, 40);
            PanelIndicadores.TabIndex = 4;
            // 
            // LabelTotalTickets
            // 
            LabelTotalTickets.AutoSize = true;
            LabelTotalTickets.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LabelTotalTickets.Location = new Point(250, 10);
            LabelTotalTickets.Name = "LabelTotalTickets";
            LabelTotalTickets.Size = new Size(0, 15);
            LabelTotalTickets.TabIndex = 0;
            // 
            // LabelRespondidos
            // 
            LabelRespondidos.AutoSize = true;
            LabelRespondidos.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LabelRespondidos.ForeColor = Color.DarkGreen;
            LabelRespondidos.Location = new Point(450, 10);
            LabelRespondidos.Name = "LabelRespondidos";
            LabelRespondidos.Size = new Size(0, 15);
            LabelRespondidos.TabIndex = 1;
            // 
            // LabelPendientes
            // 
            LabelPendientes.AutoSize = true;
            LabelPendientes.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            LabelPendientes.ForeColor = Color.DarkRed;
            LabelPendientes.Location = new Point(650, 10);
            LabelPendientes.Name = "LabelPendientes";
            LabelPendientes.Size = new Size(0, 15);
            LabelPendientes.TabIndex = 2;
            // 
            // FrmTickets
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1000, 600);
            Controls.Add(DataGridTickets);
            Controls.Add(PanelFiltros);
            Controls.Add(PanelAcciones);
            Controls.Add(PanelHeader);
            Controls.Add(PanelIndicadores);
            Name = "FrmTickets";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Tickets TI";
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            PanelFiltros.ResumeLayout(false);
            PanelFiltros.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)DataGridTickets).EndInit();
            PanelAcciones.ResumeLayout(false);
            PanelIndicadores.ResumeLayout(false);
            PanelIndicadores.PerformLayout();
            ResumeLayout(false);
        }

        private Button btnAsignar;
        private Button btnOficios;
        private Button btnSinTicket;
        private Label lbUser;
        private Label lbDominio;
        private Label lbSegmento;
    }
}
