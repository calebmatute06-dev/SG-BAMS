using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de recuperación de contraseña.
    /// Orquesta las operaciones de verificación y actualización de contraseñas
    /// delegando el acceso a datos en la clase ClsRecuperacion.
    /// </summary>
    public class RecuperacionService : IRecuperacionService
    {
        private readonly ClsRecuperacion recuperacion;

        /// <summary>
        /// Constructor del servicio de recuperación.
        /// </summary>
        /// <param name="recuperacion">Instancia de ClsRecuperacion para acceso a datos.</param>
        /// <exception cref="ArgumentNullException">Si recuperacion es nulo.</exception>
        public RecuperacionService(ClsRecuperacion recuperacion)
        {
            this.recuperacion = recuperacion ?? throw new ArgumentNullException(nameof(recuperacion));
        }

        /// <inheritdoc/>
        public bool VerificarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            return recuperacion.VerificarCorreo(correo);
        }

        /// <inheritdoc/>
        public bool EsContrasenaActual(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
                return false;

            return recuperacion.ContraIgualAntigua(correo, contrasena);
        }

        /// <inheritdoc/>
        public bool ActualizarContrasena(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;

            try
            {
                recuperacion.ActualizarContrasena(correo, nuevaContrasena);

                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[ERROR] ActualizarContrasena: {ex.Message}");
                return false;
            }
        }
    }
}