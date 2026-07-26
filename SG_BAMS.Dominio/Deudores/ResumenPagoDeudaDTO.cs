using System.Collections.Generic;

namespace SG_BAMS
{
    /// <summary>
    /// Representa una línea de producto dentro del resumen de una deuda.
    /// </summary>
    public class LineaProductoDeudaDTO
    {
        public string Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Total { get; set; }
    }

    /// <summary>
    /// Resultado ya calculado del resumen de una deuda: productos, subtotal, descuento
    /// y saldos. El formulario Pago_Deuda solo debe encargarse de formatear estos valores
    /// como texto para mostrarlos (ver auditoría SOLID, hallazgo PGD03).
    /// </summary>
    public class ResumenPagoDeudaDTO
    {
        public List<LineaProductoDeudaDTO> Productos { get; set; } = new List<LineaProductoDeudaDTO>();
        public decimal Subtotal { get; set; }
        public decimal Descuento { get; set; }
        public decimal TotalCompra => Subtotal - Descuento;
        public decimal SaldoPagado { get; set; }
        public decimal SaldoPendiente { get; set; }
    }
}
