namespace SG_BAMS
{
    partial class frmModificarRol
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
            label2 = new Label();
            txtDescri = new Krypton.Toolkit.KryptonTextBox();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            btmSalir = new Krypton.Toolkit.KryptonButton();
            label5 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 24);
            panel1.TabIndex = 131;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 299);
            pictureBox2.TabIndex = 132;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(659, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 299);
            pictureBox1.TabIndex = 125;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 275);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 24);
            panel2.TabIndex = 130;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(66, 127);
            label2.Name = "label2";
            label2.Size = new Size(252, 31);
            label2.TabIndex = 124;
            label2.Text = "Ingrese el rol de usuario:";
            // 
            // txtDescri
            // 
            txtDescri.Location = new Point(320, 113);
            txtDescri.Margin = new Padding(3, 4, 3, 4);
            txtDescri.MaxLength = 70;
            txtDescri.Multiline = true;
            txtDescri.Name = "txtDescri";
            txtDescri.Size = new Size(302, 45);
            txtDescri.StateCommon.Back.Color1 = Color.SkyBlue;
            txtDescri.StateCommon.Border.Rounding = 15F;
            txtDescri.StateCommon.Content.Color1 = Color.Navy;
            txtDescri.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescri.TabIndex = 143;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(191, 193);
            btmModificar.Name = "btmModificar";
            btmModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideDefault.Back.Color2 = Color.White;
            btmModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideFocus.Back.Color2 = Color.White;
            btmModificar.Size = new Size(143, 65);
            btmModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmModificar.StateCommon.Back.Color2 = Color.White;
            btmModificar.StateCommon.Border.Rounding = 30F;
            btmModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btmModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btmModificar.StatePressed.Back.Color1 = Color.Transparent;
            btmModificar.StatePressed.Back.Color2 = Color.Transparent;
            btmModificar.TabIndex = 178;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Modificar";
            btmModificar.Click += btmModificar_Click;
            // 
            // btmSalir
            // 
            btmSalir.Location = new Point(355, 191);
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
            btmSalir.TabIndex = 179;
            btmSalir.Values.DropDownArrowColor = Color.Empty;
            btmSalir.Values.Text = "Salir";
            btmSalir.Click += btmSalir_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(557, 229);
            label5.Name = "label5";
            label5.Size = new Size(102, 35);
            label5.TabIndex = 347;
            label5.Text = "BAMS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(223, 45);
            label8.Name = "label8";
            label8.Size = new Size(324, 29);
            label8.TabIndex = 358;
            label8.Text = "Modificar Roles de Usuario";
            // 
            // frmModificarRol
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(683, 299);
            Controls.Add(label8);
            Controls.Add(label5);
            Controls.Add(btmSalir);
            Controls.Add(btmModificar);
            Controls.Add(txtDescri);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmModificarRol";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmModificarRol";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label label2;
        private Krypton.Toolkit.KryptonTextBox txtDescri;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton btmSalir;
        private Label label5;
        private Label label8;
    }
}