using System;

namespace SG_BAMS.ProductoInventario.DTO
{
    /// <summary>
    /// Transporta todos los datos necesarios para crear o modificar un producto del inventario.
    /// Reemplaza el paso de parámetros sueltos entre AgregarProducto, ModificarProducto,
    /// InventarioAdmin, InventarioEmp y ClsProducto.
    /// </summary>
    public class ProductoDTO
    {
        /// <summary>
        /// Id del producto. 0 cuando es un producto nuevo (todavía no insertado).
        /// </summary>
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int IdMarca { get; set; }
        public int IdTipo { get; set; }
        public int IdModelo { get; set; }
        public int IdEstado { get; set; }
        public decimal Precio { get; set; }
        public string CodigoBarra { get; set; }
        public int IdProveedor { get; set; }
        public int Stock { get; set; }

        public string MarcaActual { get; set; }
        public string TipoActual { get; set; }
        public string ModeloActual { get; set; }
        public string EstadoActual { get; set; }
        public string ProveedorActual { get; set; }

        
    }
}