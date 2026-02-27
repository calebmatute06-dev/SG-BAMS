
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.TipoProd
{
    internal class clsTipoProducto : ClsConexion
    {
        public async Task<DataTable> LeerTiposProductoAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();

                string query = "SELECT * FROM v_DetalleTipoProducto";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        tabla.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los tipos de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarTipoProductoAsync(string descripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_tipo_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@descripcion_forma_pago", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar tipo de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> ModificarTipoProductoAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_tipo_producto", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_tipo_producto", id);
                    cmd.Parameters.AddWithValue("@descripcion_forma_pago", nuevaDescripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el tipo de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
