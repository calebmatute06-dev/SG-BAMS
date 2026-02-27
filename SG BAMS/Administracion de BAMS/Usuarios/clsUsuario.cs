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

        public async Task<bool> InsertarUsuarioAsync(string nombre, string password, int idRol, byte[] imagen)
        {
            try
            {
                // Usamos tu método de apertura de conexión
                AbrirConexion();

                // 1. Nombre del Procedimiento Almacenado en lugar de la consulta SQL
                using (SqlCommand cmd = new SqlCommand("PA_insertar_usuario", Conectar))
                {
                    // 2. IMPORTANTE: Definir el tipo de comando como StoredProcedure
                    cmd.CommandType = CommandType.StoredProcedure;

                    // 3. Los parámetros deben llamarse IGUAL que en el PROCEDURE de SQL
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);
                    cmd.Parameters.AddWithValue("@contraseña_login", password);
                    cmd.Parameters.AddWithValue("@id_rol_usuario", idRol);

                    // Manejo de imagen (Usando VarBinary para que coincida con el tipo image/blob)
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
                AbrirConexion(); // Usando tu método corregido
                using (SqlCommand cmd = new SqlCommand("PA_actualizar_usuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", id);
                    cmd.Parameters.AddWithValue("@nombre_usuario", nombre);

                    // Si el txt de la contraseña está vacío, AddWithValue enviará una cadena vacía ""
                    // Y el CASE de SQL que pusimos arriba dirá: "Ah, está vacío, dejo la contra vieja".
                    cmd.Parameters.AddWithValue("@contraseña_login", password.Trim());

                    cmd.Parameters.AddWithValue("@id_rol_usuario", idRol);
                    cmd.Parameters.AddWithValue("@id_estado", idEstado);

                    // Para la imagen (si es nula enviamos DBNull)
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


