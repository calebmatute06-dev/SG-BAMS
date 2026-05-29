namespace SG_BAMS
{
    partial class InventarioEmp
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
            panel4 = new Panel();
            panel3 = new Panel();
            txtBuscar = new Krypton.Toolkit.KryptonTextBox();
            btnNoti = new Button();
            panel7 = new Panel();
            panel2 = new Panel();
            dgvInventarioEmp = new DataGridView();
            label10 = new Label();
            label3 = new Label();
            label2 = new Label();
            btnFacturas = new ReaLTaiizor.Controls.NightButton();
            btnClientes = new ReaLTaiizor.Controls.NightButton();
            btnInventario = new ReaLTaiizor.Controls.NightButton();
            btnDeudores = new ReaLTaiizor.Controls.NightButton();
            btnMenu = new ReaLTaiizor.Controls.NightButton();
            panel1 = new Panel();
            btnPerfil = new ReaLTaiizor.Controls.NightButton();
            btnCerrar = new ReaLTaiizor.Controls.NightButton();
            ((System.ComponentModel.ISupportInitialize)dgvInventarioEmp).BeginInit();
            SuspendLayout();
            // 
            // panel4
            // 
            panel4.BackColor = Color.Navy;
            panel4.Location = new Point(-3, 881);
            panel4.Name = "panel4";
            panel4.Size = new Size(1495, 24);
            panel4.TabIndex = 197;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(264, 23);
            panel3.Name = "panel3";
            panel3.Size = new Size(24, 869);
            panel3.TabIndex = 196;
            // 
            // txtBuscar
            // 
            txtBuscar.CueHint.CueHintText = "Ingrese un Nombre, ID, Tipo, Proveedor, Modelo, Marca";
            txtBuscar.Location = new Point(614, 183);
            txtBuscar.Name = "txtBuscar";
            txtBuscar.Size = new Size(679, 39);
            txtBuscar.StateCommon.Back.Color1 = Color.SkyBlue;
            txtBuscar.StateCommon.Border.Rounding = 20F;
            txtBuscar.StateCommon.Content.Color1 = Color.Gray;
            txtBuscar.TabIndex = 208;
            txtBuscar.Text = "Ingrese un Nombre, ID, Tipo, Proveedor, Modelo, Marca";
            txtBuscar.TextChanged += txtBuscar_TextChanged;
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
            btnNoti.Location = new Point(1402, 29);
            btnNoti.Name = "btnNoti";
            btnNoti.Size = new Size(59, 44);
            btnNoti.TabIndex = 201;
            btnNoti.UseVisualStyleBackColor = false;
            btnNoti.Click += btnNoti_Click;
            // 
            // panel7
            // 
            panel7.BackColor = Color.Navy;
            panel7.Location = new Point(1467, 21);
            panel7.Name = "panel7";
            panel7.Size = new Size(24, 884);
            panel7.TabIndex = 199;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(1, -1);
            panel2.Name = "panel2";
            panel2.Size = new Size(1490, 24);
            panel2.TabIndex = 211;
            // 
            // dgvInventarioEmp
            // 
            dgvInventarioEmp.BackgroundColor = Color.SkyBlue;
            dgvInventarioEmp.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvInventarioEmp.Location = new Point(311, 239);
            dgvInventarioEmp.Name = "dgvInventarioEmp";
            dgvInventarioEmp.RowHeadersWidth = 51;
            dgvInventarioEmp.Size = new Size(1139, 632);
            dgvInventarioEmp.TabIndex = 213;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.Transparent;
            label10.Font = new Font("Arial", 36F, FontStyle.Bold);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(774, 53);
            label10.Name = "label10";
            label10.Size = new Size(317, 70);
            label10.TabIndex = 214;
            label10.Text = "Inventario";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(509, 195);
            label3.Name = "label3";
            label3.Size = new Size(99, 27);
            label3.TabIndex = 355;
            label3.Text = "Buscar:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial", 26.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(79, 53);
            label2.Name = "label2";
            label2.Size = new Size(151, 51);
            label2.TabIndex = 388;
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
            btnFacturas.Location = new Point(10, 197);
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
            btnFacturas.TabIndex = 392;
            btnFacturas.Text = "Facturas";
            btnFacturas.Click += btnFacturas_Click;
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
            btnClientes.Location = new Point(10, 268);
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
            btnClientes.TabIndex = 391;
            btnClientes.Text = "Clientes";
            btnClientes.Click += btnClientes_Click;
            // 
            // btnInventario
            // 
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.DialogResult = DialogResult.None;
            btnInventario.Font = new Font("Arial Narrow", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnInventario.ForeColor = Color.White;
            btnInventario.HoverBackColor = Color.SkyBlue;
            btnInventario.HoverForeColor = Color.White;
            btnInventario.InterpolationType = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
            btnInventario.Location = new Point(10, 336);
            btnInventario.Margin = new Padding(3, 4, 3, 4);
            btnInventario.MinimumSize = new Size(165, 63);
            btnInventario.Name = "btnInventario";
            btnInventario.NormalBackColor = Color.SkyBlue;
            btnInventario.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            btnInventario.PressedBackColor = Color.Navy;
            btnInventario.PressedForeColor = Color.White;
            btnInventario.Radius = 20;
            btnInventario.Size = new Size(246, 63);
            btnInventario.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            btnInventario.TabIndex = 390;
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
            btnDeudores.Location = new Point(9, 404);
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
            btnMenu.Location = new Point(10, 127);
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
            btnMenu.TabIndex = 393;
            btnMenu.Text = "Menu Principal";
            btnMenu.Click += btnMenu_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(-3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(24, 901);
            panel1.TabIndex = 197;
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
            btnPerfil.Location = new Point(9, 540);
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
            btnPerfil.TabIndex = 399;
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
            btnCerrar.Location = new Point(9, 473);
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
            btnCerrar.TabIndex = 398;
            btnCerrar.Text = "Cerrar Sesión";
            btnCerrar.Click += btnCerrar_Click;
            // 
            // InventarioEmp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1491, 905);
            Controls.Add(panel1);
            Controls.Add(label2);
            Controls.Add(btnFacturas);
            Controls.Add(btnClientes);
            Controls.Add(btnInventario);
            Controls.Add(btnDeudores);
            Controls.Add(btnMenu);
            Controls.Add(label3);
            Controls.Add(label10);
            Controls.Add(dgvInventarioEmp);
            Controls.Add(panel7);
            Controls.Add(panel2);
            Controls.Add(panel4);
            Controls.Add(panel3);
            Controls.Add(txtBuscar);
            Controls.Add(btnNoti);
            Controls.Add(btnPerfil);
            Controls.Add(btnCerrar);
            FormBorderStyle = FormBorderStyle.None;
            Name = "InventarioEmp";
            Text = "InventarioEmp";
            Load += InventarioEmp_Load;
            ((System.ComponentModel.ISupportInitialize)dgvInventarioEmp).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel4;
        private Panel panel3;
        private Krypton.Toolkit.KryptonTextBox txtBuscar;
        private Button btnNoti;
        private Panel panel7;
        private Panel panel2;
        private DataGridView dgvInventarioEmp;
        private Label label10;
        private Label label3;
        private Label label2;
        private ReaLTaiizor.Controls.NightButton btnFacturas;
        private ReaLTaiizor.Controls.NightButton btnClientes;
        private ReaLTaiizor.Controls.NightButton btnInventario;
        private ReaLTaiizor.Controls.NightButton btnDeudores;
        private ReaLTaiizor.Controls.NightButton btnMenu;
        private Panel panel1;
        private ReaLTaiizor.Controls.NightButton btnPerfil;
        private ReaLTaiizor.Controls.NightButton btnCerrar;
    }
}