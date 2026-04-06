using Krypton.Toolkit;
using Krypton.Toolkit;
using SG_BAMS.Bitacora;
using SG_BAMS.Bitacora;
using SG_BAMS.MenuPrincipal;
using SG_BAMS.Proveedor;
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

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacora = new BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedores = new ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }



        private void btnadmin_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin admin = new NotificacionesAdmin();
            admin.Show();
        }

        private void btnmenuprincipal_Click(object sender, EventArgs e)
        {
            this.Refresh();
        }

        private void btnajustes_Click(object sender, EventArgs e)
        {
            Ajustes Ajus = new Ajustes();
            Ajus.Show();
        }

        private void btnperfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }

        private void BtnCerrarSesión_Click(object sender, EventArgs e)
        {
            this.Hide();
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
        }

        private void btndeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin Deu = new DeudoresAdmin();
            Deu.Show();
            this.Hide();
        }

        private void btninventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin Intad = new InventarioAdmin();
            Intad.Show();
            this.Hide();
        }

        private void btncompra_Click(object sender, EventArgs e)
        {
            Compras Comp = new Compras();
            Comp.Show();
            this.Hide();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm Fact = new FacturasAdm();
            Fact.Show();
            this.Hide();
        }

        private void btninventario2_Click(object sender, EventArgs e)
        {
            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Hide();
        }

        private void btninventario3_Click(object sender, EventArgs e)
        {

            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Hide();
        }

        private void btndeudores2_Click(object sender, EventArgs e)
        {
            DeudoresAdmin Deu = new DeudoresAdmin();
            Deu.Show();
            this.Hide();
        }

        private void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesAdm Client = new ClientesAdm();
            Client.Show();
            this.Hide();
        }

        private void btnAdministracion_Click(object sender, EventArgs e)
        {
            AdministracionBAMS Admin = new AdministracionBAMS();
            Admin.Show();
            this.Hide();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin reportesAdmin = new ReportesAdmin();
            reportesAdmin.Show();
            this.Close();
        }

        private void btnAsis_Click(object sender, EventArgs e)
        {
            AsistentedeIA AIA = new AsistentedeIA();
            AIA.ShowDialog();
        }
    }
}
