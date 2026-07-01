using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace SG_BAMS.Login
{
    /// <summary>
    /// Clase encargada de la validación de credenciales usando solo PA.
    /// </summary>
    internal class ClsLogin : ClsRepositorioBaseDatos
    {
        public static int idusuario;
        public string NombreUsuario { get; set; }

        public int ValidarUsuario(string usuario_correo, string contra)
        {
            int rol = 0;
            try
            {
                string contraHasheada = ClsSeguridad.HashSHA256(contra);
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("sp_Login_ValidarUsuario", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario_correo", usuario_correo);
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