namespace OficiosTI
{
    partial class FrmOficioRespuesta
    {
        private System.ComponentModel.IContainer components = null;

        private Panel panelHeader;
        private Label lblTitulo;

        private Panel panelFormulario;

        private Label lblNumeroOficio;
        private TextBox txtNumeroOficio;

        private Label lblOficioReferencia;
        private TextBox txtOficioReferencia;

        private Label lblAsunto;
        private TextBox txtAsunto;

        private Label lblDestinatario;
        private TextBox txtDestinatario;

        private Label lblCargo;
        private TextBox txtCargo;

        private Label lblRespuesta;
        private TextBox txtRespuesta;

        private Label lblCopias;
        private TextBox txtCopias;

        private Panel panelBotones;
        private Button btnGuardar;
        private Button btnPreview;

        private Label lblFirmante;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            panelHeader = new Panel();
            lblTitulo = new Label();
            panelFormulario = new Panel();
            lblNumeroOficio = new Label();
            txtNumeroOficio = new TextBox();
            lblOficioReferencia = new Label();
            txtOficioReferencia = new TextBox();
            lblAsunto = new Label();
            txtAsunto = new TextBox();
            lblDestinatario = new Label();
            txtDestinatario = new TextBox();
            lblCargo = new Label();
            txtCargo = new TextBox();
            lblRespuesta = new Label();
            txtRespuesta = new TextBox();
            lblCopias = new Label();
            txtCopias = new TextBox();
            panelBotones = new Panel();
            btnGuardar = new Button();
            btnPreview = new Button();
            panelHeader.SuspendLayout();
            panelFormulario.SuspendLayout();
            panelBotones.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(0, 51, 102);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(600, 48);
            panelHeader.TabIndex = 2;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(12, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(252, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Maquetador de Oficio de Respuesta";
            // 
            // panelFormulario
            // 
            panelFormulario.BackColor = SystemColors.Control;
            panelFormulario.Controls.Add(lblNumeroOficio);
            panelFormulario.Controls.Add(txtNumeroOficio);
            panelFormulario.Controls.Add(lblOficioReferencia);
            panelFormulario.Controls.Add(txtOficioReferencia);
            panelFormulario.Controls.Add(lblAsunto);
            panelFormulario.Controls.Add(txtAsunto);
            panelFormulario.Controls.Add(lblDestinatario);
            panelFormulario.Controls.Add(txtDestinatario);
            panelFormulario.Controls.Add(lblCargo);
            panelFormulario.Controls.Add(txtCargo);
            panelFormulario.Controls.Add(lblRespuesta);
            panelFormulario.Controls.Add(txtRespuesta);
            panelFormulario.Controls.Add(lblCopias);
            panelFormulario.Controls.Add(txtCopias);
            panelFormulario.Dock = DockStyle.Fill;
            panelFormulario.Location = new Point(0, 48);
            panelFormulario.Name = "panelFormulario";
            panelFormulario.Padding = new Padding(20);
            panelFormulario.Size = new Size(600, 492);
            panelFormulario.TabIndex = 0;            
            // 
            // lblNumeroOficio
            // 
            lblNumeroOficio.AutoSize = true;
            lblNumeroOficio.Location = new Point(20, 20);
            lblNumeroOficio.Name = "lblNumeroOficio";
            lblNumeroOficio.Size = new Size(102, 15);
            lblNumeroOficio.TabIndex = 0;
            lblNumeroOficio.Text = "Número de Oficio";
            // 
            // txtNumeroOficio
            // 
            txtNumeroOficio.Location = new Point(20, 38);
            txtNumeroOficio.Name = "txtNumeroOficio";
            txtNumeroOficio.Size = new Size(260, 23);
            txtNumeroOficio.TabIndex = 1;
            // 
            // lblOficioReferencia
            // 
            lblOficioReferencia.AutoSize = true;
            lblOficioReferencia.Location = new Point(300, 20);
            lblOficioReferencia.Name = "lblOficioReferencia";
            lblOficioReferencia.Size = new Size(128, 15);
            lblOficioReferencia.TabIndex = 2;
            lblOficioReferencia.Text = "Oficio que se responde";
            // 
            // txtOficioReferencia
            // 
            txtOficioReferencia.Location = new Point(300, 38);
            txtOficioReferencia.Name = "txtOficioReferencia";
            txtOficioReferencia.Size = new Size(260, 23);
            txtOficioReferencia.TabIndex = 3;
            // 
            // lblAsunto
            // 
            lblAsunto.AutoSize = true;
            lblAsunto.Location = new Point(20, 78);
            lblAsunto.Name = "lblAsunto";
            lblAsunto.Size = new Size(45, 15);
            lblAsunto.TabIndex = 4;
            lblAsunto.Text = "Asunto";
            // 
            // txtAsunto
            // 
            txtAsunto.Location = new Point(20, 96);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.Size = new Size(540, 23);
            txtAsunto.TabIndex = 5;
            // 
            // lblDestinatario
            // 
            lblDestinatario.AutoSize = true;
            lblDestinatario.Location = new Point(20, 136);
            lblDestinatario.Name = "lblDestinatario";
            lblDestinatario.Size = new Size(70, 15);
            lblDestinatario.TabIndex = 6;
            lblDestinatario.Text = "Destinatario";
            // 
            // txtDestinatario
            // 
            txtDestinatario.CharacterCasing = CharacterCasing.Upper;
            txtDestinatario.Location = new Point(20, 154);
            txtDestinatario.Name = "txtDestinatario";
            txtDestinatario.Size = new Size(260, 23);
            txtDestinatario.TabIndex = 7;
            txtDestinatario.TextChanged += txtDestinatario_TextChanged;
            txtDestinatario.Leave += txtDestinatario_Leave;
            // 
            // lblCargo
            // 
            lblCargo.AutoSize = true;
            lblCargo.Location = new Point(300, 136);
            lblCargo.Name = "lblCargo";
            lblCargo.Size = new Size(39, 15);
            lblCargo.TabIndex = 8;
            lblCargo.Text = "Cargo";
            // 
            // txtCargo
            // 
            txtCargo.CharacterCasing = CharacterCasing.Upper;
            txtCargo.Location = new Point(300, 154);
            txtCargo.Name = "txtCargo";
            txtCargo.Size = new Size(260, 23);
            txtCargo.TabIndex = 9;
            txtCargo.TextChanged += txtCargo_TextChanged;
            txtCargo.Leave += txtCargo_Leave;

            // lblFirmante
            lblFirmante = new Label();
            lblFirmante.AutoSize = true;
            lblFirmante.Location = new Point(20, 190);
            lblFirmante.Name = "lblFirmante";
            lblFirmante.Size = new Size(60, 15);
            lblFirmante.Text = "Firmante";

            // cmbFirmante
            cmbFirmante = new ComboBox();
            cmbFirmante.Location = new Point(20, 210);
            cmbFirmante.Name = "cmbFirmante";
            cmbFirmante.Size = new Size(260, 23);
            cmbFirmante.DropDownStyle = ComboBoxStyle.DropDownList;

            // chkFirmaPorAusencia
            chkFirmaPorAusencia = new CheckBox();
            chkFirmaPorAusencia.Location = new Point(20, 240);
            chkFirmaPorAusencia.Name = "chkFirmaPorAusencia";
            chkFirmaPorAusencia.Size = new Size(250, 20);
            chkFirmaPorAusencia.Text = "Firmar por ausencia del titular";

            // 
            // lblRespuesta
            // 
            lblRespuesta.AutoSize = true;
            lblRespuesta.Location = new Point(20, 194);
            lblRespuesta.Name = "lblRespuesta";
            lblRespuesta.Size = new Size(60, 15);
            lblRespuesta.TabIndex = 10;
            lblRespuesta.Text = "Respuesta";
            // 
            // txtRespuesta
            // 
            txtRespuesta.Location = new Point(20, 212);
            txtRespuesta.Multiline = true;
            txtRespuesta.Name = "txtRespuesta";
            txtRespuesta.ScrollBars = ScrollBars.Vertical;
            txtRespuesta.Size = new Size(540, 180);
            txtRespuesta.TabIndex = 11;
            // 
            // lblCopias
            // 
            lblCopias.AutoSize = true;
            lblCopias.Location = new Point(20, 402);
            lblCopias.Name = "lblCopias";
            lblCopias.Size = new Size(86, 15);
            lblCopias.TabIndex = 12;
            lblCopias.Text = "Copias (C.C.P.)";
            // 
            // txtCopias
            // 
            txtCopias.Location = new Point(20, 420);
            txtCopias.Multiline = true;
            txtCopias.Name = "txtCopias";
            txtCopias.Size = new Size(540, 70);
            txtCopias.TabIndex = 13;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnGuardar);
            panelBotones.Controls.Add(btnPreview);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 540);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(600, 60);
            panelBotones.TabIndex = 1;
            // 
            // btnGuardar
            // 
            btnGuardar.Location = new Point(170, 14);
            btnGuardar.Name = "btnGuardar";
            btnGuardar.Size = new Size(140, 32);
            btnGuardar.TabIndex = 0;
            btnGuardar.Text = "Guardar Oficio";
            btnGuardar.Click += BtnGuardar_Click;
            // 
            // btnPreview
            // 
            btnPreview.Location = new Point(330, 14);
            btnPreview.Name = "btnPreview";
            btnPreview.Size = new Size(140, 32);
            btnPreview.TabIndex = 1;
            btnPreview.Text = "Vista Previa";
            btnPreview.Click += BtnPreview_Click;
            // 
            // FrmOficioRespuesta
            // 
            ClientSize = new Size(600, 600);
            Controls.Add(panelFormulario);
            Controls.Add(panelBotones);
            Controls.Add(panelHeader);
            Name = "FrmOficioRespuesta";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Oficio de Respuesta";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panelFormulario.ResumeLayout(false);
            panelFormulario.PerformLayout();
            panelBotones.ResumeLayout(false);
            ResumeLayout(false);
        }

    }
}