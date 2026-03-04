using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System;

namespace SG_BAMS.Reportes
{
    public class ReporteDocumento : IDocument
    {
        private List<ReporteDTO> _lista;
        private DateTime _desde;
        private DateTime _hasta;

        public ReporteDocumento(List<ReporteDTO> lista, DateTime desde, DateTime hasta)
        {
            _lista = lista;
            _desde = desde;
            _hasta = hasta;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(10).FontFamily(Fonts.Verdana));

                // --- ENCABEZADO ---
                page.Header().Row(row =>
                {
                    row.RelativeItem().Column(col =>
                    {
                        col.Item().Text("REPORTE ADMINISTRATIVO - BAMS")
                            .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

                        col.Item().Text($"Periodo consultado: {_desde:dd/MM/yyyy} al {_hasta:dd/MM/yyyy}")
                            .FontSize(12).Italic();

                        col.Item().PaddingTop(5).LineHorizontal(1).LineColor(Colors.Grey.Lighten1);
                    });
                });

                // --- CONTENIDO (TABLA) ---
                page.Content().PaddingVertical(15).Table(table =>
                {
                    // Definición de anchos de columna
                    table.ColumnsDefinition(columns =>
                    {
                        columns.ConstantColumn(40);  // ID
                        columns.RelativeColumn(2);   // Usuario
                        columns.RelativeColumn(2);   // Tipo
                        columns.RelativeColumn(4);   // Descripción
                        columns.RelativeColumn(2);   // Fecha
                    });

                    // Encabezados de la tabla (Corregido el error CS0311)
                    table.Header(header =>
                    {
                        header.Cell().Element(CellStyle).Text("ID").SemiBold();
                        header.Cell().Element(CellStyle).Text("Usuario").SemiBold();
                        header.Cell().Element(CellStyle).Text("Tipo").SemiBold();
                        header.Cell().Element(CellStyle).Text("Descripción").SemiBold();
                        header.Cell().Element(CellStyle).Text("Fecha").SemiBold();

                        // Estilo del contenedor del encabezado
                        static IContainer CellStyle(IContainer container) =>
                            container.Background(Colors.Blue.Lighten5)
                                     .Padding(5)
                                     .BorderBottom(1)
                                     .BorderColor(Colors.Blue.Medium);
                    });

                    // Filas de datos
                    foreach (var item in _lista)
                    {
                        table.Cell().Element(BlockStyle).Text(item.ID);
                        table.Cell().Element(BlockStyle).Text(item.Usuario);
                        table.Cell().Element(BlockStyle).Text(item.Tipo);
                        table.Cell().Element(BlockStyle).Text(item.Descripcion);
                        table.Cell().Element(BlockStyle).Text(item.Fecha.ToString("dd/MM/yyyy"));

                        // Estilo de cada celda de datos
                        static IContainer BlockStyle(IContainer container) =>
                            container.BorderBottom(1)
                                     .BorderColor(Colors.Grey.Lighten3)
                                     .Padding(5);
                    }
                });

                // --- PIE DE PÁGINA ---
                page.Footer().Row(row =>
                {
                    row.RelativeItem().Text(x =>
                    {
                        x.Span("Generado el: ").FontSize(9);
                        x.Span(DateTime.Now.ToString("dd/MM/yyyy HH:mm")).FontSize(9);
                    });

                    row.RelativeItem().AlignRight().Text(x =>
                    {
                        x.Span("Página ").FontSize(9);
                        x.CurrentPageNumber().FontSize(9);
                    });
                });
            });
        }
    }
}