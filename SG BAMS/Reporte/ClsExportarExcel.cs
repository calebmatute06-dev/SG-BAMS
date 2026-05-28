using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Linq;

/// <summary>
/// 
/// </summary>
public class ClsExportarExcel
{
    /// <summary>
    /// Exporta el DataGridView a Excel.
    /// </summary>
    /// <param name="dgv">El DataGridView.</param>
    /// <param name="tituloReporte">El título del reporte.</param>
    /// <param name="desde">La fecha desde.</param>
    /// <param name="hasta">La fecha hasta.</param>
    public void ExportarDataGridView(DataGridView dgv, string tituloReporte, DateTime desde, DateTime hasta)
    {
        try
        {
            if (dgv.Rows.Count == 0) return;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte BAMS");
                var rangoTitulo = worksheet.Range(1, 1, 1, dgv.Columns.Count).Merge();
                rangoTitulo.Value = "SISTEMA BAMS - REPORTE DE " + tituloReporte.ToUpper();
                rangoTitulo.Style.Font.Bold = true;
                rangoTitulo.Style.Font.FontSize = 16;
                rangoTitulo.Style.Font.FontColor = XLColor.FromHtml("#2F5597");
                rangoTitulo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                if (tituloReporte.ToUpper().Contains("VENTAS") || tituloReporte.ToUpper().Contains("COMPRAS"))
                {
                    var rangoFechas = worksheet.Range(2, 1, 2, dgv.Columns.Count).Merge();
                    rangoFechas.Value = $"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}";
                    rangoFechas.Style.Font.Italic = true;
                    rangoFechas.Style.Font.FontSize = 12;
                    rangoFechas.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                var rangoInfo = worksheet.Range(3, 1, 3, dgv.Columns.Count).Merge();
                rangoInfo.Value = $"Generado el: {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:hh:mm:ss tt}";
                rangoInfo.Style.Font.FontSize = 10;
                rangoInfo.Style.Font.Italic = true;
                rangoInfo.Style.Font.FontColor = XLColor.Gray;
                rangoInfo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                int filaInicioTabla = 5;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var celda = worksheet.Cell(filaInicioTabla, i + 1);
                    celda.Value = dgv.Columns[i].HeaderText;
                    celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2F5597");
                    celda.Style.Font.FontColor = XLColor.White;
                    celda.Style.Font.Bold = true;
                    celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                decimal totalGeneral = 0;
                int indiceColumnaSumar = -1;
                string tituloUpper = tituloReporte.ToUpper();
                string etiquetaTotal = "TOTAL GENERAL:";

                if (tituloUpper.Contains("VENTAS"))
                {
                    indiceColumnaSumar = 5;
                    etiquetaTotal = "TOTAL VENTAS:";
                }
                else if (tituloUpper.Contains("COMPRAS"))
                {
                    indiceColumnaSumar = 6;
                    etiquetaTotal = "TOTAL EN COMPRAS:";
                }
                else if (tituloUpper.Contains("DEUDORES"))
                {
                    indiceColumnaSumar = 6;
                    etiquetaTotal = "TOTAL SALDO PENDIENTE:";
                }
                else if (tituloUpper.Contains("INVENTARIO"))
                {
                    indiceColumnaSumar = dgv.Columns.Count - 1;
                    etiquetaTotal = "CAPITAL TOTAL EN STOCK:";
                }

                for (int r = 0; r < dgv.Rows.Count; r++)
                {
                    if (dgv.Rows[r].IsNewRow) continue;

                    for (int c = 0; c < dgv.Columns.Count; c++)
                    {
                        var celdaDgv = dgv.Rows[r].Cells[c];
                        var celdaExcel = worksheet.Cell(r + filaInicioTabla + 1, c + 1);
                        string header = dgv.Columns[c].HeaderText.ToUpper();

                        if (celdaDgv.Value is DateTime fecha)
                        {
                            celdaExcel.Value = fecha.Date;
                            celdaExcel.Style.DateFormat.Format = "dd/mm/yyyy";
                        }
                        else if (header.Contains("RTN") || header.Contains("TEL") || header.Contains("ID"))
                        {
                            celdaExcel.SetValue(celdaDgv.Value?.ToString() ?? "");
                            celdaExcel.Style.NumberFormat.Format = "@";
                        }
                        else if (decimal.TryParse(celdaDgv.Value?.ToString(), out decimal num))
                        {
                            celdaExcel.Value = num;
                            if (c == indiceColumnaSumar) totalGeneral += num;
                            if (header.Contains("PRECIO") || header.Contains("TOTAL") || header.Contains("SALDO") || header.Contains("CAPITAL") || header.Contains("MONTO") || header.Contains("ABONADO"))
                                celdaExcel.Style.NumberFormat.Format = "\"L. \"#,##0.00";
                        }
                        else
                        {
                            celdaExcel.Value = celdaDgv.Value?.ToString() ?? "";
                        }
                        celdaExcel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    }
                }

                int filaTotales = dgv.Rows.Count + filaInicioTabla + 1;
                if (indiceColumnaSumar != -1)
                {
                    var rangoEtiqueta = worksheet.Range(filaTotales, 1, filaTotales, indiceColumnaSumar).Merge();
                    rangoEtiqueta.Value = etiquetaTotal;
                    rangoEtiqueta.Style.Font.Bold = true;
                    rangoEtiqueta.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    var celdaMonto = worksheet.Cell(filaTotales, indiceColumnaSumar + 1);
                    celdaMonto.Value = totalGeneral;
                    celdaMonto.Style.Font.Bold = true;
                    celdaMonto.Style.Fill.BackgroundColor = XLColor.LightGray;
                    celdaMonto.Style.NumberFormat.Format = "\"L. \"#,##0.00";
                    celdaMonto.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                var ultimaFilaFinal = (indiceColumnaSumar != -1) ? filaTotales : dgv.Rows.Count + filaInicioTabla;
                var rangoTabla = worksheet.Range(filaInicioTabla, 1, ultimaFilaFinal, dgv.Columns.Count);
                rangoTabla.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rangoTabla.Style.Border.InsideBorder = XLBorderStyleValues.Thin;

                worksheet.Columns().AdjustToContents();
                worksheet.Rows().Height = 22;

                string nombreArchivo = $"Reporte_{tituloReporte}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string ruta = Path.Combine(Path.GetTempPath(), nombreArchivo);

                workbook.SaveAs(ruta);
                Process.Start(new ProcessStartInfo { FileName = ruta, UseShellExecute = true });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al exportar a Excel: " + ex.Message, "BAMS");
        }
    }
}