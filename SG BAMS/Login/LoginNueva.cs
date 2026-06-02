using SG_BAMS.Login;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class LoginNueva : Form
    {
        private string correo;

        public LoginNueva(string correo)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.correo = correo;
        }

        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string pass1 = txtContra.Text.Trim();
            string pass2 = txtContraNueva.Text.Trim();

            

            if (string.IsNullOrEmpty(pass1) || string.IsNullOrEmpty(pass2))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            if (!ClsValidaciones.EsPasswordValido(txtContra, "La contraseña")) 
                return;

            if (!ClsValidaciones.EsPasswordValido(txtContraNueva, "La confirmación"))
                return;

            if (pass1 != pass2)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            string passHash = ClsSeguridad.HashSHA256(pass1);
            ClsRecuperacion rec = new ClsRecuperacion();

            if (rec.ContraIgualAntigua(correo, passHash))
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la actual.",
                                "Contraseña repetida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContra.Clear();
                txtContraNueva.Clear();
                txtContra.Focus();
                return;
            }

            if (rec.ActualizarContrasena(correo, passHash))
            {
                MessageBox.Show("Contraseña actualizada correctamente.");
                Login.Login LG = new Login.Login();
                LG.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña.");
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            Login.Login LG = new Login.Login();
            LG.Show();
            this.Hide();
        }
    }


}
