using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// 
    /// </summary>
    internal class ClsVerProducto
    {
        /// <summary>
        /// La conexión a la base de datos
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Muestra el listado completo de productos.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener la lista: " + ex.Message</exception>
        public DataTable MostrarProductosCompleto()
        {
            DataTable tabla = new DataTable();
            string query = @"SELECT * FROM Vista_Productos_Detallada 
                             ORDER BY 
                                CASE WHEN [Estado] = 'Activo' THEN 1 ELSE 2 END ASC, 
                                [Stock Actual] DESC;";

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    using (SqlDataReader leer = cmd.ExecuteReader())
                    {
                        tabla.Load(leer);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Busca productos por filtro.
        /// </summary>
        /// <param name="filtro">El filtro de búsqueda.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al ejecutar procedimiento: " + ex.Message</exception>
        public DataTable BuscarProductos(string filtro)
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_BuscarProductos", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@filtro", filtro);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al ejecutar procedimiento: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}