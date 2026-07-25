using Microsoft.Data.SqlClient;
using SG_BAMS.Dominio;        
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

 
namespace SG_BAMS.AccesoDatos
{
    public class ClsFactura : ClsRepositorioBaseDatos
    {
        /// <summary>
        /// Agrega una factura a partir de los datos contenidos en el DTO.
        /// </summary>
        /// <param name="dto">Datos completos de la factura a registrar.</param>
        /// <returns>El id de la factura creada, o 0 si falló.</returns>
        public async Task<int> AgregarFacturas(FacturaDTO dto)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_factura", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", dto.IdUsuario);
                    cmd.Parameters.AddWithValue("@id_cliente", dto.IdCliente);
                    cmd.Parameters.AddWithValue("@id_tipo_forma_pago", dto.IdFormaPago);
                    cmd.Parameters.AddWithValue("@fecha_venta", dto.Fecha);
                    cmd.Parameters.AddWithValue("@bateria_vieja", dto.CantidadBateriaVieja);
                    cmd.Parameters.AddWithValue("@manejo_rebaja", dto.RebajaBateria);
                    cmd.Parameters.AddWithValue("@total_factura", dto.Total);
                    int idFactura = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                    return idFactura;
                }
            }
            catch (Exception ex)
            {
                throw new FacturaException("No se pudo agregar la factura.", ex);
            }
            finally
            {
                Cerrar();
            }
        }
 
        /// <summary>
        /// Obteners the formas pago.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerFormasPago()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_FormasPago_Listar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
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
 
        /// <summary>
        /// Se mantiene igual: guarda un producto individual de la factura.
        /// </summary>
        public async Task GuardarProductoFactura(int idFactura, int idProducto, int cantidad, double PrecioHistoria)
        {
            AbrirConexion();
            using (SqlCommand cmd = new SqlCommand("PA_insertar_factura_producto", Conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id_factura", idFactura);
                cmd.Parameters.AddWithValue("@id_producto", idProducto);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);
                cmd.Parameters.AddWithValue("@precio_historia", PrecioHistoria);
                await cmd.ExecuteNonQueryAsync();
            }
            Cerrar();
        }
 
        /// <summary>
        /// Nuevo: guarda todo el detalle de una factura de una sola vez,
        /// a partir de la lista de DetalleDTO. Reusa GuardarProductoFactura
        /// para no duplicar la lógica de conexión.
        /// </summary>
        /// <param name="idFactura">Id de la factura ya creada.</param>
        /// <param name="detalle">Lista de líneas de producto del DTO.</param>
        public async Task GuardarDetalleFactura(int idFactura, List<DetalleDTO> detalle)
        {
            foreach (var item in detalle)
            {
                await GuardarProductoFactura(idFactura, item.IdProducto, item.Cantidad, item.Precio);
            }
        }
 
        public async Task<DataRow> ObtenerProductoPorCodigoBarra(string codigoBarra)
        {
            ClsRepositorioBaseDatos objConexion = new ClsRepositorioBaseDatos();
            try
            {
                objConexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ObtenerPorCodigoBarra", objConexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@codigo", codigoBarra.Trim());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    await Task.Run(() => da.Fill(dt));
                    return dt.Rows.Count > 0 ? dt.Rows[0] : null;
                }
            }
            finally
            {
                objConexion.Cerrar();
            }
        }
 
        public async Task<DataTable> ObtenerStockProductos()
        {
            DataTable dt = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Producto_ListarConStock", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        dt.Load(reader);
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
 
    }
}