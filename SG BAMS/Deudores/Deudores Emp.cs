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
    public partial class Deudores_Emp : Form
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



        public Deudores_Emp()
        {
            InitializeComponent();
        }

        private void kryptonButton11_Click(object sender, EventArgs e)
        {
            Ajustes Ajust = new Ajustes();
            Ajust.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            NotificacionesEmp Notiemp = new NotificacionesEmp();
            Notiemp.Show();
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

        private void kryptonButton9_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp Menemp = new MenuPrincipalEmp();
            Menemp.Show();
            this.Close();
        }

        private void kryptonButton8_Click(object sender, EventArgs e)
        {
            FacturasEmp Factemp = new FacturasEmp();
            Factemp.Show();
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            ClientesEmp Clientemp = new ClientesEmp();
            Clientemp.Show();
            this.Close();
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {

        }
    }
}
