namespace SG_BAMS
{
    partial class frmImagenEmpleado
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
            panel2 = new Panel();
            pctCamara = new PictureBox();
            cmbUsuarios = new Krypton.Toolkit.KryptonComboBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            btnCapturar = new Krypton.Toolkit.KryptonButton();
            btnBorrar = new Krypton.Toolkit.KryptonButton();
            btnEncender = new Krypton.Toolkit.KryptonButton();
            btnDetener = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            label3 = new Label();
            label1 = new Label();
            label2 = new Label();
            ((System.ComponentModel.ISupportInitialize)pctCamara).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 388);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(809, 22);
            panel2.TabIndex = 88;
            // 
            // pctCamara
            // 
            pctCamara.Location = new Point(265, 57);
            pctCamara.Margin = new Padding(3, 2, 3, 2);
            pctCamara.Name = "pctCamara";
            pctCamara.Size = new Size(486, 295);
            pctCamara.TabIndex = 148;
            pctCamara.TabStop = false;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.DropDownWidth = 300;
            cmbUsuarios.Location = new Point(49, 61);
            cmbUsuarios.Margin = new Padding(3, 2, 3, 2);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(199, 40);
            cmbUsuarios.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbUsuarios.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbUsuarios.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbUsuarios.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUsuarios.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbUsuarios.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbUsuarios.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUsuarios.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbUsuarios.TabIndex = 149;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(784, -1);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 411);
            pictureBox2.TabIndex = 150;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 411);
            pictureBox1.TabIndex = 151;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, -1);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(809, 22);
            panel1.TabIndex = 89;
            // 
            // btnCapturar
            // 
            btnCapturar.Location = new Point(42, 106);
            btnCapturar.Margin = new Padding(3, 2, 3, 2);
            btnCapturar.Name = "btnCapturar";
            btnCapturar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideDefault.Back.Color2 = Color.White;
            btnCapturar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideFocus.Back.Color2 = Color.White;
            btnCapturar.Size = new Size(209, 51);
            btnCapturar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCapturar.StateCommon.Back.Color2 = Color.White;
            btnCapturar.StateCommon.Border.Rounding = 30F;
            btnCapturar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCapturar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCapturar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCapturar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCapturar.StatePressed.Back.Color1 = Color.Transparent;
            btnCapturar.StatePressed.Back.Color2 = Color.Transparent;
            btnCapturar.TabIndex = 152;
            btnCapturar.Values.DropDownArrowColor = Color.Empty;
            btnCapturar.Values.Text = "Capturar";
            btnCapturar.Click += btnCapturar_Click_1;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(42, 162);
            btnBorrar.Margin = new Padding(3, 2, 3, 2);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideDefault.Back.Color2 = Color.White;
            btnBorrar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideFocus.Back.Color2 = Color.White;
            btnBorrar.Size = new Size(209, 51);
            btnBorrar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateCommon.Back.Color2 = Color.White;
            btnBorrar.StateCommon.Border.Rounding = 30F;
            btnBorrar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnBorrar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBorrar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnBorrar.StatePressed.Back.Color1 = Color.Transparent;
            btnBorrar.StatePressed.Back.Color2 = Color.Transparent;
            btnBorrar.TabIndex = 153;
            btnBorrar.Values.DropDownArrowColor = Color.Empty;
            btnBorrar.Values.Text = "Borrar";
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnEncender
            // 
            btnEncender.Location = new Point(42, 217);
            btnEncender.Margin = new Padding(3, 2, 3, 2);
            btnEncender.Name = "btnEncender";
            btnEncender.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideDefault.Back.Color2 = Color.White;
            btnEncender.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideFocus.Back.Color2 = Color.White;
            btnEncender.Size = new Size(209, 51);
            btnEncender.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEncender.StateCommon.Back.Color2 = Color.White;
            btnEncender.StateCommon.Border.Rounding = 30F;
            btnEncender.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEncender.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnEncender.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEncender.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnEncender.StatePressed.Back.Color1 = Color.Transparent;
            btnEncender.StatePressed.Back.Color2 = Color.Transparent;
            btnEncender.TabIndex = 154;
            btnEncender.Values.DropDownArrowColor = Color.Empty;
            btnEncender.Values.Text = "Encender Cámara";
            btnEncender.Click += btnEncender_Click;
            // 
            // btnDetener
            // 
            btnDetener.Location = new Point(42, 272);
            btnDetener.Margin = new Padding(3, 2, 3, 2);
            btnDetener.Name = "btnDetener";
            btnDetener.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideDefault.Back.Color2 = Color.White;
            btnDetener.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideFocus.Back.Color2 = Color.White;
            btnDetener.Size = new Size(209, 51);
            btnDetener.StateCommon.Back.Color1 = Color.SkyBlue;
            btnDetener.StateCommon.Back.Color2 = Color.White;
            btnDetener.StateCommon.Border.Rounding = 30F;
            btnDetener.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnDetener.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDetener.StateNormal.Back.Color1 = Color.SkyBlue;
            btnDetener.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnDetener.StatePressed.Back.Color1 = Color.Transparent;
            btnDetener.StatePressed.Back.Color2 = Color.Transparent;
            btnDetener.TabIndex = 155;
            btnDetener.Values.DropDownArrowColor = Color.Empty;
            btnDetener.Values.Text = "Detener Cámara";
            btnDetener.Click += btnDetener_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(31, 334);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(116, 49);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 30F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 156;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(408, 26);
            label3.Name = "label3";
            label3.Size = new Size(193, 29);
            label3.TabIndex = 342;
            label3.Text = "Imagen del Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(91, 27);
            label1.Name = "label1";
            label1.Size = new Size(97, 29);
            label1.TabIndex = 343;
            label1.Text = "Usuarios";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(698, 357);
            label2.Name = "label2";
            label2.Size = new Size(84, 29);
            label2.TabIndex = 349;
            label2.Text = "BAMS";
            // 
            // frmImagenEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(809, 410);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(btnSalir);
            Controls.Add(btnDetener);
            Controls.Add(btnEncender);
            Controls.Add(btnBorrar);
            Controls.Add(btnCapturar);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(cmbUsuarios);
            Controls.Add(pctCamara);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmImagenEmpleado";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrImagenEmpleado";
            Load += frmImagenEmpleado_Load;
            ((System.ComponentModel.ISupportInitialize)pctCamara).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel2;
        private PictureBox pctCamara;
        private Krypton.Toolkit.KryptonComboBox cmbUsuarios;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
        private Krypton.Toolkit.KryptonButton btnCapturar;
        private Krypton.Toolkit.KryptonButton btnBorrar;
        private Krypton.Toolkit.KryptonButton btnEncender;
        private Krypton.Toolkit.KryptonButton btnDetener;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Label label3;
        private Label label1;
        private Label label2;
    }
}