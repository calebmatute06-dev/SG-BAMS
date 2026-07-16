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
        private PlaceholderTextBox phUsuario;
       

        // Variables para el control de intentos
        private int intentosFallidos = 0;
        private const int MaxIntentos = 3;
        private const int SegundosBloqueo = 30;
        private System.Windows.Forms.Timer timerBloqueo;
        private int segundosRestantes;

        public Login()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            txtCon.KeyPress += new KeyPressEventHandler(txtCon_KeyPress);
            phUsuario = new PlaceholderTextBox(txtUsuCorr, "Ingrese Usuario o Correo valido");
            InicializarTimer();
        }

        /// <summary>
        /// Inicializa el timer de bloqueo
        /// </summary>
        private void InicializarTimer()
        {
            timerBloqueo = new System.Windows.Forms.Timer();
            timerBloqueo.Interval = 1000; // 1 segundo
            timerBloqueo.Tick += TimerBloqueo_Tick;
        }

        /// <summary>
        /// Activa el bloqueo del formulario por 30 segundos
        /// </summary>
        private void ActivarBloqueo()
        {
            segundosRestantes = SegundosBloqueo;

            txtUsuCorr.Enabled = false;
            txtCon.Enabled = false;
            btninicioSesion1.Enabled = false;

            lblBloqueo.Visible = true;
            lblBloqueo.Text = $"⛔ Cuenta bloqueada. Espere {segundosRestantes} segundos...";

            timerBloqueo.Start();
        }

        /// <summary>
        /// Tick del timer: descuenta segundos y desbloquea al llegar a 0
        /// </summary>
        private void TimerBloqueo_Tick(object sender, EventArgs e)
        {
            segundosRestantes--;
            lblBloqueo.Text = $"⛔ Cuenta bloqueada. Espere {segundosRestantes} segundos...";

            if (segundosRestantes <= 0)
            {
                timerBloqueo.Stop();
                DesactivarBloqueo();
            }
        }

        /// <summary>
        /// Desactiva el bloqueo y restaura los controles
        /// </summary>
        private void DesactivarBloqueo()
        {
            txtUsuCorr.Enabled = true;
            txtCon.Enabled = true;
            btninicioSesion1.Enabled = true;

            lblBloqueo.Visible = false;
            intentosFallidos = 0;

            txtUsuCorr.Clear();
            txtCon.Clear();
            txtUsuCorr.Focus();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void txtCon_KeyPress(object sender, KeyPressEventArgs e)
        {
        }

        private void btninicioSesion1_Click(object sender, EventArgs e)
        {

            if (ClsValidaciones.CampoVacio(txtUsuCorr, "Usuario")) return;
            if (ClsValidaciones.CampoVacio(txtCon, "Contraseña")) return;
            if (placeholderPassword)
            {
                MessageBox.Show("Ingrese una contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCon.Focus();
                return;
            }
            if (!ClsValidaciones.EsPasswordValido(txtCon, "La contraseña")) return;

            ClsLogin login = new ClsLogin();

            try
            {
                int rol = login.ValidarUsuario(txtUsuCorr.Text, txtCon.Text);
                UsuarioLogueado = login.NombreUsuario;

                switch (rol)
                {
                    case 1:
                        var archivos = Directory.GetFiles(DetectorRostroService.DirectorioRostros, "*.jpg")
                            .Where(f => Path.GetFileNameWithoutExtension(f) == UsuarioLogueado ||
                                        Path.GetFileNameWithoutExtension(f).StartsWith(UsuarioLogueado + "_"))
                            .ToList();

                        if (archivos.Count == 0)
                        {
                            MessageBox.Show($"El usuario '{UsuarioLogueado}' no tiene un registro facial registrado.",
                                "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        MessageBox.Show("Login correcto. ¡Bienvenido Administrador!", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginFacial frmFacialAdm = new LoginFacial();
                        frmFacialAdm.UsuarioAValidar = UsuarioLogueado;
                        frmFacialAdm.RolAsignado = rol;
                        frmFacialAdm.Show();
                        this.Hide();
                        break;

                    case 2:
                        var archivos2 = Directory.GetFiles(DetectorRostroService.DirectorioRostros, "*.jpg")
                            .Where(f => Path.GetFileNameWithoutExtension(f) == UsuarioLogueado ||
                                        Path.GetFileNameWithoutExtension(f).StartsWith(UsuarioLogueado + "_"))
                            .ToList();

                        if (archivos2.Count == 0)
                        {
                            MessageBox.Show($"El usuario '{UsuarioLogueado}' no tiene un registro facial registrado.",
                                "Sin registro facial", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        }

                        MessageBox.Show("Login correcto. ¡Bienvenido Empleado!", "Éxito",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoginFacial frmFacial = new LoginFacial();
                        frmFacial.UsuarioAValidar = UsuarioLogueado;
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
                        MessageBox.Show("El usuario está inactivo. No puede ingresar.",
                            "Cuenta Inactiva", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        break;

                    case 0:
                    default:
                        intentosFallidos++;
                        int intentosRestantes = MaxIntentos - intentosFallidos;

                        if (intentosFallidos >= MaxIntentos)
                        {
                            MessageBox.Show(
                                $"Ha superado el número máximo de intentos.\nEl acceso estará bloqueado por {SegundosBloqueo} segundos.",
                                "Acceso Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            ActivarBloqueo();
                        }
                        else
                        {
                            MessageBox.Show(
                                $"Usuario o contraseña incorrectos.\nIntentos restantes: {intentosRestantes}",
                                "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        break;
                }

                txtCon.Clear();
                txtCon.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de conexión: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnsalirLogin1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private bool _passwordVisible = false;
        private Label lblOjo;
        private bool placeholderPassword = true;
        private const string textoPlaceholder = "Ingrese su contraseña";

        private void Login_Load(object sender, EventArgs e)
        {
            phUsuario = new PlaceholderTextBox(txtUsuCorr, "Ingrese Usuario o Correo valido");
            txtCon.Text = textoPlaceholder;
            txtCon.ForeColor = Color.Gray;
            txtCon.UseSystemPasswordChar = false;

            txtCon.Enter += TxtCon_Enter;
            txtCon.Leave += TxtCon_Leave;

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

            if (!placeholderPassword)
            {
                txtCon.UseSystemPasswordChar = true;
            }
            else
            {
                txtCon.UseSystemPasswordChar = false;
            }

            lblOjo.Text = "👁";


            lblOjo.Click += (s, ev) =>
            {
                if (placeholderPassword)
                    return;
                _passwordVisible = !_passwordVisible;
                txtCon.UseSystemPasswordChar = !_passwordVisible;
                lblOjo.Text = _passwordVisible ? "🙈" : "👁";
            };

            txtCon.Parent.Controls.Add(lblOjo);
            lblOjo.BringToFront();


            lblBloqueo.Visible = false;




        }

        private void btnOlvidar_Click(object sender, EventArgs e)
        {
            LoginCorreo LC = new LoginCorreo(txtUsuCorr.Text.Trim());
            LC.Show();
           
        }

        private void Nombre_Click(object sender, EventArgs e)
        {

        }
        private void TxtCon_Enter(object sender, EventArgs e)
        {
            if (placeholderPassword)
            {
                txtCon.Clear();
                txtCon.ForeColor = Color.Black;

                if (!_passwordVisible)
                    txtCon.UseSystemPasswordChar = true;

                placeholderPassword = false;
            }
        }
        private void TxtCon_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtCon.Text))
            {
                txtCon.Text = textoPlaceholder;
                txtCon.ForeColor = Color.Gray;
                txtCon.UseSystemPasswordChar = false;
                placeholderPassword = true;
            }
        }
    }


}