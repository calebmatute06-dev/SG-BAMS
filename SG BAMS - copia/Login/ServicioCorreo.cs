using MimeKit;
using MailKit.Net.Smtp;
using System;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación del servicio de envío de correos electrónicos utilizando MailKit.
    /// Construye y envía mensajes a través del servidor SMTP configurado.
    /// </summary>
    public class ServicioCorreo : IServicioCorreo
    {
        private readonly IConfiguracionCorreo config;

        /// <summary>
        /// Constructor del servicio de correo.
        /// </summary>
        /// <param name="config">Configuración del servidor de correo electrónico.</param>
        /// <exception cref="ArgumentNullException">Si config es nulo.</exception>
        public ServicioCorreo(IConfiguracionCorreo config)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
        }

        /// <summary>
        /// Envía un token de recuperación de contraseña a la dirección de correo especificada.
        /// </summary>
        /// <param name="correoDestino">Dirección de correo del destinatario.</param>
        /// <param name="token">Token de recuperación a enviar en el cuerpo del mensaje.</param>
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
        /// Construye el mensaje MIME con el token de recuperación de contraseña.
        /// </summary>
        /// <param name="correoDestino">Dirección de correo del destinatario.</param>
        /// <param name="token">Token a incluir en el cuerpo del mensaje.</param>
        /// <returns>Mensaje MIME configurado y listo para enviar.</returns>
        private MimeMessage ConstruirMensaje(string correoDestino, string token)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress(config.NombreRemitente, config.Remitente));
            mensaje.To.Add(new MailboxAddress(string.Empty, correoDestino));
            mensaje.Subject = "Token de recuperación - BAMS";

            mensaje.Body = new TextPart("plain")
            {
                Text = $"Tu token de recuperación es: {token}\n\nEste token expira en 10 minutos."
            };

            return mensaje;
        }

        /// <summary>
        /// Establece la conexión con el servidor SMTP y envía el mensaje.
        /// </summary>
        /// <param name="mensaje">Mensaje MIME a enviar.</param>
        private void EnviarMensaje(MimeMessage mensaje)
        {
            using (var cliente = new SmtpClient())
            {
                cliente.Connect(config.ServidorSmtp, config.Puerto, config.UsarSsl);
                cliente.Authenticate(config.Usuario, config.Contrasena);
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            }
        }
    }
}