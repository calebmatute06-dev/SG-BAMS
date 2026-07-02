using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS.ProductoInventario
{
    internal class ClsModificarCompras : ClsRepositorioBaseDatos
    {
        

        public DataTable ListarFormasPago()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_FormasPago_Listar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex) { throw new Exception("Error al listar formas de pago: " + ex.Message); }
            finally { Cerrar(); }
            return dt;
        }

        public DataTable ListarProveedoresActivos()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Proveedores_Activos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex) { throw new Exception("Error al listar proveedores: " + ex.Message); }
            finally { Cerrar(); }
            return dt;
        }

        public DataTable ObtenerDetalleCompra(int idCompra)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_ListarProductos", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex) { throw new Exception("Error al obtener detalle: " + ex.Message); }
            finally { Cerrar(); }
            return dt;
        }

        public DataTable ObtenerCabeceraCompra(int idCompra)
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_ObtenerCabecera", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idCompra);
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        da.Fill(dt);
                }
            }
            catch (Exception ex) { throw new Exception("Error en cabecera: " + ex.Message); }
            finally { Cerrar(); }
            return dt;
        }

        public void GuardarCambiosDetalle(int idCompra, int idProd, int cant, decimal precio)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_ActualizarDetalleCompra", Conectar))
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
            finally { Cerrar(); }
        }

        public void ActualizarCabeceraCompra(int idCompra, int idProv, int idPago, DateTime fecha, string nota)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_ActualizarCabecera", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.Parameters.AddWithValue("@idProv", idProv);
                    cmd.Parameters.AddWithValue("@idPago", idPago);
                    cmd.Parameters.AddWithValue("@fecha", fecha);
                    cmd.Parameters.AddWithValue("@nota", nota);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { Cerrar(); }
        }

        public void EliminarProductoDeBD(int idCompra, int idProd)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_EliminarProductoDeCompra", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_compra", idCompra);
                    cmd.Parameters.AddWithValue("@id_producto", idProd);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { Cerrar(); }
        }

        public void RevertirStockProductoNuevo(int idCompra, int idProd, int cant)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_RevertirStockEliminarProducto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idC", idCompra);
                    cmd.Parameters.AddWithValue("@idP", idProd);
                    cmd.Parameters.AddWithValue("@cant", cant);
                    cmd.ExecuteNonQuery();
                }
            }
            finally { Cerrar(); }
        }

        public bool EliminarCompraCompleta(int idCompra)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_EliminarCompleta", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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
                Cerrar();
            }
        }
    }
}