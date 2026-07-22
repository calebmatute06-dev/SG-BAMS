using System;
using System.Data;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Contrato para construir la expresión de filtrado (RowFilter) de la bitácora
    /// según texto de búsqueda y rango de fechas.
    /// </summary>
    public interface IFiltroBitacora
    {
        string ConstruirFiltro(DataTable dt, string textoBusqueda, DateTime desde, DateTime hasta);
    }
}
