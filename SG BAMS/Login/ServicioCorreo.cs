using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de correo usando MailKit y SMTP de Gmail.
    /// Ya no depende de ClsCorreo. Usa IConfiguracionCorreo para obtener
    /// los datos del servidor.
    /// </summary>
    public class ServicioCorreo : IServicioCorreo
    {
        private readonly IConfiguracionCorreo _config;

        /// <summary>
        /// Constructor que recibe la configuración por inyección de dependencias.
        /// </summary>
        /// <param name="config">Configuración del servidor de correo.</param>
        /// <exception cref="ArgumentNullException">Si config es nulo.</exception>
        public ServicioCorreo(IConfiguracionCorreo config)
        {
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Envía un token de recuperación de contraseña al correo especificado.
        /// Orquesta la construcción del mensaje y su posterior envío.
        /// </summary>
        /// <param name="correoDestino">Dirección de correo del destinatario.</param>
        /// <param name="token">Token de recuperación a enviar.</param>
        /// <exception cref="ArgumentException">Si correoDestino o token son nulos o vacíos.</exception>
        public void EnviarToken(string correoDestino, string token)
        {
            if (string.IsNullOrWhiteSpace(correoDestino))
                throw new ArgumentException("El destinatario es obligatorio.", nameof(correoDestino));
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("El token es obligatorio.", nameof(token));

            MimeMessage mensaje = ConstruirMensaje(correoDestino, token);
            EnviarMensaje(mensaje);
        }

        /// <summary>
        /// Construye el mensaje MIME con el token de recuperación.
        /// SRP: Responsabilidad exclusiva de construir el MimeMessage.
        /// </summary>
        /// <param name="correoDestino">Destinatario del correo.</param>
        /// <param name="token">Token a incluir en el cuerpo del mensaje.</param>
        /// <returns>Mensaje MIME listo para enviar.</returns>
        private MimeMessage ConstruirMensaje(string correoDestino, string token)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(_config.NombreRemitente, _config.Remitente));
            mensaje.To.Add(new MailboxAddress(string.Empty, correoDestino));
            mensaje.Subject = "Token de recuperación - BAMS";

            mensaje.Body = new TextPart("plain")
            {
                Text = $"Tu token de recuperación es: {token}\n\nEste token expira en 10 minutos."
            };

            return mensaje;
        }

        /// <summary>
        /// Realiza la conexión SMTP y envía el mensaje.
        /// SRP: Responsabilidad exclusiva del envío vía SMTP.
        /// </summary>
        /// <param name="mensaje">Mensaje MIME a enviar.</param>
        private void EnviarMensaje(MimeMessage mensaje)
        {
            using (var cliente = new SmtpClient())
            {
                cliente.Connect(_config.ServidorSmtp, _config.Puerto, _config.UsarSsl);
                cliente.Authenticate(_config.Usuario, _config.Contrasena);
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            }
        }
    }
}