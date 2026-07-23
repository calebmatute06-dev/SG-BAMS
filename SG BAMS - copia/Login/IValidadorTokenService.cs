namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la validación de tokens de recuperación de contraseña.
    /// </summary>
    public interface IValidadorTokenService
    {
        /// <summary>
        /// Valida si un token ingresado por el usuario coincide con el token generado.
        /// </summary>
        /// <param name="correo">Correo del usuario que solicita la recuperación.</param>
        /// <param name="tokenIngresado">Token ingresado por el usuario en el formulario.</param>
        /// <param name="tokenGenerado">Token original generado por el sistema.</param>
        /// <returns>True si el token ingresado es válido.</returns>
        bool ValidarToken(string correo, string tokenIngresado, string tokenGenerado);
    }
}