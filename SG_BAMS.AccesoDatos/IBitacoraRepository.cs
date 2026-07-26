
using System.Data;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Contrato de acceso a datos para los registros de la bitácora del sistema.
    /// </summary>
    public interface IBitacoraRepository
    {
        /// <summary>
        /// Obtiene todos los registros de la bitácora.
        /// </summary>
        DataTable ObtenerRegistros();
    }
}

