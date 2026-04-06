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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMarcaProductos));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            pictureBox16 = new PictureBox();
            dgvMarcas = new DataGridView();
            label2 = new Label();
            btnAgregar = new Krypton.Toolkit.KryptonButton();
            btnModificar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarcas).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(580, 451);
            label7.Name = "label7";
            label7.Size = new Size(77, 31);
            label7.TabIndex = 112;
            label7.Text = "BAMS";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 508);
            pictureBox1.TabIndex = 111;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 24);
            panel1.TabIndex = 109;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(659, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 511);
            pictureBox2.TabIndex = 110;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 487);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 24);
            panel2.TabIndex = 108;
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = (Image)resources.GetObject("pictureBox16.BackgroundImage");
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Image = (Image)resources.GetObject("pictureBox16.Image");
            pictureBox16.Location = new Point(500, 41);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(49, 48);
            pictureBox16.TabIndex = 106;
            pictureBox16.TabStop = false;
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
            dgvMarcas.Location = new Point(77, 99);
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
            dgvMarcas.Size = new Size(512, 258);
            dgvMarcas.TabIndex = 174;
            dgvMarcas.CellContentDoubleClick += dgvMarcas_CellContentDoubleClick;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 21F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(183, 43);
            label2.Name = "label2";
            label2.Size = new Size(311, 42);
            label2.TabIndex = 341;
            label2.Text = "Marcas de Productos";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(104, 373);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(143, 65);
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
            btnModificar.Location = new Point(267, 373);
            btnModificar.Name = "btnModificar";
            btnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideDefault.Back.Color2 = Color.White;
            btnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideFocus.Back.Color2 = Color.White;
            btnModificar.Size = new Size(143, 65);
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
            btnSalir.Location = new Point(429, 373);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(143, 65);
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
            // frmMarcaProductos
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(683, 511);
            Controls.Add(btnSalir);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar);
            Controls.Add(label2);
            Controls.Add(dgvMarcas);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(pictureBox16);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmMarcaProductos";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "fmrMarcaProductos";
            Load += frmMarcaProductos_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvMarcas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label7;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Panel panel2;
        private PictureBox pictureBox16;
        private DataGridView dgvMarcas;
        private Label label2;
        private Krypton.Toolkit.KryptonButton btnAgregar;
        private Krypton.Toolkit.KryptonButton btnModificar;
        private Krypton.Toolkit.KryptonButton btnSalir;
    }
}