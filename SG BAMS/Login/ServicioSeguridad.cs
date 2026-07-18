using System;
using System.Security.Cryptography;
using System.Text;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IServicioSeguridad que proporciona generación de tokens
    /// criptográficamente seguros y hashing SHA256.
    /// </summary>
    public class ServicioSeguridad : IServicioSeguridad
    {
        private readonly IGeneradorToken _generadorToken;

        /// <summary>
        /// Constructor que recibe el generador de tokens por inyección.
        /// </summary>
        /// <param name="generadorToken">Generador de tokens (criptográfico por defecto).</param>
        /// <exception cref="ArgumentNullException">Si generadorToken es nulo.</exception>
        public ServicioSeguridad(IGeneradorToken generadorToken)
        {
            _generadorToken = generadorToken ?? throw new ArgumentNullException(nameof(generadorToken));
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// Usa GeneradorTokenCriptografico por defecto.
        /// </summary>
        public ServicioSeguridad() : this(new GeneradorTokenCriptografico())
        {
        }

        /// <inheritdoc/>
        public string GenerarToken()
        {
            return _generadorToken.GenerarToken();
        }

        /// <inheritdoc/>
        public string HashSHA256(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                throw new ArgumentException("El texto a hashear no puede ser nulo o vacío.", nameof(texto));

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

        /// <inheritdoc/>
        public bool VerificarHash(string textoPlano, string hashAlmacenado)
        {
            if (string.IsNullOrEmpty(textoPlano) || string.IsNullOrEmpty(hashAlmacenado))
                return false;

            string hashCalculado = HashSHA256(textoPlano);
            return string.Equals(hashCalculado, hashAlmacenado, StringComparison.OrdinalIgnoreCase);
        }
    }
}