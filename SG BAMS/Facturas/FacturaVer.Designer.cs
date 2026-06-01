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
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            panel2 = new Panel();
            txtTotal = new Krypton.Toolkit.KryptonTextBox();
            panel1 = new Panel();
            label2 = new Label();
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
            txtSubtotal = new Krypton.Toolkit.KryptonTextBox();
            label8 = new Label();
            txtRebaja = new Krypton.Toolkit.KryptonTextBox();
            label10 = new Label();
            label5 = new Label();
            fechaDT = new DateTimePicker();
            ((System.ComponentModel.ISupportInitialize)cmbPago).BeginInit();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();
            // 
            // cmbPago
            // 
            cmbPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPago.DropDownWidth = 300;
            cmbPago.Enabled = false;
            cmbPago.Location = new Point(214, 406);
            cmbPago.Margin = new Padding(3, 2, 3, 2);
            cmbPago.Name = "cmbPago";
            cmbPago.Size = new Size(146, 27);
            cmbPago.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbPago.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbPago.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbPago.StateCommon.ComboBox.Content.Color1 = Color.Black;
            cmbPago.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbPago.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbPago.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPago.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbPago.TabIndex = 344;
            // 
            // txtBateriaVieja
            // 
            txtBateriaVieja.Location = new Point(213, 364);
            txtBateriaVieja.Name = "txtBateriaVieja";
            txtBateriaVieja.Size = new Size(172, 30);
            txtBateriaVieja.StateCommon.Back.Color1 = Color.White;
            txtBateriaVieja.StateCommon.Border.Color1 = Color.Navy;
            txtBateriaVieja.StateCommon.Border.Rounding = 5F;
            txtBateriaVieja.StateCommon.Content.Color1 = Color.Black;
            txtBateriaVieja.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtBateriaVieja.TabIndex = 343;
            // 
            // txtCliente
            // 
            txtCliente.Location = new Point(132, 102);
            txtCliente.Name = "txtCliente";
            txtCliente.Size = new Size(218, 30);
            txtCliente.StateCommon.Back.Color1 = Color.White;
            txtCliente.StateCommon.Border.Color1 = Color.Navy;
            txtCliente.StateCommon.Border.Rounding = 5F;
            txtCliente.StateCommon.Content.Color1 = Color.Navy;
            txtCliente.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtCliente.TabIndex = 341;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(43, 448);
            BtnSalir.Margin = new Padding(3, 2, 3, 2);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(128, 45);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 5F;
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
            panel2.Location = new Point(6, 237);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(562, 18);
            panel2.TabIndex = 236;
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(564, 436);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(96, 31);
            txtTotal.StateCommon.Back.Color1 = Color.White;
            txtTotal.StateCommon.Border.Color1 = Color.Navy;
            txtTotal.StateCommon.Border.Rounding = 5F;
            txtTotal.StateCommon.Content.Color1 = Color.Navy;
            txtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtTotal.TabIndex = 342;
            txtTotal.Text = "0";
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(965, 18);
            panel1.TabIndex = 324;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(500, 442);
            label2.Name = "label2";
            label2.Size = new Size(63, 22);
            label2.TabIndex = 329;
            label2.Text = "Total:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(388, 105);
            label7.Name = "label7";
            label7.Size = new Size(74, 22);
            label7.TabIndex = 333;
            label7.Text = "Fecha:";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(52, 410);
            label9.Name = "label9";
            label9.Size = new Size(158, 22);
            label9.TabIndex = 332;
            label9.Text = "Forma de Pago:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(53, 370);
            label1.Name = "label1";
            label1.Size = new Size(133, 22);
            label1.TabIndex = 331;
            label1.Text = "Batería Vieja:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(38, 105);
            label3.Name = "label3";
            label3.Size = new Size(81, 22);
            label3.TabIndex = 330;
            label3.Text = "Cliente:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 27.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(265, 27);
            label4.Name = "label4";
            label4.Size = new Size(222, 44);
            label4.TabIndex = 327;
            label4.Text = "Ver Factura";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(771, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(22, 514);
            panel3.TabIndex = 325;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(-1, 498);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(966, 18);
            panel4.TabIndex = 326;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Controls.Add(panel2);
            panel8.Location = new Point(0, -1);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(22, 516);
            panel8.TabIndex = 323;
            // 
            // dgvFacturas
            // 
            dgvFacturas.BackgroundColor = Color.SkyBlue;
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(80, 145);
            dgvFacturas.Margin = new Padding(3, 2, 3, 2);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.RowHeadersWidth = 51;
            dgvFacturas.Size = new Size(631, 200);
            dgvFacturas.TabIndex = 345;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(44, 58);
            label6.Name = "label6";
            label6.Size = new Size(99, 29);
            label6.TabIndex = 346;
            label6.Text = "Factura";
            // 
            // lblFactura
            // 
            lblFactura.AutoSize = true;
            lblFactura.BackColor = Color.Transparent;
            lblFactura.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFactura.ForeColor = Color.Navy;
            lblFactura.Location = new Point(153, 58);
            lblFactura.Name = "lblFactura";
            lblFactura.Size = new Size(20, 29);
            lblFactura.TabIndex = 347;
            lblFactura.Text = ".";
            // 
            // txtSubtotal
            // 
            txtSubtotal.Location = new Point(564, 358);
            txtSubtotal.Name = "txtSubtotal";
            txtSubtotal.Size = new Size(96, 31);
            txtSubtotal.StateCommon.Back.Color1 = Color.White;
            txtSubtotal.StateCommon.Border.Color1 = Color.Navy;
            txtSubtotal.StateCommon.Border.Rounding = 5F;
            txtSubtotal.StateCommon.Content.Color1 = Color.Navy;
            txtSubtotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtSubtotal.TabIndex = 351;
            txtSubtotal.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(464, 370);
            label8.Name = "label8";
            label8.Size = new Size(94, 22);
            label8.TabIndex = 350;
            label8.Text = "Subtotal:";
            // 
            // txtRebaja
            // 
            txtRebaja.Location = new Point(564, 398);
            txtRebaja.Name = "txtRebaja";
            txtRebaja.Size = new Size(96, 31);
            txtRebaja.StateCommon.Back.Color1 = Color.White;
            txtRebaja.StateCommon.Border.Color1 = Color.Navy;
            txtRebaja.StateCommon.Border.Rounding = 5F;
            txtRebaja.StateCommon.Content.Color1 = Color.Navy;
            txtRebaja.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            txtRebaja.TabIndex = 349;
            txtRebaja.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(435, 406);
            label10.Name = "label10";
            label10.Size = new Size(128, 22);
            label10.TabIndex = 348;
            label10.Text = "Descuentos:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(676, 470);
            label5.Name = "label5";
            label5.Size = new Size(84, 29);
            label5.TabIndex = 352;
            label5.Text = "BAMS";
            // 
            // fechaDT
            // 
            fechaDT.CalendarFont = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fechaDT.CalendarForeColor = Color.Navy;
            fechaDT.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            fechaDT.Location = new Point(464, 104);
            fechaDT.Margin = new Padding(3, 2, 3, 2);
            fechaDT.Name = "fechaDT";
            fechaDT.Size = new Size(284, 26);
            fechaDT.TabIndex = 353;
            // 
            // FacturaVer
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(792, 516);
            Controls.Add(fechaDT);
            Controls.Add(label5);
            Controls.Add(txtSubtotal);
            Controls.Add(label8);
            Controls.Add(txtRebaja);
            Controls.Add(label10);
            Controls.Add(lblFactura);
            Controls.Add(label6);
            Controls.Add(dgvFacturas);
            Controls.Add(cmbPago);
            Controls.Add(txtBateriaVieja);
            Controls.Add(txtCliente);
            Controls.Add(BtnSalir);
            Controls.Add(txtTotal);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(label7);
            Controls.Add(label9);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel8);
            Name = "FacturaVer";
            Text = "FacturaVer";
            Load += FacturaVer_Load;
            ((System.ComponentModel.ISupportInitialize)cmbPago).EndInit();
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonComboBox cmbPago;
        private Krypton.Toolkit.KryptonTextBox txtBateriaVieja;
        private Krypton.Toolkit.KryptonTextBox txtCliente;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Panel panel2;
        private Krypton.Toolkit.KryptonTextBox txtTotal;
        private Panel panel1;
        private Label label2;
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
        private Krypton.Toolkit.KryptonTextBox txtSubtotal;
        private Label label8;
        private Krypton.Toolkit.KryptonTextBox txtRebaja;
        private Label label10;
        private Label label5;
        private DateTimePicker fechaDT;
    }
}