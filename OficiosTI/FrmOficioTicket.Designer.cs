namespace OficiosTI
{
    partial class FrmOficioTicket
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
            ticketBindingSource = new BindingSource(components);
            label1 = new Label();
            txtNumOf = new TextBox();
            label2 = new Label();
            cmbOficinas = new ComboBox();
            label3 = new Label();
            cmbTipos = new ComboBox();
            AsignarF = new Button();
            PanelHeader = new Panel();
            TicketTitulo = new Label();
            ((System.ComponentModel.ISupportInitialize)ticketBindingSource).BeginInit();
            PanelHeader.SuspendLayout();
            SuspendLayout();
            // 
            // ticketBindingSource
            // 
            ticketBindingSource.DataSource = typeof(Data.Entities.Ticket);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 74);
            label1.Name = "label1";
            label1.Size = new Size(112, 15);
            label1.TabIndex = 1;
            label1.Text = "Asignar Núm Oficio";
            // 
            // txtNumOf
            // 
            txtNumOf.Location = new Point(7, 92);
            txtNumOf.Name = "txtNumOf";
            txtNumOf.Size = new Size(125, 23);
            txtNumOf.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(151, 74);
            label2.Name = "label2";
            label2.Size = new Size(83, 15);
            label2.TabIndex = 3;
            label2.Text = "Departamento";
            // 
            // cmbOficinas
            // 
            cmbOficinas.FormattingEnabled = true;
            cmbOficinas.Location = new Point(151, 92);
            cmbOficinas.Name = "cmbOficinas";
            cmbOficinas.Size = new Size(188, 23);
            cmbOficinas.TabIndex = 4;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(353, 74);
            label3.Name = "label3";
            label3.Size = new Size(31, 15);
            label3.TabIndex = 5;
            label3.Text = "Tipo";
            // 
            // cmbTipos
            // 
            cmbTipos.FormattingEnabled = true;
            cmbTipos.Location = new Point(353, 92);
            cmbTipos.Name = "cmbTipos";
            cmbTipos.Size = new Size(121, 23);
            cmbTipos.TabIndex = 6;
            // 
            // AsignarF
            // 
            AsignarF.Location = new Point(7, 148);
            AsignarF.Name = "AsignarF";
            AsignarF.Size = new Size(117, 31);
            AsignarF.TabIndex = 7;
            AsignarF.Text = "Asignar Oficio";
            AsignarF.UseVisualStyleBackColor = true;
            AsignarF.Click += AsignarF_Click;
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.FromArgb(31, 58, 95);
            PanelHeader.Controls.Add(TicketTitulo);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(509, 50);
            PanelHeader.TabIndex = 8;
            // 
            // TicketTitulo
            // 
            TicketTitulo.AutoSize = true;
            TicketTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            TicketTitulo.ForeColor = Color.White;
            TicketTitulo.Location = new Point(15, 14);
            TicketTitulo.Name = "TicketTitulo";
            TicketTitulo.Size = new Size(175, 21);
            TicketTitulo.TabIndex = 0;
            TicketTitulo.Text = "Asignar Folio al ticket";
            // 
            // FrmOficioTicket
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(509, 258);
            Controls.Add(PanelHeader);
            Controls.Add(AsignarF);
            Controls.Add(cmbTipos);
            Controls.Add(label3);
            Controls.Add(cmbOficinas);
            Controls.Add(label2);
            Controls.Add(txtNumOf);
            Controls.Add(label1);
            Name = "FrmOficioTicket";
            RightToLeftLayout = true;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Oficios Ticket";
            ((System.ComponentModel.ISupportInitialize)ticketBindingSource).EndInit();
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private BindingSource ticketBindingSource;
        private Label label1;
        private TextBox txtNumOf;
        private Label label2;
        private ComboBox cmbOficinas;
        private Label label3;
        private ComboBox cmbTipos;
        private Button AsignarF;
        private Panel PanelHeader;
        private Label TicketTitulo;
    }
}