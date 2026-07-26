using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Define el contrato para el servicio de gestión del perfil de usuario.
    /// </summary>
    public interface IPerfilService
    {
        /// <summary>
        /// Obtiene los datos del perfil de un usuario desde la vista de la base de datos.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario cuyo perfil se desea consultar.</param>
        /// <returns>DataTable con los datos del perfil del usuario.</returns>
        Task<DataTable> ObtenerPerfilDesdeVista(string nombreUsuario);

        /// <summary>
        /// Actualiza la foto de perfil de un usuario en la base de datos.
        /// </summary>
        /// <param name="nombreUsuario">Nombre del usuario al que se le actualizará la foto.</param>
        /// <param name="imagenBytes">Arreglo de bytes que representa la nueva imagen de perfil.</param>
        Task ActualizarFotoUsuario(string nombreUsuario, byte[] imagenBytes);
    }
}