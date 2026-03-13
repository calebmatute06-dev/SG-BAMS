using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class DocumentoDinamico : IDocument
{
    private DataGridView _dgv;
    private string _tituloCabecera;
    private DateTime _desde;
    private DateTime _hasta;

    // Constructor que recibe el DGV, el título y el rango de fechas
    public DocumentoDinamico(DataGridView dgv, string tituloCabecera, DateTime desde, DateTime hasta)
    {
        _dgv = dgv;
        _tituloCabecera = tituloCabecera;
        _desde = desde;
        _hasta = hasta;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.Size(PageSizes.A4.Landscape());

            // Cabecera optimizada con Rango de Fechas
            page.Header().Column(col =>
            {
                col.Item().Text($"SISTEMA BAMS - {_tituloCabecera.ToUpper()}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                // Mostramos el rango solo para Ventas y Compras
                if (_tituloCabecera.ToUpper().Contains("VENTAS") || _tituloCabecera.ToUpper().Contains("COMPRAS"))
                {
                    col.Item().Text($"Rango del reporte: {_desde:dd/MM/yyyy} al {_hasta:dd/MM/yyyy}")
                        .FontSize(12)
                        .Italic()
                        .FontColor(Colors.Grey.Darken2);
                }
            });

            page.Content().PaddingVertical(10).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    for (int i = 0; i < _dgv.Columns.Count; i++) columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    foreach (DataGridViewColumn col in _dgv.Columns)
                    {
                        header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(col.HeaderText).SemiBold().FontSize(10);
                    }
                });

                foreach (DataGridViewRow row in _dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string valorTexto = cell.Value is DateTime fecha
                                ? fecha.ToString("dd/MM/yyyy")
                                : cell.Value?.ToString() ?? "";

                            string nombreColumna = _dgv.Columns[cell.ColumnIndex].Name;
                            var celda = table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5);

                            // Lógica de Semáforo de Stock
                            if (nombreColumna == "Stock_Actual" && int.TryParse(valorTexto, out int stock))
                            {
                                if (stock == 0) celda.Background("#FFC0C0").Text(valorTexto).FontColor("#8B0000").Bold();
                                else if (stock <= 10) celda.Background("#FFE0C0").Text(valorTexto).FontColor("#A52A2A");
                                else celda.Background("#C0FFC0").Text(valorTexto).FontColor("#006400");
                            }
                            else
                            {
                                celda.Text(valorTexto).FontSize(9);
                            }
                        }
                    }
                }
            });

            page.Footer().PaddingTop(5).Row(row =>
            {
                row.RelativeItem().Column(c =>
                {
                    c.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy} - {DateTime.Now:hh:mm:ss tt}").FontSize(8).FontColor(Colors.Grey.Medium);
                });
                row.RelativeItem().AlignRight().Text(x => { x.Span("Pág ").FontSize(8); x.CurrentPageNumber().FontSize(8); });
            });
        });
    }
}