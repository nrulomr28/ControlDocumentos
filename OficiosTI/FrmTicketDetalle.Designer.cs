namespace OficiosTI
{
    partial class FrmTicketDetalle
    {
        private System.ComponentModel.IContainer components = null;

        private Panel PanelHeader;
        private Label lblTicketId;

        private Panel PanelDatos;
        private Label labelPersona;
        private Label labelAsunto;
        private Label labelMensaje;

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
            label5 = new Label();
            btnAsignarTicket = new Button();
            txtInstruccion = new RichTextBox();
            cmbAnalistas = new ComboBox();
            label4 = new Label();
            label3 = new Label();
            txtPrioridad = new TextBox();
            txtExtension = new TextBox();
            label2 = new Label();
            labelPersona = new Label();
            txtPersona = new TextBox();
            labelAsunto = new Label();
            txtAsunto = new TextBox();
            labelMensaje = new Label();
            txtMensaje = new TextBox();
            PanelHilo = new Panel();
            dataGridRelacion = new DataGridView();
            label1 = new Label();
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
            PanelDatos.Controls.Add(label5);
            PanelDatos.Controls.Add(btnAsignarTicket);
            PanelDatos.Controls.Add(txtInstruccion);
            PanelDatos.Controls.Add(cmbAnalistas);
            PanelDatos.Controls.Add(label4);
            PanelDatos.Controls.Add(label3);
            PanelDatos.Controls.Add(txtPrioridad);
            PanelDatos.Controls.Add(txtExtension);
            PanelDatos.Controls.Add(label2);
            PanelDatos.Controls.Add(labelPersona);
            PanelDatos.Controls.Add(txtPersona);
            PanelDatos.Controls.Add(labelAsunto);
            PanelDatos.Controls.Add(txtAsunto);
            PanelDatos.Controls.Add(labelMensaje);
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
            labelRespuesta.Text = "Agregar Respuesta";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(264, 150);
            label5.Name = "label5";
            label5.Size = new Size(66, 15);
            label5.TabIndex = 14;
            label5.Text = "Instrucción";
            // 
            // btnAsignarTicket
            // 
            btnAsignarTicket.Location = new Point(702, 214);
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
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 150);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 10;
            label4.Text = "Asignar a: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(702, 70);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 9;
            label3.Text = "Prioridad";
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
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(700, 15);
            label2.Name = "label2";
            label2.Size = new Size(57, 15);
            label2.TabIndex = 6;
            label2.Text = "Extensión";
            // 
            // labelPersona
            // 
            labelPersona.Location = new Point(15, 15);
            labelPersona.Name = "labelPersona";
            labelPersona.Size = new Size(100, 23);
            labelPersona.TabIndex = 0;
            labelPersona.Text = "Persona";
            // 
            // txtPersona
            // 
            txtPersona.Location = new Point(15, 35);
            txtPersona.Name = "txtPersona";
            txtPersona.ReadOnly = true;
            txtPersona.Size = new Size(300, 23);
            txtPersona.TabIndex = 1;
            // 
            // labelAsunto
            // 
            labelAsunto.Location = new Point(330, 15);
            labelAsunto.Name = "labelAsunto";
            labelAsunto.Size = new Size(100, 23);
            labelAsunto.TabIndex = 2;
            labelAsunto.Text = "Asunto";
            // 
            // txtAsunto
            // 
            txtAsunto.Location = new Point(330, 35);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.ReadOnly = true;
            txtAsunto.Size = new Size(355, 23);
            txtAsunto.TabIndex = 3;
            // 
            // labelMensaje
            // 
            labelMensaje.Location = new Point(15, 70);
            labelMensaje.Name = "labelMensaje";
            labelMensaje.Size = new Size(100, 23);
            labelMensaje.TabIndex = 4;
            labelMensaje.Text = "Mensaje";
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
            PanelHilo.Controls.Add(label1);
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
            // label1
            // 
            label1.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 258);
            label1.Name = "label1";
            label1.Size = new Size(180, 18);
            label1.TabIndex = 2;
            label1.Text = "Tickets Relacionados";
            // 
            // labelHilo
            // 
            labelHilo.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHilo.Location = new Point(12, 26);
            labelHilo.Name = "labelHilo";
            labelHilo.Size = new Size(149, 18);
            labelHilo.TabIndex = 0;
            labelHilo.Text = "Hilo del Ticket";
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
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            PanelDatos.ResumeLayout(false);
            PanelDatos.PerformLayout();
            PanelHilo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridRelacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridHilo).EndInit();
            ResumeLayout(false);
        }

        private Label label1;
        private DataGridView dataGridRelacion;
        private CheckBox chkCerrarTicket;
        private TextBox txtPrioridad;
        private TextBox txtExtension;
        private Label label2;
        private ComboBox cmbAnalistas;
        private Label label4;
        private Label label3;
        private RichTextBox txtInstruccion;
        private Button btnAsignarTicket;
        private Label label5;
        private Button btnGuardaryCerrar;
    }
}