using System;

namespace SG_BAMS.ComprasDTO
{
    /// <summary>
    /// Representa una línea de producto dentro de una compra.
    /// Reemplaza a la clase anidada "DetalleCompra" que vivía dentro de ClsCompras.
    /// </summary>
    public class DetalleCompraDTO
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        /// <summary>
        /// Calculado, nunca se asigna manualmente ni se guarda en BD.
        /// </summary>
        public decimal Subtotal => Cantidad * Precio;
    }
}