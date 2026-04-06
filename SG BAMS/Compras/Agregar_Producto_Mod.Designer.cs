namespace SG_BAMS
{
    partial class Agregar_Producto_Mod
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
            numCantidad = new Krypton.Toolkit.KryptonNumericUpDown();
            kryptonButton3 = new Krypton.Toolkit.KryptonButton();
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            btnProductoNuevo = new Krypton.Toolkit.KryptonButton();
            kryptonLabel2 = new Krypton.Toolkit.KryptonLabel();
            kryptonLabel1 = new Krypton.Toolkit.KryptonLabel();
            label1 = new Label();
            kryptonLabel6 = new Krypton.Toolkit.KryptonLabel();
            kryptonGroup1 = new Krypton.Toolkit.KryptonGroup();
            panel1 = new Panel();
            panel3 = new Panel();
            pictureBox1 = new PictureBox();
            pictureBox4 = new PictureBox();
            kryptonButton13 = new Krypton.Toolkit.KryptonButton();
            cmbProductos = new Krypton.Toolkit.KryptonComboBox();
            kryptonLabel3 = new Krypton.Toolkit.KryptonLabel();
            txtCodigo = new Krypton.Toolkit.KryptonTextBox();
            btnEscanear = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbProductos).BeginInit();
            SuspendLayout();
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(192, 277);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(166, 33);
            txtPrecio.StateCommon.Back.Color1 = Color.SkyBlue;
            txtPrecio.StateCommon.Border.Rounding = 10F;
            txtPrecio.TabIndex = 228;
            txtPrecio.KeyPress += txtPrecio_KeyPress;
            txtPrecio.Leave += txtPrecio_Leave;
            // 
            // numCantidad
            // 
            numCantidad.Increment = new decimal(new int[] { 1, 0, 0, 0 });
            numCantidad.Location = new Point(175, 227);
            numCantidad.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            numCantidad.Minimum = new decimal(new int[] { 0, 0, 0, 0 });
            numCantidad.Name = "numCantidad";
            numCantidad.Size = new Size(150, 32);
            numCantidad.StateCommon.Back.Color1 = Color.SkyBlue;
            numCantidad.StateCommon.Border.Rounding = 10F;
            numCantidad.StateCommon.Content.Color1 = Color.Navy;
            numCantidad.TabIndex = 227;
            numCantidad.Value = new decimal(new int[] { 0, 0, 0, 0 });
            numCantidad.KeyPress += numCantidad_KeyPress;
            // 
            // kryptonButton3
            // 
            kryptonButton3.Location = new Point(30, 348);
            kryptonButton3.Name = "kryptonButton3";
            kryptonButton3.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton3.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton3.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton3.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton3.Size = new Size(121, 39);
            kryptonButton3.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton3.StateCommon.Back.Color2 = Color.White;
            kryptonButton3.StateCommon.Border.Rounding = 20F;
            kryptonButton3.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton3.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            kryptonButton3.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton3.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton3.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton3.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton3.TabIndex = 224;
            kryptonButton3.Values.DropDownArrowColor = Color.Empty;
            kryptonButton3.Values.Text = "Aceptar";
            kryptonButton3.Click += kryptonButton3_Click;
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(157, 348);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(121, 39);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 20F;
            btnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancelar.StatePressed.Back.Color1 = Color.Transparent;
            btnCancelar.StatePressed.Back.Color2 = Color.Transparent;
            btnCancelar.TabIndex = 223;
            btnCancelar.Values.DropDownArrowColor = Color.Empty;
            btnCancelar.Values.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click_1;
            // 
            // btnProductoNuevo
            // 
            btnProductoNuevo.Location = new Point(337, 348);
            btnProductoNuevo.Name = "btnProductoNuevo";
            btnProductoNuevo.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnProductoNuevo.OverrideDefault.Back.Color2 = Color.White;
            btnProductoNuevo.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnProductoNuevo.OverrideFocus.Back.Color2 = Color.White;
            btnProductoNuevo.Size = new Size(184, 39);
            btnProductoNuevo.StateCommon.Back.Color1 = Color.SkyBlue;
            btnProductoNuevo.StateCommon.Back.Color2 = Color.White;
            btnProductoNuevo.StateCommon.Border.Rounding = 20F;
            btnProductoNuevo.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnProductoNuevo.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnProductoNuevo.StateNormal.Back.Color1 = Color.SkyBlue;
            btnProductoNuevo.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnProductoNuevo.StatePressed.Back.Color1 = Color.Transparent;
            btnProductoNuevo.StatePressed.Back.Color2 = Color.Transparent;
            btnProductoNuevo.TabIndex = 222;
            btnProductoNuevo.Values.DropDownArrowColor = Color.Empty;
            btnProductoNuevo.Values.Text = "Nuevo Producto";
            btnProductoNuevo.Click += btnProductoNuevo_Click;
            // 
            // kryptonLabel2
            // 
            kryptonLabel2.Location = new Point(53, 279);
            kryptonLabel2.Name = "kryptonLabel2";
            kryptonLabel2.Size = new Size(147, 31);
            kryptonLabel2.StateCommon.ShortText.Color1 = Color.Navy;
            kryptonLabel2.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            kryptonLabel2.TabIndex = 221;
            kryptonLabel2.Values.Text = "Precio Costo:";
            // 
            // kryptonLabel1
            // 
            kryptonLabel1.Location = new Point(53, 227);
            kryptonLabel1.Name = "kryptonLabel1";
            kryptonLabel1.Size = new Size(108, 31);
            kryptonLabel1.StateCommon.ShortText.Color1 = Color.Navy;
            kryptonLabel1.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            kryptonLabel1.TabIndex = 220;
            kryptonLabel1.Values.Text = "Cantidad:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.SkyBlue;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(41, 42);
            label1.Name = "label1";
            label1.Size = new Size(178, 24);
            label1.TabIndex = 219;
            label1.Text = "Agregar Producto";
            // 
            // kryptonLabel6
            // 
            kryptonLabel6.Location = new Point(53, 174);
            kryptonLabel6.Name = "kryptonLabel6";
            kryptonLabel6.Size = new Size(98, 31);
            kryptonLabel6.StateCommon.ShortText.Color1 = Color.Navy;
            kryptonLabel6.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            kryptonLabel6.TabIndex = 218;
            kryptonLabel6.Values.Text = "Nombre:";
            // 
            // kryptonGroup1
            // 
            kryptonGroup1.Location = new Point(27, 26);
            kryptonGroup1.Size = new Size(199, 59);
            kryptonGroup1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup1.StateCommon.Border.Rounding = 70F;
            kryptonGroup1.TabIndex = 217;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(551, 24);
            panel1.TabIndex = 216;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(10, 393);
            panel3.Name = "panel3";
            panel3.Size = new Size(551, 24);
            panel3.TabIndex = 215;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 417);
            pictureBox1.TabIndex = 214;
            pictureBox1.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(527, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 404);
            pictureBox4.TabIndex = 213;
            pictureBox4.TabStop = false;
            // 
            // kryptonButton13
            // 
            kryptonButton13.Location = new Point(418, 26);
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
            kryptonButton13.TabIndex = 225;
            kryptonButton13.Values.DropDownArrowColor = Color.Empty;
            kryptonButton13.Values.Text = "BAMS";
            // 
            // cmbProductos
            // 
            cmbProductos.DropDownWidth = 300;
            cmbProductos.Location = new Point(157, 169);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(364, 38);
            cmbProductos.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbProductos.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbProductos.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProductos.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbProductos.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProductos.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbProductos.TabIndex = 336;
            // 
            // kryptonLabel3
            // 
            kryptonLabel3.Location = new Point(39, 126);
            kryptonLabel3.Name = "kryptonLabel3";
            kryptonLabel3.Size = new Size(91, 31);
            kryptonLabel3.StateCommon.ShortText.Color1 = Color.Navy;
            kryptonLabel3.StateCommon.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            kryptonLabel3.TabIndex = 337;
            kryptonLabel3.Values.Text = "Codigo:";
            // 
            // txtCodigo
            // 
            txtCodigo.Enabled = false;
            txtCodigo.Location = new Point(142, 124);
            txtCodigo.Name = "txtCodigo";
            txtCodigo.Size = new Size(216, 33);
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCodigo.StateCommon.Border.Rounding = 10F;
            txtCodigo.TabIndex = 338;
            // 
            // btnEscanear
            // 
            btnEscanear.Location = new Point(364, 120);
            btnEscanear.Name = "btnEscanear";
            btnEscanear.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEscanear.OverrideDefault.Back.Color2 = Color.White;
            btnEscanear.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEscanear.OverrideFocus.Back.Color2 = Color.White;
            btnEscanear.Size = new Size(90, 37);
            btnEscanear.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEscanear.StateCommon.Back.Color2 = Color.White;
            btnEscanear.StateCommon.Border.Rounding = 20F;
            btnEscanear.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEscanear.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnEscanear.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEscanear.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnEscanear.StatePressed.Back.Color1 = Color.Transparent;
            btnEscanear.StatePressed.Back.Color2 = Color.Transparent;
            btnEscanear.TabIndex = 339;
            btnEscanear.Values.DropDownArrowColor = Color.Empty;
            btnEscanear.Values.Text = "Cambiar";
            btnEscanear.Click += btnEscanear_Click;
            // 
            // Agregar_Producto_Mod
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(551, 416);
            Controls.Add(btnEscanear);
            Controls.Add(txtCodigo);
            Controls.Add(kryptonLabel3);
            Controls.Add(cmbProductos);
            Controls.Add(txtPrecio);
            Controls.Add(numCantidad);
            Controls.Add(kryptonButton3);
            Controls.Add(btnCancelar);
            Controls.Add(btnProductoNuevo);
            Controls.Add(kryptonLabel2);
            Controls.Add(kryptonLabel1);
            Controls.Add(label1);
            Controls.Add(kryptonLabel6);
            Controls.Add(kryptonGroup1);
            Controls.Add(panel1);
            Controls.Add(panel3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox4);
            Controls.Add(kryptonButton13);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Agregar_Producto_Mod";
            Text = "Agregar_Producto_Mod";
            Load += Agregar_Producto_Mod_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonTextBox txtPrecio;
        private Krypton.Toolkit.KryptonNumericUpDown numCantidad;
        private Krypton.Toolkit.KryptonButton kryptonButton3;
        private Krypton.Toolkit.KryptonButton btnCancelar;
        private Krypton.Toolkit.KryptonButton btnProductoNuevo;
        private Krypton.Toolkit.KryptonLabel kryptonLabel2;
        private Krypton.Toolkit.KryptonLabel kryptonLabel1;
        private Label label1;
        private Krypton.Toolkit.KryptonLabel kryptonLabel6;
        private Krypton.Toolkit.KryptonGroup kryptonGroup1;
        private Panel panel1;
        private Panel panel3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox4;
        private Krypton.Toolkit.KryptonButton kryptonButton13;
        private Krypton.Toolkit.KryptonComboBox cmbProductos;
        private Krypton.Toolkit.KryptonLabel kryptonLabel3;
        private Krypton.Toolkit.KryptonTextBox txtCodigo;
        private Krypton.Toolkit.KryptonButton btnEscanear;
    }
}