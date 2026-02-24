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
        public List<dynamic> ObtenerUsuarios()
        {
            var lista = new List<dynamic>();

            try
            {
                Abrir();

                // ✅ dbo. para evitar problemas de esquema
                using (SqlCommand cmd = new SqlCommand("dbo.SP_CargarUsuarios", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // SP: SELECT id_usuario, nombre_usuario AS NombreCompleto
                            lista.Add(new
                            {
                                Usuario_id = Convert.ToInt32(reader["id_usuario"]),
                                NombreCompleto = reader["NombreCompleto"].ToString()
                            });
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

            return lista;
        }

        // 2) SP_ContarFotosUsuario
        public int ContarFotosUsuario(int usuario_id)
        {
            int total = 0;

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("dbo.SP_ContarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;

                    object result = cmd.ExecuteScalar();
                    total = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
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
        public void GuardarFotoRostro(int usuario_id, byte[] rostro_data)
        {
            if (rostro_data == null || rostro_data.Length == 0)
                throw new ArgumentException("rostro_data está vacío");

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("dbo.SP_GuardarFotos", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;
                    cmd.Parameters.Add("@RostroData", SqlDbType.VarBinary, -1).Value = rostro_data; // -1 = MAX

                    cmd.ExecuteNonQuery();
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
        }

        // 4) SP_ObtenerRostrosPorUsuario
        public List<byte[]> ObtenerRostrosPorUsuario(int usuario_id)
        {
            var lista = new List<byte[]>();

            try
            {
                Abrir();

                using (SqlCommand cmd = new SqlCommand("dbo.SP_ObtenerRostrosPorUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // SP: SELECT rostro_data FROM Rostro ...
                            lista.Add((byte[])dr["rostro_data"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los rostros: " + ex.Message, ex);
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

                using (SqlCommand cmd = new SqlCommand("dbo.SP_BorrarFotosUsuario", sc))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Usuario_id", SqlDbType.Int).Value = usuario_id;

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