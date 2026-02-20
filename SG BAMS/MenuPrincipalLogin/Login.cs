using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.MenuPrincipalLogin
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }


        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btninicioSesion_Click(object sender, EventArgs e)
        {
            ClsLogin login = new ClsLogin();
            try
            {
                int rol = login.ValidarUsuario(txtUsu.Text, txtCon.Text);

                if (rol == 1)
                {
                    MessageBox.Show("Login correcto. ¡Bienvenido Administrador!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MenuPrincipalAdm MenAdm = new MenuPrincipalAdm();
                    MenAdm.Show();
                    txtUsu.Clear();
                    txtCon.Clear();
                    this.Hide();

                }
                else if (rol == 2)
                {
                    MessageBox.Show("Login correcto. ¡Bienvenido Empleado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MenuPrincipalEmp MenEmp = new MenuPrincipalEmp();
                    MenEmp.Show();
                    txtUsu.Clear();
                    txtCon.Clear();
                    this.Hide();

                }
                else if (rol == -1)
                {
                    MessageBox.Show("El usuario está inactivo. No puede ingresar.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUsu.Clear();
                    txtCon.Clear();
                }
                else
                {
                    MessageBox.Show("Error.....Usuario o contraseña incorrectos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtUsu.Clear();
                    txtCon.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnsalirLogin_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
