namespace OficiosTI
{
    partial class FrmTicketDetalle
    {
        private System.ComponentModel.IContainer components = null;

        private Panel PanelHeader;
        private Label lblTicketId;

        private Panel PanelDatos;
        private Label lblRemitente;
        private Label lblAsunto;
        private Label lblMensajeOficio;

        private TextBox txtPersona;
        private TextBox txtAsunto;
        private TextBox txtMensaje;

        private Panel PanelHilo;
        private Label labelHilo;
        private DataGridView dataGridHilo;
        private Label labelRespuesta;
        private TextBox txtRespuesta;
        private Button BtnGuardarRespuesta;
        private Button BtnGenerarOficio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            PanelHeader = new Panel();
            lblTicketId = new Label();
            PanelDatos = new Panel();
            btnGuardaryCerrar = new Button();
            chkCerrarTicket = new CheckBox();
            txtRespuesta = new TextBox();
            BtnGenerarOficio = new Button();
            BtnGuardarRespuesta = new Button();
            labelRespuesta = new Label();
            lblInstruccion = new Label();
            btnAsignarTicket = new Button();
            txtInstruccion = new RichTextBox();
            cmbAnalistas = new ComboBox();
            lblAsignar = new Label();
            lblPrioridad = new Label();
            txtPrioridad = new TextBox();
            txtExtension = new TextBox();
            lblExtension = new Label();
            lblRemitente = new Label();
            txtPersona = new TextBox();
            lblMensajeOficio = new Label();
            txtAsunto = new TextBox();
            lblAsunto = new Label();
            txtMensaje = new TextBox();
            PanelHilo = new Panel();
            dataGridRelacion = new DataGridView();
            lblTicketsRelacionados = new Label();
            labelHilo = new Label();
            dataGridHilo = new DataGridView();
            PanelHeader.SuspendLayout();
            PanelDatos.SuspendLayout();
            PanelHilo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridRelacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridHilo).BeginInit();
            SuspendLayout();
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.FromArgb(31, 58, 95);
            PanelHeader.Controls.Add(lblTicketId);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(817, 50);
            PanelHeader.TabIndex = 4;
            // 
            // lblTicketId
            // 
            lblTicketId.AutoSize = true;
            lblTicketId.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            lblTicketId.ForeColor = Color.White;
            lblTicketId.Location = new Point(15, 14);
            lblTicketId.Name = "lblTicketId";
            lblTicketId.Size = new Size(69, 21);
            lblTicketId.TabIndex = 0;
            lblTicketId.Text = "Ticket #";
            // 
            // PanelDatos
            // 
            PanelDatos.BackColor = Color.WhiteSmoke;
            PanelDatos.Controls.Add(btnGuardaryCerrar);
            PanelDatos.Controls.Add(chkCerrarTicket);
            PanelDatos.Controls.Add(txtRespuesta);
            PanelDatos.Controls.Add(BtnGenerarOficio);
            PanelDatos.Controls.Add(BtnGuardarRespuesta);
            PanelDatos.Controls.Add(labelRespuesta);
            PanelDatos.Controls.Add(lblInstruccion);
            PanelDatos.Controls.Add(btnAsignarTicket);
            PanelDatos.Controls.Add(txtInstruccion);
            PanelDatos.Controls.Add(cmbAnalistas);
            PanelDatos.Controls.Add(lblAsignar);
            PanelDatos.Controls.Add(lblPrioridad);
            PanelDatos.Controls.Add(txtPrioridad);
            PanelDatos.Controls.Add(txtExtension);
            PanelDatos.Controls.Add(lblExtension);
            PanelDatos.Controls.Add(lblRemitente);
            PanelDatos.Controls.Add(txtPersona);
            PanelDatos.Controls.Add(lblMensajeOficio);
            PanelDatos.Controls.Add(txtAsunto);
            PanelDatos.Controls.Add(lblAsunto);
            PanelDatos.Controls.Add(txtMensaje);
            PanelDatos.Dock = DockStyle.Top;
            PanelDatos.Location = new Point(0, 50);
            PanelDatos.Name = "PanelDatos";
            PanelDatos.Size = new Size(817, 425);
            PanelDatos.TabIndex = 1;
            // 
            // btnGuardaryCerrar
            // 
            btnGuardaryCerrar.Location = new Point(339, 371);
            btnGuardaryCerrar.Name = "btnGuardaryCerrar";
            btnGuardaryCerrar.Size = new Size(150, 30);
            btnGuardaryCerrar.TabIndex = 15;
            btnGuardaryCerrar.Text = "Guardar y Cerrar Ticket";
            btnGuardaryCerrar.UseVisualStyleBackColor = true;
            btnGuardaryCerrar.Click += btnGuardaryCerrar_Click;
            // 
            // chkCerrarTicket
            // 
            chkCerrarTicket.AutoSize = true;
            chkCerrarTicket.Location = new Point(15, 369);
            chkCerrarTicket.Name = "chkCerrarTicket";
            chkCerrarTicket.Size = new Size(109, 19);
            chkCerrarTicket.TabIndex = 2;
            chkCerrarTicket.Text = "Cerrar sin oficio";
            chkCerrarTicket.UseVisualStyleBackColor = true;
            // 
            // txtRespuesta
            // 
            txtRespuesta.Location = new Point(12, 303);
            txtRespuesta.Multiline = true;
            txtRespuesta.Name = "txtRespuesta";
            txtRespuesta.Size = new Size(794, 60);
            txtRespuesta.TabIndex = 1;
            // 
            // BtnGenerarOficio
            // 
            BtnGenerarOficio.Location = new Point(519, 371);
            BtnGenerarOficio.Name = "BtnGenerarOficio";
            BtnGenerarOficio.Size = new Size(150, 30);
            BtnGenerarOficio.TabIndex = 1;
            BtnGenerarOficio.Text = "Ver Oficio Solicitud";
            BtnGenerarOficio.Click += BtnGenerarOficio_Click;
            // 
            // BtnGuardarRespuesta
            // 
            BtnGuardarRespuesta.Location = new Point(167, 370);
            BtnGuardarRespuesta.Name = "BtnGuardarRespuesta";
            BtnGuardarRespuesta.Size = new Size(150, 30);
            BtnGuardarRespuesta.TabIndex = 0;
            BtnGuardarRespuesta.Text = "Guardar Respuesta";
            BtnGuardarRespuesta.Click += BtnGuardarRespuesta_Click;
            // 
            // labelRespuesta
            // 
            labelRespuesta.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRespuesta.Location = new Point(15, 277);
            labelRespuesta.Name = "labelRespuesta";
            labelRespuesta.Size = new Size(122, 23);
            labelRespuesta.TabIndex = 0;
            labelRespuesta.Text = "Agregar respuesta";
            // 
            // lblInstruccion
            // 
            lblInstruccion.AutoSize = true;
            lblInstruccion.Location = new Point(264, 150);
            lblInstruccion.Name = "lblInstruccion";
            lblInstruccion.Size = new Size(66, 15);
            lblInstruccion.TabIndex = 14;
            lblInstruccion.Text = "Instrucción";
            // 
            // btnAsignarTicket
            // 
            btnAsignarTicket.Location = new Point(15, 204);
            btnAsignarTicket.Name = "btnAsignarTicket";
            btnAsignarTicket.Size = new Size(98, 23);
            btnAsignarTicket.TabIndex = 13;
            btnAsignarTicket.Text = "Asignar Ticket";
            btnAsignarTicket.UseVisualStyleBackColor = true;
            btnAsignarTicket.Click += btnAsignarTicket_Click;
            // 
            // txtInstruccion
            // 
            txtInstruccion.Location = new Point(264, 170);
            txtInstruccion.Name = "txtInstruccion";
            txtInstruccion.Size = new Size(421, 96);
            txtInstruccion.TabIndex = 12;
            txtInstruccion.Text = "";
            // 
            // cmbAnalistas
            // 
            cmbAnalistas.BackColor = SystemColors.Control;
            cmbAnalistas.FormattingEnabled = true;
            cmbAnalistas.Location = new Point(15, 175);
            cmbAnalistas.Name = "cmbAnalistas";
            cmbAnalistas.Size = new Size(243, 23);
            cmbAnalistas.TabIndex = 11;
            // 
            // lblAsignar
            // 
            lblAsignar.AutoSize = true;
            lblAsignar.Location = new Point(15, 150);
            lblAsignar.Name = "lblAsignar";
            lblAsignar.Size = new Size(62, 15);
            lblAsignar.TabIndex = 10;
            lblAsignar.Text = "Asignar a: ";
            // 
            // lblPrioridad
            // 
            lblPrioridad.AutoSize = true;
            lblPrioridad.Location = new Point(702, 70);
            lblPrioridad.Name = "lblPrioridad";
            lblPrioridad.Size = new Size(55, 15);
            lblPrioridad.TabIndex = 9;
            lblPrioridad.Text = "Prioridad";
            // 
            // txtPrioridad
            // 
            txtPrioridad.BackColor = SystemColors.Control;
            txtPrioridad.Location = new Point(700, 90);
            txtPrioridad.Name = "txtPrioridad";
            txtPrioridad.Size = new Size(100, 23);
            txtPrioridad.TabIndex = 8;
            // 
            // txtExtension
            // 
            txtExtension.BackColor = SystemColors.Control;
            txtExtension.Location = new Point(700, 35);
            txtExtension.Name = "txtExtension";
            txtExtension.Size = new Size(100, 23);
            txtExtension.TabIndex = 7;
            // 
            // lblExtension
            // 
            lblExtension.AutoSize = true;
            lblExtension.Location = new Point(700, 15);
            lblExtension.Name = "lblExtension";
            lblExtension.Size = new Size(57, 15);
            lblExtension.TabIndex = 6;
            lblExtension.Text = "Extensión";
            // 
            // lblRemitente
            // 
            lblRemitente.Location = new Point(15, 15);
            lblRemitente.Name = "lblRemitente";
            lblRemitente.Size = new Size(100, 23);
            lblRemitente.TabIndex = 0;
            lblRemitente.Text = "Remitente";
            // 
            // txtPersona
            // 
            txtPersona.Location = new Point(15, 35);
            txtPersona.Name = "txtPersona";
            txtPersona.ReadOnly = true;
            txtPersona.Size = new Size(300, 23);
            txtPersona.TabIndex = 1;
            // 
            // lblMensajeOficio
            // 
            lblMensajeOficio.Location = new Point(330, 15);
            lblMensajeOficio.Name = "lblMensajeOficio";
            lblMensajeOficio.Size = new Size(355, 23);
            lblMensajeOficio.TabIndex = 2;
            lblMensajeOficio.Text = "Mensaje / Oficio: ";
            // 
            // txtAsunto
            // 
            txtAsunto.Location = new Point(330, 35);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.ReadOnly = true;
            txtAsunto.Size = new Size(355, 23);
            txtAsunto.TabIndex = 3;
            // 
            // lblAsunto
            // 
            lblAsunto.Location = new Point(15, 70);
            lblAsunto.Name = "lblAsunto";
            lblAsunto.Size = new Size(100, 23);
            lblAsunto.TabIndex = 4;
            lblAsunto.Text = "Asunto:";
            // 
            // txtMensaje
            // 
            txtMensaje.Location = new Point(15, 90);
            txtMensaje.Multiline = true;
            txtMensaje.Name = "txtMensaje";
            txtMensaje.ReadOnly = true;
            txtMensaje.Size = new Size(670, 47);
            txtMensaje.TabIndex = 5;
            // 
            // PanelHilo
            // 
            PanelHilo.BackColor = Color.White;
            PanelHilo.Controls.Add(dataGridRelacion);
            PanelHilo.Controls.Add(lblTicketsRelacionados);
            PanelHilo.Controls.Add(labelHilo);
            PanelHilo.Controls.Add(dataGridHilo);
            PanelHilo.Dock = DockStyle.Fill;
            PanelHilo.Location = new Point(0, 475);
            PanelHilo.Name = "PanelHilo";
            PanelHilo.Size = new Size(817, 420);
            PanelHilo.TabIndex = 0;
            // 
            // dataGridRelacion
            // 
            dataGridRelacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridRelacion.Location = new Point(15, 279);
            dataGridRelacion.Name = "dataGridRelacion";
            dataGridRelacion.Size = new Size(788, 114);
            dataGridRelacion.TabIndex = 3;
            // 
            // lblTicketsRelacionados
            // 
            lblTicketsRelacionados.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTicketsRelacionados.Location = new Point(12, 258);
            lblTicketsRelacionados.Name = "lblTicketsRelacionados";
            lblTicketsRelacionados.Size = new Size(180, 18);
            lblTicketsRelacionados.TabIndex = 2;
            lblTicketsRelacionados.Text = "Tickets Relacionados";
            // 
            // labelHilo
            // 
            labelHilo.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHilo.Location = new Point(12, 26);
            labelHilo.Name = "labelHilo";
            labelHilo.Size = new Size(149, 18);
            labelHilo.TabIndex = 0;
            labelHilo.Text = "Hilo del ticket";
            // 
            // dataGridHilo
            // 
            dataGridHilo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHilo.Location = new Point(12, 47);
            dataGridHilo.MultiSelect = false;
            dataGridHilo.Name = "dataGridHilo";
            dataGridHilo.ReadOnly = true;
            dataGridHilo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridHilo.Size = new Size(788, 177);
            dataGridHilo.TabIndex = 1;
            // 
            // FrmTicketDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(817, 895);
            Controls.Add(PanelHilo);
            Controls.Add(PanelDatos);
            Controls.Add(PanelHeader);
            Name = "FrmTicketDetalle";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Detalle del Ticket";
            FormClosing += FrmTicketDetalle_FormClosing;
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            PanelDatos.ResumeLayout(false);
            PanelDatos.PerformLayout();
            PanelHilo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridRelacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridHilo).EndInit();
            ResumeLayout(false);
        }

        private Label lblTicketsRelacionados;
        private DataGridView dataGridRelacion;
        private CheckBox chkCerrarTicket;
        private TextBox txtPrioridad;
        private TextBox txtExtension;
        private Label lblAsignar;
        private ComboBox cmbAnalistas;
        private Label lblExtension;
        private Label lblPrioridad;
        private RichTextBox txtInstruccion;
        private Button btnAsignarTicket;
        private Label lblInstruccion;
        private Button btnGuardaryCerrar;
    }
}