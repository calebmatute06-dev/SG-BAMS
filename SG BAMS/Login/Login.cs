using Microsoft.VisualBasic.Logging;
using SG_BAMS.Administracion_de_BAMS.Usuarios;
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

            txtUsu.KeyPress += new KeyPressEventHandler(txtUsu_KeyPress);
            txtCon.KeyPress += new KeyPressEventHandler(txtCon_KeyPress);
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void txtUsu_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void txtCon_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void btninicioSesion1_Click(object sender, EventArgs e)
        {
            if (ClsValidaciones.CampoVacio(txtUsu, "Usuario")) return;
            if (ClsValidaciones.CampoVacio(txtCon, "Contraseña")) return;


            if (!ClsValidaciones.EsPasswordValido(txtCon, "La contraseña")) return;

            ClsLogin login = new ClsLogin();

            try
            {
                UsuarioLogueado = txtUsu.Text;
                int rol = login.ValidarUsuario(txtUsu.Text, txtCon.Text);

                switch (rol)
                {
                    case 1:
                        var archivos = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                            .Where(f => Path.GetFileNameWithoutExtension(f) == txtUsu.Text ||
                                        Path.GetFileNameWithoutExtension(f).StartsWith(txtUsu.Text + "_"))
                            .ToList();

                        if (archivos.Count == 0)
                        {
                            MessageBox.Show($"El usuario '{txtUsu.Text}' no tiene un registro facial registrado.",
                                "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        string bienvenida = rol == 1 ? "¡Bienvenido Administrador!" : "¡Bienvenido Empleado!";
                        MessageBox.Show($"Login correcto. {bienvenida}", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginFacial frmFacialAdm = new LoginFacial();
                        frmFacialAdm.UsuarioAValidar = txtUsu.Text;
                        frmFacialAdm.RolAsignado = rol;
                        frmFacialAdm.Show();
                        this.Hide();
                        break;

                    case 2:
                        var archivos2 = Directory.GetFiles(clsSoporte.DirectorioRostros, "*.jpg")
                            .Where(f => Path.GetFileNameWithoutExtension(f) == txtUsu.Text ||
                                        Path.GetFileNameWithoutExtension(f).StartsWith(txtUsu.Text + "_"))
                            .ToList();

                        if (archivos2.Count == 0)
                        {
                            MessageBox.Show($"El usuario '{txtUsu.Text}' no tiene un registro facial registrado.",
                                "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        string bienvenida2 = rol == 1 ? "¡Bienvenido Administrador!" : "¡Bienvenido Empleado!";
                        MessageBox.Show($"Login correcto. {bienvenida2}", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginFacial frmFacial = new LoginFacial();
                        frmFacial.UsuarioAValidar = txtUsu.Text;
                        frmFacial.RolAsignado = rol;
                        frmFacial.Show();
                        this.Hide();
                        break;

                    case -1:
                        MessageBox.Show("El usuario está inactivo. No puede ingresar.", "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 0:
                    default:
                        MessageBox.Show("Usuario o contraseña incorrectos.", "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }


                txtCon.Clear();
                txtCon.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsalirLogin1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

       
    }
}