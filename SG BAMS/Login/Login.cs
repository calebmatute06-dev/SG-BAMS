using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario principal de inicio de sesión.
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

        private readonly ILoginService _loginService;
        private readonly IServicioCorreo _servicioCorreo;
        private readonly IServicioSeguridad _servicioSeguridad;
        private readonly ISesionUsuarioService _sesionUsuario;
        private readonly ControlBloqueoIntentos _controlBloqueo;
        private PasswordToggleHelper _passwordToggle;
        private readonly NavegadorPostLogin _navegadorPostLogin;
        private PlaceholderTextBox _phUsuario;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public Login() : this(
            new LoginService(),
            new ServicioCorreo(new ConfiguracionCorreo()),
            new ServicioSeguridad(),
            SesionUsuarioService.Instancia,
            new RepositorioRostros(DetectorRostroService.DirectorioRostros))
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// </summary>
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

            _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
            _servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
            _servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
            _sesionUsuario = sesionUsuario ?? throw new ArgumentNullException(nameof(sesionUsuario));


            _controlBloqueo = new ControlBloqueoIntentos(3, 30, lblBloqueo,
                txtUsuCorr, txtCon, btninicioSesion1);

 
            _navegadorPostLogin = new NavegadorPostLogin(repositorioRostros);

            txtCon.KeyPress += new KeyPressEventHandler(txtCon_KeyPress);
            _phUsuario = new PlaceholderTextBox(txtUsuCorr, "Ingrese Usuario o Correo valido");
        }

        /// <summary>
        /// Evento Load del formulario.
        /// </summary>
        private void Login_Load(object sender, EventArgs e)
        {

            _passwordToggle = new PasswordToggleHelper(txtCon, txtCon.Parent);
            lblBloqueo.Visible = false;
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtCon_KeyPress(object sender, KeyPressEventArgs e) { }

        /// <summary>
        /// Evento Click del botón Iniciar Sesión.
        /// Delega autenticación a ILoginService, bloqueo a ControlBloqueoIntentos,
        /// y navegación a NavegadorPostLogin.
        /// </summary>
        private void btninicioSesion1_Click(object sender, EventArgs e)
        {
            if (ClsValidaciones.CampoVacio(txtUsuCorr, "Usuario")) return;
            if (ClsValidaciones.CampoVacio(txtCon, "Contraseña")) return;

            if (_passwordToggle != null && _passwordToggle.EsPlaceholder)
            {
                MessageBox.Show("Ingrese una contraseña.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCon.Focus();
                return;
            }

            if (!ClsValidaciones.EsPasswordValido(txtCon, "La contraseña")) return;

            try
            {

                int rol = _loginService.ValidarUsuario(txtUsuCorr.Text, txtCon.Text);
                string nombreUsuario = _loginService.ObtenerNombreUsuario();
                string nombreCompleto = _loginService.ObtenerNombreCompleto();


                _sesionUsuario.NombreUsuario = nombreUsuario;
                _sesionUsuario.NombreCompleto = nombreCompleto;
                _sesionUsuario.RolUsuario = rol;


                bool loginExitoso = _navegadorPostLogin.Navegar(rol, this, nombreUsuario);

                if (!loginExitoso && rol == 0)
                {
                    bool bloqueado = _controlBloqueo.RegistrarIntentoFallido();

                    if (bloqueado)
                    {
                        MessageBox.Show(
                            $"Ha superado el número máximo de intentos.\nEl acceso estará bloqueado por 30 segundos.",
                            "Acceso Bloqueado", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show(
                            $"Usuario o contraseña incorrectos.\nIntentos restantes: {_controlBloqueo.IntentosRestantes}",
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

        /// <summary>
        /// Evento Click del botón "Olvidé mi contraseña".
        /// Abre el formulario de recuperación inyectando las dependencias necesarias.
        /// 
        /// DIP: Crea IRecuperacionService con sus dependencias concretas
        /// y las inyecta en LoginCorreo.
        /// </summary>
        private void btnOlvidar_Click(object sender, EventArgs e)
        {
            IRecuperacionService recuperacionService = new RecuperacionService(
                new ClsRecuperacion(new ClsRepositorioBaseDatos(), _servicioSeguridad));

            LoginCorreo formularioCorreo = new LoginCorreo(
                txtUsuCorr.Text.Trim(),
                recuperacionService,
                _servicioSeguridad,
                _servicioCorreo);

            formularioCorreo.Show();
        }

        private void Nombre_Click(object sender, EventArgs e) { }
    }
}