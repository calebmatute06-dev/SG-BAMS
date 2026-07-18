using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Formulario para la validación del token de recuperación de contraseña.
    /// </summary>
    public partial class LoginToken : Form
    {
        private readonly string _correo;
        private readonly string _token;
        private readonly IValidadorTokenService _validadorToken;
        private readonly IRecuperacionService _recuperacionService;
        private readonly INavegacionFormsService _navegacionForms;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
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

            _correo = !string.IsNullOrWhiteSpace(correo)
                ? correo
                : throw new ArgumentNullException(nameof(correo));
            _token = !string.IsNullOrWhiteSpace(token)
                ? token
                : throw new ArgumentNullException(nameof(token));
            _validadorToken = validadorToken ?? throw new ArgumentNullException(nameof(validadorToken));
            _recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            _navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));
        }

        /// <summary>
        /// Evento Click del botón Confirmar.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string tokenIngresado = txtToken.Text.Trim();

            if (string.IsNullOrEmpty(tokenIngresado))
            {
                MessageBox.Show("Ingrese el token recibido.");
                return;
            }

            if (_validadorToken.ValidarToken(_correo, tokenIngresado, _token))
            {
                _navegacionForms.NavegarANuevaContrasena(_correo, _recuperacionService);
                this.Hide();
            }
            else
            {
                MessageBox.Show("Token incorrecto. Intente de nuevo.");
            }
        }

        /// <summary>
        /// Evento Click del botón Salir.
        /// </summary>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            _navegacionForms.NavegarAlLogin();
            _navegacionForms.CerrarFormulario(this);
        }
    }
}