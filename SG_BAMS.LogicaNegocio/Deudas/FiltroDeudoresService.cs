namespace SG_BAMS
{
    /// <summary>
    /// Construye el filtro (RowFilter) para la grilla de deudores a partir del texto de
    /// búsqueda ingresado por el usuario. Única responsabilidad: traducir un nombre de
    /// cliente a una expresión de filtro válida y segura contra los caracteres especiales
    /// de DataView.RowFilter (comillas y corchetes).
    /// Antes esta lógica vivía duplicada dentro de DeudoresAdmin y Deudores_Emp
    /// (ver auditoría SOLID, hallazgos DAD02 y DEM01).
    /// </summary>
    public class FiltroDeudoresService
    {
        /// <summary>
        /// Construye la expresión de RowFilter que muestra únicamente deudas activas,
        /// opcionalmente acotadas por nombre de cliente.
        /// </summary>
        public string ConstruirRowFilter(string nombreBuscado)
        {
            string filtroNombre = (nombreBuscado ?? string.Empty).Trim();

            var condiciones = new System.Collections.Generic.List<string>
            {
                "[Estado Deuda] = 'Activo'"
            };

            if (!string.IsNullOrWhiteSpace(filtroNombre))
            {
                string nombreEscapado = filtroNombre
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]");

                condiciones.Add($"Cliente LIKE '%{nombreEscapado}%'");
            }

            return string.Join(" AND ", condiciones);
        }
    }
}
