using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación del servicio de notificaciones que utiliza procedimientos
    /// almacenados para listar y marcar como leídas las notificaciones del sistema.
    /// </summary>
    internal class ClsNotificaciones : INotificacionesService
    {
        private readonly ClsRepositorioBaseDatos repositorio;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public ClsNotificaciones() : this(new ClsRepositorioBaseDatos())
        {
        }

        /// <summary>
        /// Constructor principal que recibe el repositorio de base de datos.
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos para ejecutar las consultas.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsNotificaciones(ClsRepositorioBaseDatos repositorio)
        {
            this.repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        /// <inheritdoc/>
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_Listar", repositorio.Conectar))
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
                repositorio.Cerrar();
            }
            return tablaDatos;
        }

        /// <inheritdoc/>
        public async Task<bool> MarcarComoLeida(int idNotificacion)
        {
            try
            {
                repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_MarcarLeida", repositorio.Conectar))
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
                repositorio.Cerrar();
            }
        }
    }
}