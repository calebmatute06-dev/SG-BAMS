using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Clase para gestionar notificaciones usando solo Procedimientos Almacenados.
    /// </summary>
    internal class ClsNotificaciones : ClsRepositorioBaseDatos
    {
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_Listar", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@esAdmin", esAdmin ? 1 : 0);
                    using (SqlDataAdapter adaptador = new SqlDataAdapter(cmd))
                    {
                        adaptador.Fill(tablaDatos);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cargar notificaciones: " + ex.Message);
            }
            finally
            {
                Cerrar();
            }
            return tablaDatos;
        }

        public async Task<bool> MarcarComoLeida(int idNotificacion)
        {
            try
            {
                AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_MarcarLeida", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", idNotificacion);
                    int filasAfectadas = await cmd.ExecuteNonQueryAsync();
                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                Cerrar();
            }
        }
    }
}