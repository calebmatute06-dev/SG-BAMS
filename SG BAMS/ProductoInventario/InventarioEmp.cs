using Microsoft.Data.SqlClient;
using SG_BAMS.ProductoInventario;
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
    public partial class InventarioEmp : Form
    {
        ClsVerProducto logica = new ClsVerProducto();

        public InventarioEmp()
        {
            InitializeComponent();
        }

        private void InventarioEmp_Load(object sender, EventArgs e)
        {
            CargarInventarioCompleto();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarInventarioCompleto();
                return;
            }

            try
            {
                dgvInventarioEmp.DataSource = logica.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        public void CargarInventarioCompleto()
        {
            try
            {
                dgvInventarioEmp.DataSource = logica.MostrarProductosCompleto();
                dgvInventarioEmp.ReadOnly = true;
                dgvInventarioEmp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInventarioEmp.AllowUserToAddRows = false;
                dgvInventarioEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvInventarioEmp.Columns.Contains("Producto"))
                {
                    dgvInventarioEmp.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dgvInventarioEmp_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesEmp CE = new ClientesEmp();
            CE.Show();
            this.Close();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp menu = new MenuPrincipalEmp();
            menu.Show();
            this.Close();
        }

        private void btnFactu_Click(object sender, EventArgs e)
        {
            FacturasEmp facturasEmp = new FacturasEmp();
            facturasEmp.Show();
            this.Close();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp deudoresEmp = new Deudores_Emp();
            deudoresEmp.Show();
            this.Close();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.Show();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}
