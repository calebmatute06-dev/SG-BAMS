using System.Data;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Define el contrato para obtener el catálogo de clasificaciones disponibles para proveedores.
    /// </summary>
    public interface IClasificacionRepository
    {
        /// <summary>
        /// Obtiene todas las clasificaciones de proveedor disponibles en el sistema.
        /// </summary>
        /// <returns>DataTable con las clasificaciones.</returns>
        DataTable ObtenerClasificaciones();
    }
}