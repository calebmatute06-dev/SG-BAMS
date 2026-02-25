using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.ModeloAuto
{
    internal class clsModeloAuto : ClsConexion
    {
        public async Task<DataTable> LeerModelosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                // Usamos AbrirCConexion definido en tu ClsConexion
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleModelosAuto";

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
                throw new Exception("Error al obtener los modelos de auto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarModeloAutoAsync(string nombreModelo)
        {
            try
            {
                // Usamos el método de apertura de tu clase base ClsConexion
                AbrirConexion();

                string query = "INSERT INTO Modelo_de_auto (nombre_modelo_auto) VALUES (@nombre)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    // Evitamos inyección SQL usando parámetros
                    cmd.Parameters.AddWithValue("@nombre", nombreModelo);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el modelo de auto: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
