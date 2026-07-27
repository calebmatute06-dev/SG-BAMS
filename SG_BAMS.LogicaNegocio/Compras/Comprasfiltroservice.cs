using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Contrato para construir el RowFilter de la grilla de compras.
    /// </summary>
    public interface IComprasFiltroService
    {
        string ConstruirRowFilter(DataTable datos, string texto, string placeholder, DateTime desde, DateTime hasta);
    }

    /// <summary>
    /// Extrae la construcción del filtro (antes vivía directamente dentro
    /// del formulario Compras, mezclando lógica de filtrado con la UI).
    /// </summary>
    public class ComprasFiltroService : IComprasFiltroService
    {
        public string ConstruirRowFilter(DataTable datos, string texto, string placeholder, DateTime desde, DateTime hasta)
        {
            if (datos == null) return string.Empty;

            string textoLimpio = texto?.Trim() ?? "";
            if (textoLimpio == placeholder) textoLimpio = "";

            var condiciones = new List<string>();

            if (!string.IsNullOrWhiteSpace(textoLimpio))
            {
                string textoFiltro = textoLimpio
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]");

                var condicionesTexto = new List<string>();
                foreach (DataColumn columna in datos.Columns)
                {
                    if (columna.DataType == typeof(string) || columna.DataType == typeof(int) || columna.DataType == typeof(decimal))
                    {
                        condicionesTexto.Add($"Convert([{columna.ColumnName}], 'System.String') LIKE '%{textoFiltro}%'");
                    }
                }
                if (condicionesTexto.Count > 0)
                    condiciones.Add("(" + string.Join(" OR ", condicionesTexto) + ")");
            }
            else
            {
                string fInicio = desde.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fFin = hasta.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

                foreach (DataColumn columna in datos.Columns)
                {
                    if (columna.DataType == typeof(DateTime))
                    {
                        condiciones.Add($"([{columna.ColumnName}] >= #{fInicio}# AND [{columna.ColumnName}] < #{fFin}#)");
                        break;
                    }
                }
            }

            return string.Join(" AND ", condiciones);
        }
    }
}
