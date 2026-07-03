using System;

namespace SG_BAMS.Facturas.DTO
{
    /// <summary>
    /// Representa una línea de producto dentro de una factura.
    /// </summary>
    public class DetalleDTO
    {
        public int IdProducto { get; set; }

        /// <summary>
        /// Solo se usa para mostrar en pantalla (grid). No se persiste en BD,
        /// ya que el nombre real vive en la tabla Producto.
        /// </summary>
        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }

        public double Precio { get; set; }

        /// <summary>
        /// Calculado, nunca se asigna manualmente ni se guarda en BD.
        /// </summary>
        public double Subtotal => Cantidad * Precio;
    }
}