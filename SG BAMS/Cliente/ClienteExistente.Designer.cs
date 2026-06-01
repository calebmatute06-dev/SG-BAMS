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
            panel3.Location = new Point(476, 0);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(22, 266);
            panel3.TabIndex = 176;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(19, 0);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(479, 18);
            panel1.TabIndex = 171;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(-2, 248);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(500, 18);
            panel2.TabIndex = 170;
            // 
            // panel8
            // 
            panel8.BackColor = Color.Navy;
            panel8.Location = new Point(-2, -1);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(22, 267);
            panel8.TabIndex = 164;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(82, 122);
            label3.Name = "label3";
            label3.Size = new Size(76, 19);
            label3.TabIndex = 182;
            label3.Text = "Nombre:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(128, 44);
            label4.Name = "label4";
            label4.Size = new Size(249, 34);
            label4.TabIndex = 331;
            label4.Text = "Cliente Existente";
            // 
            // cmbClientes
            // 
            cmbClientes.CueHint.Color1 = Color.DimGray;
            cmbClientes.CueHint.CueHintText = "Seleccione un cliente";
            cmbClientes.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbClientes.DropDownWidth = 300;
            cmbClientes.Location = new Point(164, 122);
            cmbClientes.Margin = new Padding(3, 2, 3, 2);
            cmbClientes.Name = "cmbClientes";
            cmbClientes.Size = new Size(227, 26);
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
            BtnSalir.Location = new Point(269, 177);
            BtnSalir.Margin = new Padding(3, 2, 3, 2);
            BtnSalir.Name = "BtnSalir";
            BtnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideDefault.Back.Color2 = Color.White;
            BtnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnSalir.OverrideFocus.Back.Color2 = Color.White;
            BtnSalir.Size = new Size(89, 45);
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
            BtnAsignar.Location = new Point(115, 177);
            BtnAsignar.Margin = new Padding(3, 2, 3, 2);
            BtnAsignar.Name = "BtnAsignar";
            BtnAsignar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            BtnAsignar.OverrideDefault.Back.Color2 = Color.White;
            BtnAsignar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            BtnAsignar.OverrideFocus.Back.Color2 = Color.White;
            BtnAsignar.Size = new Size(123, 45);
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
            label5.Location = new Point(382, 220);
            label5.Name = "label5";
            label5.Size = new Size(84, 29);
            label5.TabIndex = 346;
            label5.Text = "BAMS";
            // 
            // ClienteExistente
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(498, 266);
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
            Name = "ClienteExistente";
            ShowIcon = false;
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