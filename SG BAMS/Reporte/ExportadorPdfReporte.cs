using System;
using System.Data;
using System.IO;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Adapta DocumentoReportePdf a la interfaz común IExportadorReporte,
    /// para que ReportesAdmin pueda pedir un PDF exactamente igual que
    /// pide un Excel (mismo contrato, sin conocer QuestPDF).
    /// </summary>
    public class ExportadorPdfReporte : IExportadorReporte
    {
        public string Exportar(DataTable datos, ReporteTipo tipo, DateTime desde, DateTime hasta)
        {
            if (datos == null || datos.Rows.Count == 0) return null;

            string ruta = Path.Combine(Path.GetTempPath(), $"Reporte_{tipo}_{DateTime.Now:yyyyMMdd_HHmmss}.pdf");
            var documento = new DocumentoReportePdf(datos, tipo, desde, hasta);
            documento.GeneratePdf(ruta);
            return ruta;
        }
    }
}
