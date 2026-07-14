
using System.Data;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Contrato para construir la expresión de filtrado (RowFilter) de la bitácora
/// según texto de búsqueda y rango de fechas.
/// </summary>
public interface IFiltroBitacora
{
    /// <summary>
    /// Construye la expresión de filtro a aplicar sobre un <see cref="DataView"/>.
    /// </summary>
    /// <param name="dt">Tabla de origen sobre la que se calculan las columnas a filtrar.</param>
    /// <param name="textoBusqueda">Texto ingresado por el usuario.</param>
    /// <param name="desde">Fecha inicial del rango.</param>
    /// <param name="hasta">Fecha final del rango.</param>
    /// <returns>La expresión lista para asignarse a <c>DataView.RowFilter</c>.</returns>
    string ConstruirFiltro(DataTable dt, string textoBusqueda, DateTime desde, DateTime hasta);
}
