using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_BAMS.Facturas
{
    internal class ClsAgregarFactura:ClsConexion
    {

        public async Task<int> AgregarFacturas(int idusuario,int idcliente, int pago, DateTime fecha, int bateria, double rebaja)
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



        private dynamic datosTemp;

        public void ImprimirFactura(int id, string cliente, string fecha, string sub, string desc, string total, string pago, DataGridView dgv, string nombreVendedor)
        {
            datosTemp = new { id, cliente, fecha, sub, desc, total, pago, dgv, nombreVendedor };
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(DisenoFacturaFinal);
            PrintPreviewDialog ppd = new PrintPreviewDialog { Document = pd };
            ppd.ShowDialog();
        }

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
            g.DrawString("Tel: 9651-2489  E-mail: matuteaguilarg@yahoo.com", fDetalles, Brushes.Black, margin, y); y += 20;
            g.DrawString("R.T.N. 08201979002910", fDetalles, Brushes.Black, margin, y); y += 40;

       
            g.DrawString($"Factura N.{datosTemp.id}", fEncabezado, Brushes.Black, margin, y);
            g.DrawString("CLIENTE", fEncabezado, Brushes.Black, margin + 300, y); y += 25;
            g.DrawString($"FECHA: {datosTemp.fecha}", fDetalles, Brushes.Black, margin, y);
            g.DrawString(datosTemp.cliente.ToUpper(), fDetalles, Brushes.Black, margin + 300, y); y += 20;
            g.DrawString($"Vendedor: {datosTemp.nombreVendedor}", fDetalles, Brushes.Black, margin, y); y += 40;

      
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
            int yInicioTotales = y; 

            g.DrawString("SUBTOTAL", fDetalles, Brushes.Black, margin + 450, y);
            g.DrawString($"L. {Convert.ToDouble(datosTemp.sub).ToString("N2", hn)}", fDetalles, Brushes.Black, margin + 600, y); y += 20;
            g.DrawString("DESCUENTO BATERÍA", fDetalles, Brushes.Black, margin + 450, y);
            g.DrawString($"L. {Convert.ToDouble(datosTemp.desc).ToString("N2", hn)}", fDetalles, Brushes.Black, margin + 600, y); y += 25;
            g.DrawString("TOTAL", fTotales, Brushes.Black, margin + 450, y);
            g.DrawString($"L. {valTotal.ToString("N2", hn)}", fTotales, Brushes.Black, margin + 600, y);

            g.DrawString($"Forma de Pago: {datosTemp.pago}", fDetalles, Brushes.Black, margin, yInicioTotales);

            y += 40; 

            
            string textoTotal = NumeroALetras(valTotal);
            g.DrawString($"SON: {textoTotal}", fEncabezado, Brushes.Black, margin, y); y += 60;

          
            string frase = "LA FACTURA ES BENEFICIO DE TODOS EXIJALA";
            SizeF size = g.MeasureString(frase, fPie);
            g.DrawString(frase, fPie, Brushes.Black, (e.PageBounds.Width - size.Width) / 2, y);
        }

        private string NumeroALetras(double total)
        {
            try
            {
                long entero = (long)Math.Truncate(total);
                int decimales = (int)((total - entero) * 100);
                string res = ToText(entero);

                if (decimales > 0)
                    res += " CON " + decimales.ToString("00") + "/100 LEMPIRAS";
                else
                    res += " LEMPIRAS"; 

                return res.ToUpper();
            }
            catch { return total.ToString("N2") + " LEMPIRAS"; }
        }

        private string ToText(double value)
        {
            string str = "";
            value = Math.Truncate(value);
            if (value == 0) str = "CERO";
            else if (value == 1) str = "UNO";
            else if (value == 100) str = "CIEN";
            else if (value < 20) str = new string[] { "", "UN", "DOS", "TRES", "CUATRO", "CINCO", "SEIS", "SIETE", "OCHO", "NUEVE", "DIEZ", "ONCE", "DOCE", "TRECE", "CATORCE", "QUINCE", "DIECISEIS", "DIECISIETE", "DIECIOCHO", "DIECINUEVE" }[(int)value];
            else if (value < 100)
            {
                int u = (int)value % 10;
                str = new string[] { "", "", "VEINTE", "TREINTA", "CUARENTA", "CINCUENTA", "SESENTA", "SETENTA", "OCHENTA", "NOVENTA" }[(int)value / 10];
                if (u > 0) str += " Y " + ToText(u);
            }
            else if (value < 1000)
            {
                int r = (int)value % 100;
                str = new string[] { "", "CIENTO", "DOSCIENTOS", "TRESCIENTOS", "CUATROCIENTOS", "QUINIENTOS", "SEISCIENTOS", "SETECIENTOS", "OCHOCIENTOS", "NOVECIENTOS" }[(int)value / 100];
                if (r > 0) str += " " + ToText(r);
            }
            else if (value < 1000000)
            {
                double mil = Math.Truncate(value / 1000);
                double resto = value % 1000;
                str = (mil == 1 ? "" : ToText(mil)) + " MIL";
                if (resto > 0) str += " " + ToText(resto);
            }
            return str;
        }





    }
}
