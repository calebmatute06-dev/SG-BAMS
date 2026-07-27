using System;
using System.Collections.Generic;

namespace SG_BAMS.ComprasDTO
{
    /// <summary>
    /// Transporta todos los datos necesarios para crear o modificar una compra.
    /// Reemplaza el paso de parámetros sueltos entre Ingresar_datos__Compra_,
    /// Modificar_datos__Compra_, ClsCompras y ClsModificarCompras.
    /// </summary>
    public class CompraDTO
    {
        /// <summary>
        /// Se llena solo al modificar una compra ya existente (0 = compra nueva).
        /// </summary>
        public int IdCompra { get; set; }

        public int IdProveedor { get; set; }

        public int IdFormaPago { get; set; }

        public DateTime Fecha { get; set; }

        public string Nota { get; set; }

        public List<DetalleCompraDTO> Detalle { get; set; } = new List<DetalleCompraDTO>();

        /// <summary>
        /// Calculado a partir del detalle.
        /// </summary>
        public decimal Total
        {
            get
            {
                decimal total = 0;
                foreach (var d in Detalle) total += d.Subtotal;
                return total;
            }
        }
    }
}
