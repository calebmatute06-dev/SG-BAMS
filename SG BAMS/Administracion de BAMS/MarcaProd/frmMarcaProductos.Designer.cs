namespace SG_BAMS
{
    partial class frmMarcaProductos
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
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            dgvMarcas = new DataGridView();
            btnAgregar = new Krypton.Toolkit.KryptonButton();
            btnModificar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            label5 = new Label();
            label8 = new Label();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarcas).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 418);
            pictureBox1.TabIndex = 111;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(643, 18);
            panel1.TabIndex = 109;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(622, 0);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 420);
            pictureBox2.TabIndex = 110;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 401);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(643, 18);
            panel2.TabIndex = 108;
            // 
            // dgvMarcas
            // 
            dataGridViewCellStyle1.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Navy;
            dgvMarcas.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvMarcas.BackgroundColor = Color.SkyBlue;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvMarcas.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvMarcas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.SkyBlue;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvMarcas.DefaultCellStyle = dataGridViewCellStyle3;
            dgvMarcas.Location = new Point(67, 74);
            dgvMarcas.Margin = new Padding(3, 2, 3, 2);
            dgvMarcas.Name = "dgvMarcas";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvMarcas.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvMarcas.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = Color.Navy;
            dgvMarcas.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvMarcas.Size = new Size(513, 257);
            dgvMarcas.TabIndex = 174;
            dgvMarcas.CellContentDoubleClick += dgvMarcas_CellContentDoubleClick;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(117, 335);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(125, 49);
            btnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateCommon.Back.Color2 = Color.White;
            btnAgregar.StateCommon.Border.Rounding = 30F;
            btnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAgregar.StatePressed.Back.Color1 = Color.Transparent;
            btnAgregar.StatePressed.Back.Color2 = Color.Transparent;
            btnAgregar.TabIndex = 342;
            btnAgregar.Values.DropDownArrowColor = Color.Empty;
            btnAgregar.Values.Text = "Agregar";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(260, 335);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideDefault.Back.Color2 = Color.White;
            btnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideFocus.Back.Color2 = Color.White;
            btnModificar.Size = new Size(125, 49);
            btnModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnModificar.StateCommon.Back.Color2 = Color.White;
            btnModificar.StateCommon.Border.Rounding = 30F;
            btnModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnModificar.StatePressed.Back.Color1 = Color.Transparent;
            btnModificar.StatePressed.Back.Color2 = Color.Transparent;
            btnModificar.TabIndex = 343;
            btnModificar.Values.DropDownArrowColor = Color.Empty;
            btnModificar.Values.Text = "Modificar";
            btnModificar.Click += btnModificar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(401, 335);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(125, 49);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 30F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 344;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(532, 370);
            label5.Name = "label5";
            label5.Size = new Size(84, 29);
            label5.TabIndex = 347;
            label5.Text = "BAMS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.SkyBlue;
            label8.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(231, 36);
            label8.Name = "label8";
            label8.Size = new Size(198, 22);
            label8.TabIndex = 352;
            label8.Text = "Marca de Productos";
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.CaptionVisible = false;
            kryptonGroupBox2.Location = new Point(209, 27);
            kryptonGroupBox2.Size = new Size(238, 39);
            kryptonGroupBox2.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox2.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox2.TabIndex = 353;
            // 
            // frmMarcaProductos
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(643, 419);
            Controls.Add(label8);
            Controls.Add(kryptonGroupBox2);
            Controls.Add(label5);
            Controls.Add(btnSalir);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(dgvMarcas);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            FormBorderStyle = FormBorderStyle.None;
            Name = "frmMarcaProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrMarcaProductos";
            Load += frmMarcaProductos_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarcas).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Panel panel2;
        private DataGridView dgvMarcas;
        private Krypton.Toolkit.KryptonButton btnAgregar;
        private Krypton.Toolkit.KryptonButton btnModificar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Label label5;
        private Label label8;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
    }
}