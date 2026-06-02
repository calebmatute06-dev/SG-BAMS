namespace SG_BAMS.Proveedor
{
    partial class ModificarProveedor
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
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            txtDireccion = new Krypton.Toolkit.KryptonTextBox();
            label2 = new Label();
            txtRTN = new Krypton.Toolkit.KryptonTextBox();
            label1 = new Label();
            txtTelefono = new Krypton.Toolkit.KryptonTextBox();
            txtNombre = new Krypton.Toolkit.KryptonTextBox();
            label15 = new Label();
            label7 = new Label();
            label4 = new Label();
            cmbClasificacion = new Krypton.Toolkit.KryptonComboBox();
            cmbEstado = new Krypton.Toolkit.KryptonComboBox();
            label5 = new Label();
            label3 = new Label();
            txtID = new Krypton.Toolkit.KryptonTextBox();
            label6 = new Label();
            label8 = new Label();
            btnCancelar = new Krypton.Toolkit.KryptonButton();
            btnAceptar = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbClasificacion).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).BeginInit();
            SuspendLayout();
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(-1, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(25, 631);
            pictureBox4.TabIndex = 187;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-1, 607);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(519, 24);
            pictureBox1.TabIndex = 186;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(491, 0);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(26, 615);
            pictureBox3.TabIndex = 165;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(-1, -15);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(515, 36);
            pictureBox2.TabIndex = 164;
            pictureBox2.TabStop = false;
            // 
            // txtDireccion
            // 
            txtDireccion.Location = new Point(173, 269);
            txtDireccion.MaxLength = 70;
            txtDireccion.Name = "txtDireccion";
            txtDireccion.Size = new Size(250, 31);
            txtDireccion.StateCommon.Back.Color1 = Color.White;
            txtDireccion.StateCommon.Border.Color1 = Color.Navy;
            txtDireccion.StateCommon.Border.Rounding = 5F;
            txtDireccion.StateCommon.Content.Color1 = Color.Navy;
            txtDireccion.TabIndex = 202;
            txtDireccion.KeyPress += txtDireccion_KeyPress;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(50, 275);
            label2.Name = "label2";
            label2.Size = new Size(115, 33);
            label2.TabIndex = 201;
            label2.Text = "Dirección:";
            // 
            // txtRTN
            // 
            txtRTN.Location = new Point(170, 325);
            txtRTN.MaxLength = 14;
            txtRTN.Name = "txtRTN";
            txtRTN.Size = new Size(250, 31);
            txtRTN.StateCommon.Back.Color1 = Color.White;
            txtRTN.StateCommon.Border.Color1 = Color.Navy;
            txtRTN.StateCommon.Border.Rounding = 5F;
            txtRTN.StateCommon.Content.Color1 = Color.Navy;
            txtRTN.TabIndex = 200;
            txtRTN.KeyPress += txtRTN_KeyPress;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(41, 448);
            label1.Name = "label1";
            label1.Size = new Size(143, 33);
            label1.TabIndex = 198;
            label1.Text = "Clasificación:";
            // 
            // txtTelefono
            // 
            txtTelefono.Location = new Point(173, 219);
            txtTelefono.MaxLength = 8;
            txtTelefono.Name = "txtTelefono";
            txtTelefono.Size = new Size(250, 31);
            txtTelefono.StateCommon.Back.Color1 = Color.White;
            txtTelefono.StateCommon.Border.Color1 = Color.Navy;
            txtTelefono.StateCommon.Border.Rounding = 5F;
            txtTelefono.StateCommon.Content.Color1 = Color.Navy;
            txtTelefono.TabIndex = 197;
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            // 
            // txtNombre
            // 
            txtNombre.Location = new Point(173, 160);
            txtNombre.MaxLength = 70;
            txtNombre.Name = "txtNombre";
            txtNombre.Size = new Size(250, 31);
            txtNombre.StateCommon.Back.Color1 = Color.White;
            txtNombre.StateCommon.Border.Color1 = Color.Navy;
            txtNombre.StateCommon.Border.Rounding = 5F;
            txtNombre.StateCommon.Content.Color1 = Color.Navy;
            txtNombre.TabIndex = 196;
            txtNombre.KeyPress += txtNombre_KeyPress;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.BackColor = Color.Transparent;
            label15.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label15.ForeColor = Color.Navy;
            label15.Location = new Point(53, 328);
            label15.Name = "label15";
            label15.Size = new Size(69, 33);
            label15.TabIndex = 194;
            label15.Text = "RTN:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(48, 219);
            label7.Name = "label7";
            label7.Size = new Size(116, 33);
            label7.TabIndex = 193;
            label7.Text = "Télefono: ";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(55, 165);
            label4.Name = "label4";
            label4.Size = new Size(111, 33);
            label4.TabIndex = 192;
            label4.Text = "Nombre: ";
            // 
            // cmbClasificacion
            // 
            cmbClasificacion.DropDownWidth = 300;
            cmbClasificacion.Location = new Point(174, 443);
            cmbClasificacion.Name = "cmbClasificacion";
            cmbClasificacion.Size = new Size(250, 30);
            cmbClasificacion.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbClasificacion.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbClasificacion.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbClasificacion.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbClasificacion.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbClasificacion.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbClasificacion.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 12F);
            cmbClasificacion.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbClasificacion.TabIndex = 236;
            cmbClasificacion.SelectedIndexChanged += cmbClasificacion_SelectedIndexChanged;
            // 
            // cmbEstado
            // 
            cmbEstado.DropDownWidth = 300;
            cmbEstado.Location = new Point(171, 384);
            cmbEstado.Name = "cmbEstado";
            cmbEstado.Size = new Size(250, 30);
            cmbEstado.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbEstado.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbEstado.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbEstado.StateCommon.ComboBox.Content.Color1 = Color.Navy;
            cmbEstado.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbEstado.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbEstado.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 12F);
            cmbEstado.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbEstado.TabIndex = 240;
            cmbEstado.SelectedIndexChanged += cmbEstado_SelectedIndexChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(53, 384);
            label5.Name = "label5";
            label5.Size = new Size(91, 33);
            label5.TabIndex = 239;
            label5.Text = "Estado:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(56, 113);
            label3.Name = "label3";
            label3.Size = new Size(44, 33);
            label3.TabIndex = 192;
            label3.Text = "ID:";
            // 
            // txtID
            // 
            txtID.Enabled = false;
            txtID.Location = new Point(173, 107);
            txtID.Name = "txtID";
            txtID.Size = new Size(250, 31);
            txtID.StateCommon.Back.Color1 = Color.White;
            txtID.StateCommon.Border.Color1 = Color.Navy;
            txtID.StateCommon.Border.Rounding = 5F;
            txtID.StateCommon.Content.Color1 = Color.Navy;
            txtID.TabIndex = 196;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.BackColor = Color.Transparent;
            label6.Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(99, 40);
            label6.Name = "label6";
            label6.Size = new Size(349, 40);
            label6.TabIndex = 352;
            label6.Text = "Modificar Proveedor";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(393, 565);
            label8.Name = "label8";
            label8.Size = new Size(102, 35);
            label8.TabIndex = 354;
            label8.Text = "BAMS";
            // 
            // btnCancelar
            // 
            btnCancelar.Location = new Point(235, 525);
            btnCancelar.Name = "btnCancelar";
            btnCancelar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideDefault.Back.Color2 = Color.White;
            btnCancelar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCancelar.OverrideFocus.Back.Color2 = Color.White;
            btnCancelar.Size = new Size(135, 51);
            btnCancelar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateCommon.Back.Color2 = Color.White;
            btnCancelar.StateCommon.Border.Rounding = 5F;
            btnCancelar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCancelar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancelar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCancelar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCancelar.StatePressed.Back.Color1 = Color.Transparent;
            btnCancelar.StatePressed.Back.Color2 = Color.Transparent;
            btnCancelar.TabIndex = 358;
            btnCancelar.Values.DropDownArrowColor = Color.Empty;
            btnCancelar.Values.Text = "Cancelar";
            btnCancelar.Click += btnCancelar_Click;
            // 
            // btnAceptar
            // 
            btnAceptar.Location = new Point(94, 525);
            btnAceptar.Name = "btnAceptar";
            btnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideDefault.Back.Color2 = Color.White;
            btnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAceptar.OverrideFocus.Back.Color2 = Color.White;
            btnAceptar.Size = new Size(135, 51);
            btnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateCommon.Back.Color2 = Color.White;
            btnAceptar.StateCommon.Border.Rounding = 5F;
            btnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            btnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            btnAceptar.TabIndex = 357;
            btnAceptar.Values.DropDownArrowColor = Color.Empty;
            btnAceptar.Values.Text = "Aceptar";
            btnAceptar.Click += btnAceptar_Click_1;
            // 
            // ModificarProveedor
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(515, 629);
            Controls.Add(btnCancelar);
            Controls.Add(btnAceptar);
            Controls.Add(label8);
            Controls.Add(label6);
            Controls.Add(cmbEstado);
            Controls.Add(label5);
            Controls.Add(cmbClasificacion);
            Controls.Add(txtDireccion);
            Controls.Add(label2);
            Controls.Add(txtRTN);
            Controls.Add(label1);
            Controls.Add(txtTelefono);
            Controls.Add(txtID);
            Controls.Add(txtNombre);
            Controls.Add(label15);
            Controls.Add(label3);
            Controls.Add(label7);
            Controls.Add(label4);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Name = "ModificarProveedor";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += ModificarProveedor_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbClasificacion).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbEstado).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Krypton.Toolkit.KryptonTextBox txtDireccion;
        private Label label2;
        private Krypton.Toolkit.KryptonTextBox txtRTN;
        private Label label1;
        private Krypton.Toolkit.KryptonTextBox txtTelefono;
        private Krypton.Toolkit.KryptonTextBox txtNombre;
        private Label label15;
        private Label label7;
        private Label label4;
        private Krypton.Toolkit.KryptonComboBox cmbClasificacion;
        private Krypton.Toolkit.KryptonComboBox cmbEstado;
        private Label label5;
        private Label label3;
        private Krypton.Toolkit.KryptonTextBox txtID;
        private Label label6;
        private Label label8;
        private Krypton.Toolkit.KryptonButton btnCancelar;
        private Krypton.Toolkit.KryptonButton btnAceptar;
    }
}