
using System.Data;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Contrato de acceso a datos para los registros de la bitácora del sistema.
/// </summary>
public interface IBitacoraRepository
{
    /// <summary>
    /// Obtiene de forma asíncrona todos los registros de la bitácora.
    /// </summary>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    /// <returns>Una tabla con los registros obtenidos.</returns>
    Task<DataTable> ObtenerRegistrosAsync(CancellationToken cancellationToken = default);
}
