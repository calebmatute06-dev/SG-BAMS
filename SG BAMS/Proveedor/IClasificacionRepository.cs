using System.Data;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Contrato para obtener el catálogo de clasificaciones disponibles para un proveedor.
    /// </summary>
    public interface IClasificacionRepository
    {
        /// <summary>
        /// Obtiene todas las clasificaciones disponibles.
        /// </summary>
        DataTable ObtenerClasificaciones();
    }
}
