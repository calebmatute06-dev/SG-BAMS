using System;
using System.Data;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Abstracción del acceso a datos de reportes. ReportesAdmin (y
    /// cualquier prueba) depende de esta interfaz en lugar de depender de
    /// ClsReportesDatos como tipo concreto (resuelve RD01).
    /// </summary>
    public interface IReportesRepository
    {
        DataTable ReporteVentas(DateTime desde, DateTime hasta);
        DataTable ReporteCompras(DateTime desde, DateTime hasta);
        DataTable ReporteDeudores();
        DataTable ReporteInventario();
    }
}
