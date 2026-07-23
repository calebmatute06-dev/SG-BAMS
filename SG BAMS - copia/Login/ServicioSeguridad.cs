using System;
using System.Security.Cryptography;
using System.Text;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de seguridad del sistema.
    /// Proporciona generación de tokens criptográficamente seguros y hashing SHA256.
    /// </summary>
    public class ServicioSeguridad : IServicioSeguridad
    {
        private readonly IGeneradorToken generadorToken;

        /// <summary>
        /// Constructor principal que recibe el generador de tokens.
        /// </summary>
        /// <param name="generadorToken">Generador de tokens criptográficos.</param>
        /// <exception cref="ArgumentNullException">Si generadorToken es nulo.</exception>
        public ServicioSeguridad(IGeneradorToken generadorToken)
        {
            this.generadorToken = generadorToken ?? throw new ArgumentNullException(nameof(generadorToken));
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// Utiliza el generador de tokens criptográfico por defecto.
        /// </summary>
        public ServicioSeguridad() : this(new GeneradorTokenCriptografico())
        {
        }

        /// <inheritdoc/>
        public string GenerarToken()
        {
            return generadorToken.GenerarToken();
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