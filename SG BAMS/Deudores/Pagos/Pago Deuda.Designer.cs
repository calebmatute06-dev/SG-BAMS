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
            panel1 = new Panel();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox4 = new PictureBox();
            label2 = new Label();
            label3 = new Label();
            btnAceptar = new Krypton.Toolkit.KryptonButton();
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            label5 = new Label();
            cmbDeudores = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbDeudores).BeginInit();
            SuspendLayout();
            // 
            // txtMonto
            // 
            txtMonto.Location = new Point(194, 181);
            txtMonto.Name = "txtMonto";
            txtMonto.Size = new Size(325, 30);
            txtMonto.StateCommon.Back.Color1 = Color.White;
            txtMonto.StateCommon.Border.Color1 = Color.Navy;
            txtMonto.StateCommon.Border.Rounding = 5F;
            txtMonto.StateCommon.Content.Color1 = Color.Gray;
            txtMonto.StateCommon.Content.Font = new Font("Arial Narrow", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtMonto.TabIndex = 169;
            txtMonto.KeyPress += txtMonto_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(157, 47);
            label1.Name = "label1";
            label1.Size = new Size(228, 35);
            label1.TabIndex = 168;
            label1.Text = "Agregar Pagos";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(562, 24);
            panel1.TabIndex = 163;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(0, 331);
            panel3.Name = "panel3";
            panel3.Size = new Size(562, 24);
            panel3.TabIndex = 162;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 357);
            pictureBox1.TabIndex = 161;
            pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(538, -7);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 364);
            pictureBox4.TabIndex = 160;
            pictureBox4.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(37, 132);
            label2.Name = "label2";
            label2.Size = new Size(158, 24);
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
            label3.Location = new Point(38, 184);
            label3.Name = "label3";
            label3.Size = new Size(120, 24);
            label3.TabIndex = 194;
            label3.Text = "Pago a Deuda:";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(120, 265);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(121, 39);
            btnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateCommon.Back.Color2 = Color.White;
            btnAceptar.StateCommon.Border.Rounding = 5F;
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
            btnCancelar.Location = new Point(264, 265);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(121, 39);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 5F;
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
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(416, 293);
            label5.Name = "label5";
            label5.Size = new Size(102, 35);
            label5.TabIndex = 345;
            label5.Text = "BAMS";
            // 
            // cmbDeudores
            // 
            cmbDeudores.DropDownWidth = 300;
            cmbDeudores.Location = new Point(194, 117);
            cmbDeudores.Name = "cmbDeudores";
            cmbDeudores.Size = new Size(325, 30);
            cmbDeudores.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbDeudores.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbDeudores.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbDeudores.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbDeudores.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbDeudores.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbDeudores.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbDeudores.TabIndex = 346;
            // 
            // Pago_Deuda
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(560, 355);
            Controls.Add(cmbDeudores);
            Controls.Add(label5);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(txtMonto);
            Controls.Add(label1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            Name = "Pago_Deuda";
            Text = "Pago_Deuda";
            Load += Pago_Deuda_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbDeudores).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Krypton.Toolkit.KryptonTextBox txtMonto;
        private Label label1;
        private Panel panel1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox4;
        private Label label2;
        private Label label3;
        private Krypton.Toolkit.KryptonButton btnAceptar;
        private Krypton.Toolkit.KryptonButton btnCancelar;
        private Label label5;
        private Krypton.Toolkit.KryptonComboBox cmbDeudores;
    }
}