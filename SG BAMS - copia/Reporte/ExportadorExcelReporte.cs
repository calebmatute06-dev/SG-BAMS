using System;
using System.Data;
using System.IO;
using System.Linq;
using ClosedXML.Excel;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Exporta un reporte a una hoja de cálculo Excel (.xlsx).
    /// Reemplaza a ClsExportarExcel. Cada responsabilidad que antes vivía
    /// dentro de un único método (CE01) ahora es un método propio; la
    /// columna a totalizar, la etiqueta y las columnas monetarias vienen
    /// de ReporteConfig en lugar de comparar texto (CE02/CE03); ya no
    /// depende de DataGridView ni de MessageBox (CE04) y ya no abre el
    /// archivo generado, solo devuelve la ruta (CE05).
    /// </summary>
    public class ExportadorExcelReporte : IExportadorReporte
    {
        private const int FilaTitulo = 1;
        private const int FilaPeriodo = 2;
        private const int FilaInfoGeneracion = 3;
        private const int FilaInicioTabla = 5;

        public string Exportar(DataTable datos, ReporteTipo tipo, DateTime desde, DateTime hasta)
        {
            if (datos == null || datos.Rows.Count == 0) return null;

            var config = ReporteConfig.Obtener(tipo);

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte BAMS");

                EscribirEncabezado(worksheet, datos, tipo, desde, hasta);
                EscribirCabeceraColumnas(worksheet, datos);
                decimal totalGeneral = EscribirFilas(worksheet, datos, config);
                EscribirTotales(worksheet, datos, config, totalGeneral);

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().Height = 22;

                string ruta = ConstruirRutaArchivo(tipo);
                workbook.SaveAs(ruta);
                return ruta;
            }
        }

        private void EscribirEncabezado(IXLWorksheet worksheet, DataTable datos, ReporteTipo tipo, DateTime desde, DateTime hasta)
        {
            var rangoTitulo = worksheet.Range(FilaTitulo, 1, FilaTitulo, datos.Columns.Count).Merge();
            rangoTitulo.Value = "SISTEMA BAMS - REPORTE DE " + tipo.ToString().ToUpper();
            rangoTitulo.Style.Font.Bold = true;
            rangoTitulo.Style.Font.FontSize = 16;
            rangoTitulo.Style.Font.FontColor = XLColor.FromHtml("#2F5597");
            rangoTitulo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

            if (tipo == ReporteTipo.Ventas || tipo == ReporteTipo.Compras)
            {
                var rangoFechas = worksheet.Range(FilaPeriodo, 1, FilaPeriodo, datos.Columns.Count).Merge();
                rangoFechas.Value = $"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}";
                rangoFechas.Style.Font.Italic = true;
                rangoFechas.Style.Font.FontSize = 12;
                rangoFechas.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }

            var rangoInfo = worksheet.Range(FilaInfoGeneracion, 1, FilaInfoGeneracion, datos.Columns.Count).Merge();
            rangoInfo.Value = $"Generado el: {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:hh:mm:ss tt}";
            rangoInfo.Style.Font.FontSize = 10;
            rangoInfo.Style.Font.Italic = true;
            rangoInfo.Style.Font.FontColor = XLColor.Gray;
            rangoInfo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        }

        private void EscribirCabeceraColumnas(IXLWorksheet worksheet, DataTable datos)
        {
            for (int i = 0; i < datos.Columns.Count; i++)
            {
                var celda = worksheet.Cell(FilaInicioTabla, i + 1);
                celda.Value = datos.Columns[i].ColumnName;
                celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2F5597");
                celda.Style.Font.FontColor = XLColor.White;
                celda.Style.Font.Bold = true;
                celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
        }

        private decimal EscribirFilas(IXLWorksheet worksheet, DataTable datos, ConfiguracionReporte config)
        {
            decimal totalGeneral = 0;

            for (int r = 0; r < datos.Rows.Count; r++)
            {
                for (int c = 0; c < datos.Columns.Count; c++)
                {
                    string nombreColumna = datos.Columns[c].ColumnName;
                    object valor = datos.Rows[r][c];
                    var celdaExcel = worksheet.Cell(r + FilaInicioTabla + 1, c + 1);

                    if (valor is DateTime fecha)
                    {
                        celdaExcel.Value = fecha.Date;
                        celdaExcel.Style.DateFormat.Format = "dd/mm/yyyy";
                    }
                    else if (decimal.TryParse(valor?.ToString(), out decimal num))
                    {
                        celdaExcel.Value = num;
                        if (nombreColumna == config.ColumnaTotal) totalGeneral += num;

                        if (config.ColumnasMonetarias.Contains(nombreColumna))
                        {
                            celdaExcel.Style.NumberFormat.Format = "\"L. \"#,##0.00";
                        }

                        if (nombreColumna == config.ColumnaStock)
                        {
                            AplicarColorStock(celdaExcel, (int)num);
                        }
                    }
                    else
                    {
                        celdaExcel.Value = valor?.ToString() ?? "";
                    }

                    celdaExcel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }
            }

            return totalGeneral;
        }

        private void AplicarColorStock(IXLCell celda, int stock)
        {
            var nivel = ServicioColorStock.Evaluar(stock);
            var (fondo, letra) = ServicioColorStock.ColoresHex(nivel);
            celda.Style.Fill.BackgroundColor = XLColor.FromHtml(fondo);
            celda.Style.Font.FontColor = XLColor.FromHtml(letra);
        }

        private void EscribirTotales(IXLWorksheet worksheet, DataTable datos, ConfiguracionReporte config, decimal totalGeneral)
        {
            int indiceColumnaSumar = datos.Columns.IndexOf(config.ColumnaTotal);
            int filaTotales = datos.Rows.Count + FilaInicioTabla + 1;
            int ultimaFilaFinal = filaTotales;

            if (indiceColumnaSumar >= 0)
            {
                var rangoEtiqueta = worksheet.Range(filaTotales, 1, filaTotales, indiceColumnaSumar + 1).Merge();
                rangoEtiqueta.Value = config.EtiquetaTotal;
                rangoEtiqueta.Style.Font.Bold = true;
                rangoEtiqueta.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                var celdaMonto = worksheet.Cell(filaTotales, indiceColumnaSumar + 1);
                celdaMonto.Value = totalGeneral;
                celdaMonto.Style.Font.Bold = true;
                celdaMonto.Style.Fill.BackgroundColor = XLColor.LightGray;
                celdaMonto.Style.NumberFormat.Format = "\"L. \"#,##0.00";
                celdaMonto.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
            }
            else
            {
                ultimaFilaFinal = datos.Rows.Count + FilaInicioTabla;
            }

            var rangoTabla = worksheet.Range(FilaInicioTabla, 1, ultimaFilaFinal, datos.Columns.Count);
            rangoTabla.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
            rangoTabla.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
        }

        private string ConstruirRutaArchivo(ReporteTipo tipo)
        {
            // Se agrega Guid.NewGuid() para evitar colisiones entre hilos al ejecutar pruebas de carga en NBomber
            string nombreArchivo = $"Reporte_{tipo}_{DateTime.Now:yyyyMMdd_HHmmss}_{Guid.NewGuid():N}.xlsx";
            return Path.Combine(Path.GetTempPath(), nombreArchivo);
        }
    }
}