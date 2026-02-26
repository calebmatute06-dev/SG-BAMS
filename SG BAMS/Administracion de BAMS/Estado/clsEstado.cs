using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Estado
{
    internal class clsEstado : ClsConexion
    {
        public async Task<DataTable> LeerEstadosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                // Usamos el método de apertura de tu clase ClsConexion
                AbrirConexion();

                string query = "SELECT * FROM v_DetalleEstados";

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
                throw new Exception("Error al cargar los estados: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarEstadoAsync(string descripcion)
        {
            try
            {
                // Usamos tu método de apertura de ClsConexion
                AbrirConexion();

                string query = "INSERT INTO Estado (descripcion_estado) VALUES (@desc)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@desc", descripcion);

                    // Ejecución asíncrona para evitar que la UI se congele
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el estado: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> ModificarEstadoAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();
                // El nombre del campo en tu BD es descripcion_estado
                string query = "UPDATE Estado SET descripcion_estado = @desc WHERE id_estado = @id";

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
                throw new Exception("Error al modificar el estado: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
