using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Gestiona las operaciones de persistencia y lógica de datos para los usuarios del sistema, 
    /// incluyendo inserción, modificación y consulta mediante procedimientos almacenados y vistas.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsUsuario : ClsConexion
    {
        /// <summary>
        /// Recupera de forma asíncrona todos los usuarios detallados desde la vista de sistema.
        /// </summary>
        /// <returns>Un <see cref="DataTable"/> con la información completa de los usuarios.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error en la consulta SQL.</exception>
        public async Task<DataTable> LeerUsuariosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleUsuarios";

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
                throw new Exception("Error al obtener la lista de usuarios: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos de forma asíncrona.
        /// </summary>
        /// <param name="nombre">Nombre de identificación del usuario.</param>
        /// <param name="password">Contraseña de acceso.</param>
        /// <param name="idRol">Identificador del rol asignado.</param>
        /// <param name="imagen">Arreglo de bytes que representa la imagen de perfil o rostro del usuario.</param>
        /// <returns>True si el registro fue exitoso; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada cuando falla el procedimiento almacenado de inserción.</exception>
        public async Task<bool> InsertarUsuarioAsync(string nombre, string password, int idRol, byte[] imagen)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_usuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);
                    cmd.Parameters.AddWithValue("@contraseña_login", password);
                    cmd.Parameters.AddWithValue("@id_rol_usuario", idRol);

                    SqlParameter paramImg = new SqlParameter("@imagen_usuario", SqlDbType.Image);
                    paramImg.Value = (object)imagen ?? DBNull.Value;
                    cmd.Parameters.Add(paramImg);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar mediante procedimiento: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene de forma asíncrona el catálogo de roles disponibles en el sistema.
        /// </summary>
        /// <returns>Un <see cref="DataTable"/> con los identificadores y descripciones de los roles.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error al cargar la tabla de roles.</exception>
        public async Task<DataTable> ListarRolesAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT id_rol_usuario, descripcion_rol FROM Rol";
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
                throw new Exception("Error al cargar roles: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }

        /// <summary>
        /// Actualiza de forma asíncrona la información de un usuario existente.
        /// </summary>
        /// <param name="id">Identificador único del usuario a modificar.</param>
        /// <param name="nombre">Nuevo nombre de usuario.</param>
        /// <param name="password">Nueva contraseña o contraseña actual.</param>
        /// <param name="idRol">Nuevo identificador de rol.</param>
        /// <param name="idEstado">Nuevo identificador de estado (Activo/Inactivo).</param>
        /// <param name="imagen">Nuevo arreglo de bytes de la imagen del usuario.</param>
        /// <returns>True si la actualización fue exitosa; de lo contrario, False.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error en el procedimiento de actualización.</exception>
        public async Task<bool> ModificarUsuarioAsync(int id, string nombre, string password, int idRol, int idEstado, byte[] imagen)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_usuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", id);
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);
                    cmd.Parameters.AddWithValue("@contraseña_login", password.Trim());
                    cmd.Parameters.AddWithValue("@id_rol_usuario", idRol);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

                    SqlParameter paramImg = new SqlParameter("@imagen_usuario", SqlDbType.Image);
                    paramImg.Value = (object)imagen ?? DBNull.Value;
                    cmd.Parameters.Add(paramImg);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Obtiene de forma asíncrona el catálogo de estados (Activo, Inactivo, etc.) para los usuarios.
        /// </summary>
        /// <returns>Un <see cref="DataTable"/> con los registros de la tabla Estado.</returns>
        /// <exception cref="System.Exception">Lanzada cuando ocurre un error al cargar los estados.</exception>
        public async Task<DataTable> ListarEstadosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT id_estado, descripcion_estado FROM Estado";
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
                throw new Exception("Error al cargar estados: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tabla;
        }
    }
}