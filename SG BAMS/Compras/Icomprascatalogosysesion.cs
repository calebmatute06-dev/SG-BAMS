using System.Data;

namespace SG_BAMS.ComprasContratos
{
    /// <summary>
    /// Contrato para los catálogos usados por el módulo de Compras
    /// (formas de pago, proveedores activos, siguiente ID sugerido).
    /// Un único punto de verdad: tanto ClsModificarCompras como
    /// Ingresar_datos__Compra_ deben consumir esta interfaz en lugar de
    /// reimplementar las mismas consultas.
    /// </summary>
    public interface ICargaCombosRepository
    {
        DataTable ListarFormasPago();
        DataTable ListarProveedoresActivos();
        string SugerirSiguienteID();
    }

    /// <summary>
    /// Contrato para el listado general de compras (pantalla Compras).
    /// </summary>
    public interface IMostrarComprasRepository
    {
        DataTable ListarCompras();
    }

    /// <summary>
    /// Contrato para la consulta de productos de una compra puntual.
    /// </summary>
    public interface IDetalleCompraRepository
    {
        DataTable ListarProductosDeCompra(int idCompra);
    }

    /// <summary>
    /// Abstrae la obtención del usuario en sesión, para que las clases de
    /// acceso a datos no dependan directamente de ClsPasarUsuario.
    /// </summary>
    public interface IUsuarioSesion
    {
        int IdUsuario();
    }
}
