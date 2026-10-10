namespace OficiosTI
{
    partial class FrmCapOficios
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            CmbTipo = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            CmbEjercicio = new ComboBox();
            label3 = new Label();
            DateRecepcion = new DateTimePicker();
            label4 = new Label();
            CmbDocumento = new ComboBox();
            label5 = new Label();
            contextMenuStrip1 = new ContextMenuStrip(components);
            TxtNumOficio = new TextBox();
            DateDocumento = new DateTimePicker();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            label9 = new Label();
            label10 = new Label();
            label11 = new Label();
            label12 = new Label();
            label13 = new Label();
            label14 = new Label();
            CmbRemitente = new ComboBox();
            CmbDestinatario = new ComboBox();
            CmbPrioridad = new ComboBox();
            TextAsunto = new RichTextBox();
            TextFolioIni = new TextBox();
            TextFolioFin = new TextBox();
            label15 = new Label();
            TextUbicacion = new TextBox();
            button1 = new Button();
            panelHeader = new Panel();
            lblTitulo = new Label();
            label16 = new Label();
            CmbTicket = new ComboBox();
            TextPeticion = new RichTextBox();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            CmbSeRecibe = new ComboBox();
            CmbClasif = new ComboBox();
            label20 = new Label();
            btnAdjuntar = new Button();
            btnQuitarAdjunto = new Button();
            label21 = new Label();
            cmbCargoRemitente = new ComboBox();
            cmbCargoDestinatario = new ComboBox();
            label22 = new Label();
            btnVerArchivo = new Button();
            lblEstado = new Label();
            cmbEstatus = new ComboBox();
            lblhistorial = new Label();
            txtSeguimiento = new RichTextBox();
            dataGridTickets = new DataGridView();
            lblTickets = new Label();
            panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTickets).BeginInit();
            SuspendLayout();
            // 
            // CmbTipo
            // 
            CmbTipo.FormattingEnabled = true;
            CmbTipo.Location = new Point(15, 59);
            CmbTipo.Name = "CmbTipo";
            CmbTipo.Size = new Size(121, 23);
            CmbTipo.TabIndex = 5;
            CmbTipo.SelectedIndexChanged += CmbTipo_SelectedIndexChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 41);
            label1.Name = "label1";
            label1.Size = new Size(90, 15);
            label1.TabIndex = 0;
            label1.Text = "Tipo de captura";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(142, 43);
            label2.Name = "label2";
            label2.Size = new Size(51, 15);
            label2.TabIndex = 1;
            label2.Text = "Ejercicio";
            // 
            // CmbEjercicio
            // 
            CmbEjercicio.FormattingEnabled = true;
            CmbEjercicio.Location = new Point(142, 61);
            CmbEjercicio.Name = "CmbEjercicio";
            CmbEjercicio.Size = new Size(121, 23);
            CmbEjercicio.TabIndex = 6;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(280, 43);
            label3.Name = "label3";
            label3.Size = new Size(112, 15);
            label3.TabIndex = 2;
            label3.Text = "Fecha de Recepción";
            // 
            // DateRecepcion
            // 
            DateRecepcion.Location = new Point(280, 61);
            DateRecepcion.Name = "DateRecepcion";
            DateRecepcion.ShowCheckBox = true;
            DateRecepcion.Size = new Size(228, 23);
            DateRecepcion.TabIndex = 7;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(16, 87);
            label4.Name = "label4";
            label4.Size = new Size(70, 15);
            label4.TabIndex = 6;
            label4.Text = "Documento";
            // 
            // CmbDocumento
            // 
            CmbDocumento.FormattingEnabled = true;
            CmbDocumento.Location = new Point(15, 105);
            CmbDocumento.Name = "CmbDocumento";
            CmbDocumento.Size = new Size(245, 23);
            CmbDocumento.TabIndex = 10;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(519, 43);
            label5.Name = "label5";
            label5.Size = new Size(61, 15);
            label5.TabIndex = 3;
            label5.Text = "No. Oficio";
            // 
            // contextMenuStrip1
            // 
            contextMenuStrip1.Name = "contextMenuStrip1";
            contextMenuStrip1.Size = new Size(61, 4);
            // 
            // TxtNumOficio
            // 
            TxtNumOficio.Location = new Point(519, 61);
            TxtNumOficio.Name = "TxtNumOficio";
            TxtNumOficio.Size = new Size(248, 23);
            TxtNumOficio.TabIndex = 8;
            // 
            // DateDocumento
            // 
            DateDocumento.Location = new Point(785, 61);
            DateDocumento.Name = "DateDocumento";
            DateDocumento.ShowCheckBox = true;
            DateDocumento.Size = new Size(222, 23);
            DateDocumento.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(784, 43);
            label6.Name = "label6";
            label6.Size = new Size(104, 15);
            label6.TabIndex = 4;
            label6.Text = "Fecha Documento";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(16, 131);
            label7.Name = "label7";
            label7.Size = new Size(61, 15);
            label7.TabIndex = 13;
            label7.Text = "Remitente";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(16, 177);
            label8.Name = "label8";
            label8.Size = new Size(70, 15);
            label8.TabIndex = 17;
            label8.Text = "Destinatario";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(519, 87);
            label9.Name = "label9";
            label9.Size = new Size(55, 15);
            label9.TabIndex = 15;
            label9.Text = "Prioridad";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(19, 229);
            label10.Name = "label10";
            label10.Size = new Size(50, 15);
            label10.TabIndex = 16;
            label10.Text = "Petición";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(17, 310);
            label11.Name = "label11";
            label11.Size = new Size(45, 15);
            label11.TabIndex = 17;
            label11.Text = "Asunto";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(517, 310);
            label12.Name = "label12";
            label12.Size = new Size(111, 15);
            label12.TabIndex = 18;
            label12.Text = "No. Control Folio(s)";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(516, 232);
            label13.Name = "label13";
            label13.Size = new Size(74, 15);
            label13.TabIndex = 19;
            label13.Text = "Clasificación";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(635, 310);
            label14.Name = "label14";
            label14.Size = new Size(90, 15);
            label14.TabIndex = 20;
            label14.Text = "Ubicación física";
            // 
            // CmbRemitente
            // 
            CmbRemitente.FormattingEnabled = true;
            CmbRemitente.Location = new Point(16, 149);
            CmbRemitente.Name = "CmbRemitente";
            CmbRemitente.Size = new Size(488, 23);
            CmbRemitente.TabIndex = 16;
            // 
            // CmbDestinatario
            // 
            CmbDestinatario.FormattingEnabled = true;
            CmbDestinatario.Location = new Point(17, 195);
            CmbDestinatario.Name = "CmbDestinatario";
            CmbDestinatario.Size = new Size(490, 23);
            CmbDestinatario.TabIndex = 18;
            // 
            // CmbPrioridad
            // 
            CmbPrioridad.FormattingEnabled = true;
            CmbPrioridad.Location = new Point(519, 105);
            CmbPrioridad.Name = "CmbPrioridad";
            CmbPrioridad.Size = new Size(248, 23);
            CmbPrioridad.TabIndex = 14;
            // 
            // TextAsunto
            // 
            TextAsunto.Location = new Point(16, 328);
            TextAsunto.Name = "TextAsunto";
            TextAsunto.Size = new Size(493, 96);
            TextAsunto.TabIndex = 22;
            TextAsunto.Text = "";
            // 
            // TextFolioIni
            // 
            TextFolioIni.Location = new Point(522, 328);
            TextFolioIni.Name = "TextFolioIni";
            TextFolioIni.Size = new Size(43, 23);
            TextFolioIni.TabIndex = 23;
            // 
            // TextFolioFin
            // 
            TextFolioFin.Location = new Point(582, 328);
            TextFolioFin.Name = "TextFolioFin";
            TextFolioFin.Size = new Size(43, 23);
            TextFolioFin.TabIndex = 24;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(566, 336);
            label15.Name = "label15";
            label15.Size = new Size(12, 15);
            label15.TabIndex = 28;
            label15.Text = "-";
            // 
            // TextUbicacion
            // 
            TextUbicacion.Location = new Point(636, 328);
            TextUbicacion.Name = "TextUbicacion";
            TextUbicacion.Size = new Size(371, 23);
            TextUbicacion.TabIndex = 25;
            // 
            // button1
            // 
            button1.Location = new Point(855, 475);
            button1.Name = "button1";
            button1.Size = new Size(147, 38);
            button1.TabIndex = 27;
            button1.Text = "Guardar";
            button1.UseVisualStyleBackColor = true;
            button1.Click += btnGuardar_Click;
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(0, 51, 102);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(1014, 31);
            panelHeader.TabIndex = 32;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(7, 9);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(134, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Captura de Oficios";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(785, 87);
            label16.Name = "label16";
            label16.Size = new Size(88, 15);
            label16.TabIndex = 33;
            label16.Text = "Requiere Ticket";
            // 
            // CmbTicket
            // 
            CmbTicket.FormattingEnabled = true;
            CmbTicket.Location = new Point(784, 105);
            CmbTicket.Name = "CmbTicket";
            CmbTicket.Size = new Size(223, 23);
            CmbTicket.TabIndex = 15;
            // 
            // TextPeticion
            // 
            TextPeticion.Location = new Point(17, 250);
            TextPeticion.Name = "TextPeticion";
            TextPeticion.Size = new Size(490, 50);
            TextPeticion.TabIndex = 20;
            TextPeticion.Text = "";
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(518, 354);
            label17.Name = "label17";
            label17.Size = new Size(38, 15);
            label17.TabIndex = 36;
            label17.Text = "Inicial";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(582, 354);
            label18.Name = "label18";
            label18.Size = new Size(32, 15);
            label18.TabIndex = 37;
            label18.Text = "Final";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(280, 87);
            label19.Name = "label19";
            label19.Size = new Size(54, 15);
            label19.TabIndex = 38;
            label19.Text = "Se recibe";
            // 
            // CmbSeRecibe
            // 
            CmbSeRecibe.FormattingEnabled = true;
            CmbSeRecibe.Location = new Point(280, 106);
            CmbSeRecibe.Name = "CmbSeRecibe";
            CmbSeRecibe.Size = new Size(227, 23);
            CmbSeRecibe.TabIndex = 11;
            // 
            // CmbClasif
            // 
            CmbClasif.FormattingEnabled = true;
            CmbClasif.Location = new Point(519, 250);
            CmbClasif.Name = "CmbClasif";
            CmbClasif.Size = new Size(488, 23);
            CmbClasif.TabIndex = 21;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(20, 448);
            label20.Name = "label20";
            label20.Size = new Size(72, 15);
            label20.TabIndex = 41;
            label20.Text = "Archivo PDF";
            // 
            // btnAdjuntar
            // 
            btnAdjuntar.Location = new Point(126, 436);
            btnAdjuntar.Name = "btnAdjuntar";
            btnAdjuntar.Size = new Size(120, 38);
            btnAdjuntar.TabIndex = 26;
            btnAdjuntar.Text = "Adjuntar";
            btnAdjuntar.UseVisualStyleBackColor = true;
            btnAdjuntar.Click += btnAdjuntar_Click;
            // 
            // btnQuitarAdjunto
            // 
            btnQuitarAdjunto.Location = new Point(252, 436);
            btnQuitarAdjunto.Name = "btnQuitarAdjunto";
            btnQuitarAdjunto.Size = new Size(140, 38);
            btnQuitarAdjunto.TabIndex = 42;
            btnQuitarAdjunto.Text = "Quitar Adjunto";
            btnQuitarAdjunto.UseVisualStyleBackColor = true;
            btnQuitarAdjunto.Click += btnQuitarAdjunto_Click;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(521, 133);
            label21.Name = "label21";
            label21.Size = new Size(96, 15);
            label21.TabIndex = 14;
            label21.Text = "Cargo Remitente";
            // 
            // cmbCargoRemitente
            // 
            cmbCargoRemitente.FormattingEnabled = true;
            cmbCargoRemitente.Location = new Point(522, 150);
            cmbCargoRemitente.Name = "cmbCargoRemitente";
            cmbCargoRemitente.Size = new Size(485, 23);
            cmbCargoRemitente.TabIndex = 17;
            // 
            // cmbCargoDestinatario
            // 
            cmbCargoDestinatario.FormattingEnabled = true;
            cmbCargoDestinatario.Location = new Point(522, 195);
            cmbCargoDestinatario.Name = "cmbCargoDestinatario";
            cmbCargoDestinatario.Size = new Size(485, 23);
            cmbCargoDestinatario.TabIndex = 19;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(521, 177);
            label22.Name = "label22";
            label22.Size = new Size(105, 15);
            label22.TabIndex = 46;
            label22.Text = "Cargo Destinatario";
            // 
            // btnVerArchivo
            // 
            btnVerArchivo.Location = new Point(401, 436);
            btnVerArchivo.Name = "btnVerArchivo";
            btnVerArchivo.Size = new Size(102, 38);
            btnVerArchivo.TabIndex = 47;
            btnVerArchivo.Text = "Ver PDF";
            btnVerArchivo.UseVisualStyleBackColor = true;
            btnVerArchivo.Click += btnVerArchivo_Click;
            // 
            // lblEstado
            // 
            lblEstado.AutoSize = true;
            lblEstado.Location = new Point(522, 380);
            lblEstado.Name = "lblEstado";
            lblEstado.Size = new Size(42, 15);
            lblEstado.TabIndex = 48;
            lblEstado.Text = "Estado";
            // 
            // cmbEstatus
            // 
            cmbEstatus.FormattingEnabled = true;
            cmbEstatus.Location = new Point(519, 402);
            cmbEstatus.Name = "cmbEstatus";
            cmbEstatus.Size = new Size(248, 23);
            cmbEstatus.TabIndex = 49;
            // 
            // lblhistorial
            // 
            lblhistorial.AutoSize = true;
            lblhistorial.Location = new Point(20, 498);
            lblhistorial.Name = "lblhistorial";
            lblhistorial.Size = new Size(105, 15);
            lblhistorial.TabIndex = 50;
            lblhistorial.Text = "Historial del Oficio";
            // 
            // txtSeguimiento
            // 
            txtSeguimiento.Location = new Point(19, 516);
            txtSeguimiento.Name = "txtSeguimiento";
            txtSeguimiento.Size = new Size(484, 96);
            txtSeguimiento.TabIndex = 51;
            txtSeguimiento.Text = "";
            // 
            // dataGridTickets
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dataGridTickets.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dataGridTickets.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridTickets.Location = new Point(20, 650);
            dataGridTickets.Name = "dataGridTickets";
            dataGridTickets.Size = new Size(475, 150);
            dataGridTickets.TabIndex = 52;
            // 
            // lblTickets
            // 
            lblTickets.AutoSize = true;
            lblTickets.Location = new Point(20, 632);
            lblTickets.Name = "lblTickets";
            lblTickets.Size = new Size(103, 15);
            lblTickets.TabIndex = 53;
            lblTickets.Text = "Tickets Generados";
            // 
            // FrmCapOficios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1014, 548);
            Controls.Add(lblTickets);
            Controls.Add(dataGridTickets);
            Controls.Add(txtSeguimiento);
            Controls.Add(lblhistorial);
            Controls.Add(cmbEstatus);
            Controls.Add(lblEstado);
            Controls.Add(btnVerArchivo);
            Controls.Add(label22);
            Controls.Add(cmbCargoDestinatario);
            Controls.Add(cmbCargoRemitente);
            Controls.Add(label21);
            Controls.Add(btnQuitarAdjunto);
            Controls.Add(btnAdjuntar);
            Controls.Add(label20);
            Controls.Add(CmbClasif);
            Controls.Add(CmbSeRecibe);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(TextPeticion);
            Controls.Add(CmbTicket);
            Controls.Add(label16);
            Controls.Add(panelHeader);
            Controls.Add(button1);
            Controls.Add(TextUbicacion);
            Controls.Add(label15);
            Controls.Add(TextFolioFin);
            Controls.Add(TextFolioIni);
            Controls.Add(TextAsunto);
            Controls.Add(CmbPrioridad);
            Controls.Add(CmbDestinatario);
            Controls.Add(CmbRemitente);
            Controls.Add(label14);
            Controls.Add(label13);
            Controls.Add(label12);
            Controls.Add(label11);
            Controls.Add(label10);
            Controls.Add(label9);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(DateDocumento);
            Controls.Add(TxtNumOficio);
            Controls.Add(label5);
            Controls.Add(CmbDocumento);
            Controls.Add(label4);
            Controls.Add(DateRecepcion);
            Controls.Add(label3);
            Controls.Add(CmbEjercicio);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(CmbTipo);
            Name = "FrmCapOficios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmCapOficios";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridTickets).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox CmbTipo;
        private Label label1;
        private Label label2;
        private ComboBox CmbEjercicio;
        private Label label3;
        private DateTimePicker DateRecepcion;
        private Label label4;
        private ComboBox CmbDocumento;
        private Label label5;
        private ContextMenuStrip contextMenuStrip1;
        private TextBox TxtNumOficio;
        private DateTimePicker DateDocumento;
        private Label label6;
        private Label label7;
        private Label label8;
        private Label label9;
        private Label label10;
        private Label label11;
        private Label label12;
        private Label label13;
        private Label label14;
        private ComboBox CmbRemitente;
        private ComboBox CmbDestinatario;
        private ComboBox CmbPrioridad;
        private RichTextBox TextAsunto;
        private TextBox TextFolioIni;
        private TextBox TextFolioFin;
        private Label label15;
        private TextBox TextUbicacion;
        private Button button1;
        private Panel panelHeader;
        private Label lblTitulo;
        private Label label16;
        private ComboBox CmbTicket;
        private RichTextBox TextPeticion;
        private Label label17;
        private Label label18;
        private Label label19;
        private ComboBox CmbSeRecibe;
        private ComboBox CmbClasif;
        private Label label20;
        private Button btnAdjuntar;
        private Button btnQuitarAdjunto;
        private Label label21;
        private ComboBox cmbCargoRemitente;
        private ComboBox cmbCargoDestinatario;
        private Label label22;
        private Button btnVerArchivo;
        private Label lblEstado;
        private ComboBox cmbEstatus;
        private Label lblhistorial;
        private RichTextBox txtSeguimiento;
        private DataGridView dataGridTickets;
        private Label lblTickets;
    }
}