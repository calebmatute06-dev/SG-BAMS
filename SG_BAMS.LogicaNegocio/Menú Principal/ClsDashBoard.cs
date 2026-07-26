using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación del servicio de dashboard que proporciona contadores
    /// y datos para los gráficos del panel principal.
    /// </summary>
    public class ClsDashboard : IDashboardService
    {
        private readonly ClsRepositorioBaseDatos repositorio;

        /// <summary>
        /// Constructor del servicio de dashboard.
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos para ejecutar las consultas.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsDashboard(ClsRepositorioBaseDatos repositorio)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        /// <inheritdoc/>
        public async Task<int> ObtenerTotalClientes()
        {
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_TotalActivos", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                repositorio.Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<int> ObtenerTotalDeudores()
        {
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_TotalDeudores", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                repositorio.Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<int> ObtenerTotalProductos()
        {
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_TotalActivos", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    object resultado = await cmd.ExecuteScalarAsync();
                    return resultado != null ? Convert.ToInt32(resultado) : 0;
                }
            }
            catch (Exception)
            {
                return -1;
            }
            finally
            {
                repositorio.Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerDatosGraficoStock()
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_stock_productos", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaDatos.Load(reader);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                repositorio.Cerrar();
            }
            return tablaDatos;
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_productos_mas_vendidos", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener ventas para el gráfico: " + ex.Message);
            }
            finally
            {
                repositorio.Cerrar();
            }
            return tablaVentas;
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_ultimas_ventas", repositorio.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tablaVentas.Load(reader);
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                repositorio.Cerrar();
            }
            return tablaVentas;
        }
    }
}