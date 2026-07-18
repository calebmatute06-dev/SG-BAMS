using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para la construcción de gráficos del dashboard.
    /// </summary>
    public interface IChartBuilderService
    {
        /// <summary>Carga el gráfico de doughnut de stock en el chart especificado.</summary>
        Task CargarGraficoStock(Chart chart, DataTable tablaStock);

        /// <summary>Carga el gráfico de barras de productos más vendidos.</summary>
        Task CargarGraficoMasVendidos(Chart chart, DataTable datosVentas);
    }
}