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
    public partial class MenuPrincipalEmp : Form
    {
        public MenuPrincipalEmp()
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

                lblConteoProductos.Text = totalProductos.ToString();
            }
            else
            {
                lblConteoProductos.Text = "0";
            }
        }

        //-----------------------------------------------------------------------------------------




        private async Task CargarVentasRecientes()
        {
            ClsUltimasVentas clsUltimasVentas = new ClsUltimasVentas();
            DataTable datosVentas = await clsUltimasVentas.ObtenerVentasRecientes();

            if (datosVentas != null)
            {

                dgvVentas.DataSource = datosVentas;

                // FIX: Validación de existencia de columnas para evitar ArgumentOutOfRangeException
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


        private async Task CargarGraficoStock()
        {
            DataTable tablaStock = await clsGraficoStock.ObtenerDatosGrafico();

            if (tablaStock != null && tablaStock.Rows.Count > 0)
            {
                chartStock1.Series.Clear();
                chartStock1.Legends.Clear();
                chartStock1.ChartAreas[0].Position.Auto = true;

                Legend leyendaEstandar = chartStock1.Legends.Add("Default");
                leyendaEstandar.BackColor = Color.Transparent;
                leyendaEstandar.IsTextAutoFit = true;
                leyendaEstandar.LegendStyle = LegendStyle.Table;
                leyendaEstandar.Docking = Docking.Right;

                var serieInventario = chartStock1.Series.Add("StockSeries");
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

                chartStock1.BackColor = Color.SkyBlue;
                chartStock1.ChartAreas[0].BackColor = Color.Transparent;
            }
        }





        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarVentasRecientes();
            await CargarGraficoStock();

            ClsTemas.CargarPreferencia();


            ClsTemas.AplicarTema(this);



        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }


        private void kryptonButton13_Click(object sender, EventArgs e)
        {
            FacturasEmp fact = new FacturasEmp();

            fact.Show();
            this.Close();
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {

        }





        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private async void kryptonButton17_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

        }









        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes aju = new Ajustes();

            aju.Show();
        }




        private void lblConteoClientes_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipalEmp_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void btnfacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp fact = new FacturasEmp();

            fact.Show();
            this.Close();
        }

        private async void btnclientes_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.ShowDialog();
            await ActualizarLabel();
            this.Close();
        }

        private async void btninventario_Click(object sender, EventArgs e)
        {
            InventarioEmp inventarioForm = new InventarioEmp();
            inventarioForm.ShowDialog();
            await ActualizarLabelProductos();
            this.Close();
        }

        private async void btndeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp deud = new Deudores_Emp();

            deud.ShowDialog();
            this.Close();
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
        }

        private void btnempleado_Click(object sender, EventArgs e)
        {
            Perfil per = new Perfil();

            per.Show();
        }

        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();
            this.Hide();
        }

        private void btninventario3_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();

            this.Hide();
        }

        private async void btndeudores2_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresForm = new Deudores_Emp();
            deudoresForm.ShowDialog();
            await ActualizarLabelDeudores();
            this.Hide();
        }

        private async void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.ShowDialog();
            await ActualizarLabel();
            this.Hide();
        }

        private void btnnotificaciones_Click(object sender, EventArgs e)
        {

            NotificacionesAdmin notif = new NotificacionesAdmin();
            notif.Show();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm frmCA = new ClientesAdm();
            frmCA.Show();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm frmFA = new FacturasAdm();
            frmFA.Show();
        }
    }
}