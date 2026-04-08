using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
    public partial class MenuPrincipalEmp : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="MenuPrincipalEmp" />.
        /// </summary>
        public MenuPrincipalEmp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

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

                lblConteoProductos.Text = totalProductos.ToString();
            }
            else
            {
                lblConteoProductos.Text = "0";
            }
        }





        /// <summary>
        /// Carga las ventas recientes.
        /// </summary>
        private async Task CargarVentasRecientes()
        {
            ClsUltimasVentas clsUltimasVentas = new ClsUltimasVentas();
            DataTable datosVentas = await clsUltimasVentas.ObtenerVentasRecientes();

            if (datosVentas != null)
            {

                dgvVentas.DataSource = datosVentas;


                if (dgvVentas.Columns.Contains("factura_id"))
                    dgvVentas.Columns["factura_id"].HeaderText = "N° Factura";

                if (dgvVentas.Columns.Contains("nombre_completo_cliente"))
                    dgvVentas.Columns["nombre_completo_cliente"].HeaderText = "Cliente";

                if (dgvVentas.Columns.Contains("fecha_registro"))
                    dgvVentas.Columns["fecha_registro"].HeaderText = "Fecha";

                if (dgvVentas.Columns.Contains("metodo_pago"))
                    dgvVentas.Columns["metodo_pago"].HeaderText = "Pago";

                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                chartStock1.Series.Clear();
                chartStock1.Legends.Clear();


                if (chartStock1.ChartAreas.Count == 0)
                {
                    chartStock1.ChartAreas.Add(new ChartArea("Default"));
                }

                var area = chartStock1.ChartAreas[0];
                area.Position.Auto = true;
                area.BackColor = Color.Transparent;
                area.AxisX.Enabled = AxisEnabled.False;
                area.AxisY.Enabled = AxisEnabled.False;


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


                Legend leyenda = chartStock1.Legends.Add("Leyenda");
                leyenda.BackColor = Color.Transparent;
                leyenda.Docking = Docking.Bottom;
                leyenda.Font = new Font("Segoe UI", 9f, FontStyle.Bold);


                var seriePastel = chartStock1.Series.Add("StockSeries");
                seriePastel.ChartType = SeriesChartType.Pie;
                seriePastel.IsValueShownAsLabel = true;
                seriePastel.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
                seriePastel["PieLabelStyle"] = "Inside";




                int p1 = seriePastel.Points.AddY(sinStock);
                seriePastel.Points[p1].Color = Color.FromArgb(210, 50, 50);
                seriePastel.Points[p1].LegendText = $"Sin Stock ({sinStock} productos)";
                seriePastel.Points[p1].Label = sinStock > 0 ? sinStock.ToString() : "";
                seriePastel.Points[p1].LabelForeColor = Color.White;


                int p2 = seriePastel.Points.AddY(bajoStock);
                seriePastel.Points[p2].Color = Color.FromArgb(220, 180, 0);
                seriePastel.Points[p2].LegendText = $"Bajo Stock ({bajoStock} productos)";
                seriePastel.Points[p2].Label = bajoStock > 0 ? bajoStock.ToString() : "";
                seriePastel.Points[p2].LabelForeColor = Color.Black;


                int p3 = seriePastel.Points.AddY(conStock);
                seriePastel.Points[p3].Color = Color.FromArgb(50, 160, 60);
                seriePastel.Points[p3].LegendText = $"Con Stock ({conStock} productos)";
                seriePastel.Points[p3].Label = conStock > 0 ? conStock.ToString() : "";
                seriePastel.Points[p3].LabelForeColor = Color.White;

                chartStock1.BackColor = Color.SkyBlue;
            }
        }





        /// <summary>
        /// Maneja el evento Load del control MenuPrincipalEmp.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;

            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarVentasRecientes();
            await CargarGraficoStock();

            ClsTemas.CargarPreferencia();


            ClsTemas.AplicarTema(this);



        }





        /// <summary>
        /// Maneja el evento Shown del control MenuPrincipalEmp.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void MenuPrincipalEmp_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }


        /// <summary>
        /// Maneja el evento Click del control btninventario2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btninventario3.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btninventario3_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();

            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btndeudores2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void btndeudores2_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresForm = new Deudores_Emp();
            deudoresForm.Show();
            await ActualizarLabelDeudores();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnclientes2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private async void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.Show();
            await ActualizarLabel();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnnotificaciones.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs" /> que contiene los datos del evento.</param>
        private void btnnotificaciones_Click(object sender, EventArgs e)
        {

            NotificacionesAdmin notif = new NotificacionesAdmin();
            notif.Show();
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
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnInventario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
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
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        /// <summary>
        /// Maneja el evento Click del control btnVentas.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnVentas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();

        }
    }
}