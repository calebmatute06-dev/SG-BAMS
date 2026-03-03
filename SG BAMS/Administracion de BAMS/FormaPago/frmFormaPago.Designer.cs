namespace SG_BAMS
{
    partial class frmFormaPago
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmFormaPago));
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            label7 = new Label();
            pictureBox1 = new PictureBox();
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            panel2 = new Panel();
            label1 = new Label();
            pictureBox16 = new PictureBox();
            btmSalir = new Krypton.Toolkit.KryptonButton();
            btnAgregar = new Krypton.Toolkit.KryptonButton();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            dgvFormasPago = new Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvFormasPago).BeginInit();
            SuspendLayout();
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(511, 338);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 121;
            label7.Text = "BAMS";
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 381);
            pictureBox1.TabIndex = 120;
            pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(598, 18);
            panel1.TabIndex = 118;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(577, 0);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 383);
            pictureBox2.TabIndex = 119;
            pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 365);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 18);
            panel2.TabIndex = 117;
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(160, 36);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 115;
            label1.Text = "Tipo de Forma de Pago";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // pictureBox16
            // 
            pictureBox16.BackgroundImage = (Image)resources.GetObject("pictureBox16.BackgroundImage");
            pictureBox16.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox16.Location = new Point(396, 36);
            pictureBox16.Margin = new Padding(3, 2, 3, 2);
            pictureBox16.Name = "pictureBox16";
            pictureBox16.Size = new Size(41, 36);
            pictureBox16.TabIndex = 135;
            pictureBox16.TabStop = false;
            // 
            // btmSalir
            // 
            btmSalir.Location = new Point(412, 312);
            btmSalir.Margin = new Padding(3, 2, 3, 2);
            btmSalir.Name = "btmSalir";
            btmSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideDefault.Back.Color2 = Color.White;
            btmSalir.OverrideDefault.Border.Rounding = 40F;
            btmSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btmSalir.OverrideFocus.Back.Color2 = Color.White;
            btmSalir.Size = new Size(103, 49);
            btmSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btmSalir.StateCommon.Back.Color2 = Color.White;
            btmSalir.StateCommon.Border.Rounding = 40F;
            btmSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btmSalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btmSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btmSalir.StateNormal.Back.Color2 = Color.White;
            btmSalir.StateNormal.Border.Rounding = 40F;
            btmSalir.StateTracking.Border.Rounding = 40F;
            btmSalir.TabIndex = 137;
            btmSalir.Values.DropDownArrowColor = Color.Empty;
            btmSalir.Values.Text = "Salir";
            btmSalir.Click += btmSalir_Click;
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(181, 279);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideDefault.Border.Rounding = 40F;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(103, 49);
            btnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateCommon.Back.Color2 = Color.White;
            btnAgregar.StateCommon.Border.Rounding = 40F;
            btnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateNormal.Back.Color2 = Color.White;
            btnAgregar.StateNormal.Border.Rounding = 40F;
            btnAgregar.StateTracking.Border.Rounding = 40F;
            btnAgregar.TabIndex = 138;
            btnAgregar.Values.DropDownArrowColor = Color.Empty;
            btnAgregar.Values.Text = "Agregar";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(303, 279);
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
            btmModificar.TabIndex = 140;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Modificar";
            btmModificar.Click += btmModificar_Click;
            // 
            // dgvFormasPago
            // 
            dgvFormasPago.AllowUserToAddRows = false;
            dgvFormasPago.AllowUserToDeleteRows = false;
            dgvFormasPago.BorderStyle = BorderStyle.None;
            dgvFormasPago.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFormasPago.Location = new Point(77, 90);
            dgvFormasPago.Name = "dgvFormasPago";
            dgvFormasPago.ReadOnly = true;
            dgvFormasPago.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Arial Narrow", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Navy;
            dataGridViewCellStyle1.SelectionBackColor = Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvFormasPago.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvFormasPago.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFormasPago.Size = new Size(448, 174);
            dgvFormasPago.StateCommon.Background.Color1 = Color.SkyBlue;
            dgvFormasPago.StateCommon.Background.Color2 = Color.SkyBlue;
            dgvFormasPago.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            dgvFormasPago.TabIndex = 156;
            dgvFormasPago.CellContentClick += dgvFormasPago_CellContentClick;
            // 
            // frmFormaPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(597, 382);
            Controls.Add(dgvFormasPago);
            Controls.Add(btmModificar);
            Controls.Add(btnAgregar);
            Controls.Add(btmSalir);
            Controls.Add(pictureBox16);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(label1);
            Name = "frmFormaPago";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmFormaPago";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox16).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvFormasPago).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label7;
        private PictureBox pictureBox1;
        private Panel panel1;
        private PictureBox pictureBox2;
        private Panel panel2;
        private Label label1;
        private PictureBox pictureBox16;
        private Krypton.Toolkit.KryptonButton btmSalir;
        private Krypton.Toolkit.KryptonButton btnAgregar;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonDataGridView dgvFormasPago;
    }
}