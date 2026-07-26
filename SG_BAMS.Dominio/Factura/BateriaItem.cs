using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Dominio
{
    public class BateriaItem
    {
        public string Tipo { get; set; }
        public double Precio { get; set; }
        public int Cantidad { get; set; }
        public double Subtotal => System.Math.Round(Precio * Cantidad, 2);
    }
}
