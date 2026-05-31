namespace SG_BAMS
{
    partial class frmModificarMarcaProducto
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
            panel1 = new Panel();
            pictureBox2 = new PictureBox();
            pictureBox1 = new PictureBox();
            panel2 = new Panel();
            label2 = new Label();
            txtDescri = new Krypton.Toolkit.KryptonTextBox();
            btnModificar = new Krypton.Toolkit.KryptonButton();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            label5 = new Label();
            label8 = new Label();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BackColor = Color.Navy;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(683, 24);
            panel1.TabIndex = 99;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(24, 307);
            pictureBox2.TabIndex = 100;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(659, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 307);
            pictureBox1.TabIndex = 92;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 283);
            panel2.Name = "panel2";
            panel2.Size = new Size(683, 24);
            panel2.TabIndex = 98;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(39, 121);
            label2.Name = "label2";
            label2.Size = new Size(301, 31);
            label2.TabIndex = 91;
            label2.Text = "Ingrese la marca de producto:";
            // 
            // txtDescri
            // 
            txtDescri.CueHint.Color1 = Color.DimGray;
            txtDescri.CueHint.CueHintText = "Letras y números. Sin letras sueltas.";
            txtDescri.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescri.Location = new Point(336, 118);
            txtDescri.Margin = new Padding(3, 4, 3, 4);
            txtDescri.MaxLength = 70;
            txtDescri.Multiline = true;
            txtDescri.Name = "txtDescri";
            txtDescri.Size = new Size(302, 39);
            txtDescri.StateCommon.Back.Color1 = Color.White;
            txtDescri.StateCommon.Border.Color1 = Color.Navy;
            txtDescri.StateCommon.Border.Rounding = 5F;
            txtDescri.StateCommon.Content.Color1 = Color.Black;
            txtDescri.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtDescri.TabIndex = 142;
            // 
            // btnModificar
            // 
            btnModificar.Location = new Point(197, 193);
            btnModificar.Name = "btnModificar";
            btnModificar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideDefault.Back.Color2 = Color.White;
            btnModificar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnModificar.OverrideFocus.Back.Color2 = Color.White;
            btnModificar.Size = new Size(143, 65);
            btnModificar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnModificar.StateCommon.Back.Color2 = Color.White;
            btnModificar.StateCommon.Border.Rounding = 5F;
            btnModificar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnModificar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnModificar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnModificar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnModificar.StatePressed.Back.Color1 = Color.Transparent;
            btnModificar.StatePressed.Back.Color2 = Color.Transparent;
            btnModificar.TabIndex = 344;
            btnModificar.Values.DropDownArrowColor = Color.Empty;
            btnModificar.Values.Text = "Modificar";
            btnModificar.Click += btnModificar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(365, 192);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(143, 65);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 5F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 345;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.BackColor = Color.Transparent;
            label5.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.Navy;
            label5.Location = new Point(551, 245);
            label5.Name = "label5";
            label5.Size = new Size(102, 35);
            label5.TabIndex = 347;
            label5.Text = "BAMS";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 16.2F, FontStyle.Bold);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(148, 54);
            label8.Name = "label8";
            label8.Size = new Size(452, 33);
            label8.TabIndex = 354;
            label8.Text = "Modificar la marca de productos";
            // 
            // frmModificarMarcaProducto
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(683, 307);
            Controls.Add(label8);
            Controls.Add(label5);
            Controls.Add(btnSalir);
            Controls.Add(btnModificar);
            Controls.Add(txtDescri);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(label2);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "frmModificarMarcaProducto";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "frmModificarMarcaProducto";
            Load += frmModificarMarcaProducto_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Label label2;
        private Krypton.Toolkit.KryptonTextBox txtDescri;
        private Krypton.Toolkit.KryptonButton btnModificar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Label label5;
        private Label label8;
    }
}