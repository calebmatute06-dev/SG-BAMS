using System.Data;
using SG_BAMS.Proveedor.DTO;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Contrato de acceso a datos para las operaciones de proveedores:
    /// listado, búsqueda, validación de duplicados, alta y modificación.
    /// </summary>
    public interface IProveedorRepository
    {
        /// <summary>
        /// Obtiene todos los proveedores registrados.
        /// </summary>
        DataTable ObtenerProveedores();

        /// <summary>
        /// Busca proveedores que coincidan con el filtro de texto dado.
        /// </summary>
        DataTable Buscar(string filtro);

        /// <summary>
        /// Indica si ya existe un proveedor con el nombre dado.
        /// </summary>
        bool ExisteNombre(string nombre);

        /// <summary>
        /// Indica si ya existe un proveedor con el RTN dado.
        /// </summary>
        bool ExisteRtn(string rtn);

        /// <summary>
        /// Indica si el RTN dado pertenece a otro proveedor distinto de <paramref name="idProveedorActual"/>.
        /// </summary>
        bool ExisteRtnExcluyendo(string rtn, int idProveedorActual);

        /// <summary>
        /// Registra un nuevo proveedor a partir de los datos del DTO.
        /// </summary>
        void Agregar(ProveedorDTO proveedor);

        /// <summary>
        /// Actualiza un proveedor existente a partir de los datos del DTO (debe traer IdProveedor).
        /// </summary>
        void Modificar(ProveedorDTO proveedor);
    }
}
