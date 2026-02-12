using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    internal class ClsLogin:Clsconexion
    {
        
        public int ValidarUsuario(string usuario, string contra)
        {


            int rol = 0;

            try
            {
                AbrirConexion();

                string query = @"
                    SELECT id_rol_usuario, id_estado
                    FROM credenciales_usuario
                    WHERE nombre_usuario = @usuario
                      AND contraseña_login = @contra";

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
