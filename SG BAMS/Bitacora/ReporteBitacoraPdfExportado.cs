using System;
using System.Collections.Generic;
#nullable enable
using QuestPDF.Fluent;         
using QuestPDF.Infrastructure; 

namespace SG_BAMS.Bitacora;

/// <summary>
/// Exportador concreto que genera el reporte de bitácora en formato PDF usando QuestPDF.
/// Única implementación de <see cref="IReporteExportador"/> para este formato;
/// agregar otro formato solo requiere una nueva clase, sin tocar esta.
/// </summary>
internal sealed class ReporteBitacoraPdfExportador : IReporteExportado
{
    /// <inheritdoc />
    public async Task ExportarAsync(IReadOnlyList<BitacoraDTO> datos, string rutaArchivo, CancellationToken cancellationToken = default)
    {
        QuestPDF.Settings.License = LicenseType.Community;
        var documento = new ReporteBitacora(datos);
        await Task.Run(() => documento.GeneratePdf(rutaArchivo), cancellationToken).ConfigureAwait(false);
    }
}
