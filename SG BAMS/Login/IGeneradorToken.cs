namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la generación de tokens de seguridad.
    /// </summary>
    public interface IGeneradorToken
    {
        /// <summary>
        /// Genera un token aleatorio seguro.
        /// </summary>
        /// <returns>Token generado como cadena de texto.</returns>
        string GenerarToken();
    }
}