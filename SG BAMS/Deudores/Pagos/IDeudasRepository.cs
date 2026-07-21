using System;
using System.Data;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Contrato de acceso a datos para la gestión de pagos y consultas de deudas.
    /// </summary>
    public interface IDeudasRepository
    {
        /// <summary>
        /// Registra un pago sobre una deuda. Devuelve false si la operación falla.
        /// </summary>
        Task<bool> InsertarPago(int idDeuda, decimal montoPago, DateTime fechaPago);

        /// <summary>
        /// Obtiene el detalle de saldo (descuento, pagado, pendiente) de una deuda.
        /// Devuelve null si no existe o si ocurre un error.
        /// </summary>
        DataRow ObtenerSaldoDetalle(int idDeuda);

        /// <summary>
        /// Obtiene los deudores activos disponibles para registrar un pago.
        /// </summary>
        DataTable ObtenerDeudoresActivos();

        /// <summary>
        /// Obtiene las últimas ventas registradas.
        /// </summary>
        DataTable ObtenerUltimasVentas();

        /// <summary>
        /// Obtiene los productos asociados a una deuda.
        /// </summary>
        DataTable ObtenerProductosPorDeuda(int idDeuda);

        /// <summary>
        /// Obtiene las deudas activas de un cliente.
        /// </summary>
        DataTable ObtenerDeudasPorCliente(int idCliente);

        /// <summary>
        /// Indica si el cliente tiene alguna deuda activa.
        /// </summary>
        Task<bool> ClienteTieneDeudaActiva(int idCliente);
    }
}
