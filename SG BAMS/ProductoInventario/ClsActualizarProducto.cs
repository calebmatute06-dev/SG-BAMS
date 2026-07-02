using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Clase para actualizar productos usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsActualizarProducto : ClsRepositorioBaseDatos
    {
        

        public void EjecutarActualizacion(int id, string nombre, int idMarca, int idTipo,
            int idModelo, int idEstado, decimal precio, string codBarra, int idProveedor, int stock)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", id);
                    cmd.Parameters.AddWithValue("@nombre_producto", nombre);
                    cmd.Parameters.AddWithValue("@id_marca_producto", idMarca);
                    cmd.Parameters.AddWithValue("@id_tipo_producto", idTipo);
                    cmd.Parameters.AddWithValue("@id_modelo_auto", idModelo);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);
                    cmd.Parameters.Add("@precio_venta", SqlDbType.Money).Value = precio;
                    cmd.Parameters.AddWithValue("@codigo_barra", codBarra);
                    cmd.Parameters.AddWithValue("@id_proveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar producto e inventario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public bool ExisteProductoEnOtros(int idActual, string nombre, int idMarca, int idProveedor)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@id", idActual);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally { Cerrar(); }
        }

        public bool ExisteCodigoEnOtros(int idActual, string codigo)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ExisteCodigoEnOtros", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", idActual);
                    int conteo = Convert.ToInt32(cmd.ExecuteScalar());
                    return conteo > 0;
                }
            }
            finally { Cerrar(); }
        }
    }
}