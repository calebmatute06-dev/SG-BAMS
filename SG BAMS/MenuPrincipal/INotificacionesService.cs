using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Define el contrato para el servicio de notificaciones.
    /// </summary>
    public interface INotificacionesService
    {
        /// <summary>
        /// Lista las notificaciones según el tipo de usuario.
        /// </summary>
        /// <param name="esAdmin">True si es administrador, false si es empleado.</param>
        /// <returns>DataTable con las notificaciones.</returns>
        DataTable ListarNotificaciones(bool esAdmin);

        /// <summary>
        /// Marca una notificación como leída.
        /// </summary>
        /// <param name="idNotificacion">ID de la notificación a marcar.</param>
        /// <returns>True si se marcó correctamente.</returns>
        Task<bool> MarcarComoLeida(int idNotificacion);
    }
}