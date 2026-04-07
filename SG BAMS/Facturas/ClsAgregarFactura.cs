using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="SG_BAMS.ClsConexion" />
    internal class ClsAgregarFactura : ClsConexion
    {
        /// <summary>
        /// Agregars the facturas.
        /// </summary>
        /// <param name="idusuario">The idusuario.</param>
        /// <param name="idcliente">The idcliente.</param>
        /// <param name="pago">The pago.</param>
        /// <param name="fecha">The fecha.</param>
        /// <param name="bateria">The bateria.</param>
        /// <param name="rebaja">The rebaja.</param>
        /// <returns></returns>
        public async Task<int> AgregarFacturas(int idusuario, int idcliente, int pago, DateTime fecha, int bateria, double rebaja)
        {
            try
            {
                AbrirConexion();

                using (SqlCommand cmd = new SqlCommand("PA_insertar_facturas", Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_usuario", idusuario);
                    cmd.Parameters.AddWithValue("@id_cliente", idcliente);
                    cmd.Parameters.AddWithValue("@id_tipo_forma_pago", pago);
                    cmd.Parameters.AddWithValue("@fecha_venta", fecha);
                    cmd.Parameters.AddWithValue("@bateria_vieja", bateria);
                    cmd.Parameters.AddWithValue("@manejo_rebaja", rebaja);

                    int idFactura = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    return idFactura;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return 0;
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// Obteners the formas pago.
        /// </summary>
        /// <returns></returns>
        public async Task<DataTable> ObtenerFormasPago()
        {
            DataTable dt = new DataTable();

            try
            {
                AbrirConexion();
                string query = "SELECT * FROM Tipo_Forma_de_pago";

                using (SqlCommand cmd = new SqlCommand(query, Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    dt.Load(reader);
                }
                return dt;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                Cerrar();
            }
        }

        /// <summary>
        /// The datos temporary
        /// </summary>
        private dynamic datosTemp;

        /// <summary>
        /// Imprimirs the factura.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cliente">The cliente.</param>
        /// <param name="fecha">The fecha.</param>
        /// <param name="sub">The sub.</param>
        /// <param name="desc">The desc.</param>
        /// <param name="total">The total.</param>
        /// <param name="pago">The pago.</param>
        /// <param name="dgv">The DGV.</param>
        /// <param name="nombreVendedor">The nombre vendedor.</param>
        /// <param name="esGobierno">if set to <c>true</c> [es gobierno].</param>
        /// <param name="montoExento">The monto exento.</param>
        /// <param name="rtnCliente">The RTN cliente.</param>
        public void ImprimirFactura(int id, string cliente, string fecha, string sub, string desc, string total,
            string pago, DataGridView dgv, string nombreVendedor, bool esGobierno = false, double montoExento = 0, string rtnCliente = "Sin RTN")
        {
            datosTemp = new { id, cliente, fecha, sub, desc, total, pago, dgv, nombreVendedor, esGobierno, montoExento, rtnCliente };
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(DisenoFacturaFinal);
            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.ShowDialog();
        }

        /// <summary>
        /// Disenoes the factura final.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="PrintPageEventArgs"/> instance containing the event data.</param>
        private void DisenoFacturaFinal(object sender, PrintPageEventArgs e)
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

            g.DrawString($"Factura N.{datosTemp.id}", fEncabezado, Brushes.Black, margin, y);
            g.DrawString("CLIENTE", fEncabezado, Brushes.Black, margin + 300, y); y += 25;
            g.DrawString($"FECHA: {datosTemp.fecha}", fDetalles, Brushes.Black, margin, y);
            g.DrawString(datosTemp.cliente.ToUpper(), fDetalles, Brushes.Black, margin + 300, y); y += 20;
            g.DrawString($"Vendedor: {datosTemp.nombreVendedor}", fDetalles, Brushes.Black, margin, y);
            g.DrawString($"R.T.N.: {datosTemp.rtnCliente}", fDetalles, Brushes.Black, margin + 300, y); y += 40;

            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 10;
            g.DrawString("Descripción", fEncabezado, Brushes.Black, margin, y);
            g.DrawString("Cantidad", fEncabezado, Brushes.Black, margin + 280, y);
            g.DrawString("Precio unidad", fEncabezado, Brushes.Black, margin + 420, y);
            g.DrawString("Subtotal", fEncabezado, Brushes.Black, margin + 600, y); y += 25;
            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

            foreach (DataGridViewRow fila in datosTemp.dgv.Rows)
            {
                if (fila.IsNewRow) continue;
                g.DrawString(fila.Cells["nombre_producto"].Value.ToString(), fDetalles, Brushes.Black, margin, y);
                g.DrawString(Convert.ToInt32(fila.Cells["cantidad"].Value).ToString("N0", hn), fDetalles, Brushes.Black, margin + 280, y);
                g.DrawString(Convert.ToDouble(fila.Cells["precio"].Value).ToString("N2", hn), fDetalles, Brushes.Black, margin + 420, y);
                g.DrawString(Convert.ToDouble(fila.Cells["subtotal"].Value).ToString("N2", hn), fDetalles, Brushes.Black, margin + 600, y);
                y += 20;
            }
            y += 20;
            g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

            double valTotal = Convert.ToDouble(datosTemp.total);
            double valDescuento = Convert.ToDouble(datosTemp.desc);
            bool esGobierno = (bool)datosTemp.esGobierno;
            double montoExento = (double)datosTemp.montoExento;

            double impExonerado, impExento, impGravado, isv15, totalFinal;

            
            double baseConDescuento = Math.Max(valTotal, 0);

            if (esGobierno)
            {
                
                impExonerado = Math.Round(baseConDescuento / 1.15, 2);
                impExento = 0;
                impGravado = 0;
                isv15 = 0;
                totalFinal = impExonerado; 
            }
            else
            {
             
                double exentoConISV = Math.Min(montoExento, baseConDescuento);
                double gravadoConISV = baseConDescuento - exentoConISV;

               
                impExento = Math.Round(exentoConISV / 1.15, 2);

            
                impGravado = Math.Round(gravadoConISV / 1.15, 2);

            
                isv15 = Math.Round(gravadoConISV - impGravado, 2);

                impExonerado = 0;

              
                totalFinal = Math.Round(impExento + gravadoConISV, 2);
            }

            int xLabel = margin + 350;
            int xValor = margin + 600;

            double valSubtotal = Convert.ToDouble(datosTemp.sub);

            g.DrawString($"Forma de Pago: {datosTemp.pago}", fDetalles, Brushes.Black, margin, y);

            g.DrawString("SUBTOTAL", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {valSubtotal.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE EXONERADO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impExonerado.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE EXENTO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impExento.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("IMPORTE GRAVADO", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {impGravado.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("DESCUENTOS Y REBAJAS", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {valDescuento.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 20;

            g.DrawString("15% ISV", fDetalles, Brushes.Black, xLabel, y);
            g.DrawString($"L. {isv15.ToString("N2", hn)}", fDetalles, Brushes.Black, xValor, y); y += 25;

            g.DrawString("TOTAL", fTotales, Brushes.Black, xLabel, y);
            g.DrawString($"L. {totalFinal.ToString("N2", hn)}", fTotales, Brushes.Black, xValor, y);

            y += 40;

            g.DrawString($"SON: {NumeroALetras(totalFinal)}", fEncabezado, Brushes.Black, margin, y);

            y += 60;

            string frase = "LA FACTURA ES BENEFICIO DE TODOS EXIJALA";
            SizeF size = g.MeasureString(frase, fPie);
            g.DrawString(frase, fPie, Brushes.Black, (e.PageBounds.Width - size.Width) / 2, y);
        }

        /// <summary>
        /// Numeroes a letras.
        /// </summary>
        /// <param name="total">The total.</param>
        /// <returns></returns>
        private string NumeroALetras(double total)
        {
            if (total < 0) total = 0;
            long entero = (long)Math.Round(total, MidpointRounding.AwayFromZero);
            return $"{EnteroALetras(entero)} LEMPIRAS";
        }

        /// <summary>
        /// Enteroes a letras.
        /// </summary>
        /// <param name="numero">The numero.</param>
        /// <returns></returns>
        private string EnteroALetras(long numero)
        {
            if (numero == 0) return "CERO";
            if (numero < 0) return "MENOS " + EnteroALetras(-numero);

            string[] unidades = {
                "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE",
                "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE",
                "DIECISÉIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE"
            };

            string[] decenas = {
                "", "DIEZ", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA",
                "SESENTA", "SETENTA", "OCHENTA", "NOVENTA"
            };

            string[] centenas = {
                "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS",
                "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS"
            };

            string resultado = "";

            if (numero >= 1_000_000)
            {
                long millones = numero / 1_000_000;
                resultado += (millones == 1 ? "UN MILLÓN " : EnteroALetras(millones) + " MILLONES ");
                numero %= 1_000_000;
            }

            if (numero >= 1_000)
            {
                long miles = numero / 1_000;
                resultado += (miles == 1 ? "MIL " : EnteroALetras(miles) + " MIL ");
                numero %= 1_000;
            }

            if (numero >= 100)
            {
                int c = (int)(numero / 100);
                resultado += (numero == 100 ? "CIEN " : centenas[c] + " ");
                numero %= 100;
            }

            if (numero >= 20)
            {
                int d = (int)(numero / 10);
                int u = (int)(numero % 10);
                resultado += decenas[d] + (u > 0 ? " Y " + unidades[u] + " " : " ");
            }
            else if (numero > 0)
            {
                resultado += unidades[numero] + " ";
            }

            return resultado.Trim();
        }

        /// <summary>
        /// Imprimirs the factura normal.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cliente">The cliente.</param>
        /// <param name="total">The total.</param>
        /// <param name="pago">The pago.</param>
        /// <param name="dgv">The DGV.</param>
        public void ImprimirFacturaNormal(int id, string cliente, string total, string pago, DataGridView dgv)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += (sender, e) =>
            {
                Graphics g = e.Graphics;
                CultureInfo hn = new CultureInfo("es-HN");

                Font fEncabezado = new Font("Arial", 11, FontStyle.Bold);
                Font fDetalles = new Font("Arial", 10, FontStyle.Regular);
                Font fTablaHead = new Font("Arial", 10, FontStyle.Bold);

                int y = 40;
                int margin = 50;
                int width = e.PageBounds.Width - (margin * 2);

                g.DrawString("VENTA DE BATERÍAS MATUTE", new Font("Arial", 13, FontStyle.Bold), Brushes.Black, margin, y); y += 30;

                g.DrawString($"Factura N.{id}", fEncabezado, Brushes.Black, margin, y);
                g.DrawString("CLIENTE", fEncabezado, Brushes.Black, margin + 300, y); y += 22;

                g.DrawString($"FECHA: {DateTime.Now:dd/MM/yyyy}", fDetalles, Brushes.Black, margin, y);
                g.DrawString(cliente.ToUpper(), fDetalles, Brushes.Black, margin + 300, y); y += 18;

                g.DrawString($"Vendedor: {SG_BAMS.Login.Login.UsuarioLogueado}", fDetalles, Brushes.Black, margin, y);
                y += 35;

               
                g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 10;

                g.DrawString("Descripción", fTablaHead, Brushes.Black, margin, y);
                g.DrawString("Cantidad", fTablaHead, Brushes.Black, margin + 280, y);
                g.DrawString("Precio unidad", fTablaHead, Brushes.Black, margin + 420, y);
                g.DrawString("Subtotal", fTablaHead, Brushes.Black, margin + 600, y);
                y += 20;
                g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

           
                foreach (DataGridViewRow fila in dgv.Rows)
                {
                    if (fila.IsNewRow) continue;

                    string nombre = fila.Cells["nombre_producto"].Value?.ToString() ?? "";
                    int cant = Convert.ToInt32(fila.Cells["cantidad"].Value);
                    double precio = Convert.ToDouble(fila.Cells["precio"].Value);
                    double sub = Convert.ToDouble(fila.Cells["subtotal"].Value);

                    g.DrawString(nombre, fDetalles, Brushes.Black, margin, y);
                    g.DrawString(cant.ToString("N0", hn), fDetalles, Brushes.Black, margin + 280, y);
                    g.DrawString(precio.ToString("N2", hn), fDetalles, Brushes.Black, margin + 420, y);
                    g.DrawString(sub.ToString("N2", hn), fDetalles, Brushes.Black, margin + 600, y);
                    y += 20;
                }

                y += 15;
                g.DrawLine(Pens.Black, margin, y, margin + width, y); y += 15;

               
                double valTotal = Convert.ToDouble(total);

                g.DrawString($"Forma de Pago: {pago}", fDetalles, Brushes.Black, margin, y);
                g.DrawString("Total", fDetalles, Brushes.Black, margin + 520, y);
                g.DrawString($"L. {valTotal.ToString("N2", hn)}", fDetalles, Brushes.Black, margin + 600, y);
                y += 50;

             
                
            };

            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.ShowDialog();
        }


    }



}