namespace SG_BAMS.Login
{
    /// <summary>
    /// Encapsula los datos del usuario que ha sido autenticado exitosamente.
    /// Reemplaza las variables estáticas globales idusuario y RolUsuario,
    /// eliminando el estado compartido que violaba SRP y generaba acoplamiento oculto.
    /// </summary>
    public class UsuarioAutenticado
    {
        /// <summary>Identificador único del usuario en la base de datos.</summary>
        public int IdUsuario { get; set; }

        /// <summary>Correo electrónico del usuario autenticado.</summary>
        public string Correo { get; set; }

        /// <summary>Rol del usuario en el sistema (Administrador, Operador, etc.).</summary>
        public string Rol { get; set; }

        /// <summary>Nombre completo del usuario para mostrar en la interfaz.</summary>
        public string NombreCompleto { get; set; }
    }
}