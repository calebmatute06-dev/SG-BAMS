
namespace SG_BAMS
{
    partial class frmAgregarFormaPago
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
            txtdescri = new Krypton.Toolkit.KryptonTextBox();
            label2 = new Label();
            btnAgregar = new Krypton.Toolkit.KryptonButton();
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
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(609, 18);
            panel1.TabIndex = 120;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(3, 2, 3, 2);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(21, 224);
            pictureBox2.TabIndex = 121;
            pictureBox2.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(588, 0);
            pictureBox1.Margin = new Padding(3, 2, 3, 2);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(21, 224);
            pictureBox1.TabIndex = 114;
            pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Navy;
            panel2.Location = new Point(0, 206);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(598, 18);
            panel2.TabIndex = 119;
            // 
            // txtdescri
            // 
            txtdescri.CueHint.Color1 = Color.DimGray;
            txtdescri.CueHint.CueHintText = "Letras y números. Sin letras sueltas.";
            txtdescri.CueHint.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtdescri.Location = new Point(312, 94);
            txtdescri.MaxLength = 70;
            txtdescri.Multiline = true;
            txtdescri.Name = "txtdescri";
            txtdescri.Size = new Size(264, 28);
            txtdescri.StateCommon.Back.Color1 = Color.White;
            txtdescri.StateCommon.Border.Color1 = Color.Navy;
            txtdescri.StateCommon.Border.Rounding = 5F;
            txtdescri.StateCommon.Content.Color1 = Color.Black;
            txtdescri.StateCommon.Content.Font = new Font("Arial Narrow", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtdescri.TabIndex = 116;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(26, 94);
            label2.Name = "label2";
            label2.Size = new Size(274, 25);
            label2.TabIndex = 113;
            label2.Text = "Ingrese el tipo de forma de pago:";
            // 
            // btnAgregar
            // 
            btnAgregar.Location = new Point(156, 145);
            btnAgregar.Margin = new Padding(3, 2, 3, 2);
            btnAgregar.Name = "btnAgregar";
            btnAgregar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideDefault.Back.Color2 = Color.White;
            btnAgregar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnAgregar.OverrideFocus.Back.Color2 = Color.White;
            btnAgregar.Size = new Size(125, 49);
            btnAgregar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateCommon.Back.Color2 = Color.White;
            btnAgregar.StateCommon.Border.Rounding = 5F;
            btnAgregar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnAgregar.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnAgregar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnAgregar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnAgregar.StatePressed.Back.Color1 = Color.Transparent;
            btnAgregar.StatePressed.Back.Color2 = Color.Transparent;
            btnAgregar.TabIndex = 344;
            btnAgregar.Values.DropDownArrowColor = Color.Empty;
            btnAgregar.Values.Text = "Agregar";
            btnAgregar.Click += btnAgregar_Click;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(312, 145);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(125, 49);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 5F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 346;
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
            label5.Location = new Point(493, 171);
            label5.Name = "label5";
            label5.Size = new Size(84, 29);
            label5.TabIndex = 347;
            label5.Text = "BAMS";
            label5.Click += label5_Click;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.BackColor = Color.Transparent;
            label8.Font = new Font("Arial", 16.2F, FontStyle.Bold);
            label8.ForeColor = Color.Navy;
            label8.Location = new Point(171, 40);
            label8.Name = "label8";
            label8.Size = new Size(269, 26);
            label8.TabIndex = 352;
            label8.Text = "Agregar la forma de pago";
            // 
            // frmAgregarFormaPago
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(606, 224);
            Controls.Add(label8);
            Controls.Add(label5);
            Controls.Add(btnSalir);
            Controls.Add(btnAgregar);
            Controls.Add(panel1);
            Controls.Add(pictureBox2);
            Controls.Add(pictureBox1);
            Controls.Add(panel2);
            Controls.Add(txtdescri);
            Controls.Add(label2);
            Name = "frmAgregarFormaPago";
            ShowIcon = false;
            StartPosition = FormStartPosition.CenterScreen;
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private Panel panel1;
        private PictureBox pictureBox2;
        private PictureBox pictureBox1;
        private Panel panel2;
        private Krypton.Toolkit.KryptonTextBox txtdescri;
        private Label label2;
        private Krypton.Toolkit.KryptonButton btnAgregar;
        private Krypton.Toolkit.KryptonButton btnSalir;
        private Label label5;
        private Label label8;
    }
}