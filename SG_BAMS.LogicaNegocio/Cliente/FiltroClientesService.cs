using System.Collections.Generic;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Construye el filtro (RowFilter) para la grilla de clientes a partir del texto de
    /// búsqueda y del estado (activos/inactivos) elegidos por el usuario. Única
    /// responsabilidad: traducir esos criterios a una expresión de filtro válida y segura
    /// contra los caracteres especiales de DataView.RowFilter.
    /// Antes esta lógica vivía duplicada dentro de ClientesAdm y ClientesEmp
    /// (ver auditoría SOLID, hallazgo CAD01/CAD07).
    /// </summary>
    public class FiltroClientesService
    {
        /// <summary>
        /// Construye la expresión de RowFilter combinando el estado (activo/inactivo)
        /// y, opcionalmente, un texto de búsqueda por nombre, apellido, RTN o teléfono.
        /// </summary>
        /// <param name="textoBusqueda">Texto ingresado por el usuario (puede venir vacío).</param>
        /// <param name="mostrarInactivos">true para mostrar inactivos; false para mostrar solo activos.</param>
        public string ConstruirRowFilter(string textoBusqueda, bool mostrarInactivos)
        {
            var condiciones = new List<string>
            {
                mostrarInactivos ? "(Estado <> 'Activo')" : "(Estado = 'Activo')"
            };

            string texto = (textoBusqueda ?? string.Empty).Trim();

            if (!string.IsNullOrWhiteSpace(texto))
            {
                string textoSeguro = texto
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Replace("*", "[*]")
                    .Replace("%", "[%]");

                condiciones.Add($"(Nombre LIKE '%{textoSeguro}%' OR " +
                                $"Apellido LIKE '%{textoSeguro}%' OR " +
                                $"RTN LIKE '%{textoSeguro}%' OR " +
                                $"Teléfono LIKE '%{textoSeguro}%')");
            }

            return string.Join(" AND ", condiciones);
        }
    }
}
