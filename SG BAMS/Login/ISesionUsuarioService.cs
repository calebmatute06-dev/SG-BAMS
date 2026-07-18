namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de sesión del usuario actual.
    /// Reemplaza las variables estáticas globales UsuarioLogueado y UsuarioLogueadoCompleto
    /// eliminando el acoplamiento oculto.
    /// </summary>
    public interface ISesionUsuarioService
    {
        /// <summary>Nombre recortado del usuario (para reconocimiento facial).</summary>
        string NombreUsuario { get; set; }

        /// <summary>Nombre completo del usuario (para perfil y consultas).</summary>
        string NombreCompleto { get; set; }

        /// <summary>ID del usuario autenticado.</summary>
        int IdUsuario { get; set; }

        /// <summary>Rol del usuario (1=Admin, 2=Empleado, 3=Soporte).</summary>
        int RolUsuario { get; set; }

        /// <summary>Indica si hay un usuario autenticado.</summary>
        bool EstaAutenticado { get; }
    }
}