using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SG_BAMS.Login
{
    public partial class LoginToken : Form
    {
        private string correo;
        private string token;

        public LoginToken(string correo, string token)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            correo = correo;
            token = token;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtToken.Text.Trim() == token)
            {
                LoginNueva CC = new LoginNueva(correo);
                CC.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Token incorrecto. Intente de nuevo.");
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Login LG = new Login();
            LG.Show();
            this.Hide();
        }
    }
}
