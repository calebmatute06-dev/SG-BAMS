using System;
using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// 
    /// </summary>
    public class BitacoraDTO
    {
        /// <summary>
        /// Gets or sets the nombre.
        /// </summary>
        /// <value>
        /// The nombre.
        /// </value>
        public string Nombre { get; set; }
        /// <summary>
        /// Gets or sets the accion.
        /// </summary>
        /// <value>
        /// The accion.
        /// </value>
        public string Accion { get; set; }
        /// <summary>
        /// Gets or sets the modulo.
        /// </summary>
        /// <value>
        /// The modulo.
        /// </value>
        public string Modulo { get; set; }
        /// <summary>
        /// Gets or sets the fecha.
        /// </summary>
        /// <value>
        /// The fecha.
        /// </value>
        public DateTime Fecha { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="QuestPDF.Infrastructure.IDocument" />
    internal class ReporteBitacora : IDocument
    {
        /// <summary>
        /// The datos
        /// </summary>
        private readonly List<BitacoraDTO> _datos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReporteBitacora"/> class.
        /// </summary>
        /// <param name="datos">The datos.</param>
        public ReporteBitacora(List<BitacoraDTO> datos)
        {
            _datos = datos ?? new List<BitacoraDTO>();
        }

        /// <summary>
        /// Provides metadata values like author and keywords used in PDF creation.
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Override this method to customize document's metadata.
        /// </remarks>
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        /// <summary>
        /// Configures the document content by specifying its layout structure and visual element.
        /// </summary>
        /// <param name="container">The document container used for defining content via the FluentAPI.</param>
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
                        header.Cell().Element(Encabezado).Text("Fecha").Bold();
                    });

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