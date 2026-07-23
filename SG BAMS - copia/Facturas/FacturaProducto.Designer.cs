namespace SG_BAMS
{
    partial class FacturaProducto
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
            label4 = new Label();
            panel3 = new Panel();
            panel1 = new Panel();
            panel4 = new Panel();
            panel8 = new Panel();
            Nombre = new Label();
            label1 = new Label();
            BtnAceptar = new Krypton.Toolkit.KryptonButton();
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            cmbProductos = new Krypton.Toolkit.KryptonComboBox();
            txtCantidad = new Krypton.Toolkit.KryptonTextBox();
            lblStock = new Label();
            lblNumero = new Label();
            btnEscanear = new Krypton.Toolkit.KryptonButton();
            txtCodigo = new Krypton.Toolkit.KryptonTextBox();
            label2 = new Label();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)cmbProductos).BeginInit();
            SuspendLayout();
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(205, 40);
            label4.Name = "label4";
            label4.Size = new Size(307, 40);
            label4.TabIndex = 258;
            label4.Text = "Agregar Producto";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(675, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 400);
            panel3.TabIndex = 256;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(22, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(665, 24);
            panel1.TabIndex = 255;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(2, 307);
            panel4.Name = "panel4";
            panel4.Size = new Size(688, 24);
            panel4.TabIndex = 257;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(-2, -1);
            panel8.Name = "panel8";
            panel8.Size = new Size(25, 401);
            panel8.TabIndex = 251;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(59, 185);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(105, 27);
            Nombre.TabIndex = 264;
            Nombre.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(45, 229);
            label1.Name = "label1";
            label1.Size = new Size(120, 27);
            label1.TabIndex = 264;
            label1.Text = "Cantidad:";
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(528, 149);
            BtnAceptar.Name = "BtnAceptar";
            BtnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideDefault.Back.Color2 = Color.White;
            BtnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideFocus.Back.Color2 = Color.White;
            BtnAceptar.Size = new Size(123, 60);
            BtnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateCommon.Back.Color2 = Color.White;
            BtnAceptar.StateCommon.Border.Rounding = 5F;
            BtnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAceptar.TabIndex = 319;
            BtnAceptar.Values.DropDownArrowColor = Color.Empty;
            BtnAceptar.Values.Text = "Aceptar";
            BtnAceptar.Click += BtnAceptar_Click;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(528, 229);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(122, 60);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 5F;
            BtnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnSalir.StatePressed.Back.Color1 = Color.Transparent;
            BtnSalir.StatePressed.Back.Color2 = Color.Transparent;
            BtnSalir.TabIndex = 319;
            BtnSalir.Values.DropDownArrowColor = Color.Empty;
            BtnSalir.Values.Text = "Cancelar";
            BtnSalir.Click += BtnSalir_Click;
            // 
            // cmbProductos
            // 
            cmbProductos.DropDownWidth = 300;
            cmbProductos.Location = new Point(171, 179);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(298, 30);
            cmbProductos.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbProductos.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbProductos.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbProductos.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProductos.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbProductos.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProductos.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbProductos.TabIndex = 334;
            cmbProductos.SelectedIndexChanged += cmbProductos_SelectedIndexChanged;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(171, 229);
            txtCantidad.Margin = new Padding(3, 4, 3, 4);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(298, 34);
            txtCantidad.StateCommon.Back.Color1 = Color.White;
            txtCantidad.StateCommon.Border.Color1 = Color.Navy;
            txtCantidad.StateCommon.Border.Rounding = 5F;
            txtCantidad.StateCommon.Content.Color1 = Color.Gray;
            txtCantidad.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidad.TabIndex = 335;
            // 
            // lblStock
            // 
            lblStock.AutoSize = true;
            lblStock.BackColor = Color.Transparent;
            lblStock.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            lblStock.ForeColor = Color.Navy;
            lblStock.Location = new Point(495, 93);
            lblStock.Name = "lblStock";
            lblStock.Size = new Size(82, 27);
            lblStock.TabIndex = 338;
            lblStock.Text = "Stock:";
            // 
            // lblNumero
            // 
            lblNumero.AutoSize = true;
            lblNumero.BackColor = Color.Transparent;
            lblNumero.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNumero.ForeColor = Color.Navy;
            lblNumero.Location = new Point(592, 93);
            lblNumero.Name = "lblNumero";
            lblNumero.Size = new Size(25, 27);
            lblNumero.TabIndex = 340;
            lblNumero.Text = "0";
            // 
            // btnEscanear
            // 
            btnEscanear.Location = new Point(393, 127);
            btnEscanear.Name = "btnEscanear";
            btnEscanear.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEscanear.OverrideDefault.Back.Color2 = Color.White;
            btnEscanear.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEscanear.OverrideFocus.Back.Color2 = Color.White;
            btnEscanear.Size = new Size(90, 37);
            btnEscanear.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEscanear.StateCommon.Back.Color2 = Color.White;
            btnEscanear.StateCommon.Border.Rounding = 5F;
            btnEscanear.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEscanear.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEscanear.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEscanear.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnEscanear.StatePressed.Back.Color1 = Color.Transparent;
            btnEscanear.StatePressed.Back.Color2 = Color.Transparent;
            btnEscanear.TabIndex = 345;
            btnEscanear.Values.DropDownArrowColor = Color.Empty;
            btnEscanear.Values.Text = "Cambiar";
            btnEscanear.Click += btnEscanear_Click;
            // 
            // txtCodigo
            // 
            txtCodigo.Enabled = false;
            txtCodigo.Location = new Point(171, 131);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(216, 31);
            txtCodigo.StateCommon.Back.Color1 = Color.White;
            txtCodigo.StateCommon.Border.Color1 = Color.Navy;
            txtCodigo.StateCommon.Border.Rounding = 5F;
            txtCodigo.TabIndex = 344;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(59, 137);
            label2.Name = "label2";
            label2.Size = new Size(99, 27);
            label2.TabIndex = 346;
            label2.Text = "Codigo:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(22, 25);
            label5.Name = "label5";
            label5.Size = new Size(102, 35);
            label5.TabIndex = 347;
            label5.Text = "BAMS";
            // 
            // FacturaProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(698, 331);
            Controls.Add(label5);
            Controls.Add(label2);
            Controls.Add(btnEscanear);
            Controls.Add(txtCodigo);
            Controls.Add(lblNumero);
            Controls.Add(lblStock);
            Controls.Add(txtCantidad);
            Controls.Add(cmbProductos);
            Controls.Add(BtnSalir);
            Controls.Add(BtnAceptar);
            Controls.Add(label1);
            Controls.Add(Nombre);
            Controls.Add(label4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel8);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FacturaProducto";
            ShowIcon = false;
            Load += FacturaProducto_Load;
            ((System.ComponentModel.ISupportInitialize)cmbProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label4;
        private Panel panel3;
        private Panel panel1;
        private Panel panel4;
        private Panel panel8;
        private Label Nombre;
        private Label label1;
        private Krypton.Toolkit.KryptonButton BtnAceptar;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Krypton.Toolkit.KryptonComboBox cmbProductos;
        private Krypton.Toolkit.KryptonTextBox txtCantidad;
        private Label lblStock;
        private Label lblNumero;
        private Krypton.Toolkit.KryptonButton btnEscanear;
        private Krypton.Toolkit.KryptonTextBox txtCodigo;
        private Label label2;
        private Label label5;
    }
}