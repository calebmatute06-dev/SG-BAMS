namespace SG_BAMS
{
    partial class FacturaVer
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
            cmbPago = new Krypton.Toolkit.KryptonComboBox();
            txtBateriaVieja = new Krypton.Toolkit.KryptonTextBox();
            txtCliente = new Krypton.Toolkit.KryptonTextBox();
            fechaDT = new MonthCalendar();
            kryptonGroupBox1 = new Krypton.Toolkit.KryptonGroupBox();
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            panel2 = new Panel();
            txtTotal = new Krypton.Toolkit.KryptonTextBox();
            panel1 = new Panel();
            label2 = new Label();
            kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            label5 = new Label();
            label7 = new Label();
            label9 = new Label();
            label1 = new Label();
            label3 = new Label();
            label4 = new Label();
            panel3 = new Panel();
            panel4 = new Panel();
            panel8 = new Panel();
            dgvFacturas = new DataGridView();
            label6 = new Label();
            lblFactura = new Label();
            ((System.ComponentModel.ISupportInitialize)cmbPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();
            // 
            // cmbPago
            // 
            cmbPago.DropDownWidth = 300;
            cmbPago.Location = new Point(202, 429);
            cmbPago.Name = "cmbPago";
            cmbPago.Size = new Size(146, 38);
            cmbPago.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbPago.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbPago.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbPago.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPago.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbPago.TabIndex = 344;
            // 
            // txtBateriaVieja
            // 
            txtBateriaVieja.Location = new Point(194, 165);
            txtBateriaVieja.Margin = new Padding(3, 4, 3, 4);
            txtBateriaVieja.Name = "txtBateriaVieja";
            txtBateriaVieja.Size = new Size(197, 36);
            txtBateriaVieja.StateCommon.Back.Color1 = Color.SkyBlue;
            txtBateriaVieja.StateCommon.Border.Rounding = 10F;
            txtBateriaVieja.StateCommon.Content.Color1 = Color.Navy;
            txtBateriaVieja.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtBateriaVieja.TabIndex = 343;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(194, 120);
            txtCliente.Margin = new Padding(3, 4, 3, 4);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(197, 36);
            txtCliente.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCliente.StateCommon.Border.Rounding = 10F;
            txtCliente.StateCommon.Content.Color1 = Color.Navy;
            txtCliente.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtCliente.TabIndex = 341;
            // 
            // fechaDT
            // 
            fechaDT.Location = new Point(737, 225);
            fechaDT.Margin = new Padding(10, 12, 10, 12);
            fechaDT.MaxSelectionCount = 1;
            fechaDT.Name = "fechaDT";
            fechaDT.TabIndex = 338;
            // 
            // kryptonGroupBox1
            // 
            kryptonGroupBox1.CaptionVisible = false;
            kryptonGroupBox1.Location = new Point(704, 187);
            kryptonGroupBox1.Margin = new Padding(3, 4, 3, 4);
            kryptonGroupBox1.Size = new Size(276, 288);
            kryptonGroupBox1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox1.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox1.TabIndex = 339;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(299, 494);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(146, 60);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 30F;
            BtnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnSalir.StatePressed.Back.Color1 = Color.Transparent;
            BtnSalir.StatePressed.Back.Color2 = Color.Transparent;
            BtnSalir.TabIndex = 335;
            BtnSalir.Values.DropDownArrowColor = Color.Empty;
            BtnSalir.Values.Text = "Cancelar";
            BtnSalir.Click += BtnSalir_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(7, 316);
            panel2.Name = "panel2";
            panel2.Size = new Size(642, 24);
            panel2.TabIndex = 236;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(531, 440);
            txtTotal.Margin = new Padding(3, 4, 3, 4);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(110, 36);
            txtTotal.StateCommon.Back.Color1 = Color.SkyBlue;
            txtTotal.StateCommon.Border.Rounding = 10F;
            txtTotal.StateCommon.Content.Color1 = Color.Navy;
            txtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtTotal.TabIndex = 342;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(16, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(981, 24);
            panel1.TabIndex = 324;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(472, 444);
            label2.Name = "label2";
            label2.Size = new Size(57, 24);
            label2.TabIndex = 329;
            label2.Text = "Total";
            // 
            // kryptonGroupBox3
            // 
            kryptonGroupBox3.CaptionVisible = false;
            kryptonGroupBox3.Location = new Point(24, 41);
            kryptonGroupBox3.Margin = new Padding(3, 4, 3, 4);
            kryptonGroupBox3.Size = new Size(255, 65);
            kryptonGroupBox3.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox3.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox3.TabIndex = 340;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(873, 529);
            label5.Name = "label5";
            label5.Size = new Size(113, 42);
            label5.TabIndex = 328;
            label5.Text = "BAMS";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(811, 159);
            label7.Name = "label7";
            label7.Size = new Size(67, 24);
            label7.TabIndex = 333;
            label7.Text = "Fecha";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(53, 440);
            label9.Name = "label9";
            label9.Size = new Size(153, 24);
            label9.TabIndex = 332;
            label9.Text = "Forma de Pago";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(53, 172);
            label1.Name = "label1";
            label1.Size = new Size(128, 24);
            label1.TabIndex = 331;
            label1.Text = "Bateria Vieja";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(53, 128);
            label3.Name = "label3";
            label3.Size = new Size(75, 24);
            label3.TabIndex = 330;
            label3.Text = "Cliente";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.SkyBlue;
            label4.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(58, 57);
            label4.Name = "label4";
            label4.Size = new Size(179, 35);
            label4.TabIndex = 327;
            label4.Text = "Ver Factura";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(986, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 600);
            panel3.TabIndex = 325;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(-5, 576);
            panel4.Name = "panel4";
            panel4.Size = new Size(1001, 24);
            panel4.TabIndex = 326;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Controls.Add(panel2);
            panel8.Location = new Point(-8, 0);
            panel8.Name = "panel8";
            panel8.Size = new Size(25, 600);
            panel8.TabIndex = 323;
            // 
            // dgvFacturas
            // 
            dgvFacturas.BackgroundColor = Color.SkyBlue;
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(48, 225);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.RowHeadersWidth = 51;
            dgvFacturas.Size = new Size(639, 188);
            dgvFacturas.TabIndex = 345;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(401, 57);
            label6.Name = "label6";
            label6.Size = new Size(124, 35);
            label6.TabIndex = 346;
            label6.Text = "Factura";
            // 
            // lblFactura
            // 
            lblFactura.AutoSize = true;
            lblFactura.BackColor = Color.Transparent;
            lblFactura.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFactura.ForeColor = Color.Navy;
            lblFactura.Location = new Point(531, 57);
            lblFactura.Name = "lblFactura";
            lblFactura.Size = new Size(23, 35);
            lblFactura.TabIndex = 347;
            lblFactura.Text = ".";
            // 
            // FacturaVer
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1003, 600);
            Controls.Add(lblFactura);
            Controls.Add(label6);
            Controls.Add(dgvFacturas);
            Controls.Add(cmbPago);
            Controls.Add(txtBateriaVieja);
            Controls.Add(txtCliente);
            Controls.Add(fechaDT);
            Controls.Add(kryptonGroupBox1);
            Controls.Add(BtnSalir);
            Controls.Add(txtTotal);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label5);
            Controls.Add(label7);
            Controls.Add(label9);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel8);
            Controls.Add(kryptonGroupBox3);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FacturaVer";
            Text = "FacturaVer";
            Load += FacturaVer_Load;
            ((System.ComponentModel.ISupportInitialize)cmbPago).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).EndInit();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonComboBox cmbPago;
        private Krypton.Toolkit.KryptonTextBox txtBateriaVieja;
        private Krypton.Toolkit.KryptonTextBox txtCliente;
        private MonthCalendar fechaDT;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox1;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Panel panel2;
        private Krypton.Toolkit.KryptonTextBox txtTotal;
        private Panel panel1;
        private Label label2;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Label label5;
        private Label label7;
        private Label label9;
        private Label label1;
        private Label label3;
        private Label label4;
        private Panel panel3;
        private Panel panel4;
        private Panel panel8;
        private DataGridView dgvFacturas;
        private Label label6;
        private Label lblFactura;
    }
}