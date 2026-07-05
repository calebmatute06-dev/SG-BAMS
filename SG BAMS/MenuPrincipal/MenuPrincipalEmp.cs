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
                leyenda.Font = new Font("Segoe UI", 7.5f, FontStyle.Regular);
                leyenda.LegendStyle = LegendStyle.Table;
                leyenda.TableStyle = LegendTableStyle.Wide;
                leyenda.Alignment = StringAlignment.Center;

                var serie = chartStock1.Series.Add("StockSeries");
                serie.ChartType = SeriesChartType.Doughnut;
                serie["DoughnutRadius"] = "65";
                serie.IsValueShownAsLabel = true;
                serie.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
                serie["PieLabelStyle"] = "Outside";
                serie["PieLineColor"] = "Gray";

                int p1 = serie.Points.AddY(sinStock);
                serie.Points[p1].Color = ColorTranslator.FromHtml("#E74C3C");
                serie.Points[p1].LegendText = $"Sin Stock ({sinStock})";
                serie.Points[p1].Label = sinStock > 0 ? sinStock.ToString() : "";

                int p2 = serie.Points.AddY(bajoStock);
                serie.Points[p2].Color = ColorTranslator.FromHtml("#F39C12");
                serie.Points[p2].LegendText = $"Bajo Stock ({bajoStock})";
                serie.Points[p2].Label = bajoStock > 0 ? bajoStock.ToString() : "";

                int p3 = serie.Points.AddY(conStock);
                serie.Points[p3].Color = ColorTranslator.FromHtml("#27AE60");
                serie.Points[p3].LegendText = $"Con Stock ({conStock})";
                serie.Points[p3].Label = conStock > 0 ? conStock.ToString() : "";

                serie.BorderColor = Color.White;
                serie.BorderWidth = 2;
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


            dgvVentas.BorderStyle = BorderStyle.None;
            dgvVentas.BackgroundColor = Color.White;
            dgvVentas.RowHeadersVisible = false;
            dgvVentas.EnableHeadersVisualStyles = false;
            dgvVentas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvVentas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvVentas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvVentas.ColumnHeadersHeight = 28;

            dgvVentas.DefaultCellStyle.BackColor = Color.White;
            dgvVentas.DefaultCellStyle.ForeColor = Color.Navy;
            dgvVentas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvVentas.DefaultCellStyle.Padding = new Padding(3);
            dgvVentas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvVentas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvVentas.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvVentas.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvVentas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvVentas.GridColor = Color.LightGray;
            dgvVentas.RowTemplate.Height = 32;
            dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvVentas.ClearSelection();
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