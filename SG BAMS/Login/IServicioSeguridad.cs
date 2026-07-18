namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para operaciones de seguridad:
    /// generación de tokens y hashing de contraseñas.
    /// </summary>
    public interface IServicioSeguridad
    {
        /// <summary>
        /// Genera un token aleatorio seguro para recuperación de contraseña.
        /// </summary>
        /// <returns>Token generado como cadena.</returns>
        string GenerarToken();

        /// <summary>
        /// Calcula el hash SHA256 de un texto.
        /// </summary>
        /// <param name="texto">Texto a hashear (ej: contraseña).</param>
        /// <returns>Hash SHA256 en formato hexadecimal.</returns>
        string HashSHA256(string texto);

        /// <summary>
        /// Verifica si un texto plano coincide con un hash SHA256 almacenado.
        /// </summary>
        /// <param name="textoPlano">Texto sin hashear.</param>
        /// <param name="hashAlmacenado">Hash contra el cual comparar.</param>
        /// <returns>True si coinciden.</returns>
        bool VerificarHash(string textoPlano, string hashAlmacenado);
    }
}