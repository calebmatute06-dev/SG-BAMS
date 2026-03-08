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
    }
}