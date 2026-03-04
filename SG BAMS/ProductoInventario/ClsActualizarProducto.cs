using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsActualizarProducto
    {
        // Instancia de tu clase de conexión
        private ClsConexion conexion = new ClsConexion();

        public void EjecutarActualizacion(int id, string nombre, int idMarca, int idTipo, int idModelo, int idEstado, decimal precio, string servicio, string codBarra)
        {
            try
            {
                conexion.AbrirConexion();

                // Usamos el nombre exacto de tu PA: PA_actualizar_producto
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_producto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos los parámetros siguiendo tu script SQL
                    cmd.Parameters.AddWithValue("@id_producto", id);
                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

                    // Para el dinero es mejor especificar el SqlDbType para evitar errores de redondeo
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;

                    cmd.Parameters.AddWithValue("@descripcion_tipo_servicio", servicio);
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);

                    // Ejecutamos la instrucción en la base de datos
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                // Lanzamos la excepción para que el Formulario pueda mostrarla en un MessageBox
                throw new Exception("Error al actualizar el producto en la base de datos: " + ex.Message);
            }
            finally
            {
                // Siempre cerramos la conexión pase lo que pase
                conexion.Cerrar();
            }
        }
    }
}