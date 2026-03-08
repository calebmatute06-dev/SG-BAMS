using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsActualizarProducto
    {
        private ClsConexion conexion = new ClsConexion();

        public void EjecutarActualizacion(int id, string nombre, int idMarca, int idTipo, int idModelo, int idEstado, decimal precio, string servicio, string codBarra)
        {
            try
            {
                conexion.AbrirConexion();

 
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_producto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_producto", id);
                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;

                    cmd.Parameters.AddWithValue("@descripcion_tipo_servicio", servicio);
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto en la base de datos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}