using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio;
using SG_BAMS.LogicaNegocio.AdministracionBAMS;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario principal de inicio de sesión del sistema BAMS.
    /// Gestiona la autenticación de usuarios, el bloqueo por intentos fallidos
    /// y la navegación hacia los formularios correspondientes según el rol.
    /// </summary>
    public partial class Login : Form
    {
        public static string UsuarioLogueado
        {
            get => SesionUsuarioService.Instancia.NombreUsuario;
            private set => SesionUsuarioService.Instancia.NombreUsuario = value;
        }

        public static string UsuarioLogueadoCompleto
        {
            get => SesionUsuarioService.Instancia.NombreCompleto;
            private set => SesionUsuarioService.Instancia.NombreCompleto = value;
        }

        private readonly ILoginService loginService;
        private readonly IServicioCorreo servicioCorreo;
        private readonly IServicioSeguridad servicioSeguridad;
        private readonly ISesionUsuarioService sesionUsuario;
        private readonly ControlBloqueoIntentos controlBloqueo;
        private PasswordToggleHelper passwordToggle;
        private readonly NavegadorPostLogin navegadorPostLogin;
        private PlaceholderTextBox phUsuario;

        public Login() : this(
            new LoginService(),
            new ServicioCorreo(new ConfiguracionCorreo()),
            new ServicioSeguridad(),
            SesionUsuarioService.Instancia,
            new RepositorioRostros(DetectorRostroService.DirectorioRostros))
        {
        }

        public Login(
            ILoginService loginService,
            IServicioCorreo servicioCorreo,
            IServicioSeguridad servicioSeguridad,
            ISesionUsuarioService sesionUsuario,
            IRepositorioRostros repositorioRostros)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;

            this.loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            this.servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
            this.servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
            this.sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));

            controlBloqueo = new ControlBloqueoIntentos(3, 30, lblBloqueo,
                txtUsuCorr, txtCon, btninicioSesion1);

            navegadorPostLogin = new NavegadorPostLogin(repositorioRostros);

            txtCon.KeyPress += new KeyPressEventHandler(txtCon_KeyPress);
            phUsuario = new PlaceholderTextBox(txtUsuCorr, "Ingrese Usuario o Correo valido");
        }

        private void Login_Load(object sender, EventArgs e)
        {
            passwordToggle = new PasswordToggleHelper(txtCon, txtCon.Parent);
            lblBloqueo.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtCon_KeyPress(object sender, KeyPressEventArgs e) { }

        private void btninicioSesion1_Click(object sender, EventArgs e)
        {
            if (ClsValidaciones.CampoVacio(txtUsuCorr, "Usuario")) return;
            if (ClsValidaciones.CampoVacio(txtCon, "Contraseña")) return;

            if (passwordToggle != null && passwordToggle.EsPlaceholder)
            {
                MessageBox.Show("Ingrese una contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCon.Focus();
                return;
            }

            if (!ClsValidaciones.EsPasswordValido(txtCon, "La contraseña")) return;

            try
            {
                int rol = loginService.ValidarUsuario(txtUsuCorr.Text, txtCon.Text);
                string nombreUsuario = loginService.ObtenerNombreUsuario();
                string nombreCompleto = loginService.ObtenerNombreCompleto();

                sesionUsuario.NombreUsuario = nombreUsuario;
                sesionUsuario.NombreCompleto = nombreCompleto;
                sesionUsuario.RolUsuario = rol;

                bool loginExitoso = navegadorPostLogin.Navegar(rol, this, nombreUsuario);

                if (!loginExitoso && rol == 0)
                {
                    bool bloqueado = controlBloqueo.RegistrarIntentoFallido();

                    if (bloqueado)
                    {
                        MessageBox.Show(
                            $"Ha superado el número máximo de intentos.\nEl acceso estará bloqueado por 30 segundos.",
                            "Acceso Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Usuario o contraseña incorrectos.\nIntentos restantes: {controlBloqueo.IntentosRestantes}",
                            "Error de Acceso", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
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

        private void btnsalirLogin1_Click(object sender, EventArgs e) => Application.Exit();

        private void btnOlvidar_Click(object sender, EventArgs e)
        {
            IRecuperacionService recuperacionService = new RecuperacionService(
                new ClsRecuperacion(new ClsRepositorioBaseDatos(), servicioSeguridad));

            LoginCorreo formularioCorreo = new LoginCorreo(
                txtUsuCorr.Text.Trim(),
                recuperacionService,
                servicioSeguridad,
                servicioCorreo);

            formularioCorreo.Show();
        }

        private void Nombre_Click(object sender, EventArgs e) { }
    }
}