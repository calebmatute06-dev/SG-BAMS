using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsVerProducto
    {
        private ClsConexion conexion = new ClsConexion();

        public DataTable MostrarProductosCompleto()
        {
            DataTable tabla = new DataTable();
            string query = "SELECT * FROM Vista_Productos_Detallada";
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