using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato completo para el servicio de dashboard,
    /// incluyendo contadores y datos para gráficos del panel principal.
    /// </summary>
    public interface IDashboardService : IContadoresDashboardService, IGraficosDashboardService
    {
    }

    /// <summary>
    /// Define el contrato para los contadores estadísticos del dashboard.
    /// </summary>
    public interface IContadoresDashboardService
    {
        /// <summary>
        /// Obtiene el total de clientes activos en el sistema.
        /// </summary>
        /// <returns>Cantidad de clientes activos, o -1 si ocurre un error.</returns>
        Task<int> ObtenerTotalClientes();

        /// <summary>
        /// Obtiene el total de deudores activos en el sistema.
        /// </summary>
        /// <returns>Cantidad de deudores activos, o -1 si ocurre un error.</returns>
        Task<int> ObtenerTotalDeudores();

        /// <summary>
        /// Obtiene el total de productos activos en el inventario.
        /// </summary>
        /// <returns>Cantidad de productos activos, o -1 si ocurre un error.</returns>
        Task<int> ObtenerTotalProductos();
    }

    /// <summary>
    /// Define el contrato para la obtención de datos de gráficos del dashboard.
    /// </summary>
    public interface IGraficosDashboardService
    {
        /// <summary>
        /// Obtiene los datos para el gráfico de stock de productos.
        /// </summary>
        /// <returns>DataTable con los datos de stock, o null si ocurre un error.</returns>
        Task<DataTable> ObtenerDatosGraficoStock();

        /// <summary>
        /// Obtiene los datos de los productos más vendidos para el gráfico de barras.
        /// </summary>
        /// <returns>DataTable con los productos más vendidos.</returns>
        Task<DataTable> ObtenerProductosMasVendidos();

        /// <summary>
        /// Obtiene los datos de las últimas ventas registradas en el sistema.
        /// </summary>
        /// <returns>DataTable con las ventas recientes, o null si ocurre un error.</returns>
        Task<DataTable> ObtenerVentasRecientes();
    }
}