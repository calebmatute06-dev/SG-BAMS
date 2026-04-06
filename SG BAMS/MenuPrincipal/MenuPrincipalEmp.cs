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
            this.Hide();
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









       




        private void lblConteoClientes_Click(object sender, EventArgs e)
        {

        }

        private void MenuPrincipalEmp_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void btnfacturas_Click(object sender, EventArgs e)
        {

            this.Hide();
            FacturasEmp fact = new FacturasEmp();

            fact.Show();

        }

        private async void btnclientes_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.Show();
            this.Close();
        }

        private async void btninventario_Click(object sender, EventArgs e)
        {
            InventarioEmp inventarioForm = new InventarioEmp();
            inventarioForm.Show();
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

        private void btnmenuprincipal_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnAsis_Click(object sender, EventArgs e)
        {
            AsistentedeIA AIA = new AsistentedeIA();
            AIA.ShowDialog();
        }
    }
}