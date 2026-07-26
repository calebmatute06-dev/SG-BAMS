namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para las operaciones de recuperación de contraseña.
    /// </summary>
    public interface IRecuperacionService
    {
        /// <summary>
        /// Verifica si un correo electrónico está registrado en el sistema.
        /// </summary>
        /// <param name="correo">Correo electrónico a verificar.</param>
        /// <returns>True si el correo existe en la base de datos.</returns>
        bool VerificarCorreo(string correo);

        /// <summary>
        /// Verifica si la contraseña ingresada coincide con la contraseña actual del usuario.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="contrasena">Contraseña a comparar en texto plano.</param>
        /// <returns>True si la contraseña coincide con la actual.</returns>
        bool EsContrasenaActual(string correo, string contrasena);

        /// <summary>
        /// Actualiza la contraseña de un usuario en la base de datos.
        /// La contraseña se hashea internamente antes de ser almacenada.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="nuevaContrasena">Nueva contraseña en texto plano.</param>
        /// <returns>True si la actualización se realizó correctamente.</returns>
        bool ActualizarContrasena(string correo, string nuevaContrasena);
    }
}