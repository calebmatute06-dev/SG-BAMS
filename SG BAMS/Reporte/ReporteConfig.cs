using System.Collections.Generic;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Describe cómo debe presentarse un tipo de reporte: qué columna se
    /// totaliza, con qué etiqueta, cuáles columnas son monetarias, cuál
    /// columna representa stock (si aplica) y cómo se renombran los
    /// encabezados técnicos de la base de datos hacia texto de usuario.
    /// </summary>
    public class ConfiguracionReporte
    {
        public string ColumnaTotal { get; set; }
        public string EtiquetaTotal { get; set; }
        public string[] ColumnasMonetarias { get; set; }
        public string ColumnaStock { get; set; }
        public Dictionary<string, string> RenombresColumnas { get; set; }
        public string ColumnaOrden { get; set; }
        public bool OrdenDescendente { get; set; }
    }

    /// <summary>
    /// Punto único de verdad para la configuración de presentación de cada
    /// tipo de reporte. Antes esta información estaba duplicada e
    /// inconsistente entre ClsExportarExcel (CE02/CE03), DocumentoDinamico
    /// (DD02/DD03) y ReportesAdmin (RA03/RA04). Agregar un reporte nuevo
    /// ahora solo requiere una entrada aquí (OCP), en vez de tocar cada
    /// clase que presenta o exporta reportes.
    /// </summary>
    public static class ReporteConfig
    {
        private static readonly Dictionary<ReporteTipo, ConfiguracionReporte> _config =
            new Dictionary<ReporteTipo, ConfiguracionReporte>
            {
                [ReporteTipo.Ventas] = new ConfiguracionReporte
                {
                    ColumnaTotal = "Total_Venta",
                    EtiquetaTotal = "TOTAL VENTAS:",
                    ColumnasMonetarias = new[] { "Total_Venta" },
                    ColumnaStock = null,
                    RenombresColumnas = new Dictionary<string, string>
                    {
                        ["Telefono"] = "Teléfono",
                        ["Metodo_Pago"] = "Método de Pago",
                        ["Total_Venta"] = "Total",
                        ["Recibio_Chatarra"] = "Batería Vieja",
                    },
                },
                [ReporteTipo.Compras] = new ConfiguracionReporte
                {
                    ColumnaTotal = "Inversion_Total",
                    EtiquetaTotal = "TOTAL EN COMPRAS:",
                    ColumnasMonetarias = new[] { "Inversion_Total", "Precio_Unitario" },
                    ColumnaStock = null,
                    RenombresColumnas = new Dictionary<string, string>
                    {
                        ["Inversion_Total"] = "Total",
                        ["RTN_Proveedor"] = "RTN",
                        ["Telefono_Proveedor"] = "Teléfono",
                    },
                },
                [ReporteTipo.Deudores] = new ConfiguracionReporte
                {
                    ColumnaTotal = "Saldo_Pendiente",
                    EtiquetaTotal = "TOTAL SALDO PENDIENTE:",
                    ColumnasMonetarias = new[] { "Monto_Credito", "Saldo_Pendiente", "Abonado" },
                    ColumnaStock = null,
                    RenombresColumnas = new Dictionary<string, string>
                    {
                        ["Fecha_Inicio"] = "Fecha de Inicio",
                        ["Monto_Credito"] = "Monto Deuda",
                        ["Saldo_Pendiente"] = "Saldo a Cobrar",
                    },
                    ColumnaOrden = "Saldo_Pendiente",
                    OrdenDescendente = true,
                },
                [ReporteTipo.Inventario] = new ConfiguracionReporte
                {
                    ColumnaTotal = "Total_Venta_Esperada",
                    EtiquetaTotal = "CAPITAL TOTAL EN STOCK:",
                    ColumnasMonetarias = new[] { "Precio_Unitario", "Total_Venta_Esperada" },
                    ColumnaStock = "Stock_Actual",
                    RenombresColumnas = new Dictionary<string, string>
                    {
                        ["Stock_Actual"] = "Stock Actual",
                        ["Precio_Unitario"] = "Precio Venta",
                        ["Total_Venta_Esperada"] = "Capital",
                    },
                    ColumnaOrden = "Stock_Actual",
                    OrdenDescendente = true,
                },
            };

        public static ConfiguracionReporte Obtener(ReporteTipo tipo) => _config[tipo];

        /// <summary>
        /// Traduce el texto que viene del combo del formulario (cmbReporte)
        /// al enum ReporteTipo. Único lugar donde se interpreta ese texto.
        /// </summary>
        public static bool TryDesdeTexto(string texto, out ReporteTipo tipo)
        {
            switch (texto)
            {
                case "Ventas": tipo = ReporteTipo.Ventas; return true;
                case "Compras": tipo = ReporteTipo.Compras; return true;
                case "Deudores": tipo = ReporteTipo.Deudores; return true;
                case "Inventario": tipo = ReporteTipo.Inventario; return true;
                default: tipo = default; return false;
            }
        }
    }
}
