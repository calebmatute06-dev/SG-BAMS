namespace SG_BAMS
{
    partial class FacturasEmp
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
            panel5 = new Panel();
            panel8 = new Panel();
            panel3 = new Panel();
            panel4 = new Panel();
            panel2 = new Panel();
            BtnRefrescar = new Button();
            label6 = new Label();
            label5 = new Label();
            label3 = new Label();
            txtBusqueda = new Krypton.Toolkit.KryptonTextBox();
            dgvFacturas = new DataGridView();
            dtpFin = new DateTimePicker();
            dtpInicio = new DateTimePicker();
            BtnNueva = new Krypton.Toolkit.KryptonButton();
            BtnVer = new Krypton.Toolkit.KryptonButton();
            label10 = new Label();
            kryptonButton14 = new Krypton.Toolkit.KryptonButton();
            label1 = new Label();
            BtnNotificaciones = new Button();
            Nombre = new Label();
            label2 = new Label();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            btnPerfil = new ReaLTaiizor.Controls.NightButton();
            btnCerrar = new ReaLTaiizor.Controls.NightButton();
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).BeginInit();
            SuspendLayout();
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(0, 0);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(21, 678);
            panel5.TabIndex = 147;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(235, 16);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(21, 664);
            panel8.TabIndex = 148;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(0, 662);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(1436, 18);
            panel3.TabIndex = 149;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(1415, 0);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(21, 672);
            panel4.TabIndex = 146;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(5, 0);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(1428, 18);
            panel2.TabIndex = 145;
            // 
            // BtnRefrescar
            // 
            BtnRefrescar.BackgroundImage = Properties.Resources.refresh;
            BtnRefrescar.BackgroundImageLayout = ImageLayout.Stretch;
            BtnRefrescar.Location = new Point(1146, 206);
            BtnRefrescar.Margin = new Padding(3, 2, 3, 2);
            BtnRefrescar.Name = "BtnRefrescar";
            BtnRefrescar.Size = new Size(57, 33);
            BtnRefrescar.TabIndex = 351;
            BtnRefrescar.UseVisualStyleBackColor = true;
            BtnRefrescar.Click += BtnRefrescar_Click;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial Narrow", 14.25F);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(550, 142);
            label6.Name = "label6";
            label6.Size = new Size(48, 23);
            label6.TabIndex = 350;
            label6.Text = "Final:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Arial Narrow", 14.25F);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(298, 142);
            label5.Name = "label5";
            label5.Size = new Size(52, 23);
            label5.TabIndex = 349;
            label5.Text = "Inicial:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 24F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(292, 103);
            label3.Name = "label3";
            label3.Size = new Size(94, 37);
            label3.TabIndex = 347;
            label3.Text = "Fecha";
            label3.TextAlign = ContentAlignment.TopCenter;
            // 
            // txtBusqueda
            // 
            txtBusqueda.Location = new Point(562, 207);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(578, 32);
            txtBusqueda.StateCommon.Back.Color1 = Color.SkyBlue;
            txtBusqueda.StateCommon.Border.Rounding = 10F;
            txtBusqueda.StateCommon.Content.Color1 = Color.Navy;
            txtBusqueda.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtBusqueda.TabIndex = 346;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // dgvFacturas
            // 
            dgvFacturas.BackgroundColor = Color.SkyBlue;
            dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvFacturas.Location = new Point(298, 244);
            dgvFacturas.Margin = new Padding(3, 2, 3, 2);
            dgvFacturas.Name = "dgvFacturas";
            dgvFacturas.RowHeadersWidth = 51;
            dgvFacturas.Size = new Size(1050, 333);
            dgvFacturas.TabIndex = 332;
            dgvFacturas.CellDoubleClick += dgvFacturas_CellDoubleClick;
            // 
            // dtpFin
            // 
            dtpFin.CalendarForeColor = Color.Navy;
            dtpFin.Location = new Point(550, 168);
            dtpFin.Name = "dtpFin";
            dtpFin.Size = new Size(227, 23);
            dtpFin.TabIndex = 345;
            // 
            // dtpInicio
            // 
            dtpInicio.Location = new Point(296, 168);
            dtpInicio.Name = "dtpInicio";
            dtpInicio.Size = new Size(227, 23);
            dtpInicio.TabIndex = 344;
            // 
            // BtnNueva
            // 
            BtnNueva.Location = new Point(667, 598);
            BtnNueva.Margin = new Padding(3, 2, 3, 2);
            BtnNueva.Name = "BtnNueva";
            BtnNueva.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnNueva.OverrideDefault.Back.Color2 = Color.White;
            BtnNueva.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnNueva.OverrideFocus.Back.Color2 = Color.White;
            BtnNueva.Size = new Size(196, 45);
            BtnNueva.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnNueva.StateCommon.Back.Color2 = Color.White;
            BtnNueva.StateCommon.Border.Rounding = 30F;
            BtnNueva.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnNueva.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnNueva.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnNueva.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnNueva.StatePressed.Back.Color1 = Color.Transparent;
            BtnNueva.StatePressed.Back.Color2 = Color.Transparent;
            BtnNueva.TabIndex = 342;
            BtnNueva.Values.DropDownArrowColor = Color.Empty;
            BtnNueva.Values.Text = "Nueva Factura";
            BtnNueva.Click += BtnNueva_Click;
            // 
            // BtnVer
            // 
            BtnVer.Location = new Point(927, 598);
            BtnVer.Margin = new Padding(3, 2, 3, 2);
            BtnVer.Name = "BtnVer";
            BtnVer.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnVer.OverrideDefault.Back.Color2 = Color.White;
            BtnVer.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnVer.OverrideFocus.Back.Color2 = Color.White;
            BtnVer.Size = new Size(75, 45);
            BtnVer.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnVer.StateCommon.Back.Color2 = Color.White;
            BtnVer.StateCommon.Border.Rounding = 30F;
            BtnVer.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnVer.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnVer.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnVer.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnVer.StatePressed.Back.Color1 = Color.Transparent;
            BtnVer.StatePressed.Back.Color2 = Color.Transparent;
            BtnVer.TabIndex = 343;
            BtnVer.Values.DropDownArrowColor = Color.Empty;
            BtnVer.Values.Text = "Ver";
            BtnVer.Click += BtnVer_Click;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(765, 28);
            label10.Name = "label10";
            label10.Size = new Size(225, 56);
            label10.TabIndex = 338;
            label10.Text = "Facturas";
            // 
            // kryptonButton14
            // 
            kryptonButton14.Location = new Point(423, 215);
            kryptonButton14.Margin = new Padding(3, 2, 3, 2);
            kryptonButton14.Name = "kryptonButton14";
            kryptonButton14.Size = new Size(0, 0);
            kryptonButton14.StateCommon.Border.Rounding = 100F;
            kryptonButton14.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton14.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton14.TabIndex = 337;
            kryptonButton14.Values.DropDownArrowColor = Color.Empty;
            kryptonButton14.Values.Text = "";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(886, 573);
            label1.Name = "label1";
            label1.Size = new Size(0, 15);
            label1.TabIndex = 336;
            // 
            // BtnNotificaciones
            // 
            BtnNotificaciones.BackColor = Color.Transparent;
            BtnNotificaciones.BackgroundImage = Properties.Resources.campana;
            BtnNotificaciones.BackgroundImageLayout = ImageLayout.Stretch;
            BtnNotificaciones.FlatAppearance.BorderColor = Color.White;
            BtnNotificaciones.FlatAppearance.BorderSize = 0;
            BtnNotificaciones.FlatStyle = FlatStyle.Flat;
            BtnNotificaciones.ForeColor = Color.Navy;
            BtnNotificaciones.Location = new Point(1358, 20);
            BtnNotificaciones.Margin = new Padding(3, 2, 3, 2);
            BtnNotificaciones.Name = "BtnNotificaciones";
            BtnNotificaciones.Size = new Size(52, 33);
            BtnNotificaciones.TabIndex = 333;
            BtnNotificaciones.UseVisualStyleBackColor = false;
            BtnNotificaciones.Click += BtnNotificaciones_Click;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(477, 214);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(84, 22);
            Nombre.TabIndex = 352;
            Nombre.Text = "Buscar:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(65, 43);
            label2.Name = "label2";
            label2.Size = new Size(117, 41);
            label2.TabIndex = 388;
            label2.Text = "BAMS";
            // 
            // btnFacturas
            // 
            btnFacturas.BackColor = Color.SkyBlue;
            btnFacturas.DialogResult = DialogResult.None;
            btnFacturas.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFacturas.ForeColor = Color.White;
            btnFacturas.HoverBackColor = Color.SkyBlue;
            btnFacturas.HoverForeColor = Color.White;
            btnFacturas.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnFacturas.Location = new Point(8, 156);
            btnFacturas.MinimumSize = new Size(144, 47);
            btnFacturas.Name = "btnFacturas";
            btnFacturas.NormalBackColor = Color.SkyBlue;
            btnFacturas.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnFacturas.PressedBackColor = Color.Navy;
            btnFacturas.PressedForeColor = Color.White;
            btnFacturas.Radius = 20;
            btnFacturas.Size = new Size(215, 47);
            btnFacturas.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnFacturas.TabIndex = 392;
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
            btnClientes.Location = new Point(8, 209);
            btnClientes.MinimumSize = new Size(144, 47);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.Navy;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(215, 47);
            btnClientes.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnClientes.TabIndex = 391;
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
            btnInventario.Location = new Point(8, 260);
            btnInventario.MinimumSize = new Size(144, 47);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.Navy;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(215, 47);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 390;
            btnInventario.Text = "Inventario";
            btnInventario.Click += btnInventario_Click;
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
            btnDeudores.Location = new Point(8, 311);
            btnDeudores.MinimumSize = new Size(144, 47);
            btnDeudores.Name = "btnDeudores";
            btnDeudores.NormalBackColor = Color.Navy;
            btnDeudores.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnDeudores.PressedBackColor = Color.Navy;
            btnDeudores.PressedForeColor = Color.White;
            btnDeudores.Radius = 20;
            btnDeudores.Size = new Size(215, 47);
            btnDeudores.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnDeudores.TabIndex = 389;
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
            btnMenu.Location = new Point(8, 103);
            btnMenu.MinimumSize = new Size(144, 47);
            btnMenu.Name = "btnMenu";
            btnMenu.NormalBackColor = Color.Navy;
            btnMenu.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnMenu.PressedBackColor = Color.Navy;
            btnMenu.PressedForeColor = Color.White;
            btnMenu.Radius = 20;
            btnMenu.Size = new Size(215, 47);
            btnMenu.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnMenu.TabIndex = 393;
            btnMenu.Text = "Menu Principal";
            btnMenu.Click += btnMenu_Click;
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
            btnPerfil.Location = new Point(8, 414);
            btnPerfil.MinimumSize = new Size(144, 47);
            btnPerfil.Name = "btnPerfil";
            btnPerfil.NormalBackColor = Color.Navy;
            btnPerfil.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnPerfil.PressedBackColor = Color.Navy;
            btnPerfil.PressedForeColor = Color.White;
            btnPerfil.Radius = 20;
            btnPerfil.Size = new Size(215, 47);
            btnPerfil.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnPerfil.TabIndex = 397;
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
            btnCerrar.Location = new Point(8, 364);
            btnCerrar.MinimumSize = new Size(144, 47);
            btnCerrar.Name = "btnCerrar";
            btnCerrar.NormalBackColor = Color.Navy;
            btnCerrar.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnCerrar.PressedBackColor = Color.Navy;
            btnCerrar.PressedForeColor = Color.White;
            btnCerrar.Radius = 20;
            btnCerrar.Size = new Size(215, 47);
            btnCerrar.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnCerrar.TabIndex = 396;
            btnCerrar.Text = "Cerrar Sesión";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // FacturasEmp
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1436, 679);
            Controls.Add(Nombre);
            Controls.Add(BtnRefrescar);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label3);
            Controls.Add(txtBusqueda);
            Controls.Add(dgvFacturas);
            Controls.Add(dtpFin);
            Controls.Add(dtpInicio);
            Controls.Add(BtnNueva);
            Controls.Add(BtnVer);
            Controls.Add(label10);
            Controls.Add(kryptonButton14);
            Controls.Add(label1);
            Controls.Add(BtnNotificaciones);
            Controls.Add(panel5);
            Controls.Add(panel8);
            Controls.Add(panel3);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(label2);
            Controls.Add(btnFacturas);
            Controls.Add(btnClientes);
            Controls.Add(btnInventario);
            Controls.Add(btnDeudores);
            Controls.Add(btnMenu);
            Controls.Add(btnPerfil);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "FacturasEmp";
            Text = "FacturasEmp";
            Load += FacturasEmp_Load;
            ((System.ComponentModel.ISupportInitialize)dgvFacturas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel5;
        private Panel panel8;
        private Panel panel3;
        private Panel panel4;
        private Panel panel2;
        private Button BtnRefrescar;
        private Label label6;
        private Label label5;
        private Label label3;
        private Krypton.Toolkit.KryptonTextBox txtBusqueda;
        private DataGridView dgvFacturas;
        private DateTimePicker dtpFin;
        private DateTimePicker dtpInicio;
        private Krypton.Toolkit.KryptonButton BtnNueva;
        private Krypton.Toolkit.KryptonButton BtnVer;
        private Label label10;
        private Krypton.Toolkit.KryptonButton kryptonButton14;
        private Label label1;
        private Krypton.Toolkit.KryptonButton kryptonButton11;
        private PictureBox pictureBox18;
        private Button BtnNotificaciones;
        private Label Nombre;
        private Label label2;
        private ReaLTaiizor.Controls.NightButton btnFacturas;
        private ReaLTaiizor.Controls.NightButton btnClientes;
        private ReaLTaiizor.Controls.NightButton btnInventario;
        private ReaLTaiizor.Controls.NightButton btnDeudores;
        private ReaLTaiizor.Controls.NightButton btnMenu;
        private ReaLTaiizor.Controls.NightButton btnPerfil;
        private ReaLTaiizor.Controls.NightButton btnCerrar;
    }
}