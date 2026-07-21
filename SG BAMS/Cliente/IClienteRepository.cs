using System.Data;
using System.Threading.Tasks;
using SG_BAMS.Cliente.DTO;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Contrato de acceso a datos para el catálogo de clientes: alta, modificación,
    /// listado, búsqueda por nombre, catálogo de estados y verificación de RTN duplicado.
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>
        /// Registra un cliente nuevo. Devuelve el Id generado, o 0 si falla.
        /// </summary>
        Task<int> AgregarClientes(ClienteDTO dto);

        /// <summary>
        /// Actualiza un cliente existente. Devuelve el número de filas afectadas.
        /// </summary>
        Task<int> ModificarClientes(ClienteDTO dto);

        /// <summary>
        /// Obtiene el listado completo de clientes para la grilla de administración.
        /// </summary>
        Task<DataTable> VerClienteTabla();

        /// <summary>
        /// Obtiene el catálogo de clientes (id + nombre completo) usado para el combo
        /// de "cliente existente".
        /// </summary>
        Task<DataTable> ObtenerClientes();

        /// <summary>
        /// Obtiene el catálogo de estados de cliente.
        /// </summary>
        Task<DataTable> ObtenerEstados();

        /// <summary>
        /// Indica si el RTN ya pertenece a otro cliente, excluyendo opcionalmente
        /// el cliente indicado en <paramref name="idClienteActual"/> (usar 0 al
        /// registrar, o el id del cliente actual al modificar).
        /// </summary>
        bool RTNYaExiste(string rtn, int idClienteActual = 0);
    }
}
