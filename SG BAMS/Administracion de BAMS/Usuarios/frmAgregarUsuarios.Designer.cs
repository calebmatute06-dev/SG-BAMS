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
            label1 = new Label();
            label2 = new Label();
            label9 = new Label();
            txtNombre = new Krypton.Toolkit.KryptonTextBox();
            txtContra = new Krypton.Toolkit.KryptonTextBox();
            label4 = new Label();
            label5 = new Label();
            pictureBox16 = new PictureBox();
            panel2 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            cmbRol = new Krypton.Toolkit.KryptonComboBox();
            btmAgregar = new Krypton.Toolkit.KryptonButton();
            btnImagen = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(159, 21);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 1;
            label1.Text = "Ingresar Usuarios";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(76, 73);
            label2.Name = "label2";
            label2.Size = new Size(145, 25);
            label2.TabIndex = 2;
            label2.Text = "Nombre Usuario:";
            label2.Click += label2_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 15F);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(533, 327);
            label9.Name = "label9";
            label9.Size = new Size(65, 28);
            label9.TabIndex = 17;
            label9.Text = "BAMS";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(227, 65);
            txtNombre.Multiline = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(193, 40);
            txtNombre.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNombre.StateCommon.Border.Rounding = 15F;
            txtNombre.StateCommon.Content.Color1 = Color.Navy;
            txtNombre.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.TabIndex = 18;
            // 
            // txtContra
            // 
            txtContra.Location = new Point(227, 120);
            txtContra.Multiline = true;
            txtContra.Name = "txtContra";
            txtContra.Size = new Size(193, 40);
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
            label4.Location = new Point(76, 128);
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
            label5.Location = new Point(76, 184);
            label5.Name = "label5";
            label5.Size = new Size(134, 25);
            label5.TabIndex = 21;
            label5.Text = "Rol de Usuario:";
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = Properties.Resources.perfiles;
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(369, 23);
            pictureBox16.Margin = new Padding(3, 2, 3, 2);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(35, 29);
            pictureBox16.TabIndex = 72;
            pictureBox16.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 357);
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
            pictureBox1.Size = new Size(21, 376);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, -1);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 374);
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
            cmbRol.Location = new Point(227, 176);
            cmbRol.Margin = new Padding(3, 2, 3, 2);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(193, 40);
            cmbRol.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbRol.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbRol.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbRol.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbRol.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbRol.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbRol.TabIndex = 129;
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            // 
            // btmAgregar
            // 
            btmAgregar.Location = new Point(176, 281);
            btmAgregar.Margin = new Padding(3, 2, 3, 2);
            btmAgregar.Name = "btmAgregar";
            btmAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideDefault.Back.Color2 = Color.White;
            btmAgregar.OverrideDefault.Border.Rounding = 40F;
            btmAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideFocus.Back.Color2 = Color.White;
            btmAgregar.Size = new Size(103, 49);
            btmAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateCommon.Back.Color2 = Color.White;
            btmAgregar.StateCommon.Border.Rounding = 40F;
            btmAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmAgregar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btmAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateNormal.Back.Color2 = Color.White;
            btmAgregar.StateNormal.Border.Rounding = 40F;
            btmAgregar.StateTracking.Border.Rounding = 40F;
            btmAgregar.TabIndex = 131;
            btmAgregar.Values.DropDownArrowColor = Color.Empty;
            btmAgregar.Values.Text = "Agregar";
            btmAgregar.Click += btmAgregar_Click;
            // 
            // btnImagen
            // 
            btnImagen.Location = new Point(394, 231);
            btnImagen.Margin = new Padding(3, 2, 3, 2);
            btnImagen.Name = "btnImagen";
            btnImagen.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnImagen.OverrideDefault.Back.Color2 = Color.White;
            btnImagen.OverrideDefault.Border.Rounding = 40F;
            btnImagen.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnImagen.OverrideFocus.Back.Color2 = Color.White;
            btnImagen.Size = new Size(205, 49);
            btnImagen.StateCommon.Back.Color1 = Color.SkyBlue;
            btnImagen.StateCommon.Back.Color2 = Color.White;
            btnImagen.StateCommon.Border.Rounding = 40F;
            btnImagen.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnImagen.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnImagen.StateNormal.Back.Color1 = Color.SkyBlue;
            btnImagen.StateNormal.Back.Color2 = Color.White;
            btnImagen.StateNormal.Border.Rounding = 40F;
            btnImagen.StateTracking.Border.Rounding = 40F;
            btnImagen.TabIndex = 132;
            btnImagen.Values.DropDownArrowColor = Color.Empty;
            btnImagen.Values.Text = "Imagen de Empleado";
            btnImagen.Click += btnImagen_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(301, 281);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideDefault.Border.Rounding = 40F;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(103, 49);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 40F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.Color2 = Color.White;
            btnSalir.StateNormal.Border.Rounding = 40F;
            btnSalir.StateTracking.Border.Rounding = 40F;
            btnSalir.TabIndex = 133;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // frmAgregarUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(625, 374);
            Controls.Add(btnSalir);
            Controls.Add(btnImagen);
            Controls.Add(btmAgregar);
            Controls.Add(cmbRol);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(pictureBox16);
            Controls.Add(label5);
            Controls.Add(txtContra);
            Controls.Add(label4);
            Controls.Add(txtNombre);
            Controls.Add(label9);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "frmAgregarUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrAgregarUsuarios";
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label9;
        private Krypton.Toolkit.KryptonTextBox txtNombre;
        private Krypton.Toolkit.KryptonTextBox txtContra;
        private Label label4;
        private Label label5;
        private PictureBox pictureBox16;
        private Panel panel2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Krypton.Toolkit.KryptonComboBox cmbRol;
        private Krypton.Toolkit.KryptonButton btmAgregar;
        private Krypton.Toolkit.KryptonButton btnImagen;
        private Krypton.Toolkit.KryptonButton btnSalir;
    }
}