namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para obtener la configuración necesaria
    /// del servidor de correo electrónico.
    /// </summary>
    public interface IConfiguracionCorreo
    {
        /// <summary>
        /// Servidor SMTP para el envío de correos. Ejemplo: smtp.gmail.com.
        /// </summary>
        string ServidorSmtp { get; }

        /// <summary>
        /// Puerto del servidor SMTP. Ejemplo: 587 para STARTTLS.
        /// </summary>
        int Puerto { get; }

        /// <summary>
        /// Indica si se debe utilizar conexión SSL directa.
        /// </summary>
        bool UsarSsl { get; }

        /// <summary>
        /// Dirección de correo electrónico del remitente.
        /// </summary>
        string Remitente { get; }

        /// <summary>
        /// Nombre descriptivo que aparece como remitente en los correos enviados.
        /// </summary>
        string NombreRemitente { get; }

        /// <summary>
        /// Usuario para la autenticación en el servidor SMTP.
        /// </summary>
        string Usuario { get; }

        /// <summary>
        /// Contraseña para la autenticación en el servidor SMTP.
        /// </summary>
        string Contrasena { get; }
    }
}