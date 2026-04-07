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
        /// The factor zoom
        /// </summary>
        public static float FactorZoom = 1.0f;
        /// <summary>
        /// The ultimo factor aplicado
        /// </summary>
        public static float UltimoFactorAplicado = 1.0f;

        /// <summary>
        /// The ruta archivo
        /// </summary>
        private static string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "config_zoom.txt";

        /// <summary>
        /// Guardars the configuracion.
        /// </summary>
        public static void GuardarConfiguracion()
        {
            File.WriteAllText(rutaArchivo, FactorZoom.ToString());
        }

        /// <summary>
        /// Cargars the configuracion.
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