using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario para la validación del token de recuperación de contraseña.
    /// Permite al usuario ingresar el token recibido por correo y, si es válido,
    /// continuar con el establecimiento de una nueva contraseña.
    /// </summary>
    public partial class LoginToken : Form
    {
        private readonly string correo;
        private readonly string token;
        private readonly IValidadorTokenService validadorToken;
        private readonly IRecuperacionService recuperacionService;
        private readonly INavegacionFormsService navegacionForms;

        /// <summary>
        /// Constructor principal del formulario de validación de token.
        /// </summary>
        /// <param name="correo">Correo del usuario que solicitó la recuperación.</param>
        /// <param name="token">Token generado que debe ser validado.</param>
        /// <param name="validadorToken">Servicio de validación de tokens.</param>
        /// <param name="recuperacionService">Servicio de recuperación de contraseña.</param>
        /// <param name="navegacionForms">Servicio de navegación entre formularios.</param>
        /// <exception cref="ArgumentNullException">Si algún parámetro es nulo o vacío.</exception>
        public LoginToken(
            string correo,
            string token,
            IValidadorTokenService validadorToken,
            IRecuperacionService recuperacionService,
            INavegacionFormsService navegacionForms)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.correo = !string.IsNullOrWhiteSpace(correo)
                ? correo
                : throw new ArgumentNullException(nameof(correo));
            this.token = !string.IsNullOrWhiteSpace(token)
                ? token
                : throw new ArgumentNullException(nameof(token));
            this.validadorToken = validadorToken ?? throw new ArgumentNullException(nameof(validadorToken));
            this.recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            this.navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <summary>
        /// Evento Click del botón Confirmar.
        /// Valida el token ingresado contra el token generado por el sistema.
        /// Si es correcto, navega al formulario de nueva contraseña.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string tokenIngresado = txtToken.Text.Trim();

            if (string.IsNullOrEmpty(tokenIngresado))
            {
                MessageBox.Show("Ingrese el token recibido.");
                return;
            }

            if (validadorToken.ValidarToken(correo, tokenIngresado, token))
            {
                navegacionForms.NavegarANuevaContrasena(correo, recuperacionService);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Token incorrecto. Intente de nuevo.");
            }
        }

        /// <summary>
        /// Evento Click del botón Salir. Regresa al formulario de inicio de sesión.
        /// </summary>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            navegacionForms.NavegarAlLogin();
            navegacionForms.CerrarFormulario(this);
        }
    }
}