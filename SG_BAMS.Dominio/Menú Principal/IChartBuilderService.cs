using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Contrato para construcción de gráficos.
    /// No expone tipos de UI (Chart) para evitar dependencias entre ensamblados con TFMs distintos.
    /// </summary>
    public interface IChartBuilderService
    {
        Task CargarGraficoStock(object chartHost, DataTable tablaStock);
        Task CargarGraficoMasVendidos(object chartHost, DataTable datosVentas);
    }
}