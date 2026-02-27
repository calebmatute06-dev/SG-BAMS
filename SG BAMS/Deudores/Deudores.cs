using SG_BAMS.Bitacora;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Deudores : Form
    {

        // Variable global para manejar el filtrado (PascalCase por ser campo de clase)
        private DataTable dtDeudores;

        public void CargarGridDeudores()
        {
            
            
        }

        public Deudores()
        {
            InitializeComponent();
            CargarGridDeudores();

            
        }




        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm Menad = new MenuPrincipalAdm();
            Menad.Show();
            this.Close();

        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasAdm factad = new FacturasAdm();
            factad.Show();
            this.Close();
        }

        private void kryptonButton7_Click(object sender, EventArgs e)
        {
            Compras Comp = new Compras();
            Comp.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesAdm Clientad = new ClientesAdm();
            Clientad.Show();
            this.Close();
        }

        private void kryptonButton5_Click(object sender, EventArgs e)
        {
            InventarioAdmin Invad = new InventarioAdmin();
            Invad.Show();
            this.Close();
        }

        private void kryptonButton4_Click(object sender, EventArgs e)
        {
            
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Show();

        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin Repoad = new ReporteAdmin();
            Repoad.Show();
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
           
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
            this.Close();


        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();

        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin Notad = new NotificacionesAdmin();
            Notad.Show();
            this.Close();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes Ajus = new Ajustes();
            Ajus.Show();


        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
        }

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            Pago_Deuda PagDe = new Pago_Deuda();
            PagDe.ShowDialog();
            CargarGridDeudores();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void kryptonButton12_Click(object sender, EventArgs e)
        {
          
        }

        private void dgvDeudores_DoubleClick(object sender, EventArgs e)
        {

        }

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menuPrincipalAdm = new MenuPrincipalAdm();
            menuPrincipalAdm.Show(this);
            this.Hide();
        }

        private void btnFactura_Click(object sender, EventArgs e)
        {
            FacturasAdm facturasAdm = new FacturasAdm();
            facturasAdm.Show();
            this.Hide();
        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientesAdm = new ClientesAdm();
            clientesAdm.Show();
            this.Hide();
        }

        private void btnInve_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventarioAdmin = new InventarioAdmin();
            inventarioAdmin.Show();
            this.Hide();
        }

        private void btnProvee_Click(object sender, EventArgs e)
        {
            Proveedor.ProveedoresAdmin proveedoresAdmin = new Proveedor.ProveedoresAdmin();
            proveedoresAdmin.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores deudores = new Deudores();
            deudores.Show();
            this.Hide();
        }

        

        
    }
}
