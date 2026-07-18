using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación de IDashboardService que proporciona contadores y gráficos
    /// para el panel principal (Dashboard).
    /// </summary>
    internal class ClsDashboard : IDashboardService
    {
        private readonly ClsRepositorioBaseDatos _repositorio;

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// 
        /// DIP: Recibe el repositorio por constructor en lugar de heredarlo
        /// o instanciarlo con "new".
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsDashboard(ClsRepositorioBaseDatos repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }



        /// <inheritdoc/>
        public async Task<int> ObtenerTotalClientes()
        {
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_TotalActivos", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<int> ObtenerTotalDeudores()
        {
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_TotalDeudores", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
        }

        /// <inheritdoc/>
        public async Task<int> ObtenerTotalProductos()
        {
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_TotalActivos", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
        }


        /// <inheritdoc/>
        public async Task<DataTable> ObtenerDatosGraficoStock()
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_stock_productos", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
            return tablaDatos;
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_productos_mas_vendidos", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
            return tablaVentas;
        }

        /// <inheritdoc/>
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_ultimas_ventas", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
            return tablaVentas;
        }
    }
}