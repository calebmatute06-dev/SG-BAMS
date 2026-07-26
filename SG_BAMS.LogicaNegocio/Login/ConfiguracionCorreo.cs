namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación concreta de IConfiguracionCorreo con los valores
    /// de conexión al servidor SMTP de Gmail del sistema BAMS.
    /// </summary>
    public class ConfiguracionCorreo : IConfiguracionCorreo
    {
        /// <summary>Servidor SMTP de Gmail para envío de correos.</summary>
        public string ServidorSmtp => "smtp.gmail.com";

        /// <summary>Puerto estándar para SMTP con STARTTLS.</summary>
        public int Puerto => 587;

        /// <summary>Indica si se debe usar SSL directo. False porque se usa STARTTLS.</summary>
        public bool UsarSsl => false;

        /// <summary>Dirección de correo electrónico del remitente.</summary>
        public string Remitente => "automatedbatteriessoporte@gmail.com";

        /// <summary>Nombre descriptivo que aparece como remitente en los correos.</summary>
        public string NombreRemitente => "BAMS";

        /// <summary>Usuario para autenticación SMTP (coincide con el correo remitente).</summary>
        public string Usuario => "automatedbatteriessoporte@gmail.com";

        /// <summary>Contraseña de aplicación generada para Gmail.</summary>
        public string Contrasena => "vlqt eugu fivf eqpa";
    }
}