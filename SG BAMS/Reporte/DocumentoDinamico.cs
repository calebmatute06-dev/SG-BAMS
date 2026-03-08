using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

public class DocumentoDinamico : IDocument
{
    private DataGridView _dgv;

    public DocumentoDinamico(DataGridView dgv)
    {
        _dgv = dgv;
    }

    public void Compose(IDocumentContainer container)
    {
        container.Page(page =>
        {
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.Size(PageSizes.A4.Landscape());

            page.Header().Text("SISTEMA BAMS - REPORTE").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

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
                        header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(col.HeaderText).SemiBold();
                    }
                });

                foreach (DataGridViewRow row in _dgv.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            string valorTexto = "";
                            if (cell.Value is DateTime fecha)
                            {
                                valorTexto = fecha.ToString("dd/MM/yyyy");
                            }
                            else
                            {
                                valorTexto = cell.Value?.ToString() ?? "";
                            }

                            string nombreColumna = _dgv.Columns[cell.ColumnIndex].Name;
                            var celda = table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5);

                            if (nombreColumna == "Stock_Actual" && int.TryParse(valorTexto, out int stock))
                            {
                                if (stock == 0)
                                    celda.Background("#FFC0C0").Text(valorTexto).FontColor("#8B0000").Bold();
                                else if (stock <= 10)
                                    celda.Background("#FFE0C0").Text(valorTexto).FontColor("#A52A2A");
                                else
                                    celda.Background("#C0FFC0").Text(valorTexto).FontColor("#006400");
                            }
                            else
                            {
                                celda.Text(valorTexto);
                            }
                        }
                    }
                }
            });

            page.Footer().PaddingTop(5).Row(row =>
            {
                row.RelativeItem().Column(col =>
                {
                    col.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy}").FontSize(9).FontColor(Colors.Grey.Medium);
                    col.Item().Text($"Hora: {DateTime.Now:hh:mm:ss tt}").FontSize(9).FontColor(Colors.Grey.Medium);
                });

                row.RelativeItem().AlignRight().Text(x =>
                {
                    x.Span("Pág ").FontSize(9);
                    x.CurrentPageNumber().FontSize(9);
                });
            });
        });
    }
}