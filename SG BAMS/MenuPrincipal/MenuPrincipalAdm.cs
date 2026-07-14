using Krypton.Toolkit;
using SG_BAMS.Bitacora;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace SG_BAMS
{
    public partial class MenuPrincipalAdm : Form
    {
        private readonly ClsDashboard dashboard = new ClsDashboard();

        public MenuPrincipalAdm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void VerificarNotificacionesAhora()
        {
            try
            {
                var noti = new ClsNotificaciones();
                var dt = noti.ListarNotificaciones(true);

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
                var dt = noti.ListarNotificaciones(true);

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
            label8.Text = totalProductos != -1 ? totalProductos.ToString() : "0";
        }

        private async Task CargarGraficoStock()
        {
            DataTable tablaStock = await dashboard.ObtenerDatosGraficoStock();

            if (tablaStock != null && tablaStock.Rows.Count > 0)
            {
                chartStock.Series.Clear();
                chartStock.Legends.Clear();

                if (chartStock.ChartAreas.Count == 0)
                    chartStock.ChartAreas.Add(new ChartArea("Default"));

                var area = chartStock.ChartAreas[0];
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

                Legend leyenda = chartStock.Legends.Add("Leyenda");
                leyenda.BackColor = Color.Transparent;
                leyenda.Docking = Docking.Bottom;
                leyenda.Font = new Font("Segoe UI", 7.5f, FontStyle.Regular);
                leyenda.LegendStyle = LegendStyle.Table;
                leyenda.TableStyle = LegendTableStyle.Wide;
                leyenda.Alignment = StringAlignment.Center;

                var serie = chartStock.Series.Add("StockSeries");
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
        private async Task CargarGraficoMasVendidos()
        {
            try
            {
                DataTable datosVentas = await dashboard.ObtenerProductosMasVendidos();

                if (datosVentas != null && datosVentas.Rows.Count > 0)
                {
                    chartMasVendidos.Series.Clear();
                    chartMasVendidos.Legends.Clear();
                    chartMasVendidos.ChartAreas[0].AxisX.CustomLabels.Clear();
                    chartMasVendidos.DataSource = null;

                    // Color de fondo = el celeste de la tarjeta contenedora (ajustá si tu tarjeta usa otro tono)
                    chartMasVendidos.BackColor = ColorTranslator.FromHtml("#8ED1E8");

                    var serieBarras = chartMasVendidos.Series.Add("MasVendidos");
                    serieBarras.ChartType = SeriesChartType.Column;
                    serieBarras["PointWidth"] = "0.6";
                    serieBarras["BarLabelStyle"] = "Top";

                    // Paleta con más "vida": degradado de azul intenso a turquesa
                    string[] paletaHex = { "#1B6FA8", "#2389C9", "#2FA6D8", "#3FC1D0", "#54D6C4" };

                    for (int i = 0; i < datosVentas.Rows.Count; i++)
                    {
                        DataRow fila = datosVentas.Rows[i];
                        string nombreProducto = fila["producto"].ToString().Trim();
                        int totalVendido = Convert.ToInt32(fila["total_vendido"]);

                        int idx = serieBarras.Points.AddXY(i, totalVendido);
                        serieBarras.Points[idx].AxisLabel = nombreProducto;
                        serieBarras.Points[idx].Label = totalVendido.ToString();
                        serieBarras.Points[idx].LabelForeColor = Color.FromArgb(30, 30, 30);
                        serieBarras.Points[idx].Font = new Font("Segoe UI", 9f, FontStyle.Bold);

                        string colorHex = paletaHex[Math.Min(i, paletaHex.Length - 1)];
                        serieBarras.Points[idx].Color = ColorTranslator.FromHtml(colorHex);
                        serieBarras.Points[idx]["PointWidth"] = "0.6";
                    }

                    // Efecto de brillo/degradado dentro de cada barra (más "premium" que color plano)
                    serieBarras["BarDrawingStyle"] = "Cylinder"; // prueba visual moderna; alternativa: "Default"

                    var area = chartMasVendidos.ChartAreas[0];
                    area.AxisX.Interval = 1;
                    area.AxisX.LabelStyle.Angle = -35;
                    area.AxisX.LabelStyle.Font = new Font("Segoe UI", 8.5f, FontStyle.Regular);
                    area.AxisX.LabelStyle.ForeColor = Color.FromArgb(40, 40, 40);
                    area.AxisX.MajorGrid.Enabled = false;
                    area.AxisX.LineColor = Color.FromArgb(255, 255, 255, 120);

                    area.AxisY.MajorGrid.LineColor = Color.FromArgb(255, 255, 255, 90); // grid blanco translúcido, sutil sobre el celeste
                    area.AxisY.LabelStyle.Font = new Font("Segoe UI", 8.5f);
                    area.AxisY.LabelStyle.ForeColor = Color.FromArgb(40, 40, 40);
                    area.AxisY.LineColor = Color.Transparent;

                    area.BackColor = Color.Transparent; // el área en sí transparente, se ve el celeste del chart de fondo
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar gráfico: " + ex.Message);
            }
        }
        private async void MenuPrincipalAdm_Load(object sender, EventArgs e)
        {
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;

            VerificarNotificacionesAlCargar();

            await ActualizarLabelClientes();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarGraficoStock();
            await CargarGraficoMasVendidos();
        }

        private void btnadmin_Click(object sender, EventArgs e) { NotificacionesAdmin admin = new NotificacionesAdmin(); admin.ShowDialog(); }
        private void btninventario2_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(); Invad.Show(); this.Hide(); }
        private void btninventario3_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(); Invad.Show(); this.Hide(); }
        private void btndeudores2_Click(object sender, EventArgs e) { DeudoresAdmin Deu = new DeudoresAdmin(); Deu.Show(); this.Hide(); }
        private void btnclientes2_Click(object sender, EventArgs e) { ClientesAdm Client = new ClientesAdm(); Client.Show(); this.Hide(); }
        private void btnAdministracion_Click(object sender, EventArgs e) { AdministracionBAMS Admin = new AdministracionBAMS(); Admin.Show(); this.Hide(); }
        private void btnProd_Click(object sender, EventArgs e) { InventarioAdmin Invad = new InventarioAdmin(); Invad.Show(); this.Hide(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasAdm FA = new FacturasAdm(); FA.Show(); this.Hide(); }
        private void btnCompra_Click(object sender, EventArgs e) { Compras CF = new Compras(); CF.Show(); this.Hide(); }
        private void btnClientes_Click_1(object sender, EventArgs e) { ClientesAdm CA = new ClientesAdm(); CA.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioAdmin IA = new InventarioAdmin(); IA.Show(); this.Hide(); }
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            PA.Show();
            this.Hide();
        }
        private void btnDeudores_Click(object sender, EventArgs e) { DeudoresAdmin DA = new DeudoresAdmin(); DA.Show(); this.Hide(); }
        private void btnReportes_Click(object sender, EventArgs e) { ReportesAdmin RA = new ReportesAdmin(); RA.Show(); this.Hide(); }
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
        }

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

        private void btnPerfil_Click_1(object sender, EventArgs e) { Perfil perfil = new Perfil(); perfil.ShowDialog(); }
    }
}