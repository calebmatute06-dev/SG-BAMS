using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase encargada de la validación de credenciales de acceso al sistema.
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsLogin : ClsConexion
    {
        /// <summary>
        /// El identificador del usuario autenticado.
        /// </summary>
        public static int idusuario;

        public string NombreUsuario { get; set; }

        /// <summary>
        /// Valida el usuario comparando la contraseña hasheada con SHA-256.
        /// </summary>
        /// <param name="usuario">El nombre de usuario.</param>
        /// <param name="contra">La contraseña en texto plano ingresada por el usuario.</param>
        /// <returns>
        /// Rol del usuario (1=Administrador, 2=Empleado, 3=Soporte),
        /// -1 si está inactivo, 0 si las credenciales son incorrectas.
        /// </returns>
        /// <exception cref="System.Exception">Error al validar el usuario.</exception>
        public int ValidarUsuario(string usuario, string contra)
        {
            int rol = 0;
            try
            {
                
                string contraHasheada = ClsSeguridad.HashSHA256(contra);

                AbrirConexion();

                
                string query = @"
                    SELECT ID_Rol_Usuario, ID_Estado, ID_Usuario, NombreUsuario
                    FROM credenciales_usuarios
                    WHERE Correo = @usuario COLLATE Latin1_General_CS_AS
                      AND Contraseña = @contra";

                SqlCommand cmd = new SqlCommand(query, Conectar);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contra", contraHasheada);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int estado = Convert.ToInt32(reader["ID_Estado"]);
                        if (estado == 1)
                        {
                            rol = Convert.ToInt32(reader["ID_Rol_Usuario"]);
                            idusuario = Convert.ToInt32(reader["ID_Usuario"]);
                            NombreUsuario = reader["NombreUsuario"].ToString();
                        }
                        else
                        {
                            rol = -1; 
                        }
                    }
                    else
                    {
                        rol = 0; 
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return rol;
        }
    }
}