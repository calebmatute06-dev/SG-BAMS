using System.Collections.Generic;
using System.Data;
using SG_BAMS.ComprasDTO;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Contrato para el acceso a datos de compras (alta, validación de
    /// duplicados y consulta de productos por proveedor). Permite que las
    /// pantallas dependan de esta abstracción en lugar de ClsCompras directamente.
    /// </summary>
    public interface IComprasRepository
    {
        DataTable ObtenerProductosPorProveedor(int idProv);
        bool ValidarProductoEnCompra(string idCompra, int idProducto);
        void AgregarDetalleACompraExistente(string idCompra, int idProducto, int cantidad, decimal precio);
        bool GuardarNuevaCompra(CompraDTO compra);
    }
}