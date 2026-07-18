using System;
using System.Security.Cryptography;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IGeneradorToken que utiliza RNGCryptoServiceProvider
    /// para generar tokens criptográficamente seguros.
    /// </summary>
    public class GeneradorTokenCriptografico : IGeneradorToken
    {
        /// <summary>
        /// Genera un token numérico de 6 dígitos usando un generador criptográfico seguro.
        /// </summary>
        /// <returns>Token de 6 dígitos como cadena.</returns>
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