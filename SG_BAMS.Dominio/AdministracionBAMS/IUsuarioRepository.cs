using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Dominio.AdministracionBAMS
{
    /// <summary>
    /// Contrato que define las operaciones de acceso a datos para la entidad Usuario.
    /// Aplicando DIP: los formularios dependen de esta abstracción, no de la implementación concreta.
    /// </summary>
    public interface IUsuarioRepository
    {
        Task<DataTable> LeerUsuariosAsync();
        Task<bool> InsertarUsuarioAsync(string nombre, string password, int idRol, byte[] imagen, string correo);
        Task<DataTable> ListarRolesAsync();
        Task<bool> ModificarUsuarioAsync(int id, string nombre, string password, int idRol, int idEstado, byte[] imagen, string correo);
        Task<DataTable> ListarEstadosAsync();
        Task<bool> ExisteUsuarioAsync(string nombreUsuario, int idExcluir = 0);
        Task<bool> ExisteCorreoAsync(string correo);
        bool CorreoModificar(string correo, int idUsuarioActual = 0);
    }
}