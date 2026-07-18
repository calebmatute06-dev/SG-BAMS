namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para las operaciones de seguridad del sistema,
    /// incluyendo generación de tokens y hashing de contraseñas.
    /// </summary>
    public interface IServicioSeguridad
    {
        /// <summary>
        /// Genera un token aleatorio seguro para recuperación de contraseña
        /// u otros procesos de verificación.
        /// </summary>
        /// <returns>Token generado como cadena de texto.</returns>
        string GenerarToken();

        /// <summary>
        /// Calcula el hash SHA256 de un texto.
        /// </summary>
        /// <param name="texto">Texto a hashear, como una contraseña en texto plano.</param>
        /// <returns>Hash SHA256 en formato hexadecimal.</returns>
        string HashSHA256(string texto);

        /// <summary>
        /// Verifica si un texto plano coincide con un hash SHA256 previamente calculado.
        /// </summary>
        /// <param name="textoPlano">Texto sin hashear a verificar.</param>
        /// <param name="hashAlmacenado">Hash SHA256 contra el cual comparar.</param>
        /// <returns>True si el texto plano produce el mismo hash.</returns>
        bool VerificarHash(string textoPlano, string hashAlmacenado);
    }
}