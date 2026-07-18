using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Windows.Forms;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de INavegacionFormsService para el flujo de login.
    /// </summary>
    public class NavegacionFormsService : INavegacionFormsService
    {
        private readonly IServicioCorreo _servicioCorreo;
        private readonly IServicioSeguridad _servicioSeguridad;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="servicioCorreo">Servicio de envío de correos.</param>
        /// <param name="servicioSeguridad">Servicio de seguridad.</param>
        public NavegacionFormsService(IServicioCorreo servicioCorreo, IServicioSeguridad servicioSeguridad)
        {
            _servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
            _servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
        }

        /// <inheritdoc/>
        public void NavegarANuevaContrasena(string correo, IRecuperacionService recuperacionService)
        {
            LoginNueva formularioNueva = new LoginNueva(correo, recuperacionService, this);
            formularioNueva.Show();
        }

        /// <inheritdoc/>
        public void NavegarAlLogin()
        {
            Form loginOriginal = Application.OpenForms["Login"];

            if (loginOriginal != null)
            {
                loginOriginal.Show();
            }
            else
            {
                IRecuperacionService recuperacionService = new RecuperacionService(
                    new ClsRecuperacion(new ClsRepositorioBaseDatos(), _servicioSeguridad));

                ILoginService loginService = new LoginService(recuperacionService);

                Login nuevoLogin = new Login(
                    loginService,
                    _servicioCorreo,
                    _servicioSeguridad,
                    SesionUsuarioService.Instancia,
                    new RepositorioRostros(DetectorRostroService.DirectorioRostros));

                nuevoLogin.Show();
            }
        }

        /// <inheritdoc/>
        public void CerrarFormulario(Form formularioActual)
        {
            formularioActual?.Close();
        }

        /// <inheritdoc/>
        public void IrA<T>() where T : Form, new()
        {
            T formulario = new T();
            formulario.Show();
        }

        /// <inheritdoc/>
        public void MostrarFormulario(Form formulario)
        {
            formulario?.Show();
        }
    }
}