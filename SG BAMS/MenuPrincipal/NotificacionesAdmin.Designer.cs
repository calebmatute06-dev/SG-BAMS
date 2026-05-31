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
            cantidadnotificaciones = new Label();
            label3 = new Label();
            pictureBox4 = new PictureBox();
            pictureBox3 = new PictureBox();
            pictureBox1 = new PictureBox();
            pictureBox2 = new PictureBox();
            label6 = new Label();
            notificaciones = new ListBox();
            btnsalir1 = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)pictureBox4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // cantidadnotificaciones
            // 
            cantidadnotificaciones.AutoSize = true;
            cantidadnotificaciones.BackColor = Color.Navy;
            cantidadnotificaciones.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            cantidadnotificaciones.ForeColor = Color.White;
            cantidadnotificaciones.Location = new Point(299, 67);
            cantidadnotificaciones.Name = "cantidadnotificaciones";
            cantidadnotificaciones.Size = new Size(28, 33);
            cantidadnotificaciones.TabIndex = 103;
            cantidadnotificaciones.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Arial Narrow", 16.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(139, 67);
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
            label6.Location = new Point(181, 21);
            label6.Name = "label6";
            label6.Size = new Size(129, 44);
            label6.TabIndex = 97;
            label6.Text = "BAMS";
            // 
            // notificaciones
            // 
            notificaciones.DrawMode = DrawMode.OwnerDrawFixed;
            notificaciones.FormattingEnabled = true;
            notificaciones.Location = new Point(11, 123);
            notificaciones.Name = "notificaciones";
            notificaciones.Size = new Size(477, 384);
            notificaciones.TabIndex = 105;
            notificaciones.MouseClick += listBox1_MouseClick;
            notificaciones.SelectedIndexChanged += listBox1_SelectedIndexChanged;
            // 
            // btnsalir1
            // 
            btnsalir1.Location = new Point(397, 21);
            btnsalir1.Name = "btnsalir1";
            btnsalir1.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnsalir1.OverrideDefault.Back.Color2 = Color.White;
            btnsalir1.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnsalir1.OverrideFocus.Back.Color2 = Color.White;
            btnsalir1.Size = new Size(91, 59);
            btnsalir1.StateCommon.Back.Color1 = Color.SkyBlue;
            btnsalir1.StateCommon.Back.Color2 = Color.White;
            btnsalir1.StateCommon.Border.Rounding = 5F;
            btnsalir1.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnsalir1.StateCommon.Content.ShortText.Font = new Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnsalir1.StateNormal.Back.Color1 = Color.SkyBlue;
            btnsalir1.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnsalir1.StatePressed.Back.Color1 = Color.Transparent;
            btnsalir1.StatePressed.Back.Color2 = Color.Transparent;
            btnsalir1.TabIndex = 145;
            btnsalir1.Values.DropDownArrowColor = Color.Empty;
            btnsalir1.Values.Text = "Salir";
            btnsalir1.Click += btnsalir1_Click;
            // 
            // NotificacionesAdmin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(507, 549);
            Controls.Add(btnsalir1);
            Controls.Add(notificaciones);
            Controls.Add(cantidadnotificaciones);
            Controls.Add(label3);
            Controls.Add(pictureBox4);
            Controls.Add(pictureBox3);
            Controls.Add(pictureBox1);
            Controls.Add(pictureBox2);
            Controls.Add(label6);
            FormBorderStyle = FormBorderStyle.None;
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
        private Label cantidadnotificaciones;
        private Label label3;
        private PictureBox pictureBox4;
        private PictureBox pictureBox3;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Label label6;
        private ListBox notificaciones;
        private Krypton.Toolkit.KryptonButton btnsalir1;
    }
}