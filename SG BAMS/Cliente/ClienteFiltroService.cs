using System.Collections.Generic;
using System.Data;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Construye el RowFilter para el listado de clientes a partir del texto
    /// de búsqueda y el filtro de estado. Extrae la lógica que antes estaba
    /// duplicada dentro de ClientesAdm y ClientesEmp (ver hallazgos CAD02 y
    /// CAD03 de la auditoría), igual que Comprasfiltroservice.cs hace para Compras.
    /// </summary>
    public class ClienteFiltroService
    {
        /// <summary>
        /// Arma el RowFilter combinando el filtro de estado (activo/inactivo)
        /// con la búsqueda de texto sobre nombre, apellido, RTN y teléfono.
        /// </summary>
        public string ConstruirRowFilter(bool mostrarInactivos, string textoBusqueda, string placeholderTexto)
        {
            var condiciones = new List<string>();

            string filtroEstado = mostrarInactivos ? "Estado <> 'Activo'" : "Estado = 'Activo'";
            condiciones.Add($"({filtroEstado})");

            string texto = textoBusqueda?.Trim() ?? "";
            if (texto == placeholderTexto)
                texto = "";

            if (!string.IsNullOrWhiteSpace(texto))
            {
                string textoSeguro = EscaparParaLike(texto);
                string filtroTexto = $"(Nombre LIKE '%{textoSeguro}%' OR " +
                                     $"Apellido LIKE '%{textoSeguro}%' OR " +
                                     $"RTN LIKE '%{textoSeguro}%' OR " +
                                     $"Teléfono LIKE '%{textoSeguro}%')";
                condiciones.Add(filtroTexto);
            }

            return string.Join(" AND ", condiciones);
        }

        /// <summary>
        /// Aplica el filtro directamente sobre la vista del DataTable.
        /// </summary>
        public void AplicarFiltro(DataTable datosCli, DataView vista, bool mostrarInactivos, string textoBusqueda, string placeholderTexto)
        {
            if (datosCli == null || vista == null) return;
            vista.RowFilter = ConstruirRowFilter(mostrarInactivos, textoBusqueda, placeholderTexto);
        }

        private string EscaparParaLike(string texto)
        {
            return texto
                .Replace("'", "''")
                .Replace("[", "[[]")
                .Replace("]", "[]]")
                .Replace("*", "[*]")
                .Replace("%", "[%]");
        }
    }
}
