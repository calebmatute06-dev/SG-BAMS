using System;
using System.Security.Cryptography;
using System.Text;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase de utilidades de seguridad para el sistema BAMS.
    /// </summary>
    internal static class ClsSeguridad
    {
        /// <summary>
        /// Genera el hash SHA-256 de una cadena de texto.
        /// </summary>
        /// <param name="texto">El texto a hashear (contraseña en plano).</param>
        /// <returns>Hash SHA-256 en formato hexadecimal en mayúsculas.</returns>
        public static string HashSHA256(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);

                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("X2")); 

                return sb.ToString();
            }
        }

        public static string GenerarToken()
        {
            Random rnd = new Random();
            return rnd.Next(100000, 999999).ToString();
        }

    }
}