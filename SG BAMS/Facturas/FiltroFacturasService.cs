using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class FiltroFacturasService
    {
        public DataView Filtrar(DataTable datos, string texto, System.DateTime? fechaInicio, System.DateTime? fechaFin)
        {
            DataView dv = datos.DefaultView;
            var condiciones = new List<string>();

            if (fechaInicio.HasValue && fechaFin.HasValue)
            {
                string fInicio = fechaInicio.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fFin = fechaFin.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                condiciones.Add($"([Fecha] >= #{fInicio}# AND [Fecha] < #{fFin}#)");
            }

            if (!string.IsNullOrWhiteSpace(texto))
            {
                string textoFiltro = texto
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]");

                condiciones.Add($"(Convert([Factura], 'System.String') LIKE '%{textoFiltro}%' OR " +
                                 $"[Vendedor] LIKE '%{textoFiltro}%' OR " +
                                 $"[Cliente] LIKE '%{textoFiltro}%' OR " +
                                 $"[Método de Pago] LIKE '%{textoFiltro}%' OR " +
                                 $"[RTN Cliente] LIKE '%{textoFiltro}%')");
            }

            dv.RowFilter = condiciones.Count > 0 ? string.Join(" AND ", condiciones) : "";
            return dv;
        }
    }
}
