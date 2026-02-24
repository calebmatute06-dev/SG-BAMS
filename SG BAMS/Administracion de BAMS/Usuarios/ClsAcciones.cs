using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    public class ClsAcciones
    {
        // ✅ OPCIÓN A: Cadena directa en el código
        // Cambia SOLO estas 2 partes en tu PC:
        //   User ID=PEGA_TU_USUARIO_AQUI;
        //   Password=PEGA_TU_PASSWORD_AQUI;
        private readonly string cadenaConexion =
            "Data Source=AutoBattDB.mssql.somee.com;" +
            "Initial Catalog=AutoBattDB;" +
            "User ID=PEGA_TU_USUARIO_AQUI;" +
            "Password=PEGA_TU_PASSWORD_AQUI;" +
            "TrustServerCertificate=True;";

        private SqlConnection cn;

        private void Abrir()
        {
            if (cn == null) cn = new SqlConnection(cadenaConexion);
            if (cn.State == ConnectionState.Closed) cn.Open();
        }

        private void Cerrar()
        {
            if (cn != null && cn.State == ConnectionState.Open) cn.Close();
        }

        public List<dynamic> ObtenerUsuarios()
        {
            var lista = new List<dynamic>();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_CargarUsuarios", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new
                            {
                                Usuario_id = Convert.ToInt32(dr["id_usuario"]),
                                NombreCompleto = dr["NombreCompleto"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar usuarios: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return lista;
        }

        public int ContarFotosUsuario(int usuario_id)
        {
            int total = 0;

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ContarFotosUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    object result = cmd.ExecuteScalar();
                    total = (result == null || result == DBNull.Value) ? 0 : Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al contar fotos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return total;
        }

        public void GuardarFotoRostro(int usuario_id, byte[] rostro_data)
        {
            if (rostro_data == null || rostro_data.Length == 0)
                throw new ArgumentException("rostro_data está vacío");

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_GuardarFotos", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    cmd.Parameters.Add("@RostroData", SqlDbType.VarBinary, rostro_data.Length).Value = rostro_data;

                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al guardar rostro: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }

        public List<byte[]> ObtenerRostrosPorUsuario(int usuario_id)
        {
            var lista = new List<byte[]>();

            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_ObtenerRostrosPorUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            // Asegúrate que tu SP devuelva esta columna con este nombre
                            if (dr["rostro_data"] != DBNull.Value)
                                lista.Add((byte[])dr["rostro_data"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener rostros: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }

            return lista;
        }

        public void BorrarFotosUsuario(int usuario_id)
        {
            try
            {
                Abrir();
                using (SqlCommand cmd = new SqlCommand("SP_BorrarFotosUsuario", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Usuario_id", usuario_id);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al borrar fotos: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
        }
    }
}