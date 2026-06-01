namespace SG_BAMS.Bitacora
{
    partial class BitacoraAdmin
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
            label10 = new Label();
            kryptonButton14 = new Krypton.Toolkit.KryptonButton();
            label1 = new Label();
            btnNoti = new Button();
            panel8 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            btnRefresh = new Button();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            label4 = new Label();
            label3 = new Label();
            btnExportar = new Krypton.Toolkit.KryptonButton();
            dgvBitacora = new DataGridView();
            label5 = new Label();
            label6 = new Label();
            btnReportes = new ReaLTaiizor.Controls.NightButton();
            btnPerfil = new ReaLTaiizor.Controls.NightButton();
            btnCerrar = new ReaLTaiizor.Controls.NightButton();
            btnBitacora = new ReaLTaiizor.Controls.NightButton();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnCompra = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnProveedores = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            txtBuscar = new Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).BeginInit();
            SuspendLayout();
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(659, 49);
            label10.Name = "label10";
            label10.Size = new Size(269, 70);
            label10.TabIndex = 90;
            label10.Text = "Bitácora";
            // 
            // kryptonButton14
            // 
            kryptonButton14.Location = new Point(328, 279);
            kryptonButton14.Name = "kryptonButton14";
            kryptonButton14.Size = new Size(0, 0);
            kryptonButton14.StateCommon.Border.Rounding = 100F;
            kryptonButton14.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton14.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton14.TabIndex = 79;
            kryptonButton14.Values.DropDownArrowColor = Color.Empty;
            kryptonButton14.Values.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(1110, 763);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 76;
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
            btnNoti.Location = new Point(1205, 32);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(59, 44);
            btnNoti.TabIndex = 71;
            btnNoti.UseVisualStyleBackColor = false;
            btnNoti.Click += btnNoti_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(270, 23);
            panel8.Name = "panel8";
            panel8.Size = new Size(24, 869);
            panel8.TabIndex = 68;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(0, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(25, 923);
            panel5.TabIndex = 67;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(1, 884);
            panel3.Name = "panel3";
            panel3.Size = new Size(1295, 24);
            panel3.TabIndex = 69;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(1273, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(23, 899);
            panel4.TabIndex = 65;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(7, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1285, 24);
            panel2.TabIndex = 64;
            // 
            // btnRefresh
            // 
            btnRefresh.BackgroundImage = Properties.Resources.refresh;
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;
            btnRefresh.Location = new Point(1197, 137);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(66, 67);
            btnRefresh.TabIndex = 323;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(922, 174);
            dtpHasta.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpHasta.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(269, 27);
            dtpHasta.TabIndex = 327;
            dtpHasta.ValueChanged += dtpHasta_ValueChanged;
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(922, 141);
            dtpDesde.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpDesde.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(269, 27);
            dtpDesde.TabIndex = 326;
            dtpDesde.Value = new DateTime(2026, 1, 1, 0, 0, 0, 0);
            dtpDesde.ValueChanged += dtpDesde_ValueChanged;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(844, 140);
            label4.Name = "label4";
            label4.Size = new Size(75, 29);
            label4.TabIndex = 328;
            label4.Text = "Desde:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(846, 174);
            label3.Name = "label3";
            label3.Size = new Size(69, 29);
            label3.TabIndex = 328;
            label3.Text = "Hasta:";
            // 
            // btnExportar
            // 
            btnExportar.Location = new Point(726, 792);
            btnExportar.Name = "btnExportar";
            btnExportar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnExportar.OverrideDefault.Back.Color2 = Color.White;
            btnExportar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnExportar.OverrideFocus.Back.Color2 = Color.White;
            btnExportar.Size = new Size(138, 69);
            btnExportar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnExportar.StateCommon.Back.Color2 = Color.White;
            btnExportar.StateCommon.Border.Rounding = 5F;
            btnExportar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnExportar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExportar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnExportar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnExportar.StatePressed.Back.Color1 = Color.Transparent;
            btnExportar.StatePressed.Back.Color2 = Color.Transparent;
            btnExportar.TabIndex = 329;
            btnExportar.Values.DropDownArrowColor = Color.Empty;
            btnExportar.Values.Text = "PDF";
            btnExportar.Click += btnExportar_Click;
            // 
            // dgvBitacora
            // 
            dgvBitacora.AccessibleDescription = "";
            dataGridViewCellStyle1.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle1.ForeColor = Color.Navy;
            dgvBitacora.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dgvBitacora.BackgroundColor = Color.SkyBlue;
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = SystemColors.Control;
            dataGridViewCellStyle2.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle2.ForeColor = Color.Navy;
            dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = DataGridViewTriState.True;
            dgvBitacora.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            dgvBitacora.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = SystemColors.Window;
            dataGridViewCellStyle3.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle3.ForeColor = Color.SkyBlue;
            dataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = DataGridViewTriState.False;
            dgvBitacora.DefaultCellStyle = dataGridViewCellStyle3;
            dgvBitacora.Location = new Point(496, 220);
            dgvBitacora.Name = "dgvBitacora";
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = SystemColors.Control;
            dataGridViewCellStyle4.Font = new Font("Arial", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dataGridViewCellStyle4.ForeColor = Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = DataGridViewTriState.True;
            dgvBitacora.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            dgvBitacora.RowHeadersWidth = 51;
            dataGridViewCellStyle5.Font = new Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            dataGridViewCellStyle5.ForeColor = Color.Navy;
            dgvBitacora.RowsDefaultCellStyle = dataGridViewCellStyle5;
            dgvBitacora.Size = new Size(579, 546);
            dgvBitacora.TabIndex = 330;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.White;
            label5.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(320, 156);
            label5.Name = "label5";
            label5.Size = new Size(88, 31);
            label5.TabIndex = 331;
            label5.Text = "Buscar:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(80, 49);
            label6.Name = "label6";
            label6.Size = new Size(151, 51);
            label6.TabIndex = 332;
            label6.Text = "BAMS";
            // 
            // btnReportes
            // 
            btnReportes.BackColor = Color.Transparent;
            btnReportes.DialogResult = DialogResult.None;
            btnReportes.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnReportes.ForeColor = Color.SkyBlue;
            btnReportes.HoverBackColor = Color.Navy;
            btnReportes.HoverForeColor = Color.White;
            btnReportes.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnReportes.Location = new Point(9, 607);
            btnReportes.Margin = new Padding(3, 4, 3, 4);
            btnReportes.MinimumSize = new Size(165, 63);
            btnReportes.Name = "btnReportes";
            btnReportes.NormalBackColor = Color.Navy;
            btnReportes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnReportes.PressedBackColor = Color.Navy;
            btnReportes.PressedForeColor = Color.White;
            btnReportes.Radius = 20;
            btnReportes.Size = new Size(246, 63);
            btnReportes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnReportes.TabIndex = 333;
            btnReportes.Text = "Reportes";
            btnReportes.Click += btnReportes_Click;
            // 
            // btnPerfil
            // 
            btnPerfil.BackColor = Color.Transparent;
            btnPerfil.DialogResult = DialogResult.None;
            btnPerfil.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnPerfil.ForeColor = Color.SkyBlue;
            btnPerfil.HoverBackColor = Color.Navy;
            btnPerfil.HoverForeColor = Color.White;
            btnPerfil.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnPerfil.Location = new Point(9, 809);
            btnPerfil.Margin = new Padding(3, 4, 3, 4);
            btnPerfil.MinimumSize = new Size(165, 63);
            btnPerfil.Name = "btnPerfil";
            btnPerfil.NormalBackColor = Color.Navy;
            btnPerfil.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnPerfil.PressedBackColor = Color.Navy;
            btnPerfil.PressedForeColor = Color.White;
            btnPerfil.Radius = 20;
            btnPerfil.Size = new Size(246, 63);
            btnPerfil.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnPerfil.TabIndex = 342;
            btnPerfil.Text = "Perfil";
            btnPerfil.Click += btnPerfil_Click;
            // 
            // btnCerrar
            // 
            btnCerrar.BackColor = Color.Transparent;
            btnCerrar.DialogResult = DialogResult.None;
            btnCerrar.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCerrar.ForeColor = Color.SkyBlue;
            btnCerrar.HoverBackColor = Color.Navy;
            btnCerrar.HoverForeColor = Color.White;
            btnCerrar.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnCerrar.Location = new Point(9, 743);
            btnCerrar.Margin = new Padding(3, 4, 3, 4);
            btnCerrar.MinimumSize = new Size(165, 63);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.NormalBackColor = Color.Navy;
            btnCerrar.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCerrar.PressedBackColor = Color.Navy;
            btnCerrar.PressedForeColor = Color.White;
            btnCerrar.Radius = 20;
            btnCerrar.Size = new Size(246, 63);
            btnCerrar.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCerrar.TabIndex = 341;
            btnCerrar.Text = "Cerrar Sesión";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnBitacora
            // 
            btnBitacora.BackColor = Color.SkyBlue;
            btnBitacora.DialogResult = DialogResult.None;
            btnBitacora.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBitacora.ForeColor = Color.White;
            btnBitacora.HoverBackColor = Color.SkyBlue;
            btnBitacora.HoverForeColor = Color.White;
            btnBitacora.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnBitacora.Location = new Point(9, 675);
            btnBitacora.Margin = new Padding(3, 4, 3, 4);
            btnBitacora.MinimumSize = new Size(165, 63);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.NormalBackColor = Color.SkyBlue;
            btnBitacora.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnBitacora.PressedBackColor = Color.Navy;
            btnBitacora.PressedForeColor = Color.White;
            btnBitacora.Radius = 20;
            btnBitacora.Size = new Size(246, 63);
            btnBitacora.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnBitacora.TabIndex = 340;
            btnBitacora.Text = "Bitácora";
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
            btnFacturas.Location = new Point(9, 200);
            btnFacturas.Margin = new Padding(3, 4, 3, 4);
            btnFacturas.MinimumSize = new Size(165, 63);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.NormalBackColor = Color.Navy;
            btnFacturas.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnFacturas.PressedBackColor = Color.Navy;
            btnFacturas.PressedForeColor = Color.White;
            btnFacturas.Radius = 20;
            btnFacturas.Size = new Size(246, 63);
            btnFacturas.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnFacturas.TabIndex = 339;
            btnFacturas.Text = "Facturas";
            btnFacturas.Click += btnFacturas_Click;
            // 
            // btnCompra
            // 
            btnCompra.BackColor = Color.Transparent;
            btnCompra.DialogResult = DialogResult.None;
            btnCompra.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCompra.ForeColor = Color.SkyBlue;
            btnCompra.HoverBackColor = Color.Navy;
            btnCompra.HoverForeColor = Color.White;
            btnCompra.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnCompra.Location = new Point(9, 267);
            btnCompra.Margin = new Padding(3, 4, 3, 4);
            btnCompra.MinimumSize = new Size(165, 63);
            btnCompra.Name = "btnCompra";
            btnCompra.NormalBackColor = Color.Navy;
            btnCompra.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCompra.PressedBackColor = Color.Navy;
            btnCompra.PressedForeColor = Color.White;
            btnCompra.Radius = 20;
            btnCompra.Size = new Size(246, 63);
            btnCompra.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCompra.TabIndex = 338;
            btnCompra.Text = "Compras";
            btnCompra.Click += btnCompra_Click;
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
            btnClientes.Location = new Point(9, 333);
            btnClientes.Margin = new Padding(3, 4, 3, 4);
            btnClientes.MinimumSize = new Size(165, 63);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.Navy;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(246, 63);
            btnClientes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnClientes.TabIndex = 337;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
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
            btnInventario.Location = new Point(9, 401);
            btnInventario.Margin = new Padding(3, 4, 3, 4);
            btnInventario.MinimumSize = new Size(165, 63);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.Navy;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(246, 63);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 336;
            btnInventario.Text = "Inventario";
            btnInventario.Click += btnInventario_Click;
            // 
            // btnProveedores
            // 
            btnProveedores.BackColor = Color.Transparent;
            btnProveedores.DialogResult = DialogResult.None;
            btnProveedores.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProveedores.ForeColor = Color.SkyBlue;
            btnProveedores.HoverBackColor = Color.Navy;
            btnProveedores.HoverForeColor = Color.White;
            btnProveedores.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnProveedores.Location = new Point(9, 469);
            btnProveedores.Margin = new Padding(3, 4, 3, 4);
            btnProveedores.MinimumSize = new Size(165, 63);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.NormalBackColor = Color.Navy;
            btnProveedores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnProveedores.PressedBackColor = Color.Navy;
            btnProveedores.PressedForeColor = Color.White;
            btnProveedores.Radius = 20;
            btnProveedores.Size = new Size(246, 63);
            btnProveedores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnProveedores.TabIndex = 335;
            btnProveedores.Text = "Proveedores";
            btnProveedores.Click += btnProveedores_Click;
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
            btnDeudores.Location = new Point(9, 539);
            btnDeudores.Margin = new Padding(3, 4, 3, 4);
            btnDeudores.MinimumSize = new Size(165, 63);
            btnDeudores.Name = "btnDeudores";
            btnDeudores.NormalBackColor = Color.Navy;
            btnDeudores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnDeudores.PressedBackColor = Color.Navy;
            btnDeudores.PressedForeColor = Color.White;
            btnDeudores.Radius = 20;
            btnDeudores.Size = new Size(246, 63);
            btnDeudores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnDeudores.TabIndex = 334;
            btnDeudores.Text = "Deudores";
            btnDeudores.Click += btnDeudores_Click;
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
            btnMenu.Location = new Point(9, 129);
            btnMenu.Margin = new Padding(3, 4, 3, 4);
            btnMenu.MinimumSize = new Size(165, 63);
            btnMenu.Name = "btnMenu";
            btnMenu.NormalBackColor = Color.Navy;
            btnMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnMenu.PressedBackColor = Color.Navy;
            btnMenu.PressedForeColor = Color.White;
            btnMenu.Radius = 20;
            btnMenu.Size = new Size(246, 63);
            btnMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnMenu.TabIndex = 343;
            btnMenu.Text = "Menu Principal";
            btnMenu.Click += btnMenu_Click;
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(404, 155);
            txtBuscar.Margin = new Padding(3, 4, 3, 4);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(417, 34);
            txtBuscar.StateCommon.Back.Color1 = Color.White;
            txtBuscar.StateCommon.Border.Color1 = Color.Navy;
            txtBuscar.StateCommon.Border.Rounding = 5F;
            txtBuscar.StateCommon.Content.Color1 = Color.Black;
            txtBuscar.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscar.TabIndex = 344;
            // 
            // BitacoraAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1294, 908);
            Controls.Add(txtBuscar);
            Controls.Add(label5);
            Controls.Add(dgvBitacora);
            Controls.Add(btnExportar);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Controls.Add(btnRefresh);
            Controls.Add(label10);
            Controls.Add(kryptonButton14);
            Controls.Add(label1);
            Controls.Add(btnNoti);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(panel8);
            Controls.Add(label6);
            Controls.Add(btnReportes);
            Controls.Add(btnPerfil);
            Controls.Add(btnCerrar);
            Controls.Add(btnBitacora);
            Controls.Add(btnFacturas);
            Controls.Add(btnCompra);
            Controls.Add(btnClientes);
            Controls.Add(btnInventario);
            Controls.Add(btnProveedores);
            Controls.Add(btnDeudores);
            Controls.Add(btnMenu);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "BitacoraAdmin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bitacora";
            Load += Bitacora_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBitacora).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label10;
        private Krypton.Toolkit.KryptonButton kryptonButton14;
        private Label label1;
        private Button btnNoti;
        private Panel panel8;
        private Panel panel5;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
        private Krypton.Toolkit.KryptonDataGridView dgvBitacora1;
        private Button btnRefresh;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
        private Label label4;
        private Label label3;
        private Krypton.Toolkit.KryptonButton btnExportar;
        private DataGridView dgvBitacora;
        private Label label5;
        private Label label6;
        private ReaLTaiizor.Controls.NightButton btnReportes;
        private ReaLTaiizor.Controls.NightButton btnPerfil;
        private ReaLTaiizor.Controls.NightButton btnCerrar;
        private ReaLTaiizor.Controls.NightButton btnBitacora;
        private ReaLTaiizor.Controls.NightButton btnFacturas;
        private ReaLTaiizor.Controls.NightButton btnCompra;
        private ReaLTaiizor.Controls.NightButton btnClientes;
        private ReaLTaiizor.Controls.NightButton btnInventario;
        private ReaLTaiizor.Controls.NightButton btnProveedores;
        private ReaLTaiizor.Controls.NightButton btnDeudores;
        private ReaLTaiizor.Controls.NightButton btnMenu;
        private Krypton.Toolkit.KryptonTextBox txtBuscar;
    }
}