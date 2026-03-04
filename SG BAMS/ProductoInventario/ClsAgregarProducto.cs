using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsAgregarProducto
    {
        private ClsConexion conexion = new ClsConexion();

        public void EjecutarInsercion(string nombre, int idMarca, int idTipo, int idModelo, decimal precio, string servicio, string codBarra)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_producto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Agregamos los 7 parámetros que pide el PA actualizado
                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;
                    cmd.Parameters.AddWithValue("@descripcion_tipo_servicio", servicio);
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);

                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }
    }
}