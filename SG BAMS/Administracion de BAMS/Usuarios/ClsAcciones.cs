using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    public class ClsAcciones
    {
        private String CadenaConexion = "Data Source = AutoBattDB.mssql.somee.com; " +
                                        "Initial catalog = AutoBattDB; " +
                                        "User ID = exobonnie_SQLLogin_1; " +
                                        "Password = w6et2uoghs;" +
                                        "TrustServerCertificate=True;";

        private SqlConnection sc;

        private void Abrir()
        {
            if (sc == null) sc = new SqlConnection(CadenaConexion);
            if (sc.State != ConnectionState.Open) sc.Open();
        }

        private void Cerrar()
        {
            if (sc != null && sc.State == ConnectionState.Open) sc.Close();
        }

        // 1) SP_CargarUsuarios
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> usuarios = new List<Usuario>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_CargarUsuarios", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = Convert.ToInt32(reader["id_usuario"]);
                            string nombre = reader["nombre_usuario"].ToString();

                            usuarios.Add(new Usuario(id, nombre));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar los usuarios: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return usuarios;
        }

        // 2) SP_ContarFotosUsuario
        public int ContarFotosUsuario(int usuario_id)
        {
            int total = 0;

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ContarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    object result = cmd.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        total = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar fotos del usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return total;
        }

        // 3) SP_GuardarFotos (tu SP NO devuelve ID; aquí es void)
        public int GuardarFotoRostro(int usuario_id, byte[] rostro_data)
        {
            int nuevoRostroId = 0;

            try
            {
                Abrir();

                using (SqlCommand command = new SqlCommand("SP_GuardarFotos", sc))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    command.Parameters.AddWithValue("@RostroData", rostro_data);

                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                        nuevoRostroId = Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar la foto del rostro: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return nuevoRostroId;
        }

        // 4) SP_ObtenerRostrosPorUsuario
        public List<byte[]> ObtenerRostrosPorUsuario(int usuario_id)
        {
            List<byte[]> lista = new List<byte[]>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_ObtenerRostrosPorUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        byte[] data = (byte[])dr["RostroData"];
                        lista.Add(data);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el rostro: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }

            return lista;
        }

        // 5) SP_BorrarFotosUsuario
        public void BorrarFotosUsuario(int usuario_id)
        {
            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("SP_BorrarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar fotos del usuario: " + ex.Message, ex);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}
public class Usuario
{
    public int Usuario_id { get; set; }
    public string NombreCompleto { get; set; }

    public Usuario(int usuario_id, string nombreCompleto)
    {
        Usuario_id = usuario_id;
        NombreCompleto = nombreCompleto;
    }
}