using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de autenticación de usuarios.
    /// Orquesta las operaciones de validación de credenciales y recuperación de contraseña.
    /// </summary>
    public class LoginService : ILoginService
    {
        private ClsLogin ultimoLogin;
        private readonly IRecuperacionService recuperacionService;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public LoginService() : this(new RecuperacionService(new ClsRecuperacion()))
        {
        }

        /// <summary>
        /// Constructor principal que recibe el servicio de recuperación de contraseña.
        /// </summary>
        /// <param name="recuperacionService">Servicio de recuperación de contraseña.</param>
        /// <exception cref="ArgumentNullException">Si recuperacionService es nulo.</exception>
        public LoginService(IRecuperacionService recuperacionService)
        {
            this.recuperacionService = recuperacionService ?? throw new ArgumentNullException(nameof(recuperacionService));
        }

        /// <inheritdoc/>
        public bool VerificarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;
            return recuperacionService.VerificarCorreo(correo);
        }

        /// <inheritdoc/>
        public int ValidarUsuario(string usuarioOCorreo, string contrasena)
        {
            ultimoLogin = new ClsLogin();
            int rol = ultimoLogin.ValidarUsuario(usuarioOCorreo, contrasena);
            return rol;
        }

        /// <inheritdoc/>
        public string ObtenerNombreUsuario()
        {
            if (ultimoLogin != null)
                return ultimoLogin.NombreUsuario;
            return string.Empty;
        }

        /// <inheritdoc/>
        public string ObtenerNombreCompleto()
        {
            if (ultimoLogin != null)
                return ultimoLogin.NombreCompleto;
            return string.Empty;
        }

        /// <inheritdoc/>
        public bool ContraIgualAntigua(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;
            return recuperacionService.EsContrasenaActual(correo, nuevaContrasena);
        }

        /// <inheritdoc/>
        public bool ActualizarContrasena(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;
            try
            {
                bool resultado = recuperacionService.ActualizarContrasena(correo, nuevaContrasena);
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