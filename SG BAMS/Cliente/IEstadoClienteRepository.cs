using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS.Cliente
{
    /// <summary>
    /// Contrato para obtener el catálogo de estados disponibles para un cliente.
    /// Separado de IClienteRepository porque es un catálogo de referencia,
    /// no una operación propia del CRUD de clientes (mismo patrón ya aplicado
    /// en Proveedor con IEstadoRepository).
    /// Solo el formulario que realmente necesita el combo de estados
    /// (ClienteModificar) depende de esta interfaz — ClienteAgregar,
    /// ClienteExistente, ClientesAdm y ClientesEmp no la necesitan y por
    /// lo tanto no dependen de ella.
    /// </summary>
    public interface IEstadoClienteRepository
    {
        /// <summary>
        /// Obtiene el catálogo de estados posibles para un cliente.
        /// </summary>
        Task<DataTable> ObtenerEstados();
    }
}