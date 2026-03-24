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
            btmModificar = new Krypton.Toolkit.KryptonButton();
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            label3 = new Label();
            btnImagen = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbRol).BeginInit();
            SuspendLayout();
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(87, 105);
            label2.Name = "label2";
            label2.Size = new Size(178, 31);
            label2.TabIndex = 2;
            label2.Text = "Nombre Usuario:";
            //label2.Click += label2_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 15F);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(609, 423);
            label9.Name = "label9";
            label9.Size = new Size(80, 35);
            label9.TabIndex = 17;
            label9.Text = "BAMS";
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(259, 95);
            txtNombre.Margin = new Padding(3, 4, 3, 4);
            txtNombre.MaxLength = 70;
            txtNombre.Multiline = true;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(221, 53);
            txtNombre.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNombre.StateCommon.Border.Rounding = 15F;
            txtNombre.StateCommon.Content.Color1 = Color.Navy;
            txtNombre.StateCommon.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNombre.TabIndex = 18;
            txtNombre.TextChanged += txtNombre_TextChanged;
            // 
            // txtContra
            // 
            txtContra.Location = new Point(259, 168);
            txtContra.Margin = new Padding(3, 4, 3, 4);
            txtContra.MaxLength = 70;
            txtContra.Multiline = true;
            txtContra.Name = "txtContra";
            txtContra.Size = new Size(221, 53);
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
            label4.Location = new Point(87, 179);
            label4.Name = "label4";
            label4.Size = new Size(132, 31);
            label4.TabIndex = 19;
            label4.Text = "Contraseña:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(87, 253);
            label5.Name = "label5";
            label5.Size = new Size(164, 31);
            label5.TabIndex = 21;
            label5.Text = "Rol de Usuario:";
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = Properties.Resources.perfiles;
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(484, 35);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(40, 39);
            pictureBox16.TabIndex = 72;
            pictureBox16.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 465);
            panel2.Name = "panel2";
            panel2.Size = new Size(714, 24);
            panel2.TabIndex = 77;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(690, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 487);
            pictureBox1.TabIndex = 9;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, -1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 490);
            pictureBox2.TabIndex = 78;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(714, 24);
            panel1.TabIndex = 78;
            // 
            // cmbRol
            // 
            cmbRol.DropDownWidth = 300;
            cmbRol.Location = new Point(259, 243);
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
            cmbRol.TabIndex = 129;
            cmbRol.SelectedIndexChanged += cmbRol_SelectedIndexChanged;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(59, 342);
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
            btmModificar.TabIndex = 151;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Agregar";
            btmModificar.Click += btmModificar_Click;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Location = new Point(524, 345);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton1.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton1.Size = new Size(132, 65);
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
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(217, 34);
            label3.Name = "label3";
            label3.Size = new Size(264, 42);
            label3.TabIndex = 342;
            label3.Text = "Ingresar Usuarios";
            // 
            // btnImagen
            // 
            btnImagen.Location = new Point(229, 342);
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
            btnImagen.TabIndex = 344;
            btnImagen.Values.DropDownArrowColor = Color.Empty;
            btnImagen.Values.Text = "Imagen del Usuario";
            btnImagen.Click += btnImagen_Click;
            // 
            // frmAgregarUsuarios
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(714, 488);
            Controls.Add(btnImagen);
            Controls.Add(label3);
            Controls.Add(kryptonButton1);
            Controls.Add(btmModificar);
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
            Margin = new Padding(3, 4, 3, 4);
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
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Label label3;
        private Krypton.Toolkit.KryptonButton btnImagen;
    }
}