using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS
{
    public class ClsProducto
    {
        public string ID { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Precio { get; set; } = string.Empty;  // Lo ponemos como string para facilitar el Label, o decimal si harás cálculos
        public string Marca { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public string ModeloAuto { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Stock { get; set; } = string.Empty;
    }
}
