using SG_BAMS.MenuPrincipal;
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

                // Opcional: Aqui podemos cambiar el nombre de las columnas para el usuario final
                dgvVentas.Columns["factura_id"].HeaderText = "N° Factura";
                dgvVentas.Columns["nombre_completo_cliente"].HeaderText = "Cliente";
                dgvVentas.Columns["fecha_registro"].HeaderText = "Fecha";
                dgvVentas.Columns["metodo_pago"].HeaderText = "Pago";

                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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

                // Configuramos la leyenda para que no corte el texto
                Legend leyendaEstandar = chartStock.Legends.Add("Default");
                leyendaEstandar.BackColor = Color.Transparent;
                leyendaEstandar.IsTextAutoFit = true; // Ajusta el tamaño de letra para que quepa
                leyendaEstandar.LegendStyle = LegendStyle.Table; // Formato de tabla para mejor orden
                leyendaEstandar.Docking = Docking.Right;

                var serieInventario = chartStock.Series.Add("StockSeries");
                serieInventario.ChartType = SeriesChartType.Pie;

                foreach (DataRow filaDatos in tablaStock.Rows)
                {
                    string nombreArticulo = filaDatos["producto"].ToString();
                    int cantidadReal = Convert.ToInt32(filaDatos["cantidad"]);

                    double valorVisual = (cantidadReal == 0) ? 0.6 : cantidadReal;

                    int puntoIndice = serieInventario.Points.AddXY(nombreArticulo, valorVisual);
                    var puntoActual = serieInventario.Points[puntoIndice];

                    // Asignamos los estados completos
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






        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarVentasRecientes();
            await CargarGraficoStock();
        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            //this.Close();
            this.Dispose();
        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();

        }

        private void kryptonButton13_Click(object sender, EventArgs e)
        {
            FacturasEmp fact = new FacturasEmp();

            fact.Show();
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton15_Click_1(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();
        }

        private async void kryptonButton16_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresForm = new Deudores_Emp();
            deudoresForm.ShowDialog(); // El código se detiene aquí hasta cerrar la ventana
            await ActualizarLabelDeudores(); // Se refresca al cerrar
        }

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private async void kryptonButton17_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.ShowDialog(); // Espera a que cierres la ventana de clientes
            await ActualizarLabel(); // Refresca el contador automáticamente
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Perfil per = new Perfil();

            per.Show();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasEmp fact = new FacturasEmp();

            fact.Show();
        }

        private async void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.ShowDialog();
            await ActualizarLabel();
        }

        private async void kryptonButton5_Click(object sender, EventArgs e)
        {
            InventarioEmp inventarioForm = new InventarioEmp();
            inventarioForm.ShowDialog(); // Espera a que se cierre
            await ActualizarLabelProductos(); // Refresca el número
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Deudores_Emp deud = new Deudores_Emp();

            deud.Show();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes aju = new Ajustes();

            aju.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesEmp notif = new NotificacionesEmp();
            notif.Show();
        }



        

        private void lblConteoClientes_Click(object sender, EventArgs e)
        {

        }
    }
}
