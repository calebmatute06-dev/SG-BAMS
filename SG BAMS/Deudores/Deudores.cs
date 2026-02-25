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


        public void CargarGridDeudores()
        {
            try
            {
                ClsDeuda objetoDeuda = new ClsDeuda();
                dgvDeudores.DataSource = objetoDeuda.ListarDeudores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
            ProveedoresAdmin Proad = new ProveedoresAdmin();
            Proad.Show();
            this.Close();
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
            Bitacora Bit = new Bitacora();
            Bit.Show();
            this.Close();
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
            PagDe.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }
    }
}
