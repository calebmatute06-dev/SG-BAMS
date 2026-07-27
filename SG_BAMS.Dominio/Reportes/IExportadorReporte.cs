using System;
using System.Data;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Abstracción común para cualquier forma de exportar un reporte
    /// (Excel, PDF, o lo que se agregue después). ReportesAdmin depende de
    /// esta interfaz en lugar de instanciar ClsExportarExcel o
    /// DocumentoDinamico directamente (resuelve RA06).
    ///
    /// Recibe un DataTable (ya neutral respecto a la UI) en lugar de un
    /// DataGridView (resuelve CE04 / DD04), y devuelve la ruta del archivo
    /// generado en lugar de abrirlo o mostrar errores por sí misma
    /// (resuelve CE05): abrir el archivo y reportar errores es
    /// responsabilidad de la capa de presentación.
    /// </summary>
    public interface IExportadorReporte
    {
        string Exportar(DataTable datos, ReporteTipo tipo, DateTime desde, DateTime hasta);
    }
}
