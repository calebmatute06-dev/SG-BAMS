using System.Data;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Contrato para obtener el catálogo de estados disponibles para un proveedor.
    /// Separado de <see cref="IProveedorRepository"/> porque es un catálogo de
    /// referencia, no una operación propia del CRUD de proveedores.
    /// </summary>
    public interface IEstadoRepository
    {
        /// <summary>
        /// Obtiene todos los estados disponibles.
        /// </summary>
        DataTable ObtenerEstados();
    }
}
