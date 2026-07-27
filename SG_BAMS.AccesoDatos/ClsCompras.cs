using System;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ComprasDTO;

namespace SG_BAMS.AccesoDatos
{
    public class ClsCompras : ClsRepositorioBaseDatos, IComprasRepository
    {
        private readonly IUsuarioSesion _usuarioSesion;

        /// <summary>
        /// Constructor por defecto para instancias simples.
        /// </summary>
        public ClsCompras()
        {
        }

        /// <summary>
        /// Constructor con inyección de dependencias para recibir la interfaz de sesión.
        /// </summary>
        public ClsCompras(IUsuarioSesion usuarioSesion)
        {
            _usuarioSesion = usuarioSesion;
        }

        public DataTable ObtenerProductosPorProveedor(int idProv)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_ProductosPorProveedor", Conectar))
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
                Cerrar();
            }
        }

        public bool ValidarProductoEnCompra(string idCompra, int idProducto)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_ValidarProducto", Conectar))
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
                Cerrar();
            }
        }

        public void AgregarDetalleACompraExistente(string idCompra, int idProducto, int cantidad, decimal precio)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compras_AgregarDetalle", Conectar))
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
                Cerrar();
            }
        }

        /// <summary>
        /// Registra una compra nueva completa (cabecera + detalle) a partir del CompraDTO.
        /// </summary>
        public bool GuardarNuevaCompra(CompraDTO compra)
        {
            AbrirConexion();
            SqlTransaction transaccion = Conectar.BeginTransaction();
            try
            {
                int idUsuario = _usuarioSesion != null ? _usuarioSesion.IdUsuario() : 1;
                if (idUsuario == 0)
                    throw new Exception("No se ha iniciado sesión o no se pudo obtener el ID del usuario.");

                int idCompra;
                using (SqlCommand cmd = new SqlCommand("sp_Compras_InsertarCabecera", Conectar, transaccion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idU", idUsuario);
                    cmd.Parameters.AddWithValue("@fecha", compra.Fecha);
                    cmd.Parameters.AddWithValue("@idPag", compra.IdFormaPago);
                    cmd.Parameters.AddWithValue("@idProv", compra.IdProveedor);
                    cmd.Parameters.AddWithValue("@desc", (object)compra.Nota ?? DBNull.Value);
                    idCompra = Convert.ToInt32(cmd.ExecuteScalar());
                }

                foreach (var item in compra.Detalle)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_Compras_InsertarDetalle", Conectar, transaccion))
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
                Cerrar();
            }
        }
    }
}