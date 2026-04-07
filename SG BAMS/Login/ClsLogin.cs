using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Login
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsLogin : ClsConexion
    {
        /// <summary>
        /// The idusuario
        /// </summary>
        public static int idusuario;
        /// <summary>
        /// Validars the usuario.
        /// </summary>
        /// <param name="usuario">The usuario.</param>
        /// <param name="contra">The contra.</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al validar el usuario" + ex.Message</exception>
        public int ValidarUsuario(string usuario, string contra)
        {
            int rol = 0;

            try
            {
                AbrirConexion();

                string query = @"
                    SELECT id_rol_usuario, id_estado, id_usuario
                    FROM credenciales_usuarios
                    WHERE nombre_usuario = @usuario COLLATE Latin1_General_CS_AS
                      AND contraseña_login = @contra COLLATE Latin1_General_CS_AS";

                SqlCommand cmd = new SqlCommand(query, Conectar);
                cmd.Parameters.AddWithValue("@usuario", usuario);
                cmd.Parameters.AddWithValue("@contra", contra);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int estado = Convert.ToInt32(reader["id_estado"]);
                        if (estado == 1)
                        {
                            rol = Convert.ToInt32(reader["id_rol_usuario"]);
                            idusuario = Convert.ToInt32(reader["id_usuario"]);
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

                throw new Exception("Error al validar el usuario" + ex.Message);
            }
            finally
            {
                Cerrar();

            }

            return rol;


        }

        




    }
}
