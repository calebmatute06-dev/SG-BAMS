namespace SG_BAMS
{
    partial class frmModificarModelos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmModificarModelos));
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            pictureBox16 = new PictureBox();
            txtDescri = new Krypton.Toolkit.KryptonTextBox();
            label9 = new Label();
            label2 = new Label();
            btnModificar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 24);
            panel1.TabIndex = 99;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 360);
            pictureBox2.TabIndex = 100;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(659, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 375);
            pictureBox1.TabIndex = 92;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 351);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 24);
            panel2.TabIndex = 98;
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = (Image)resources.GetObject("pictureBox16.BackgroundImage");
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(477, 34);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(50, 49);
            pictureBox16.TabIndex = 97;
            pictureBox16.TabStop = false;
            // 
            // txtDescri
            // 
            txtDescri.Location = new Point(322, 96);
            txtDescri.Margin = new Padding(3, 4, 3, 4);
            txtDescri.MaxLength = 70;
            txtDescri.Multiline = true;
            txtDescri.Name = "txtDescri";
            txtDescri.Size = new Size(302, 53);
            txtDescri.StateCommon.Back.Color1 = Color.SkyBlue;
            txtDescri.StateCommon.Border.Rounding = 15F;
            txtDescri.StateCommon.Content.Color1 = Color.Navy;
            txtDescri.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescri.TabIndex = 94;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 15F);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(578, 311);
            label9.Name = "label9";
            label9.Size = new Size(80, 35);
            label9.TabIndex = 93;
            label9.Text = "BAMS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(55, 107);
            label2.Name = "label2";
            label2.Size = new Size(270, 31);
            label2.TabIndex = 91;
            label2.Text = "Ingrese el modelo de auto:";
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(181, 205);
            btnModificar.Name = "btnModificar";
            btnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideDefault.Back.Color2 = Color.White;
            btnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideFocus.Back.Color2 = Color.White;
            btnModificar.Size = new Size(143, 65);
            btnModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnModificar.StateCommon.Back.Color2 = Color.White;
            btnModificar.StateCommon.Border.Rounding = 30F;
            btnModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnModificar.StatePressed.Back.Color1 = Color.Transparent;
            btnModificar.StatePressed.Back.Color2 = Color.Transparent;
            btnModificar.TabIndex = 180;
            btnModificar.Values.DropDownArrowColor = Color.Empty;
            btnModificar.Values.Text = "Modificar";
            btnModificar.Click += btnModificar_Click_1;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(369, 206);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(143, 65);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 30F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 181;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(198, 38);
            label3.Name = "label3";
            label3.Size = new Size(273, 42);
            label3.TabIndex = 342;
            label3.Text = "Modificar Modelos";
            // 
            // frmModificarModelos
            // 
            AllowDrop = true;
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(683, 375);
            Controls.Add(label3);
            Controls.Add(btnSalir);
            Controls.Add(btnModificar);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(pictureBox16);
            Controls.Add(txtDescri);
            Controls.Add(label9);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmModificarModelos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmModificarModelos";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private PictureBox pictureBox16;
        private Krypton.Toolkit.KryptonTextBox txtDescri;
        private Label label9;
        private Label label2;
        private Krypton.Toolkit.KryptonButton btnModificar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Label label3;
    }
}