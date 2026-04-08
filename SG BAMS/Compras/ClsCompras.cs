using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SG_BAMS
{
    /// <summary>
    /// Clase que maneja operaciones relacionadas con compras y detalles de compras en la base de datos.
    /// </summary>
    public class ClsCompras
    {
        /// <summary>
        /// Obtiene los productos asociados a un proveedor específico.
        /// </summary>
        /// <param name="idProv">El identificador del proveedor.</param>
        /// <returns>Un <see cref="DataTable"/> con los productos del proveedor.</returns>
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
        /// Valida si un producto ya está incluido en una compra existente.
        /// </summary>
        /// <param name="idCompra">El identificador de la compra.</param>
        /// <param name="idProducto">El identificador del producto.</param>
        /// <returns><c>true</c> si el producto ya existe en la compra; de lo contrario, <c>false</c>.</returns>
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
        /// Agrega un detalle de producto a una compra existente.
        /// </summary>
        /// <param name="idCompra">El identificador de la compra.</param>
        /// <param name="idProducto">El identificador del producto.</param>
        /// <param name="cantidad">La cantidad de producto a agregar.</param>
        /// <param name="precio">El precio unitario del producto.</param>
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
        /// Representa un detalle de compra, incluyendo producto, cantidad y precio.
        /// </summary>
        public class DetalleCompra
        {
            /// <summary>
            /// Obtiene o establece el identificador del producto.
            /// </summary>
            public int IdProducto { get; set; }

            /// <summary>
            /// Obtiene o establece la cantidad del producto.
            /// </summary>
            public int Cantidad { get; set; }

            /// <summary>
            /// Obtiene o establece el precio unitario del producto.
            /// </summary>
            public decimal Precio { get; set; }
        }

        /// <summary>
        /// Guarda una nueva compra junto con sus detalles en la base de datos.
        /// </summary>
        /// <param name="idUsuario">El identificador del usuario que realiza la compra.</param>
        /// <param name="fecha">La fecha de la compra.</param>
        /// <param name="idPago">El identificador del tipo de pago.</param>
        /// <param name="idProv">El identificador del proveedor.</param>
        /// <param name="nota">Una nota o descripción de la compra.</param>
        /// <param name="detalles">La lista de detalles de la compra.</param>
        /// <returns><c>true</c> si la compra se guarda correctamente; de lo contrario, lanza una excepción.</returns>
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