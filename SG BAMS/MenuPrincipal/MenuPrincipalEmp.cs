using SG_BAMS.Login;
using SG_BAMS.MenuPrincipal;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS
{
    public partial class MenuPrincipalEmp : Form
    {
        private readonly ClsDashboard dashboard = new ClsDashboard();

        public MenuPrincipalEmp()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async void VerificarNotificacionesAhora()
        {
            try
            {
                var noti = new ClsNotificaciones();
                var dt = noti.ListarNotificaciones(false);

                if (dt != null && dt.Rows.Count > 0)
                {
                    ToastNotificacion.MostrarResumen(dt);
                }
                else
                {
                    ToastNotificacion.Mostrar("Centro de Notificaciones", "Sin notificaciones nuevas", 4);
                }
            }
            catch { }
        }

        private async void VerificarNotificacionesAlCargar()
        {
            try
            {
                var noti = new ClsNotificaciones();
                var dt = noti.ListarNotificaciones(false);

                if (dt != null && dt.Rows.Count > 0)
                    ToastNotificacion.MostrarResumen(dt);
            }
            catch { }
        }

        private async Task ActualizarLabelClientes()
        {
            int total = await dashboard.ObtenerTotalClientes();
            label7.Text = total != -1 ? total.ToString() : "0";
        }

        private async Task ActualizarLabelDeudores()
        {
            int totalDeudores = await dashboard.ObtenerTotalDeudores();
            label6.Text = totalDeudores != -1 ? totalDeudores.ToString() : "0";
        }

        private async Task ActualizarLabelProductos()
        {
            int totalProductos = await dashboard.ObtenerTotalProductos();
            lblConteoProductos.Text = totalProductos != -1 ? totalProductos.ToString() : "0";
        }

        private async Task CargarVentasRecientes()
        {
            DataTable datosVentas = await dashboard.ObtenerVentasRecientes();

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
            DataTable tablaStock = await dashboard.ObtenerDatosGraficoStock();

            if (tablaStock != null && tablaStock.Rows.Count > 0)
            {
                chartStock1.Series.Clear();
                chartStock1.Legends.Clear();

                if (chartStock1.ChartAreas.Count == 0)
                    chartStock1.ChartAreas.Add(new ChartArea("Default"));

                var area = chartStock1.ChartAreas[0];
                area.Position.Auto = true;
                area.BackColor = Color.Transparent;
                area.AxisX.Enabled = AxisEnabled.False;
                area.AxisY.Enabled = AxisEnabled.False;

                int sinStock = 0, bajoStock = 0, conStock = 0;

                foreach (DataRow fila in tablaStock.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["Stock"]);
                    if (cantidad == 0) sinStock++;
                    else if (cantidad < 10) bajoStock++;
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
                seriePastel.Points[p1].LegendText = $"Sin Stock ({sinStock})";
                seriePastel.Points[p1].Label = sinStock > 0 ? sinStock.ToString() : "";
                seriePastel.Points[p1].LabelForeColor = Color.White;

                int p2 = seriePastel.Points.AddY(bajoStock);
                seriePastel.Points[p2].Color = Color.FromArgb(220, 180, 0);
                seriePastel.Points[p2].LegendText = $"Bajo Stock ({bajoStock})";
                seriePastel.Points[p2].Label = bajoStock > 0 ? bajoStock.ToString() : "";
                seriePastel.Points[p2].LabelForeColor = Color.Black;

                int p3 = seriePastel.Points.AddY(conStock);
                seriePastel.Points[p3].Color = Color.FromArgb(50, 160, 60);
                seriePastel.Points[p3].LegendText = $"Con Stock ({conStock})";
                seriePastel.Points[p3].Label = conStock > 0 ? conStock.ToString() : "";
                seriePastel.Points[p3].LabelForeColor = Color.White;

                chartStock1.BackColor = Color.SkyBlue;
            }
        }

        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;

            VerificarNotificacionesAlCargar();

            await ActualizarLabelClientes();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarVentasRecientes();
            await CargarGraficoStock();

            ClsTemas.CargarPreferencia();
            ClsTemas.AplicarTema(this);
        }

        private void MenuPrincipalEmp_Shown(object sender, EventArgs e) { Ayudante_UI.AplicarZoomGlobal(this); }

        private void btninventario2_Click(object sender, EventArgs e) { InventarioEmp invemp = new InventarioEmp(); invemp.Show(); this.Hide(); }
        private void btninventario3_Click(object sender, EventArgs e) { InventarioEmp invemp = new InventarioEmp(); invemp.Show(); this.Hide(); }
        private async void btndeudores2_Click(object sender, EventArgs e) { Deudores_Emp deudoresForm = new Deudores_Emp(); deudoresForm.Show(); await ActualizarLabelDeudores(); this.Hide(); }
        private async void btnclientes2_Click(object sender, EventArgs e) { ClientesEmp clienemp = new ClientesEmp(); clienemp.Show(); await ActualizarLabelClientes(); this.Hide(); }
        private void btnnotificaciones_Click(object sender, EventArgs e) { NotificacionesAdmin notif = new NotificacionesAdmin(); notif.Show(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasEmp FE = new FacturasEmp(); FE.Show(); this.Hide(); }
        private void btnClientes_Click(object sender, EventArgs e) { ClientesEmp CE = new ClientesEmp(); CE.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioEmp IE = new InventarioEmp(); IE.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { Deudores_Emp DE = new Deudores_Emp(); DE.Show(); this.Hide(); }
        private void btnVentas_Click(object sender, EventArgs e) { FacturasEmp FE = new FacturasEmp(); FE.Show(); this.Hide(); }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea regresar al inicio de sesión?", "Confirmar",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e) { Perfil perfil = new Perfil(); perfil.Show(); }
    }
}