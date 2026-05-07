namespace OficiosTI
{
    partial class FrmPanelTickets
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitulo;

        private TabControl tabControlMain;

        private TabPage tabPendientes;
        private TabPage tabCerrados;
        private TabPage tabAnalista;
        private TabPage tabTodos;

        private DataGridView gridPendientes;
        private DataGridView gridCerrados;
        private DataGridView gridAnalista;
        private DataGridView gridTodos;

        private Panel panelAcciones;
        private Button btnAbrir;
        private Button btnAdjuntos;
        private Button btnGenerar;

        private ComboBox comboAnalista;
        private ComboBox comboTodos;
        private DataGridView DataGridTickets;
        private Label LabelPendientes;
        private Label LabelTotalTickets;
        private Label LabelRespondidos;
        private TextBox TextBuscar;

        private Panel panelIndicadores;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        //private void InitializeComponent()
        //{
        //    panelHeader = new Panel();
        //    lblTitulo = new Label();

        //    tabControlMain = new TabControl();

        //    tabPendientes = new TabPage();
        //    tabCerrados = new TabPage();
        //    tabAnalista = new TabPage();
        //    tabTodos = new TabPage();

        //    gridPendientes = new DataGridView();
        //    gridCerrados = new DataGridView();
        //    gridAnalista = new DataGridView();
        //    gridTodos = new DataGridView();

        //    panelAcciones = new Panel();
        //    btnAbrir = new Button();
        //    btnAdjuntos = new Button();
        //    btnGenerar = new Button();

        //    comboAnalista = new ComboBox();

        //    LabelTotalTickets = new Label();
        //    LabelRespondidos = new Label();
        //    LabelPendientes = new Label();

        //    panelHeader.SuspendLayout();
        //    tabControlMain.SuspendLayout();
        //    tabPendientes.SuspendLayout();
        //    tabCerrados.SuspendLayout();
        //    tabAnalista.SuspendLayout();
        //    tabTodos.SuspendLayout();
        //    ((System.ComponentModel.ISupportInitialize)gridPendientes).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)gridCerrados).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)gridAnalista).BeginInit();
        //    ((System.ComponentModel.ISupportInitialize)gridTodos).BeginInit();
        //    panelAcciones.SuspendLayout();
        //    SuspendLayout();

        //    // HEADER
        //    panelHeader.BackColor = Color.FromArgb(10, 36, 106);
        //    panelHeader.Dock = DockStyle.Top;
        //    panelHeader.Height = 45;
        //    panelHeader.Controls.Add(lblTitulo);

        //    lblTitulo.AutoSize = true;
        //    lblTitulo.ForeColor = Color.White;
        //    lblTitulo.Font = new Font("Tahoma", 10F, FontStyle.Bold);
        //    lblTitulo.Location = new Point(15, 12);
        //    lblTitulo.Text = "Dashboard de Tickets - Desarrollo TI";

        //    // TABCONTROL
        //    tabControlMain.Dock = DockStyle.Fill;
        //    tabControlMain.Controls.Add(tabPendientes);
        //    tabControlMain.Controls.Add(tabCerrados);
        //    tabControlMain.Controls.Add(tabAnalista);
        //    tabControlMain.Controls.Add(tabTodos);

        //    // TABS
        //    tabPendientes.Text = "Pendientes";
        //    tabCerrados.Text = "Cerrados sin oficio";
        //    tabAnalista.Text = "Por analista";
        //    tabTodos.Text = "Todos";

        //    // GRIDS (CONFIG BASE)
        //    ConfigurarGrid(gridPendientes);
        //    ConfigurarGrid(gridCerrados);
        //    ConfigurarGrid(gridAnalista);
        //    ConfigurarGrid(gridTodos);

        //    gridPendientes.Dock = DockStyle.Fill;
        //    gridCerrados.Dock = DockStyle.Fill;
        //    gridAnalista.Dock = DockStyle.Fill;
        //    gridTodos.Dock = DockStyle.Fill;

        //    tabPendientes.Controls.Add(gridPendientes);
        //    tabCerrados.Controls.Add(gridCerrados);

        //    // ANALISTA TAB
        //    comboAnalista.Dock = DockStyle.Top;
        //    comboAnalista.Height = 25;

        //    tabAnalista.Controls.Add(gridAnalista);
        //    tabAnalista.Controls.Add(comboAnalista);

        //    tabTodos.Controls.Add(gridTodos);

        //    // PANEL ACCIONES
        //    panelAcciones.Dock = DockStyle.Bottom;
        //    panelAcciones.Height = 50;
        //    panelAcciones.BackColor = Color.FromArgb(236, 233, 216);

        //    btnAbrir.Text = "Abrir";
        //    btnAbrir.Location = new Point(15, 10);
        //    btnAbrir.Size = new Size(100, 30);
        //    btnAbrir.Click += BtnAbrirTicket_Click;

        //    btnAdjuntos.Text = "Adjuntos";
        //    btnAdjuntos.Location = new Point(130, 10);
        //    btnAdjuntos.Size = new Size(100, 30);
        //    btnAdjuntos.Click += BtnAdjuntos_Click;

        //    btnGenerar.Text = "Generar Oficio";
        //    btnGenerar.Location = new Point(245, 10);
        //    btnGenerar.Size = new Size(140, 30);
        //    btnGenerar.Click += BtnGenerarOficio_Click;

        //    panelAcciones.Controls.Add(btnAbrir);
        //    panelAcciones.Controls.Add(btnAdjuntos);
        //    panelAcciones.Controls.Add(btnGenerar);

        //    // FORM
        //    ClientSize = new Size(1100, 650);
        //    Controls.Add(tabControlMain);
        //    Controls.Add(panelAcciones);
        //    Controls.Add(panelHeader);

        //    Text = "Dashboard Tickets";
        //    StartPosition = FormStartPosition.CenterScreen;

        //    panelHeader.ResumeLayout(false);
        //    panelHeader.PerformLayout();
        //    tabControlMain.ResumeLayout(false);
        //    tabPendientes.ResumeLayout(false);
        //    tabCerrados.ResumeLayout(false);
        //    tabAnalista.ResumeLayout(false);
        //    tabTodos.ResumeLayout(false);
        //    ((System.ComponentModel.ISupportInitialize)gridPendientes).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)gridCerrados).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)gridAnalista).EndInit();
        //    ((System.ComponentModel.ISupportInitialize)gridTodos).EndInit();
        //    panelAcciones.ResumeLayout(false);
        //    ResumeLayout(false);
        //}

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitulo = new Label();
            tabControlMain = new TabControl();
            tabPendientes = new TabPage();
            gridPendientes = new DataGridView();
            tabCerrados = new TabPage();
            gridCerrados = new DataGridView();
            tabAnalista = new TabPage();
            gridAnalista = new DataGridView();
            comboAnalista = new ComboBox();
            tabTodos = new TabPage();
            gridTodos = new DataGridView();
            panelAcciones = new Panel();
            btnAbrir = new Button();
            btnAdjuntos = new Button();
            btnGenerar = new Button();
            panelIndicadores = new Panel();
            LabelTotalTickets = new Label();
            LabelRespondidos = new Label();
            LabelPendientes = new Label();
            panelHeader.SuspendLayout();
            tabControlMain.SuspendLayout();
            tabPendientes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridPendientes).BeginInit();
            tabCerrados.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridCerrados).BeginInit();
            tabAnalista.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridAnalista).BeginInit();
            tabTodos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridTodos).BeginInit();
            panelAcciones.SuspendLayout();
            panelIndicadores.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(10, 36, 106);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 40);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1100, 45);
            panelHeader.TabIndex = 2;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(15, 12);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(259, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Dashboard de Tickets - Desarrollo TI";
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPendientes);
            tabControlMain.Controls.Add(tabCerrados);
            tabControlMain.Controls.Add(tabAnalista);
            tabControlMain.Controls.Add(tabTodos);
            tabControlMain.Dock = DockStyle.Fill;
            tabControlMain.Location = new Point(0, 85);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new Size(1100, 515);
            tabControlMain.TabIndex = 0;
            // 
            // tabPendientes
            // 
            tabPendientes.Controls.Add(gridPendientes);
            tabPendientes.Location = new Point(4, 24);
            tabPendientes.Name = "tabPendientes";
            tabPendientes.Size = new Size(1092, 487);
            tabPendientes.TabIndex = 0;
            tabPendientes.Text = "Pendientes";
            // 
            // gridPendientes
            // 
            gridPendientes.Dock = DockStyle.Fill;
            gridPendientes.Location = new Point(0, 0);
            gridPendientes.Name = "gridPendientes";
            gridPendientes.Size = new Size(1092, 487);
            gridPendientes.TabIndex = 0;
            // 
            // tabCerrados
            // 
            tabCerrados.Controls.Add(gridCerrados);
            tabCerrados.Location = new Point(4, 24);
            tabCerrados.Name = "tabCerrados";
            tabCerrados.Size = new Size(1092, 487);
            tabCerrados.TabIndex = 1;
            tabCerrados.Text = "Cerrados sin oficio";
            // 
            // gridCerrados
            // 
            gridCerrados.Dock = DockStyle.Fill;
            gridCerrados.Location = new Point(0, 0);
            gridCerrados.Name = "gridCerrados";
            gridCerrados.Size = new Size(1092, 487);
            gridCerrados.TabIndex = 0;
            // 
            // tabAnalista
            // 
            tabAnalista.Controls.Add(gridAnalista);
            tabAnalista.Controls.Add(comboAnalista);
            tabAnalista.Location = new Point(4, 24);
            tabAnalista.Name = "tabAnalista";
            tabAnalista.Size = new Size(1092, 487);
            tabAnalista.TabIndex = 2;
            tabAnalista.Text = "Por analista";
            // 
            // gridAnalista
            // 
            gridAnalista.Dock = DockStyle.Fill;
            gridAnalista.Location = new Point(0, 23);
            gridAnalista.Name = "gridAnalista";
            gridAnalista.Size = new Size(1092, 464);
            gridAnalista.TabIndex = 0;
            // 
            // comboAnalista
            // 
            comboAnalista.Dock = DockStyle.Top;
            comboAnalista.Location = new Point(0, 0);
            comboAnalista.Name = "comboAnalista";
            comboAnalista.Size = new Size(1092, 23);
            comboAnalista.TabIndex = 1;
            // 
            // tabTodos
            // 
            tabTodos.Controls.Add(gridTodos);
            tabTodos.Location = new Point(4, 24);
            tabTodos.Name = "tabTodos";
            tabTodos.Size = new Size(1092, 487);
            tabTodos.TabIndex = 3;
            tabTodos.Text = "Todos";
            // 
            // gridTodos
            // 
            gridTodos.Dock = DockStyle.Fill;
            gridTodos.Location = new Point(0, 0);
            gridTodos.Name = "gridTodos";
            gridTodos.Size = new Size(1092, 487);
            gridTodos.TabIndex = 0;
            // 
            // panelAcciones
            // 
            panelAcciones.BackColor = Color.FromArgb(236, 233, 216);
            panelAcciones.Controls.Add(btnAbrir);
            panelAcciones.Controls.Add(btnAdjuntos);
            panelAcciones.Controls.Add(btnGenerar);
            panelAcciones.Dock = DockStyle.Bottom;
            panelAcciones.Location = new Point(0, 600);
            panelAcciones.Name = "panelAcciones";
            panelAcciones.Size = new Size(1100, 50);
            panelAcciones.TabIndex = 1;
            // 
            // btnAbrir
            // 
            btnAbrir.Location = new Point(15, 10);
            btnAbrir.Name = "btnAbrir";
            btnAbrir.Size = new Size(100, 30);
            btnAbrir.TabIndex = 0;
            btnAbrir.Text = "Abrir";
            btnAbrir.Click += BtnAbrirTicket_Click;
            // 
            // btnAdjuntos
            // 
            btnAdjuntos.Location = new Point(130, 10);
            btnAdjuntos.Name = "btnAdjuntos";
            btnAdjuntos.Size = new Size(100, 30);
            btnAdjuntos.TabIndex = 1;
            btnAdjuntos.Text = "Adjuntos";
            btnAdjuntos.Click += BtnAdjuntos_Click;
            // 
            // btnGenerar
            // 
            btnGenerar.Location = new Point(245, 10);
            btnGenerar.Name = "btnGenerar";
            btnGenerar.Size = new Size(140, 30);
            btnGenerar.TabIndex = 2;
            btnGenerar.Text = "Generar Oficio";
            btnGenerar.Click += BtnGenerarOficio_Click;
            // 
            // panelIndicadores
            // 
            panelIndicadores.BackColor = Color.White;
            panelIndicadores.Controls.Add(LabelTotalTickets);
            panelIndicadores.Controls.Add(LabelRespondidos);
            panelIndicadores.Controls.Add(LabelPendientes);
            panelIndicadores.Dock = DockStyle.Top;
            panelIndicadores.Location = new Point(0, 0);
            panelIndicadores.Name = "panelIndicadores";
            panelIndicadores.Size = new Size(1100, 40);
            panelIndicadores.TabIndex = 3;
            // 
            // LabelTotalTickets
            // 
            LabelTotalTickets.AutoSize = true;
            LabelTotalTickets.Font = new Font("Tahoma", 9F, FontStyle.Bold);
            LabelTotalTickets.Location = new Point(250, 10);
            LabelTotalTickets.Name = "LabelTotalTickets";
            LabelTotalTickets.Size = new Size(0, 14);
            LabelTotalTickets.TabIndex = 0;
            // 
            // LabelRespondidos
            // 
            LabelRespondidos.AutoSize = true;
            LabelRespondidos.ForeColor = Color.DarkGreen;
            LabelRespondidos.Location = new Point(450, 10);
            LabelRespondidos.Name = "LabelRespondidos";
            LabelRespondidos.Size = new Size(0, 15);
            LabelRespondidos.TabIndex = 1;
            // 
            // LabelPendientes
            // 
            LabelPendientes.AutoSize = true;
            LabelPendientes.ForeColor = Color.DarkRed;
            LabelPendientes.Location = new Point(650, 10);
            LabelPendientes.Name = "LabelPendientes";
            LabelPendientes.Size = new Size(0, 15);
            LabelPendientes.TabIndex = 2;
            // 
            // FrmPanelTickets
            // 
            ClientSize = new Size(1100, 650);
            Controls.Add(tabControlMain);
            Controls.Add(panelAcciones);
            Controls.Add(panelHeader);
            Controls.Add(panelIndicadores);
            Name = "FrmPanelTickets";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Dashboard Tickets";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            tabControlMain.ResumeLayout(false);
            tabPendientes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridPendientes).EndInit();
            tabCerrados.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridCerrados).EndInit();
            tabAnalista.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridAnalista).EndInit();
            tabTodos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)gridTodos).EndInit();
            panelAcciones.ResumeLayout(false);
            panelIndicadores.ResumeLayout(false);
            panelIndicadores.PerformLayout();
            ResumeLayout(false);
        }

        private void ConfigurarGrid(DataGridView grid)
        {
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.Fixed3D;
            grid.ReadOnly = true;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.RowHeadersVisible = false;
        }
    }
}