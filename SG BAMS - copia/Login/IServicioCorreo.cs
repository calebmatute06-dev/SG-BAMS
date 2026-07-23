namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para el servicio de envío de correos electrónicos.
    /// </summary>
    public interface IServicioCorreo
    {
        /// <summary>
        /// Envía un token de recuperación de contraseña a la dirección
        /// de correo electrónico especificada.
        /// </summary>
        /// <param name="correoDestino">Dirección de correo del destinatario.</param>
        /// <param name="token">Token de recuperación a enviar en el cuerpo del mensaje.</param>
        void EnviarToken(string correoDestino, string token);
    }
}