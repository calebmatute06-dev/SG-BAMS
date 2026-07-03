using System;
using System.Collections.Generic;
 
namespace SG_BAMS.Facturas.DTO
{
    /// <summary>
    /// Transporta todos los datos necesarios para crear, mostrar o imprimir una factura.
    /// Reemplaza el paso de parámetros sueltos entre FacturaAgregarDatos, FacturaVer,
    /// FacturasAdm y ClsFactura.
    /// </summary>
    public class FacturaDTO
    {
        // --- Campos base del diagrama original ---
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; }
        public DateTime Fecha { get; set; }
        public List<DetalleDTO> Detalle { get; set; } = new List<DetalleDTO>();

        // --- Campos adicionales que la pantalla real necesita ---

        /// <summary>
        /// Se llena solo al ver/editar una factura ya existente (0 = factura nueva).
        /// </summary>
        public int IdFactura { get; set; }

        public int IdFormaPago { get; set; }

        public int CantidadBateriaVieja { get; set; }

        public double RebajaBateria { get; set; }

        public double MontoExento { get; set; }

        public bool EsGobierno { get; set; }

        public string RtnCliente { get; set; } = "Sin RTN";

        // --- Solo para mostrar en pantalla (no se guardan como tal en BD) ---
        public string NombreCliente { get; set; }
        public string Vendedor { get; set; }

        // --- Calculados ---
        public double Subtotal
        {
            get
            {
                double acumulador = 0;
                foreach (var d in Detalle) acumulador += d.Subtotal;
                return acumulador;
            }
        }

        public double Total => Math.Max(Subtotal - RebajaBateria, 0);
    }
}