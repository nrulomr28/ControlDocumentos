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
            label1 = new Label();
            comboBox1 = new ComboBox();
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
            lblFirmante = new Label();
          //  comboFirmantes = new ComboBox();
         //   chkFirmaPorAusencia = new CheckBox();
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
            panelHeader.Size = new Size(575, 48);
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
            panelFormulario.Controls.Add(label1);
            panelFormulario.Controls.Add(comboBox1);
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
            panelFormulario.Size = new Size(575, 655);
            panelFormulario.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 507);
            label1.Name = "label1";
            label1.Size = new Size(64, 15);
            label1.TabIndex = 16;
            label1.Text = "Firma por: ";
       //     label1.Click += label1_Click_1;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(136, 499);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(424, 23);
            comboBox1.TabIndex = 14;
    //        comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_2;
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
           // txtOficioReferencia.TextChanged += txtOficioReferencia_TextChanged;
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
       //     txtAsunto.TextChanged += txtAsunto_TextChanged_1;
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
          //  txtDestinatario.TextChanged += txtDestinatario_TextChanged;
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
          //  txtCargo.TextChanged += txtCargo_TextChanged;
            txtCargo.Leave += txtCargo_Leave;
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
         //   txtRespuesta.TextChanged += txtRespuesta_TextChanged;
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
         //   txtCopias.TextChanged += txtCopias_TextChanged_1;
            // 
            // panelBotones
            // 
            panelBotones.Controls.Add(btnGuardar);
            panelBotones.Controls.Add(btnPreview);
            panelBotones.Dock = DockStyle.Bottom;
            panelBotones.Location = new Point(0, 703);
            panelBotones.Name = "panelBotones";
            panelBotones.Size = new Size(575, 60);
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
            // lblFirmante
            // 
            lblFirmante.AutoSize = true;
            lblFirmante.Location = new Point(20, 190);
            lblFirmante.Name = "lblFirmante";
            lblFirmante.Size = new Size(60, 15);
            lblFirmante.TabIndex = 0;
            lblFirmante.Text = "Firmante";
            // 
            // comboFirmantes
            // 
         //   comboFirmantes.DropDownStyle = ComboBoxStyle.DropDownList;
          //  comboFirmantes.Location = new Point(20, 210);
          //  comboFirmantes.Name = "comboFirmantes";
         //   comboFirmantes.Size = new Size(260, 23);
          //  comboFirmantes.TabIndex = 0;
            // 
            // chkFirmaPorAusencia
            // 
        //    chkFirmaPorAusencia.Location = new Point(20, 240);
       //     chkFirmaPorAusencia.Name = "chkFirmaPorAusencia";
        //    chkFirmaPorAusencia.Size = new Size(250, 20);
         //   chkFirmaPorAusencia.TabIndex = 0;
         //   chkFirmaPorAusencia.Text = "Firmar por ausencia del titular";
            // 
            // FrmOficioRespuesta
            // 
            ClientSize = new Size(575, 763);
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

        private ComboBox comboBox1;
        private Label label1;
    }
}