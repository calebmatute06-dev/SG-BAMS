
using System.Data;
using System.Globalization;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Construye expresiones de filtrado para la bitácora.
/// Es una operación puramente en memoria (CPU-bound), por lo que
/// no requiere async/await: no realiza E/S.
/// </summary>
internal sealed class FiltroBitacoraService : IFiltroBitacora
{
    /// <inheritdoc />
    public string ConstruirFiltro(DataTable dt, string textoBusqueda, DateTime desde, DateTime hasta)
    {
        var condiciones = new List<string>();

        if (!string.IsNullOrWhiteSpace(textoBusqueda))
        {
            var textoSeguro = textoBusqueda
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Replace("*", "[*]")
                .Replace("%", "[%]");

            var condicionesTexto = dt.Columns
                .Cast<DataColumn>()
                .Where(col => col.DataType == typeof(string))
                .Select(col => $"[{col.ColumnName}] LIKE '{textoSeguro}%'")
                .ToList();

            if (condicionesTexto.Count > 0)
                condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
        }

        var fechaDesde = desde.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        var fechaHasta = hasta.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
        var columnaFecha = dt.Columns.Cast<DataColumn>()
            .FirstOrDefault(c => c.DataType == typeof(DateTime))?.ColumnName;

        if (columnaFecha is not null)
            condiciones.Add($"[{columnaFecha}] >= #{fechaDesde}# AND [{columnaFecha}] < #{fechaHasta}#");

        return condiciones.Count > 0 ? string.Join(" AND ", condiciones) : string.Empty;
    }
}
