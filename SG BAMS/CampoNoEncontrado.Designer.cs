namespace SG_BAMS
{
    partial class CampoNoEncontrado
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CampoNoEncontrado));
            label2 = new Label();
            label1 = new Label();
            pictureBox5 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            btnsalirLogin = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.White;
            label2.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(252, 98);
            label2.Name = "label2";
            label2.Size = new Size(148, 28);
            label2.TabIndex = 112;
            label2.Text = "esta busqueda";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.White;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(180, 70);
            label1.Name = "label1";
            label1.Size = new Size(294, 28);
            label1.TabIndex = 111;
            label1.Text = "No se encontraron datos para";
            label1.Click += label1_Click;
            // 
            // pictureBox5
            // 
            pictureBox5.BackColor = Color.Transparent;
            pictureBox5.BackgroundImage = (Image)resources.GetObject("pictureBox5.BackgroundImage");
            pictureBox5.BackgroundImageLayout = ImageLayout.Zoom;
            pictureBox5.Location = new Point(21, 49);
            pictureBox5.Name = "pictureBox5";
            pictureBox5.Size = new Size(131, 110);
            pictureBox5.TabIndex = 110;
            pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(498, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(16, 217);
            pictureBox4.TabIndex = 109;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(-1, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(16, 217);
            pictureBox3.TabIndex = 108;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-1, 202);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(515, 15);
            pictureBox1.TabIndex = 107;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(-1, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(515, 15);
            pictureBox2.TabIndex = 106;
            pictureBox2.TabStop = false;
            // 
            // btnsalirLogin
            // 
            btnsalirLogin.Location = new Point(326, 155);
            btnsalirLogin.Name = "btnsalirLogin";
            btnsalirLogin.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.OverrideDefault.Back.Color2 = Color.White;
            btnsalirLogin.OverrideDefault.Border.Rounding = 40F;
            btnsalirLogin.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.OverrideFocus.Back.Color2 = Color.White;
            btnsalirLogin.Size = new Size(166, 41);
            btnsalirLogin.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.StateCommon.Back.Color2 = Color.White;
            btnsalirLogin.StateCommon.Border.Rounding = 40F;
            btnsalirLogin.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalirLogin.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsalirLogin.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalirLogin.StateNormal.Back.Color2 = Color.White;
            btnsalirLogin.StateNormal.Border.Rounding = 40F;
            btnsalirLogin.StateTracking.Border.Rounding = 40F;
            btnsalirLogin.TabIndex = 113;
            btnsalirLogin.Values.DropDownArrowColor = Color.Empty;
            btnsalirLogin.Values.Text = "Aceptar";
            // 
            // CampoNoEncontrado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(514, 216);
            Controls.Add(btnsalirLogin);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox5);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Name = "CampoNoEncontrado";
            Text = "CampoNoEncontrado";
            ((System.ComponentModel.ISupportInitialize)pictureBox5).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label2;
        private Label label1;
        private PictureBox pictureBox5;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Krypton.Toolkit.KryptonButton btnsalirLogin;
    }
}