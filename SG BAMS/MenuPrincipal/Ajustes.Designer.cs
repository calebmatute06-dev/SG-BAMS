namespace SG_BAMS
{
    partial class Ajustes
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
            pictureBox4 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox2 = new PictureBox();
            label3 = new Label();
            label4 = new Label();
            cmbZoom = new Krypton.Toolkit.KryptonComboBox();
            btnsalir = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbZoom).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(653, 483);
            label1.Name = "label1";
            label1.Size = new Size(129, 44);
            label1.TabIndex = 0;
            label1.Text = "BAMS";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(0, 0);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(24, 545);
            pictureBox4.TabIndex = 65;
            pictureBox4.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(777, 0);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(24, 548);
            pictureBox1.TabIndex = 70;
            pictureBox1.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(0, 524);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(801, 24);
            pictureBox3.TabIndex = 72;
            pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(801, 24);
            pictureBox2.TabIndex = 71;
            pictureBox2.TabStop = false;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 25.8000011F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(305, 27);
            label3.Name = "label3";
            label3.Size = new Size(140, 52);
            label3.TabIndex = 74;
            label3.Text = "Ajustes";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.BackColor = Color.Transparent;
            label4.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.Navy;
            label4.Location = new Point(99, 237);
            label4.Name = "label4";
            label4.Size = new Size(189, 33);
            label4.TabIndex = 76;
            label4.Text = "Zoom de pantalla";
            // 
            // cmbZoom
            // 
            cmbZoom.DropDownWidth = 300;
            cmbZoom.Items.AddRange(new object[] { "100%", "110%", "120%", "130%", "140%", "150%" });
            cmbZoom.Location = new Point(315, 218);
            cmbZoom.Name = "cmbZoom";
            cmbZoom.Size = new Size(300, 52);
            cmbZoom.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
            cmbZoom.StateCommon.ComboBox.Border.Rounding = 40F;
            cmbZoom.StateCommon.ComboBox.Content.TextH = Krypton.Toolkit.PaletteRelativeAlign.Near;
            cmbZoom.StateCommon.Item.Content.ShortText.Color1 = Color.Navy;
            cmbZoom.StateCommon.Item.Content.ShortText.Font = new Font("Microsoft Sans Serif", 15F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cmbZoom.StateNormal.ComboBox.Border.Rounding = 40F;
            cmbZoom.TabIndex = 79;
            cmbZoom.SelectedIndexChanged += kryptonComboBox2_SelectedIndexChanged;
            // 
            // btnsalir
            // 
            btnsalir.Location = new Point(261, 443);
            btnsalir.Name = "btnsalir";
            btnsalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideDefault.Back.Color2 = Color.White;
            btnsalir.OverrideDefault.Border.Rounding = 40F;
            btnsalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideFocus.Back.Color2 = Color.White;
            btnsalir.Size = new Size(224, 53);
            btnsalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalir.StateCommon.Back.Color2 = Color.White;
            btnsalir.StateCommon.Border.Rounding = 40F;
            btnsalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalir.StateNormal.Back.Color2 = Color.White;
            btnsalir.StateNormal.Border.Rounding = 40F;
            btnsalir.StateTracking.Border.Rounding = 40F;
            btnsalir.TabIndex = 82;
            btnsalir.Values.DropDownArrowColor = Color.Empty;
            btnsalir.Values.Text = "Salir";
            btnsalir.Click += btnsalirLogin_Click;
            // 
            // Ajustes
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 547);
            Controls.Add(btnsalir);
            Controls.Add(cmbZoom);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox2);
            Controls.Add(label1);
            Name = "Ajustes";
            Text = "Ajustes";
            Load += Ajustes_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbZoom).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private PictureBox pictureBox4;
        private PictureBox pictureBox1;
        private PictureBox pictureBox3;
        private PictureBox pictureBox2;
        private Label label3;
        private Label label4;
        private Krypton.Toolkit.KryptonComboBox cmbZoom;
        private Krypton.Toolkit.KryptonButton btnsalir;
    }
}