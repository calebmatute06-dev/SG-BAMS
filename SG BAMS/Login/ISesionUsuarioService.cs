namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de sesión del usuario actual.
    /// Centraliza el estado del usuario autenticado para que esté disponible
    /// en toda la aplicación sin depender de variables estáticas globales.
    /// </summary>
    public interface ISesionUsuarioService
    {
        /// <summary>
        /// Nombre recortado del usuario autenticado.
        /// Se utiliza para el reconocimiento facial donde los archivos
        /// de imagen usan solo el primer nombre.
        /// </summary>
        string NombreUsuario { get; set; }

        /// <summary>
        /// Nombre completo del usuario autenticado.
        /// Se utiliza para consultas que requieren el nombre exacto,
        /// como la carga del perfil de usuario.
        /// </summary>
        string NombreCompleto { get; set; }

        /// <summary>
        /// Identificador único del usuario autenticado en la base de datos.
        /// </summary>
        int IdUsuario { get; set; }

        /// <summary>
        /// Rol del usuario autenticado.
        /// 1 = Administrador, 2 = Empleado, 3 = Soporte.
        /// </summary>
        int RolUsuario { get; set; }

        /// <summary>
        /// Indica si hay un usuario con sesión iniciada actualmente.
        /// </summary>
        bool EstaAutenticado { get; }
    }
}