namespace SG_BAMS.Login
{
    /// <summary>
    /// Encapsula los datos del usuario que ha iniciado sesión exitosamente.
    /// Proporciona una alternativa a las variables estáticas globales para
    /// transportar la información del usuario autenticado entre formularios.
    /// </summary>
    public class UsuarioAutenticado
    {
        /// <summary>
        /// Identificador único del usuario en la base de datos.
        /// </summary>
        public int IdUsuario { get; set; }

        /// <summary>
        /// Correo electrónico del usuario autenticado.
        /// </summary>
        public string Correo { get; set; }

        /// <summary>
        /// Rol del usuario en el sistema. Ejemplo: Administrador, Empleado, Soporte.
        /// </summary>
        public string Rol { get; set; }

        /// <summary>
        /// Nombre completo del usuario para mostrar en la interfaz.
        /// </summary>
        public string NombreCompleto { get; set; }
    }
}