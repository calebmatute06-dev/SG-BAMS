namespace SG_BAMS
{
    partial class AgregarProducto
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
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            kryptonButton20 = new Krypton.Toolkit.KryptonButton();
            txtStock = new Krypton.Toolkit.KryptonTextBox();
            label1 = new Label();
            txtPrecio = new Krypton.Toolkit.KryptonTextBox();
            txtNombre = new Krypton.Toolkit.KryptonTextBox();
            txtID = new Krypton.Toolkit.KryptonTextBox();
            label15 = new Label();
            label10 = new Label();
            label12 = new Label();
            label14 = new Label();
            label8 = new Label();
            label7 = new Label();
            label4 = new Label();
            label3 = new Label();
            btnsalir = new Krypton.Toolkit.KryptonButton();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            pictureBox4 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox9 = new PictureBox();
            cmbEstado = new Krypton.Toolkit.KryptonComboBox();
            cmbModelo = new Krypton.Toolkit.KryptonComboBox();
            cmbTipo = new Krypton.Toolkit.KryptonComboBox();
            cmbMarca = new Krypton.Toolkit.KryptonComboBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbModelo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbTipo).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbMarca).BeginInit();
            SuspendLayout();
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(271, 525);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideDefault.Border.Rounding = 40F;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(103, 44);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 40F;
            btnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateNormal.Back.Color2 = Color.White;
            btnCancelar.StateNormal.Border.Rounding = 40F;
            btnCancelar.StateTracking.Border.Rounding = 40F;
            btnCancelar.TabIndex = 160;
            btnCancelar.Values.DropDownArrowColor = Color.Empty;
            btnCancelar.Values.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // kryptonButton20
            // 
            kryptonButton20.Location = new Point(392, 525);
            kryptonButton20.Name = "kryptonButton20";
            kryptonButton20.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton20.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton20.OverrideDefault.Border.Rounding = 40F;
            kryptonButton20.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton20.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton20.Size = new Size(103, 44);
            kryptonButton20.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton20.StateCommon.Back.Color2 = Color.White;
            kryptonButton20.StateCommon.Border.Rounding = 40F;
            kryptonButton20.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton20.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kryptonButton20.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton20.StateNormal.Back.Color2 = Color.White;
            kryptonButton20.StateNormal.Border.Rounding = 40F;
            kryptonButton20.StateTracking.Border.Rounding = 40F;
            kryptonButton20.TabIndex = 159;
            kryptonButton20.Values.DropDownArrowColor = Color.Empty;
            kryptonButton20.Values.Text = "Aceptar";
            kryptonButton20.Click += kryptonButton20_Click;
            // 
            // txtStock
            // 
            txtStock.Location = new Point(207, 462);
            txtStock.Name = "txtStock";
            txtStock.Size = new Size(250, 39);
            txtStock.StateCommon.Back.Color1 = Color.SkyBlue;
            txtStock.StateCommon.Border.Rounding = 20F;
            txtStock.StateNormal.Content.Color1 = Color.Navy;
            txtStock.TabIndex = 158;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(72, 423);
            label1.Name = "label1";
            label1.Size = new Size(91, 33);
            label1.TabIndex = 153;
            label1.Text = "Estado:";
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(207, 180);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(250, 39);
            txtPrecio.StateCommon.Back.Color1 = Color.SkyBlue;
            txtPrecio.StateCommon.Border.Rounding = 20F;
            txtPrecio.StateNormal.Content.Color1 = Color.Navy;
            txtPrecio.TabIndex = 152;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(207, 127);
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 39);
            txtNombre.StateCommon.Back.Color1 = Color.SkyBlue;
            txtNombre.StateCommon.Border.Rounding = 20F;
            txtNombre.StateNormal.Content.Color1 = Color.Navy;
            txtNombre.TabIndex = 151;
            // 
            // txtID
            // 
            txtID.Location = new Point(207, 78);
            txtID.Name = "txtID";
            txtID.Size = new Size(250, 39);
            txtID.StateCommon.Back.Color1 = Color.SkyBlue;
            txtID.StateCommon.Border.Rounding = 20F;
            txtID.StateNormal.Content.Color1 = Color.Navy;
            txtID.TabIndex = 150;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Navy;
            label15.Location = new Point(72, 468);
            label15.Name = "label15";
            label15.Size = new Size(76, 33);
            label15.TabIndex = 149;
            label15.Text = "Stock:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(72, 359);
            label10.Name = "label10";
            label10.Size = new Size(149, 33);
            label10.TabIndex = 148;
            label10.Text = "Modelo Auto:";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.BackColor = Color.Transparent;
            label12.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.Navy;
            label12.Location = new Point(72, 298);
            label12.Name = "label12";
            label12.Size = new Size(71, 33);
            label12.TabIndex = 147;
            label12.Text = "Tipo: ";
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.BackColor = Color.Transparent;
            label14.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.Navy;
            label14.Location = new Point(72, 238);
            label14.Name = "label14";
            label14.Size = new Size(89, 33);
            label14.TabIndex = 146;
            label14.Text = "Marca: ";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(21, 24);
            label8.Name = "label8";
            label8.Size = new Size(269, 35);
            label8.TabIndex = 145;
            label8.Text = "Agregar Producto";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(72, 186);
            label7.Name = "label7";
            label7.Size = new Size(85, 33);
            label7.TabIndex = 144;
            label7.Text = "Precio:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(72, 133);
            label4.Name = "label4";
            label4.Size = new Size(111, 33);
            label4.TabIndex = 143;
            label4.Text = "Nombre: ";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(72, 84);
            label3.Name = "label3";
            label3.Size = new Size(50, 33);
            label3.TabIndex = 142;
            label3.Text = "ID: ";
            // 
            // btnsalir
            // 
            btnsalir.Location = new Point(21, 525);
            btnsalir.Name = "btnsalir";
            btnsalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideDefault.Back.Color2 = Color.White;
            btnsalir.OverrideDefault.Border.Rounding = 40F;
            btnsalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideFocus.Back.Color2 = Color.White;
            btnsalir.Size = new Size(76, 44);
            btnsalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalir.StateCommon.Back.Color2 = Color.White;
            btnsalir.StateCommon.Border.Rounding = 40F;
            btnsalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalir.StateNormal.Back.Color2 = Color.White;
            btnsalir.StateNormal.Border.Rounding = 40F;
            btnsalir.StateTracking.Border.Rounding = 40F;
            btnsalir.TabIndex = 141;
            btnsalir.Values.DropDownArrowColor = Color.Empty;
            btnsalir.Values.Text = "Salir";
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(501, -1);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(15, 578);
            pictureBox3.TabIndex = 140;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, -1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(515, 15);
            pictureBox2.TabIndex = 139;
            pictureBox2.TabStop = false;
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(0, -1);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(15, 578);
            pictureBox4.TabIndex = 162;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 575);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(515, 15);
            pictureBox1.TabIndex = 161;
            pictureBox1.TabStop = false;
            // 
            // pictureBox9
            // 
            pictureBox9.BackgroundImage = Properties.Resources.bateria_2;
            pictureBox9.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox9.Location = new Point(422, 14);
            pictureBox9.Name = "pictureBox9";
            pictureBox9.Size = new Size(80, 59);
            pictureBox9.TabIndex = 163;
            pictureBox9.TabStop = false;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownWidth = 300;
            cmbEstado.Location = new Point(207, 404);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(250, 52);
            cmbEstado.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbEstado.StateCommon.ComboBox.Border.Rounding = 40F;
            cmbEstado.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbEstado.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbEstado.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbEstado.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbEstado.TabIndex = 235;
            // 
            // cmbModelo
            // 
            cmbModelo.DropDownWidth = 300;
            cmbModelo.Location = new Point(207, 349);
            cmbModelo.Name = "cmbModelo";
            cmbModelo.Size = new Size(250, 52);
            cmbModelo.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbModelo.StateCommon.ComboBox.Border.Rounding = 40F;
            cmbModelo.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbModelo.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbModelo.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbModelo.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbModelo.TabIndex = 234;
            // 
            // cmbTipo
            // 
            cmbTipo.DropDownWidth = 300;
            cmbTipo.Location = new Point(207, 288);
            cmbTipo.Name = "cmbTipo";
            cmbTipo.Size = new Size(250, 52);
            cmbTipo.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbTipo.StateCommon.ComboBox.Border.Rounding = 40F;
            cmbTipo.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbTipo.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbTipo.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbTipo.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbTipo.TabIndex = 233;
            // 
            // cmbMarca
            // 
            cmbMarca.DropDownWidth = 300;
            cmbMarca.Location = new Point(207, 229);
            cmbMarca.Name = "cmbMarca";
            cmbMarca.Size = new Size(250, 52);
            cmbMarca.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbMarca.StateCommon.ComboBox.Border.Rounding = 40F;
            cmbMarca.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbMarca.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbMarca.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbMarca.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbMarca.TabIndex = 232;
            // 
            // AgregarProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(515, 589);
            Controls.Add(cmbEstado);
            Controls.Add(cmbModelo);
            Controls.Add(cmbTipo);
            Controls.Add(cmbMarca);
            Controls.Add(pictureBox9);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox1);
            Controls.Add(btnCancelar);
            Controls.Add(kryptonButton20);
            Controls.Add(txtStock);
            Controls.Add(label1);
            Controls.Add(txtPrecio);
            Controls.Add(txtNombre);
            Controls.Add(txtID);
            Controls.Add(label15);
            Controls.Add(label10);
            Controls.Add(label12);
            Controls.Add(label14);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(btnsalir);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Name = "AgregarProducto";
            Text = "AgregarProducto";
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox9).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbModelo).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbTipo).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbMarca).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonButton btnCancelar;
        private Krypton.Toolkit.KryptonButton kryptonButton20;
        private Krypton.Toolkit.KryptonTextBox txtStock;
        private Label label1;
        private Krypton.Toolkit.KryptonTextBox txtPrecio;
        private Krypton.Toolkit.KryptonTextBox txtNombre;
        private Krypton.Toolkit.KryptonTextBox txtID;
        private Label label15;
        private Label label10;
        private Label label12;
        private Label label14;
        private Label label8;
        private Label label7;
        private Label label4;
        private Label label3;
        private Krypton.Toolkit.KryptonButton btnsalir;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private PictureBox pictureBox4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox9;
        private Krypton.Toolkit.KryptonComboBox cmbEstado;
        private Krypton.Toolkit.KryptonComboBox cmbModelo;
        private Krypton.Toolkit.KryptonComboBox cmbTipo;
        private Krypton.Toolkit.KryptonComboBox cmbMarca;
    }
}