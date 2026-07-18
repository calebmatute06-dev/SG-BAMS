namespace SG_BAMS.Login
{
    /// <summary>
    /// Implementación concreta de IConfiguracionCorreo con los valores
    /// de conexión al servidor SMTP de Gmail del sistema BAMS.
    /// </summary>
    public class ConfiguracionCorreo : IConfiguracionCorreo
    {
        public string ServidorSmtp => "smtp.gmail.com";
        public int Puerto => 587;
        public bool UsarSsl => false;
        public string Remitente => "automatedbatteriessoporte@gmail.com";
        public string NombreRemitente => "BAMS";
        public string Usuario => "automatedbatteriessoporte@gmail.com";
        public string Contrasena => "vlqt eugu fivf eqpa";
    }
}