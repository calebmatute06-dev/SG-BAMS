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
            SuspendLayout();
            // 
            // btnIni
            // 
            btnIni.Location = new Point(182, 347);
            btnIni.Name = "btnIni";
            btnIni.Size = new Size(145, 29);
            btnIni.TabIndex = 0;
            btnIni.Text = "Iniciar Sesion";
            btnIni.UseVisualStyleBackColor = true;
            // 
            // txtUsu
            // 
            txtUsu.Location = new Point(319, 178);
            txtUsu.Name = "txtUsu";
            txtUsu.Size = new Size(221, 27);
            txtUsu.TabIndex = 2;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(208, 185);
            label1.Name = "label1";
            label1.Size = new Size(59, 20);
            label1.TabIndex = 3;
            label1.Text = "Usuario";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(197, 264);
            label2.Name = "label2";
            label2.Size = new Size(83, 20);
            label2.TabIndex = 4;
            label2.Text = "Contraseña";
            // 
            // txtCon
            // 
            txtCon.Location = new Point(319, 264);
            txtCon.Name = "txtCon";
            txtCon.Size = new Size(221, 27);
            txtCon.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 30F);
            label3.Location = new Point(319, 54);
            label3.Name = "label3";
            label3.Size = new Size(162, 67);
            label3.TabIndex = 6;
            label3.Text = "BAMS";
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(386, 347);
            btnSalir.Name = "btnSalir";
            btnSalir.Size = new Size(145, 29);
            btnSalir.TabIndex = 7;
            btnSalir.Text = "Iniciar Sesion";
            btnSalir.UseVisualStyleBackColor = true;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnSalir);
            Controls.Add(label3);
            Controls.Add(txtCon);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(txtUsu);
            Controls.Add(btnIni);
            Name = "Login";
            Text = "Login";
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
    }
}