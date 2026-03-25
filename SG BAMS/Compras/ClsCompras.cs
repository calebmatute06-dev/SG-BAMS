using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;

namespace SG_BAMS
{
    public class ClsCompras
    {
        public class DetalleCompra
        {
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }

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