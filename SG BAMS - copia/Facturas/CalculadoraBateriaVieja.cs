using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class CalculadoraBateriaVieja : ICalculadoraBateriaVieja
    {
        public ResultadoCalculoBateria CalcularTotales(List<BateriaItem> items)
        {
            double totalDinero = 0;
            int totalCantidad = 0;

            foreach (var item in items)
            {
                totalDinero += item.Subtotal;
                totalCantidad += item.Cantidad;
            }

            return new ResultadoCalculoBateria
            {
                TotalDinero = totalDinero,
                TotalCantidad = totalCantidad
            };
        }

        public bool ExcedeLimite(double totalBateria, double limiteFactura)
        {
            return totalBateria >= limiteFactura;
        }
    }
}
