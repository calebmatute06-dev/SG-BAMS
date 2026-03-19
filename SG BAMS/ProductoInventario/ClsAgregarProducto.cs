using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsAgregarProducto
    {
        private ClsConexion conexion = new ClsConexion();

        // Se eliminó el parámetro 'string servicio' de la firma del método
        public void EjecutarInsercion(string nombre, int idMarca, int idTipo, int idModelo, decimal precio, string codBarra, int idProveedor)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_producto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);
                    cmd.Parameters.AddWithValue("@id_proveedor", idProveedor); // El nuevo parámetro

                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }

        public bool ExisteNombreProducto(string nombre)
        {
            int conteo = 0;
            string query = "SELECT COUNT(*) FROM Producto WHERE nombre_producto = @nombre";

            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    conteo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar duplicados: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }

            return conteo > 0;
        }
    }
}