using System.Data;
using System.Threading.Tasks;
using SG_BAMS.Cliente.DTO;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Contrato de acceso a datos para las operaciones propias de clientes:
    /// listado, alta, modificación y validación de RTN duplicado.
    /// El catálogo de Estados se maneja aparte en IEstadoClienteRepository
    /// (ver ISP) porque es un catálogo de referencia, no una operación de cliente.
    /// </summary>
    public interface IClienteRepository
    {
        /// <summary>
        /// Registra un cliente nuevo a partir de los datos del DTO.
        /// Devuelve el Id generado, o 0 si no se pudo insertar.
        /// </summary>
        Task<int> AgregarClientes(ClienteDTO dto);

        /// <summary>
        /// Actualiza un cliente existente a partir de los datos del DTO.
        /// Devuelve el número de filas afectadas.
        /// </summary>
        Task<int> ModificarClientes(ClienteDTO dto);

        /// <summary>
        /// Obtiene la tabla completa de clientes (para el listado en pantalla).
        /// </summary>
        Task<DataTable> VerClienteTabla();

        /// <summary>
        /// Obtiene el catálogo de clientes en formato ID/Nombre (para combos).
        /// </summary>
        Task<DataTable> ObtenerClientes();

        /// <summary>
        /// Indica si el RTN dado ya pertenece a otro cliente distinto de <paramref name="idClienteActual"/>.
        /// </summary>
        Task<bool> RTNYaExiste(string rtn, int idClienteActual = 0);
    }
}
