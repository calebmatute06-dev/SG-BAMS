using System.Collections.Generic;
using System.Data;
using SG_BAMS.ComprasDTO;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Contrato para el acceso a datos de la modificación de compras
    /// (cabecera, detalle, reversión de stock y eliminación completa).
    /// </summary>
    public interface IModificarComprasRepository
    {
        DataTable ListarFormasPago();
        DataTable ListarProveedoresActivos();
        DataTable ObtenerDetalleCompra(int idCompra);
        DataTable ObtenerCabeceraCompra(int idCompra);
        void GuardarCambiosDetalle(int idCompra, int idProd, int cant, decimal precio);
        void ActualizarDetalleCompra(int idCompra, List<DetalleCompraDTO> detalle);
        void ActualizarCabeceraCompra(CompraDTO compra);
        void EliminarProductoDeBD(int idCompra, int idProd);
        void RevertirStockProductoNuevo(int idCompra, int idProd, int cant);
        bool EliminarCompraCompleta(int idCompra);
    }
}
