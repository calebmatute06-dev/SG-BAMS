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


                dgvVentas.Columns["factura_id"].HeaderText = "N° Factura";
                dgvVentas.Columns["nombre_completo_cliente"].HeaderText = "Cliente";
                dgvVentas.Columns["fecha_registro"].HeaderText = "Fecha";
                dgvVentas.Columns["metodo_pago"].HeaderText = "Pago";

                dgvVentas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

       






        private async void MenuPrincipalEmp_Load(object sender, EventArgs e)
        {
            await ActualizarLabel();
            await ActualizarLabelDeudores();
            await ActualizarLabelProductos();
            await CargarVentasRecientes();
       

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
            this.Close();
        }

        private void btninventario3_Click(object sender, EventArgs e)
        {
            InventarioEmp invemp = new InventarioEmp();

            invemp.Show();

            this.Close();
        }

        private async void btndeudores2_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresForm = new Deudores_Emp();
            deudoresForm.ShowDialog();
            await ActualizarLabelDeudores();
            this.Close();
        }

        private async void btnclientes2_Click(object sender, EventArgs e)
        {
            ClientesEmp clienemp = new ClientesEmp();
            clienemp.ShowDialog();
            await ActualizarLabel();
            this.Close();
        }

        private void btnnotificaciones_Click(object sender, EventArgs e)
        {

            NotificacionesEmp notif = new NotificacionesEmp();
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
