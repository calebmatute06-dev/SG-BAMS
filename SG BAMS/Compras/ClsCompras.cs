using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using SG_BAMS.Login;

namespace SG_BAMS
{
    public class ClsCompras
    {
        public DataTable ObtenerProductosPorProveedor(int idProv)
        {
            ClsConexion conexion = new ClsConexion();
            DataTable dt = new DataTable();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_ProductosPorProveedor", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
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

        public bool ValidarProductoEnCompra(string idCompra, int idProducto)
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_ValidarProducto", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public void AgregarDetalleACompraExistente(string idCompra, int idProducto, int cantidad, decimal precio)
        {
            ClsConexion conexion = new ClsConexion();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_AgregarDetalle", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public class DetalleCompra
        {
            public int IdProducto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
        }

        public bool GuardarNuevaCompra(DateTime fecha, int idPago, int idProv, string nota, List<DetalleCompra> detalles)
        {
            ClsConexion conexion = new ClsConexion();
            conexion.AbrirConexion();
            SqlTransaction transaccion = conexion.Conectar.BeginTransaction();

            try
            {
                ClsPasarUsuario obtenerUsuario = new ClsPasarUsuario();
                int idUsuario = obtenerUsuario.IdUsuario();

                if (idUsuario == 0)
                    throw new Exception("No se ha iniciado sesión o no se pudo obtener el ID del usuario.");

                int idCompra;
                using (SqlCommand cmd = new SqlCommand("sp_Compras_InsertarCabecera", conexion.Conectar, transaccion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idU", idUsuario);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@idPag", idPago);
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    cmd.Parameters.AddWithValue("@desc", (object)nota ?? DBNull.Value);
                    idCompra = Convert.ToInt32(cmd.ExecuteScalar());
                }

                foreach (var item in detalles)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Compras_InsertarDetalle", conexion.Conectar, transaccion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@idC", idCompra);
                        cmd.Parameters.AddWithValue("@idP", item.IdProducto);
                        cmd.Parameters.AddWithValue("@cant", item.Cantidad);
                        cmd.Parameters.AddWithValue("@prec", item.Precio);
                        cmd.ExecuteNonQuery();
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