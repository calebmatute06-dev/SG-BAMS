using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Linq;

public class ClsExportarExcel
{
    public void ExportarDataGridView(DataGridView dgv, string tituloReporte, DateTime desde, DateTime hasta)
    {
        try
        {
            if (dgv.Rows.Count == 0) return;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte BAMS");

                // --- 1. CABECERA DINÁMICA CENTRADA ---

                // Título Principal
                var rangoTitulo = worksheet.Range(1, 1, 1, dgv.Columns.Count).Merge();
                rangoTitulo.Value = "SISTEMA BAMS - REPORTE DE " + tituloReporte.ToUpper();
                rangoTitulo.Style.Font.Bold = true;
                rangoTitulo.Style.Font.FontSize = 16;
                rangoTitulo.Style.Font.FontColor = XLColor.FromHtml("#2F5597");
                rangoTitulo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // Línea de Periodo (Fila 2) - Se mantiene centrada
                if (tituloReporte.Contains("Ventas") || tituloReporte.Contains("Compras"))
                {
                    var rangoFechas = worksheet.Range(2, 1, 2, dgv.Columns.Count).Merge();
                    rangoFechas.Value = $"Periodo: {desde:dd/MM/yyyy} al {hasta:dd/MM/yyyy}";
                    rangoFechas.Style.Font.Italic = true;
                    rangoFechas.Style.Font.FontSize = 12;
                    rangoFechas.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Fecha y Hora de Generación (Fila 3) - ¡AHORA CENTRADA TAMBIÉN!
                var rangoInfo = worksheet.Range(3, 1, 3, dgv.Columns.Count).Merge();
                rangoInfo.Value = $"Generado el: {DateTime.Now:dd/MM/yyyy} a las {DateTime.Now:hh:mm:ss tt}";
                rangoInfo.Style.Font.FontSize = 10;
                rangoInfo.Style.Font.Italic = true;
                rangoInfo.Style.Font.FontColor = XLColor.Gray;
                rangoInfo.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                // --- 2. ENCABEZADOS DE LA TABLA (Ahora en la fila 5 para dar espacio) ---
                int filaInicioTabla = 5;
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var celda = worksheet.Cell(filaInicioTabla, i + 1);
                    celda.Value = dgv.Columns[i].HeaderText;

                    celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2F5597");
                    celda.Style.Font.FontColor = XLColor.White;
                    celda.Style.Font.Bold = true;
                    celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    celda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }

                // --- 3. DATOS ---
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
                        }
                        else
                        {
                            celdaExcel.Value = celdaDgv.Value?.ToString() ?? "";
                        }

                        celdaExcel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                        if (dgv.Columns[c].Name == "Stock_Actual" && int.TryParse(celdaDgv.Value?.ToString(), out int stock))
                        {
                            if (stock == 0) celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFC0C0");
                            else if (stock <= 10) celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFE0C0");
                            else celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#C0FFC0");
                        }
                    }
                }

                // --- 4. BORDES Y FINALIZACIÓN ---
                var ultimaFila = dgv.Rows.Count + filaInicioTabla;
                var rangoTabla = worksheet.Range(filaInicioTabla, 1, ultimaFila, dgv.Columns.Count);
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