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
            label5 = new Label();
            label4 = new Label();
            panel3 = new Panel();
            panel1 = new Panel();
            panel2 = new Panel();
            panel4 = new Panel();
            panel8 = new Panel();
            Nombre = new Label();
            label1 = new Label();
            kryptonGroupBox3 = new Krypton.Toolkit.KryptonGroupBox();
            BtnAceptar = new Krypton.Toolkit.KryptonButton();
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            cmbProductos = new Krypton.Toolkit.KryptonComboBox();
            txtCantidad = new Krypton.Toolkit.KryptonTextBox();
            panel8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbProductos).BeginInit();
            SuspendLayout();
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(507, 277);
            label5.Name = "label5";
            label5.Size = new Size(113, 42);
            label5.TabIndex = 261;
            label5.Text = "BAMS";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.SkyBlue;
            label4.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(51, 47);
            label4.Name = "label4";
            label4.Size = new Size(269, 35);
            label4.TabIndex = 258;
            label4.Text = "Agregar Producto";
            label4.Click += label4_Click;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(622, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 339);
            panel3.TabIndex = 256;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(22, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(600, 24);
            panel1.TabIndex = 255;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(7, 316);
            panel2.Name = "panel2";
            panel2.Size = new Size(642, 24);
            panel2.TabIndex = 236;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(2, 324);
            panel4.Name = "panel4";
            panel4.Size = new Size(645, 24);
            panel4.TabIndex = 257;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Controls.Add(panel2);
            panel8.Location = new Point(-2, -1);
            panel8.Name = "panel8";
            panel8.Size = new Size(25, 349);
            panel8.TabIndex = 251;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(81, 127);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(84, 24);
            Nombre.TabIndex = 264;
            Nombre.Text = "Nombre";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(81, 173);
            label1.Name = "label1";
            label1.Size = new Size(101, 24);
            label1.TabIndex = 264;
            label1.Text = "Cantidad:";
            // 
            // kryptonGroupBox3
            // 
            kryptonGroupBox3.CaptionVisible = false;
            kryptonGroupBox3.Location = new Point(34, 31);
            kryptonGroupBox3.Margin = new Padding(3, 4, 3, 4);
            kryptonGroupBox3.Size = new Size(299, 65);
            kryptonGroupBox3.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox3.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox3.TabIndex = 318;
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(54, 249);
            BtnAceptar.Name = "BtnAceptar";
            BtnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideDefault.Back.Color2 = Color.White;
            BtnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideFocus.Back.Color2 = Color.White;
            BtnAceptar.Size = new Size(123, 60);
            BtnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateCommon.Back.Color2 = Color.White;
            BtnAceptar.StateCommon.Border.Rounding = 30F;
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
            BtnSalir.Location = new Point(200, 249);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(149, 60);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 30F;
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
            cmbProductos.Location = new Point(200, 123);
            cmbProductos.Name = "cmbProductos";
            cmbProductos.Size = new Size(197, 38);
            cmbProductos.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbProductos.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbProductos.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbProductos.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbProductos.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbProductos.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbProductos.TabIndex = 334;
            // 
            // txtCantidad
            // 
            txtCantidad.Location = new Point(200, 173);
            txtCantidad.Margin = new Padding(3, 4, 3, 4);
            txtCantidad.Name = "txtCantidad";
            txtCantidad.Size = new Size(197, 33);
            txtCantidad.StateCommon.Back.Color1 = Color.SkyBlue;
            txtCantidad.StateCommon.Border.Rounding = 10F;
            txtCantidad.TabIndex = 335;
            // 
            // FacturaProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(647, 352);
            Controls.Add(txtCantidad);
            Controls.Add(cmbProductos);
            Controls.Add(BtnSalir);
            Controls.Add(BtnAceptar);
            Controls.Add(label1);
            Controls.Add(Nombre);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(panel4);
            Controls.Add(panel8);
            Controls.Add(kryptonGroupBox3);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FacturaProducto";
            Text = "FacturaProducto";
            Load += FacturaProducto_Load;
            panel8.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbProductos).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label5;
        private Label label4;
        private Panel panel3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel4;
        private Panel panel8;
        private Label Nombre;
        private Label label1;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox3;
        private Krypton.Toolkit.KryptonButton BtnAceptar;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Krypton.Toolkit.KryptonComboBox cmbProductos;
        private Krypton.Toolkit.KryptonTextBox txtCantidad;
    }
}