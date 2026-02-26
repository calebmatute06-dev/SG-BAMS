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
    public partial class frmAdministracion : Form
    {
        public frmAdministracion()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }


        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {

        }


        private void kryptonButton5_Click(object sender, EventArgs e)
        {

        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {

        }



        private void btnVerUsuarios_Click(object sender, EventArgs e)
        {
            frmUsuarios verUsuario = new frmUsuarios();
            verUsuario.Show();
        }

        private void btnRoles_Click(object sender, EventArgs e)
        {
            frmRoles verRoles = new frmRoles();
            verRoles.Show();
        }

        private void btnTproducto_Click(object sender, EventArgs e)
        {
            frmTipoProducto verTproducto = new frmTipoProducto();
            verTproducto.Show();
        }

        private void btnFPago_Click(object sender, EventArgs e)
        {
            frmFormaPago verFormaPago = new frmFormaPago();
            verFormaPago.Show();
        }

        private void btnEstado_Click(object sender, EventArgs e)
        {
            frmEstado verEstado = new frmEstado();
            verEstado.Show();
        }

        private void btnMproducto_Click(object sender, EventArgs e)
        {
            frmMarcaProductos verMproducto = new frmMarcaProductos();
            verMproducto.Show();
        }

        private void btnMauto_Click(object sender, EventArgs e)
        {
            frmModeloAuto verMauto = new frmModeloAuto();
            verMauto.Show();
        }
    }
}
