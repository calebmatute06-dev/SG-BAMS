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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Login : Form
    {
        /// <summary>
        /// El usuario logueado
        /// </summary>
        public static string UsuarioLogueado;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Login" />.
        /// </summary>
        public Login()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtUsu.KeyPress += new KeyPressEventHandler(txtUsu_KeyPress);
            txtCon.KeyPress += new KeyPressEventHandler(txtCon_KeyPress);
        }

        /// <summary>
        /// Maneja el evento Click del control btnSalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        /// <summary>
        /// Maneja el evento KeyPress del control txtUsu.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtUsu_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtCon.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs" /> que contiene los datos del evento.</param>
        private void txtCon_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Click del control btninicioSesion1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
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
                    case 3:
                        MessageBox.Show("Login correcto. ¡Bienvenido Soporte!", "Éxito",
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Soporte soporte = new Soporte();
                        soporte.Show();
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

        /// <summary>
        /// Maneja el evento Click del control btnsalirLogin1.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnsalirLogin1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        /// <summary>
        /// La visibilidad de la contraseña
        /// </summary>
        private bool _passwordVisible = false;
        /// <summary>
        /// La etiqueta del ojo
        /// </summary>
        private Label lblOjo;

        /// <summary>
        /// Maneja el evento Load del control Login.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Login_Load(object sender, EventArgs e)
        {
            lblOjo = new Label();
            lblOjo.Text = "👁";
            lblOjo.Font = new Font("Arial", 13);
            lblOjo.AutoSize = false;
            lblOjo.Size = new Size(32, 32);
            lblOjo.TextAlign = ContentAlignment.MiddleCenter;
            lblOjo.Cursor = Cursors.Hand;
            lblOjo.BackColor = Color.Transparent;

            lblOjo.Location = new Point(
                txtCon.Right + 5,
                txtCon.Top + (txtCon.Height - 32) / 2
            );

            _passwordVisible = false;
            txtCon.UseSystemPasswordChar = true;
            lblOjo.Text = "👁";

            lblOjo.Click += (s, ev) =>
            {
                _passwordVisible = !_passwordVisible;
                txtCon.UseSystemPasswordChar = !_passwordVisible;
                lblOjo.Text = _passwordVisible ? "🙈" : "👁";
            };

            txtCon.Parent.Controls.Add(lblOjo);
            lblOjo.BringToFront();
        }
    }
}