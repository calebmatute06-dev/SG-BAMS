namespace SG_BAMS
{
    partial class frmAgregarUsuarios
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
            label2 = new Label();
            txtNombre = new Krypton.Toolkit.KryptonTextBox();
            txtContra = new Krypton.Toolkit.KryptonTextBox();
            label4 = new Label();
            label5 = new Label();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            cmbRol = new Krypton.Toolkit.KryptonComboBox();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            label1 = new Label();
            label8 = new Label();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(108, 98);
            label2.Name = "label2";
            label2.Size = new Size(145, 25);
            label2.TabIndex = 2;
            label2.Text = "Nombre Usuario:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(259, 93);
            txtNombre.MaxLength = 70;
            txtNombre.Multiline = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(193, 38);
            txtNombre.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNombre.StateCommon.Border.Rounding = 15F;
            txtNombre.StateCommon.Content.Color1 = Color.Navy;
            txtNombre.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.TabIndex = 18;
            // 
            // txtContra
            // 
            txtContra.Location = new Point(259, 141);
            txtContra.MaxLength = 70;
            txtContra.Multiline = true;
            txtContra.Name = "txtContra";
            txtContra.Size = new Size(193, 37);
            txtContra.StateCommon.Back.Color1 = Color.SkyBlue;
            txtContra.StateCommon.Border.Rounding = 15F;
            txtContra.StateCommon.Content.Color1 = Color.Navy;
            txtContra.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtContra.TabIndex = 20;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(108, 141);
            label4.Name = "label4";
            label4.Size = new Size(109, 25);
            label4.TabIndex = 19;
            label4.Text = "Contraseña:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(108, 187);
            label5.Name = "label5";
            label5.Size = new Size(134, 25);
            label5.TabIndex = 21;
            label5.Text = "Rol de Usuario:";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 305);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(625, 18);
            panel2.TabIndex = 77;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(604, -1);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 324);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, -1);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 324);
            pictureBox2.TabIndex = 78;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, -1);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(625, 18);
            panel1.TabIndex = 78;
            // 
            // cmbRol
            // 
            cmbRol.DropDownWidth = 300;
            cmbRol.Location = new Point(259, 183);
            cmbRol.Margin = new Padding(3, 2, 3, 2);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(193, 40);
            cmbRol.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbRol.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbRol.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbRol.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbRol.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbRol.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateNormal.ComboBox.Border.Rounding = 30F;
            cmbRol.TabIndex = 129;
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(148, 243);
            btmModificar.Margin = new Padding(3, 2, 3, 2);
            btmModificar.Name = "btmModificar";
            btmModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideDefault.Back.Color2 = Color.White;
            btmModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideFocus.Back.Color2 = Color.White;
            btmModificar.Size = new Size(125, 42);
            btmModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmModificar.StateCommon.Back.Color2 = Color.White;
            btmModificar.StateCommon.Border.Rounding = 30F;
            btmModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btmModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btmModificar.StatePressed.Back.Color1 = Color.Transparent;
            btmModificar.StatePressed.Back.Color2 = Color.Transparent;
            btmModificar.TabIndex = 151;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Agregar";
            btmModificar.Click += btmModificar_Click;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Location = new Point(319, 243);
            kryptonButton1.Margin = new Padding(3, 2, 3, 2);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton1.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton1.Size = new Size(116, 42);
            kryptonButton1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton1.StateCommon.Back.Color2 = Color.White;
            kryptonButton1.StateCommon.Border.Rounding = 30F;
            kryptonButton1.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton1.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            kryptonButton1.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton1.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton1.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton1.TabIndex = 153;
            kryptonButton1.Values.DropDownArrowColor = Color.Empty;
            kryptonButton1.Values.Text = "Salir";
            kryptonButton1.Click += kryptonButton1_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(514, 273);
            label1.Name = "label1";
            label1.Size = new Size(84, 29);
            label1.TabIndex = 348;
            label1.Text = "BAMS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.SkyBlue;
            label8.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(252, 34);
            label8.Name = "label8";
            label8.Size = new Size(162, 22);
            label8.TabIndex = 362;
            label8.Text = "Agregar Usuario";
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.CaptionVisible = false;
            kryptonGroupBox2.Location = new Point(230, 25);
            kryptonGroupBox2.Size = new Size(204, 39);
            kryptonGroupBox2.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox2.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox2.TabIndex = 363;
            // 
            // frmAgregarUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(625, 322);
            Controls.Add(label8);
            Controls.Add(kryptonGroupBox2);
            Controls.Add(label1);
            Controls.Add(kryptonButton1);
            Controls.Add(btmModificar);
            Controls.Add(cmbRol);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(label5);
            Controls.Add(txtContra);
            Controls.Add(label4);
            Controls.Add(txtNombre);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmAgregarUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrAgregarUsuarios";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label2;
        private Krypton.Toolkit.KryptonTextBox txtNombre;
        private Krypton.Toolkit.KryptonTextBox txtContra;
        private Label label4;
        private Label label5;
        private Panel panel2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Krypton.Toolkit.KryptonComboBox cmbRol;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Label label1;
        private Label label8;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
    }
}