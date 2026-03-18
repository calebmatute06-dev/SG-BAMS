using System;
using System.IO;

namespace SG_BAMS
{
    public static class Config_Sistema
    {
        public static float FactorZoom = 1.0f;
        public static float UltimoFactorAplicado = 1.0f;

        private static string rutaArchivo = AppDomain.CurrentDomain.BaseDirectory + "config_zoom.txt";

        public static void GuardarConfiguracion()
        {
            File.WriteAllText(rutaArchivo, FactorZoom.ToString());
        }

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