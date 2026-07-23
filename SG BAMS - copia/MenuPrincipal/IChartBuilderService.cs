using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para la construcción de gráficos del panel de dashboard.
    /// </summary>
    public interface IChartBuilderService
    {
        /// <summary>
        /// Carga y configura el gráfico de tipo doughnut con los datos de stock de productos.
        /// </summary>
        /// <param name="chart">Control Chart donde se mostrará el gráfico.</param>
        /// <param name="tablaStock">Datos de stock de productos.</param>
        Task CargarGraficoStock(Chart chart, DataTable tablaStock);

        /// <summary>
        /// Carga y configura el gráfico de barras con los productos más vendidos.
        /// </summary>
        /// <param name="chart">Control Chart donde se mostrará el gráfico.</param>
        /// <param name="datosVentas">Datos de ventas de productos.</param>
        Task CargarGraficoMasVendidos(Chart chart, DataTable datosVentas);
    }
}