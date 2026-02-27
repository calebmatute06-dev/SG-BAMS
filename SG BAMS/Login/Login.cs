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

namespace SG_BAMS.Login
{
    public partial class Login : Form
    {
        public static string UsuarioLogueado;
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
                UsuarioLogueado = txtUsu.Text;

                int rol = login.ValidarUsuario(txtUsu.Text, txtCon.Text);

              
                switch (rol)
                {
                    case 1:
                        {
                            LoginFacial validacionFacial = new LoginFacial();
                            validacionFacial.UsuarioAValidar = txtUsu.Text;

                            
                            if (validacionFacial.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show("Login correcto. ¡Bienvenido Administrador!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                new MenuPrincipalAdm().Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Validación facial fallida. Acceso denegado.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;

                    case 2:
                        {
                            LoginFacial validacionFacial = new LoginFacial();
                            validacionFacial.UsuarioAValidar = txtUsu.Text;

                           
                            if (validacionFacial.ShowDialog() == DialogResult.OK)
                            {
                                MessageBox.Show("Login correcto. ¡Bienvenido Empleado!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                new MenuPrincipalEmp().Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Validación facial fallida. Acceso denegado.", "Seguridad", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        break;

                    case -1: 
                        MessageBox.Show("El usuario está inactivo. No puede ingresar.", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 0: 
                    default:
                        MessageBox.Show("Error.....Usuario o contraseña incorrectos.", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }

                
                txtUsu.Clear();
                txtCon.Clear();
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

        private void Login_Load(object sender, EventArgs e)
        {

        }
    }
}