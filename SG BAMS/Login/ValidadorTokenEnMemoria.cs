using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IValidadorTokenService que compara tokens en memoria.
    /// Comparación case-sensitive exacta.
    /// </summary>
    public class ValidadorTokenEnMemoria : IValidadorTokenService
    {
        /// <inheritdoc/>
        public bool ValidarToken(string correo, string tokenIngresado, string tokenGenerado)
        {
            if (string.IsNullOrWhiteSpace(tokenIngresado) || string.IsNullOrWhiteSpace(tokenGenerado))
                return false;

            return string.Equals(tokenIngresado, tokenGenerado, StringComparison.Ordinal);
        }
    }
}