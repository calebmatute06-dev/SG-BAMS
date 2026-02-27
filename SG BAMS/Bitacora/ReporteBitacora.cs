using System;
using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Bitacora
{
    // DTO interno
    public class BitacoraDTO
    {
        public string Nombre { get; set; }
        public string Accion { get; set; }
        public string Modulo { get; set; }
        public DateTime Fecha { get; set; }
    }

    internal class ReporteBitacora : IDocument
    {
        private readonly List<BitacoraDTO> _datos;

        public ReporteBitacora(List<BitacoraDTO> datos)
        {
            _datos = datos ?? new List<BitacoraDTO>();
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(30);

                page.Header()
                    .Text("Reporte de Bitácora")
                    .FontSize(18)
                    .Bold()
                    .AlignCenter();

                page.Content().PaddingTop(15).Table(table =>
                {
                    table.ColumnsDefinition(columns =>
                    {
                        columns.RelativeColumn(); // Nombre
                        columns.RelativeColumn(); // Acción
                        columns.RelativeColumn(); // Módulo
                        columns.RelativeColumn(); // Fecha
                    });

                    // Encabezado
                    table.Header(header =>
                    {
                        header.Cell().Element(Encabezado).Text("Nombre").Bold();
                        header.Cell().Element(Encabezado).Text("Acción").Bold();
                        header.Cell().Element(Encabezado).Text("Módulo").Bold();
                        header.Cell().Element(Encabezado).Text("Fecha").Bold();
                    });

                    // Filas
                    foreach (var item in _datos)
                    {
                        table.Cell().Element(Celda).Text(item.Nombre ?? "");
                        table.Cell().Element(Celda).Text(item.Accion ?? "");
                        table.Cell().Element(Celda).Text(item.Modulo ?? "");
                        table.Cell().Element(Celda).Text(item.Fecha.ToString("dd/MM/yyyy"));
                    }

                    static IContainer Encabezado(IContainer c) =>
                        c.Border(1).Padding(5).Background("#EAEAEA");

                    static IContainer Celda(IContainer c) =>
                        c.Border(1).Padding(5);
                });

                page.Footer()
                    .AlignCenter()
                    .Text($"Generado el {DateTime.Now:dd/MM/yyyy HH:mm}");
            });
        }
    }
}