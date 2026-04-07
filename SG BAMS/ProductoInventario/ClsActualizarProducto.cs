using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// 
    /// </summary>
    internal class ClsActualizarProducto
    {
        /// <summary>
        /// The conexion
        /// </summary>
        private ClsConexion conexion = new ClsConexion();
        /// <summary>
        /// Ejecutars the actualizacion.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="idMarca">The identifier marca.</param>
        /// <param name="idTipo">The identifier tipo.</param>
        /// <param name="idModelo">The identifier modelo.</param>
        /// <param name="idEstado">The identifier estado.</param>
        /// <param name="precio">The precio.</param>
        /// <param name="codBarra">The cod barra.</param>
        /// <param name="idProveedor">The identifier proveedor.</param>
        /// <param name="stock">The stock.</param>
        /// <exception cref="System.Exception">Error al actualizar producto e inventario: " + ex.Message</exception>
        public void EjecutarActualizacion(int id, string nombre, int idMarca, int idTipo, int idModelo, int idEstado, decimal precio, string codBarra, int idProveedor, int stock)
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
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Existes the producto en otros.
        /// </summary>
        /// <param name="idActual">The identifier actual.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="idMarca">The identifier marca.</param>
        /// <param name="idProveedor">The identifier proveedor.</param>
        /// <returns></returns>
        public bool ExisteProductoEnOtros(int idActual, string nombre, int idMarca, int idProveedor)
        {
            int conteo = 0;
            string sql = @"SELECT COUNT(*) 
                           FROM Producto p
                           INNER JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                           WHERE p.nombre_producto = @nombre 
                           AND p.id_marca_producto = @idMarca 
                           AND pp.id_proveedor = @idProveedor 
                           AND p.id_producto <> @id";
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@idMarca", idMarca);
                    cmd.Parameters.AddWithValue("@idProveedor", idProveedor);
                    cmd.Parameters.AddWithValue("@id", idActual);
                    conteo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally { conexion.Cerrar(); }
            return conteo > 0;
        }

        /// <summary>
        /// Existes the codigo en otros.
        /// </summary>
        /// <param name="idActual">The identifier actual.</param>
        /// <param name="codigo">The codigo.</param>
        /// <returns></returns>
        public bool ExisteCodigoEnOtros(int idActual, string codigo)
        {
            int conteo = 0;
            string sql = "SELECT COUNT(*) FROM Producto WHERE codigo_barra = @codigo AND id_producto <> @id";
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@codigo", codigo);
                    cmd.Parameters.AddWithValue("@id", idActual);
                    conteo = Convert.ToInt32(cmd.ExecuteScalar());
                }
            }
            finally { conexion.Cerrar(); }
            return conteo > 0;
        }
    }
}