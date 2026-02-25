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
                // Importante: Usar el nombre exacto de tu método en ClsConexion
                AbrirConexion();

                // Llamamos a la vista que creamos en SQL
                string query = "SELECT * FROM v_DetalleTipoProducto";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    // Ejecución asíncrona para no bloquear la interfaz
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
                // Nota: Usamos el nombre de columna 'descripcion_forma_pago' según tu script de BD
                string query = "INSERT INTO Tipo_producto (descripcion_forma_pago) VALUES (@desc)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@desc", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el tipo de producto: " + ex.Message);
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
                string query = "UPDATE Tipo_producto SET descripcion_forma_pago = @desc WHERE id_tipo_producto = @id";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@desc", nuevaDescripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el tipo de producto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
