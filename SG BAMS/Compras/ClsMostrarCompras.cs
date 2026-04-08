using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// 
    /// </summary>
    internal class ClsMostrarCompras
    {
        /// <summary>
        /// La conexión
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Lista las compras.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener las compras desde la base de datos: " + ex.Message</exception>
        public DataTable ListarCompras()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();

                string query = "SELECT * FROM Vista_ListadoCompras";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras desde la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}