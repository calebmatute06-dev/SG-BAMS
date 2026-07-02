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
                chartStock.ChartAreas[0].Position.Auto = true;

                int sinStock = 0;
                int bajoStock = 0;
                int conStock = 0;

                foreach (DataRow fila in tablaStock.Rows)
                {
                    int cantidad = Convert.ToInt32(fila["STOCK"]);
                    if (cantidad == 0) sinStock++;
                    else if (cantidad < 10) bajoStock++;
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

        private async Task CargarGraficoMasVendidos()
        {
            try
            {
                DataTable datosVentas = await dashboard.ObtenerProductosMasVendidos();

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
            btnMenu.Enabled = false;
            btnMenu.BackColor = Color.SkyBlue;
            btnMenu.ForeColor = Color.White;

            await ActualizarLabelClientes();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarGraficoStock();
            await CargarGraficoMasVendidos();
        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin admin = new NotificacionesAdmin();
            admin.Show();
        }

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
        private void btnProveedores_Click(object sender, EventArgs e) { ProveedoresAdmin PA = new ProveedoresAdmin(); PA.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { DeudoresAdmin DA = new DeudoresAdmin(); DA.Show(); this.Hide(); }
        private void btnReportes_Click(object sender, EventArgs e) { ReportesAdmin RA = new ReportesAdmin(); RA.Show(); this.Hide(); }
        private void btnBitacora_Click(object sender, EventArgs e) { BitacoraAdmin BA = new BitacoraAdmin(); BA.Show(); this.Hide(); }

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

        private void btnPerfil_Click_1(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}