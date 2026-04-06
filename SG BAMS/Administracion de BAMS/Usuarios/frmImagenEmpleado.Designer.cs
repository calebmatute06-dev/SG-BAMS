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
            components = new System.ComponentModel.Container();
            label7 = new Label();
            panel2 = new Panel();
            pctCamara = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
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
            ((System.ComponentModel.ISupportInitialize)pctCamara).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(821, 481);
            label7.Name = "label7";
            label7.Size = new Size(77, 31);
            label7.TabIndex = 83;
            label7.Text = "BAMS";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 517);
            panel2.Name = "panel2";
            panel2.Size = new Size(925, 29);
            panel2.TabIndex = 88;
            // 
            // pctCamara
            // 
            pctCamara.Location = new Point(303, 72);
            pctCamara.Name = "pctCamara";
            pctCamara.Size = new Size(555, 397);
            pctCamara.TabIndex = 148;
            pctCamara.TabStop = false;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.DropDownWidth = 300;
            cmbUsuarios.Location = new Point(48, 81);
            cmbUsuarios.Name = "cmbUsuarios";
            cmbUsuarios.Size = new Size(227, 46);
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
            pictureBox2.Location = new Point(896, -1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(29, 548);
            pictureBox2.TabIndex = 150;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, -1);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(29, 548);
            pictureBox1.TabIndex = 151;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, -1);
            panel1.Name = "panel1";
            panel1.Size = new Size(925, 29);
            panel1.TabIndex = 89;
            // 
            // btnCapturar
            // 
            btnCapturar.Location = new Point(48, 142);
            btnCapturar.Name = "btnCapturar";
            btnCapturar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideDefault.Back.Color2 = Color.White;
            btnCapturar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideFocus.Back.Color2 = Color.White;
            btnCapturar.Size = new Size(239, 68);
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
            btnBorrar.Location = new Point(48, 216);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideDefault.Back.Color2 = Color.White;
            btnBorrar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideFocus.Back.Color2 = Color.White;
            btnBorrar.Size = new Size(239, 68);
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
            btnEncender.Location = new Point(48, 289);
            btnEncender.Name = "btnEncender";
            btnEncender.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideDefault.Back.Color2 = Color.White;
            btnEncender.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideFocus.Back.Color2 = Color.White;
            btnEncender.Size = new Size(239, 68);
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
            btnDetener.Location = new Point(48, 363);
            btnDetener.Name = "btnDetener";
            btnDetener.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideDefault.Back.Color2 = Color.White;
            btnDetener.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideFocus.Back.Color2 = Color.White;
            btnDetener.Size = new Size(239, 68);
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
            btnSalir.Location = new Point(35, 446);
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
            label3.Location = new Point(466, 34);
            label3.Name = "label3";
            label3.Size = new Size(245, 35);
            label3.TabIndex = 342;
            label3.Text = "Imagen del Usuario";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(104, 36);
            label1.Name = "label1";
            label1.Size = new Size(122, 35);
            label1.TabIndex = 343;
            label1.Text = "Usuarios";
            // 
            // frmImagenEmpleado
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(925, 547);
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
            Controls.Add(label7);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
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
        private Label label7;
        private Panel panel2;
        private PictureBox pctCamara;
        private System.Windows.Forms.Timer timer1;
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
    }
}