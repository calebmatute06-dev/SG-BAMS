namespace SG_BAMS
{
    partial class ClienteExistente
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
            panel1 = new Panel();
            panel2 = new Panel();
            panel8 = new Panel();
            label3 = new Label();
            label4 = new Label();
            cmbClientes = new Krypton.Toolkit.KryptonComboBox();
            BtnSalir = new Krypton.Toolkit.KryptonButton();
            BtnAsignar = new Krypton.Toolkit.KryptonButton();
            label5 = new Label();
            ((System.ComponentModel.ISupportInitialize)cmbClientes).BeginInit();
            SuspendLayout();
            // 
            // panel3
            // 
            panel3.BackColor = Color.Navy;
            panel3.Location = new Point(544, 0);
            panel3.Name = "panel3";
            panel3.Size = new Size(25, 355);
            panel3.TabIndex = 176;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(22, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(547, 24);
            panel1.TabIndex = 171;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(-2, 331);
            panel2.Name = "panel2";
            panel2.Size = new Size(571, 24);
            panel2.TabIndex = 170;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(-2, -1);
            panel8.Name = "panel8";
            panel8.Size = new Size(25, 356);
            panel8.TabIndex = 164;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(94, 163);
            label3.Name = "label3";
            label3.Size = new Size(91, 24);
            label3.TabIndex = 182;
            label3.Text = "Nombre:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(146, 59);
            label4.Name = "label4";
            label4.Size = new Size(319, 44);
            label4.TabIndex = 331;
            label4.Text = "Cliente Existente";
            // 
            // cmbClientes
            // 
            cmbClientes.CueHint.Color1 = Color.DimGray;
            cmbClientes.CueHint.CueHintText = "Seleccione un cliente";
            cmbClientes.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbClientes.DropDownWidth = 300;
            cmbClientes.Location = new Point(187, 162);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(259, 26);
            cmbClientes.StateCommon.ComboBox.Back.Color1 = Color.White;
            cmbClientes.StateCommon.ComboBox.Border.Color1 = Color.Navy;
            cmbClientes.StateCommon.ComboBox.Border.Rounding = 5F;
            cmbClientes.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbClientes.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbClientes.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbClientes.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbClientes.TabIndex = 333;
            // 
            // BtnSalir
            // 
            BtnSalir.Location = new Point(307, 236);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(102, 60);
            BtnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateCommon.Back.Color2 = Color.White;
            BtnSalir.StateCommon.Border.Rounding = 5F;
            BtnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnSalir.StatePressed.Back.Color1 = Color.Transparent;
            BtnSalir.StatePressed.Back.Color2 = Color.Transparent;
            BtnSalir.TabIndex = 335;
            BtnSalir.Values.DropDownArrowColor = Color.Empty;
            BtnSalir.Values.Text = "Salir";
            BtnSalir.Click += BtnSalir_Click;
            // 
            // BtnAsignar
            // 
            BtnAsignar.Location = new Point(131, 236);
            BtnAsignar.Name = "BtnAsignar";
            BtnAsignar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAsignar.OverrideDefault.Back.Color2 = Color.White;
            BtnAsignar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAsignar.OverrideFocus.Back.Color2 = Color.White;
            BtnAsignar.Size = new Size(141, 60);
            BtnAsignar.StateCommon.Back.Color1 = Color.SkyBlue;
            BtnAsignar.StateCommon.Back.Color2 = Color.White;
            BtnAsignar.StateCommon.Border.Rounding = 5F;
            BtnAsignar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            BtnAsignar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            BtnAsignar.StateNormal.Back.Color1 = Color.SkyBlue;
            BtnAsignar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            BtnAsignar.StatePressed.Back.Color1 = Color.Transparent;
            BtnAsignar.StatePressed.Back.Color2 = Color.Transparent;
            BtnAsignar.TabIndex = 334;
            BtnAsignar.Values.DropDownArrowColor = Color.Empty;
            BtnAsignar.Values.Text = "Asignar";
            BtnAsignar.Click += BtnAsignar_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(437, 293);
            label5.Name = "label5";
            label5.Size = new Size(102, 35);
            label5.TabIndex = 346;
            label5.Text = "BAMS";
            // 
            // ClienteExistente
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(569, 355);
            Controls.Add(label5);
            Controls.Add(BtnSalir);
            Controls.Add(BtnAsignar);
            Controls.Add(cmbClientes);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(panel3);
            Controls.Add(panel1);
            Controls.Add(panel2);
            Controls.Add(panel8);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "ClienteExistente";
            Text = "ClienteExistente";
            Load += ClienteExistente_Load;
            ((System.ComponentModel.ISupportInitialize)cmbClientes).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Panel panel3;
        private Panel panel1;
        private Panel panel2;
        private Panel panel8;
        private Label label3;
        private Label label4;
        private Krypton.Toolkit.KryptonComboBox cmbClientes;
        private Krypton.Toolkit.KryptonButton BtnSalir;
        private Krypton.Toolkit.KryptonButton BtnAsignar;
        private Label label5;
    }
}