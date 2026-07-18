using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para el servicio de perfil de usuario.
    /// </summary>
    public interface IPerfilService
    {
        /// <summary>
        /// Obtiene los datos del perfil desde la vista de la base de datos.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario logueado.</param>
        /// <returns>DataTable con los datos del perfil.</returns>
        Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario);

        /// <summary>
        /// Actualiza la foto de perfil de un usuario.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario.</param>
        /// <param name="imagenBytes">Bytes de la imagen a guardar.</param>
        Task ActualizarFotoUsuario(string nombreUsuario, byte[] imagenBytes);
    }
}