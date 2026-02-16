namespace SG_BAMS
{
    partial class fmrAgregarUsuarios
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
            label2 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            label3 = new Label();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            label9 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.BackColor = Color.Transparent;
            label1.Font = new Font("Segoe UI", 14F);
            label1.ForeColor = Color.Navy;
            label1.Location = new Point(65, 9);
            label1.Name = "label1";
            label1.Size = new Size(245, 29);
            label1.TabIndex = 1;
            label1.Text = "Ingresar Usuarios";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Segoe UI", 9F);
            label2.ForeColor = Color.Navy;
            label2.Location = new Point(65, 48);
            label2.Name = "label2";
            label2.Size = new Size(97, 15);
            label2.TabIndex = 2;
            label2.Text = "Nombre Usuario:";
            label2.Click += label2_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = Color.SkyBlue;
            textBox1.Location = new Point(188, 45);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(154, 23);
            textBox1.TabIndex = 3;
            // 
            // textBox2
            // 
            textBox2.BackColor = Color.SkyBlue;
            textBox2.Location = new Point(188, 84);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(154, 23);
            textBox2.TabIndex = 5;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.BackColor = Color.Transparent;
            label3.Font = new Font("Segoe UI", 9F);
            label3.ForeColor = Color.Navy;
            label3.Location = new Point(65, 87);
            label3.Name = "label3";
            label3.Size = new Size(70, 15);
            label3.TabIndex = 4;
            label3.Text = "Contraseña:";
            // 
            // button1
            // 
            button1.BackColor = Color.SkyBlue;
            button1.ForeColor = Color.Navy;
            button1.Location = new Point(112, 165);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 6;
            button1.Text = "Agregar";
            button1.UseVisualStyleBackColor = false;
            // 
            // button2
            // 
            button2.BackColor = Color.SkyBlue;
            button2.ForeColor = Color.Navy;
            button2.Location = new Point(235, 165);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 7;
            button2.Text = "Salir";
            button2.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            button3.BackColor = Color.SkyBlue;
            button3.ForeColor = Color.Navy;
            button3.Location = new Point(87, 116);
            button3.Name = "button3";
            button3.Size = new Size(75, 43);
            button3.TabIndex = 8;
            button3.Text = "Imagen del empleado";
            button3.UseVisualStyleBackColor = false;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.BackColor = Color.Transparent;
            label9.Font = new Font("Segoe UI", 15F);
            label9.ForeColor = Color.Navy;
            label9.Location = new Point(372, 175);
            label9.Name = "label9";
            label9.Size = new Size(65, 28);
            label9.TabIndex = 17;
            label9.Text = "BAMS";
            // 
            // fmrAgregarUsuarios
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(449, 212);
            Controls.Add(label9);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(label3);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "fmrAgregarUsuarios";
            Text = "fmrAgregarUsuarios";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private TextBox textBox1;
        private TextBox textBox2;
        private Label label3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Label label9;
    }
}