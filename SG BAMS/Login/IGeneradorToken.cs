namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para la generación de tokens de seguridad.
    /// Permite intercambiar el algoritmo de generación sin afectar
    /// a las clases que lo consumen.
    /// </summary>
    public interface IGeneradorToken
    {
        /// <summary>
        /// Genera un token aleatorio seguro para operaciones como
        /// recuperación de contraseña o verificación de identidad.
        /// </summary>
        /// <returns>Token generado como cadena de texto.</returns>
        string GenerarToken();
    }
}