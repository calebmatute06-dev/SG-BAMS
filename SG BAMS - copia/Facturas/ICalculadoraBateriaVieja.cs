using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal interface ICalculadoraBateriaVieja
    {
        ResultadoCalculoBateria CalcularTotales(List<BateriaItem> items);
        bool ExcedeLimite(double totalBateria, double limiteFactura);
    }
}
