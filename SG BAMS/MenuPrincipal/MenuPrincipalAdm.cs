using Krypton.Toolkit;
using SG_BAMS.Bitacora;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class MenuPrincipalAdm : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MenuPrincipalAdm" />.
        /// </summary>
        public MenuPrincipalAdm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }


        /// <summary>
        /// La clase contador de clientes
        /// </summary>
        Clscontador_cliente clsContadorCliente = new Clscontador_cliente();
        /// <summary>
        /// La clase contador de deudas
        /// </summary>
        ClsContadorDeuda clsContadorDeuda = new ClsContadorDeuda();
        /// <summary>
        /// La clase contador de productos
        /// </summary>
        ClsContadorProducto clsContadorProducto = new ClsContadorProducto();
        /// <summary>
        /// La clase gráfico de stock
        /// </summary>
        ClsGraficoStock clsGraficoStock = new ClsGraficoStock();
        /// <summary>
        /// El objeto contador
        /// </summary>
        Clscontador_cliente objetoContador = new Clscontador_cliente();



        /// <summary>
        /// Actualiza la etiqueta de clientes.
        /// </summary>
        private async Task ActualizarLabel()
        {

            int total = await objetoContador.ObtenerTotalClientes();

            if (total != -1)
                label7.Text = total.ToString();
            else
                label7.Text = "0";
        }

        /// <summary>
        /// Actualiza la etiqueta de deudores.
        /// </summary>
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

        /// <summary>
        /// Actualiza la etiqueta de productos.
        /// </summary>
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




        /// <summary>
        /// Carga el gráfico de stock.
        /// </summary>
        private async Task CargarGraficoStock()
        {
            DataTable tablaStock = await clsGraficoStock.ObtenerDatosGrafico();

            if (tablaStock != null && tablaStock.Rows.Count > 0)
            {
                chartStock.Series.Clear();
                chartStock.Legends.Clear();
                chartStock.ChartAreas[0].Position.Auto = true;

                int sinStock = 0;
                int bajoStock = 0;
                int conStock = 0;

                foreach (DataRow fila in tablaStock.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["STOCK"]);
                    if (cantidad == 0) sinStock++;
                    else if (cantidad < 5) bajoStock++;
                    else conStock++;
                }


                Legend leyenda = chartStock.Legends.Add("Leyenda");
                leyenda.BackColor = Color.Transparent;
                leyenda.Docking = Docking.Bottom;
                leyenda.Font = new Font("Segoe UI", 9f, FontStyle.Bold);


                var seriePastel = chartStock.Series.Add("StockSeries");
                seriePastel.ChartType = SeriesChartType.Pie;
                seriePastel.IsValueShownAsLabel = true;
                seriePastel.Font = new Font("Segoe UI", 10f, FontStyle.Bold);


                seriePastel["PieLabelStyle"] = "Inside";
                seriePastel.LabelForeColor = Color.White;


                int p1 = seriePastel.Points.AddY(sinStock);
                seriePastel.Points[p1].Color = Color.FromArgb(210, 50, 50);
                seriePastel.Points[p1].LegendText = $"Sin Stock ({sinStock})";
                seriePastel.Points[p1].Label = sinStock > 0 ? sinStock.ToString() : "";

                int p2 = seriePastel.Points.AddY(bajoStock);
                seriePastel.Points[p2].Color = Color.FromArgb(220, 180, 0);
                seriePastel.Points[p2].LegendText = $"Bajo Stock ({bajoStock})";
                seriePastel.Points[p2].LabelForeColor = Color.Black;
                seriePastel.Points[p2].Label = bajoStock > 0 ? bajoStock.ToString() : "";

                int p3 = seriePastel.Points.AddY(conStock);
                seriePastel.Points[p3].Color = Color.FromArgb(50, 160, 60);
                seriePastel.Points[p3].LegendText = $"Con Stock ({conStock})";
                seriePastel.Points[p3].Label = conStock > 0 ? conStock.ToString() : "";


                var area = chartStock.ChartAreas[0];
                area.BackColor = Color.Transparent;
                area.AxisX.Enabled = AxisEnabled.False;
                area.AxisY.Enabled = AxisEnabled.False;

                chartStock.BackColor = Color.SkyBlue;
            }
        }




        /// <summary>
        /// Carga el gráfico de productos más vendidos.
        /// </summary>
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


        /// <summary>
        /// Maneja el evento Load del control MenuPrincipalAdm.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void MenuPrincipalAdm_Load(object sender, EventArgs e)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;



            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarGraficoStock();
            await CargarGraficoMasVendidos();
        }




        /// <summary>
        /// Maneja el evento Click del control btnadmin.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnadmin_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin admin = new NotificacionesAdmin();
            admin.Show();
        }






        /// <summary>
        /// Maneja el evento Click del control btninventario2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btninventario3.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btninventario3_Click(object sender, EventArgs e)
        {

            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btndeudores2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btndeudores2_Click(object sender, EventArgs e)
        {
            DeudoresAdmin Deu = new DeudoresAdmin();
            Deu.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnclientes2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesAdm Client = new ClientesAdm();
            Client.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnAdministracion.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            AdministracionBAMS Admin = new AdministracionBAMS();
            Admin.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnAsis.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnAsis_Click(object sender, EventArgs e)
        {
            AsistentedeIA AIA = new AsistentedeIA();
            AIA.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento Click del control btnProd.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnProd_Click(object sender, EventArgs e)
        {
            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Hide();
        }



        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();

        }

        /// <summary>
        /// Maneja el evento Click del control btnCompra.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }


        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click_1(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnInventario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnProveedores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnReportes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnBitacora.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnPerfil.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnPerfil_Click_1(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}