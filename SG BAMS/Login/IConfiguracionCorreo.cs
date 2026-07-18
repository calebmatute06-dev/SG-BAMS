namespace SG_BAMS.Login
{
    /// <summary>
    /// Define el contrato para obtener la configuración del servidor de correo.
    /// </summary>
    public interface IConfiguracionCorreo
    {
        /// <summary>Servidor SMTP (ej: smtp.gmail.com).</summary>
        string ServidorSmtp { get; }

        /// <summary>Puerto del servidor SMTP.</summary>
        int Puerto { get; }

        /// <summary>Indica si se debe usar SSL.</summary>
        bool UsarSsl { get; }

        /// <summary>Dirección de correo del remitente.</summary>
        string Remitente { get; }

        /// <summary>Nombre mostrado del remitente.</summary>
        string NombreRemitente { get; }

        /// <summary>Usuario para autenticación SMTP.</summary>
        string Usuario { get; }

        /// <summary>Contraseña para autenticación SMTP.</summary>
        string Contrasena { get; }
    }
}