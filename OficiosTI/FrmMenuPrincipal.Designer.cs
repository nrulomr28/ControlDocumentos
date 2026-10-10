namespace OficiosTI
{
    partial class FrmMenuPrincipal
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
            lblBienvenida = new Label();
            btnCapturarOficio = new Button();
            btnPanelAdmin = new Button();
            DataGridOficios = new DataGridView();
            txtBuscar = new TextBox();
            btnbuscar = new Button();
            label1 = new Label();
            panel1 = new Panel();
            label5 = new Label();
            cmbTipoCaptura = new ComboBox();
            label4 = new Label();
            cmbEstado = new ComboBox();
            label3 = new Label();
            label2 = new Label();
            lblTicketFaltante = new Label();
            lblEnProcesoTotal = new Label();
            lblCerradosTotal = new Label();
            lblAbiertosTotal = new Label();
            lblEnProceso = new Label();
            lblCerrados = new Label();
            lblAbiertos = new Label();
            ((System.ComponentModel.ISupportInitialize)DataGridOficios).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // lblBienvenida
            // 
            lblBienvenida.AutoSize = true;
            lblBienvenida.Font = new Font("Gotham", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblBienvenida.ForeColor = SystemColors.HotTrack;
            lblBienvenida.Location = new Point(161, 27);
            lblBienvenida.Name = "lblBienvenida";
            lblBienvenida.Size = new Size(62, 23);
            lblBienvenida.TabIndex = 0;
            lblBienvenida.Text = "label1";
            // 
            // btnCapturarOficio
            // 
            btnCapturarOficio.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCapturarOficio.Location = new Point(3, 242);
            btnCapturarOficio.Name = "btnCapturarOficio";
            btnCapturarOficio.Size = new Size(133, 45);
            btnCapturarOficio.TabIndex = 1;
            btnCapturarOficio.Text = "Capturar Oficio ";
            btnCapturarOficio.UseVisualStyleBackColor = true;
            btnCapturarOficio.Click += btnCapturarOficio_Click;
            // 
            // btnPanelAdmin
            // 
            btnPanelAdmin.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPanelAdmin.Location = new Point(3, 293);
            btnPanelAdmin.Name = "btnPanelAdmin";
            btnPanelAdmin.Size = new Size(133, 45);
            btnPanelAdmin.TabIndex = 2;
            btnPanelAdmin.Text = "Tickets";
            btnPanelAdmin.UseVisualStyleBackColor = true;
            btnPanelAdmin.Click += btnPanelAdmin_Click;
            // 
            // DataGridOficios
            // 
            DataGridOficios.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            DataGridOficios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            DataGridOficios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            DataGridOficios.Location = new Point(158, 53);
            DataGridOficios.Name = "DataGridOficios";
            DataGridOficios.Size = new Size(1301, 689);
            DataGridOficios.TabIndex = 3;
            DataGridOficios.CellFormatting += DataGridOficios_CellFormatting;
            // 
            // txtBuscar
            // 
            txtBuscar.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.Location = new Point(3, 69);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(146, 22);
            txtBuscar.TabIndex = 4;
            // 
            // btnbuscar
            // 
            btnbuscar.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnbuscar.Location = new Point(3, 204);
            btnbuscar.Name = "btnbuscar";
            btnbuscar.Size = new Size(133, 32);
            btnbuscar.TabIndex = 5;
            btnbuscar.Text = "Buscar";
            btnbuscar.UseVisualStyleBackColor = true;
            btnbuscar.Click += btnbuscar_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.Location = new Point(15, 35);
            label1.Name = "label1";
            label1.Size = new Size(96, 15);
            label1.TabIndex = 6;
            label1.Text = "Ingrese Oficio/";
            // 
            // panel1
            // 
            panel1.BackColor = SystemColors.GradientInactiveCaption;
            panel1.Controls.Add(label5);
            panel1.Controls.Add(cmbTipoCaptura);
            panel1.Controls.Add(label4);
            panel1.Controls.Add(cmbEstado);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(lblTicketFaltante);
            panel1.Controls.Add(lblEnProcesoTotal);
            panel1.Controls.Add(lblCerradosTotal);
            panel1.Controls.Add(lblAbiertosTotal);
            panel1.Controls.Add(lblEnProceso);
            panel1.Controls.Add(lblCerrados);
            panel1.Controls.Add(lblAbiertos);
            panel1.Controls.Add(label1);
            panel1.Controls.Add(btnCapturarOficio);
            panel1.Controls.Add(btnbuscar);
            panel1.Controls.Add(btnPanelAdmin);
            panel1.Controls.Add(txtBuscar);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(155, 746);
            panel1.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(21, 142);
            label5.Name = "label5";
            label5.Size = new Size(92, 15);
            label5.TabIndex = 8;
            label5.Text = "Tipo de Captura";
            // 
            // cmbTipoCaptura
            // 
            cmbTipoCaptura.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbTipoCaptura.FormattingEnabled = true;
            cmbTipoCaptura.Location = new Point(2, 162);
            cmbTipoCaptura.Name = "cmbTipoCaptura";
            cmbTipoCaptura.Size = new Size(146, 23);
            cmbTipoCaptura.TabIndex = 8;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.Location = new Point(37, 98);
            label4.Name = "label4";
            label4.Size = new Size(49, 15);
            label4.TabIndex = 8;
            label4.Text = "Estado";
            // 
            // cmbEstado
            // 
            cmbEstado.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            cmbEstado.FormattingEnabled = true;
            cmbEstado.Location = new Point(2, 116);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(147, 23);
            cmbEstado.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Gotham", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.Location = new Point(16, 50);
            label3.Name = "label3";
            label3.Size = new Size(107, 15);
            label3.TabIndex = 8;
            label3.Text = "Asunto/ Petición";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.FromArgb(255, 128, 0);
            label2.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(7, 565);
            label2.Name = "label2";
            label2.Size = new Size(139, 15);
            label2.TabIndex = 8;
            label2.Text = "Oficios Pendientes de";
            // 
            // lblTicketFaltante
            // 
            lblTicketFaltante.AutoSize = true;
            lblTicketFaltante.BackColor = Color.FromArgb(255, 128, 0);
            lblTicketFaltante.Font = new Font("Gotham", 9.749999F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTicketFaltante.Location = new Point(9, 582);
            lblTicketFaltante.Name = "lblTicketFaltante";
            lblTicketFaltante.Size = new Size(16, 15);
            lblTicketFaltante.TabIndex = 8;
            lblTicketFaltante.Text = "0";
            // 
            // lblEnProcesoTotal
            // 
            lblEnProcesoTotal.AutoSize = true;
            lblEnProcesoTotal.BackColor = SystemColors.GradientActiveCaption;
            lblEnProcesoTotal.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEnProcesoTotal.Location = new Point(14, 524);
            lblEnProcesoTotal.Name = "lblEnProcesoTotal";
            lblEnProcesoTotal.Size = new Size(16, 15);
            lblEnProcesoTotal.TabIndex = 11;
            lblEnProcesoTotal.Text = "0";
            // 
            // lblCerradosTotal
            // 
            lblCerradosTotal.AutoSize = true;
            lblCerradosTotal.BackColor = SystemColors.GradientActiveCaption;
            lblCerradosTotal.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCerradosTotal.Location = new Point(13, 474);
            lblCerradosTotal.Name = "lblCerradosTotal";
            lblCerradosTotal.Size = new Size(16, 15);
            lblCerradosTotal.TabIndex = 8;
            lblCerradosTotal.Text = "0";
            // 
            // lblAbiertosTotal
            // 
            lblAbiertosTotal.AutoSize = true;
            lblAbiertosTotal.BackColor = SystemColors.GradientActiveCaption;
            lblAbiertosTotal.Font = new Font("Gotham", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblAbiertosTotal.Location = new Point(14, 424);
            lblAbiertosTotal.Name = "lblAbiertosTotal";
            lblAbiertosTotal.Size = new Size(16, 15);
            lblAbiertosTotal.TabIndex = 8;
            lblAbiertosTotal.Text = "0";
            // 
            // lblEnProceso
            // 
            lblEnProceso.AutoSize = true;
            lblEnProceso.Location = new Point(9, 501);
            lblEnProceso.Name = "lblEnProceso";
            lblEnProceso.Size = new Size(105, 15);
            lblEnProceso.TabIndex = 10;
            lblEnProceso.Text = "Oficios en proceso";
            // 
            // lblCerrados
            // 
            lblCerrados.AutoSize = true;
            lblCerrados.Location = new Point(9, 453);
            lblCerrados.Name = "lblCerrados";
            lblCerrados.Size = new Size(94, 15);
            lblCerrados.TabIndex = 9;
            lblCerrados.Text = "Oficios Cerrados";
            // 
            // lblAbiertos
            // 
            lblAbiertos.AutoSize = true;
            lblAbiertos.Location = new Point(9, 403);
            lblAbiertos.Name = "lblAbiertos";
            lblAbiertos.Size = new Size(91, 15);
            lblAbiertos.TabIndex = 8;
            lblAbiertos.Text = "Oficios Abiertos";
            // 
            // FrmMenuPrincipal
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(1485, 746);
            Controls.Add(panel1);
            Controls.Add(DataGridOficios);
            Controls.Add(lblBienvenida);
            Name = "FrmMenuPrincipal";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmMenuPrincipal";
            ((System.ComponentModel.ISupportInitialize)DataGridOficios).EndInit();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblBienvenida;
        private Button btnCapturarOficio;
        private Button btnPanelAdmin;
        private DataGridView DataGridOficios;
        private TextBox txtBuscar;
        private Button btnbuscar;
        private Label label1;
        private Panel panel1;
        private Label lblAbiertos;
        private Label lblEnProceso;
        private Label lblCerrados;
        private Label lblEnProcesoTotal;
        private Label lblCerradosTotal;
        private Label lblAbiertosTotal;
        private Label lblTicketFaltante;
        private Label label2;
        private Label label3;
        private ComboBox cmbEstado;
        private Label label4;
        private Label label5;
        private ComboBox cmbTipoCaptura;
    }
}