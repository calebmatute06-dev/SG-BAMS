using SG_BAMS.Administracion_de_BAMS.Usuarios;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de navegación entre formularios del sistema.
    /// Gestiona la apertura, cierre y transición entre las pantallas del flujo de login.
    /// </summary>
    public class NavegacionFormsService : INavegacionFormsService
    {
        private readonly IServicioCorreo servicioCorreo;
        private readonly IServicioSeguridad servicioSeguridad;

        /// <summary>
        /// Constructor del servicio de navegación.
        /// </summary>
        /// <param name="servicioCorreo">Servicio de envío de correos electrónicos.</param>
        /// <param name="servicioSeguridad">Servicio de operaciones de seguridad.</param>
        /// <exception cref="ArgumentNullException">Si algún servicio es nulo.</exception>
        public NavegacionFormsService(IServicioCorreo servicioCorreo, IServicioSeguridad servicioSeguridad)
        {
            this.servicioCorreo = servicioCorreo ?? throw new ArgumentNullException(nameof(servicioCorreo));
            this.servicioSeguridad = servicioSeguridad ?? throw new ArgumentNullException(nameof(servicioSeguridad));
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
                    new ClsRecuperacion(new ClsRepositorioBaseDatos(), servicioSeguridad));

                ILoginService loginService = new LoginService(recuperacionService);

                Login nuevoLogin = new Login(
                    loginService,
                    servicioCorreo,
                    servicioSeguridad,
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