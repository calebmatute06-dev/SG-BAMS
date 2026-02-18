namespace SG_BAMS
{
    partial class fmrImagenEmpleado
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
            kryptonButton1 = new Krypton.Toolkit.KryptonButton();
            label7 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Arial Narrow", 18F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(88, 9);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 74;
            label1.Text = "Imagen del Empleado";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // kryptonButton1
            // 
            kryptonButton1.Location = new Point(106, 52);
            kryptonButton1.Name = "kryptonButton1";
            kryptonButton1.Size = new Size(240, 45);
            kryptonButton1.StateCommon.Content.ShortText.Color1 = Color.Navy;
            kryptonButton1.StateCommon.Content.ShortText.Font = new Font("Arial Narrow", 20.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            kryptonButton1.StateCommon.Content.ShortText.TextH = Krypton.Toolkit.PaletteRelativeAlign.Far;
            kryptonButton1.StateNormal.Back.Color1 = Color.SkyBlue;
            kryptonButton1.StateNormal.Back.Color2 = Color.SkyBlue;
            kryptonButton1.StateNormal.Border.Rounding = 15F;
            kryptonButton1.TabIndex = 75;
            kryptonButton1.Values.DropDownArrowColor = Color.Empty;
            kryptonButton1.Values.Text = "Subir Archivos";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Arial Narrow", 15.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.Navy;
            label7.Location = new Point(356, 122);
            label7.Name = "label7";
            label7.Size = new Size(60, 25);
            label7.TabIndex = 83;
            label7.Text = "BAMS";
            // 
            // fmrImagenEmpleado
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(419, 147);
            Controls.Add(label7);
            Controls.Add(kryptonButton1);
            Controls.Add(label1);
            Name = "fmrImagenEmpleado";
            Text = "fmrImagenEmpleado";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Krypton.Toolkit.KryptonButton kryptonButton1;
        private Label label7;
    }
}