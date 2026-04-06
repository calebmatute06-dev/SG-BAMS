namespace SG_BAMS
{
    partial class AsistentedeIA
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
            lstIA = new ListBox();
            btnEnviar = new Krypton.Toolkit.KryptonButton();
            btnBorrar = new Krypton.Toolkit.KryptonButton();
            txtInfo = new Krypton.Toolkit.KryptonTextBox();
            label10 = new Label();
            kryptonGroupBox2 = new Krypton.Toolkit.KryptonGroupBox();
            btnSalir = new Krypton.Toolkit.KryptonButton();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).BeginInit();
            SuspendLayout();
            // 
            // lstIA
            // 
            lstIA.FormattingEnabled = true;
            lstIA.ItemHeight = 15;
            lstIA.Location = new Point(12, 72);
            lstIA.Name = "lstIA";
            lstIA.Size = new Size(869, 409);
            lstIA.TabIndex = 0;
            // 
            // btnEnviar
            // 
            btnEnviar.Location = new Point(635, 495);
            btnEnviar.Margin = new Padding(3, 2, 3, 2);
            btnEnviar.Name = "btnEnviar";
            btnEnviar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnEnviar.OverrideDefault.Back.Color2 = Color.White;
            btnEnviar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnEnviar.OverrideFocus.Back.Color2 = Color.White;
            btnEnviar.Size = new Size(125, 37);
            btnEnviar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnEnviar.StateCommon.Back.Color2 = Color.White;
            btnEnviar.StateCommon.Border.Rounding = 30F;
            btnEnviar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnEnviar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnEnviar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnEnviar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnEnviar.StatePressed.Back.Color1 = Color.Transparent;
            btnEnviar.StatePressed.Back.Color2 = Color.Transparent;
            btnEnviar.TabIndex = 152;
            btnEnviar.Values.DropDownArrowColor = Color.Empty;
            btnEnviar.Values.Text = "Enviar";
            btnEnviar.Click += btnEnviar_Click;
            // 
            // btnBorrar
            // 
            btnBorrar.Location = new Point(766, 495);
            btnBorrar.Margin = new Padding(3, 2, 3, 2);
            btnBorrar.Name = "btnBorrar";
            btnBorrar.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideDefault.Back.Color2 = Color.White;
            btnBorrar.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnBorrar.OverrideFocus.Back.Color2 = Color.White;
            btnBorrar.Size = new Size(125, 37);
            btnBorrar.StateCommon.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateCommon.Back.Color2 = Color.White;
            btnBorrar.StateCommon.Border.Rounding = 30F;
            btnBorrar.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnBorrar.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnBorrar.StateNormal.Back.Color1 = Color.SkyBlue;
            btnBorrar.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnBorrar.StatePressed.Back.Color1 = Color.Transparent;
            btnBorrar.StatePressed.Back.Color2 = Color.Transparent;
            btnBorrar.TabIndex = 153;
            btnBorrar.Values.DropDownArrowColor = Color.Empty;
            btnBorrar.Values.Text = "Borrar";
            btnBorrar.Click += btnBorrar_Click;
            // 
            // txtInfo
            // 
            txtInfo.Location = new Point(12, 503);
            txtInfo.Name = "txtInfo";
            txtInfo.Size = new Size(617, 29);
            txtInfo.StateCommon.Back.Color1 = Color.SkyBlue;
            txtInfo.StateCommon.Border.Rounding = 10F;
            txtInfo.TabIndex = 323;
            txtInfo.KeyDown += txtInfo_KeyDown;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.BackColor = Color.SkyBlue;
            label10.Font = new Font("Arial Black", 18F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label10.ForeColor = Color.Navy;
            label10.Location = new Point(408, 22);
            label10.Name = "label10";
            label10.Size = new Size(97, 33);
            label10.TabIndex = 330;
            label10.Text = "Ayuda";
            // 
            // kryptonGroupBox2
            // 
            kryptonGroupBox2.CaptionVisible = false;
            kryptonGroupBox2.Location = new Point(300, 12);
            kryptonGroupBox2.Size = new Size(301, 49);
            kryptonGroupBox2.StateCommon.Back.Color1 = Color.SkyBlue;
            kryptonGroupBox2.StateCommon.Border.Rounding = 50F;
            kryptonGroupBox2.TabIndex = 331;
            // 
            // btnSalir
            // 
            btnSalir.Location = new Point(756, 30);
            btnSalir.Margin = new Padding(3, 2, 3, 2);
            btnSalir.Name = "btnSalir";
            btnSalir.OverrideDefault.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideDefault.Back.Color2 = Color.White;
            btnSalir.OverrideFocus.Back.Color1 = Color.SkyBlue;
            btnSalir.OverrideFocus.Back.Color2 = Color.White;
            btnSalir.Size = new Size(125, 37);
            btnSalir.StateCommon.Back.Color1 = Color.SkyBlue;
            btnSalir.StateCommon.Back.Color2 = Color.White;
            btnSalir.StateCommon.Border.Rounding = 30F;
            btnSalir.StateCommon.Content.ShortText.Color1 = Color.Navy;
            btnSalir.StateCommon.Content.ShortText.Font = new Font("Arial", 12F, FontStyle.Bold);
            btnSalir.StateNormal.Back.Color1 = Color.SkyBlue;
            btnSalir.StateNormal.Back.ColorStyle = Krypton.Toolkit.PaletteColorStyle.Solid;
            btnSalir.StatePressed.Back.Color1 = Color.Transparent;
            btnSalir.StatePressed.Back.Color2 = Color.Transparent;
            btnSalir.TabIndex = 332;
            btnSalir.Values.DropDownArrowColor = Color.Empty;
            btnSalir.Values.Text = "Salir";
            btnSalir.Click += btnSalir_Click;
            // 
            // AsistentedeIA
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(893, 544);
            Controls.Add(btnSalir);
            Controls.Add(label10);
            Controls.Add(kryptonGroupBox2);
            Controls.Add(txtInfo);
            Controls.Add(btnBorrar);
            Controls.Add(btnEnviar);
            Controls.Add(lstIA);
            FormBorderStyle = FormBorderStyle.None;
            Name = "AsistentedeIA";
            Text = "AsistentedeIA";
            Load += AsistentedeIA_Load;
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2.Panel).EndInit();
            ((System.ComponentModel.ISupportInitialize)kryptonGroupBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox lstIA;
        private Krypton.Toolkit.KryptonButton btnEnviar;
        private Krypton.Toolkit.KryptonButton btnBorrar;
        private Krypton.Toolkit.KryptonTextBox txtInfo;
        private Label label10;
        private Krypton.Toolkit.KryptonGroupBox kryptonGroupBox2;
        private Krypton.Toolkit.KryptonButton btnSalir;
    }
}