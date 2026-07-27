using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using SG_BAMS.ComprasContratos;
using SG_BAMS.ComprasDTO;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS.ProductoInventario
{
    public class ClsModificarCompras : ClsRepositorioBaseDatos, IModificarComprasRepository
    {
        private readonly ICargaCombosRepository _combos;
        private readonly IDetalleCompraRepository _detalleCompra;

        /// <summary>
        /// Constructor por defecto para compatibilidad con el diseñador:
        /// usa las implementaciones reales de catálogos y detalle.
        /// </summary>
        public ClsModificarCompras() : this(new ClsCargaCombos(), new ClsDetalleCompra()) { }

        /// <summary>
        /// Constructor con inyección de dependencias.
        /// </summary>
        public ClsModificarCompras(ICargaCombosRepository combos, IDetalleCompraRepository detalleCompra)
        {
            _combos = combos;
            _detalleCompra = detalleCompra;
        }

        /// <summary>
        /// Delegado a ICargaCombosRepository: ya no reimplementa esta
        /// consulta (antes duplicaba exactamente ClsCargaCombos.ListarFormasPago).
        /// </summary>
        public DataTable ListarFormasPago() => _combos.ListarFormasPago();

        /// <summary>
        /// Delegado a ICargaCombosRepository, mismo motivo que ListarFormasPago.
        /// </summary>
        public DataTable ListarProveedoresActivos() => _combos.ListarProveedoresActivos();

        /// <summary>
        /// Delegado a IDetalleCompraRepository: ya no reimplementa la
        /// consulta a sp_Compra_ListarProductos (antes duplicaba
        /// ClsDetalleCompra.ListarProductosDeCompra).
        /// </summary>
        public DataTable ObtenerDetalleCompra(int idCompra) => _detalleCompra.ListarProductosDeCompra(idCompra);

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

        /// <summary>
        /// Se mantiene igual: actualiza un único producto del detalle.
        /// Se sigue exponiendo por si algún flujo necesita actualizar un
        /// solo producto de forma aislada.
        /// </summary>
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

        /// <summary>
        /// Actualiza todo el detalle de una compra en una única transacción.
        /// Antes cada línea abría y cerraba su propia conexión (llamando a
        /// GuardarCambiosDetalle en un bucle); ahora, si una línea falla,
        /// se revierten todas, evitando datos inconsistentes.
        /// </summary>
        public void ActualizarDetalleCompra(int idCompra, List<DetalleCompraDTO> detalle)
        {
            AbrirConexion();
            SqlTransaction transaccion = Conectar.BeginTransaction();
            try
            {
                foreach (var item in detalle)
                {
                    using (SqlCommand cmd = new SqlCommand("sp_ActualizarDetalleCompra", Conectar, transaccion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_compra", idCompra);
                        cmd.Parameters.AddWithValue("@id_producto", item.IdProducto);
                        cmd.Parameters.AddWithValue("@nueva_cantidad", item.Cantidad);
                        cmd.Parameters.AddWithValue("@nuevo_precio", item.Precio);
                        cmd.ExecuteNonQuery();
                    }
                }
                transaccion.Commit();
            }
            catch (Exception ex)
            {
                transaccion.Rollback();
                throw new Exception("Error al actualizar el detalle de la compra: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Actualiza la cabecera de una compra existente a partir del CompraDTO.
        /// </summary>
        public void ActualizarCabeceraCompra(CompraDTO compra)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Compra_ActualizarCabecera", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idC", compra.IdCompra);
                    cmd.Parameters.AddWithValue("@idProv", compra.IdProveedor);
                    cmd.Parameters.AddWithValue("@idPago", compra.IdFormaPago);
                    cmd.Parameters.AddWithValue("@fecha", compra.Fecha);
                    cmd.Parameters.AddWithValue("@nota", compra.Nota);
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

                    return filasAfectadas > 0 || filasAfectadas == -1;
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
