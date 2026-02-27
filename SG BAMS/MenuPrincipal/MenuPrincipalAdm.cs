using Krypton.Toolkit;
using SG_BAMS.Bitacora;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.Proveedor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS
{
    public partial class MenuPrincipalAdm : Form
    {
        public MenuPrincipalAdm()
        {
            InitializeComponent();
        }


        Clscontador_cliente clsContadorCliente = new Clscontador_cliente();
        ClsContadorDeuda clsContadorDeuda = new ClsContadorDeuda();
        ClsContadorProducto clsContadorProducto = new ClsContadorProducto();
        ClsGraficoStock clsGraficoStock = new ClsGraficoStock();
        Clscontador_cliente objetoContador = new Clscontador_cliente();


        //Bloque de contadores 
        //--------------------------------------------------------------------



        // Método asíncrono para que no se congele la interfaz al conectar con Somee
        private async Task ActualizarLabel()
        {

            int total = await objetoContador.ObtenerTotalClientes();

            if (total != -1)
                label7.Text = total.ToString();
            else
                label7.Text = "0";
        }

        private async Task ActualizarLabelDeudores()
        {

            int totalDeudores = await clsContadorDeuda.ObtenerTotalDeudores();

            if (totalDeudores != -1)
            {
                label6.Text = totalDeudores.ToString();
            }
            else
            {
                label6.Text = "0";
            }
        }

        private async Task ActualizarLabelProductos()
        {

            int totalProductos = await clsContadorProducto.ObtenerTotalProductos();

            if (totalProductos != -1)
            {

                label8.Text = totalProductos.ToString();
            }
            else
            {
                label8.Text = "0";
            }
        }

        //-----------------------------------------------------------------------------------------



        private async Task CargarGraficoStock()
        {
            DataTable tablaStock = await clsGraficoStock.ObtenerDatosGrafico();

            if (tablaStock != null && tablaStock.Rows.Count > 0)
            {
                chartStock.Series.Clear();
                chartStock.Legends.Clear();
                chartStock.ChartAreas[0].Position.Auto = true;

                Legend leyendaEstandar = chartStock.Legends.Add("Default");
                leyendaEstandar.BackColor = Color.Transparent;
                leyendaEstandar.IsTextAutoFit = true;
                leyendaEstandar.LegendStyle = LegendStyle.Table;
                leyendaEstandar.Docking = Docking.Right;

                var serieInventario = chartStock.Series.Add("StockSeries");
                serieInventario.ChartType = SeriesChartType.Pie;

                foreach (DataRow filaDatos in tablaStock.Rows)
                {
                    // --- CORRECCIÓN DE NOMBRES DE COLUMNA SEGÚN LA VISTA NUEVA ---
                    string nombreArticulo = filaDatos["Nombre Producto"].ToString(); // Antes "producto"
                    int cantidadReal = Convert.ToInt32(filaDatos["STOCK"]);           // Antes "cantidad"

                    double valorVisual = (cantidadReal == 0) ? 0.6 : cantidadReal;

                    int puntoIndice = serieInventario.Points.AddXY(nombreArticulo, valorVisual);
                    var puntoActual = serieInventario.Points[puntoIndice];

                    if (cantidadReal == 0)
                    {
                        puntoActual.Color = Color.Red;
                        puntoActual.LegendText = nombreArticulo + " - Agotado";
                        puntoActual.Label = "0";
                    }
                    else if (cantidadReal < 5)
                    {
                        puntoActual.Color = Color.Yellow;
                        puntoActual.LegendText = nombreArticulo + " - A punto de agotarse";
                        puntoActual.Label = cantidadReal.ToString();
                    }
                    else
                    {
                        puntoActual.Color = Color.Green;
                        puntoActual.LegendText = nombreArticulo + " (" + cantidadReal + ")";
                        puntoActual.Label = cantidadReal.ToString();
                    }
                }

                chartStock.BackColor = Color.SkyBlue;
                chartStock.ChartAreas[0].BackColor = Color.Transparent;
            }
        }




        private async Task CargarGraficoMasVendidos()
        {
            try
            {
                ClsGraficoVentas objGraficoVentas = new ClsGraficoVentas();
                DataTable datosVentas = await objGraficoVentas.ObtenerProductosMasVendidos();



                if (datosVentas != null && datosVentas.Rows.Count > 0)
                {

                    chartMasVendidos.Series.Clear();
                    chartMasVendidos.ChartAreas[0].AxisX.CustomLabels.Clear();
                    chartMasVendidos.DataSource = null;


                    var serieBarras = chartMasVendidos.Series.Add("MasVendidos");
                    serieBarras.ChartType = SeriesChartType.Column;


                    for (int i = 0; i < datosVentas.Rows.Count; i++)
                    {
                        DataRow fila = datosVentas.Rows[i];
                        string nombreProducto = fila["producto"].ToString().Trim();
                        int totalVendido = Convert.ToInt32(fila["total_vendido"]);


                        serieBarras.Points.AddXY(i, totalVendido);


                        serieBarras.Points[i].AxisLabel = nombreProducto;


                        serieBarras.Points[i].Label = totalVendido.ToString();
                    }


                    var area = chartMasVendidos.ChartAreas[0];
                    area.AxisX.Interval = 1;
                    area.AxisX.LabelStyle.Angle = -45;
                    area.AxisX.MajorGrid.Enabled = false;


                    chartMasVendidos.Palette = ChartColorPalette.BrightPastel;
                    chartMasVendidos.BackColor = Color.SkyBlue;
                    area.BackColor = Color.Transparent;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al separar barras: " + ex.Message);
            }
        }






        private async void MenuPrincipalAdm_Load(object sender, EventArgs e)
        {
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarGraficoStock();
            await CargarGraficoMasVendidos();





        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();

            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton13_Click(object sender, EventArgs e)
        {
            InventarioAdmin invam = new InventarioAdmin();

            invam.Show();

            this.Close();

        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            FacturasAdm fact = new FacturasAdm();

            fact.Show();
            this.Close();
        }

        private async void kryptonButton17_Click(object sender, EventArgs e)
        {
            ClientesAdm Client = new ClientesAdm();
            Client.Show();
            await ActualizarLabel();

            this.Close();
        }

        private async void kryptonButton16_Click(object sender, EventArgs e)
        {
            Deudores deu = new Deudores();
            deu.Show();
            await ActualizarLabel();

            this.Close();
        }

        private async void kryptonButton15_Click(object sender, EventArgs e)
        {
            InventarioAdmin invam = new InventarioAdmin();

            invam.Show();

            await ActualizarLabel();

            this.Close();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes ajus = new Ajustes();
            ajus.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notifam = new NotificacionesAdmin();
            notifam.Show();
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasAdm fact = new FacturasAdm();

            fact.Show();
            this.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Compras compr = new Compras();
            compr.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesAdm clientesAdm = new ClientesAdm();
            clientesAdm.Show();
            this.Close();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            InventarioAdmin invam = new InventarioAdmin();

            invam.Show();
            this.Close();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proadm = new ProveedoresAdmin();
            proadm.Show();
            this.Close();
        }

        private async void kryptonButton2_Click(object sender, EventArgs e)
        {
            Deudores deu = new Deudores();
            deu.ShowDialog();
            this.Close();
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();


        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin reporte = new ReporteAdmin();
            reporte.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bit = new BitacoraAdmin();
            bit.Show();
            this.Close();
        }

        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            frmAdministracion administracion = new frmAdministracion();
            administracion.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void MenuPrincipalAdm_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm frmFA = new FacturasAdm();
            frmFA.Show();
        }

        private void BtnCerrarSesión_Click(object sender, EventArgs e)
        {
            this.Close();
            SG_BAMS.Login.Login Log = new Login.Login();

            Log.Show();
        }

        private void chartMasVendidos_Click(object sender, EventArgs e)
        {

        }
    }
}
