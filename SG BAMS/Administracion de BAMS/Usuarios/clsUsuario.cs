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
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class clsUsuario : ClsConexion
    {
        /// <summary>
        /// Leers the usuarios asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al obtener la lista de usuarios: " + ex.Message</exception>
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
        /// Insertars the usuario asynchronous.
        /// </summary>
        /// <param name="nombre">The nombre.</param>
        /// <param name="password">The password.</param>
        /// <param name="idRol">The identifier rol.</param>
        /// <param name="imagen">The imagen.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al insertar mediante procedimiento: " + ex.Message</exception>
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
        /// Listars the roles asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar roles: " + ex.Message</exception>
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
        /// Modificars the usuario asynchronous.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombre">The nombre.</param>
        /// <param name="password">The password.</param>
        /// <param name="idRol">The identifier rol.</param>
        /// <param name="idEstado">The identifier estado.</param>
        /// <param name="imagen">The imagen.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error: " + ex.Message</exception>
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
        /// Listars the estados asynchronous.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar estados: " + ex.Message</exception>
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


