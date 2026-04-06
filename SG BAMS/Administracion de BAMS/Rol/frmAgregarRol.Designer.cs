namespace SG_BAMS
{
    partial class frmAgregarRol
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
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            txtDescri = new Krypton.Toolkit.KryptonTextBox();
            label9 = new Label();
            label2 = new Label();
            pictureBox4 = new PictureBox();
            btmAgregar = new Krypton.Toolkit.KryptonButton();
            btmSalir = new Krypton.Toolkit.KryptonButton();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 24);
            panel1.TabIndex = 121;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 360);
            pictureBox2.TabIndex = 122;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(659, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 375);
            pictureBox1.TabIndex = 114;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 351);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 24);
            panel2.TabIndex = 120;
            // 
            // txtDescri
            // 
            txtDescri.Location = new Point(320, 100);
            txtDescri.Margin = new Padding(3, 4, 3, 4);
            txtDescri.MaxLength = 70;
            txtDescri.Multiline = true;
            txtDescri.Name = "txtDescri";
            txtDescri.Size = new Size(302, 55);
            txtDescri.StateCommon.Back.Color1 = Color.SkyBlue;
            txtDescri.StateCommon.Border.Rounding = 15F;
            txtDescri.StateCommon.Content.Color1 = Color.Navy;
            txtDescri.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescri.TabIndex = 116;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(586, 316);
            label9.Name = "label9";
            label9.Size = new Size(70, 30);
            label9.TabIndex = 115;
            label9.Text = "BAMS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(71, 111);
            label2.Name = "label2";
            label2.Size = new Size(252, 31);
            label2.TabIndex = 113;
            label2.Text = "Ingrese el rol de usuario:";
            // 
            // pictureBox4
            // 
            pictureBox4.BackgroundImage = Properties.Resources.roles1;
            pictureBox4.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox4.Image = Properties.Resources.roles;
            pictureBox4.Location = new Point(498, 31);
            pictureBox4.Margin = new Padding(3, 4, 3, 4);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(71, 51);
            pictureBox4.TabIndex = 0;
            pictureBox4.TabStop = false;
            // 
            // btmAgregar
            // 
            btmAgregar.Location = new Point(165, 216);
            btmAgregar.Name = "btmAgregar";
            btmAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideDefault.Back.Color2 = Color.White;
            btmAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideFocus.Back.Color2 = Color.White;
            btmAgregar.Size = new Size(143, 65);
            btmAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateCommon.Back.Color2 = Color.White;
            btmAgregar.StateCommon.Border.Rounding = 30F;
            btmAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btmAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btmAgregar.StatePressed.Back.Color1 = Color.Transparent;
            btmAgregar.StatePressed.Back.Color2 = Color.Transparent;
            btmAgregar.TabIndex = 179;
            btmAgregar.Values.DropDownArrowColor = Color.Empty;
            btmAgregar.Values.Text = "Agregar";
            btmAgregar.Click += btmAgregar_Click;
            // 
            // btmSalir
            // 
            btmSalir.Location = new Point(362, 216);
            btmSalir.Name = "btmSalir";
            btmSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideDefault.Back.Color2 = Color.White;
            btmSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideFocus.Back.Color2 = Color.White;
            btmSalir.Size = new Size(143, 65);
            btmSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btmSalir.StateCommon.Back.Color2 = Color.White;
            btmSalir.StateCommon.Border.Rounding = 30F;
            btmSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btmSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btmSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btmSalir.StatePressed.Back.Color1 = Color.Transparent;
            btmSalir.StatePressed.Back.Color2 = Color.Transparent;
            btmSalir.TabIndex = 180;
            btmSalir.Values.DropDownArrowColor = Color.Empty;
            btmSalir.Values.Text = "Salir";
            btmSalir.Click += btmSalir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(179, 39);
            label3.Name = "label3";
            label3.Size = new Size(300, 42);
            label3.TabIndex = 341;
            label3.Text = "Agregar Rol Usuario";
            // 
            // frmAgregarRol
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(683, 373);
            Controls.Add(label3);
            Controls.Add(btmSalir);
            Controls.Add(btmAgregar);
            Controls.Add(pictureBox4);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(txtDescri);
            Controls.Add(label9);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmAgregarRol";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmAgregarRol";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Krypton.Toolkit.KryptonTextBox txtDescri;
        private Label label9;
        private Label label2;
        private PictureBox pictureBox4;
        private Krypton.Toolkit.KryptonButton btmAgregar;
        private Krypton.Toolkit.KryptonButton btmSalir;
        private Label label3;
    }
}