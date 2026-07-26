using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Define el contrato para el servicio de gestión de notificaciones del sistema.
    /// </summary>
    public interface INotificacionesService
    {
        /// <summary>
        /// Obtiene la lista de notificaciones filtradas según el tipo de usuario.
        /// </summary>
        /// <param name="esAdmin">True para obtener notificaciones de administrador, false para empleado.</param>
        /// <returns>DataTable con las notificaciones disponibles.</returns>
        DataTable ListarNotificaciones(bool esAdmin);

        /// <summary>
        /// Marca una notificación específica como leída en la base de datos.
        /// </summary>
        /// <param name="idNotificacion">Identificador único de la notificación a marcar.</param>
        /// <returns>True si la operación se realizó correctamente.</returns>
        Task<bool> MarcarComoLeida(int idNotificacion);
    }
}