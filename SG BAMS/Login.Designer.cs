namespace SG_BAMS
{
    partial class Login
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
            btnIni = new Button();
            txtUsu = new TextBox();
            label1 = new Label();
            label2 = new Label();
            txtCon = new TextBox();
            label3 = new Label();
            btnSalir = new Button();
            picBa = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picBa).BeginInit();
            SuspendLayout();
            // 
            // btnIni
            // 
            btnIni.BackColor = Color.SkyBlue;
            btnIni.ForeColor = Color.Navy;
            btnIni.Location = new Point(189, 461);
            btnIni.Name = "btnIni";
            btnIni.Size = new Size(145, 29);
            btnIni.TabIndex = 0;
            btnIni.Text = "Iniciar Sesion";
            btnIni.UseVisualStyleBackColor = false;
            btnIni.Click += btnIni_Click;
            // 
            // txtUsu
            // 
            txtUsu.BackColor = Color.SkyBlue;
            txtUsu.Location = new Point(325, 303);
            txtUsu.Name = "txtUsu";
            txtUsu.Size = new Size(221, 27);
            txtUsu.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(214, 310);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 3;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(203, 389);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // txtCon
            // 
            txtCon.BackColor = Color.SkyBlue;
            txtCon.Location = new Point(325, 389);
            txtCon.Name = "txtCon";
            txtCon.Size = new Size(221, 27);
            txtCon.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 30F);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(303, 198);
            label3.Name = "label3";
            label3.Size = new Size(162, 67);
            label3.TabIndex = 6;
            label3.Text = "BAMS";
            // 
            // btnSalir
            // 
            btnSalir.BackColor = Color.SkyBlue;
            btnSalir.ForeColor = Color.Navy;
            btnSalir.Location = new Point(433, 461);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(145, 29);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Salir";
            btnSalir.UseVisualStyleBackColor = false;
            btnSalir.Click += btnSalir_Click;
            // 
            // picBa
            // 
            picBa.BackColor = Color.Transparent;
            picBa.Image = Properties.Resources.Bateria_1;
            picBa.Location = new Point(281, 12);
            picBa.Name = "picBa";
            picBa.Size = new Size(202, 183);
            picBa.TabIndex = 8;
            picBa.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 514);
            Controls.Add(picBa);
            Controls.Add(btnSalir);
            Controls.Add(label3);
            Controls.Add(txtCon);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtUsu);
            Controls.Add(btnIni);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Login";
            Text = "Login";
            ((System.ComponentModel.ISupportInitialize)picBa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnIni;
        private TextBox txtUsu;
        private Label label1;
        private Label label2;
        private TextBox txtCon;
        private Label label3;
        private Button btnSalir;
        private PictureBox picBa;
    }
}