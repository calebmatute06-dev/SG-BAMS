using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de ILoginService que utiliza IRecuperacionService y ClsLogin.
    /// </summary>
    public class LoginService : ILoginService
    {
        private ClsLogin _ultimoLogin;
        private readonly IRecuperacionService _recuperacionService;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public LoginService() : this(new RecuperacionService(new ClsRecuperacion()))
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// </summary>
        public LoginService(IRecuperacionService recuperacionService)
        {
            _recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
        }

        /// <inheritdoc/>
        public bool VerificarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;
            return _recuperacionService.VerificarCorreo(correo);
        }

        /// <inheritdoc/>
        public int ValidarUsuario(string usuarioOCorreo, string contrasena)
        {
            _ultimoLogin = new ClsLogin();
            int rol = _ultimoLogin.ValidarUsuario(usuarioOCorreo, contrasena);
            return rol;
        }

        /// <inheritdoc/>
        public string ObtenerNombreUsuario()
        {
            if (_ultimoLogin != null)
                return _ultimoLogin.NombreUsuario;
            return string.Empty;
        }

        /// <inheritdoc/>
        public string ObtenerNombreCompleto()
        {
            if (_ultimoLogin != null)
                return _ultimoLogin.NombreCompleto;
            return string.Empty;
        }

        /// <inheritdoc/>
        public bool ContraIgualAntigua(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;
            return _recuperacionService.EsContrasenaActual(correo, nuevaContrasena);
        }

        /// <inheritdoc/>
        public bool ActualizarContrasena(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;
            try
            {
                bool resultado = _recuperacionService.ActualizarContrasena(correo, nuevaContrasena);
                if (!resultado)
                    return true;
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar contraseña: " + ex.Message);
            }
        }
    }
}