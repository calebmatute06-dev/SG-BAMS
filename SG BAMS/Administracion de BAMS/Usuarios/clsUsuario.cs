using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    internal class clsUsuario : ClsConexion
    {
        public async Task<DataTable> LeerUsuariosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                string query = "SELECT * FROM v_DetalleUsuarios";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    // Ejecutamos de forma asíncrona mediante un Reader
                    using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                    {
                        // Cargamos los resultados en el DataTable
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

        public async Task<bool> InsertarUsuarioAsync(string nombre, string password, int idRol, int idEstado, byte[] imagen)
        {
            try
            {

                AbrirConexion();

                string query = @"INSERT INTO Usuario (nombre_usuario, contraseña_login, id_rol_usuario, id_estado, imagen_usuario) 
                         VALUES (@nombre, @pass, @rol, @estado, @img)";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@rol", idRol);
                    cmd.Parameters.AddWithValue("@estado", idEstado);

                    // Manejo de imagen nula
                    SqlParameter paramImg = new SqlParameter("@img", SqlDbType.VarBinary);
                    paramImg.Value = (object)imagen ?? DBNull.Value;
                    cmd.Parameters.Add(paramImg);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

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

        public async Task<bool> ModificarUsuarioAsync(int id, string nombre, string password, int idRol, int idEstado, byte[] imagen)
        {
            try
            {
                AbrirConexion();
                string query = @"UPDATE Usuario 
                         SET nombre_usuario = @nombre, 
                             contraseña_login = @pass, 
                             id_rol_usuario = @rol, 
                             id_estado = @estado, 
                             imagen_usuario = @img 
                         WHERE id_usuario = @id";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@pass", password);
                    cmd.Parameters.AddWithValue("@rol", idRol);
                    cmd.Parameters.AddWithValue("@estado", idEstado);

                    SqlParameter paramImg = new SqlParameter("@img", SqlDbType.Image);
                    paramImg.Value = (object)imagen ?? DBNull.Value;
                    cmd.Parameters.Add(paramImg);

                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

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


