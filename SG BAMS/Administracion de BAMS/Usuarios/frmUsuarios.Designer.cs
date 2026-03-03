
namespace SG_BAMS
{
    partial class frmUsuarios
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
            label1 = new Label();
            panel2 = new Panel();
            pictureBox2 = new PictureBox();
            panel1 = new Panel();
            pictureBox1 = new PictureBox();
            label7 = new Label();
            btmModificar = new Krypton.Toolkit.KryptonButton();
            btmAgregar = new Krypton.Toolkit.KryptonButton();
            BtmSalir = new Krypton.Toolkit.KryptonButton();
            pictureBox3 = new PictureBox();
            dgvUsuarios = new Krypton.Toolkit.KryptonDataGridView();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(155, 36);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 85;
            label1.Text = "Usuarios";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Click += label1_Click;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 365);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 18);
            panel2.TabIndex = 88;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(577, 0);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 383);
            pictureBox2.TabIndex = 90;
            pictureBox2.TabStop = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(598, 18);
            panel1.TabIndex = 89;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(0, 2);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 381);
            pictureBox1.TabIndex = 91;
            pictureBox1.TabStop = false;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(511, 338);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 92;
            label7.Text = "BAMS";
            // 
            // btmModificar
            // 
            btmModificar.Location = new Point(306, 279);
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
            btmModificar.TabIndex = 149;
            btmModificar.Values.DropDownArrowColor = Color.Empty;
            btmModificar.Values.Text = "Modificar";
            btmModificar.Click += btmModificar_Click;
            // 
            // btmAgregar
            // 
            btmAgregar.Location = new Point(168, 276);
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
            btmAgregar.TabIndex = 150;
            btmAgregar.Values.DropDownArrowColor = Color.Empty;
            btmAgregar.Values.Text = "Agregar";
            btmAgregar.Click += btmAgregar_Click;
            // 
            // BtmSalir
            // 
            BtmSalir.Location = new Point(415, 309);
            BtmSalir.Margin = new Padding(3, 2, 3, 2);
            BtmSalir.Name = "BtmSalir";
            BtmSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtmSalir.OverrideDefault.Back.Color2 = Color.White;
            BtmSalir.OverrideDefault.Border.Rounding = 40F;
            BtmSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtmSalir.OverrideFocus.Back.Color2 = Color.White;
            BtmSalir.Size = new Size(101, 39);
            BtmSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtmSalir.StateCommon.Back.Color2 = Color.White;
            BtmSalir.StateCommon.Border.Rounding = 40F;
            BtmSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtmSalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            BtmSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            BtmSalir.StateNormal.Back.Color2 = Color.White;
            BtmSalir.StateNormal.Border.Rounding = 40F;
            BtmSalir.StateTracking.Border.Rounding = 40F;
            BtmSalir.TabIndex = 151;
            BtmSalir.Values.DropDownArrowColor = Color.Empty;
            BtmSalir.Values.Text = "Salir";
            BtmSalir.Click += BtmSalir_Click;
            // 
            // pictureBox3
            // 
            pictureBox3.BackgroundImage = Properties.Resources.perfiles;
            pictureBox3.BackgroundImageLayout = ImageLayout.Stretch;
            pictureBox3.Location = new Point(325, 36);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(35, 29);
            pictureBox3.TabIndex = 153;
            pictureBox3.TabStop = false;
            // 
            // dgvUsuarios
            // 
            dgvUsuarios.AllowUserToAddRows = false;
            dgvUsuarios.AllowUserToDeleteRows = false;
            dgvUsuarios.BorderStyle = BorderStyle.None;
            dgvUsuarios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsuarios.Location = new Point(68, 83);
            dgvUsuarios.Name = "dgvUsuarios";
            dgvUsuarios.ReadOnly = true;
            dgvUsuarios.RowHeadersWidth = 51;
            dataGridViewCellStyle1.BackColor = Color.White;
            dataGridViewCellStyle1.Font = new Font("Arial Narrow", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Navy;
            dataGridViewCellStyle1.SelectionBackColor = Color.SkyBlue;
            dataGridViewCellStyle1.SelectionForeColor = Color.White;
            dgvUsuarios.RowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvUsuarios.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvUsuarios.Size = new Size(448, 174);
            dgvUsuarios.StateCommon.Background.Color1 = Color.SkyBlue;
            dgvUsuarios.StateCommon.Background.Color2 = Color.SkyBlue;
            dgvUsuarios.StateCommon.BackStyle = Krypton.Toolkit.PaletteBackStyle.GridBackgroundList;
            dgvUsuarios.TabIndex = 154;
            dgvUsuarios.CellContentClick += dgvUsuarios_CellContentClick;
            // 
            // frmUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(598, 382);
            Controls.Add(dgvUsuarios);
            Controls.Add(pictureBox3);
            Controls.Add(BtmSalir);
            Controls.Add(btmAgregar);
            Controls.Add(btmModificar);
            Controls.Add(label7);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(panel2);
            Controls.Add(label1);
            Name = "frmUsuarios";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "x";
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvUsuarios).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion
        private Label label1;
        private Panel panel2;
        private PictureBox pictureBox2;
        private Panel panel1;
        private PictureBox pictureBox1;
        private Label label7;
        private Krypton.Toolkit.KryptonButton btmModificar;
        private Krypton.Toolkit.KryptonButton btmAgregar;
        private Krypton.Toolkit.KryptonButton BtmSalir;
        private DataGridView dd;
        private PictureBox pictureBox3;
        private Krypton.Toolkit.KryptonDataGridView dgvUsuarios;
    }
}