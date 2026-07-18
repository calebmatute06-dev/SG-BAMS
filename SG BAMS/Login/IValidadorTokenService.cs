namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la validación de tokens de recuperación.
    /// </summary>
    public interface IValidadorTokenService
    {
        /// <summary>
        /// Valida si un token es correcto para un correo dado.
        /// </summary>
        /// <param name="correo">Correo del usuario.</param>
        /// <param name="tokenIngresado">Token ingresado por el usuario.</param>
        /// <param name="tokenGenerado">Token original generado.</param>
        /// <returns>True si el token es válido.</returns>
        bool ValidarToken(string correo, string tokenIngresado, string tokenGenerado);
    }
}