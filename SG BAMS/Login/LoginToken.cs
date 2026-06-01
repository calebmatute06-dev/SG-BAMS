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
        private string _correo;
        private string _token;

        public LoginToken(string correo, string token)
        {
            InitializeComponent();
            _correo = correo;
            _token = token;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            if (txtToken.Text.Trim() == _token)
            {
                LoginNueva CC = new LoginNueva(_correo);
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
