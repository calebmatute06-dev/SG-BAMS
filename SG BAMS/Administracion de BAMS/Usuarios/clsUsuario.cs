using Microsoft.Data.SqlClient;
using SG_BAMS.Login;
using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Administracion_de_BAMS.Usuarios
{
    /// <summary>
    /// Clase para gestión de usuarios usando solo Procedimientos Almacenados.
    /// </summary>
    internal class clsUsuario : ClsConexion
    {
        public async Task<DataTable> LeerUsuariosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuarios_Detalle", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public async Task<bool> InsertarUsuarioAsync(string nombre, string password, int idRol, byte[] imagen, string correo)
        {
            try
            {
                string passwordHasheado = ClsSeguridad.HashSHA256(password);
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_insertar_usuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);
                    cmd.Parameters.AddWithValue("@correo_usuario", correo);
                    cmd.Parameters.AddWithValue("@contraseña_login", passwordHasheado);
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

        public async Task<DataTable> ListarRolesAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ListarRoles", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public async Task<bool> ModificarUsuarioAsync(int id, string nombre, string password,
                                                       int idRol, int idEstado, byte[] imagen, string correo)
        {
            try
            {
                string passwordHasheado = string.IsNullOrWhiteSpace(password) ? null : ClsSeguridad.HashSHA256(password);
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_usuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", id);
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);
                    cmd.Parameters.AddWithValue("@correo_usuario", correo);
                    SqlParameter paramPass = new SqlParameter("@contraseña_login", SqlDbType.VarChar, 64);
                    paramPass.Value = (object)passwordHasheado ?? DBNull.Value;
                    cmd.Parameters.Add(paramPass);
                    cmd.Parameters.AddWithValue("@id_rol_usuario", idRol);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);
                    SqlParameter paramImg = new SqlParameter("@imagen_usuario", SqlDbType.Image);
                    paramImg.Value = (object)imagen ?? DBNull.Value;
                    cmd.Parameters.Add(paramImg);
                    await cmd.ExecuteNonQueryAsync();
                    return true;
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

        public async Task<DataTable> ListarEstadosAsync()
        {
            DataTable tabla = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ListarEstados", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
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

        public async Task<bool> ExisteUsuarioAsync(string nombreUsuario, int idExcluir = 0)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ExisteNombre", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombreUsuario);
                    cmd.Parameters.AddWithValue("@id_excluir", idExcluir);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar existencia de usuario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public async Task<bool> ExisteCorreo(string correo)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ExisteCorreo", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    int count = (int)await cmd.ExecuteScalarAsync();
                    return count > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar correo: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public bool CorreoModificar(string correo, int idUsuarioActual = 0)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Usuario_ExisteCorreoModificar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@correo", correo);
                    cmd.Parameters.AddWithValue("@id", idUsuarioActual);
                    return Convert.ToInt32(cmd.ExecuteScalar()) > 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar correo: " + ex.Message);
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}