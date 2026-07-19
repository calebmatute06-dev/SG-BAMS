using System.Drawing;

namespace SG_BAMS.Reporte
{
    public enum NivelStock
    {
        Agotado,
        Bajo,
        Bueno
    }

    /// <summary>
    /// Único lugar donde se decide, a partir de una cantidad de stock, el
    /// nivel (agotado/bajo/bueno) y el color asociado. Antes esta regla
    /// (stock &lt; 1, stock &lt; 10, resto) estaba reimplementada tres
    /// veces con distintos formatos de color: en ClsExportarExcel (XLColor),
    /// en DocumentoDinamico (hex de QuestPDF) y en
    /// ReportesAdmin.dgvReporte_CellFormatting_1 (System.Drawing.Color).
    /// </summary>
    public static class ServicioColorStock
    {
        public static NivelStock Evaluar(int stock)
        {
            if (stock < 1) return NivelStock.Agotado;
            if (stock < 10) return NivelStock.Bajo;
            return NivelStock.Bueno;
        }

        /// <summary>Colores en hexadecimal, usados por los exportadores (Excel y PDF).</summary>
        public static (string Fondo, string Letra) ColoresHex(NivelStock nivel)
        {
            switch (nivel)
            {
                case NivelStock.Agotado: return ("#FFC0C0", "#8B0000");
                case NivelStock.Bajo: return ("#FFE0C0", "#A52A2A");
                default: return ("#C0FFC0", "#006400");
            }
        }

        /// <summary>Colores como System.Drawing.Color, usados por el formulario (WinForms).</summary>
        public static (Color Fondo, Color Letra) ColoresWinForms(NivelStock nivel)
        {
            switch (nivel)
            {
                case NivelStock.Agotado: return (Color.FromArgb(255, 192, 192), Color.DarkRed);
                case NivelStock.Bajo: return (Color.FromArgb(255, 224, 192), Color.Brown);
                default: return (Color.FromArgb(192, 255, 192), Color.DarkGreen);
            }
        }
    }
}
