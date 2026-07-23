namespace SG_BAMS.Proveedor
{
    partial class ProveedoresAdmin
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
            txtBuscar = new Krypton.Toolkit.KryptonTextBox();
            btnNoti = new Button();
            panel8 = new Panel();
            panel5 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            btnRefresh = new Button();
            label10 = new Label();
            dgvProveedor = new DataGridView();
            Nombre = new Label();
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
            btnAgregar1 = new Krypton.Toolkit.KryptonButton();
            btnModificar = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).BeginInit();
            SuspendLayout();
            // 
            // txtBuscar
            // 
            txtBuscar.Location = new Point(388, 116);
            txtBuscar.Margin = new Padding(3, 2, 3, 2);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(500, 27);
            txtBuscar.StateCommon.Back.Color1 = Color.White;
            txtBuscar.StateCommon.Border.Color1 = Color.Navy;
            txtBuscar.StateCommon.Border.Rounding = 5F;
            txtBuscar.StateCommon.Content.Color1 = Color.Gray;
            txtBuscar.TabIndex = 113;
            txtBuscar.KeyUp += txtBuscar_KeyUp;
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
            btnNoti.Location = new Point(940, 22);
            btnNoti.Margin = new Padding(3, 2, 3, 2);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(52, 33);
            btnNoti.TabIndex = 109;
            btnNoti.UseVisualStyleBackColor = false;
            btnNoti.Click += btnNoti_Click;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(234, 16);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(21, 661);
            panel8.TabIndex = 106;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(-1, -1);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(21, 678);
            panel5.TabIndex = 105;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(4, 662);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1014, 18);
            panel3.TabIndex = 107;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(997, 16);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(21, 652);
            panel4.TabIndex = 104;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(3, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1015, 18);
            panel2.TabIndex = 117;
            // 
            // btnRefresh
            // 
            btnRefresh.BackgroundImage = Properties.Resources.refresh;
            btnRefresh.BackgroundImageLayout = ImageLayout.Stretch;
            btnRefresh.Location = new Point(894, 104);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(48, 38);
            btnRefresh.TabIndex = 324;
            btnRefresh.UseVisualStyleBackColor = true;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(474, 38);
            label10.Name = "label10";
            label10.Size = new Size(316, 56);
            label10.TabIndex = 329;
            label10.Text = "Proveedores";
            // 
            // dgvProveedor
            // 
            dgvProveedor.BackgroundColor = Color.SkyBlue;
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvProveedor.Location = new Point(288, 157);
            dgvProveedor.Name = "dgvProveedor";
            dgvProveedor.RowHeadersWidth = 51;
            dgvProveedor.Size = new Size(682, 432);
            dgvProveedor.TabIndex = 331;
            dgvProveedor.CellDoubleClick += dgvProveedor_CellDoubleClick_1;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(298, 116);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(84, 22);
            Nombre.TabIndex = 354;
            Nombre.Text = "Buscar:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(69, 38);
            label6.Name = "label6";
            label6.Size = new Size(117, 41);
            label6.TabIndex = 368;
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
            btnReportes.Location = new Point(11, 458);
            btnReportes.MinimumSize = new Size(144, 47);
            btnReportes.Name = "btnReportes";
            btnReportes.NormalBackColor = Color.Navy;
            btnReportes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnReportes.PressedBackColor = Color.Navy;
            btnReportes.PressedForeColor = Color.White;
            btnReportes.Radius = 20;
            btnReportes.Size = new Size(215, 47);
            btnReportes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnReportes.TabIndex = 369;
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
            btnPerfil.Location = new Point(11, 610);
            btnPerfil.MinimumSize = new Size(144, 47);
            btnPerfil.Name = "btnPerfil";
            btnPerfil.NormalBackColor = Color.Navy;
            btnPerfil.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnPerfil.PressedBackColor = Color.Navy;
            btnPerfil.PressedForeColor = Color.White;
            btnPerfil.Radius = 20;
            btnPerfil.Size = new Size(215, 47);
            btnPerfil.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnPerfil.TabIndex = 378;
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
            btnCerrar.Location = new Point(11, 560);
            btnCerrar.MinimumSize = new Size(144, 47);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.NormalBackColor = Color.Navy;
            btnCerrar.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCerrar.PressedBackColor = Color.Navy;
            btnCerrar.PressedForeColor = Color.White;
            btnCerrar.Radius = 20;
            btnCerrar.Size = new Size(215, 47);
            btnCerrar.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCerrar.TabIndex = 377;
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
            btnBitacora.Location = new Point(11, 509);
            btnBitacora.MinimumSize = new Size(144, 47);
            btnBitacora.Name = "btnBitacora";
            btnBitacora.NormalBackColor = Color.Navy;
            btnBitacora.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnBitacora.PressedBackColor = Color.Navy;
            btnBitacora.PressedForeColor = Color.White;
            btnBitacora.Radius = 20;
            btnBitacora.Size = new Size(215, 47);
            btnBitacora.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnBitacora.TabIndex = 376;
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
            btnFacturas.Location = new Point(11, 153);
            btnFacturas.MinimumSize = new Size(144, 47);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.NormalBackColor = Color.Navy;
            btnFacturas.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnFacturas.PressedBackColor = Color.Navy;
            btnFacturas.PressedForeColor = Color.White;
            btnFacturas.Radius = 20;
            btnFacturas.Size = new Size(215, 47);
            btnFacturas.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnFacturas.TabIndex = 375;
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
            btnCompra.Location = new Point(11, 203);
            btnCompra.MinimumSize = new Size(144, 47);
            btnCompra.Name = "btnCompra";
            btnCompra.NormalBackColor = Color.Navy;
            btnCompra.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCompra.PressedBackColor = Color.Navy;
            btnCompra.PressedForeColor = Color.White;
            btnCompra.Radius = 20;
            btnCompra.Size = new Size(215, 47);
            btnCompra.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCompra.TabIndex = 374;
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
            btnClientes.Location = new Point(11, 253);
            btnClientes.MinimumSize = new Size(144, 47);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.Navy;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(215, 47);
            btnClientes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnClientes.TabIndex = 373;
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
            btnInventario.Location = new Point(11, 304);
            btnInventario.MinimumSize = new Size(144, 47);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.Navy;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(215, 47);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 372;
            btnInventario.Text = "Inventario";
            btnInventario.Click += btnInventario_Click;
            // 
            // btnProveedores
            // 
            btnProveedores.BackColor = Color.SkyBlue;
            btnProveedores.DialogResult = DialogResult.None;
            btnProveedores.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnProveedores.ForeColor = Color.White;
            btnProveedores.HoverBackColor = Color.SkyBlue;
            btnProveedores.HoverForeColor = Color.White;
            btnProveedores.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnProveedores.Location = new Point(11, 355);
            btnProveedores.MinimumSize = new Size(144, 47);
            btnProveedores.Name = "btnProveedores";
            btnProveedores.NormalBackColor = Color.SkyBlue;
            btnProveedores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnProveedores.PressedBackColor = Color.Navy;
            btnProveedores.PressedForeColor = Color.White;
            btnProveedores.Radius = 20;
            btnProveedores.Size = new Size(215, 47);
            btnProveedores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnProveedores.TabIndex = 371;
            btnProveedores.Text = "Proveedores";
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
            btnDeudores.Location = new Point(11, 407);
            btnDeudores.MinimumSize = new Size(144, 47);
            btnDeudores.Name = "btnDeudores";
            btnDeudores.NormalBackColor = Color.Navy;
            btnDeudores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnDeudores.PressedBackColor = Color.Navy;
            btnDeudores.PressedForeColor = Color.White;
            btnDeudores.Radius = 20;
            btnDeudores.Size = new Size(215, 47);
            btnDeudores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnDeudores.TabIndex = 370;
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
            btnMenu.Location = new Point(11, 100);
            btnMenu.MinimumSize = new Size(144, 47);
            btnMenu.Name = "btnMenu";
            btnMenu.NormalBackColor = Color.Navy;
            btnMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnMenu.PressedBackColor = Color.Navy;
            btnMenu.PressedForeColor = Color.White;
            btnMenu.Radius = 20;
            btnMenu.Size = new Size(215, 47);
            btnMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnMenu.TabIndex = 379;
            btnMenu.Text = "Menu Principal";
            btnMenu.Click += btnMenu_Click;
            // 
            // btnAgregar1
            // 
            btnAgregar1.Location = new Point(493, 604);
            btnAgregar1.Margin = new Padding(3, 2, 3, 2);
            btnAgregar1.Name = "btnAgregar1";
            btnAgregar1.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar1.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar1.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar1.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar1.Size = new Size(136, 45);
            btnAgregar1.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAgregar1.StateCommon.Back.Color2 = Color.White;
            btnAgregar1.StateCommon.Border.Rounding = 5F;
            btnAgregar1.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAgregar1.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar1.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAgregar1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAgregar1.StatePressed.Back.Color1 = Color.Transparent;
            btnAgregar1.StatePressed.Back.Color2 = Color.Transparent;
            btnAgregar1.TabIndex = 380;
            btnAgregar1.Values.DropDownArrowColor = Color.Empty;
            btnAgregar1.Values.Text = "Agregar";
            btnAgregar1.Click += btnAgregar1_Click;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(634, 604);
            btnModificar.Margin = new Padding(3, 2, 3, 2);
            btnModificar.Name = "btnModificar";
            btnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideDefault.Back.Color2 = Color.White;
            btnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideFocus.Back.Color2 = Color.White;
            btnModificar.Size = new Size(136, 45);
            btnModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnModificar.StateCommon.Back.Color2 = Color.White;
            btnModificar.StateCommon.Border.Rounding = 5F;
            btnModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnModificar.StatePressed.Back.Color1 = Color.Transparent;
            btnModificar.StatePressed.Back.Color2 = Color.Transparent;
            btnModificar.TabIndex = 381;
            btnModificar.Values.DropDownArrowColor = Color.Empty;
            btnModificar.Values.Text = "Modificar";
            btnModificar.Click += btnModificar_Click_1;
            // 
            // ProveedoresAdmin
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1018, 677);
            Controls.Add(btnModificar);
            Controls.Add(btnAgregar1);
            Controls.Add(Nombre);
            Controls.Add(dgvProveedor);
            Controls.Add(label10);
            Controls.Add(btnRefresh);
            Controls.Add(panel2);
            Controls.Add(txtBuscar);
            Controls.Add(btnNoti);
            Controls.Add(panel8);
            Controls.Add(panel5);
            Controls.Add(panel3);
            Controls.Add(panel4);
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
            Margin = new Padding(3, 2, 3, 2);
            Name = "ProveedoresAdmin";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            Load += ProveedoresAdmin_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProveedor).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Krypton.Toolkit.KryptonTextBox txtBuscar;
        private Button btnNoti;
        private Panel panel8;
        private Panel panel5;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
        private Button btnRefresh;
        private Label label10;
        private DataGridView dgvProveedor;
        private Label Nombre;
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
        private Krypton.Toolkit.KryptonButton btnAgregar1;
        private Krypton.Toolkit.KryptonButton btnModificar;
    }
}