using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario para el envío de token de recuperación de contraseña..
    /// </summary>
    public partial class LoginCorreo : Form
    {
        private readonly string _correo;
        private readonly IRecuperacionService _recuperacionService;
        private readonly IServicioSeguridad _servicioSeguridad;
        private readonly IServicioCorreo _servicioCorreo;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        public LoginCorreo(
            string correom,
            IRecuperacionService recuperacionService,
            IServicioSeguridad servicioSeguridad,
            IServicioCorreo servicioCorreo)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _correo = correom;
            _recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            _servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
            _servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
        }

        private void LoginCorreo_Load(object sender, EventArgs e)
        {
            txtCorreo.Text = _correo;
            txtCorreo.StateCommon.Content.Color1 = Color.Black;
        }

        /// <summary>
        /// Evento Click del botón Confirmar.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string correoIngresado = txtCorreo.Text.Trim();

            if (ClsValidaciones.CampoVacio(txtCorreo, "Correo"))
                return;

            if (!_recuperacionService.VerificarCorreo(correoIngresado))
            {
                MessageBox.Show("El correo no está registrado.");
                return;
            }

            string token = _servicioSeguridad.GenerarToken();

            try
            {
                _servicioCorreo.EnviarToken(correoIngresado, token);
                MessageBox.Show("Token enviado a su correo.");

                IValidadorTokenService validadorToken = new ValidadorTokenEnMemoria();
                INavegacionFormsService navegacionForms = new NavegacionFormsService(_servicioCorreo, _servicioSeguridad);

                LoginToken formularioToken = new LoginToken(
                    correoIngresado,
                    token,
                    validadorToken,
                    _recuperacionService,
                    navegacionForms);

                formularioToken.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message);
            }
        }

        private void btnsalir_Click(object sender, EventArgs e)
        {
            RegresarAlLogin();
        }

        private void RegresarAlLogin()
        {
            INavegacionFormsService navegacionForms = new NavegacionFormsService(_servicioCorreo, _servicioSeguridad);
            navegacionForms.NavegarAlLogin();
            this.Close();
        }
    }
}