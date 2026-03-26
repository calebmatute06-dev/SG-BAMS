using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class ClsAgregarProductos:ClsConexion
    {

        public async Task GuardarProductoFactura(int idFactura, int idProducto, int cantidad)
        {
            AbrirConexion();

            using (SqlCommand cmd = new SqlCommand("PA_insertar_factura_producto ", Conectar))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@id_factura", idFactura);
                cmd.Parameters.AddWithValue("@id_producto", idProducto);
                cmd.Parameters.AddWithValue("@cantidad", cantidad);

                await cmd.ExecuteNonQueryAsync();
            }

            Cerrar();
        }

        public async Task<DataRow> ObtenerProductoPorCodigoBarra(string codigoBarra)
        {
            ClsConexion objConexion = new ClsConexion();
            try
            {
                objConexion.AbrirConexion();
                string query = @"SELECT id_producto, 
                                nombre_producto, 
                                precio_venta, 
                                stock
                         FROM Vista_Producto_CodigoBarra
                         WHERE codigo_barra = @codigo";

                using (SqlCommand cmd = new SqlCommand(query, objConexion.Conectar))
                {
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
                string query = "SELECT * FROM vista_nombre_productos";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
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
