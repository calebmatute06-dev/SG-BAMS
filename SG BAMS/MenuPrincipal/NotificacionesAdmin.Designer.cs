namespace SG_BAMS
{
    partial class NotificacionesAdmin
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
            btnsalir = new Krypton.Toolkit.KryptonButton();
            label1 = new Label();
            label3 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label6 = new Label();
            listBox1 = new ListBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // btnsalir
            // 
            btnsalir.Location = new Point(413, 20);
            btnsalir.Name = "btnsalir";
            btnsalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideDefault.Back.Color2 = Color.White;
            btnsalir.OverrideDefault.Border.Rounding = 40F;
            btnsalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalir.OverrideFocus.Back.Color2 = Color.White;
            btnsalir.Size = new Size(76, 44);
            btnsalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalir.StateCommon.Back.Color2 = Color.White;
            btnsalir.StateCommon.Border.Rounding = 40F;
            btnsalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalir.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnsalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalir.StateNormal.Back.Color2 = Color.White;
            btnsalir.StateNormal.Border.Rounding = 40F;
            btnsalir.StateTracking.Border.Rounding = 40F;
            btnsalir.TabIndex = 104;
            btnsalir.Values.DropDownArrowColor = Color.Empty;
            btnsalir.Values.Text = "Salir";
            btnsalir.Click += btnsalir_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.BackColor = Color.Navy;
            label1.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(299, 66);
            label1.Name = "label1";
            label1.Size = new Size(28, 33);
            label1.TabIndex = 103;
            label1.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(140, 66);
            label3.Name = "label3";
            label3.Size = new Size(153, 33);
            label3.TabIndex = 102;
            label3.Text = "Notificaciones";
            // 
            // pictureBox4
            // 
            pictureBox4.BackColor = Color.Navy;
            pictureBox4.Location = new Point(-6, -5);
            pictureBox4.Name = "pictureBox4";
            pictureBox4.Size = new Size(15, 557);
            pictureBox4.TabIndex = 101;
            pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            pictureBox3.BackColor = Color.Navy;
            pictureBox3.Location = new Point(495, -5);
            pictureBox3.Name = "pictureBox3";
            pictureBox3.Size = new Size(15, 557);
            pictureBox3.TabIndex = 100;
            pictureBox3.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.BackColor = Color.Navy;
            pictureBox1.Location = new Point(-6, 536);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(515, 15);
            pictureBox1.TabIndex = 99;
            pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            pictureBox2.BackColor = Color.Navy;
            pictureBox2.Location = new Point(-6, -1);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(515, 15);
            pictureBox2.TabIndex = 98;
            pictureBox2.TabStop = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Arial", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.Navy;
            label6.Location = new Point(181, 22);
            label6.Name = "label6";
            label6.Size = new Size(129, 44);
            label6.TabIndex = 97;
            label6.Text = "BAMS";
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(12, 122);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(477, 404);
            listBox1.TabIndex = 105;
            listBox1.MouseClick += listBox1_MouseClick;
            listBox1.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // NotificacionesAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(508, 550);
            Controls.Add(listBox1);
            Controls.Add(btnsalir);
            Controls.Add(label1);
            Controls.Add(label3);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(label6);
            Name = "NotificacionesAdmin";
            Text = "NotificacionesAdmin";
            Load += NotificacionesAdmin_Load;
            Shown += NotificacionesAdmin_Shown;
            ((System.ComponentModel.ISupportInitialize)pictureBox4).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Krypton.Toolkit.KryptonButton btnsalir;
        private Label label1;
        private Label label3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label6;
        private ListBox listBox1;
    }
}