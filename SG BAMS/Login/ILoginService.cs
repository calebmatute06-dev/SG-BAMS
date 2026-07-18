namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de autenticación y recuperación
    /// de contraseña.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Verifica si un correo electrónico existe en el sistema.
        /// </summary>
        /// <param name="correo">Correo a verificar.</param>
        /// <returns>True si el correo está registrado.</returns>
        bool VerificarCorreo(string correo);

        /// <summary>
        /// Valida las credenciales de un usuario.
        /// </summary>
        /// <param name="usuarioOCorreo">Nombre de usuario o correo.</param>
        /// <param name="contrasena">Contraseña en texto plano.</param>
        /// <returns>ID del rol (1=Admin, 2=Empleado, 3=Soporte, -1=Inactivo, 0=Inválido).</returns>
        int ValidarUsuario(string usuarioOCorreo, string contrasena);

        /// <summary>
        /// Obtiene el nombre del último usuario validado (recortado al primer nombre).
        /// Se usa para reconocimiento facial.
        /// Ejemplo: "Jorge"
        /// </summary>
        string ObtenerNombreUsuario();

        /// <summary>
        /// Obtiene el nombre completo del último usuario validado (sin recortar).
        /// Se usa para consultas que requieren el nombre exacto, como el perfil.
        /// Ejemplo: "Jorge_Rodriguez"
        /// </summary>
        string ObtenerNombreCompleto();

        /// <summary>
        /// Verifica si la contraseña nueva es igual a la anterior.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <returns>True si es igual a la anterior.</returns>
        bool ContraIgualAntigua(string correo, string nuevaContrasena);

        /// <summary>
        /// Actualiza la contraseña de un usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <returns>True si se actualizó correctamente.</returns>
        bool ActualizarContrasena(string correo, string nuevaContrasena);
    }
}