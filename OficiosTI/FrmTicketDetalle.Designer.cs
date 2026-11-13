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
            lblOficina = new Label();
            lblTicketId = new Label();
            PanelDatos = new Panel();
            btnAsignarTicketOf = new Button();
            CmbAsignarOficina = new ComboBox();
            lblAsignarOficina = new Label();
            btnGuardaryCerrar = new Button();
            chkCerrarTicket = new CheckBox();
            txtRespuesta = new TextBox();
            BtnGenerarOficio = new Button();
            BtnGuardarRespuesta = new Button();
            labelRespuesta = new Label();
            btnAsignarTicket = new Button();
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
            PanelHeader.Controls.Add(lblOficina);
            PanelHeader.Controls.Add(lblTicketId);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(812, 50);
            PanelHeader.TabIndex = 4;
            // 
            // lblOficina
            // 
            lblOficina.AutoSize = true;
            lblOficina.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblOficina.ForeColor = Color.White;
            lblOficina.Location = new Point(258, 14);
            lblOficina.Name = "lblOficina";
            lblOficina.Size = new Size(65, 21);
            lblOficina.TabIndex = 1;
            lblOficina.Text = "Oficina";
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
            PanelDatos.Controls.Add(btnAsignarTicketOf);
            PanelDatos.Controls.Add(CmbAsignarOficina);
            PanelDatos.Controls.Add(lblAsignarOficina);
            PanelDatos.Controls.Add(btnGuardaryCerrar);
            PanelDatos.Controls.Add(chkCerrarTicket);
            PanelDatos.Controls.Add(txtRespuesta);
            PanelDatos.Controls.Add(BtnGenerarOficio);
            PanelDatos.Controls.Add(BtnGuardarRespuesta);
            PanelDatos.Controls.Add(labelRespuesta);
            PanelDatos.Controls.Add(btnAsignarTicket);
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
            PanelDatos.Size = new Size(812, 371);
            PanelDatos.TabIndex = 1;
            // 
            // btnAsignarTicketOf
            // 
            btnAsignarTicketOf.Location = new Point(707, 119);
            btnAsignarTicketOf.Name = "btnAsignarTicketOf";
            btnAsignarTicketOf.Size = new Size(96, 23);
            btnAsignarTicketOf.TabIndex = 18;
            btnAsignarTicketOf.Text = "AsignarOficina";
            btnAsignarTicketOf.UseVisualStyleBackColor = true;
            btnAsignarTicketOf.Click += btnAsignarTicketOf_Click;
            // 
            // CmbAsignarOficina
            // 
            CmbAsignarOficina.BackColor = SystemColors.Window;
            CmbAsignarOficina.FormattingEnabled = true;
            CmbAsignarOficina.Location = new Point(278, 174);
            CmbAsignarOficina.Name = "CmbAsignarOficina";
            CmbAsignarOficina.Size = new Size(240, 23);
            CmbAsignarOficina.TabIndex = 17;
            // 
            // lblAsignarOficina
            // 
            lblAsignarOficina.AutoSize = true;
            lblAsignarOficina.Location = new Point(274, 150);
            lblAsignarOficina.Name = "lblAsignarOficina";
            lblAsignarOficina.Size = new Size(111, 15);
            lblAsignarOficina.TabIndex = 16;
            lblAsignarOficina.Text = "Reasignar a Oficina ";
            // 
            // btnGuardaryCerrar
            // 
            btnGuardaryCerrar.BackColor = SystemColors.Window;
            btnGuardaryCerrar.Location = new Point(538, 317);
            btnGuardaryCerrar.Name = "btnGuardaryCerrar";
            btnGuardaryCerrar.Size = new Size(150, 46);
            btnGuardaryCerrar.TabIndex = 15;
            btnGuardaryCerrar.Text = "Guardar Respuesta y Cerrar Ticket";
            btnGuardaryCerrar.UseVisualStyleBackColor = false;
            btnGuardaryCerrar.Click += btnGuardaryCerrar_Click;
            // 
            // chkCerrarTicket
            // 
            chkCerrarTicket.AutoSize = true;
            chkCerrarTicket.BackColor = Color.WhiteSmoke;
            chkCerrarTicket.Location = new Point(15, 323);
            chkCerrarTicket.Name = "chkCerrarTicket";
            chkCerrarTicket.Size = new Size(109, 19);
            chkCerrarTicket.TabIndex = 2;
            chkCerrarTicket.Text = "Cerrar sin oficio";
            chkCerrarTicket.UseVisualStyleBackColor = false;
            // 
            // txtRespuesta
            // 
            txtRespuesta.Location = new Point(12, 234);
            txtRespuesta.Multiline = true;
            txtRespuesta.Name = "txtRespuesta";
            txtRespuesta.Size = new Size(794, 77);
            txtRespuesta.TabIndex = 1;
            // 
            // BtnGenerarOficio
            // 
            BtnGenerarOficio.BackColor = SystemColors.Window;
            BtnGenerarOficio.Location = new Point(650, 167);
            BtnGenerarOficio.Name = "BtnGenerarOficio";
            BtnGenerarOficio.Size = new Size(150, 30);
            BtnGenerarOficio.TabIndex = 1;
            BtnGenerarOficio.Text = "Ver Oficio Solicitud";
            BtnGenerarOficio.UseVisualStyleBackColor = false;
            BtnGenerarOficio.Click += BtnGenerarOficio_Click;
            // 
            // BtnGuardarRespuesta
            // 
            BtnGuardarRespuesta.BackColor = SystemColors.Window;
            BtnGuardarRespuesta.Location = new Point(377, 317);
            BtnGuardarRespuesta.Name = "BtnGuardarRespuesta";
            BtnGuardarRespuesta.Size = new Size(150, 48);
            BtnGuardarRespuesta.TabIndex = 0;
            BtnGuardarRespuesta.Text = "Guardar Respuesta";
            BtnGuardarRespuesta.UseVisualStyleBackColor = false;
            BtnGuardarRespuesta.Click += BtnGuardarRespuesta_Click;
            // 
            // labelRespuesta
            // 
            labelRespuesta.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelRespuesta.Location = new Point(14, 211);
            labelRespuesta.Name = "labelRespuesta";
            labelRespuesta.Size = new Size(221, 19);
            labelRespuesta.TabIndex = 0;
            labelRespuesta.Text = "Agregar Respuesta/Instrucción";
            // 
            // btnAsignarTicket
            // 
            btnAsignarTicket.BackColor = SystemColors.Window;
            btnAsignarTicket.Location = new Point(177, 317);
            btnAsignarTicket.Name = "btnAsignarTicket";
            btnAsignarTicket.Size = new Size(189, 48);
            btnAsignarTicket.TabIndex = 13;
            btnAsignarTicket.Text = "Asignar / Responder Ticket";
            btnAsignarTicket.UseVisualStyleBackColor = false;
            btnAsignarTicket.Click += btnAsignarTicket_Click;
            // 
            // cmbAnalistas
            // 
            cmbAnalistas.BackColor = SystemColors.Window;
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
            label4.Size = new Size(105, 15);
            label4.TabIndex = 10;
            label4.Text = "Asignar a analista: ";
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
            txtPrioridad.BackColor = SystemColors.Window;
            txtPrioridad.Location = new Point(700, 90);
            txtPrioridad.Name = "txtPrioridad";
            txtPrioridad.Size = new Size(100, 23);
            txtPrioridad.TabIndex = 8;
            // 
            // txtExtension
            // 
            txtExtension.BackColor = SystemColors.Window;
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
            txtPersona.BackColor = SystemColors.Window;
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
            txtAsunto.BackColor = SystemColors.Window;
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
            txtMensaje.BackColor = SystemColors.Window;
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
            PanelHilo.Location = new Point(0, 421);
            PanelHilo.Name = "PanelHilo";
            PanelHilo.Size = new Size(812, 410);
            PanelHilo.TabIndex = 0;
            // 
            // dataGridRelacion
            // 
            dataGridRelacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridRelacion.Location = new Point(12, 242);
            dataGridRelacion.Name = "dataGridRelacion";
            dataGridRelacion.Size = new Size(788, 114);
            dataGridRelacion.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(12, 218);
            label1.Name = "label1";
            label1.Size = new Size(180, 18);
            label1.TabIndex = 2;
            label1.Text = "Tickets Relacionados";
            // 
            // labelHilo
            // 
            labelHilo.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            labelHilo.Location = new Point(12, 6);
            labelHilo.Name = "labelHilo";
            labelHilo.Size = new Size(149, 18);
            labelHilo.TabIndex = 0;
            labelHilo.Text = "Hilo del Ticket";
            // 
            // dataGridHilo
            // 
            dataGridHilo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHilo.Location = new Point(12, 28);
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
            ClientSize = new Size(812, 831);
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
        private Button btnAsignarTicket;
        private Button btnGuardaryCerrar;
        private ComboBox CmbAsignarOficina;
        private Label lblAsignarOficina;
        private Button btnAsignarTicketOf;
        private Label lblOficina;
    }
}