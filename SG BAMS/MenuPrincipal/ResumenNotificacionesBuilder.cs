using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Constructor de texto de resumen para notificaciones del sistema.
    /// Genera un mensaje formateado con el conteo de notificaciones por tipo.
    /// </summary>
    public static class ResumenNotificacionesBuilder
    {
        /// <summary>
        /// Construye un texto de resumen a partir de los datos de notificaciones.
        /// </summary>
        /// <param name="notificaciones">DataTable con las notificaciones. Debe contener la columna "tipo".</param>
        /// <returns>Texto de resumen formateado con el conteo por categoría, o cadena vacía si no hay notificaciones.</returns>
        public static string Construir(DataTable notificaciones)
        {
            if (notificaciones == null || notificaciones.Rows.Count == 0)
                return string.Empty;

            int total = notificaciones.Rows.Count;
            int criticas = 0, warnings = 0, info = 0;

            foreach (DataRow row in notificaciones.Rows)
            {
                string tipo = row["tipo"].ToString().ToLower();
                if (tipo == "danger") criticas++;
                else if (tipo == "warning") warnings++;
                else info++;
            }

            string mensaje = $"Tienes {total} notificación(es) pendiente(s)";
            if (criticas > 0) mensaje += $"  |  🛑 {criticas}";
            if (warnings > 0) mensaje += $"  |  ⚠️ {warnings}";
            if (info > 0) mensaje += $"  |  ℹ️ {info}";

            return mensaje;
        }
    }
}