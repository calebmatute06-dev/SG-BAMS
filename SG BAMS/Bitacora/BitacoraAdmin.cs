using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Proveedor;

namespace SG_BAMS.Bitacora
{
    public partial class BitacoraAdmin : Form
    {
        ClsBitacora bitacora = new ClsBitacora();

        public BitacoraAdmin()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }

        private void btnExportar_Click(object sender, EventArgs e)
        {

        }

        private void Bitacora_Load(object sender, EventArgs e)
        {
            bitacora.cargarDatos(dgvBitacora);
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            bitacora.BuscarBitacora(txtBuscar, dgvBitacora);
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin proveedores = new ProveedoresAdmin();
            proveedores.Show();
            this.Hide();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            bitacora.cargarDatos(dgvBitacora);
        }
    }
}
