using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Rol
{
    internal class clsRol : ClsConexion
    {
        public async Task<DataTable> LeerRolesAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                // Usamos el método de apertura de tu clase base
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleRoles";

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
                throw new Exception("Error al obtener los roles: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        public async Task<bool> InsertarRolAsync(string descripcion)
        {
            try
            {
                AbrirConexion();
                // Según tu script, la columna es 'descripcion_rol'
                string query = "INSERT INTO Rol (descripcion_rol) VALUES (@desc)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@desc", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar el rol: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> ModificarRolAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();
                // Usamos el nombre de columna 'descripcion_rol' de tu script original
                string query = "UPDATE Rol SET descripcion_rol = @desc WHERE id_rol_usuario = @id";

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
                throw new Exception("Error al modificar el rol: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

    }
}
