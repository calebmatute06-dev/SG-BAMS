namespace SG_BAMS
{
    partial class FacturaAgregarDatos
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
            label7 = new Label();
            label9 = new Label();
            label1 = new Label();
            label3 = new Label();
            label5 = new Label();
            label4 = new Label();
            panel3 = new Panel();
            panel1 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            panel8 = new Panel();
            BtnAgregar = new Krypton.Toolkit.KryptonButton();
            BtnAceptar = new Krypton.Toolkit.KryptonButton();
            BtnCancelar = new Krypton.Toolkit.KryptonButton();
            DateTFecha = new MonthCalendar();
            label2 = new Label();
            kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            TxtCliente = new Krypton.Toolkit.KryptonTextBox();
            TxtBateria = new Krypton.Toolkit.KryptonTextBox();
            TxtTotal = new Krypton.Toolkit.KryptonTextBox();
            cmbPago = new Krypton.Toolkit.KryptonComboBox();
            dgvProductos = new DataGridView();
            panel5 = new Panel();
            BtnEliminar = new Krypton.Toolkit.KryptonButton();
            btnBateria = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbPago).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(882, 181);
            label7.Name = "label7";
            label7.Size = new Size(80, 27);
            label7.TabIndex = 307;
            label7.Text = "Fecha";
            label7.Click += label7_Click;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(57, 582);
            label9.Name = "label9";
            label9.Size = new Size(176, 27);
            label9.TabIndex = 304;
            label9.Text = "Forma de Pago";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(50, 490);
            label1.Name = "label1";
            label1.Size = new Size(152, 27);
            label1.TabIndex = 303;
            label1.Text = "Batería Vieja";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(58, 131);
            label3.Name = "label3";
            label3.Size = new Size(92, 27);
            label3.TabIndex = 302;
            label3.Text = "Cliente";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(988, 689);
            label5.Name = "label5";
            label5.Size = new Size(113, 42);
            label5.TabIndex = 299;
            label5.Text = "BAMS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.SkyBlue;
            label4.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(61, 43);
            label4.Name = "label4";
            label4.Size = new Size(223, 35);
            label4.TabIndex = 296;
            label4.Text = "Nueva Factura";
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(0, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 755);
            panel3.TabIndex = 294;
            panel3.Paint += panel3_Paint;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 24);
            panel1.TabIndex = 293;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(3, 734);
            panel4.Name = "panel4";
            panel4.Size = new Size(1134, 24);
            panel4.TabIndex = 295;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(642, 24);
            panel2.TabIndex = 236;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(0, -4);
            panel8.Name = "panel8";
            panel8.Size = new Size(25, 600);
            panel8.TabIndex = 289;
            // 
            // BtnAgregar
            // 
            BtnAgregar.Location = new Point(67, 649);
            BtnAgregar.Name = "BtnAgregar";
            BtnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideDefault.Back.Color2 = Color.White;
            BtnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAgregar.OverrideFocus.Back.Color2 = Color.White;
            BtnAgregar.Size = new Size(272, 60);
            BtnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAgregar.StateCommon.Back.Color2 = Color.White;
            BtnAgregar.StateCommon.Border.Rounding = 40F;
            BtnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            BtnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAgregar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAgregar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAgregar.TabIndex = 314;
            BtnAgregar.Values.DropDownArrowColor = Color.Empty;
            BtnAgregar.Values.Text = "Agregar Producto";
            BtnAgregar.Click += BtnAgregar_Click;
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(632, 649);
            BtnAceptar.Name = "BtnAceptar";
            BtnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideDefault.Back.Color2 = Color.White;
            BtnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideFocus.Back.Color2 = Color.White;
            BtnAceptar.Size = new Size(146, 60);
            BtnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateCommon.Back.Color2 = Color.White;
            BtnAceptar.StateCommon.Border.Rounding = 40F;
            BtnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            BtnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAceptar.TabIndex = 314;
            BtnAceptar.Values.DropDownArrowColor = Color.Empty;
            BtnAceptar.Values.Text = "Aceptar";
            BtnAceptar.Click += BtnAceptar_Click;
            // 
            // BtnCancelar
            // 
            BtnCancelar.Location = new Point(791, 649);
            BtnCancelar.Name = "BtnCancelar";
            BtnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnCancelar.OverrideDefault.Back.Color2 = Color.White;
            BtnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnCancelar.OverrideFocus.Back.Color2 = Color.White;
            BtnCancelar.Size = new Size(146, 60);
            BtnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnCancelar.StateCommon.Back.Color2 = Color.White;
            BtnCancelar.StateCommon.Border.Rounding = 40F;
            BtnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            BtnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnCancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnCancelar.StatePressed.Back.Color1 = Color.Transparent;
            BtnCancelar.StatePressed.Back.Color2 = Color.Transparent;
            BtnCancelar.TabIndex = 314;
            BtnCancelar.Values.DropDownArrowColor = Color.Empty;
            BtnCancelar.Values.Text = "Cancelar";
            BtnCancelar.Click += BtnCancelar_Click;
            // 
            // DateTFecha
            // 
            DateTFecha.Enabled = false;
            DateTFecha.Location = new Point(811, 220);
            DateTFecha.Margin = new Padding(10, 12, 10, 12);
            DateTFecha.Name = "DateTFecha";
            DateTFecha.TabIndex = 315;
            DateTFecha.DateChanged += DateTFecha_DateChanged;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(602, 491);
            label2.Name = "label2";
            label2.Size = new Size(67, 27);
            label2.TabIndex = 302;
            label2.Text = "Total";
            // 
            // kryptonGroupBox3
            // 
            kryptonGroupBox3.CaptionVisible = false;
            kryptonGroupBox3.Location = new Point(50, 31);
            kryptonGroupBox3.Margin = new Padding(3, 4, 3, 4);
            kryptonGroupBox3.Size = new Size(257, 65);
            kryptonGroupBox3.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox3.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox3.TabIndex = 317;
            // 
            // TxtCliente
            // 
            TxtCliente.Location = new Point(182, 123);
            TxtCliente.Margin = new Padding(3, 4, 3, 4);
            TxtCliente.Name = "TxtCliente";
            TxtCliente.Size = new Size(239, 36);
            TxtCliente.StateCommon.Back.Color1 = Color.SkyBlue;
            TxtCliente.StateCommon.Border.Rounding = 10F;
            TxtCliente.StateCommon.Content.Color1 = Color.Navy;
            TxtCliente.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            TxtCliente.TabIndex = 321;
            // 
            // TxtBateria
            // 
            TxtBateria.Location = new Point(216, 481);
            TxtBateria.Margin = new Padding(3, 4, 3, 4);
            TxtBateria.Name = "TxtBateria";
            TxtBateria.Size = new Size(112, 37);
            TxtBateria.StateCommon.Back.Color1 = Color.SkyBlue;
            TxtBateria.StateCommon.Border.Rounding = 10F;
            TxtBateria.StateCommon.Content.Color1 = Color.Navy;
            TxtBateria.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            TxtBateria.TabIndex = 321;
            TxtBateria.Text = "0";
            // 
            // TxtTotal
            // 
            TxtTotal.Location = new Point(678, 481);
            TxtTotal.Margin = new Padding(3, 4, 3, 4);
            TxtTotal.Name = "TxtTotal";
            TxtTotal.Size = new Size(110, 37);
            TxtTotal.StateCommon.Back.Color1 = Color.SkyBlue;
            TxtTotal.StateCommon.Border.Rounding = 10F;
            TxtTotal.StateCommon.Content.Color1 = Color.Navy;
            TxtTotal.StateCommon.Content.Font = new Font("Arial Narrow", 12F);
            TxtTotal.TabIndex = 321;
            TxtTotal.Text = "0";
            // 
            // cmbPago
            // 
            cmbPago.DropDownStyle = ComboBoxStyle.DropDownList;
            cmbPago.DropDownWidth = 300;
            cmbPago.Location = new Point(239, 571);
            cmbPago.Name = "cmbPago";
            cmbPago.Size = new Size(146, 38);
            cmbPago.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbPago.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbPago.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbPago.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbPago.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbPago.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbPago.TabIndex = 322;
            // 
            // dgvProductos
            // 
            dgvProductos.BackgroundColor = Color.SkyBlue;
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProductos.Location = new Point(57, 187);
            dgvProductos.Name = "dgvProductos";
            dgvProductos.RowHeadersWidth = 51;
            dgvProductos.Size = new Size(678, 267);
            dgvProductos.TabIndex = 323;
            dgvProductos.CellContentClick += dgvProductos_CellContentClick;
            dgvProductos.CellValidating += dgvProductos_CellValidating;
            dgvProductos.CellValueChanged += dgvProductos_CellValueChanged;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(1107, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(30, 758);
            panel5.TabIndex = 295;
            // 
            // BtnEliminar
            // 
            BtnEliminar.Location = new Point(354, 649);
            BtnEliminar.Name = "BtnEliminar";
            BtnEliminar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideDefault.Back.Color2 = Color.White;
            BtnEliminar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnEliminar.OverrideFocus.Back.Color2 = Color.White;
            BtnEliminar.Size = new Size(272, 60);
            BtnEliminar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnEliminar.StateCommon.Back.Color2 = Color.White;
            BtnEliminar.StateCommon.Border.Rounding = 40F;
            BtnEliminar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnEliminar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            BtnEliminar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnEliminar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnEliminar.StatePressed.Back.Color1 = Color.Transparent;
            BtnEliminar.StatePressed.Back.Color2 = Color.Transparent;
            BtnEliminar.TabIndex = 324;
            BtnEliminar.Values.DropDownArrowColor = Color.Empty;
            BtnEliminar.Values.Text = "Eliminar Producto";
            BtnEliminar.Click += BtnEliminar_Click;
            // 
            // btnBateria
            // 
            btnBateria.Location = new Point(343, 481);
            btnBateria.Name = "btnBateria";
            btnBateria.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBateria.OverrideDefault.Back.Color2 = Color.White;
            btnBateria.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBateria.OverrideFocus.Back.Color2 = Color.White;
            btnBateria.Size = new Size(145, 39);
            btnBateria.StateCommon.Back.Color1 = Color.SkyBlue;
            btnBateria.StateCommon.Back.Color2 = Color.White;
            btnBateria.StateCommon.Border.Rounding = 40F;
            btnBateria.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnBateria.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnBateria.StateNormal.Back.Color1 = Color.SkyBlue;
            btnBateria.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnBateria.StatePressed.Back.Color1 = Color.Transparent;
            btnBateria.StatePressed.Back.Color2 = Color.Transparent;
            btnBateria.TabIndex = 325;
            btnBateria.Values.DropDownArrowColor = Color.Empty;
            btnBateria.Values.Text = "Calcular";
            btnBateria.Click += btnBateria_Click;
            // 
            // FacturaAgregarDatos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1136, 756);
            Controls.Add(btnBateria);
            Controls.Add(BtnEliminar);
            Controls.Add(panel4);
            Controls.Add(panel5);
            Controls.Add(dgvProductos);
            Controls.Add(cmbPago);
            Controls.Add(TxtTotal);
            Controls.Add(TxtBateria);
            Controls.Add(TxtCliente);
            Controls.Add(DateTFecha);
            Controls.Add(panel3);
            Controls.Add(BtnCancelar);
            Controls.Add(BtnAceptar);
            Controls.Add(BtnAgregar);
            Controls.Add(label7);
            Controls.Add(label9);
            Controls.Add(label1);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(panel1);
            Controls.Add(panel8);
            Controls.Add(kryptonGroupBox3);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FacturaAgregarDatos";
            Text = "FacturaAgregarDatos";
            Load += FacturaAgregarDatos_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbPago).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label7;
        private Label label9;
        private Label label1;
        private Label label3;
        private Label label5;
        private Label label4;
        private Panel panel3;
        private Panel panel1;
        private Panel panel4;
        private Panel panel2;
        private Panel panel8;
        private Krypton.Toolkit.KryptonButton BtnAgregar;
        private Krypton.Toolkit.KryptonButton BtnAceptar;
        private Krypton.Toolkit.KryptonButton BtnCancelar;
        private MonthCalendar DateTFecha;
        private Label label2;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Krypton.Toolkit.KryptonTextBox TxtCliente;
        private Krypton.Toolkit.KryptonTextBox TxtBateria;
        private Krypton.Toolkit.KryptonTextBox TxtTotal;
        private Krypton.Toolkit.KryptonComboBox cmbPago;
        private Panel panel5;
        private Krypton.Toolkit.KryptonButton BtnEliminar;
        public DataGridView dgvProductos;
        private Krypton.Toolkit.KryptonButton btnBateria;
    }
}