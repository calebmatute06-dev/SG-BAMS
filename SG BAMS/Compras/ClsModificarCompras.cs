using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsModificarCompras
    {
        private ClsConexion conexion = new ClsConexion();

        public DataTable ListarFormasPago()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                string query = "SELECT id_tipo_forma_pago, descripcion_forma_pago FROM Tipo_Forma_de_pago";
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.Conectar);
                da.Fill(dt);
            }
            catch (Exception ex) { throw new Exception("Error al listar formas de pago: " + ex.Message); }
            finally { conexion.Cerrar(); }
            return dt;
        }

        public DataTable ListarProveedoresActivos()
        {
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                string query = "SELECT id_proveedor, nombre_proveedor FROM Proveedor WHERE id_estado = 1";
                SqlDataAdapter da = new SqlDataAdapter(query, conexion.Conectar);
                da.Fill(dt);
            }
            catch (Exception ex) { throw new Exception("Error al listar proveedores: " + ex.Message); }
            finally { conexion.Cerrar(); }
            return dt;
        }

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
            catch (Exception ex) { throw new Exception("Error al obtener detalle: " + ex.Message); }
            finally { conexion.Cerrar(); }
            return dt;
        }

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
            catch (Exception ex) { throw new Exception("Error en cabecera: " + ex.Message); }
            finally { conexion.Cerrar(); }
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
            catch (Exception ex) { throw new Exception("Error al procesar producto " + idProd + ": " + ex.Message); }
            finally { conexion.Cerrar(); }
        }

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

        public void RevertirStockProductoNuevo(int idCompra, int idProd, int cant)
        {
            try
            {
                conexion.AbrirConexion();
                string sql = @"
                    UPDATE Inventario SET stock = stock - @cant WHERE id_producto = @idP;
                    DELETE FROM Compra_producto WHERE id_compra = @idC AND id_producto = @idP;";

                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@cant", cant);
                    cmd.Parameters.AddWithValue("@idP", idProd);
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { conexion.Cerrar(); }
        }

        public bool EliminarCompraCompleta(int idCompra)
        {
            try
            {
                conexion.AbrirConexion();
                string sql = @"
            UPDATE I
            SET I.stock = I.stock - CP.cantidad
            FROM Inventario I
            INNER JOIN Compra_producto CP ON I.id_producto = CP.id_producto
            WHERE CP.id_compra = @id;

            DELETE FROM Compra_producto WHERE id_compra = @id;
            DELETE FROM Compra WHERE id_compra = @id;";

                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar la compra y ajustar stock: " + ex.Message);
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}