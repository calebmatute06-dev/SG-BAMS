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
            dgvProductosCompra = new Krypton.Toolkit.KryptonDataGridView();
            ID = new DataGridViewTextBoxColumn();
            Producto = new DataGridViewTextBoxColumn();
            Cantidad = new DataGridViewTextBoxColumn();
            Precio = new DataGridViewTextBoxColumn();
            Subtotal = new DataGridViewTextBoxColumn();
            lblIDCompra = new Krypton.Toolkit.KryptonLabel();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            lblTotal = new Krypton.Toolkit.KryptonLabel();
            label8 = new Label();
            btnAceptar = new Krypton.Toolkit.KryptonButton();
            kryptonButton4 = new Krypton.Toolkit.KryptonButton();
            kryptonButton5 = new Krypton.Toolkit.KryptonButton();
            kryptonButton13 = new Krypton.Toolkit.KryptonButton();
            cmbFormaPago = new Krypton.Toolkit.KryptonComboBox();
            cmbProveedor = new Krypton.Toolkit.KryptonComboBox();
            label4 = new Label();
            txtPrecio = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductosCompra).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbFormaPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbProveedor).BeginInit();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(901, -2);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 549);
            pictureBox4.TabIndex = 9;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(1, -2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 549);
            pictureBox1.TabIndex = 10;
            pictureBox1.TabStop = false;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(1, 535);
            panel3.Name = "panel3";
            panel3.Size = new Size(924, 24);
            panel3.TabIndex = 54;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(1, -2);
            panel1.Name = "panel1";
            panel1.Size = new Size(912, 24);
            panel1.TabIndex = 55;
            // 
            // kryptonGroup1
            // 
            kryptonGroup1.Location = new Point(66, 49);
            kryptonGroup1.Size = new Size(165, 59);
            kryptonGroup1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup1.StateCommon.Border.Rounding = 70F;
            kryptonGroup1.TabIndex = 80;
            // 
            // dtpFechaPedido
            // 
            dtpFechaPedido.Location = new Point(591, 66);
            dtpFechaPedido.Name = "dtpFechaPedido";
            dtpFechaPedido.Size = new Size(293, 218);
            dtpFechaPedido.TabIndex = 87;
            // 
            // sqlCommand1
            // 
            sqlCommand1.CommandTimeout = 30;
            sqlCommand1.EnableOptimizedParameterBinding = false;
            // 
            // dgvProductosCompra
            // 
            dgvProductosCompra.BorderStyle = BorderStyle.None;
            dgvProductosCompra.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductosCompra.Columns.AddRange(new DataGridViewColumn[] { ID, Producto, Cantidad, Precio, Subtotal });
            dgvProductosCompra.Location = new Point(66, 301);
            dgvProductosCompra.Name = "dgvProductosCompra";
            dgvProductosCompra.ReadOnly = true;
            dgvProductosCompra.RowHeadersWidth = 51;
            dgvProductosCompra.Size = new Size(643, 183);
            dgvProductosCompra.TabIndex = 95;
            // 
            // ID
            // 
            ID.HeaderText = "ID";
            ID.MinimumWidth = 6;
            ID.Name = "ID";
            ID.ReadOnly = true;
            ID.Width = 125;
            // 
            // Producto
            // 
            Producto.HeaderText = "Producto";
            Producto.MinimumWidth = 6;
            Producto.Name = "Producto";
            Producto.ReadOnly = true;
            Producto.Width = 125;
            // 
            // Cantidad
            // 
            Cantidad.HeaderText = "Cantidad";
            Cantidad.MinimumWidth = 6;
            Cantidad.Name = "Cantidad";
            Cantidad.ReadOnly = true;
            Cantidad.Width = 125;
            // 
            // Precio
            // 
            Precio.HeaderText = "Precio";
            Precio.MinimumWidth = 6;
            Precio.Name = "Precio";
            Precio.ReadOnly = true;
            Precio.Width = 125;
            // 
            // Subtotal
            // 
            Subtotal.HeaderText = "Subtotal";
            Subtotal.MinimumWidth = 6;
            Subtotal.Name = "Subtotal";
            Subtotal.ReadOnly = true;
            Subtotal.Width = 125;
            // 
            // lblIDCompra
            // 
            lblIDCompra.Location = new Point(203, 117);
            lblIDCompra.Name = "lblIDCompra";
            lblIDCompra.Size = new Size(136, 31);
            lblIDCompra.StateCommon.ShortText.Color1 = Color.Navy;
            lblIDCompra.StateCommon.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblIDCompra.TabIndex = 97;
            lblIDCompra.Values.Text = "xxx-xxx";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(83, 66);
            label1.Name = "label1";
            label1.Size = new Size(133, 24);
            label1.TabIndex = 150;
            label1.Text = "Ingresar Compra";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(75, 121);
            label2.Name = "label2";
            label2.Size = new Size(105, 24);
            label2.TabIndex = 171;
            label2.Text = "ID compras: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(70, 155);
            label3.Name = "label3";
            label3.Size = new Size(127, 24);
            label3.TabIndex = 172;
            label3.Text = "Forma de pago:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(73, 193);
            label5.Name = "label5";
            label5.Size = new Size(90, 24);
            label5.TabIndex = 174;
            label5.Text = "Proveedor:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(66, 271);
            label6.Name = "label6";
            label6.Size = new Size(88, 24);
            label6.TabIndex = 175;
            label6.Text = "Productos:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(713, 439);
            label7.Name = "label7";
            label7.Size = new Size(51, 24);
            label7.TabIndex = 176;
            label7.Text = "Total:";
            // 
            // lblTotal
            // 
            lblTotal.Location = new Point(759, 437);
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
            label8.Location = new Point(673, 39);
            label8.Name = "label8";
            label8.Size = new Size(131, 24);
            label8.TabIndex = 180;
            label8.Text = "Fecha de pedido";
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(42, 490);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(121, 39);
            btnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateCommon.Back.Color2 = Color.White;
            btnAceptar.StateCommon.Border.Rounding = 20F;
            btnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            btnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            btnAceptar.TabIndex = 183;
            btnAceptar.Values.DropDownArrowColor = Color.Empty;
            btnAceptar.Values.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click;
            // 
            // kryptonButton4
            // 
            kryptonButton4.Location = new Point(175, 490);
            kryptonButton4.Name = "kryptonButton4";
            kryptonButton4.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton4.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton4.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton4.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton4.Size = new Size(121, 39);
            kryptonButton4.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton4.StateCommon.Back.Color2 = Color.White;
            kryptonButton4.StateCommon.Border.Rounding = 20F;
            kryptonButton4.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton4.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kryptonButton4.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton4.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton4.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton4.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton4.TabIndex = 182;
            kryptonButton4.Values.DropDownArrowColor = Color.Empty;
            kryptonButton4.Values.Text = "Cancelar";
            kryptonButton4.Click += kryptonButton4_Click;
            // 
            // kryptonButton5
            // 
            kryptonButton5.Location = new Point(637, 490);
            kryptonButton5.Name = "kryptonButton5";
            kryptonButton5.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton5.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton5.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton5.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton5.Size = new Size(159, 39);
            kryptonButton5.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton5.StateCommon.Back.Color2 = Color.White;
            kryptonButton5.StateCommon.Border.Rounding = 20F;
            kryptonButton5.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton5.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kryptonButton5.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton5.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton5.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton5.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton5.TabIndex = 181;
            kryptonButton5.Values.DropDownArrowColor = Color.Empty;
            kryptonButton5.Values.Text = "Agregar Producto";
            kryptonButton5.Click += kryptonButton5_Click;
            // 
            // kryptonButton13
            // 
            kryptonButton13.Location = new Point(790, 490);
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
            // cmbFormaPago
            // 
            cmbFormaPago.DropDownWidth = 178;
            cmbFormaPago.Location = new Point(203, 151);
            cmbFormaPago.Name = "cmbFormaPago";
            cmbFormaPago.Size = new Size(178, 32);
            cmbFormaPago.StateActive.ComboBox.Border.Rounding = 10F;
            cmbFormaPago.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbFormaPago.StateCommon.ComboBox.Border.Rounding = 70F;
            cmbFormaPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbFormaPago.StateCommon.Item.Border.Rounding = 90F;
            cmbFormaPago.TabIndex = 187;
            // 
            // cmbProveedor
            // 
            cmbProveedor.DropDownWidth = 178;
            cmbProveedor.Location = new Point(203, 190);
            cmbProveedor.Name = "cmbProveedor";
            cmbProveedor.Size = new Size(278, 32);
            cmbProveedor.StateActive.ComboBox.Border.Rounding = 10F;
            cmbProveedor.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbProveedor.StateCommon.ComboBox.Border.Rounding = 70F;
            cmbProveedor.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProveedor.StateCommon.Item.Border.Rounding = 90F;
            cmbProveedor.TabIndex = 188;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(73, 232);
            label4.Name = "label4";
            label4.Size = new Size(90, 24);
            label4.TabIndex = 189;
            label4.Text = "Proveedor:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(204, 227);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(277, 33);
            txtPrecio.StateCommon.Back.Color1 = Color.SkyBlue;
            txtPrecio.StateCommon.Border.Rounding = 10F;
            txtPrecio.TabIndex = 211;
            // 
            // Ingresar_datos__Compra_
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(925, 561);
            Controls.Add(txtPrecio);
            Controls.Add(label4);
            Controls.Add(cmbProveedor);
            Controls.Add(cmbFormaPago);
            Controls.Add(btnAceptar);
            Controls.Add(kryptonButton4);
            Controls.Add(kryptonButton5);
            Controls.Add(label8);
            Controls.Add(lblTotal);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(lblIDCompra);
            Controls.Add(dgvProductosCompra);
            Controls.Add(dtpFechaPedido);
            Controls.Add(kryptonGroup1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            Controls.Add(kryptonButton13);
            Name = "Ingresar_datos__Compra_";
            Text = "Ingresar_datos__Compra_";
            Load += Ingresar_datos__Compra__Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductosCompra).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbFormaPago).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbProveedor).EndInit();
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
        private Krypton.Toolkit.KryptonDataGridView dgvProductosCompra;
        private Krypton.Toolkit.KryptonLabel lblIDCompra;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label5;
        private Label label6;
        private Label label7;
        private Krypton.Toolkit.KryptonLabel lblTotal;
        private Label label8;
        private Krypton.Toolkit.KryptonButton btnAceptar;
        private Krypton.Toolkit.KryptonButton kryptonButton4;
        private Krypton.Toolkit.KryptonButton kryptonButton5;
        private Krypton.Toolkit.KryptonButton kryptonButton13;
        private Krypton.Toolkit.KryptonComboBox cmbFormaPago;
        private Krypton.Toolkit.KryptonComboBox cmbProveedor;
        private Label label4;
        private Krypton.Toolkit.KryptonTextBox txtPrecio;
        private DataGridViewTextBoxColumn ID;
        private DataGridViewTextBoxColumn Producto;
        private DataGridViewTextBoxColumn Cantidad;
        private DataGridViewTextBoxColumn Precio;
        private DataGridViewTextBoxColumn Subtotal;
    }
}