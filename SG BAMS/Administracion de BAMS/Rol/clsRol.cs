using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Rol
{
    /// <summary>
    /// Provee los métodos de acceso a datos para la gestión de roles de usuario en el sistema.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsRol : ClsConexion
    {
        /// <summary>
        /// Obtiene el listado completo de roles desde la vista detallada de la base de datos de forma asíncrona.
        /// </summary>
        /// <returns>Un DataTable con la información de los roles.</returns>
        /// <exception cref="System.Exception">Lanzada si ocurre un error durante la consulta SQL.</exception>
        public async Task<DataTable> LeerRolesAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
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

        /// <summary>
        /// Registra un nuevo rol de usuario en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="descripcion">La descripción o nombre del nuevo rol.</param>
        /// <returns>True si el rol fue insertado correctamente; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada si el procedimiento almacenado falla.</exception>
        public async Task<bool> InsertarRolAsync(string descripcion)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_rol", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@descripcion_rol", descripcion);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el rol: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Actualiza la descripción de un rol existente de forma asíncrona.
        /// </summary>
        /// <param name="id">El identificador único del rol a modificar.</param>
        /// <param name="nuevaDescripcion">La nueva descripción que se asignará al rol.</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada si ocurre un error en la base de datos.</exception>
        public async Task<bool> ModificarRolAsync(int id, string nuevaDescripcion)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_actualizar_rol", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_rol_usuario", id);
                    cmd.Parameters.AddWithValue("@descripcion_rol", nuevaDescripcion);

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