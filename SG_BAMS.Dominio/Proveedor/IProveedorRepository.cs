using System.Data;
using SG_BAMS.Proveedor.DTO;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Define el contrato de acceso a datos para las operaciones de proveedores:
    /// listado, búsqueda, validación de duplicados, alta y modificación.
    /// </summary>
    public interface IProveedorRepository
    {
        /// <summary>
        /// Obtiene todos los proveedores registrados en el sistema.
        /// </summary>
        /// <returns>DataTable con los datos de proveedores.</returns>
        DataTable ObtenerProveedores();

        /// <summary>
        /// Busca proveedores cuyo nombre coincida con el filtro especificado.
        /// </summary>
        /// <param name="filtro">Texto de búsqueda.</param>
        /// <returns>DataTable con los proveedores que coinciden con el filtro.</returns>
        DataTable Buscar(string filtro);

        /// <summary>
        /// Verifica si ya existe un proveedor con el nombre especificado.
        /// </summary>
        /// <param name="nombre">Nombre del proveedor a verificar.</param>
        /// <returns>True si el nombre ya está registrado.</returns>
        bool ExisteNombre(string nombre);

        /// <summary>
        /// Verifica si ya existe un proveedor con el RTN especificado.
        /// </summary>
        /// <param name="rtn">RTN del proveedor a verificar.</param>
        /// <returns>True si el RTN ya está registrado.</returns>
        bool ExisteRtn(string rtn);

        /// <summary>
        /// Verifica si el RTN ya pertenece a otro proveedor distinto del actual.
        /// </summary>
        /// <param name="rtn">RTN a verificar.</param>
        /// <param name="idProveedorActual">Identificador del proveedor actual a excluir de la búsqueda.</param>
        /// <returns>True si el RTN ya está registrado en otro proveedor.</returns>
        bool ExisteRtnExcluyendo(string rtn, int idProveedorActual);

        /// <summary>
        /// Registra un nuevo proveedor en la base de datos.
        /// </summary>
        /// <param name="proveedor">DTO con los datos del proveedor a registrar.</param>
        void Agregar(ProveedorDTO proveedor);

        /// <summary>
        /// Actualiza los datos de un proveedor existente.
        /// </summary>
        /// <param name="proveedor">DTO con los datos actualizados del proveedor. Debe incluir IdProveedor.</param>
        void Modificar(ProveedorDTO proveedor);
    }
}