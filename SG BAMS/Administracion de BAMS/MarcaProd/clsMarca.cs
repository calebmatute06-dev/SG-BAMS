using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.MarcaProd
{
    internal class clsMarca : ClsConexion
    {
        public async Task<DataTable> LeerMarcasAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                // Usamos tu método AbrirCConexion de ClsConexion
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleMarcas";

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
                throw new Exception("Error al obtener las marcas: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarMarcaAsync(string nombreMarca)
        {
            try
            {
                // Usamos tu método de apertura de conexión
                AbrirConexion();

                string query = "INSERT INTO Marca_producto (nombre_marca) VALUES (@nombre)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombreMarca);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar la marca: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> ModificarMarcaAsync(int id, string nuevoNombre)
        {
            try
            {
                AbrirConexion();
                string query = "UPDATE Marca_producto SET nombre_marca = @nombre WHERE id_marca_producto = @id";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nuevoNombre);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar la marca: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
