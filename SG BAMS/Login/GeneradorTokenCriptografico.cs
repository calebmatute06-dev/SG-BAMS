using System;
using System.Security.Cryptography;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IGeneradorToken que utiliza RandomNumberGenerator
    /// para generar tokens criptográficamente seguros.
    /// </summary>
    public class GeneradorTokenCriptografico : IGeneradorToken
    {
        /// <summary>
        /// Genera un token numérico de 6 dígitos utilizando un generador
        /// de números aleatorios criptográficamente seguro.
        /// </summary>
        /// <returns>Token de 6 dígitos como cadena de texto.</returns>
        public string GenerarToken()
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                byte[] bytes = new byte[4];
                rng.GetBytes(bytes);
                int numero = BitConverter.ToInt32(bytes, 0) & 0x7FFFFFFF;
                return (numero % 900000 + 100000).ToString();
            }
        }
    }
}