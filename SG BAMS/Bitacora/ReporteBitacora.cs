using System;
using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;


namespace SG_BAMS.Bitacora;

/// <summary>
/// Documento PDF del reporte de bitácora, construido con QuestPDF.
/// Única responsabilidad: definir la estructura visual del PDF a partir
/// de una lista de <see cref="BitacoraDTO"/> ya preparada.
/// </summary>
internal sealed class ReporteBitacora : IDocument
{
    private readonly IReadOnlyList<BitacoraDTO> _datos;

    /// <summary>
    /// Crea el documento a partir de los datos a mostrar.
    /// </summary>
    /// <param name="datos">Registros de bitácora a incluir; si es <c>null</c>, se usa una lista vacía.</param>
    public ReporteBitacora(IReadOnlyList<BitacoraDTO>? datos)
    {
        _datos = datos ?? Array.Empty<BitacoraDTO>();
    }

    /// <inheritdoc />
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    /// <inheritdoc />
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
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                    columns.RelativeColumn();
                });

                table.Header(header =>
                {
                    header.Cell().Element(Encabezado).Text("Nombre").Bold();
                    header.Cell().Element(Encabezado).Text("Acción").Bold();
                    header.Cell().Element(Encabezado).Text("Módulo").Bold();
                    header.Cell().Element(Encabezado).Text("Fecha y Hora").Bold();
                });

                foreach (var item in _datos)
                {
                    table.Cell().Element(Celda).Text(item.Nombre);
                    table.Cell().Element(Celda).Text(item.Accion);
                    table.Cell().Element(Celda).Text(item.Modulo);
                    table.Cell().Element(Celda).Text(item.Fecha.ToString("dd/MM/yyyy HH:mm:ss"));
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