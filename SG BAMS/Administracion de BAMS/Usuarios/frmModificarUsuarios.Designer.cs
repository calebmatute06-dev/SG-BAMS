namespace SG_BAMS
{
    partial class frmModificarUsuarios
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
            pictureBox16 = new PictureBox();
            label5 = new Label();
            txtContra = new Krypton.Toolkit.KryptonTextBox();
            label4 = new Label();
            txtNombre = new Krypton.Toolkit.KryptonTextBox();
            label9 = new Label();
            label2 = new Label();
            label3 = new Label();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            panel2 = new Panel();
            cmbRol = new Krypton.Toolkit.KryptonComboBox();
            cmbEstado = new Krypton.Toolkit.KryptonComboBox();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            btnImagen = new Krypton.Toolkit.KryptonButton();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).BeginInit();
            SuspendLayout();
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = Properties.Resources.perfiles;
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(519, 37);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(40, 39);
            pictureBox16.TabIndex = 84;
            pictureBox16.TabStop = false;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(91, 253);
            label5.Name = "label5";
            label5.Size = new Size(164, 31);
            label5.TabIndex = 79;
            label5.Text = "Rol de Usuario:";
            // 
            // txtContra
            // 
            txtContra.Location = new Point(277, 162);
            txtContra.Margin = new Padding(3, 4, 3, 4);
            txtContra.MaxLength = 70;
            txtContra.Multiline = true;
            txtContra.Name = "txtContra";
            txtContra.Size = new Size(221, 53);
            txtContra.StateCommon.Back.Color1 = Color.SkyBlue;
            txtContra.StateCommon.Border.Rounding = 15F;
            txtContra.StateCommon.Content.Color1 = Color.Navy;
            txtContra.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F);
            txtContra.TabIndex = 78;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(91, 171);
            label4.Name = "label4";
            label4.Size = new Size(132, 31);
            label4.TabIndex = 77;
            label4.Text = "Contraseña:";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(277, 87);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.MaxLength = 70;
            txtNombre.Multiline = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(221, 53);
            txtNombre.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNombre.StateCommon.Border.Rounding = 15F;
            txtNombre.StateCommon.Content.Color1 = Color.Navy;
            txtNombre.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F);
            txtNombre.TabIndex = 76;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 15F);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(653, 504);
            label9.Name = "label9";
            label9.Size = new Size(80, 35);
            label9.TabIndex = 75;
            label9.Text = "BAMS";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(91, 95);
            label2.Name = "label2";
            label2.Size = new Size(178, 31);
            label2.TabIndex = 74;
            label2.Text = "Nombre Usuario:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(91, 327);
            label3.Name = "label3";
            label3.Size = new Size(88, 31);
            label3.TabIndex = 85;
            label3.Text = "Estado:";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 567);
            pictureBox1.TabIndex = 88;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(734, 1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 564);
            pictureBox2.TabIndex = 89;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(11, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(746, 24);
            panel1.TabIndex = 88;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 544);
            panel2.Name = "panel2";
            panel2.Size = new Size(758, 24);
            panel2.TabIndex = 87;
            // 
            // cmbRol
            // 
            cmbRol.DropDownWidth = 300;
            cmbRol.Location = new Point(277, 242);
            cmbRol.Name = "cmbRol";
            cmbRol.Size = new Size(221, 46);
            cmbRol.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbRol.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbRol.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbRol.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbRol.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbRol.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbRol.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbRol.TabIndex = 145;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownWidth = 300;
            cmbEstado.Location = new Point(277, 313);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(221, 46);
            cmbEstado.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbEstado.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbEstado.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbEstado.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbEstado.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbEstado.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbEstado.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbEstado.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbEstado.TabIndex = 146;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(90, 405);
            btmModificar.Name = "btmModificar";
            btmModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideDefault.Back.Color2 = Color.White;
            btmModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideFocus.Back.Color2 = Color.White;
            btmModificar.Size = new Size(150, 65);
            btmModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmModificar.StateCommon.Back.Color2 = Color.White;
            btmModificar.StateCommon.Border.Rounding = 30F;
            btmModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btmModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btmModificar.StatePressed.Back.Color1 = Color.Transparent;
            btmModificar.StatePressed.Back.Color2 = Color.Transparent;
            btmModificar.TabIndex = 150;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Modificar";
            btmModificar.Click += btmModificar_Click_1;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(570, 408);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(132, 65);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 30F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 152;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // btnImagen
            // 
            btnImagen.Location = new Point(269, 405);
            btnImagen.Name = "btnImagen";
            btnImagen.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnImagen.OverrideDefault.Back.Color2 = Color.White;
            btnImagen.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnImagen.OverrideFocus.Back.Color2 = Color.White;
            btnImagen.Size = new Size(270, 65);
            btnImagen.StateCommon.Back.Color1 = Color.SkyBlue;
            btnImagen.StateCommon.Back.Color2 = Color.White;
            btnImagen.StateCommon.Border.Rounding = 30F;
            btnImagen.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnImagen.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnImagen.StateNormal.Back.Color1 = Color.SkyBlue;
            btnImagen.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnImagen.StatePressed.Back.Color1 = Color.Transparent;
            btnImagen.StatePressed.Back.Color2 = Color.Transparent;
            btnImagen.TabIndex = 153;
            btnImagen.Values.DropDownArrowColor = Color.Empty;
            btnImagen.Values.Text = "Imagen del Usuario";
            btnImagen.Click += btnImagen_Click_1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Narrow", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(234, 35);
            label6.Name = "label6";
            label6.Size = new Size(279, 42);
            label6.TabIndex = 343;
            label6.Text = "Modificar Usuarios";
            // 
            // frmModificarUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(757, 563);
            Controls.Add(label6);
            Controls.Add(btnImagen);
            Controls.Add(btnSalir);
            Controls.Add(btmModificar);
            Controls.Add(cmbEstado);
            Controls.Add(cmbRol);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(label3);
            Controls.Add(pictureBox16);
            Controls.Add(label5);
            Controls.Add(txtContra);
            Controls.Add(label4);
            Controls.Add(txtNombre);
            Controls.Add(label9);
            Controls.Add(label2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmModificarUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrModificarUsuarios";
            Load += fmrModificarUsuarios_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox16;
        private Label label5;
        private Krypton.Toolkit.KryptonTextBox txtContra;
        private Label label4;
        private Krypton.Toolkit.KryptonTextBox txtNombre;
        private Label label9;
        private Label label2;
        private Label label3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Panel panel1;
        private Panel panel2;
        private Krypton.Toolkit.KryptonComboBox cmbRol;
        private Krypton.Toolkit.KryptonComboBox cmbEstado;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Krypton.Toolkit.KryptonButton btnImagen;
        private Label label6;
    }
}