using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal static class ConversorNumeroALetras
    {
        public static string ATexto(double total, string moneda = "LEMPIRAS")
        {
            if (total < 0) total = 0;
            long entero = (long)Math.Round(total, MidpointRounding.AwayFromZero);
            return $"{EnteroALetras(entero)} {moneda}";
        }

        private static string EnteroALetras(long numero)
        {
            if (numero == 0) return "CERO";
            if (numero < 0) return "MENOS " + EnteroALetras(-numero);

            string[] unidades = {
                "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE",
                "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE",
                "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE"
            };
            string[] decenas = {
                "", "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA",
                "SESENTA", "SETENTA", "OCHENTA", "NOVENTA"
            };
            string[] centenas = {
                "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS",
                "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS"
            };

            string resultado = "";

            if (numero >= 1_000_000)
            {
                long millones = numero / 1_000_000;
                resultado += (millones == 1 ? "UN MILLÓN " : EnteroALetras(millones) + " MILLONES ");
                numero %= 1_000_000;
            }
            if (numero >= 1_000)
            {
                long miles = numero / 1_000;
                resultado += (miles == 1 ? "MIL " : EnteroALetras(miles) + " MIL ");
                numero %= 1_000;
            }
            if (numero >= 100)
            {
                int c = (int)(numero / 100);
                resultado += (numero == 100 ? "CIEN " : centenas[c] + " ");
                numero %= 100;
            }
            if (numero >= 20)
            {
                int d = (int)(numero / 10);
                int u = (int)(numero % 10);
                resultado += decenas[d] + (u > 0 ? " Y " + unidades[u] + " " : " ");
            }
            else if (numero > 0)
            {
                resultado += unidades[numero] + " ";
            }

            return resultado.Trim();
        }

    }
}
