namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de envío de correos electrónicos.
    /// </summary>
    public interface IServicioCorreo
    {
        /// <summary>
        /// Envía un token de recuperación de contraseña al correo especificado.
        /// </summary>
        /// <param name="correoDestino">Dirección de correo del destinatario.</param>
        /// <param name="token">Token de recuperación a enviar.</param>
        void EnviarToken(string correoDestino, string token);
    }
}