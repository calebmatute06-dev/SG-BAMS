namespace SG_BAMS.Facturas
{
    partial class BateriaVieja
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
            txtPrecio = new Krypton.Toolkit.KryptonTextBox();
            cmbBaterias = new Krypton.Toolkit.KryptonComboBox();
            Nombre = new Label();
            label1 = new Label();
            label2 = new Label();
            dgvBateria = new DataGridView();
            BtnAceptar = new Krypton.Toolkit.KryptonButton();
            Agregar = new Krypton.Toolkit.KryptonButton();
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).BeginInit();
            SuspendLayout();
            // 
            // txtPrecio
            // 
            txtPrecio.Location = new Point(188, 173);
            txtPrecio.Margin = new Padding(3, 4, 3, 4);
            txtPrecio.Name = "txtPrecio";
            txtPrecio.Size = new Size(197, 36);
            txtPrecio.StateCommon.Back.Color1 = Color.SkyBlue;
            txtPrecio.StateCommon.Border.Rounding = 10F;
            txtPrecio.StateCommon.Content.Color1 = Color.Navy;
            txtPrecio.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtPrecio.TabIndex = 336;
            // 
            // cmbBaterias
            // 
            cmbBaterias.DropDownWidth = 300;
            cmbBaterias.Location = new Point(188, 100);
            cmbBaterias.Name = "cmbBaterias";
            cmbBaterias.Size = new Size(197, 38);
            cmbBaterias.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbBaterias.StateCommon.ComboBox.Border.Rounding = 20F;
            cmbBaterias.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbBaterias.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbBaterias.StateCommon.Item.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbBaterias.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbBaterias.TabIndex = 337;
            // 
            // Nombre
            // 
            Nombre.AutoSize = true;
            Nombre.BackColor = Color.Transparent;
            Nombre.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            Nombre.ForeColor = Color.Navy;
            Nombre.Location = new Point(67, 111);
            Nombre.Name = "Nombre";
            Nombre.Size = new Size(105, 27);
            Nombre.TabIndex = 338;
            Nombre.Text = "Nombre:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(197, 22);
            label1.Name = "label1";
            label1.Size = new Size(188, 40);
            label1.TabIndex = 339;
            label1.Text = "Bateria Vieja";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial", 13.8F, FontStyle.Bold);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(67, 182);
            label2.Name = "label2";
            label2.Size = new Size(90, 27);
            label2.TabIndex = 340;
            label2.Text = "Precio:";
            // 
            // dgvBateria
            // 
            dgvBateria.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBateria.Location = new Point(37, 302);
            dgvBateria.Name = "dgvBateria";
            dgvBateria.RowHeadersWidth = 51;
            dgvBateria.Size = new Size(490, 246);
            dgvBateria.TabIndex = 341;
            // 
            // BtnAceptar
            // 
            BtnAceptar.Location = new Point(133, 564);
            BtnAceptar.Name = "BtnAceptar";
            BtnAceptar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideDefault.Back.Color2 = Color.White;
            BtnAceptar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAceptar.OverrideFocus.Back.Color2 = Color.White;
            BtnAceptar.Size = new Size(123, 60);
            BtnAceptar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateCommon.Back.Color2 = Color.White;
            BtnAceptar.StateCommon.Border.Rounding = 30F;
            BtnAceptar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAceptar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAceptar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAceptar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAceptar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAceptar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAceptar.TabIndex = 342;
            BtnAceptar.Values.DropDownArrowColor = Color.Empty;
            BtnAceptar.Values.Text = "Aceptar";
            // 
            // Agregar
            // 
            Agregar.Location = new Point(67, 241);
            Agregar.Name = "Agregar";
            Agregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            Agregar.OverrideDefault.Back.Color2 = Color.White;
            Agregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            Agregar.OverrideFocus.Back.Color2 = Color.White;
            Agregar.Size = new Size(138, 45);
            Agregar.StateCommon.Back.Color1 = Color.SkyBlue;
            Agregar.StateCommon.Back.Color2 = Color.White;
            Agregar.StateCommon.Border.Rounding = 30F;
            Agregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            Agregar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Agregar.StateNormal.Back.Color1 = Color.SkyBlue;
            Agregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            Agregar.StateNormal.Content.ShortText.Font = new Font("Arial", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Agregar.StatePressed.Back.Color1 = Color.Transparent;
            Agregar.StatePressed.Back.Color2 = Color.Transparent;
            Agregar.TabIndex = 343;
            Agregar.Values.DropDownArrowColor = Color.Empty;
            Agregar.Values.Text = "Agregar";
            Agregar.Click += Agregar_Click;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Location = new Point(300, 564);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.OverrideDefault.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideDefault.Back.Color2 = Color.White;
            kryptonButton1.OverrideFocus.Back.Color1 = Color.SkyBlue;
            kryptonButton1.OverrideFocus.Back.Color2 = Color.White;
            kryptonButton1.Size = new Size(139, 60);
            kryptonButton1.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonButton1.StateCommon.Back.Color2 = Color.White;
            kryptonButton1.StateCommon.Border.Rounding = 30F;
            kryptonButton1.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton1.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            kryptonButton1.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            kryptonButton1.StatePressed.Back.Color1 = Color.Transparent;
            kryptonButton1.StatePressed.Back.Color2 = Color.Transparent;
            kryptonButton1.TabIndex = 344;
            kryptonButton1.Values.DropDownArrowColor = Color.Empty;
            kryptonButton1.Values.Text = "Cancelar";
            // 
            // BateriaVieja
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(591, 651);
            Controls.Add(kryptonButton1);
            Controls.Add(Agregar);
            Controls.Add(BtnAceptar);
            Controls.Add(dgvBateria);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(Nombre);
            Controls.Add(cmbBaterias);
            Controls.Add(txtPrecio);
            Name = "BateriaVieja";
            Text = "BateriaVieja";
            Load += BateriaVieja_Load;
            ((System.ComponentModel.ISupportInitialize)cmbBaterias).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvBateria).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Krypton.Toolkit.KryptonTextBox txtPrecio;
        private Krypton.Toolkit.KryptonComboBox cmbBaterias;
        private Label Nombre;
        private Label label1;
        private Label label2;
        private DataGridView dgvBateria;
        private Krypton.Toolkit.KryptonButton BtnAceptar;
        private Krypton.Toolkit.KryptonButton Agregar;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
    }
}