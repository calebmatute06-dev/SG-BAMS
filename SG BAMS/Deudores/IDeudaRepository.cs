using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Contrato de acceso a datos para el listado de deudores y la creación/actualización
    /// automática de deudas a partir de facturas.
    /// </summary>
    public interface IDeudaRepository
    {
        /// <summary>
        /// Obtiene el listado completo de deudores.
        /// </summary>
        DataTable ListarDeudores();

        /// <summary>
        /// Crea o actualiza la deuda asociada a una factura.
        /// </summary>
        Task<bool> CrearDeudaManual(int idFactura, int idCliente, double montoTotal, DateTime fechaVenta);
    }
}
