namespace OficiosTI
{
    partial class FrmOficioNuevo
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
            panelHeader = new Panel();
            lblTitulo = new Label();
            label1 = new Label();
            txtNof = new TextBox();
            label2 = new Label();
            txtAsunto = new TextBox();
            label3 = new Label();
            txtDest = new TextBox();
            txtCrgo = new TextBox();
            label4 = new Label();
            label5 = new Label();
            txtRespC = new TextBox();
            label6 = new Label();
            txtCcp = new TextBox();
            label7 = new Label();
            cmbFirmas = new ComboBox();
            panel1 = new Panel();
            btnWord = new Button();
            btnGuardarNu = new Button();
            panelHeader.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // panelHeader
            // 
            panelHeader.BackColor = Color.FromArgb(0, 51, 102);
            panelHeader.Controls.Add(lblTitulo);
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Location = new Point(0, 0);
            panelHeader.Name = "panelHeader";
            panelHeader.Size = new Size(585, 48);
            panelHeader.TabIndex = 3;
            // 
            // lblTitulo
            // 
            lblTitulo.AutoSize = true;
            lblTitulo.Font = new Font("Tahoma", 10F, FontStyle.Bold);
            lblTitulo.ForeColor = Color.White;
            lblTitulo.Location = new Point(12, 14);
            lblTitulo.Name = "lblTitulo";
            lblTitulo.Size = new Size(137, 17);
            lblTitulo.TabIndex = 0;
            lblTitulo.Text = "Crear Oficio Nuevo";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 69);
            label1.Name = "label1";
            label1.Size = new Size(102, 15);
            label1.TabIndex = 4;
            label1.Text = "Número de Oficio";
            // 
            // txtNof
            // 
            txtNof.Location = new Point(12, 87);
            txtNof.Name = "txtNof";
            txtNof.Size = new Size(169, 23);
            txtNof.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 123);
            label2.Name = "label2";
            label2.Size = new Size(45, 15);
            label2.TabIndex = 6;
            label2.Text = "Asunto";
            // 
            // txtAsunto
            // 
            txtAsunto.Location = new Point(12, 141);
            txtAsunto.Name = "txtAsunto";
            txtAsunto.Size = new Size(560, 23);
            txtAsunto.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 186);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 8;
            label3.Text = "Destinatario";
            // 
            // txtDest
            // 
            txtDest.Location = new Point(14, 204);
            txtDest.Name = "txtDest";
            txtDest.Size = new Size(274, 23);
            txtDest.TabIndex = 9;
            // 
            // txtCrgo
            // 
            txtCrgo.Location = new Point(298, 204);
            txtCrgo.Name = "txtCrgo";
            txtCrgo.Size = new Size(274, 23);
            txtCrgo.TabIndex = 10;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(301, 187);
            label4.Name = "label4";
            label4.Size = new Size(39, 15);
            label4.TabIndex = 11;
            label4.Text = "Cargo";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 246);
            label5.Name = "label5";
            label5.Size = new Size(60, 15);
            label5.TabIndex = 12;
            label5.Text = "Respuesta";
            // 
            // txtRespC
            // 
            txtRespC.Location = new Point(12, 264);
            txtRespC.Multiline = true;
            txtRespC.Name = "txtRespC";
            txtRespC.Size = new Size(552, 184);
            txtRespC.TabIndex = 13;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 471);
            label6.Name = "label6";
            label6.Size = new Size(34, 15);
            label6.TabIndex = 14;
            label6.Text = "C.c.p";
            // 
            // txtCcp
            // 
            txtCcp.Location = new Point(17, 490);
            txtCcp.Multiline = true;
            txtCcp.Name = "txtCcp";
            txtCcp.Size = new Size(547, 39);
            txtCcp.TabIndex = 15;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 550);
            label7.Name = "label7";
            label7.Size = new Size(61, 15);
            label7.TabIndex = 16;
            label7.Text = "Firma por:";
            // 
            // cmbFirmas
            // 
            cmbFirmas.FormattingEnabled = true;
            cmbFirmas.Location = new Point(19, 568);
            cmbFirmas.Name = "cmbFirmas";
            cmbFirmas.Size = new Size(545, 23);
            cmbFirmas.TabIndex = 17;
            // 
            // panel1
            // 
            panel1.Controls.Add(btnWord);
            panel1.Controls.Add(btnGuardarNu);
            panel1.Location = new Point(21, 620);
            panel1.Name = "panel1";
            panel1.Size = new Size(551, 71);
            panel1.TabIndex = 18;
            // 
            // btnWord
            // 
            btnWord.Location = new Point(260, 20);
            btnWord.Name = "btnWord";
            btnWord.Size = new Size(113, 34);
            btnWord.TabIndex = 0;
            btnWord.Text = "Vista Previa";
            btnWord.UseVisualStyleBackColor = true;
            btnWord.Click += btnWord_Click;
            // 
            // btnGuardarNu
            // 
            btnGuardarNu.Location = new Point(128, 20);
            btnGuardarNu.Name = "btnGuardarNu";
            btnGuardarNu.Size = new Size(113, 34);
            btnGuardarNu.TabIndex = 0;
            btnGuardarNu.Text = "Guardar Oficio";
            btnGuardarNu.UseVisualStyleBackColor = true;
            btnGuardarNu.Click += btnGuardarNu_Click;
            // 
            // FrmOficioNuevo
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(585, 703);
            Controls.Add(panel1);
            Controls.Add(cmbFirmas);
            Controls.Add(label7);
            Controls.Add(txtCcp);
            Controls.Add(label6);
            Controls.Add(txtRespC);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(txtCrgo);
            Controls.Add(txtDest);
            Controls.Add(label3);
            Controls.Add(txtAsunto);
            Controls.Add(label2);
            Controls.Add(txtNof);
            Controls.Add(label1);
            Controls.Add(panelHeader);
            Name = "FrmOficioNuevo";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Oficios Nuevos";
            panelHeader.ResumeLayout(false);
            panelHeader.PerformLayout();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panelHeader;
        private Label lblTitulo;
        private Label label1;
        private TextBox txtNof;
        private Label label2;
        private TextBox txtAsunto;
        private Label label3;
        private TextBox txtDest;
        private TextBox txtCrgo;
        private Label label4;
        private Label label5;
        private TextBox txtRespC;
        private Label label6;
        private TextBox txtCcp;
        private Label label7;
        private ComboBox cmbFirmas;
        private Panel panel1;
        private Button btnWord;
        private Button btnGuardarNu;
    }
}