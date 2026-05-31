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
            ((System.ComponentModel.ISupportInitialize)cmbProveedor).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbFormaPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvIngresarCompra).BeginInit();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(1024, -3);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(23, 763);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-1, -3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 763);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(-1, 739);
            panel3.Name = "panel3";
            panel3.Size = new Size(1048, 24);
            panel3.TabIndex = 54;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(-1, -3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1026, 24);
            panel1.TabIndex = 55;
            // 
            // dtpFechaPedido
            // 
            dtpFechaPedido.Enabled = false;
            dtpFechaPedido.Location = new Point(725, 63);
            dtpFechaPedido.Name = "dtpFechaPedido";
            dtpFechaPedido.Size = new Size(293, 218);
            dtpFechaPedido.TabIndex = 87;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // lblIDCompra
            // 
            lblIDCompra.Location = new Point(166, 92);
            lblIDCompra.Name = "lblIDCompra";
            lblIDCompra.Size = new Size(136, 31);
            lblIDCompra.StateCommon.ShortText.Color1 = Color.Navy;
            lblIDCompra.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblIDCompra.TabIndex = 97;
            lblIDCompra.Values.Text = "x";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(362, 33);
            label1.Name = "label1";
            label1.Size = new Size(337, 46);
            label1.TabIndex = 150;
            label1.Text = "Ingresar Compra";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Black", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(142, 252);
            label6.Name = "label6";
            label6.Size = new Size(128, 28);
            label6.TabIndex = 175;
            label6.Text = "Productos:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(813, 631);
            label7.Name = "label7";
            label7.Size = new Size(51, 24);
            label7.TabIndex = 176;
            label7.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.Location = new Point(859, 630);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(136, 31);
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
            label8.Location = new Point(807, 36);
            label8.Name = "label8";
            label8.Size = new Size(131, 24);
            label8.TabIndex = 180;
            label8.Text = "Fecha de pedido";
            // 
            // txtNotaDetalle
            // 
            txtNotaDetalle.CueHint.Color1 = Color.DimGray;
            txtNotaDetalle.CueHint.CueHintText = "Solo letras y espacios";
            txtNotaDetalle.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtNotaDetalle.Location = new Point(225, 216);
            txtNotaDetalle.Name = "txtNotaDetalle";
            txtNotaDetalle.Size = new Size(277, 33);
            txtNotaDetalle.StateCommon.Back.Color1 = Color.White;
            txtNotaDetalle.StateCommon.Border.Color1 = Color.Navy;
            txtNotaDetalle.StateCommon.Border.Rounding = 10F;
            txtNotaDetalle.TabIndex = 211;
            // 
            // cmbProveedor
            // 
            cmbProveedor.CueHint.Color1 = Color.DimGray;
            cmbProveedor.CueHint.CueHintText = "Seleccione un proveedor";
            cmbProveedor.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProveedor.DropDownWidth = 300;
            cmbProveedor.Location = new Point(225, 165);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(312, 41);
            cmbProveedor.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbProveedor.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbProveedor.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbProveedor.StateCommon.ComboBox.Content.Color1 = Color.Black;
            cmbProveedor.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProveedor.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProveedor.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbProveedor.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProveedor.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbProveedor.TabIndex = 336;
            // 
            // cmbFormaPago
            // 
            cmbFormaPago.CueHint.Color1 = Color.DimGray;
            cmbFormaPago.CueHint.CueHintText = "Seleccione un pago";
            cmbFormaPago.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFormaPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbFormaPago.DropDownWidth = 300;
            cmbFormaPago.Location = new Point(225, 115);
            cmbFormaPago.Name = "cmbFormaPago";
            cmbFormaPago.Size = new Size(219, 41);
            cmbFormaPago.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbFormaPago.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbFormaPago.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbFormaPago.StateCommon.ComboBox.Content.Color1 = Color.Black;
            cmbFormaPago.StateCommon.ComboBox.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFormaPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbFormaPago.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbFormaPago.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbFormaPago.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbFormaPago.TabIndex = 337;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(702, 677);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(213, 53);
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
            btnAceptar.Location = new Point(37, 677);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(130, 55);
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
            btnCancelar.Location = new Point(173, 679);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(130, 55);
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
            btnQuitar.Location = new Point(548, 677);
            btnQuitar.Name = "btnQuitar";
            btnQuitar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnQuitar.OverrideDefault.Back.Color2 = Color.White;
            btnQuitar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnQuitar.OverrideFocus.Back.Color2 = Color.White;
            btnQuitar.Size = new Size(146, 55);
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
            label9.Location = new Point(37, 92);
            label9.Name = "label9";
            label9.Size = new Size(133, 27);
            label9.TabIndex = 343;
            label9.Text = "ID Compra:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(31, 127);
            label2.Name = "label2";
            label2.Size = new Size(182, 27);
            label2.TabIndex = 344;
            label2.Text = "Forma de pago:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(80, 177);
            label3.Name = "label3";
            label3.Size = new Size(133, 27);
            label3.TabIndex = 345;
            label3.Text = "Proveedor:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(142, 220);
            label5.Name = "label5";
            label5.Size = new Size(71, 27);
            label5.TabIndex = 346;
            label5.Text = "Nota:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(921, 697);
            label4.Name = "label4";
            label4.Size = new Size(102, 35);
            label4.TabIndex = 347;
            label4.Text = "BAMS";
            // 
            // dgvIngresarCompra
            // 
            dgvIngresarCompra.BackgroundColor = Color.SkyBlue;
            dgvIngresarCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvIngresarCompra.Location = new Point(142, 284);
            dgvIngresarCompra.Margin = new Padding(3, 4, 3, 4);
            dgvIngresarCompra.Name = "dgvIngresarCompra";
            dgvIngresarCompra.RowHeadersWidth = 51;
            dgvIngresarCompra.Size = new Size(643, 373);
            dgvIngresarCompra.TabIndex = 348;
            dgvIngresarCompra.CellBeginEdit += dgvIngresarCompra_CellBeginEdit;
            dgvIngresarCompra.CellValueChanged += dgvIngresarCompra_CellValueChanged_1;
            // 
            // Ingresar_datos__Compra_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1043, 761);
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
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Ingresar_datos__Compra_";
            Text = "Ingresar_datos__Compra_";
            Load += Ingresar_datos__Compra__Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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