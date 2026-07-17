using SG_BAMS.Facturas.DTO;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class GeneradorFacturaImpresa
    {
        public void ImprimirFactura(FacturaDTO factura, ResultadoImpuestosFactura impuestos, string nombreVendedor, string formaPagoTexto)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) => DisenoFacturaFinal(e, factura, impuestos, nombreVendedor, formaPagoTexto);
            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.ShowDialog();
        }

        private void DisenoFacturaFinal(PrintPageEventArgs e, FacturaDTO factura, ResultadoImpuestosFactura impuestos, string nombreVendedor, string formaPagoTexto)
        {
            Graphics g = e.Graphics;
            CultureInfo hn = new CultureInfo("es-HN");

            Font fEmpresa = new Font("Arial", 16, FontStyle.Bold);
            Font fDetalles = new Font("Arial", 10, FontStyle.Regular);
            Font fEncabezado = new Font("Arial", 11, FontStyle.Bold);
            Font fTotales = new Font("Arial", 11, FontStyle.Bold);
            Font fPie = new Font("Arial", 11, FontStyle.Bold | FontStyle.Italic);

            int y = 40;
            int margin = 50;
            int width = e.PageBounds.Width - (margin * 2);

            g.DrawString("VENTA DE BATERÍAS MATUTE", fEmpresa, Brushes.Black, margin, y); y += 30;
            g.DrawString("GRODSBIN ISAIAS MATUTE AGUILAR", fEncabezado, Brushes.Black, margin, y); y += 25;
            g.DrawString("Col. Gilberto Rodriguez, Una Cuadra de Cuerpo de Bomberos", fDetalles, Brushes.Black, margin, y); y += 20;
            g.DrawString("Talanga, Francisco Morazán, Honduras, C. A.", fDetalles, Brushes.Black, margin, y); y += 20;
            g.DrawString("Tel: 9651-2489  E-mail: matuteaguilarg@gmail.com", fDetalles, Brushes.Black, margin, y); y += 20;
            g.DrawString("R.T.N. 08201979002910", fDetalles, Brushes.Black, margin, y); y += 40;

            g.DrawString($"Factura N.{factura.IdFactura}", fEncabezado, Brushes.Black, margin, y);
            g.DrawString("CLIENTE", fEncabezado, Brushes.Black, margin + 300, y); y += 25;
            g.DrawString($"FECHA: {factura.Fecha.ToShortDateString()}", fDetalles, Brushes.Black, margin, y);
            g.DrawString(factura.NombreCliente.ToUpper(), fDetalles, Brushes.Black, margin + 300, y); y += 20;
            g.DrawString($"Vendedor: {nombreVendedor}", fDetalles, Brushes.Black, margin, y);
            g.DrawString($"R.T.N.: {factura.RtnCliente}", fDetalles, Brushes.Black, margin + 300, y); y += 40;

            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 10;
            g.DrawString("Descripción", fEncabezado, Brushes.Black, margin, y);
            g.DrawString("Cantidad", fEncabezado, Brushes.Black, margin + 280, y);
            g.DrawString("Precio unidad", fEncabezado, Brushes.Black, margin + 420, y);
            g.DrawString("Subtotal", fEncabezado, Brushes.Black, margin + 600, y); y += 25;
            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

            foreach (var linea in factura.Detalle)
            {
                g.DrawString(linea.NombreProducto, fDetalles, Brushes.Black, margin, y);
                g.DrawString(linea.Cantidad.ToString("N0", hn), fDetalles, Brushes.Black, margin + 280, y);
                g.DrawString(linea.Precio.ToString("N2", hn), fDetalles, Brushes.Black, margin + 420, y);
                g.DrawString(linea.Subtotal.ToString("N2", hn), fDetalles, Brushes.Black, margin + 600, y);
                y += 20;
            }
            y += 20;
            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

            int xLabel = margin + 350;
            int xValor = margin + 600;

            g.DrawString($"Forma de Pago: {formaPagoTexto}", fDetalles, Brushes.Black, margin, y);

            g.DrawString("SUBTOTAL", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {factura.Subtotal.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE EXONERADO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impuestos.ImporteExonerado.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE EXENTO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impuestos.ImporteExento.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE GRAVADO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impuestos.ImporteGravado.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("DESCUENTOS Y REBAJAS", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {factura.RebajaBateria.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("15% ISV", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impuestos.Isv15.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 25;

            g.DrawString("TOTAL", fTotales, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impuestos.TotalFinal.ToString("N2", hn)}", fTotales, Brushes.Black, xValor, y);

            y += 40;
            g.DrawString($"SON: {ConversorNumeroALetras.ATexto(impuestos.TotalFinal)}", fEncabezado, Brushes.Black, margin, y);

            y += 60;
            string frase = "LA FACTURA ES BENEFICIO DE TODOS EXIJALA";
            SizeF size = g.MeasureString(frase, fPie);
            g.DrawString(frase, fPie, Brushes.Black, (e.PageBounds.Width - size.Width) / 2, y);
        }
    }
}

