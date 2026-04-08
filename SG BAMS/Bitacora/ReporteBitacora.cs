using System;
using System.Collections.Generic;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Bitacora
{
    /// <summary>
    /// Objeto de transferencia de datos que representa un registro de la bitácora.
    /// </summary>
    public class BitacoraDTO
    {
        /// <summary>
        /// Obtiene o establece el nombre del usuario que realizó la acción.
        /// </summary>
        /// <value>
        /// El nombre del usuario.
        /// </value>
        public string Nombre { get; set; }

        /// <summary>
        /// Obtiene o establece la acción realizada por el usuario.
        /// </summary>
        /// <value>
        /// La acción ejecutada.
        /// </value>
        public string Accion { get; set; }

        /// <summary>
        /// Obtiene o establece el módulo del sistema donde se realizó la acción.
        /// </summary>
        /// <value>
        /// El nombre del módulo.
        /// </value>
        public string Modulo { get; set; }

        /// <summary>
        /// Obtiene o establece la fecha y hora en que se registró la acción.
        /// </summary>
        /// <value>
        /// La fecha y hora del registro.
        /// </value>
        public DateTime Fecha { get; set; }
    }

    /// <summary>
    /// Genera el documento PDF del reporte de bitácora utilizando la librería QuestPDF.
    /// </summary>
    /// <seealso cref="QuestPDF.Infrastructure.IDocument" />
    internal class ReporteBitacora : IDocument
    {
        /// <summary>
        /// Lista de registros de bitácora que se mostrarán en el reporte.
        /// </summary>
        private readonly List<BitacoraDTO> _datos;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ReporteBitacora"/>.
        /// </summary>
        /// <param name="datos">La lista de registros de bitácora a incluir en el reporte.
        /// Si es nula, se inicializa con una lista vacía.</param>
        public ReporteBitacora(List<BitacoraDTO> datos)
        {
            _datos = datos ?? new List<BitacoraDTO>();
        }

        /// <summary>
        /// Devuelve los metadatos del documento PDF, como autor y palabras clave.
        /// </summary>
        /// <returns>Los metadatos predeterminados del documento.</returns>
        /// <remarks>
        /// Se puede sobrescribir este método para personalizar los metadatos del documento.
        /// </remarks>
        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        /// <summary>
        /// Define el contenido y la estructura visual del documento PDF mediante la API fluida de QuestPDF.
        /// </summary>
        /// <param name="container">El contenedor del documento utilizado para construir el contenido.</param>
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