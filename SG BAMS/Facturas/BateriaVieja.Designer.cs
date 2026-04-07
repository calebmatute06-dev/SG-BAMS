namespace SG_BAMS.Facturas
{
    partial class BateriaVieja
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
            txtPrecio = new Krypton.Toolkit.KryptonTextBox();
            cmbBaterias = new Krypton.Toolkit.KryptonComboBox();
            Nombre = new Label();
            label1 = new Label();
            label2 = new Label();
            dgvBateria = new DataGridView();
            BtnAceptar = new Krypton.Toolkit.KryptonButton();
            BtnAgregar = new Krypton.Toolkit.KryptonButton();
            label3 = new Label();
            txtCantidad = new Krypton.Toolkit.KryptonTextBox();
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            label4 = new Label();
            label5 = new Label();
            txtTotal = new Krypton.Toolkit.KryptonTextBox();
            txtCantidadTotal = new Krypton.Toolkit.KryptonTextBox();
            BtnEliminar = new Krypton.Toolkit.KryptonButton();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).BeginInit();
            SuspendLayout();
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(220, 138);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(172, 32);
            txtPrecio.StateCommon.Back.Color1 = Color.SkyBlue;
            txtPrecio.StateCommon.Border.Rounding = 10F;
            txtPrecio.StateCommon.Content.Color1 = Color.Navy;
            txtPrecio.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecio.TabIndex = 336;
            txtPrecio.KeyPress += txtPrecio_KeyPress;
            // 
            // cmbBaterias
            // 
            cmbBaterias.DropDownWidth = 300;
            cmbBaterias.Location = new Point(220, 92);
            cmbBaterias.Margin = new Padding(3, 2, 3, 2);
            cmbBaterias.Name = "cmbBaterias";
            cmbBaterias.Size = new Size(172, 34);
            cmbBaterias.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbBaterias.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbBaterias.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbBaterias.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbBaterias.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBaterias.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbBaterias.TabIndex = 337;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(114, 100);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(90, 22);
            Nombre.TabIndex = 338;
            Nombre.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial Narrow", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(343, 38);
            label1.Name = "label1";
            label1.Size = new Size(145, 31);
            label1.TabIndex = 339;
            label1.Text = "Batería Víeja";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(114, 145);
            label2.Name = "label2";
            label2.Size = new Size(77, 22);
            label2.TabIndex = 340;
            label2.Text = "Precio:";
            // 
            // dgvBateria
            // 
            dgvBateria.BackgroundColor = Color.SkyBlue;
            dgvBateria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBateria.Location = new Point(56, 222);
            dgvBateria.Margin = new Padding(3, 2, 3, 2);
            dgvBateria.Name = "dgvBateria";
            dgvBateria.RowHeadersWidth = 51;
            dgvBateria.Size = new Size(480, 184);
            dgvBateria.TabIndex = 341;
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(279, 422);
            BtnAceptar.Margin = new Padding(3, 2, 3, 2);
            BtnAceptar.Name = "BtnAceptar";
            BtnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideDefault.Back.Color2 = Color.White;
            BtnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideFocus.Back.Color2 = Color.White;
            BtnAceptar.Size = new Size(108, 45);
            BtnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateCommon.Back.Color2 = Color.White;
            BtnAceptar.StateCommon.Border.Rounding = 30F;
            BtnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAceptar.TabIndex = 342;
            BtnAceptar.Values.DropDownArrowColor = Color.Empty;
            BtnAceptar.Values.Text = "Aceptar";
            BtnAceptar.Click += BtnAceptar_Click;
            // 
            // BtnAgregar
            // 
            BtnAgregar.Location = new Point(484, 133);
            BtnAgregar.Margin = new Padding(3, 2, 3, 2);
            BtnAgregar.Name = "BtnAgregar";
            BtnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideDefault.Back.Color2 = Color.White;
            BtnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideFocus.Back.Color2 = Color.White;
            BtnAgregar.Size = new Size(121, 34);
            BtnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAgregar.StateCommon.Back.Color2 = Color.White;
            BtnAgregar.StateCommon.Border.Rounding = 30F;
            BtnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAgregar.StateNormal.Content.ShortText.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAgregar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAgregar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAgregar.TabIndex = 343;
            BtnAgregar.Values.DropDownArrowColor = Color.Empty;
            BtnAgregar.Values.Text = "Agregar";
            BtnAgregar.Click += Agregar_Click;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(114, 185);
            label3.Name = "label3";
            label3.Size = new Size(100, 22);
            label3.TabIndex = 346;
            label3.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(220, 178);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(172, 32);
            txtCantidad.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCantidad.StateCommon.Border.Rounding = 10F;
            txtCantidad.StateCommon.Content.Color1 = Color.Navy;
            txtCantidad.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidad.TabIndex = 345;
            txtCantidad.KeyPress += txtCantidad_KeyPress;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(429, 422);
            BtnSalir.Margin = new Padding(3, 2, 3, 2);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(108, 45);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 30F;
            BtnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnSalir.StatePressed.Back.Color1 = Color.Transparent;
            BtnSalir.StatePressed.Back.Color2 = Color.Transparent;
            BtnSalir.TabIndex = 347;
            BtnSalir.Values.DropDownArrowColor = Color.Empty;
            BtnSalir.Values.Text = "Salir";
            BtnSalir.Click += BtnSalir_Click;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(542, 263);
            label4.Name = "label4";
            label4.Size = new Size(63, 22);
            label4.TabIndex = 348;
            label4.Text = "Total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(540, 304);
            label5.Name = "label5";
            label5.Size = new Size(151, 22);
            label5.TabIndex = 349;
            label5.Text = "Cantidad Total:";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(612, 256);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(104, 33);
            txtTotal.StateCommon.Back.Color1 = Color.SkyBlue;
            txtTotal.StateCommon.Border.Rounding = 10F;
            txtTotal.StateCommon.Content.Color1 = Color.Navy;
            txtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotal.StateNormal.Content.Color1 = Color.Navy;
            txtTotal.TabIndex = 350;
            txtTotal.Text = "0";
            // 
            // txtCantidadTotal
            // 
            txtCantidadTotal.Location = new Point(696, 297);
            txtCantidadTotal.Name = "txtCantidadTotal";
            txtCantidadTotal.Size = new Size(69, 33);
            txtCantidadTotal.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCantidadTotal.StateCommon.Border.Rounding = 10F;
            txtCantidadTotal.StateCommon.Content.Color1 = Color.Navy;
            txtCantidadTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidadTotal.StateNormal.Content.Color1 = Color.Navy;
            txtCantidadTotal.TabIndex = 351;
            txtCantidadTotal.Text = "0";
            // 
            // BtnEliminar
            // 
            BtnEliminar.Location = new Point(484, 176);
            BtnEliminar.Margin = new Padding(3, 2, 3, 2);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideDefault.Back.Color2 = Color.White;
            BtnEliminar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideFocus.Back.Color2 = Color.White;
            BtnEliminar.Size = new Size(121, 34);
            BtnEliminar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnEliminar.StateCommon.Back.Color2 = Color.White;
            BtnEliminar.StateCommon.Border.Rounding = 30F;
            BtnEliminar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnEliminar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnEliminar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnEliminar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnEliminar.StateNormal.Content.ShortText.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnEliminar.StatePressed.Back.Color1 = Color.Transparent;
            BtnEliminar.StatePressed.Back.Color2 = Color.Transparent;
            BtnEliminar.TabIndex = 352;
            BtnEliminar.Values.DropDownArrowColor = Color.Empty;
            BtnEliminar.Values.Text = "Eliminar";
            BtnEliminar.Click += BtnEliminar_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(1, -8);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(801, 26);
            panel1.TabIndex = 353;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(1, 471);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(801, 17);
            panel2.TabIndex = 354;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(780, -6);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(22, 492);
            panel3.TabIndex = 355;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(-1, -6);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(23, 494);
            panel4.TabIndex = 356;
            // 
            // kryptonGroupBox3
            // 
            kryptonGroupBox3.CaptionVisible = false;
            kryptonGroupBox3.Location = new Point(298, 29);
            kryptonGroupBox3.Size = new Size(233, 47);
            kryptonGroupBox3.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox3.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox3.TabIndex = 358;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(695, 439);
            label6.Name = "label6";
            label6.Size = new Size(84, 29);
            label6.TabIndex = 359;
            label6.Text = "BAMS";
            // 
            // BateriaVieja
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(798, 488);
            Controls.Add(label6);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(BtnEliminar);
            Controls.Add(txtCantidadTotal);
            Controls.Add(txtTotal);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(BtnSalir);
            Controls.Add(label3);
            Controls.Add(txtCantidad);
            Controls.Add(BtnAgregar);
            Controls.Add(BtnAceptar);
            Controls.Add(dgvBateria);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Nombre);
            Controls.Add(cmbBaterias);
            Controls.Add(txtPrecio);
            Controls.Add(kryptonGroupBox3);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "BateriaVieja";
            Text = "BateriaVieja";
            Load += BateriaVieja_Load;
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Krypton.Toolkit.KryptonTextBox txtPrecio;
        private Krypton.Toolkit.KryptonComboBox cmbBaterias;
        private Label Nombre;
        private Label label1;
        private Label label2;
        private DataGridView dgvBateria;
        private Krypton.Toolkit.KryptonButton BtnAceptar;
        private Krypton.Toolkit.KryptonButton BtnAgregar;
        private Label label3;
        private Krypton.Toolkit.KryptonTextBox txtCantidad;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Label label4;
        private Label label5;
        private Krypton.Toolkit.KryptonTextBox txtTotal;
        private Krypton.Toolkit.KryptonTextBox txtCantidadTotal;
        private Krypton.Toolkit.KryptonButton BtnEliminar;
        private Panel panel1;
        private Panel panel2;
        private Panel panel3;
        private Panel panel4;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Label label6;
    }
}