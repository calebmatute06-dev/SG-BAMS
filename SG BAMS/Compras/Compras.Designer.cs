namespace SG_BAMS
{
    partial class Compras
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
            label1 = new Label();
            btnNoti = new Button();
            panel8 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            pictureBox3 = new PictureBox();
            panel4 = new Panel();
            panel2 = new Panel();
            dgvComprasAdmin = new DataGridView();
            label10 = new Label();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            btnCompra = new Krypton.Toolkit.KryptonButton();
            btnModificarC = new Krypton.Toolkit.KryptonButton();
            btnEliminarC = new Krypton.Toolkit.KryptonButton();
            Nombre = new Label();
            txtBuscarCompra = new TextBox();
            kryptonGroup5 = new Krypton.Toolkit.KryptonGroup();
            label3 = new Label();
            label4 = new Label();
            dtpHasta = new DateTimePicker();
            dtpDesde = new DateTimePicker();
            btnRefresh = new Button();
            label6 = new Label();
            btnReportes = new ReaLTaiizor.Controls.NightButton();
            btnPerfil = new ReaLTaiizor.Controls.NightButton();
            btnCerrar = new ReaLTaiizor.Controls.NightButton();
            btnBitacora = new ReaLTaiizor.Controls.NightButton();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnComprasMenu = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnProveedores = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvComprasAdmin).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup5.Panel).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(880, 575);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 60;
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
            btnNoti.Location = new Point(1023, 23);
            btnNoti.Margin = new Padding(3, 2, 3, 2);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(52, 33);
            btnNoti.TabIndex = 54;
            btnNoti.UseVisualStyleBackColor = false;
            btnNoti.Click += btnNoti_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(231, 17);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(21, 664);
            panel8.TabIndex = 51;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(-1, -1);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(21, 682);
            panel5.TabIndex = 50;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(1, 663);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1100, 18);
            panel3.TabIndex = 52;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(-25, 283);
            pictureBox3.Margin = new Padding(3, 2, 3, 2);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(21, 826);
            pictureBox3.TabIndex = 49;
            pictureBox3.TabStop = false;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(1080, 1);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(21, 669);
            panel4.TabIndex = 48;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(1, -1);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1100, 18);
            panel2.TabIndex = 47;
            // 
            // dgvComprasAdmin
            // 
            dgvComprasAdmin.AllowUserToAddRows = false;
            dgvComprasAdmin.AllowUserToDeleteRows = false;
            dgvComprasAdmin.BackgroundColor = Color.SkyBlue;
            dgvComprasAdmin.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvComprasAdmin.Location = new Point(270, 208);
            dgvComprasAdmin.Margin = new Padding(3, 2, 3, 2);
            dgvComprasAdmin.MultiSelect = false;
            dgvComprasAdmin.Name = "dgvComprasAdmin";
            dgvComprasAdmin.RowHeadersWidth = 51;
            dgvComprasAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvComprasAdmin.Size = new Size(792, 362);
            dgvComprasAdmin.TabIndex = 155;
            dgvComprasAdmin.CellContentDoubleClick += dgvComprasAdmin_CellContentDoubleClick;
            dgvComprasAdmin.CellDoubleClick += dgvComprasAdmin_CellDoubleClick_1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.SkyBlue;
            label10.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(588, 31);
            label10.Name = "label10";
            label10.Size = new Size(132, 33);
            label10.TabIndex = 200;
            label10.Text = "Compras";
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.CaptionVisible = false;
            kryptonGroupBox2.Location = new Point(503, 23);
            kryptonGroupBox2.Size = new Size(301, 49);
            kryptonGroupBox2.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox2.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox2.TabIndex = 201;
            // 
            // btnCompra
            // 
            btnCompra.Location = new Point(395, 595);
            btnCompra.Margin = new Padding(3, 2, 3, 2);
            btnCompra.Name = "btnCompra";
            btnCompra.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnCompra.OverrideDefault.Back.Color2 = Color.White;
            btnCompra.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnCompra.OverrideFocus.Back.Color2 = Color.White;
            btnCompra.Size = new Size(156, 41);
            btnCompra.StateCommon.Back.Color1 = Color.SkyBlue;
            btnCompra.StateCommon.Back.Color2 = Color.White;
            btnCompra.StateCommon.Border.Rounding = 40F;
            btnCompra.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnCompra.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnCompra.StateNormal.Back.Color1 = Color.SkyBlue;
            btnCompra.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnCompra.StatePressed.Back.Color1 = Color.Transparent;
            btnCompra.StatePressed.Back.Color2 = Color.Transparent;
            btnCompra.TabIndex = 315;
            btnCompra.Values.DropDownArrowColor = Color.Empty;
            btnCompra.Values.Text = "Comprar";
            btnCompra.Click += btnCompra_Click_1;
            // 
            // btnModificarC
            // 
            btnModificarC.Location = new Point(568, 595);
            btnModificarC.Margin = new Padding(3, 2, 3, 2);
            btnModificarC.Name = "btnModificarC";
            btnModificarC.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificarC.OverrideDefault.Back.Color2 = Color.White;
            btnModificarC.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificarC.OverrideFocus.Back.Color2 = Color.White;
            btnModificarC.Size = new Size(156, 41);
            btnModificarC.StateCommon.Back.Color1 = Color.SkyBlue;
            btnModificarC.StateCommon.Back.Color2 = Color.White;
            btnModificarC.StateCommon.Border.Rounding = 40F;
            btnModificarC.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnModificarC.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnModificarC.StateNormal.Back.Color1 = Color.SkyBlue;
            btnModificarC.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnModificarC.StatePressed.Back.Color1 = Color.Transparent;
            btnModificarC.StatePressed.Back.Color2 = Color.Transparent;
            btnModificarC.TabIndex = 316;
            btnModificarC.Values.DropDownArrowColor = Color.Empty;
            btnModificarC.Values.Text = "Modificar";
            btnModificarC.Click += btnModificarC_Click;
            // 
            // btnEliminarC
            // 
            btnEliminarC.Location = new Point(747, 595);
            btnEliminarC.Margin = new Padding(3, 2, 3, 2);
            btnEliminarC.Name = "btnEliminarC";
            btnEliminarC.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEliminarC.OverrideDefault.Back.Color2 = Color.White;
            btnEliminarC.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEliminarC.OverrideFocus.Back.Color2 = Color.White;
            btnEliminarC.Size = new Size(156, 41);
            btnEliminarC.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEliminarC.StateCommon.Back.Color2 = Color.White;
            btnEliminarC.StateCommon.Border.Rounding = 40F;
            btnEliminarC.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEliminarC.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnEliminarC.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEliminarC.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnEliminarC.StatePressed.Back.Color1 = Color.Transparent;
            btnEliminarC.StatePressed.Back.Color2 = Color.Transparent;
            btnEliminarC.TabIndex = 317;
            btnEliminarC.Values.DropDownArrowColor = Color.Empty;
            btnEliminarC.Values.Text = "Eliminar";
            btnEliminarC.Click += btnEliminarC_Click;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(270, 164);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(84, 22);
            Nombre.TabIndex = 356;
            Nombre.Text = "Buscar:";
            // 
            // txtBuscarCompra
            // 
            txtBuscarCompra.BackColor = Color.SkyBlue;
            txtBuscarCompra.BorderStyle = BorderStyle.None;
            txtBuscarCompra.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBuscarCompra.ForeColor = Color.Navy;
            txtBuscarCompra.Location = new Point(382, 160);
            txtBuscarCompra.Margin = new Padding(3, 2, 3, 2);
            txtBuscarCompra.Name = "txtBuscarCompra";
            txtBuscarCompra.Size = new Size(278, 19);
            txtBuscarCompra.TabIndex = 355;
            // 
            // kryptonGroup5
            // 
            kryptonGroup5.Location = new Point(360, 154);
            kryptonGroup5.Margin = new Padding(3, 2, 3, 2);
            kryptonGroup5.Size = new Size(323, 39);
            kryptonGroup5.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroup5.StateCommon.Border.Rounding = 70F;
            kryptonGroup5.TabIndex = 354;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(696, 172);
            label3.Name = "label3";
            label3.Size = new Size(54, 23);
            label3.TabIndex = 360;
            label3.Text = "Hasta:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(694, 146);
            label4.Name = "label4";
            label4.Size = new Size(60, 23);
            label4.TabIndex = 361;
            label4.Text = "Desde:";
            // 
            // dtpHasta
            // 
            dtpHasta.Location = new Point(762, 172);
            dtpHasta.Margin = new Padding(3, 2, 3, 2);
            dtpHasta.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpHasta.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpHasta.Name = "dtpHasta";
            dtpHasta.Size = new Size(236, 23);
            dtpHasta.TabIndex = 359;
            // 
            // dtpDesde
            // 
            dtpDesde.Location = new Point(762, 147);
            dtpDesde.Margin = new Padding(3, 2, 3, 2);
            dtpDesde.MaxDate = new DateTime(2100, 12, 31, 0, 0, 0, 0);
            dtpDesde.MinDate = new DateTime(2025, 1, 1, 0, 0, 0, 0);
            dtpDesde.Name = "dtpDesde";
            dtpDesde.Size = new Size(236, 23);
            dtpDesde.TabIndex = 358;
            dtpDesde.Value = new DateTime(2026, 1, 1, 0, 0, 0, 0);
            // 
            // btnRefresh
            // 
            btnRefresh.BackgroundImage = Properties.Resources.refresh;
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;
            btnRefresh.Location = new Point(1004, 146);
            btnRefresh.Margin = new Padding(3, 2, 3, 2);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(58, 50);
            btnRefresh.TabIndex = 362;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click_1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(64, 36);
            label6.Name = "label6";
            label6.Size = new Size(117, 41);
            label6.TabIndex = 363;
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
            btnReportes.Location = new Point(9, 456);
            btnReportes.MinimumSize = new Size(144, 47);
            btnReportes.Name = "btnReportes";
            btnReportes.NormalBackColor = Color.Navy;
            btnReportes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnReportes.PressedBackColor = Color.Navy;
            btnReportes.PressedForeColor = Color.White;
            btnReportes.Radius = 20;
            btnReportes.Size = new Size(215, 47);
            btnReportes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnReportes.TabIndex = 364;
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
            btnPerfil.Location = new Point(9, 608);
            btnPerfil.MinimumSize = new Size(144, 47);
            btnPerfil.Name = "btnPerfil";
            btnPerfil.NormalBackColor = Color.Navy;
            btnPerfil.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnPerfil.PressedBackColor = Color.Navy;
            btnPerfil.PressedForeColor = Color.White;
            btnPerfil.Radius = 20;
            btnPerfil.Size = new Size(215, 47);
            btnPerfil.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnPerfil.TabIndex = 373;
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
            btnCerrar.Location = new Point(9, 558);
            btnCerrar.MinimumSize = new Size(144, 47);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.NormalBackColor = Color.Navy;
            btnCerrar.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCerrar.PressedBackColor = Color.Navy;
            btnCerrar.PressedForeColor = Color.White;
            btnCerrar.Radius = 20;
            btnCerrar.Size = new Size(215, 47);
            btnCerrar.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCerrar.TabIndex = 372;
            btnCerrar.Text = "Cerrar Sesión";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // btnBitacora
            // 
            btnBitacora.BackColor = Color.Transparent;
            btnBitacora.DialogResult = DialogResult.None;
            btnBitacora.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBitacora.ForeColor = Color.SkyBlue;
            btnBitacora.HoverBackColor = Color.Navy;
            btnBitacora.HoverForeColor = Color.White;
            btnBitacora.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnBitacora.Location = new Point(9, 507);
            btnBitacora.MinimumSize = new Size(144, 47);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.NormalBackColor = Color.Navy;
            btnBitacora.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnBitacora.PressedBackColor = Color.Navy;
            btnBitacora.PressedForeColor = Color.White;
            btnBitacora.Radius = 20;
            btnBitacora.Size = new Size(215, 47);
            btnBitacora.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnBitacora.TabIndex = 371;
            btnBitacora.Text = "Bitácora";
            btnBitacora.Click += btnBitacora_Click;
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
            btnFacturas.Location = new Point(9, 151);
            btnFacturas.MinimumSize = new Size(144, 47);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.NormalBackColor = Color.Navy;
            btnFacturas.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnFacturas.PressedBackColor = Color.Navy;
            btnFacturas.PressedForeColor = Color.White;
            btnFacturas.Radius = 20;
            btnFacturas.Size = new Size(215, 47);
            btnFacturas.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnFacturas.TabIndex = 370;
            btnFacturas.Text = "Facturas";
            btnFacturas.Click += btnFacturas_Click;
            // 
            // btnComprasMenu
            // 
            btnComprasMenu.BackColor = Color.Transparent;
            btnComprasMenu.DialogResult = DialogResult.None;
            btnComprasMenu.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnComprasMenu.ForeColor = Color.SkyBlue;
            btnComprasMenu.HoverBackColor = Color.Navy;
            btnComprasMenu.HoverForeColor = Color.White;
            btnComprasMenu.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnComprasMenu.Location = new Point(9, 201);
            btnComprasMenu.MinimumSize = new Size(144, 47);
            btnComprasMenu.Name = "btnComprasMenu";
            btnComprasMenu.NormalBackColor = Color.Navy;
            btnComprasMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnComprasMenu.PressedBackColor = Color.Navy;
            btnComprasMenu.PressedForeColor = Color.White;
            btnComprasMenu.Radius = 20;
            btnComprasMenu.Size = new Size(215, 47);
            btnComprasMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnComprasMenu.TabIndex = 369;
            btnComprasMenu.Text = "Compras";
            btnComprasMenu.Click += btnComprasMenu_Click;
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
            btnClientes.Location = new Point(9, 251);
            btnClientes.MinimumSize = new Size(144, 47);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.Navy;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(215, 47);
            btnClientes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnClientes.TabIndex = 368;
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
            btnInventario.Location = new Point(9, 302);
            btnInventario.MinimumSize = new Size(144, 47);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.Navy;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(215, 47);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 367;
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
            btnProveedores.Location = new Point(9, 353);
            btnProveedores.MinimumSize = new Size(144, 47);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.NormalBackColor = Color.Navy;
            btnProveedores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnProveedores.PressedBackColor = Color.Navy;
            btnProveedores.PressedForeColor = Color.White;
            btnProveedores.Radius = 20;
            btnProveedores.Size = new Size(215, 47);
            btnProveedores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnProveedores.TabIndex = 366;
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
            btnDeudores.Location = new Point(9, 405);
            btnDeudores.MinimumSize = new Size(144, 47);
            btnDeudores.Name = "btnDeudores";
            btnDeudores.NormalBackColor = Color.Navy;
            btnDeudores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnDeudores.PressedBackColor = Color.Navy;
            btnDeudores.PressedForeColor = Color.White;
            btnDeudores.Radius = 20;
            btnDeudores.Size = new Size(215, 47);
            btnDeudores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnDeudores.TabIndex = 365;
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
            btnMenu.Location = new Point(9, 98);
            btnMenu.MinimumSize = new Size(144, 47);
            btnMenu.Name = "btnMenu";
            btnMenu.NormalBackColor = Color.Navy;
            btnMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnMenu.PressedBackColor = Color.Navy;
            btnMenu.PressedForeColor = Color.White;
            btnMenu.Radius = 20;
            btnMenu.Size = new Size(215, 47);
            btnMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnMenu.TabIndex = 374;
            btnMenu.Text = "Menu Principal";
            btnMenu.Click += btnMenu_Click;
            // 
            // Compras
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1100, 681);
            Controls.Add(btnRefresh);
            Controls.Add(label3);
            Controls.Add(label4);
            Controls.Add(dtpHasta);
            Controls.Add(dtpDesde);
            Controls.Add(Nombre);
            Controls.Add(txtBuscarCompra);
            Controls.Add(kryptonGroup5);
            Controls.Add(btnEliminarC);
            Controls.Add(btnModificarC);
            Controls.Add(btnCompra);
            Controls.Add(label10);
            Controls.Add(kryptonGroupBox2);
            Controls.Add(dgvComprasAdmin);
            Controls.Add(label1);
            Controls.Add(btnNoti);
            Controls.Add(panel8);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(pictureBox3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(label6);
            Controls.Add(btnReportes);
            Controls.Add(btnPerfil);
            Controls.Add(btnCerrar);
            Controls.Add(btnBitacora);
            Controls.Add(btnFacturas);
            Controls.Add(btnComprasMenu);
            Controls.Add(btnClientes);
            Controls.Add(btnInventario);
            Controls.Add(btnProveedores);
            Controls.Add(btnDeudores);
            Controls.Add(btnMenu);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 2, 3, 2);
            Name = "Compras";
            Text = "Compras";
            Load += Compras_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvComprasAdmin).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup5.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroup5).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label label1;
        private Button btnNoti;
        private Panel panel8;
        private Panel panel5;
        private Panel panel3;
        private PictureBox pictureBox3;
        private Panel panel4;
        private Panel panel2;
        private Krypton.Toolkit.KryptonButton btnCompra;
        private DataGridView dgvComprasAdmin;
        private Label label10;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonButton btnModificarC;
        private Krypton.Toolkit.KryptonButton btnEliminarC;
        private Label Nombre;
        private TextBox txtBuscarCompra;
        private Krypton.Toolkit.KryptonGroup kryptonGroup5;
        private Label label3;
        private Label label4;
        private DateTimePicker dtpHasta;
        private DateTimePicker dtpDesde;
        private Button btnRefresh;
        private Label label6;
        private ReaLTaiizor.Controls.NightButton btnReportes;
        private ReaLTaiizor.Controls.NightButton btnPerfil;
        private ReaLTaiizor.Controls.NightButton btnCerrar;
        private ReaLTaiizor.Controls.NightButton btnBitacora;
        private ReaLTaiizor.Controls.NightButton btnFacturas;
        private ReaLTaiizor.Controls.NightButton btnComprasMenu;
        private ReaLTaiizor.Controls.NightButton btnClientes;
        private ReaLTaiizor.Controls.NightButton btnInventario;
        private ReaLTaiizor.Controls.NightButton btnProveedores;
        private ReaLTaiizor.Controls.NightButton btnDeudores;
        private ReaLTaiizor.Controls.NightButton btnMenu;
        //private Krypton.Toolkit.KryptonButton btnComprar;
    }
}