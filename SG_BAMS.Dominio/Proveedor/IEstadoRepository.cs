using System.Data;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Define el contrato para obtener el catálogo de estados disponibles para proveedores.
    /// Separado de <see cref="IProveedorRepository"/> por ser un catálogo de referencia,
    /// no una operación del CRUD de proveedores.
    /// </summary>
    public interface IEstadoRepository
    {
        /// <summary>
        /// Obtiene todos los estados de proveedor disponibles en el sistema.
        /// </summary>
        /// <returns>DataTable con los estados.</returns>
        DataTable ObtenerEstados();
    }
}