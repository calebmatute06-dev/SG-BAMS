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
    public partial class LoginCorreo : Form
    {
        public LoginCorreo()
        {
            InitializeComponent();
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string correo = txtCorreo.Text.Trim();

            if (string.IsNullOrEmpty(correo))
            {
                MessageBox.Show("Ingrese su correo.");
                return;
            }

            ClsRecuperacion rec = new ClsRecuperacion();

            if (!rec.VerificarCorreo(correo))
            {
                MessageBox.Show("El correo no está registrado.");
                return;
            }

            string token = ClsSeguridad.GenerarToken();

            try
            {
                ClsCorreo.EnviarToken(correo, token);
                MessageBox.Show("Token enviado a su correo.");

                LoginToken LT = new LoginToken(correo, token);
                LT.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message);
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
