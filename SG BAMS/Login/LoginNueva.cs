using System;
using System.Windows.Forms;
using SG_BAMS.Login;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para establecer una nueva contraseña después de validar el token.
    /// </summary>
    public partial class LoginNueva : Form
    {
        private readonly string _correo;
        private readonly IRecuperacionService _recuperacionService;
        private readonly INavegacionFormsService _navegacionForms;
        private PlaceholderTextBox phContra;
        private PlaceholderTextBox phContraNueva;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        /// <param name="correo">Correo del usuario que va a cambiar su contraseña.</param>
        /// <param name="recuperacionService">Servicio de recuperación de contraseña.</param>
        /// <param name="navegacionForms">Servicio de navegación entre formularios.</param>
        /// <exception cref="ArgumentNullException">Si algún parámetro es nulo.</exception>
        public LoginNueva(
            string correo,
            IRecuperacionService recuperacionService,
            INavegacionFormsService navegacionForms)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            _correo = correo ?? throw new ArgumentNullException(nameof(correo));
            _recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
            _navegacionForms = navegacionForms ?? throw new ArgumentNullException(nameof(navegacionForms));

            phContra = new PlaceholderTextBox(txtContra, "Ingrese la contraseña Nueva");
            phContraNueva = new PlaceholderTextBox(txtContraNueva, "Confirme la contraseña Nueva");
        }

        /// <summary>
        /// Evento Click del botón Confirmar.
        /// Valida que las contraseñas coincidan y delega la actualización a IRecuperacionService.
        /// </summary>
        private void btnConfirmar_Click(object sender, EventArgs e)
        {
            string pass1 = phContra.GetRealValue().Trim();
            string pass2 = phContraNueva.GetRealValue().Trim();

            if (string.IsNullOrEmpty(pass1) || string.IsNullOrEmpty(pass2))
            {
                MessageBox.Show("Complete todos los campos.");
                return;
            }

            if (!ClsValidaciones.EsPasswordValido(txtContra, "La contraseña"))
                return;

            if (!ClsValidaciones.EsPasswordValido(txtContraNueva, "La confirmación"))
                return;

            if (pass1 != pass2)
            {
                MessageBox.Show("Las contraseñas no coinciden.");
                return;
            }

            if (_recuperacionService.EsContrasenaActual(_correo, pass1))
            {
                MessageBox.Show("La nueva contraseña no puede ser igual a la actual.",
                                "Contraseña repetida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtContra.Clear();
                txtContraNueva.Clear();
                txtContra.Focus();
                return;
            }

            if (_recuperacionService.ActualizarContrasena(_correo, pass1))
            {
                MessageBox.Show("Contraseña actualizada correctamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _navegacionForms.NavegarAlLogin();
                _navegacionForms.CerrarFormulario(this);
            }
            else
            {
                MessageBox.Show("Error al actualizar la contraseña.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Evento Click del botón Salir.
        /// DIP: La navegación se delega en INavegacionFormsService.
        /// </summary>
        private void btnsalir_Click(object sender, EventArgs e)
        {
            _navegacionForms.NavegarAlLogin();
            _navegacionForms.CerrarFormulario(this);
        }
    }
}