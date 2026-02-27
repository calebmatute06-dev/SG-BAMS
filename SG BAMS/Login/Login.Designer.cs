namespace SG_BAMS.Login
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
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            picBa = new PictureBox();
            btninicioSesion = new Krypton.Toolkit.KryptonButton();
            btnsalirLogin = new Krypton.Toolkit.KryptonButton();
            txtUsu = new Krypton.Toolkit.KryptonTextBox();
            txtCon = new Krypton.Toolkit.KryptonTextBox();
            panel2 = new Panel();
            pictureBox4 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox3 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)picBa).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            SuspendLayout();
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
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 30F);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(312, 198);
            label3.Name = "label3";
            label3.Size = new Size(162, 67);
            label3.TabIndex = 6;
            label3.Text = "BAMS";
            // 
            // picBa
            // 
            picBa.BackColor = Color.Transparent;
            picBa.Image = Properties.Resources.Bateria_1;
            picBa.Location = new Point(289, 12);
            picBa.Name = "picBa";
            picBa.Size = new Size(202, 183);
            picBa.TabIndex = 8;
            picBa.TabStop = false;
            // 
            // btninicioSesion
            // 
            btninicioSesion.Location = new Point(184, 451);
            btninicioSesion.Name = "btninicioSesion";
            btninicioSesion.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btninicioSesion.OverrideDefault.Back.Color2 = Color.White;
            btninicioSesion.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btninicioSesion.OverrideFocus.Back.Color2 = Color.White;
            btninicioSesion.Size = new Size(224, 41);
            btninicioSesion.StateCommon.Back.Color1 = Color.SkyBlue;
            btninicioSesion.StateCommon.Back.Color2 = Color.White;
            btninicioSesion.StateCommon.Border.Rounding = 40F;
            btninicioSesion.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btninicioSesion.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btninicioSesion.StateNormal.Back.Color1 = Color.SkyBlue;
            btninicioSesion.StateNormal.Back.Color2 = Color.Transparent;
            btninicioSesion.StateNormal.Border.Rounding = 40F;
            btninicioSesion.TabIndex = 38;
            btninicioSesion.Values.DropDownArrowColor = Color.Empty;
            btninicioSesion.Values.Text = "Iniciar Sesión";
            btninicioSesion.Click += btninicioSesion_Click;
            // 
            // btnsalirLogin
            // 
            btnsalirLogin.Location = new Point(431, 451);
            btnsalirLogin.Name = "btnsalirLogin";
            btnsalirLogin.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.OverrideDefault.Back.Color2 = Color.White;
            btnsalirLogin.OverrideDefault.Border.Rounding = 40F;
            btnsalirLogin.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.OverrideFocus.Back.Color2 = Color.White;
            btnsalirLogin.Size = new Size(224, 41);
            btnsalirLogin.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.StateCommon.Back.Color2 = Color.White;
            btnsalirLogin.StateCommon.Border.Rounding = 40F;
            btnsalirLogin.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalirLogin.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsalirLogin.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.StateNormal.Back.Color2 = Color.White;
            btnsalirLogin.StateNormal.Border.Rounding = 40F;
            btnsalirLogin.StateTracking.Border.Rounding = 40F;
            btnsalirLogin.TabIndex = 39;
            btnsalirLogin.Values.DropDownArrowColor = Color.Empty;
            btnsalirLogin.Values.Text = "Salir";
            btnsalirLogin.Click += btnsalirLogin_Click;
            // 
            // txtUsu
            // 
            txtUsu.Location = new Point(307, 303);
            txtUsu.Name = "txtUsu";
            txtUsu.Size = new Size(220, 39);
            txtUsu.StateCommon.Back.Color1 = Color.SkyBlue;
            txtUsu.StateCommon.Border.Rounding = 20F;
            txtUsu.TabIndex = 40;
            // 
            // txtCon
            // 
            txtCon.Location = new Point(307, 370);
            txtCon.Name = "txtCon";
            txtCon.Size = new Size(220, 39);
            txtCon.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCon.StateCommon.Border.Rounding = 20F;
            txtCon.TabIndex = 41;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(-151, 235);
            panel2.Name = "panel2";
            panel2.Size = new Size(12, 34);
            panel2.TabIndex = 42;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(-1, -4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 545);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(776, -17);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 558);
            pictureBox1.TabIndex = 43;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(-1, -4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(801, 24);
            pictureBox2.TabIndex = 44;
            pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(-1, 517);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(801, 24);
            pictureBox3.TabIndex = 45;
            pictureBox3.TabStop = false;
            // 
            // Login
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(800, 537);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(txtCon);
            Controls.Add(txtUsu);
            Controls.Add(btnsalirLogin);
            Controls.Add(btninicioSesion);
            Controls.Add(picBa);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            ForeColor = SystemColors.ActiveCaptionText;
            Name = "Login";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            Load += Login_Load;
            ((System.ComponentModel.ISupportInitialize)picBa).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Label label2;
        private Label label3;
        private PictureBox picBa;
        private Krypton.Toolkit.KryptonButton btninicioSesion;
        private Krypton.Toolkit.KryptonButton btnsalirLogin;
        private Krypton.Toolkit.KryptonTextBox txtUsu;
        private Krypton.Toolkit.KryptonTextBox txtCon;
        private Panel panel2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox3;
    }
}