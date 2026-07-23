using System;
using System.Collections.Generic;

namespace SG_BAMS.Bitacora;

/// <summary>
/// Contrato para exportar registros de bitácora a un archivo, independientemente
/// del formato final (PDF, Excel, CSV, etc.).
/// </summary>
public interface IReporteExportado
{
    /// <summary>
    /// Exporta de forma asíncrona los datos indicados hacia la ruta de archivo dada.
    /// </summary>
    /// <param name="datos">Registros a exportar.</param>
    /// <param name="rutaArchivo">Ruta completa del archivo de salida.</param>
    /// <param name="cancellationToken">Token para cancelar la operación.</param>
    Task ExportarAsync(IReadOnlyList<BitacoraDTO> datos, string rutaArchivo, CancellationToken cancellationToken = default);
}