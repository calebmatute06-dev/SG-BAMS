using System;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using ClosedXML.Excel;
using System.Linq;

public class ClsExportarExcel
{
    public void ExportarDataGridView(DataGridView dgv)
    {
        try
        {
            if (dgv.Rows.Count == 0) return;

            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Reporte BAMS");

                // 1. ENCABEZADOS
                for (int i = 0; i < dgv.Columns.Count; i++)
                {
                    var celda = worksheet.Cell(1, i + 1);
                    celda.Value = dgv.Columns[i].HeaderText;

                    // Diseño del encabezado
                    celda.Style.Fill.BackgroundColor = XLColor.FromHtml("#2F5597");
                    celda.Style.Font.FontColor = XLColor.White;
                    celda.Style.Font.Bold = true;
                    celda.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    celda.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                }

                // 2. DATOS
                for (int r = 0; r < dgv.Rows.Count; r++)
                {
                    if (dgv.Rows[r].IsNewRow) continue;

                    for (int c = 0; c < dgv.Columns.Count; c++)
                    {
                        var celdaDgv = dgv.Rows[r].Cells[c];
                        var celdaExcel = worksheet.Cell(r + 2, c + 1);
                        string header = dgv.Columns[c].HeaderText.ToUpper();

                        // --- TRATAMIENTO DE DATOS ---
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

                        // Centrado de datos
                        celdaExcel.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        celdaExcel.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;

                        // --- LÓGICA DE COLORES DE STOCK ---
                        if (dgv.Columns[c].Name == "Stock_Actual" && int.TryParse(celdaDgv.Value?.ToString(), out int stock))
                        {
                            if (stock == 0) celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFC0C0");
                            else if (stock <= 10) celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#FFE0C0");
                            else celdaExcel.Style.Fill.BackgroundColor = XLColor.FromHtml("#C0FFC0");
                        }
                    }
                }

                // 3. BORDES Y ESTILOS FINALES
                // Seleccionamos todo el rango que tiene datos
                var rangoTabla = worksheet.Range(1, 1, dgv.Rows.Count + 1, dgv.Columns.Count);

                // Aplicamos bordes exteriores e interiores (rejilla)
                rangoTabla.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                rangoTabla.Style.Border.InsideBorder = XLBorderStyleValues.Thin;
                rangoTabla.Style.Border.OutsideBorderColor = XLColor.Black;
                rangoTabla.Style.Border.InsideBorderColor = XLColor.Black;

                // Ajustes de visualización
                worksheet.Columns().AdjustToContents(); // Ancho automático
                worksheet.Rows().Height = 22; // Un alto de fila más estético

                // 4. GUARDAR Y ABRIR
                string nombreArchivo = $"Reporte_BAMS_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
                string ruta = Path.Combine(Path.GetTempPath(), nombreArchivo);

                workbook.SaveAs(ruta);
                Process.Start(new ProcessStartInfo { FileName = ruta, UseShellExecute = true });
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error al exportar: " + ex.Message, "BAMS");
        }
    }
}