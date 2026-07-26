using System.Data;

namespace SG_BAMS.ProductoInventario
{
    /// <summary>
    /// Contrato de acceso a datos para los catálogos que alimentan los ComboBox
    /// del módulo de Inventario (Marca, Tipo, Modelo, Estado, Proveedor).
    /// </summary>
    public interface IComboRepository
    {
        /// <summary>
        /// Obtiene los datos del catálogo solicitado.
        /// </summary>
        /// <param name="tipoTabla">Catálogo a consultar (debe existir en <see cref="ComboCatalogo.Configuraciones"/>).</param>
        /// <param name="idProveedorActual">
        /// Id del proveedor a excluir de la exclusión de "activos" al modificar (solo aplica a "Proveedor"; 0 si no aplica).
        /// </param>
        DataTable ObtenerDatos(string tipoTabla, int idProveedorActual = 0);
    }
}
