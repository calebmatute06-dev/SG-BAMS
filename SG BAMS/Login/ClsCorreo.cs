using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Login
{
    public class ClsCorreo
    {
        public static void EnviarToken(string correoDestino, string token)
        {
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("BAMS", "automatedbatteriessoporte@gmail.com"));
            mensaje.To.Add(new MailboxAddress("", correoDestino));
            mensaje.Subject = "Token de recuperación - BAMS";
            mensaje.Body = new TextPart("plain")
            {
                Text = $"Tu token de recuperación es: {token}\n\nEste token expira en 10 minutos."
            };

            using (var cliente = new SmtpClient())
            {
                cliente.Connect("smtp.gmail.com", 587, false);
                cliente.Authenticate("automatedbatteriessoporte@gmail.com", "vlqt eugu fivf eqpa");
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            }
        }
    }
}
