using System;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Implementación de INotificacionesService para gestionar notificaciones
    /// usando procedimientos almacenados.
    /// </summary>
    internal class ClsNotificaciones : INotificacionesService
    {
        private readonly ClsRepositorioBaseDatos _repositorio;

        /// <summary>
        /// Constructor sin parámetros para compatibilidad.
        /// </summary>
        public ClsNotificaciones() : this(new ClsRepositorioBaseDatos())
        {
        }

        /// <summary>
        /// Constructor principal con inyección de dependencias.
        /// 
        /// DIP: Recibe el repositorio por constructor en lugar de heredarlo.
        /// </summary>
        /// <param name="repositorio">Repositorio de base de datos.</param>
        /// <exception cref="ArgumentNullException">Si repositorio es nulo.</exception>
        public ClsNotificaciones(ClsRepositorioBaseDatos repositorio)
        {
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
        }

        /// <inheritdoc/>
        public DataTable ListarNotificaciones(bool esAdmin)
        {
            DataTable tablaDatos = new DataTable();
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_Listar", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
            return tablaDatos;
        }

        /// <inheritdoc/>
        public async Task<bool> MarcarComoLeida(int idNotificacion)
        {
            try
            {
                _repositorio.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Notificaciones_MarcarLeida", _repositorio.Conectar))
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
                _repositorio.Cerrar();
            }
        }
    }
}