using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Clase unificada para alimentar el panel principal (Dashboard).
    /// Consolida: ClsContadorCliente, ClsContadorDeuda, ClsContadorProducto,
    /// ClsGraficoStock, ClsGraficoVentas, ClsUltimasVentas.
    /// </summary>
    internal class ClsDashboard : ClsRepositorioBaseDatos
    {
        // ========== CONTADORES ==========

        /// <summary>
        /// Obtiene el total de clientes activos.
        /// </summary>
        public async Task<int> ObtenerTotalClientes()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Cliente_TotalActivos", Conectar))
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
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene el total de deudores activos.
        /// </summary>
        public async Task<int> ObtenerTotalDeudores()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_TotalDeudores", Conectar))
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
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene el total de productos activos.
        /// </summary>
        public async Task<int> ObtenerTotalProductos()
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_TotalActivos", Conectar))
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
                Cerrar();
            }
        }

        // ========== GRÁFICOS ==========

        /// <summary>
        /// Obtiene los datos del gráfico de stock.
        /// </summary>
        public async Task<DataTable> ObtenerDatosGraficoStock()
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_stock_productos", Conectar))
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
                Cerrar();
            }
            return tablaDatos;
        }

        /// <summary>
        /// Obtiene los productos más vendidos para el gráfico.
        /// </summary>
        public async Task<DataTable> ObtenerProductosMasVendidos()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_productos_mas_vendidos", Conectar))
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
                Cerrar();
            }
            return tablaVentas;
        }

        /// <summary>
        /// Obtiene las últimas ventas registradas.
        /// </summary>
        public async Task<DataTable> ObtenerVentasRecientes()
        {
            DataTable tablaVentas = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_vista_ultimas_ventas", Conectar))
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
                Cerrar();
            }
            return tablaVentas;
        }
    }
}