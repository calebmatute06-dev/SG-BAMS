using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para el servicio de dashboard que alimenta
    /// el panel principal con contadores y gráficos.
    /// </summary>
    public interface IDashboardService : IContadoresDashboardService, IGraficosDashboardService
    {
    }

    /// <summary>
    /// Define el contrato para los contadores del dashboard.
    /// 
    /// PRINCIPIOS SOLID APLICADOS:
    /// - ISP: Interfaz pequeña enfocada solo en contadores.
    /// </summary>
    public interface IContadoresDashboardService
    {
        /// <summary>Obtiene el total de clientes activos.</summary>
        Task<int> ObtenerTotalClientes();

        /// <summary>Obtiene el total de deudores activos.</summary>
        Task<int> ObtenerTotalDeudores();

        /// <summary>Obtiene el total de productos activos.</summary>
        Task<int> ObtenerTotalProductos();
    }

    /// <summary>
    /// Define el contrato para los gráficos del dashboard.
    /// 
    /// PRINCIPIOS SOLID APLICADOS:
    /// - ISP: Interfaz pequeña enfocada solo en gráficos.
    /// </summary>
    public interface IGraficosDashboardService
    {
        /// <summary>Obtiene los datos del gráfico de stock.</summary>
        Task<DataTable> ObtenerDatosGraficoStock();

        /// <summary>Obtiene los productos más vendidos para el gráfico.</summary>
        Task<DataTable> ObtenerProductosMasVendidos();

        /// <summary>Obtiene las últimas ventas registradas.</summary>
        Task<DataTable> ObtenerVentasRecientes();
    }
}