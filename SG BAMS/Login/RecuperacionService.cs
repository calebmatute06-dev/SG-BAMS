using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación de IRecuperacionService que utiliza ClsRecuperacion
    /// internamente para las operaciones de base de datos.
    /// </summary>
    public class RecuperacionService : IRecuperacionService
    {
        private readonly ClsRecuperacion _recuperacion;

        /// <summary>
        /// Constructor.
        /// </summary>
        public RecuperacionService(ClsRecuperacion recuperacion)
        {
            _recuperacion = recuperacion ?? throw new ArgumentNullException(nameof(recuperacion));
        }

        /// <inheritdoc/>
        public bool VerificarCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                return false;

            return _recuperacion.VerificarCorreo(correo);
        }

        /// <inheritdoc/>
        public bool EsContrasenaActual(string correo, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(contrasena))
                return false;

            return _recuperacion.ContraIgualAntigua(correo, contrasena);
        }

        /// <inheritdoc/>
        public bool ActualizarContrasena(string correo, string nuevaContrasena)
        {
            if (string.IsNullOrWhiteSpace(correo) || string.IsNullOrWhiteSpace(nuevaContrasena))
                return false;

            try
            {
                _recuperacion.ActualizarContrasena(correo, nuevaContrasena);

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