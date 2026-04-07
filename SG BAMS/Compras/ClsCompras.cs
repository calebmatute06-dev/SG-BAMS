using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    public class ClsCompras
    {
        /// <summary>
        /// Obteners the productos por proveedor.
        /// </summary>
        /// <param name="idProv">The identifier prov.</param>
        /// <returns></returns>
        public DataTable ObtenerProductosPorProveedor(int idProv)
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();

            try
            {
                conexion.AbrirConexion();
                string query = @"SELECT p.id_producto, 
                         p.codigo_barra,
                         (p.nombre_producto + ' -- ' + m.nombre_marca) AS DisplayFull
                         FROM Producto p
                         INNER JOIN Proveedor_Producto pp ON p.id_producto = pp.id_producto
                         INNER JOIN Marca_producto m ON p.id_marca_producto = m.id_marca_producto
                         WHERE p.id_estado = 1 AND pp.id_proveedor = @idProv";

                using (SqlCommand cmd = new SqlCommand(query, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Validars the producto en compra.
        /// </summary>
        /// <param name="idCompra">The identifier compra.</param>
        /// <param name="idProducto">The identifier producto.</param>
        /// <returns></returns>
        public bool ValidarProductoEnCompra(string idCompra, int idProducto)
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                string sql = "SELECT COUNT(*) FROM Compra_producto WHERE id_compra = @idC AND id_producto = @idP";
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.Parameters.AddWithValue("@idP", idProducto);
                    return (int)cmd.ExecuteScalar() > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// Agregars the detalle a compra existente.
        /// </summary>
        /// <param name="idCompra">The identifier compra.</param>
        /// <param name="idProducto">The identifier producto.</param>
        /// <param name="cantidad">The cantidad.</param>
        /// <param name="precio">The precio.</param>
        public void AgregarDetalleACompraExistente(string idCompra, int idProducto, int cantidad, decimal precio)
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                string sql = "INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) VALUES (@idC, @idP, @cant, @prec)";
                using (SqlCommand cmd = new SqlCommand(sql, conexion.Conectar))
                {
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.Parameters.AddWithValue("@idP", idProducto);
                    cmd.Parameters.AddWithValue("@cant", cantidad);
                    cmd.Parameters.AddWithValue("@prec", precio);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public class DetalleCompra
        {
            /// <summary>
            /// Gets or sets the identifier producto.
            /// </summary>
            /// <value>
            /// The identifier producto.
            /// </value>
            public int IdProducto { get; set; }
            /// <summary>
            /// Gets or sets the cantidad.
            /// </summary>
            /// <value>
            /// The cantidad.
            /// </value>
            public int Cantidad { get; set; }
            /// <summary>
            /// Gets or sets the precio.
            /// </summary>
            /// <value>
            /// The precio.
            /// </value>
            public decimal Precio { get; set; }
        }

        /// <summary>
        /// Guardars the nueva compra.
        /// </summary>
        /// <param name="idUsuario">The identifier usuario.</param>
        /// <param name="fecha">The fecha.</param>
        /// <param name="idPago">The identifier pago.</param>
        /// <param name="idProv">The identifier prov.</param>
        /// <param name="nota">The nota.</param>
        /// <param name="detalles">The detalles.</param>
        /// <returns></returns>
        public bool GuardarNuevaCompra(int idUsuario, DateTime fecha, int idPago, int idProv, string nota, List<DetalleCompra> detalles)
        {
            ClsConexion conexion = new ClsConexion();
            conexion.AbrirConexion();
            SqlTransaction transaccion = conexion.Conectar.BeginTransaction();

            try
            {
                string queryCabecera = @"INSERT INTO Compra (id_usuario, fecha_pedido, id_tipo_forma_pago, id_proveedor, desc_compra) 
                                         VALUES (@idU, @fecha, @idPag, @idProv, @desc);
                                         SELECT SCOPE_IDENTITY();";

                int idCompra;
                using (SqlCommand cmd = new SqlCommand(queryCabecera, conexion.Conectar, transaccion))
                {
                    cmd.Parameters.AddWithValue("@idU", idUsuario);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@idPag", idPago);
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    cmd.Parameters.AddWithValue("@desc", (object)nota ?? DBNull.Value);
                    idCompra = Convert.ToInt32(cmd.ExecuteScalar());
                }

                foreach (var item in detalles)
                {
                    string queryInv = "IF NOT EXISTS (SELECT 1 FROM Inventario WHERE id_producto = @idP) INSERT INTO Inventario (id_producto, stock) VALUES (@idP, 0)";
                    using (SqlCommand cmdInv = new SqlCommand(queryInv, conexion.Conectar, transaccion))
                    {
                        cmdInv.Parameters.AddWithValue("@idP", item.IdProducto);
                        cmdInv.ExecuteNonQuery();
                    }

                    string queryDet = "INSERT INTO Compra_producto (id_compra, id_producto, cantidad, precio_costo_unitario) VALUES (@idC, @idP, @cant, @prec)";
                    using (SqlCommand cmdDet = new SqlCommand(queryDet, conexion.Conectar, transaccion))
                    {
                        cmdDet.Parameters.AddWithValue("@idC", idCompra);
                        cmdDet.Parameters.AddWithValue("@idP", item.IdProducto);
                        cmdDet.Parameters.AddWithValue("@cant", item.Cantidad);
                        cmdDet.Parameters.AddWithValue("@prec", item.Precio);
                        cmdDet.ExecuteNonQuery();
                    }
                }

                transaccion.Commit();
                return true;
            }
            catch (Exception)
            {
                transaccion.Rollback();
                throw;
            }
            finally
            {
                conexion.Cerrar();
            }
        }
    }
}