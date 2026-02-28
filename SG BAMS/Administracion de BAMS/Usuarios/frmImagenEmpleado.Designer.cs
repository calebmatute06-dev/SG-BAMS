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
            label1 = new Label();
            label7 = new Label();
            panel2 = new Panel();
            btnCapturar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            btnBorrar = new Krypton.Toolkit.KryptonButton();
            btnEncender = new Krypton.Toolkit.KryptonButton();
            btnDetener = new Krypton.Toolkit.KryptonButton();
            label2 = new Label();
            pctCamara = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            cmbUsuarios = new Krypton.Toolkit.KryptonComboBox();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pctCamara).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(299, 23);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 74;
            label1.Text = "Imagen del Usuario";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(719, 361);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 83;
            label7.Text = "BAMS";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(1, 388);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(809, 22);
            panel2.TabIndex = 88;
            // 
            // btnCapturar
            // 
            btnCapturar.Location = new Point(43, 107);
            btnCapturar.Margin = new Padding(3, 2, 3, 2);
            btnCapturar.Name = "btnCapturar";
            btnCapturar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnCapturar.OverrideDefault.Border.Rounding = 40F;
            btnCapturar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCapturar.OverrideFocus.Back.Color2 = Color.White;
            btnCapturar.Size = new Size(199, 51);
            btnCapturar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCapturar.StateCommon.Back.Color2 = Color.SkyBlue;
            btnCapturar.StateCommon.Border.Rounding = 40F;
            btnCapturar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCapturar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCapturar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCapturar.StateNormal.Back.Color2 = Color.White;
            btnCapturar.StateNormal.Border.Rounding = 40F;
            btnCapturar.StateTracking.Border.Rounding = 40F;
            btnCapturar.TabIndex = 144;
            btnCapturar.Values.DropDownArrowColor = Color.Empty;
            btnCapturar.Values.Text = "Capturar";
            btnCapturar.Click += btnCapturar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(32, 339);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideDefault.Border.Rounding = 40F;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(90, 45);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 40F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.Color2 = Color.White;
            btnSalir.StateNormal.Border.Rounding = 40F;
            btnSalir.StateTracking.Border.Rounding = 40F;
            btnSalir.TabIndex = 142;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(43, 162);
            btnBorrar.Margin = new Padding(3, 2, 3, 2);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnBorrar.OverrideDefault.Border.Rounding = 40F;
            btnBorrar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideFocus.Back.Color2 = Color.White;
            btnBorrar.Size = new Size(199, 51);
            btnBorrar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateCommon.Back.Color2 = Color.SkyBlue;
            btnBorrar.StateCommon.Border.Rounding = 40F;
            btnBorrar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnBorrar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBorrar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateNormal.Back.Color2 = Color.White;
            btnBorrar.StateNormal.Border.Rounding = 40F;
            btnBorrar.StateTracking.Border.Rounding = 40F;
            btnBorrar.TabIndex = 144;
            btnBorrar.Values.DropDownArrowColor = Color.Empty;
            btnBorrar.Values.Text = "Borrar";
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnEncender
            // 
            btnEncender.Location = new Point(43, 217);
            btnEncender.Margin = new Padding(3, 2, 3, 2);
            btnEncender.Name = "btnEncender";
            btnEncender.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnEncender.OverrideDefault.Border.Rounding = 40F;
            btnEncender.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideFocus.Back.Color2 = Color.White;
            btnEncender.Size = new Size(199, 51);
            btnEncender.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEncender.StateCommon.Back.Color2 = Color.SkyBlue;
            btnEncender.StateCommon.Border.Rounding = 40F;
            btnEncender.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEncender.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEncender.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEncender.StateNormal.Back.Color2 = Color.White;
            btnEncender.StateNormal.Border.Rounding = 40F;
            btnEncender.StateTracking.Border.Rounding = 40F;
            btnEncender.TabIndex = 144;
            btnEncender.Values.DropDownArrowColor = Color.Empty;
            btnEncender.Values.Text = "Encender Cámara";
            btnEncender.Click += btnEncender_Click;
            // 
            // btnDetener
            // 
            btnDetener.Location = new Point(43, 272);
            btnDetener.Margin = new Padding(3, 2, 3, 2);
            btnDetener.Name = "btnDetener";
            btnDetener.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnDetener.OverrideDefault.Border.Rounding = 40F;
            btnDetener.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideFocus.Back.Color2 = Color.White;
            btnDetener.Size = new Size(199, 51);
            btnDetener.StateCommon.Back.Color1 = Color.SkyBlue;
            btnDetener.StateCommon.Back.Color2 = Color.SkyBlue;
            btnDetener.StateCommon.Border.Rounding = 40F;
            btnDetener.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnDetener.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDetener.StateNormal.Back.Color1 = Color.SkyBlue;
            btnDetener.StateNormal.Back.Color2 = Color.White;
            btnDetener.StateNormal.Border.Rounding = 40F;
            btnDetener.StateTracking.Border.Rounding = 40F;
            btnDetener.TabIndex = 144;
            btnDetener.Values.DropDownArrowColor = Color.Empty;
            btnDetener.Values.Text = "Detener Cámara";
            btnDetener.Click += btnDetener_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(43, 23);
            label2.Name = "label2";
            label2.Size = new Size(99, 33);
            label2.TabIndex = 147;
            label2.Text = "Usuarios";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pctCamara
            // 
            pctCamara.Location = new Point(266, 54);
            pctCamara.Margin = new Padding(3, 2, 3, 2);
            pctCamara.Name = "pctCamara";
            pctCamara.Size = new Size(486, 298);
            pctCamara.TabIndex = 148;
            pctCamara.TabStop = false;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios.DropDownWidth = 300;
            cmbUsuarios.Location = new Point(43, 61);
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
            pictureBox2.Location = new Point(785, -1);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(25, 411);
            pictureBox2.TabIndex = 150;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(1, -1);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(25, 411);
            pictureBox1.TabIndex = 151;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(1, -1);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(809, 22);
            panel1.TabIndex = 89;
            // 
            // frmImagenEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(809, 411);
            Controls.Add(panel1);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(cmbUsuarios);
            Controls.Add(pctCamara);
            Controls.Add(label2);
            Controls.Add(btnDetener);
            Controls.Add(btnEncender);
            Controls.Add(btnBorrar);
            Controls.Add(btnSalir);
            Controls.Add(btnCapturar);
            Controls.Add(panel2);
            Controls.Add(label7);
            Controls.Add(label1);
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

        private Label label1;
        private Label label7;
        private Panel panel2;
        private Krypton.Toolkit.KryptonButton btnCapturar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Krypton.Toolkit.KryptonButton btnBorrar;
        private Krypton.Toolkit.KryptonButton btnEncender;
        private Krypton.Toolkit.KryptonButton btnDetener;
        private Label label2;
        private PictureBox pctCamara;
        private System.Windows.Forms.Timer timer1;
        private Krypton.Toolkit.KryptonComboBox cmbUsuarios;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel1;
    }
}