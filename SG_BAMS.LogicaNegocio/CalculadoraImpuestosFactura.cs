using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SG_BAMS.Dominio;

namespace SG_BAMS.LogicaNegocio
{
    public class CalculadoraImpuestosFactura
    {
        public ResultadoImpuestosFactura Calcular(double subtotal, double descuento, bool esGobierno, double montoExento)
        {
            double baseConDescuento = Math.Max(subtotal - descuento, 0);

            if (esGobierno)
            {
                double exonerado = Math.Round(baseConDescuento / 1.15, 2);
                return new ResultadoImpuestosFactura
                {
                    ImporteExonerado = exonerado,
                    ImporteExento = 0,
                    ImporteGravado = 0,
                    Isv15 = 0,
                    TotalFinal = exonerado
                };
            }

            double exentoConISV = Math.Min(montoExento, baseConDescuento);
            double gravadoConISV = baseConDescuento - exentoConISV;

            double impExento = Math.Round(exentoConISV / 1.15, 2);
            double impGravado = Math.Round(gravadoConISV / 1.15, 2);
            double isv15 = Math.Round(gravadoConISV - impGravado, 2);

            return new ResultadoImpuestosFactura
            {
                ImporteExonerado = 0,
                ImporteExento = impExento,
                ImporteGravado = impGravado,
                Isv15 = isv15,
                TotalFinal = Math.Round(impExento + gravadoConISV, 2)
            };
        }
    }
}
