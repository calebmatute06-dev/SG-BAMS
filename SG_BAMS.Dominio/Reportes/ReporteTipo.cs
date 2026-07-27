namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Tipos de reporte soportados por el módulo. Reemplaza las comparaciones
    /// de texto (tituloReporte.Contains("VENTAS"), etc.) que antes estaban
    /// repetidas en ClsExportarExcel, DocumentoDinamico y ReportesAdmin.
    /// </summary>
    public enum ReporteTipo
    {
        Ventas,
        Compras,
        Deudores,
        Inventario
    }
}
