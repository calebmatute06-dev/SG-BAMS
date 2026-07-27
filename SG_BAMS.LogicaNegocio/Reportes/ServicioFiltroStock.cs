namespace SG_BAMS.Reporte
{
    public enum CategoriaStock
    {
        Todos,
        SinStock,
        BajoStock,
        BuenStock
    }

    /// <summary>
    /// Único punto que construye expresiones de RowFilter para el stock.
    /// Antes el formulario tenía dos mecanismos independientes que
    /// competían por el mismo DataView.RowFilter (Min/Max numérico y
    /// cmbCant por categoría), resuelto con un comentario ad hoc sobre
    /// cuál "tenía prioridad" (RA08). Con este servicio solo hay una
    /// fuente de verdad, y el formulario decide con cuál de las dos formas
    /// de filtrar (rango o categoría) se construye el filtro activo.
    /// </summary>
    public class ServicioFiltroStock
    {
        public string PorRango(int minimo, int maximo)
        {
            return $"Stock_Actual >= {minimo} AND Stock_Actual <= {maximo}";
        }

        public string PorCategoria(CategoriaStock categoria)
        {
            switch (categoria)
            {
                case CategoriaStock.SinStock: return "Stock_Actual < 1";
                case CategoriaStock.BajoStock: return "Stock_Actual >= 1 AND Stock_Actual < 10";
                case CategoriaStock.BuenStock: return "Stock_Actual >= 10";
                default: return string.Empty;
            }
        }
    }
}
