namespace SG_BAMS
{
    partial class Modificar_Datos__Deudor_
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
            kryptonLabel8 = new Krypton.Toolkit.KryptonLabel();
            fechainicio = new Krypton.Toolkit.KryptonMonthCalendar();
            kryptonGroup1 = new Krypton.Toolkit.KryptonGroup();
            panel1 = new Panel();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            lblnombre = new Krypton.Toolkit.KryptonLabel();
            lbliddeuda = new Krypton.Toolkit.KryptonLabel();
            fechafinal = new Krypton.Toolkit.KryptonMonthCalendar();
            label4 = new Label();
            label6 = new Label();
            btnajsutes = new Krypton.Toolkit.KryptonButton();
            pictureBox18 = new PictureBox();
            btnotifiaciones = new Button();
            btnaceptar = new Krypton.Toolkit.KryptonButton();
            btncancelar = new Krypton.Toolkit.KryptonButton();
            kryptonButton13 = new Krypton.Toolkit.KryptonButton();
            lblmontoinicial = new Krypton.Toolkit.KryptonLabel();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(81, 67);
            label1.Name = "label1";
            label1.Size = new Size(151, 24);
            label1.TabIndex = 167;
            label1.Text = "Informacion deudor";
            // 
            // kryptonLabel8
            // 
            kryptonLabel8.Location = new Point(56, 232);
            kryptonLabel8.Name = "kryptonLabel8";
            kryptonLabel8.Size = new Size(136, 31);
            kryptonLabel8.TabIndex = 166;
            kryptonLabel8.Values.Text = "";
            // 
            // fechainicio
            // 
            fechainicio.Location = new Point(373, 113);
            fechainicio.Name = "fechainicio";
            fechainicio.Size = new Size(293, 218);
            fechainicio.StateCheckedNormal.Day.Border.Rounding = 10F;
            fechainicio.TabIndex = 158;
            // 
            // kryptonGroup1
            // 
            kryptonGroup1.Location = new Point(64, 51);
            kryptonGroup1.Size = new Size(179, 59);
            kryptonGroup1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup1.StateCommon.Border.Rounding = 70F;
            kryptonGroup1.TabIndex = 155;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(-1, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1016, 24);
            panel1.TabIndex = 154;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(-1, 377);
            panel3.Name = "panel3";
            panel3.Size = new Size(1027, 24);
            panel3.TabIndex = 153;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 401);
            pictureBox1.TabIndex = 152;
            pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(1002, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 401);
            pictureBox4.TabIndex = 151;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(54, 150);
            label2.Name = "label2";
            label2.Size = new Size(77, 24);
            label2.TabIndex = 168;
            label2.Text = "ID deuda";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(55, 197);
            label3.Name = "label3";
            label3.Size = new Size(69, 24);
            label3.TabIndex = 169;
            label3.Text = "Nombre";
            label3.Click += label3_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(59, 244);
            label5.Name = "label5";
            label5.Size = new Size(100, 24);
            label5.TabIndex = 171;
            label5.Text = "Monto inicial";
            // 
            // lblnombre
            // 
            lblnombre.Location = new Point(184, 197);
            lblnombre.Name = "lblnombre";
            lblnombre.Size = new Size(136, 31);
            lblnombre.StateCommon.ShortText.Color1 = Color.Navy;
            lblnombre.TabIndex = 174;
            lblnombre.Values.Text = "xxx-xxx";
            // 
            // lbliddeuda
            // 
            lbliddeuda.Location = new Point(185, 150);
            lbliddeuda.Name = "lbliddeuda";
            lbliddeuda.Size = new Size(136, 31);
            lbliddeuda.StateCommon.ShortText.Color1 = Color.Navy;
            lbliddeuda.TabIndex = 173;
            lbliddeuda.Values.Text = "xxx-xxx";
            // 
            // fechafinal
            // 
            fechafinal.Location = new Point(687, 113);
            fechafinal.Name = "fechafinal";
            fechafinal.Size = new Size(293, 218);
            fechafinal.StateCheckedNormal.Day.Border.Rounding = 10F;
            fechafinal.TabIndex = 175;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(473, 86);
            label4.Name = "label4";
            label4.Size = new Size(97, 24);
            label4.TabIndex = 177;
            label4.Text = "Fecha Inicio";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(790, 86);
            label6.Name = "label6";
            label6.Size = new Size(94, 24);
            label6.TabIndex = 178;
            label6.Text = "Fecha Final";
            // 
            // btnajsutes
            // 
            btnajsutes.Location = new Point(899, 32);
            btnajsutes.Name = "btnajsutes";
            btnajsutes.OverrideDefault.Back.Color1 = Color.Transparent;
            btnajsutes.OverrideDefault.Back.Color2 = Color.Transparent;
            btnajsutes.OverrideDefault.Border.Rounding = 40F;
            btnajsutes.OverrideFocus.Back.Color1 = Color.White;
            btnajsutes.OverrideFocus.Back.Color2 = Color.SkyBlue;
            btnajsutes.Size = new Size(98, 41);
            btnajsutes.StateCommon.Back.Color1 = Color.White;
            btnajsutes.StateCommon.Back.Color2 = Color.SkyBlue;
            btnajsutes.StateCommon.Border.Rounding = 40F;
            btnajsutes.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnajsutes.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnajsutes.StateNormal.Back.Color1 = Color.Transparent;
            btnajsutes.StateNormal.Back.Color2 = Color.Transparent;
            btnajsutes.StateNormal.Border.Draw = Krypton.Toolkit.InheritBool.False;
            btnajsutes.StatePressed.Back.Color1 = Color.Transparent;
            btnajsutes.StatePressed.Back.Color2 = Color.Transparent;
            btnajsutes.StateTracking.Border.Rounding = 40F;
            btnajsutes.TabIndex = 181;
            btnajsutes.Values.DropDownArrowColor = Color.Empty;
            btnajsutes.Values.Text = "Ajustes";
            // 
            // pictureBox18
            // 
            pictureBox18.BackgroundImage = Properties.Resources.ajus;
            pictureBox18.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox18.Location = new Point(844, 31);
            pictureBox18.Name = "pictureBox18";
            pictureBox18.Size = new Size(53, 49);
            pictureBox18.TabIndex = 180;
            pictureBox18.TabStop = false;
            // 
            // btnotifiaciones
            // 
            btnotifiaciones.BackColor = Color.Transparent;
            btnotifiaciones.BackgroundImage = Properties.Resources.campana;
            btnotifiaciones.BackgroundImageLayout = ImageLayout.Stretch;
            btnotifiaciones.FlatAppearance.BorderColor = Color.White;
            btnotifiaciones.FlatAppearance.BorderSize = 0;
            btnotifiaciones.FlatStyle = FlatStyle.Flat;
            btnotifiaciones.ForeColor = Color.Navy;
            btnotifiaciones.Location = new Point(779, 31);
            btnotifiaciones.Name = "btnotifiaciones";
            btnotifiaciones.Size = new Size(59, 44);
            btnotifiaciones.TabIndex = 179;
            btnotifiaciones.UseVisualStyleBackColor = false;
            // 
            // btnaceptar
            // 
            btnaceptar.Location = new Point(59, 304);
            btnaceptar.Name = "btnaceptar";
            btnaceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnaceptar.OverrideDefault.Back.Color2 = Color.White;
            btnaceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnaceptar.OverrideFocus.Back.Color2 = Color.White;
            btnaceptar.Size = new Size(121, 39);
            btnaceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnaceptar.StateCommon.Back.Color2 = Color.White;
            btnaceptar.StateCommon.Border.Rounding = 20F;
            btnaceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnaceptar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnaceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnaceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnaceptar.StatePressed.Back.Color1 = Color.Transparent;
            btnaceptar.StatePressed.Back.Color2 = Color.Transparent;
            btnaceptar.TabIndex = 183;
            btnaceptar.Values.DropDownArrowColor = Color.Empty;
            btnaceptar.Values.Text = "Aceptar";
            btnaceptar.Click += btnaceptar_Click;
            // 
            // btncancelar
            // 
            btncancelar.Location = new Point(192, 304);
            btncancelar.Name = "btncancelar";
            btncancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btncancelar.OverrideDefault.Back.Color2 = Color.White;
            btncancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btncancelar.OverrideFocus.Back.Color2 = Color.White;
            btncancelar.Size = new Size(121, 39);
            btncancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btncancelar.StateCommon.Back.Color2 = Color.White;
            btncancelar.StateCommon.Border.Rounding = 20F;
            btncancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btncancelar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btncancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btncancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btncancelar.StatePressed.Back.Color1 = Color.Transparent;
            btncancelar.StatePressed.Back.Color2 = Color.Transparent;
            btncancelar.TabIndex = 182;
            btncancelar.Values.DropDownArrowColor = Color.Empty;
            btncancelar.Values.Text = "Cancelar";
            // 
            // kryptonButton13
            // 
            kryptonButton13.Location = new Point(914, 341);
            kryptonButton13.Name = "kryptonButton13";
            kryptonButton13.OverrideDefault.Back.Color1 = Color.Transparent;
            kryptonButton13.OverrideDefault.Back.Color2 = Color.Transparent;
            kryptonButton13.OverrideDefault.Border.Rounding = 40F;
            kryptonButton13.OverrideFocus.Back.Color1 = Color.White;
            kryptonButton13.OverrideFocus.Back.Color2 = Color.SkyBlue;
            kryptonButton13.Size = new Size(107, 41);
            kryptonButton13.StateCommon.Back.Color1 = Color.White;
            kryptonButton13.StateCommon.Back.Color2 = Color.SkyBlue;
            kryptonButton13.StateCommon.Border.Rounding = 40F;
            kryptonButton13.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton13.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kryptonButton13.StateNormal.Back.Color1 = Color.Transparent;
            kryptonButton13.StateNormal.Back.Color2 = Color.Transparent;
            kryptonButton13.StateNormal.Border.Draw = Krypton.Toolkit.InheritBool.False;
            kryptonButton13.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton13.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton13.StateTracking.Border.Rounding = 40F;
            kryptonButton13.TabIndex = 184;
            kryptonButton13.Values.DropDownArrowColor = Color.Empty;
            kryptonButton13.Values.Text = "BAMS";
            // 
            // lblmontoinicial
            // 
            lblmontoinicial.Location = new Point(184, 244);
            lblmontoinicial.Name = "lblmontoinicial";
            lblmontoinicial.Size = new Size(136, 31);
            lblmontoinicial.StateCommon.ShortText.Color1 = Color.Navy;
            lblmontoinicial.TabIndex = 185;
            lblmontoinicial.Values.Text = "xxx-xxx";
            // 
            // Modificar_Datos__Deudor_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1027, 398);
            Controls.Add(lblmontoinicial);
            Controls.Add(btnaceptar);
            Controls.Add(btncancelar);
            Controls.Add(btnajsutes);
            Controls.Add(pictureBox18);
            Controls.Add(btnotifiaciones);
            Controls.Add(label6);
            Controls.Add(label4);
            Controls.Add(fechafinal);
            Controls.Add(lblnombre);
            Controls.Add(lbliddeuda);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(kryptonLabel8);
            Controls.Add(fechainicio);
            Controls.Add(kryptonGroup1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            Controls.Add(kryptonButton13);
            Name = "Modificar_Datos__Deudor_";
            Text = "Modificar_Datos__Deudor_";
            Load += Modificar_Datos__Deudor__Load;
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox18).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel8;
        private Krypton.Toolkit.KryptonMonthCalendar fechainicio;
        private Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private Panel panel1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label3;
        private Label label5;
        private Krypton.Toolkit.KryptonLabel lblnombre;
        private Krypton.Toolkit.KryptonLabel lbliddeuda;
        private Krypton.Toolkit.KryptonMonthCalendar fechafinal;
        private Label label4;
        private Label label6;
        private Krypton.Toolkit.KryptonButton btnajsutes;
        private PictureBox pictureBox18;
        private Button btnotifiaciones;
        private Krypton.Toolkit.KryptonButton btnaceptar;
        private Krypton.Toolkit.KryptonButton btncancelar;
        private Krypton.Toolkit.KryptonButton kryptonButton13;
        private Krypton.Toolkit.KryptonLabel lblmontoinicial;
    }
}