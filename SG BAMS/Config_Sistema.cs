using System;
using System.IO;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public static class Config_Sistema
    {
        /// <summary>
        /// El factor de zoom
        /// </summary>
        public static float FactorZoom = 1.0f;
        /// <summary>
        /// El último factor aplicado
        /// </summary>
        public static float UltimoFactorAplicado = 1.0f;

        /// <summary>
        /// La ruta del archivo de configuración
        /// </summary>
        private static string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "config_zoom.txt";

        /// <summary>
        /// Guarda la configuración.
        /// </summary>
        public static void GuardarConfiguracion()
        {
            File.WriteAllText(rutaArchivo, FactorZoom.ToString());
        }

        /// <summary>
        /// Carga la configuración.
        /// </summary>
        public static void CargarConfiguracion()
        {
            if (File.Exists(rutaArchivo))
            {
                string contenido = File.ReadAllText(rutaArchivo);
                if (float.TryParse(contenido, out float valorGuardado))
                {
                    FactorZoom = valorGuardado;
                    UltimoFactorAplicado = valorGuardado;
                }
            }
        }
    }
}