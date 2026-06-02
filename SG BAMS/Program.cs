using SG_BAMS.Administracion_de_BAMS.Usuarios;
using SG_BAMS.Bitacora;
using SG_BAMS.Facturas;
using SG_BAMS.Login;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;

namespace SG_BAMS
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            clsSoporte.InicializarDirectorio();
            ApplicationConfiguration.Initialize();
            Application.Run(new AdministracionBAMS());
        }
    }
} 