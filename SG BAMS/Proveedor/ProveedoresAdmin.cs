using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Bitacora;

namespace SG_BAMS.Proveedor
{
    public partial class ProveedoresAdmin : Form
    {
        ClsProveedor proveedor = new ClsProveedor();

        public ProveedoresAdmin()
        {
            InitializeComponent();
        }

        private void ProveedoresAdmin_Load(object sender, EventArgs e)
        {
            proveedor.cargarDatos(dgvProveedor);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {

        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacora = new BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            proveedor.BuscarProveedor(txtBuscar, dgvProveedor);
        }
    }
}
