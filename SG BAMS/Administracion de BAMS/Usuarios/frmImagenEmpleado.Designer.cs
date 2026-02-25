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
            pictureBox3 = new PictureBox();
            panel1 = new Panel();
            pictureBox7 = new PictureBox();
            btnEntrenar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            btnBorrar = new Krypton.Toolkit.KryptonButton();
            btnEncender = new Krypton.Toolkit.KryptonButton();
            btnDetener = new Krypton.Toolkit.KryptonButton();
            label2 = new Label();
            pctCamara = new PictureBox();
            timer1 = new System.Windows.Forms.Timer(components);
            cmbUsuarios2 = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pctCamara).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(306, 19);
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
            label7.Location = new Point(723, 369);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 83;
            label7.Text = "BAMS";
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(2, 396);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(808, 18);
            panel2.TabIndex = 88;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(789, 1);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(21, 411);
            pictureBox3.TabIndex = 90;
            pictureBox3.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(2, -1);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(808, 18);
            panel1.TabIndex = 91;
            // 
            // pictureBox7
            // 
            pictureBox7.BackColor = Color.Navy;
            pictureBox7.Location = new Point(2, 1);
            pictureBox7.Margin = new Padding(3, 2, 3, 2);
            pictureBox7.Name = "pictureBox7";
            pictureBox7.Size = new Size(21, 411);
            pictureBox7.TabIndex = 92;
            pictureBox7.TabStop = false;
            // 
            // btnEntrenar
            // 
            btnEntrenar.Location = new Point(29, 107);
            btnEntrenar.Margin = new Padding(3, 2, 3, 2);
            btnEntrenar.Name = "btnEntrenar";
            btnEntrenar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEntrenar.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnEntrenar.OverrideDefault.Border.Rounding = 40F;
            btnEntrenar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEntrenar.OverrideFocus.Back.Color2 = Color.White;
            btnEntrenar.Size = new Size(227, 49);
            btnEntrenar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEntrenar.StateCommon.Back.Color2 = Color.SkyBlue;
            btnEntrenar.StateCommon.Border.Rounding = 40F;
            btnEntrenar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEntrenar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEntrenar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEntrenar.StateNormal.Back.Color2 = Color.White;
            btnEntrenar.StateNormal.Border.Rounding = 40F;
            btnEntrenar.StateTracking.Border.Rounding = 40F;
            btnEntrenar.TabIndex = 134;
            btnEntrenar.Values.DropDownArrowColor = Color.Empty;
            btnEntrenar.Values.Text = "Entrenar";
            btnEntrenar.Click += btnEntrenar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(29, 334);
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
            btnSalir.TabIndex = 142;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(29, 160);
            btnBorrar.Margin = new Padding(3, 2, 3, 2);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnBorrar.OverrideDefault.Border.Rounding = 40F;
            btnBorrar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideFocus.Back.Color2 = Color.White;
            btnBorrar.Size = new Size(227, 49);
            btnBorrar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateCommon.Back.Color2 = Color.SkyBlue;
            btnBorrar.StateCommon.Border.Rounding = 40F;
            btnBorrar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnBorrar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBorrar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateNormal.Back.Color2 = Color.White;
            btnBorrar.StateNormal.Border.Rounding = 40F;
            btnBorrar.StateTracking.Border.Rounding = 40F;
            btnBorrar.TabIndex = 143;
            btnBorrar.Values.DropDownArrowColor = Color.Empty;
            btnBorrar.Values.Text = "Borrar";
            btnBorrar.Click += btnBorrar_Click;
            // 
            // btnEncender
            // 
            btnEncender.Location = new Point(29, 213);
            btnEncender.Margin = new Padding(3, 2, 3, 2);
            btnEncender.Name = "btnEncender";
            btnEncender.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnEncender.OverrideDefault.Border.Rounding = 40F;
            btnEncender.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEncender.OverrideFocus.Back.Color2 = Color.White;
            btnEncender.Size = new Size(227, 49);
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
            btnDetener.Location = new Point(29, 266);
            btnDetener.Margin = new Padding(3, 2, 3, 2);
            btnDetener.Name = "btnDetener";
            btnDetener.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideDefault.Back.Color2 = SystemColors.Window;
            btnDetener.OverrideDefault.Border.Rounding = 40F;
            btnDetener.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnDetener.OverrideFocus.Back.Color2 = Color.White;
            btnDetener.Size = new Size(227, 49);
            btnDetener.StateCommon.Back.Color1 = Color.SkyBlue;
            btnDetener.StateCommon.Back.Color2 = Color.SkyBlue;
            btnDetener.StateCommon.Border.Rounding = 40F;
            btnDetener.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnDetener.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDetener.StateNormal.Back.Color1 = Color.SkyBlue;
            btnDetener.StateNormal.Back.Color2 = Color.White;
            btnDetener.StateNormal.Border.Rounding = 40F;
            btnDetener.StateTracking.Border.Rounding = 40F;
            btnDetener.TabIndex = 145;
            btnDetener.Values.DropDownArrowColor = Color.Empty;
            btnDetener.Values.Text = "Detener Cámara";
            btnDetener.Click += btnDetener_Click;
            // 
            // label2
            // 
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(41, 32);
            label2.Name = "label2";
            label2.Size = new Size(113, 29);
            label2.TabIndex = 147;
            label2.Text = "Usuarios";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pctCamara
            // 
            pctCamara.Location = new Point(273, 65);
            pctCamara.Name = "pctCamara";
            pctCamara.Size = new Size(496, 301);
            pctCamara.TabIndex = 148;
            pctCamara.TabStop = false;
            //pctCamara.Click += pctCamara_Click;
            // 
            // timer1
            // 
            timer1.Tick += timer1_Tick;
            // 
            // cmbUsuarios
            // 
            cmbUsuarios2.DropDownWidth = 300;
            cmbUsuarios2.Location = new Point(29, 63);
            cmbUsuarios2.Margin = new Padding(3, 2, 3, 2);
            cmbUsuarios2.Name = "cmbUsuarios";
            cmbUsuarios2.Size = new Size(227, 40);
            cmbUsuarios2.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbUsuarios2.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbUsuarios2.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbUsuarios2.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUsuarios2.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbUsuarios2.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbUsuarios2.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbUsuarios2.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbUsuarios2.TabIndex = 149;
            // 
            // frmImagenEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(809, 411);
            Controls.Add(cmbUsuarios2);
            Controls.Add(pctCamara);
            Controls.Add(label2);
            Controls.Add(btnDetener);
            Controls.Add(btnEncender);
            Controls.Add(btnBorrar);
            Controls.Add(btnSalir);
            Controls.Add(btnEntrenar);
            Controls.Add(pictureBox7);
            Controls.Add(panel1);
            Controls.Add(pictureBox3);
            Controls.Add(panel2);
            Controls.Add(label7);
            Controls.Add(label1);
            Name = "frmImagenEmpleado";
            Text = "fmrImagenEmpleado";
            Load += frmImagenEmpleado_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox7).EndInit();
            ((System.ComponentModel.ISupportInitialize)pctCamara).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbUsuarios2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label7;
        private Panel panel2;
        private PictureBox pictureBox3;
        private Panel panel1;
        private PictureBox pictureBox7;
        private Krypton.Toolkit.KryptonButton btnEntrenar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Krypton.Toolkit.KryptonButton btnBorrar;
        private Krypton.Toolkit.KryptonButton btnEncender;
        private Krypton.Toolkit.KryptonButton btnDetener;
        private Label label2;
        private PictureBox pctCamara;
        private System.Windows.Forms.Timer timer1;
        private Krypton.Toolkit.KryptonComboBox cmbUsuarios2;
    }
}