using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace SG_BAMS.Reporte
{
    /// <summary>
    /// 
    /// </summary>
    ///
    public class DocumentoDinamico : IDocument
    {
        /// <summary>
        /// El DataGridView
        /// </summary>
        private DataGridView _dgv;
        /// <summary>
        /// El título de cabecera
        /// </summary>
        private string _tituloCabecera;
        /// <summary>
        /// La fecha desde
        /// </summary>
        private DateTime _desde;
        /// <summary>
        /// La fecha hasta
        /// </summary>
        private DateTime _hasta;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="DocumentoDinamico" />.
        /// </summary>
        /// <param name="dgv">El DataGridView.</param>
        /// <param name="tituloCabecera">El título de cabecera.</param>
        /// <param name="desde">La fecha desde.</param>
        /// <param name="hasta">La fecha hasta.</param>
        public DocumentoDinamico(DataGridView dgv, string tituloCabecera, DateTime desde, DateTime hasta)
        {
            _dgv = dgv;
            _tituloCabecera = tituloCabecera;
            _desde = desde;
            _hasta = hasta;
        }

        /// <summary>
        /// Configura el contenido del documento especificando su estructura de diseño y elementos visuales.
        /// </summary>
        /// <param name="container">El contenedor del documento utilizado para definir el contenido a través de FluentAPI.</param>
        public void Compose(IDocumentContainer container)
        {
            decimal totalGeneral = 0;
            int indiceColumna = -1;

            string titulo = _tituloCabecera.ToUpper();

            if (titulo.Contains("VENTAS"))
                indiceColumna = 5;
            else if (titulo.Contains("COMPRAS"))
                indiceColumna = 6;
            else if (titulo.Contains("DEUDORES"))
                indiceColumna = 6;
            else if (titulo.Contains("INVENTARIO"))
            {

                indiceColumna = _dgv.Columns.Count - 1;
            }

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

                    if (titulo.Contains("VENTAS") || titulo.Contains("COMPRAS"))
                    {
                        col.Item().Text($"Rango del reporte: {_desde:dd/MM/yyyy} al {_hasta:dd/MM/yyyy}")
                            .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
                    }
                    else if (titulo.Contains("INVENTARIO"))
                    {
                        col.Item().Text($"Estado actual del stock al: {DateTime.Now:dd/MM/yyyy}")
                            .FontSize(12).Italic().FontColor(Colors.Grey.Darken2);
                    }
                });

                page.Content().PaddingVertical(10).Column(col =>
                {
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
                                        string[] columnasDinero = { "Total_Venta", "Inversion_Total", "Monto_Credito",
                                        "Saldo_Pendiente", "Precio_Unitario", "Total_Venta_Esperada", "Abonado" };

                                        if (columnasDinero.Contains(nombreColumna) && decimal.TryParse(valorTexto, out decimal monto))
                                            celda.Text($"L. {monto:N2}").FontSize(9);
                                        else
                                            celda.Text(valorTexto).FontSize(9);
                                    }
                                }
                            }
                        }
                    });

                    if (indiceColumna != -1)
                    {
                        col.Item().PaddingTop(10).AlignRight().Table(tTotal =>
                        {
                            tTotal.ColumnsDefinition(c =>
                            {
                                c.RelativeColumn();
                                c.ConstantColumn(160);
                            });

                            string etiqueta = "TOTAL GENERAL:";
                            string tituloUpper = _tituloCabecera.ToUpper();

                            if (tituloUpper.Contains("VENTAS"))
                                etiqueta = "TOTAL VENTAS:";
                            else if (tituloUpper.Contains("COMPRAS"))
                                etiqueta = "TOTAL EN COMPRAS:";
                            else if (tituloUpper.Contains("DEUDORES"))
                                etiqueta = "TOTAL SALDO PENDIENTE:";
                            else if (tituloUpper.Contains("INVENTARIO"))
                                etiqueta = "CAPITAL TOTAL EN STOCK:";

                            tTotal.Cell().Padding(5).AlignRight().Text(etiqueta).SemiBold().FontSize(12);

                            tTotal.Cell().Background(Colors.Grey.Lighten4).Border(1).BorderColor(Colors.Grey.Lighten2)
                                .Padding(5).AlignCenter().Text($"L. {totalGeneral:N2}").SemiBold().FontSize(12).FontColor(Colors.Blue.Medium);
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
}