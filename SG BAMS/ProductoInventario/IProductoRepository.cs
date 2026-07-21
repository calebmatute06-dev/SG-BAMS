using System.Data;
using SG_BAMS.ProductoInventario.DTO;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Contrato de acceso a datos para el catálogo de productos del inventario:
    /// listado, búsqueda, verificación de duplicados, alta y modificación.
    /// </summary>
    public interface IProductoRepository
    {
        /// <summary>
        /// Obtiene el listado completo y detallado de productos.
        /// </summary>
        DataTable MostrarProductosCompleto();

        /// <summary>
        /// Busca productos que coincidan con el filtro de texto dado.
        /// </summary>
        DataTable BuscarProductos(string filtro);

        /// <summary>
        /// Indica si ya existe un producto con el mismo nombre, marca y proveedor,
        /// excluyendo opcionalmente el producto indicado en <paramref name="idExcluir"/>
        /// (usar 0 al insertar, o el id del producto actual al modificar).
        /// Unifica lo que antes eran los métodos ExisteProductoMarcaProveedor y ExisteProductoEnOtros.
        /// </summary>
        bool ExisteProductoDuplicado(string nombre, int idMarca, int idProveedor, int idExcluir = 0);

        /// <summary>
        /// Indica si el código de barra ya pertenece a otro producto, excluyendo opcionalmente
        /// el producto indicado en <paramref name="idExcluir"/> (usar 0 al insertar, o el id
        /// del producto actual al modificar).
        /// Unifica lo que antes eran los métodos ExisteCodigoBarra y ExisteCodigoEnOtros.
        /// </summary>
        bool ExisteCodigoBarraDuplicado(string codigo, int idExcluir = 0);

        /// <summary>
        /// Registra un nuevo producto a partir de los datos del DTO.
        /// </summary>
        void EjecutarInsercion(ProductoDTO dto);

        /// <summary>
        /// Actualiza un producto existente a partir de los datos del DTO (debe traer IdProducto).
        /// </summary>
        void EjecutarActualizacion(ProductoDTO dto);
    }
}
