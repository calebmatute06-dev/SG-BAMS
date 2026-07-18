using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de sesión del usuario actual.
    /// Utiliza el patrón Singleton para mantener el estado de la sesión
    /// de forma centralizada y accesible desde toda la aplicación.
    /// </summary>
    public class SesionUsuarioService : ISesionUsuarioService
    {
        private static readonly Lazy<SesionUsuarioService> instancia =
            new Lazy<SesionUsuarioService>(() => new SesionUsuarioService());

        /// <summary>
        /// Instancia única del servicio de sesión (Singleton).
        /// </summary>
        public static SesionUsuarioService Instancia => instancia.Value;

        /// <inheritdoc/>
        public string NombreUsuario { get; set; }

        /// <inheritdoc/>
        public string NombreCompleto { get; set; }

        /// <inheritdoc/>
        public int IdUsuario { get; set; }

        /// <inheritdoc/>
        public int RolUsuario { get; set; }

        /// <inheritdoc/>
        public bool EstaAutenticado => !string.IsNullOrEmpty(NombreUsuario);

        /// <summary>
        /// Constructor privado para garantizar el patrón Singleton.
        /// </summary>
        private SesionUsuarioService() { }

        /// <summary>
        /// Elimina todos los datos de la sesión actual al cerrar sesión.
        /// </summary>
        public void CerrarSesion()
        {
            NombreUsuario = null;
            NombreCompleto = null;
            IdUsuario = 0;
            RolUsuario = 0;
        }
    }
}