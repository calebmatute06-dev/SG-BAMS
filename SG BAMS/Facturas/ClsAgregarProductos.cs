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

        public async Task<double> ObtenerPrecioProducto(int idProducto)
        {
            double precio = 0;
            

            try
            {
                AbrirConexion();
                string query = "SELECT precio_venta FROM vista_precio WHERE id_producto = @IdProducto";

                using (SqlCommand cmd = new SqlCommand(query,Conectar))
                {
                    cmd.Parameters.AddWithValue("@IdProducto", idProducto);

                    object result = await cmd.ExecuteScalarAsync();

                    if (result != null)
                        precio = Convert.ToDouble(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener precio: " + ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();
            }

            return precio;
        }

        public async Task<DataTable> ObtenerStockProductos()
        {
           
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                string query = "SELECT * FROM vista_stock_productos";

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
