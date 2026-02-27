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
            dgvProveedor.Columns["idClasificacion"].Visible = false;
            dgvProveedor.Columns["idEstado"].Visible = false;
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

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            proveedor.BuscarProveedor(txtBuscar, dgvProveedor);
        }

        private void btnAgregar_Click_1(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.Show();
            this.Hide();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvProveedor.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            int idProveedor = Convert.ToInt32(dgvProveedor.CurrentRow.Cells["idProveedor"].Value);
            string nombre = dgvProveedor.CurrentRow.Cells["Nombre"].Value.ToString();
            string contacto = dgvProveedor.CurrentRow.Cells["Contacto"].Value.ToString();
            string direccion = dgvProveedor.CurrentRow.Cells["Dirección"].Value.ToString();
            string rtn = dgvProveedor.CurrentRow.Cells["RTN"].Value.ToString();
            int idEstado = Convert.ToInt32(dgvProveedor.CurrentRow.Cells["idEstado"].Value);
            int idClasificacion = Convert.ToInt32(dgvProveedor.CurrentRow.Cells["idClasificacion"].Value);

            ModificarProveedor frm = new ModificarProveedor(idProveedor, nombre, contacto, direccion, rtn, idEstado, idClasificacion);
            frm.Show();
            this.Hide();
        }

        private void dtpHasta_ValueChanged(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            proveedor.cargarDatos(dgvProveedor);
        }
    }
}
