using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de ISesionUsuarioService como singleton.
    /// Mantiene el estado de la sesión del usuario actual de forma centralizada.
    /// Reemplaza las variables estáticas UsuarioLogueado y UsuarioLogueadoCompleto.
    /// </summary>
    public class SesionUsuarioService : ISesionUsuarioService
    {
        private static readonly Lazy<SesionUsuarioService> _instancia =
            new Lazy<SesionUsuarioService>(() => new SesionUsuarioService());

        /// <summary>Instancia singleton del servicio de sesión.</summary>
        public static SesionUsuarioService Instancia => _instancia.Value;

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

        private SesionUsuarioService() { }

        /// <summary>
        /// Limpia los datos de sesión al cerrar sesión.
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