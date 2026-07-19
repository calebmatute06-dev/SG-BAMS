using System;
using System.Data;
using System.Linq;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// Define la estructura visual del PDF de un reporte. Reemplaza a
    /// DocumentoDinamico. Ya no recibe un DataGridView sino un DataTable
    /// (resuelve DD04); Compose() delega en métodos propios en lugar de
    /// hacerlo todo junto (resuelve DD01); la columna a totalizar, la
    /// etiqueta y las columnas monetarias vienen de ReporteConfig en vez
    /// de comparar texto (resuelve DD02/DD03).
    /// </summary>
    public class DocumentoReportePdf : IDocument
    {
        private readonly DataTable _datos;
        private readonly ReporteTipo _tipo;
        private readonly ConfiguracionReporte _config;
        private readonly DateTime _desde;
        private readonly DateTime _hasta;
        private readonly decimal _totalGeneral;

        public DocumentoReportePdf(DataTable datos, ReporteTipo tipo, DateTime desde, DateTime hasta)
        {
            _datos = datos;
            _tipo = tipo;
            _config = ReporteConfig.Obtener(tipo);
            _desde = desde;
            _hasta = hasta;
            _totalGeneral = CalcularTotal();
        }

        private decimal CalcularTotal()
        {
            decimal total = 0;
            if (string.IsNullOrEmpty(_config.ColumnaTotal) || !_datos.Columns.Contains(_config.ColumnaTotal))
                return total;

            foreach (DataRow fila in _datos.Rows)
            {
                if (decimal.TryParse(fila[_config.ColumnaTotal]?.ToString(), out decimal valor))
                    total += valor;
            }
            return total;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(1, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.Size(PageSizes.A4.Landscape());
                page.Header().Column(col => ComponerEncabezado(col));
                page.Content().PaddingVertical(10).Column(col => ComponerContenido(col));
                page.Footer().PaddingTop(5).Row(row => ComponerPie(row));
            });
        }

        private void ComponerEncabezado(ColumnDescriptor col)
        {
            col.Item().Text($"SISTEMA BAMS - REPORTE DE {_tipo.ToString().ToUpper()}")
                .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);

            if (_tipo == ReporteTipo.Ventas || _tipo == ReporteTipo.Compras)
            {
                col.Item().Text($"Rango del reporte: {_desde:dd/MM/yyyy} al {_hasta:dd/MM/yyyy}")
                    .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
            }
            else if (_tipo == ReporteTipo.Inventario)
            {
                col.Item().Text($"Estado actual del stock al: {DateTime.Now:dd/MM/yyyy}")
                    .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
            }
        }

        private void ComponerContenido(ColumnDescriptor col)
        {
            col.Item().Table(table => ComponerTabla(table));

            if (!string.IsNullOrEmpty(_config.ColumnaTotal) && _datos.Columns.Contains(_config.ColumnaTotal))
            {
                col.Item().PaddingTop(10).AlignRight().Table(tTotal => ComponerTotales(tTotal));
            }
        }

        private void ComponerTabla(TableDescriptor table)
        {
            table.ColumnsDefinition(columns =>
            {
                for (int i = 0; i < _datos.Columns.Count; i++) columns.RelativeColumn();
            });

            table.Header(header =>
            {
                foreach (DataColumn c in _datos.Columns)
                {
                    header.Cell().Background(Colors.Grey.Lighten3).Padding(5).Text(c.ColumnName).SemiBold().FontSize(10);
                }
            });

            foreach (DataRow fila in _datos.Rows)
            {
                foreach (DataColumn columna in _datos.Columns)
                {
                    object valor = fila[columna];
                    string valorTexto = valor is DateTime fecha ? fecha.ToString("dd/MM/yyyy") : valor?.ToString() ?? "";
                    var celda = table.Cell().BorderBottom(1).BorderColor(Colors.Grey.Lighten4).Padding(5);

                    if (columna.ColumnName == _config.ColumnaStock && int.TryParse(valorTexto, out int stock))
                    {
                        EscribirCeldaStock(celda, valorTexto, stock);
                    }
                    else
                    {
                        EscribirCeldaValor(celda, columna.ColumnName, valorTexto);
                    }
                }
            }
        }

        private void EscribirCeldaStock(QuestPDF.Infrastructure.IContainer celda, string valorTexto, int stock)
        {
            var nivel = ServicioColorStock.Evaluar(stock);
            var (fondo, letra) = ServicioColorStock.ColoresHex(nivel);
            var texto = celda.Background(fondo).Text(valorTexto).FontColor(letra);
            if (nivel == NivelStock.Agotado) texto.Bold();
        }

        private void EscribirCeldaValor(QuestPDF.Infrastructure.IContainer celda, string nombreColumna, string valorTexto)
        {
            if (_config.ColumnasMonetarias.Contains(nombreColumna) && decimal.TryParse(valorTexto, out decimal monto))
                celda.Text($"L. {monto:N2}").FontSize(9);
            else
                celda.Text(valorTexto).FontSize(9);
        }

        private void ComponerTotales(TableDescriptor tTotal)
        {
            tTotal.ColumnsDefinition(c =>
            {
                c.RelativeColumn();
                c.ConstantColumn(160);
            });

            tTotal.Cell().Padding(5).AlignRight().Text(_config.EtiquetaTotal).SemiBold().FontSize(12);
            tTotal.Cell().Background(Colors.Grey.Lighten4).Border(1).BorderColor(Colors.Grey.Lighten2)
                .Padding(5).AlignCenter().Text($"L. {_totalGeneral:N2}").SemiBold().FontSize(12).FontColor(Colors.Blue.Medium);
        }

        private void ComponerPie(RowDescriptor row)
        {
            row.RelativeItem().Column(c =>
            {
                c.Item().Text($"Generado el: {DateTime.Now:dd/MM/yyyy} - {DateTime.Now:hh:mm:ss tt}")
                    .FontSize(8).FontColor(Colors.Grey.Medium);
            });
            row.RelativeItem().AlignRight().Text(x =>
            {
                x.Span("Pág ").FontSize(8);
                x.CurrentPageNumber().FontSize(8);
            });
        }
    }
}