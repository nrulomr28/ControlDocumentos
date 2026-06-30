namespace OficiosTI
{
    partial class FrmOficioSin
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
            PanelHeader = new Panel();
            LabelTitulo = new Label();
            btnCrearOficio = new Button();
            dataGridOficios = new DataGridView();
            PanelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOficios).BeginInit();
            SuspendLayout();
            // 
            // PanelHeader
            // 
            PanelHeader.BackColor = Color.FromArgb(31, 58, 95);
            PanelHeader.Controls.Add(LabelTitulo);
            PanelHeader.Dock = DockStyle.Top;
            PanelHeader.Location = new Point(0, 0);
            PanelHeader.Name = "PanelHeader";
            PanelHeader.Size = new Size(800, 50);
            PanelHeader.TabIndex = 4;
            // 
            // LabelTitulo
            // 
            LabelTitulo.AutoSize = true;
            LabelTitulo.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            LabelTitulo.ForeColor = Color.White;
            LabelTitulo.Location = new Point(15, 14);
            LabelTitulo.Name = "LabelTitulo";
            LabelTitulo.Size = new Size(146, 21);
            LabelTitulo.TabIndex = 0;
            LabelTitulo.Text = "Oficios sin Tickets";
            // 
            // btnCrearOficio
            // 
            btnCrearOficio.Location = new Point(15, 56);
            btnCrearOficio.Name = "btnCrearOficio";
            btnCrearOficio.RightToLeft = RightToLeft.Yes;
            btnCrearOficio.Size = new Size(96, 29);
            btnCrearOficio.TabIndex = 5;
            btnCrearOficio.Text = "Crear Oficio";
            btnCrearOficio.UseVisualStyleBackColor = true;
            btnCrearOficio.Click += btnCrearOficio_Click;
            // 
            // dataGridOficios
            // 
            dataGridOficios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridOficios.Location = new Point(15, 103);
            dataGridOficios.Name = "dataGridOficios";
            dataGridOficios.Size = new Size(761, 317);
            dataGridOficios.TabIndex = 6;
            // 
            // FrmOficioSin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dataGridOficios);
            Controls.Add(btnCrearOficio);
            Controls.Add(PanelHeader);
            Name = "FrmOficioSin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "FrmOficioSin";
            PanelHeader.ResumeLayout(false);
            PanelHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridOficios).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel PanelHeader;
        private Label LabelTitulo;
        private Button btnCrearOficio;
        private DataGridView dataGridOficios;
    }
}