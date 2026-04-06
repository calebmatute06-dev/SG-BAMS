namespace SG_BAMS
{
    partial class Pago_Deuda
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
            txtMonto = new Krypton.Toolkit.KryptonTextBox();
            label1 = new Label();
            cmbDeudores = new Krypton.Toolkit.KryptonComboBox();
            kryptonGroup1 = new Krypton.Toolkit.KryptonGroup();
            panel1 = new Panel();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            btnAceptar = new Krypton.Toolkit.KryptonButton();
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)cmbDeudores).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            SuspendLayout();
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(170, 136);
            txtMonto.Margin = new Padding(3, 2, 3, 2);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(228, 28);
            txtMonto.StateCommon.Back.Color1 = Color.SkyBlue;
            txtMonto.StateCommon.Border.Rounding = 10F;
            txtMonto.StateCommon.Content.Color1 = Color.Navy;
            txtMonto.StateCommon.Content.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMonto.TabIndex = 169;
            txtMonto.KeyPress += txtMonto_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(67, 46);
            label1.Name = "label1";
            label1.Size = new Size(123, 19);
            label1.TabIndex = 168;
            label1.Text = "Agregar Pagos";
            // 
            // cmbDeudores
            // 
            cmbDeudores.DropDownWidth = 178;
            cmbDeudores.Location = new Point(170, 99);
            cmbDeudores.Margin = new Padding(3, 2, 3, 2);
            cmbDeudores.Name = "cmbDeudores";
            cmbDeudores.Size = new Size(228, 27);
            cmbDeudores.StateActive.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateActive.ComboBox.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateActive.ComboBox.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateActive.ComboBox.Border.Rounding = 10F;
            cmbDeudores.StateActive.ComboBox.Content.Color1 = Color.Navy;
            cmbDeudores.StateActive.ComboBox.Content.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDeudores.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateCommon.ComboBox.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateCommon.ComboBox.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateCommon.ComboBox.Border.Rounding = 70F;
            cmbDeudores.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbDeudores.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDeudores.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbDeudores.StateCommon.DropBack.Color1 = Color.SkyBlue;
            cmbDeudores.StateCommon.DropBack.Color2 = Color.SkyBlue;
            cmbDeudores.StateCommon.Item.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateCommon.Item.Back.Color2 = Color.SkyBlue;
            cmbDeudores.StateCommon.Item.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateCommon.Item.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateCommon.Item.Border.Rounding = 70F;
            cmbDeudores.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbDeudores.StateCommon.Item.Content.ShortText.Color2 = Color.Navy;
            cmbDeudores.StateDisabled.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateDisabled.ComboBox.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateDisabled.ComboBox.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateDisabled.ComboBox.Content.Color1 = Color.Navy;
            cmbDeudores.StateDisabled.Item.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateDisabled.Item.Back.Color2 = Color.SkyBlue;
            cmbDeudores.StateDisabled.Item.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateDisabled.Item.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateDisabled.Item.Content.ShortText.Color1 = Color.Navy;
            cmbDeudores.StateDisabled.Item.Content.ShortText.Color2 = Color.Navy;
            cmbDeudores.StateNormal.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateNormal.ComboBox.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateNormal.ComboBox.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateNormal.ComboBox.Content.Color1 = Color.Navy;
            cmbDeudores.StateNormal.Item.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateNormal.Item.Back.Color2 = Color.SkyBlue;
            cmbDeudores.StateNormal.Item.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateNormal.Item.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateNormal.Item.Content.ShortText.Color1 = Color.Navy;
            cmbDeudores.StateNormal.Item.Content.ShortText.Color2 = Color.Navy;
            cmbDeudores.StateTracking.Item.Back.Color1 = Color.SkyBlue;
            cmbDeudores.StateTracking.Item.Back.Color2 = Color.SkyBlue;
            cmbDeudores.StateTracking.Item.Border.Color1 = Color.SkyBlue;
            cmbDeudores.StateTracking.Item.Border.Color2 = Color.SkyBlue;
            cmbDeudores.StateTracking.Item.Content.ShortText.Color1 = Color.Navy;
            cmbDeudores.StateTracking.Item.Content.ShortText.Color2 = Color.Navy;
            cmbDeudores.TabIndex = 166;
            // 
            // kryptonGroup1
            // 
            kryptonGroup1.Location = new Point(49, 34);
            kryptonGroup1.Margin = new Padding(3, 2, 3, 2);
            kryptonGroup1.Size = new Size(165, 44);
            kryptonGroup1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup1.StateCommon.Border.Rounding = 70F;
            kryptonGroup1.TabIndex = 164;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(435, 18);
            panel1.TabIndex = 163;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(0, 248);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(435, 18);
            panel3.TabIndex = 162;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 268);
            pictureBox1.TabIndex = 161;
            pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(414, -4);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(21, 273);
            pictureBox4.TabIndex = 160;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(32, 99);
            label2.Name = "label2";
            label2.Size = new Size(132, 20);
            label2.TabIndex = 193;
            label2.Text = "Nombre del Deudor:";
            label2.Click += label2_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(33, 138);
            label3.Name = "label3";
            label3.Size = new Size(99, 20);
            label3.TabIndex = 194;
            label3.Text = "Pago a Deuda:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(330, 216);
            label5.Name = "label5";
            label5.Size = new Size(78, 32);
            label5.TabIndex = 195;
            label5.Text = "BAMS";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(49, 190);
            btnAceptar.Margin = new Padding(3, 2, 3, 2);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(106, 29);
            btnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateCommon.Back.Color2 = Color.White;
            btnAceptar.StateCommon.Border.Rounding = 30F;
            btnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            btnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            btnAceptar.TabIndex = 196;
            btnAceptar.Values.DropDownArrowColor = Color.Empty;
            btnAceptar.Values.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(186, 190);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(106, 29);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 30F;
            btnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancelar.StatePressed.Back.Color1 = Color.Transparent;
            btnCancelar.StatePressed.Back.Color2 = Color.Transparent;
            btnCancelar.TabIndex = 197;
            btnCancelar.Values.DropDownArrowColor = Color.Empty;
            btnCancelar.Values.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // Pago_Deuda
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(434, 266);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtMonto);
            Controls.Add(label1);
            Controls.Add(cmbDeudores);
            Controls.Add(kryptonGroup1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Pago_Deuda";
            Text = "Pago_Deuda";
            Load += Pago_Deuda_Load;
            ((System.ComponentModel.ISupportInitialize)cmbDeudores).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Krypton.Toolkit.KryptonTextBox txtMonto;
        private Label label1;
        private Krypton.Toolkit.KryptonComboBox cmbDeudores;
        private Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private Panel panel1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label3;
        private Label label5;
        private Krypton.Toolkit.KryptonButton btnAceptar;
        private Krypton.Toolkit.KryptonButton btnCancelar;
    }
}