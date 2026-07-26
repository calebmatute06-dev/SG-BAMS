using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del validador de tokens que compara los valores en memoria.
    /// Realiza una comparación exacta entre el token ingresado y el token generado.
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