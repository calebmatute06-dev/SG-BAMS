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
            label2 = new Label();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            btnPerfil = new ReaLTaiizor.Controls.NightButton();
            btnCerrar = new ReaLTaiizor.Controls.NightButton();
            ((System.ComponentModel.ISupportInitialize)dgvClientes).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(1, 883);
            panel3.Name = "panel3";
            panel3.Size = new Size(1294, 24);
            panel3.TabIndex = 147;
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(1258, 0);
            panel4.Name = "panel4";
            panel4.Size = new Size(24, 904);
            panel4.TabIndex = 144;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(270, 21);
            panel8.Name = "panel8";
            panel8.Size = new Size(24, 869);
            panel8.TabIndex = 146;
            // 
            // panel5
            // 
            panel5.BackColor = Color.Navy;
            panel5.Location = new Point(-1, 0);
            panel5.Name = "panel5";
            panel5.Size = new Size(24, 904);
            panel5.TabIndex = 145;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(7, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(1285, 24);
            panel2.TabIndex = 143;
            // 
            // chkActivo
            // 
            chkActivo.Location = new Point(1050, 204);
            chkActivo.Name = "chkActivo";
            chkActivo.Size = new Size(104, 31);
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
            dgvClientes.Location = new Point(357, 261);
            dgvClientes.Name = "dgvClientes";
            dgvClientes.RowHeadersWidth = 51;
            dgvClientes.Size = new Size(825, 501);
            dgvClientes.TabIndex = 339;
            dgvClientes.CellDoubleClick += dgvClientes_CellDoubleClick;
            // 
            // BtnModificar
            // 
            BtnModificar.Location = new Point(718, 792);
            BtnModificar.Name = "BtnModificar";
            BtnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnModificar.OverrideDefault.Back.Color2 = Color.White;
            BtnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnModificar.OverrideFocus.Back.Color2 = Color.White;
            BtnModificar.Size = new Size(146, 60);
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
            txtBusqueda.CueHint.Color1 = Color.DimGray;
            txtBusqueda.CueHint.CueHintText = "Busqueda por Nombre o ID";
            txtBusqueda.Location = new Point(473, 195);
            txtBusqueda.Margin = new Padding(3, 4, 3, 4);
            txtBusqueda.Name = "txtBusqueda";
            txtBusqueda.Size = new Size(570, 33);
            txtBusqueda.StateCommon.Back.Color1 = Color.SkyBlue;
            txtBusqueda.StateCommon.Border.Rounding = 10F;
            txtBusqueda.TabIndex = 336;
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            // 
            // kryptonButton14
            // 
            kryptonButton14.Location = new Point(329, 277);
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
            label1.Location = new Point(1013, 765);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
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
            btnNoti.Location = new Point(1195, 28);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(59, 44);
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
            label7.Location = new Point(370, 204);
            label7.Name = "label7";
            label7.Size = new Size(99, 27);
            label7.TabIndex = 355;
            label7.Text = "Buscar:";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 36F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(683, 81);
            label10.Name = "label10";
            label10.Size = new Size(263, 70);
            label10.TabIndex = 356;
            label10.Text = "Clientes";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(75, 53);
            label2.Name = "label2";
            label2.Size = new Size(151, 51);
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
            btnFacturas.Location = new Point(10, 204);
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
            btnFacturas.TabIndex = 386;
            btnFacturas.Text = "Facturas";
            btnFacturas.Click += btnFacturas_Click;
            // 
            // btnClientes
            // 
            btnClientes.BackColor = Color.SkyBlue;
            btnClientes.DialogResult = DialogResult.None;
            btnClientes.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClientes.ForeColor = Color.White;
            btnClientes.HoverBackColor = Color.SkyBlue;
            btnClientes.HoverForeColor = Color.White;
            btnClientes.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnClientes.Location = new Point(10, 275);
            btnClientes.Margin = new Padding(3, 4, 3, 4);
            btnClientes.MinimumSize = new Size(165, 63);
            btnClientes.Name = "btnClientes";
            btnClientes.NormalBackColor = Color.SkyBlue;
            btnClientes.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnClientes.PressedBackColor = Color.Navy;
            btnClientes.PressedForeColor = Color.White;
            btnClientes.Radius = 20;
            btnClientes.Size = new Size(246, 63);
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
            btnInventario.Location = new Point(10, 343);
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
            btnInventario.TabIndex = 383;
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
            btnDeudores.Location = new Point(10, 411);
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
            btnDeudores.TabIndex = 381;
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
            btnMenu.Location = new Point(10, 133);
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
            btnMenu.TabIndex = 387;
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
            btnPerfil.Location = new Point(10, 548);
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
            btnPerfil.TabIndex = 389;
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
            btnCerrar.Location = new Point(10, 481);
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
            btnCerrar.TabIndex = 388;
            btnCerrar.Text = "Cerrar Sesión";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // ClientesEmp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1281, 908);
            Controls.Add(label10);
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
            Controls.Add(btnPerfil);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ClientesEmp";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ClientesEmp";
            Load += ClientesEmp_Load;
            ((System.ComponentModel.ISupportInitialize)dgvClientes).EndInit();
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