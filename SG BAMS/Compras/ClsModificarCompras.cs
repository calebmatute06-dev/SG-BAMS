using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsModificarCompras
    {
        // Instancia de tu clase de conexión ya existente
        private ClsConexion conexion = new ClsConexion();

        // Recupera los productos asociados a la compra para el DataGridView
        public DataTable ObtenerDetalleCompra(int idCompra)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                string query = @"SELECT 
                                    CP.id_producto AS [ID], 
                                    P.nombre_producto AS [Producto], 
                                    CP.cantidad AS [Cantidad], 
                                    CP.precio_costo_unitario AS [Precio],
                                    (CP.cantidad * CP.precio_costo_unitario) AS [Subtotal]
                                 FROM Compra_producto CP
                                 INNER JOIN Producto P ON CP.id_producto = P.id_producto
                                 WHERE CP.id_compra = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener detalle de productos: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }

        // Recupera los datos de la cabecera (Proveedor, Pago, Fecha) para los controles
        public DataTable ObtenerCabeceraCompra(int idCompra)
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                string query = "SELECT id_proveedor, id_tipo_forma_pago, fecha_pedido, desc_compra FROM Compra WHERE id_compra = @id";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener datos de la cabecera: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
            return dt;
        }
        public void GuardarCambiosDetalle(int idCompra, int idProd, int cant, decimal precio)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarDetalleCompra", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);
                    cmd.Parameters.AddWithValue("@id_producto", idProd);
                    cmd.Parameters.AddWithValue("@nueva_cantidad", cant);
                    cmd.Parameters.AddWithValue("@nuevo_precio", precio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al procesar producto " + idProd + ": " + ex.Message);
            }
            finally { conexion.Cerrar(); }
        }

        public void EliminarProductoDeBD(int idCompra, int idProd)
        {
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProductoDeCompra", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);
                    cmd.Parameters.AddWithValue("@id_producto", idProd);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }

        //
        public void ActualizarCabeceraCompra(int idCompra, int idProv, int idPago, DateTime fecha, string nota)
        {
            try
            {
                conexion.AbrirConexion();
                string query = @"UPDATE Compra SET id_proveedor = @idProv, id_tipo_forma_pago = @idPago, 
                         fecha_pedido = @fecha, desc_compra = @nota WHERE id_compra = @idC";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    cmd.Parameters.AddWithValue("@idPago", idPago);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }

        public bool EliminarCompraCompleta(int idCompra)
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();

                // 1. Restamos el stock en la tabla Inventario antes de borrar los detalles
                // 2. Borramos los productos de la compra (Compra_producto)
                // 3. Borramos la cabecera de la compra (Compra)
                string sql = @"
            -- Ajuste de Inventario
            UPDATE I
            SET I.stock = I.stock - CP.cantidad
            FROM Inventario I
            INNER JOIN Compra_producto CP ON I.id_producto = CP.id_producto
            WHERE CP.id_compra = @id;

            -- Eliminación de Detalles
            DELETE FROM Compra_producto WHERE id_compra = @id;

            -- Eliminación de Cabecera
            DELETE FROM Compra WHERE id_compra = @id;";

                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    int filasAfectadas = cmd.ExecuteNonQuery();

                    // Si se ejecutó correctamente, devolvemos true
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                // Esto atrapará cualquier error y lo enviará al MessageBox de tu formulario
                throw new Exception("Error al eliminar la compra y ajustar stock: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}