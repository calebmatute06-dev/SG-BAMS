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
        public LoginToken()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            LoginNueva LN = new LoginNueva();
            LN.Show();
            this.Hide();
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Login LG = new Login();
            LG.Show();
            this.Hide();
        }
    }
}
