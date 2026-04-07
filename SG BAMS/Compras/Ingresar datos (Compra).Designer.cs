namespace SG_BAMS
{
    partial class Ingresar_datos__Compra_
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
            pictureBox4 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel3 = new Panel();
            panel1 = new Panel();
            kryptonGroup1 = new Krypton.Toolkit.KryptonGroup();
            dtpFechaPedido = new Krypton.Toolkit.KryptonMonthCalendar();
            sqlCommand1 = new Microsoft.Data.SqlClient.SqlCommand();
            lblIDCompra = new Krypton.Toolkit.KryptonLabel();
            label1 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblTotal = new Krypton.Toolkit.KryptonLabel();
            label8 = new Label();
            txtNotaDetalle = new Krypton.Toolkit.KryptonTextBox();
            cmbProveedor = new Krypton.Toolkit.KryptonComboBox();
            cmbFormaPago = new Krypton.Toolkit.KryptonComboBox();
            btnAgregar = new Krypton.Toolkit.KryptonButton();
            btnAceptar = new Krypton.Toolkit.KryptonButton();
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            btnQuitar = new Krypton.Toolkit.KryptonButton();
            label9 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label4 = new Label();
            dgvIngresarCompra = new DataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbFormaPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresarCompra).BeginInit();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(896, -2);
            pictureBox4.Margin = new Padding(3, 2, 3, 2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(20, 430);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-1, -2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 424);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(-1, 404);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(917, 18);
            panel3.TabIndex = 54;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(-1, -2);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(898, 18);
            panel1.TabIndex = 55;
            // 
            // kryptonGroup1
            // 
            kryptonGroup1.Location = new Point(24, 19);
            kryptonGroup1.Margin = new Padding(3, 2, 3, 2);
            kryptonGroup1.Size = new Size(175, 44);
            kryptonGroup1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup1.StateCommon.Border.Rounding = 70F;
            kryptonGroup1.TabIndex = 80;
            // 
            // dtpFechaPedido
            // 
            dtpFechaPedido.Enabled = false;
            dtpFechaPedido.Location = new Point(624, 136);
            dtpFechaPedido.Margin = new Padding(3, 2, 3, 2);
            dtpFechaPedido.Name = "dtpFechaPedido";
            dtpFechaPedido.Size = new Size(230, 182);
            dtpFechaPedido.TabIndex = 87;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // lblIDCompra
            // 
            lblIDCompra.Location = new Point(145, 70);
            lblIDCompra.Margin = new Padding(3, 2, 3, 2);
            lblIDCompra.Name = "lblIDCompra";
            lblIDCompra.Size = new Size(119, 23);
            lblIDCompra.StateCommon.ShortText.Color1 = Color.Navy;
            lblIDCompra.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIDCompra.TabIndex = 97;
            lblIDCompra.Values.Text = "x";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(39, 32);
            label1.Name = "label1";
            label1.Size = new Size(137, 19);
            label1.TabIndex = 150;
            label1.Text = "Ingresar Compra";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(38, 182);
            label6.Name = "label6";
            label6.Size = new Size(106, 23);
            label6.TabIndex = 175;
            label6.Text = "Productos:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(624, 316);
            label7.Name = "label7";
            label7.Size = new Size(41, 20);
            label7.TabIndex = 176;
            label7.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.Location = new Point(664, 315);
            lblTotal.Margin = new Padding(3, 2, 3, 2);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(119, 23);
            lblTotal.StateCommon.ShortText.Color1 = Color.Navy;
            lblTotal.StateCommon.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblTotal.TabIndex = 177;
            lblTotal.Values.Text = "L xx.xx";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(699, 89);
            label8.Name = "label8";
            label8.Size = new Size(113, 20);
            label8.TabIndex = 180;
            label8.Text = "Fecha de pedido";
            // 
            // txtNotaDetalle
            // 
            txtNotaDetalle.Location = new Point(197, 156);
            txtNotaDetalle.Margin = new Padding(3, 2, 3, 2);
            txtNotaDetalle.Name = "txtNotaDetalle";
            txtNotaDetalle.Size = new Size(242, 29);
            txtNotaDetalle.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNotaDetalle.StateCommon.Border.Rounding = 10F;
            txtNotaDetalle.TabIndex = 211;
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownWidth = 300;
            cmbProveedor.Location = new Point(197, 123);
            cmbProveedor.Margin = new Padding(3, 2, 3, 2);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(273, 34);
            cmbProveedor.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbProveedor.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbProveedor.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProveedor.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbProveedor.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProveedor.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbProveedor.TabIndex = 336;
            // 
            // cmbFormaPago
            // 
            cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormaPago.DropDownWidth = 300;
            cmbFormaPago.Location = new Point(197, 89);
            cmbFormaPago.Margin = new Padding(3, 2, 3, 2);
            cmbFormaPago.Name = "cmbFormaPago";
            cmbFormaPago.Size = new Size(172, 34);
            cmbFormaPago.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbFormaPago.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbFormaPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbFormaPago.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbFormaPago.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFormaPago.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbFormaPago.TabIndex = 337;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(487, 357);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(186, 40);
            btnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateCommon.Back.Color2 = Color.White;
            btnAgregar.StateCommon.Border.Rounding = 40F;
            btnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAgregar.StatePressed.Back.Color1 = Color.Transparent;
            btnAgregar.StatePressed.Back.Color2 = Color.Transparent;
            btnAgregar.TabIndex = 339;
            btnAgregar.Values.DropDownArrowColor = Color.Empty;
            btnAgregar.Values.Text = "Agregar Producto";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(32, 357);
            btnAceptar.Margin = new Padding(3, 2, 3, 2);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(114, 41);
            btnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateCommon.Back.Color2 = Color.White;
            btnAceptar.StateCommon.Border.Rounding = 40F;
            btnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            btnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            btnAceptar.TabIndex = 340;
            btnAceptar.Values.DropDownArrowColor = Color.Empty;
            btnAceptar.Values.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click_1;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(151, 358);
            btnCancelar.Margin = new Padding(3, 2, 3, 2);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(114, 41);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 40F;
            btnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancelar.StatePressed.Back.Color1 = Color.Transparent;
            btnCancelar.StatePressed.Back.Color2 = Color.Transparent;
            btnCancelar.TabIndex = 341;
            btnCancelar.Values.DropDownArrowColor = Color.Empty;
            btnCancelar.Values.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnQuitar
            // 
            btnQuitar.Location = new Point(353, 357);
            btnQuitar.Margin = new Padding(3, 2, 3, 2);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnQuitar.OverrideDefault.Back.Color2 = Color.White;
            btnQuitar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnQuitar.OverrideFocus.Back.Color2 = Color.White;
            btnQuitar.Size = new Size(128, 41);
            btnQuitar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnQuitar.StateCommon.Back.Color2 = Color.White;
            btnQuitar.StateCommon.Border.Rounding = 40F;
            btnQuitar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnQuitar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnQuitar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnQuitar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnQuitar.StatePressed.Back.Color1 = Color.Transparent;
            btnQuitar.StatePressed.Back.Color2 = Color.Transparent;
            btnQuitar.TabIndex = 342;
            btnQuitar.Values.DropDownArrowColor = Color.Empty;
            btnQuitar.Values.Text = "Quitar";
            btnQuitar.Click += btnQuitar_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(32, 70);
            label9.Name = "label9";
            label9.Size = new Size(115, 22);
            label9.TabIndex = 343;
            label9.Text = "ID Compra:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(27, 98);
            label2.Name = "label2";
            label2.Size = new Size(157, 22);
            label2.TabIndex = 344;
            label2.Text = "Forma de pago:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(70, 128);
            label3.Name = "label3";
            label3.Size = new Size(115, 22);
            label3.TabIndex = 345;
            label3.Text = "Proveedor:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(124, 158);
            label5.Name = "label5";
            label5.Size = new Size(59, 22);
            label5.TabIndex = 346;
            label5.Text = "Nota:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(782, 373);
            label4.Name = "label4";
            label4.Size = new Size(84, 29);
            label4.TabIndex = 347;
            label4.Text = "BAMS";
            // 
            // dgvIngresarCompra
            // 
            dgvIngresarCompra.BackgroundColor = Color.SkyBlue;
            dgvIngresarCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIngresarCompra.Location = new Point(39, 205);
            dgvIngresarCompra.Name = "dgvIngresarCompra";
            dgvIngresarCompra.Size = new Size(563, 145);
            dgvIngresarCompra.TabIndex = 348;
            dgvIngresarCompra.CellBeginEdit += dgvIngresarCompra_CellBeginEdit;
            dgvIngresarCompra.CellValueChanged += dgvIngresarCompra_CellValueChanged_1;
            // 
            // Ingresar_datos__Compra_
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(913, 421);
            Controls.Add(dgvIngresarCompra);
            Controls.Add(label4);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label9);
            Controls.Add(btnQuitar);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(btnAgregar);
            Controls.Add(cmbFormaPago);
            Controls.Add(cmbProveedor);
            Controls.Add(txtNotaDetalle);
            Controls.Add(label8);
            Controls.Add(lblTotal);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label1);
            Controls.Add(lblIDCompra);
            Controls.Add(dtpFechaPedido);
            Controls.Add(kryptonGroup1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Ingresar_datos__Compra_";
            Text = "Ingresar_datos__Compra_";
            Load += Ingresar_datos__Compra__Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbProveedor).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbFormaPago).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresarCompra).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox4;
        private PictureBox pictureBox1;
        private Panel panel3;
        private Panel panel1;
        private Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private Krypton.Toolkit.KryptonMonthCalendar dtpFechaPedido;
        private Microsoft.Data.SqlClient.SqlCommand sqlCommand1;
        private Krypton.Toolkit.KryptonLabel lblIDCompra;
        private Label label1;
        private Label label6;
        private Label label7;
        private Krypton.Toolkit.KryptonLabel lblTotal;
        private Label label8;
        private Krypton.Toolkit.KryptonTextBox txtNotaDetalle;
        private Krypton.Toolkit.KryptonComboBox cmbProveedor;
        private Krypton.Toolkit.KryptonComboBox cmbFormaPago;
        private Krypton.Toolkit.KryptonButton btnAgregar;
        private Krypton.Toolkit.KryptonButton btnAceptar;
        private Krypton.Toolkit.KryptonButton btnCancelar;
        private Krypton.Toolkit.KryptonButton btnQuitar;
        private Label label9;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label4;
        private DataGridView dgvIngresarCompra;
    }
}