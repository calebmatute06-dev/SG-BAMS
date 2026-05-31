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
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).BeginInit();
            SuspendLayout();
            // 
            // txtPrecio
            // 
            txtPrecio.CueHint.Color1 = Color.Gray;
            txtPrecio.CueHint.CueHintText = "Ingrese precio";
            txtPrecio.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecio.Location = new Point(251, 184);
            txtPrecio.Margin = new Padding(3, 4, 3, 4);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(197, 34);
            txtPrecio.StateCommon.Back.Color1 = Color.White;
            txtPrecio.StateCommon.Border.Color1 = Color.Navy;
            txtPrecio.StateCommon.Border.Rounding = 5F;
            txtPrecio.StateCommon.Content.Color1 = Color.Gray;
            txtPrecio.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecio.TabIndex = 336;
            txtPrecio.KeyPress += txtPrecio_KeyPress;
            // 
            // cmbBaterias
            // 
            cmbBaterias.CueHint.Color1 = Color.Gray;
            cmbBaterias.CueHint.CueHintText = "Ingrese el nombre";
            cmbBaterias.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBaterias.DropDownWidth = 300;
            cmbBaterias.Location = new Point(251, 123);
            cmbBaterias.Name = "cmbBaterias";
            cmbBaterias.Size = new Size(197, 38);
            cmbBaterias.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbBaterias.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbBaterias.StateCommon.ComboBox.Border.Rounding = 5F;
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
            Nombre.Location = new Point(130, 133);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(105, 27);
            Nombre.TabIndex = 338;
            Nombre.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 30F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(338, 41);
            label1.Name = "label1";
            label1.Size = new Size(271, 58);
            label1.TabIndex = 339;
            label1.Text = "Batería Víeja";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(130, 193);
            label2.Name = "label2";
            label2.Size = new Size(90, 27);
            label2.TabIndex = 340;
            label2.Text = "Precio:";
            // 
            // dgvBateria
            // 
            dgvBateria.BackgroundColor = Color.SkyBlue;
            dgvBateria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBateria.Location = new Point(64, 296);
            dgvBateria.Name = "dgvBateria";
            dgvBateria.RowHeadersWidth = 51;
            dgvBateria.Size = new Size(549, 245);
            dgvBateria.TabIndex = 341;
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(319, 563);
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
            BtnAceptar.TabIndex = 342;
            BtnAceptar.Values.DropDownArrowColor = Color.Empty;
            BtnAceptar.Values.Text = "Aceptar";
            BtnAceptar.Click += BtnAceptar_Click;
            // 
            // BtnAgregar
            // 
            BtnAgregar.Location = new Point(553, 177);
            BtnAgregar.Name = "BtnAgregar";
            BtnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideDefault.Back.Color2 = Color.White;
            BtnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideFocus.Back.Color2 = Color.White;
            BtnAgregar.Size = new Size(138, 45);
            BtnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAgregar.StateCommon.Back.Color2 = Color.White;
            BtnAgregar.StateCommon.Border.Rounding = 5F;
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
            label3.Location = new Point(130, 247);
            label3.Name = "label3";
            label3.Size = new Size(120, 27);
            label3.TabIndex = 346;
            label3.Text = "Cantidad:";
            // 
            // txtCantidad
            // 
            txtCantidad.CueHint.Color1 = Color.Gray;
            txtCantidad.CueHint.CueHintText = "Ingrese la cantidad";
            txtCantidad.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidad.Location = new Point(251, 237);
            txtCantidad.Margin = new Padding(3, 4, 3, 4);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(197, 34);
            txtCantidad.StateCommon.Back.Color1 = Color.White;
            txtCantidad.StateCommon.Border.Color1 = Color.Navy;
            txtCantidad.StateCommon.Border.Rounding = 5F;
            txtCantidad.StateCommon.Content.Color1 = Color.Gray;
            txtCantidad.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidad.TabIndex = 345;
            txtCantidad.KeyPress += txtCantidad_KeyPress;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(490, 563);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(123, 60);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 5F;
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
            label4.Location = new Point(621, 352);
            label4.Name = "label4";
            label4.Size = new Size(74, 27);
            label4.TabIndex = 348;
            label4.Text = "Total:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(618, 405);
            label5.Name = "label5";
            label5.Size = new Size(181, 27);
            label5.TabIndex = 349;
            label5.Text = "Cantidad Total:";
            // 
            // txtTotal
            // 
            txtTotal.Location = new Point(699, 341);
            txtTotal.Margin = new Padding(3, 4, 3, 4);
            txtTotal.Name = "txtTotal";
            txtTotal.Size = new Size(119, 35);
            txtTotal.StateCommon.Back.Color1 = Color.White;
            txtTotal.StateCommon.Border.Color1 = Color.Navy;
            txtTotal.StateCommon.Border.Rounding = 5F;
            txtTotal.StateCommon.Content.Color1 = Color.Navy;
            txtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtTotal.StateNormal.Content.Color1 = Color.Navy;
            txtTotal.TabIndex = 350;
            txtTotal.Text = "0";
            // 
            // txtCantidadTotal
            // 
            txtCantidadTotal.Location = new Point(795, 398);
            txtCantidadTotal.Margin = new Padding(3, 4, 3, 4);
            txtCantidadTotal.Name = "txtCantidadTotal";
            txtCantidadTotal.Size = new Size(79, 35);
            txtCantidadTotal.StateCommon.Back.Color1 = Color.White;
            txtCantidadTotal.StateCommon.Border.Color1 = Color.Navy;
            txtCantidadTotal.StateCommon.Border.Rounding = 5F;
            txtCantidadTotal.StateCommon.Content.Color1 = Color.Navy;
            txtCantidadTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtCantidadTotal.StateNormal.Content.Color1 = Color.Navy;
            txtCantidadTotal.TabIndex = 351;
            txtCantidadTotal.Text = "0";
            // 
            // BtnEliminar
            // 
            BtnEliminar.Location = new Point(553, 235);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideDefault.Back.Color2 = Color.White;
            BtnEliminar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideFocus.Back.Color2 = Color.White;
            BtnEliminar.Size = new Size(138, 45);
            BtnEliminar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnEliminar.StateCommon.Back.Color2 = Color.White;
            BtnEliminar.StateCommon.Border.Rounding = 5F;
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
            panel1.Location = new Point(1, -11);
            panel1.Name = "panel1";
            panel1.Size = new Size(915, 35);
            panel1.TabIndex = 353;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(1, 628);
            panel2.Name = "panel2";
            panel2.Size = new Size(915, 23);
            panel2.TabIndex = 354;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(891, -8);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 656);
            panel3.TabIndex = 355;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(-1, -8);
            panel4.Name = "panel4";
            panel4.Size = new Size(26, 659);
            panel4.TabIndex = 356;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(788, 585);
            label6.Name = "label6";
            label6.Size = new Size(102, 35);
            label6.TabIndex = 359;
            label6.Text = "BAMS";
            // 
            // BateriaVieja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(912, 651);
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
            FormBorderStyle = FormBorderStyle.None;
            Name = "BateriaVieja";
            Text = "BateriaVieja";
            Load += BateriaVieja_Load;
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).EndInit();
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
        private Label label6;
    }
}