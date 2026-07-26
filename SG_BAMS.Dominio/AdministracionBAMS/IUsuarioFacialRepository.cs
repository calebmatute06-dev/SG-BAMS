using System.Data;

namespace SG_BAMS.Dominio.AdministracionBAMS
{
    /// <summary>
    /// Contrato para el acceso a datos relacionados con el reconocimiento facial de usuarios.
    /// SRP: separa las consultas de usuario del procesamiento de imágenes.
    /// </summary>
    public interface IUsuarioFacialRepository
    {
        /// <summary>
        /// Obtiene la lista de usuarios registrados con sus datos necesarios para el reconocimiento facial.
        /// </summary>
        DataTable ObtenerUsuarios();
    }
}