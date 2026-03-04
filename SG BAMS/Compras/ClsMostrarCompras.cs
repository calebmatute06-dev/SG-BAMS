using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsMostrarCompras
    {
        private ClsConexion conexion = new ClsConexion();

        public DataTable ListarCompras()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();

                // Consulta para traer los datos de la tabla Compra
                // Ajusta los nombres de las columnas según tu base de datos
                string query = @"SELECT 
                                    id_compra AS [ID Compra],
                                    fecha_compra AS [Fecha],
                                    proveedor AS [Proveedor],
                                    total_compra AS [Total],
                                    descripcion AS [Descripción]
                                 FROM Compra";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener las compras: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
    }
}