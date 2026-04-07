namespace SG_BAMS
{
    partial class ClientesEmp
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
            panel3 = new Panel();
            panel4 = new Panel();
            panel8 = new Panel();
            panel5 = new Panel();
            panel2 = new Panel();
            chkActivo = new Krypton.Toolkit.KryptonCheckBox();
            dgvClientes = new DataGridView();
            BtnModificar = new Krypton.Toolkit.KryptonButton();
            txtBusqueda = new Krypton.Toolkit.KryptonTextBox();
            kryptonButton14 = new Krypton.Toolkit.KryptonButton();
            label1 = new Label();
            btnNoti = new Button();
            label7 = new Label();
            label10 = new Label();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            label2 = new Label();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(1, 662);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1132, 18);
            panel3.TabIndex = 147;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(1101, 0);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(21, 678);
            panel4.TabIndex = 144;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(236, 16);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(21, 652);
            panel8.TabIndex = 146;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(-1, 0);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(21, 678);
            panel5.TabIndex = 145;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(6, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1124, 18);
            panel2.TabIndex = 143;
            // 
            // chkActivo
            // 
            chkActivo.Location = new Point(942, 203);
            chkActivo.Margin = new Padding(3, 2, 3, 2);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(91, 23);
            chkActivo.StateCommon.ShortText.Color1 = Color.Navy;
            chkActivo.StateCommon.ShortText.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            chkActivo.TabIndex = 340;
            chkActivo.Values.Text = "Inactivos";
            chkActivo.CheckedChanged += chkActivo_CheckedChanged;
            // 
            // dgvClientes
            // 
            dgvClientes.BackgroundColor = Color.SkyBlue;
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvClientes.Location = new Point(312, 250);
            dgvClientes.Margin = new Padding(3, 2, 3, 2);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.Size = new Size(722, 284);
            dgvClientes.TabIndex = 339;
            dgvClientes.CellContentClick += dgvClientes_CellContentClick;
            dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
            // 
            // BtnModificar
            // 
            BtnModificar.Location = new Point(628, 594);
            BtnModificar.Margin = new Padding(3, 2, 3, 2);
            BtnModificar.Name = "BtnModificar";
            BtnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnModificar.OverrideDefault.Back.Color2 = Color.White;
            BtnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnModificar.OverrideFocus.Back.Color2 = Color.White;
            BtnModificar.Size = new Size(128, 45);
            BtnModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnModificar.StateCommon.Back.Color2 = Color.White;
            BtnModificar.StateCommon.Border.Rounding = 30F;
            BtnModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnModificar.StatePressed.Back.Color1 = Color.Transparent;
            BtnModificar.StatePressed.Back.Color2 = Color.Transparent;
            BtnModificar.TabIndex = 338;
            BtnModificar.Values.DropDownArrowColor = Color.Empty;
            BtnModificar.Values.Text = "Modificar";
            BtnModificar.Click += BtnModificar_Click;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(424, 203);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(499, 29);
            txtBusqueda.StateCommon.Back.Color1 = Color.SkyBlue;
            txtBusqueda.StateCommon.Border.Rounding = 10F;
            txtBusqueda.TabIndex = 336;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // kryptonButton14
            // 
            kryptonButton14.Location = new Point(288, 208);
            kryptonButton14.Margin = new Padding(3, 2, 3, 2);
            kryptonButton14.Name = "kryptonButton14";
            kryptonButton14.Size = new Size(0, 0);
            kryptonButton14.StateCommon.Border.Rounding = 100F;
            kryptonButton14.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton14.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton14.TabIndex = 333;
            kryptonButton14.Values.DropDownArrowColor = Color.Empty;
            kryptonButton14.Values.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(886, 574);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 332;
            // 
            // btnNoti
            // 
            btnNoti.BackColor = Color.Transparent;
            btnNoti.BackgroundImage = Properties.Resources.campana;
            btnNoti.BackgroundImageLayout = ImageLayout.Stretch;
            btnNoti.FlatAppearance.BorderColor = Color.White;
            btnNoti.FlatAppearance.BorderSize = 0;
            btnNoti.FlatStyle = FlatStyle.Flat;
            btnNoti.ForeColor = Color.Navy;
            btnNoti.Location = new Point(1046, 21);
            btnNoti.Margin = new Padding(3, 2, 3, 2);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(52, 33);
            btnNoti.TabIndex = 329;
            btnNoti.UseVisualStyleBackColor = false;
            btnNoti.Click += btnNoti_Click;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.BackColor = Color.Transparent;
            label7.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(332, 206);
            label7.Name = "label7";
            label7.Size = new Size(84, 22);
            label7.TabIndex = 355;
            label7.Text = "Buscar:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.SkyBlue;
            label10.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(603, 50);
            label10.Name = "label10";
            label10.Size = new Size(124, 33);
            label10.TabIndex = 356;
            label10.Text = "Clientes";
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.CaptionVisible = false;
            kryptonGroupBox2.Location = new Point(514, 40);
            kryptonGroupBox2.Size = new Size(301, 49);
            kryptonGroupBox2.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox2.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox2.TabIndex = 357;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(66, 40);
            label2.Name = "label2";
            label2.Size = new Size(117, 41);
            label2.TabIndex = 380;
            label2.Text = "BAMS";
            // 
            // btnFacturas
            // 
            btnFacturas.BackColor = Color.Transparent;
            btnFacturas.DialogResult = DialogResult.None;
            btnFacturas.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFacturas.ForeColor = Color.SkyBlue;
            btnFacturas.HoverBackColor = Color.Navy;
            btnFacturas.HoverForeColor = Color.White;
            btnFacturas.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnFacturas.Location = new Point(9, 153);
            btnFacturas.MinimumSize = new Size(144, 47);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.NormalBackColor = Color.Navy;
            btnFacturas.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnFacturas.PressedBackColor = Color.Navy;
            btnFacturas.PressedForeColor = Color.White;
            btnFacturas.Radius = 20;
            btnFacturas.Size = new Size(215, 47);
            btnFacturas.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnFacturas.TabIndex = 386;
            btnFacturas.Text = "Facturas";
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.Transparent;
            btnClientes.DialogResult = DialogResult.None;
            btnClientes.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClientes.ForeColor = Color.SkyBlue;
            btnClientes.HoverBackColor = Color.Navy;
            btnClientes.HoverForeColor = Color.White;
            btnClientes.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnClientes.Location = new Point(9, 206);
            btnClientes.MinimumSize = new Size(144, 47);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.Navy;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(215, 47);
            btnClientes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnClientes.TabIndex = 384;
            btnClientes.Text = "Clientes";
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.Transparent;
            btnInventario.DialogResult = DialogResult.None;
            btnInventario.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInventario.ForeColor = Color.SkyBlue;
            btnInventario.HoverBackColor = Color.Navy;
            btnInventario.HoverForeColor = Color.White;
            btnInventario.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnInventario.Location = new Point(9, 257);
            btnInventario.MinimumSize = new Size(144, 47);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.Navy;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(215, 47);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 383;
            btnInventario.Text = "Inventario";
            // 
            // btnDeudores
            // 
            btnDeudores.BackColor = Color.Transparent;
            btnDeudores.DialogResult = DialogResult.None;
            btnDeudores.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDeudores.ForeColor = Color.SkyBlue;
            btnDeudores.HoverBackColor = Color.Navy;
            btnDeudores.HoverForeColor = Color.White;
            btnDeudores.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnDeudores.Location = new Point(9, 308);
            btnDeudores.MinimumSize = new Size(144, 47);
            btnDeudores.Name = "btnDeudores";
            btnDeudores.NormalBackColor = Color.Navy;
            btnDeudores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnDeudores.PressedBackColor = Color.Navy;
            btnDeudores.PressedForeColor = Color.White;
            btnDeudores.Radius = 20;
            btnDeudores.Size = new Size(215, 47);
            btnDeudores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnDeudores.TabIndex = 381;
            btnDeudores.Text = "Deudores";
            // 
            // btnMenu
            // 
            btnMenu.BackColor = Color.Transparent;
            btnMenu.DialogResult = DialogResult.None;
            btnMenu.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnMenu.ForeColor = Color.SkyBlue;
            btnMenu.HoverBackColor = Color.Navy;
            btnMenu.HoverForeColor = Color.White;
            btnMenu.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnMenu.Location = new Point(9, 100);
            btnMenu.MinimumSize = new Size(144, 47);
            btnMenu.Name = "btnMenu";
            btnMenu.NormalBackColor = Color.Navy;
            btnMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnMenu.PressedBackColor = Color.Navy;
            btnMenu.PressedForeColor = Color.White;
            btnMenu.Radius = 20;
            btnMenu.Size = new Size(215, 47);
            btnMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnMenu.TabIndex = 387;
            btnMenu.Text = "Menu Principal";
            // 
            // ClientesEmp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1121, 681);
            Controls.Add(label10);
            Controls.Add(kryptonGroupBox2);
            Controls.Add(label7);
            Controls.Add(chkActivo);
            Controls.Add(dgvClientes);
            Controls.Add(BtnModificar);
            Controls.Add(txtBusqueda);
            Controls.Add(kryptonButton14);
            Controls.Add(label1);
            Controls.Add(btnNoti);
            Controls.Add(panel8);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(btnFacturas);
            Controls.Add(btnClientes);
            Controls.Add(btnInventario);
            Controls.Add(btnDeudores);
            Controls.Add(btnMenu);
            FormBorderStyle = FormBorderStyle.None;
            Name = "ClientesEmp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ClientesEmp";
            Load += ClientesEmp_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel3;
        private Panel panel4;
        private Panel panel8;
        private Panel panel5;
        private Panel panel2;
        private Krypton.Toolkit.KryptonCheckBox chkActivo;
        private DataGridView dgvClientes;
        private Krypton.Toolkit.KryptonButton BtnModificar;
        private Krypton.Toolkit.KryptonTextBox txtBusqueda;
        private Krypton.Toolkit.KryptonButton kryptonButton14;
        private Label label1;
        private Button btnNoti;
        private Label label7;
        private Label label10;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Label label2;
        private ReaLTaiizor.Controls.NightButton btnFacturas;
        private ReaLTaiizor.Controls.NightButton btnClientes;
        private ReaLTaiizor.Controls.NightButton btnInventario;
        private ReaLTaiizor.Controls.NightButton btnDeudores;
        private ReaLTaiizor.Controls.NightButton btnMenu;
    }
}