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
using SG_BAMS.Reporte;

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
            dgvProveedor.Columns["idProveedor"].Visible = false;
            dgvProveedor.Columns["idClasificacion"].Visible = false;
            dgvProveedor.Columns["idEstado"].Visible = false;
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.ReadOnly = true;
            dgvProveedor.ClearSelection();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores();
            agregar.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm menu = new MenuPrincipalAdm();
            menu.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm facturas = new FacturasAdm();
            facturas.Show();
            this.Hide();
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

            ModificarProveedor(dgvProveedor.CurrentRow);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            proveedor.cargarDatos(dgvProveedor);

        }

        private void btnCompras_Click(object sender, EventArgs e)
        {
            Compras compras = new Compras();
            compras.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm clientes = new ClientesAdm();
            clientes.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventario = new InventarioAdmin();
            inventario.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin deudores = new DeudoresAdmin();
            deudores.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReportesAdmin reportes = new ReportesAdmin();
            reportes.Show();
            this.Close();
        }

        private void dgvProveedor_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow fila = dgvProveedor.Rows[e.RowIndex];
                ModificarProveedor(fila);
            }
        }

        private void ModificarProveedor(DataGridViewRow fila)
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

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.Show();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
