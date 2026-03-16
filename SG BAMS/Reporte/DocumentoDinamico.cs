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
        // --- Lógica de cálculo por índice de columna ---
        decimal totalGeneral = 0;
        int indiceColumna = -1;

        if (_tituloCabecera.ToUpper().Contains("VENTAS"))
            indiceColumna = 5;
        else if (_tituloCabecera.ToUpper().Contains("COMPRAS"))
            indiceColumna = 6;
        else if (_tituloCabecera.ToUpper().Contains("DEUDORES"))
            indiceColumna = 6;

        if (indiceColumna != -1 && _dgv.Columns.Count > indiceColumna)
        {
            foreach (DataGridViewRow row in _dgv.Rows)
            {
                if (!row.IsNewRow && row.Cells[indiceColumna].Value != null)
                {
                    if (decimal.TryParse(row.Cells[indiceColumna].Value.ToString(), out decimal valor))
                        totalGeneral += valor;
                }
            }
        }

        container.Page(page =>
        {
            page.Margin(1, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.Size(PageSizes.A4.Landscape());

            page.Header().Column(col =>
            {
                col.Item().Text($"SISTEMA BAMS - {_tituloCabecera.ToUpper()}").FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                if (_tituloCabecera.ToUpper().Contains("VENTAS") || _tituloCabecera.ToUpper().Contains("COMPRAS"))
                {
                    col.Item().Text($"Rango del reporte: {_desde:dd/MM/yyyy} al {_hasta:dd/MM/yyyy}")
                        .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
                }
            });

            page.Content().PaddingVertical(10).Column(col =>
            {
                // La tabla se genera normalmente
                col.Item().Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        for (int i = 0; i < _dgv.Columns.Count; i++) columns.RelativeColumn();
                    });

                    table.Header(header =>
                    {
                        foreach (DataGridViewColumn c in _dgv.Columns)
                        {
                            header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(c.HeaderText).SemiBold().FontSize(10);
                        }
                    });

                    foreach (DataGridViewRow row in _dgv.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            foreach (DataGridViewCell cell in row.Cells)
                            {
                                string valorTexto = cell.Value is DateTime fecha ? fecha.ToString("dd/MM/yyyy") : cell.Value?.ToString() ?? "";
                                string nombreColumna = _dgv.Columns[cell.ColumnIndex].Name;
                                var celda = table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5);

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

                // EL TOTAL SE AGREGA AQUÍ: Fuera de la tabla para que solo salga al final (Última Página)
                if (indiceColumna != -1)
                {
                    col.Item().PaddingTop(10).AlignRight().Table(tTotal =>
                    {
                        tTotal.ColumnsDefinition(c =>
                        {
                            c.RelativeColumn(); // Espacio para el texto
                            c.ConstantColumn(100); // Espacio para el monto
                        });

                        tTotal.Cell().Padding(5).AlignRight().Text("TOTAL GENERAL:").SemiBold().FontSize(12);
                        tTotal.Cell().Background(Colors.Grey.Lighten4).Border(1).BorderColor(Colors.Grey.Lighten2)
                            .Padding(5).AlignCenter().Text($"{totalGeneral:N2}").SemiBold().FontSize(12).FontColor(Colors.Blue.Medium);
                    });
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