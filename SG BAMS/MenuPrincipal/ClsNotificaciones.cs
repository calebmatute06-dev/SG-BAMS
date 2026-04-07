using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsNotificaciones : ClsConexion
    {
        /// <summary>
        /// Listars the notificaciones.
        /// </summary>
        /// <param name="esAdmin">if set to <c>true</c> [es admin].</param>
        /// <returns></returns>
        /// <exception cref="System.Exception">Error al cargar notificaciones: " + ex.Message</exception>
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                AbrirConexion();

               
                string query = "SELECT * FROM Notificaciones WHERE leida = 0";

                if (!esAdmin)
                {
                    query += " AND solo_admin = 0";
                }

                query += " ORDER BY fecha DESC";

                using (SqlDataAdapter adaptador = new SqlDataAdapter(query, Conectar))
                {
                    adaptador.Fill(tablaDatos);
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

        /// <summary>
        /// Marcars the como leida.
        /// </summary>
        /// <param name="idNotificacion">The identifier notificacion.</param>
        /// <returns></returns>
        public async Task<bool> MarcarComoLeida(int idNotificacion)
        {
            try
            {
                AbrirConexion();
                string query = "UPDATE Notificaciones SET leida = 1 WHERE id_notificacion = @id";

                using (SqlCommand comando = new SqlCommand(query, Conectar))
                {
                    comando.Parameters.AddWithValue("@id", idNotificacion);
                    int filasAfectadas = await comando.ExecuteNonQueryAsync();
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