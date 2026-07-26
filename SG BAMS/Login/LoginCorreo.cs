using System;
using System.Drawing;
using System.Windows.Forms;
using SG_BAMS.Dominio;
using SG_BAMS.LogicaNegocio.Login;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario para el envío de token de recuperación de contraseña.
    /// Permite al usuario solicitar un token que será enviado a su correo electrónico.
    /// </summary>
    public partial class LoginCorreo : Form
    {
        private readonly string correo;
        private readonly IRecuperacionService recuperacionService;
        private readonly IServicioSeguridad servicioSeguridad;
        private readonly IServicioCorreo servicioCorreo;

        /// <summary>
        /// Constructor principal del formulario de recuperación por correo.
        /// </summary>
        /// <param name="correom">Correo electrónico precargado en el campo de texto.</param>
        /// <param name="recuperacionService">Servicio de recuperación de contraseña.</param>
        /// <param name="servicioSeguridad">Servicio de seguridad para generación de tokens.</param>
        /// <param name="servicioCorreo">Servicio de envío de correos electrónicos.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public LoginCorreo(
            string correom,
            IRecuperacionService recuperacionService,
            IServicioSeguridad servicioSeguridad,
            IServicioCorreo servicioCorreo)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            correo = correom;
            this.recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            this.servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
            this.servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
        }

        /// <summary>
        /// Evento Load del formulario. Carga el correo precargado en el campo de texto.
        /// </summary>
        private void LoginCorreo_Load(object sender, EventArgs e)
        {
            txtCorreo.Text = correo;
            txtCorreo.StateCommon.Content.Color1 = Color.Black;
        }

        /// <summary>
        /// Evento Click del botón Confirmar.
        /// Verifica el correo, genera un token y lo envía al destinatario.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string correoIngresado = txtCorreo.Text.Trim();

            if (ClsValidaciones.CampoVacio(txtCorreo, "Correo"))
                return;

            if (!recuperacionService.VerificarCorreo(correoIngresado))
            {
                MessageBox.Show("El correo no está registrado.");
                return;
            }

            string token = servicioSeguridad.GenerarToken();

            try
            {
                servicioCorreo.EnviarToken(correoIngresado, token);
                MessageBox.Show("Token enviado a su correo.");

                IValidadorTokenService validadorToken = new ValidadorTokenEnMemoria();
                INavegacionFormsService navegacionForms = new NavegacionFormsService(servicioCorreo, servicioSeguridad);

                LoginToken formularioToken = new LoginToken(
                    correoIngresado,
                    token,
                    validadorToken,
                    recuperacionService,
                    navegacionForms);

                formularioToken.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al enviar correo: " + ex.Message);
            }
        }

        /// <summary>
        /// Evento Click del botón Salir. Regresa al formulario de login principal.
        /// </summary>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            RegresarAlLogin();
        }

        /// <summary>
        /// Navega de regreso al formulario de login principal y cierra este formulario.
        /// </summary>
        private void RegresarAlLogin()
        {
            INavegacionFormsService navegacionForms = new NavegacionFormsService(servicioCorreo, servicioSeguridad);
            navegacionForms.NavegarAlLogin();
            this.Close();
        }
    }
}