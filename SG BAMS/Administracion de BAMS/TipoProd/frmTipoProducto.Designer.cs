namespace SG_BAMS
{
    partial class frmTipoProducto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTipoProducto));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            pictureBox16 = new PictureBox();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            dgvTipoProducto = new Krypton.Toolkit.KryptonDataGridView();
            label1 = new Label();
            btmAgregar = new Krypton.Toolkit.KryptonButton();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            btmSalir = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvTipoProducto).BeginInit();
            SuspendLayout();
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = (Image)resources.GetObject("pictureBox16.BackgroundImage");
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(372, 36);
            pictureBox16.Margin = new Padding(3, 2, 3, 2);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(35, 29);
            pictureBox16.TabIndex = 122;
            pictureBox16.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(509, 338);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 121;
            label7.Text = "BAMS";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-2, 2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 381);
            pictureBox1.TabIndex = 120;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(-2, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(598, 18);
            panel1.TabIndex = 118;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(575, 0);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 383);
            pictureBox2.TabIndex = 119;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(-2, 365);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 18);
            panel2.TabIndex = 117;
            // 
            // dgvTipoProducto
            // 
            dgvTipoProducto.AllowUserToAddRows = false;
            dgvTipoProducto.AllowUserToDeleteRows = false;
            dgvTipoProducto.BorderStyle = BorderStyle.None;
            dgvTipoProducto.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTipoProducto.Location = new Point(65, 89);
            dgvTipoProducto.Name = "dgvTipoProducto";
            dgvTipoProducto.ReadOnly = true;
            dgvTipoProducto.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Arial Narrow", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Navy;
            dataGridViewCellStyle1.SelectionBackColor = Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvTipoProducto.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvTipoProducto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTipoProducto.Size = new Size(448, 174);
            dgvTipoProducto.StateCommon.Background.Color1 = Color.SkyBlue;
            dgvTipoProducto.StateCommon.Background.Color2 = Color.SkyBlue;
            dgvTipoProducto.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            dgvTipoProducto.TabIndex = 116;
            dgvTipoProducto.CellContentClick += dgvTipoProducto_CellContentClick;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(162, 36);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 115;
            label1.Text = "Tipo de Producto";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btmAgregar
            // 
            btmAgregar.Location = new Point(166, 276);
            btmAgregar.Margin = new Padding(3, 2, 3, 2);
            btmAgregar.Name = "btmAgregar";
            btmAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideDefault.Back.Color2 = Color.White;
            btmAgregar.OverrideDefault.Border.Rounding = 40F;
            btmAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmAgregar.OverrideFocus.Back.Color2 = Color.White;
            btmAgregar.Size = new Size(103, 49);
            btmAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateCommon.Back.Color2 = Color.White;
            btmAgregar.StateCommon.Border.Rounding = 40F;
            btmAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmAgregar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btmAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmAgregar.StateNormal.Back.Color2 = Color.White;
            btmAgregar.StateNormal.Border.Rounding = 40F;
            btmAgregar.StateTracking.Border.Rounding = 40F;
            btmAgregar.TabIndex = 149;
            btmAgregar.Values.DropDownArrowColor = Color.Empty;
            btmAgregar.Values.Text = "Agregar";
            btmAgregar.Click += btmAgregar_Click;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(304, 277);
            btmModificar.Margin = new Padding(3, 2, 3, 2);
            btmModificar.Name = "btmModificar";
            btmModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideDefault.Back.Color2 = Color.White;
            btmModificar.OverrideDefault.Border.Rounding = 40F;
            btmModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmModificar.OverrideFocus.Back.Color2 = Color.White;
            btmModificar.Size = new Size(103, 49);
            btmModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btmModificar.StateCommon.Back.Color2 = Color.White;
            btmModificar.StateCommon.Border.Rounding = 40F;
            btmModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmModificar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btmModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btmModificar.StateNormal.Back.Color2 = Color.White;
            btmModificar.StateNormal.Border.Rounding = 40F;
            btmModificar.StateTracking.Border.Rounding = 40F;
            btmModificar.TabIndex = 150;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Modificar";
            btmModificar.Click += btmModificar_Click;
            // 
            // btmSalir
            // 
            btmSalir.Location = new Point(407, 320);
            btmSalir.Margin = new Padding(3, 2, 3, 2);
            btmSalir.Name = "btmSalir";
            btmSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideDefault.Back.Color2 = Color.White;
            btmSalir.OverrideDefault.Border.Rounding = 40F;
            btmSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideFocus.Back.Color2 = Color.White;
            btmSalir.Size = new Size(101, 39);
            btmSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btmSalir.StateCommon.Back.Color2 = Color.White;
            btmSalir.StateCommon.Border.Rounding = 40F;
            btmSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmSalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btmSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btmSalir.StateNormal.Back.Color2 = Color.White;
            btmSalir.StateNormal.Border.Rounding = 40F;
            btmSalir.StateTracking.Border.Rounding = 40F;
            btmSalir.TabIndex = 151;
            btmSalir.Values.DropDownArrowColor = Color.Empty;
            btmSalir.Values.Text = "Salir";
            btmSalir.Click += btmSalir_Click;
            // 
            // frmTipoProducto
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(596, 383);
            Controls.Add(btmSalir);
            Controls.Add(btmModificar);
            Controls.Add(btmAgregar);
            Controls.Add(pictureBox16);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(dgvTipoProducto);
            Controls.Add(label1);
            Name = "frmTipoProducto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmTipoProducto";
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvTipoProducto).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox16;
        private Label label7;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Panel panel2;
        private Krypton.Toolkit.KryptonDataGridView dgvTipoProducto;
        private Label label1;
        private Krypton.Toolkit.KryptonButton btmAgregar;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton btmSalir;
    }
}