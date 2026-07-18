using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS.MenuPrincipal
{
    /// <summary>
    /// Implementación de IChartBuilderService para construir gráficos del dashboard.
    /// Centraliza la configuración visual que estaba duplicada en MenuPrincipalAdm y MenuPrincipalEmp.
    /// </summary>
    public class ChartBuilderService : IChartBuilderService
    {
        /// <inheritdoc/>
        public Task CargarGraficoStock(Chart chart, DataTable tablaStock)
        {
            if (tablaStock == null || tablaStock.Rows.Count == 0)
                return Task.CompletedTask;

            chart.Series.Clear();
            chart.Legends.Clear();

            if (chart.ChartAreas.Count == 0)
                chart.ChartAreas.Add(new ChartArea("Default"));

            var area = chart.ChartAreas[0];
            area.Position.Auto = true;
            area.BackColor = Color.Transparent;
            area.AxisX.Enabled = AxisEnabled.False;
            area.AxisY.Enabled = AxisEnabled.False;

            int sinStock = 0, bajoStock = 0, conStock = 0;

            foreach (DataRow fila in tablaStock.Rows)
            {
                int cantidad = Convert.ToInt32(fila["Stock"]);
                if (cantidad == 0) sinStock++;
                else if (cantidad < 10) bajoStock++;
                else conStock++;
            }

            Legend leyenda = chart.Legends.Add("Leyenda");
            leyenda.BackColor = Color.Transparent;
            leyenda.Docking = Docking.Bottom;
            leyenda.Font = new Font("Segoe UI", 7.5f, FontStyle.Regular);
            leyenda.LegendStyle = LegendStyle.Table;
            leyenda.TableStyle = LegendTableStyle.Wide;
            leyenda.Alignment = StringAlignment.Center;

            var serie = chart.Series.Add("StockSeries");
            serie.ChartType = SeriesChartType.Doughnut;
            serie["DoughnutRadius"] = "65";
            serie.IsValueShownAsLabel = true;
            serie.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
            serie["PieLabelStyle"] = "Outside";
            serie["PieLineColor"] = "Gray";

            int p1 = serie.Points.AddY(sinStock);
            serie.Points[p1].Color = ColorTranslator.FromHtml("#E74C3C");
            serie.Points[p1].LegendText = $"Sin Stock ({sinStock})";
            serie.Points[p1].Label = sinStock > 0 ? sinStock.ToString() : "";

            int p2 = serie.Points.AddY(bajoStock);
            serie.Points[p2].Color = ColorTranslator.FromHtml("#F39C12");
            serie.Points[p2].LegendText = $"Bajo Stock ({bajoStock})";
            serie.Points[p2].Label = bajoStock > 0 ? bajoStock.ToString() : "";

            int p3 = serie.Points.AddY(conStock);
            serie.Points[p3].Color = ColorTranslator.FromHtml("#27AE60");
            serie.Points[p3].LegendText = $"Con Stock ({conStock})";
            serie.Points[p3].Label = conStock > 0 ? conStock.ToString() : "";

            serie.BorderColor = Color.White;
            serie.BorderWidth = 2;

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public Task CargarGraficoMasVendidos(Chart chart, DataTable datosVentas)
        {
            if (datosVentas == null || datosVentas.Rows.Count == 0)
                return Task.CompletedTask;

            chart.Series.Clear();
            chart.Legends.Clear();
            chart.ChartAreas[0].AxisX.CustomLabels.Clear();
            chart.DataSource = null;

            chart.BackColor = ColorTranslator.FromHtml("#8ED1E8");

            var serieBarras = chart.Series.Add("MasVendidos");
            serieBarras.ChartType = SeriesChartType.Column;
            serieBarras["PointWidth"] = "0.6";
            serieBarras["BarLabelStyle"] = "Top";

            string[] paletaHex = { "#1B6FA8", "#2389C9", "#2FA6D8", "#3FC1D0", "#54D6C4" };

            for (int i = 0; i < datosVentas.Rows.Count; i++)
            {
                DataRow fila = datosVentas.Rows[i];
                string nombreProducto = fila["producto"].ToString().Trim();
                int totalVendido = Convert.ToInt32(fila["total_vendido"]);

                int idx = serieBarras.Points.AddXY(i, totalVendido);
                serieBarras.Points[idx].AxisLabel = nombreProducto;
                serieBarras.Points[idx].Label = totalVendido.ToString();
                serieBarras.Points[idx].LabelForeColor = Color.FromArgb(30, 30, 30);
                serieBarras.Points[idx].Font = new Font("Segoe UI", 9f, FontStyle.Bold);

                string colorHex = paletaHex[Math.Min(i, paletaHex.Length - 1)];
                serieBarras.Points[idx].Color = ColorTranslator.FromHtml(colorHex);
            }

            serieBarras["BarDrawingStyle"] = "Cylinder";

            var area = chart.ChartAreas[0];
            area.AxisX.Interval = 1;
            area.AxisX.LabelStyle.Angle = -35;
            area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
            area.AxisX.LabelStyle.ForeColor = Color.FromArgb(40, 40, 40);
            area.AxisX.MajorGrid.Enabled = false;
            area.AxisX.LineColor = Color.FromArgb(255, 255, 255, 120);
            area.AxisY.MajorGrid.LineColor = Color.FromArgb(255, 255, 255, 90);
            area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5f);
            area.AxisY.LabelStyle.ForeColor = Color.FromArgb(40, 40, 40);
            area.AxisY.LineColor = Color.Transparent;
            area.BackColor = Color.Transparent;

            return Task.CompletedTask;
        }
    }
}