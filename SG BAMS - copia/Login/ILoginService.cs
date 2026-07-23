namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de autenticación de usuarios
    /// y recuperación de contraseñas.
    /// </summary>
    public interface ILoginService
    {
        /// <summary>
        /// Verifica si un correo electrónico está registrado en el sistema.
        /// </summary>
        /// <param name="correo">Correo electrónico a verificar.</param>
        /// <returns>True si el correo existe en la base de datos.</returns>
        bool VerificarCorreo(string correo);

        /// <summary>
        /// Valida las credenciales de un usuario contra la base de datos.
        /// </summary>
        /// <param name="usuarioOCorreo">Nombre de usuario o correo electrónico.</param>
        /// <param name="contrasena">Contraseña en texto plano.</param>
        /// <returns>
        /// Identificador del rol si las credenciales son válidas:
        /// 1 = Administrador, 2 = Empleado, 3 = Soporte.
        /// Retorna -1 si el usuario está inactivo, 0 si las credenciales son inválidas.
        /// </returns>
        int ValidarUsuario(string usuarioOCorreo, string contrasena);

        /// <summary>
        /// Obtiene el nombre del último usuario validado, recortado al primer nombre.
        /// Se utiliza para el reconocimiento facial donde los archivos de imagen
        /// usan solo el primer nombre.
        /// </summary>
        /// <returns>Primer nombre del usuario. Ejemplo: "Jorge".</returns>
        string ObtenerNombreUsuario();

        /// <summary>
        /// Obtiene el nombre completo del último usuario validado, sin recortar.
        /// Se utiliza para consultas que requieren el nombre exacto,
        /// como la carga del perfil de usuario.
        /// </summary>
        /// <returns>Nombre completo del usuario. Ejemplo: "Jorge_Rodriguez".</returns>
        string ObtenerNombreCompleto();

        /// <summary>
        /// Verifica si la nueva contraseña es igual a la contraseña actual del usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <returns>True si la nueva contraseña coincide con la actual.</returns>
        bool ContraIgualAntigua(string correo, string nuevaContrasena);

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <returns>True si la actualización se realizó correctamente.</returns>
        bool ActualizarContrasena(string correo, string nuevaContrasena);
    }
}