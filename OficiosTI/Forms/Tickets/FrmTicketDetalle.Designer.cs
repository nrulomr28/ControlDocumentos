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

        private Panel PanelRespuesta;
        private Label labelRespuesta;
        private TextBox txtRespuesta;

        private Panel PanelAcciones;
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
            PanelRespuesta = new Panel();
            labelRespuesta = new Label();
            txtRespuesta = new TextBox();
            PanelAcciones = new Panel();
            BtnGuardarRespuesta = new Button();
            BtnGenerarOficio = new Button();
            PanelHeader.SuspendLayout();
            PanelDatos.SuspendLayout();
            PanelHilo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridRelacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridHilo).BeginInit();
            PanelRespuesta.SuspendLayout();
            PanelAcciones.SuspendLayout();
            SuspendLayout();
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.FromArgb(31, 58, 95);
            PanelHeader.Controls.Add(lblTicketId);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(755, 50);
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
            PanelDatos.Controls.Add(labelPersona);
            PanelDatos.Controls.Add(txtPersona);
            PanelDatos.Controls.Add(labelAsunto);
            PanelDatos.Controls.Add(txtAsunto);
            PanelDatos.Controls.Add(labelMensaje);
            PanelDatos.Controls.Add(txtMensaje);
            PanelDatos.Dock = DockStyle.Top;
            PanelDatos.Location = new Point(0, 50);
            PanelDatos.Name = "PanelDatos";
            PanelDatos.Size = new Size(755, 150);
            PanelDatos.TabIndex = 1;
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
            txtAsunto.Size = new Size(400, 23);
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
            txtMensaje.Size = new Size(715, 45);
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
            PanelHilo.Location = new Point(0, 200);
            PanelHilo.Name = "PanelHilo";
            PanelHilo.Size = new Size(755, 427);
            PanelHilo.TabIndex = 0;
            // 
            // dataGridRelacion
            // 
            dataGridRelacion.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridRelacion.Location = new Point(15, 256);
            dataGridRelacion.Name = "dataGridRelacion";
            dataGridRelacion.Size = new Size(715, 150);
            dataGridRelacion.TabIndex = 3;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.Location = new Point(15, 230);
            label1.Name = "label1";
            label1.Size = new Size(180, 23);
            label1.TabIndex = 2;
            label1.Text = "Tickets Relacionados";
            // 
            // labelHilo
            // 
            labelHilo.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelHilo.Location = new Point(15, 10);
            labelHilo.Name = "labelHilo";
            labelHilo.Size = new Size(100, 23);
            labelHilo.TabIndex = 0;
            labelHilo.Text = "Hilo del Ticket";
            // 
            // dataGridHilo
            // 
            dataGridHilo.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridHilo.Location = new Point(15, 35);
            dataGridHilo.MultiSelect = false;
            dataGridHilo.Name = "dataGridHilo";
            dataGridHilo.ReadOnly = true;
            dataGridHilo.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridHilo.Size = new Size(715, 182);
            dataGridHilo.TabIndex = 1;
            // 
            // PanelRespuesta
            // 
            PanelRespuesta.BackColor = Color.FromArgb(244, 246, 249);
            PanelRespuesta.Controls.Add(labelRespuesta);
            PanelRespuesta.Controls.Add(txtRespuesta);
            PanelRespuesta.Dock = DockStyle.Bottom;
            PanelRespuesta.Location = new Point(0, 627);
            PanelRespuesta.Name = "PanelRespuesta";
            PanelRespuesta.Size = new Size(755, 120);
            PanelRespuesta.TabIndex = 2;
            // 
            // labelRespuesta
            // 
            labelRespuesta.Location = new Point(15, 10);
            labelRespuesta.Name = "labelRespuesta";
            labelRespuesta.Size = new Size(100, 23);
            labelRespuesta.TabIndex = 0;
            labelRespuesta.Text = "Nueva respuesta";
            // 
            // txtRespuesta
            // 
            txtRespuesta.Location = new Point(15, 30);
            txtRespuesta.Multiline = true;
            txtRespuesta.Name = "txtRespuesta";
            txtRespuesta.Size = new Size(715, 60);
            txtRespuesta.TabIndex = 1;
            // 
            // PanelAcciones
            // 
            PanelAcciones.BackColor = Color.WhiteSmoke;
            PanelAcciones.Controls.Add(BtnGuardarRespuesta);
            PanelAcciones.Controls.Add(BtnGenerarOficio);
            PanelAcciones.Dock = DockStyle.Bottom;
            PanelAcciones.Location = new Point(0, 747);
            PanelAcciones.Name = "PanelAcciones";
            PanelAcciones.Size = new Size(755, 50);
            PanelAcciones.TabIndex = 3;
            // 
            // BtnGuardarRespuesta
            // 
            BtnGuardarRespuesta.Location = new Point(15, 10);
            BtnGuardarRespuesta.Name = "BtnGuardarRespuesta";
            BtnGuardarRespuesta.Size = new Size(150, 30);
            BtnGuardarRespuesta.TabIndex = 0;
            BtnGuardarRespuesta.Text = "Guardar Respuesta";
            BtnGuardarRespuesta.Click += BtnGuardarRespuesta_Click;
            // 
            // BtnGenerarOficio
            // 
            BtnGenerarOficio.Location = new Point(180, 10);
            BtnGenerarOficio.Name = "BtnGenerarOficio";
            BtnGenerarOficio.Size = new Size(150, 30);
            BtnGenerarOficio.TabIndex = 1;
            BtnGenerarOficio.Text = "Generar Oficio";
            // 
            // FrmTicketDetalle
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(755, 797);
            Controls.Add(PanelHilo);
            Controls.Add(PanelDatos);
            Controls.Add(PanelRespuesta);
            Controls.Add(PanelAcciones);
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
            PanelRespuesta.ResumeLayout(false);
            PanelRespuesta.PerformLayout();
            PanelAcciones.ResumeLayout(false);
            ResumeLayout(false);
        }

        private Label label1;
        private DataGridView dataGridRelacion;
    }
}