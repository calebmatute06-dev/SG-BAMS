using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;


namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Exportador concreto que genera el reporte de bitácora en formato PDF usando QuestPDF.
    /// </summary>
    public class ReporteBitacoraPdfExportador : IReporteExportador
    {
        public void Exportar(List<BitacoraDTO> datos, string rutaArchivo)
        {
            QuestPDF.Settings.License = LicenseType.Community;
            var documento = new ReporteBitacora(datos);
            documento.GeneratePdf(rutaArchivo);
        }
    }
}