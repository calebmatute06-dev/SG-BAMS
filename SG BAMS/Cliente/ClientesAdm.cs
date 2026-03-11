using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Cliente;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SG_BAMS
{
    public partial class ClientesAdm : Form
    {
        DataTable datosCli;
        public ClientesAdm()
        {
            InitializeComponent();
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dgvClientes.MultiSelect = false;
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private async Task TablaClientes()
        {

            ClsVerCliente objC = new ClsVerCliente();
            datosCli = await objC.VerClienteTabla();

            if (datosCli != null)
            {
                dgvClientes.DataSource = datosCli;

                dgvClientes.Columns["ID"].HeaderText = "ID Cliente";
                dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                dgvClientes.Columns["Apellido"].HeaderText = "Apellido";
                dgvClientes.Columns["Teléfono"].HeaderText = "Teléfono";
                dgvClientes.Columns["RTN"].HeaderText = "RTN";
                dgvClientes.Columns["Estado"].HeaderText = "Estado";
                dgvClientes.Columns["ID Estado"].Visible = false;
                DataView dv = datosCli.DefaultView;
                dv.RowFilter = "Estado = 'Activo'";

                dgvClientes.DataSource = dv;
                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClientes.AllowUserToAddRows = false;
                dgvClientes.ReadOnly = true;
                dgvClientes.ClearSelection();


            }
        }

        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            await TablaClientes();
            dgvClientes.ClearSelection();
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToOrderColumns = false;


        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;
            int idCliente, idEstado;
            string nombreCliente, apellidoCliente, telefonoCliente, rtnCliente;

            if (dgvClientes.CurrentRow != null)
            {

                idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                nombreCliente = dgvClientes.CurrentRow.Cells[1].Value.ToString();
                apellidoCliente = dgvClientes.CurrentRow.Cells[2].Value.ToString();
                telefonoCliente = dgvClientes.CurrentRow.Cells[3].Value.ToString();
                rtnCliente = dgvClientes.CurrentRow.Cells[4].Value.ToString();
                idEstado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[5].Value);
                ClienteModificar frmMo = new ClienteModificar(idCliente, nombreCliente, apellidoCliente, telefonoCliente, rtnCliente, idEstado);
                frmMo.ShowDialog();
                TablaClientes();
            }
        }


        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila",
                                "Ninguna fila seleccionada",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }
            if (dgvClientes.CurrentRow != null)
            {
                dgvClientes_CellDoubleClick(null, null);
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {

            if (datosCli != null)
            {
                DataView dv = datosCli.DefaultView;

                dv.RowFilter = string.Format("Nombre LIKE '%{0}%' OR Apellido LIKE '%{0}%' OR RTN LIKE '%{0}%' OR Teléfono LIKE '%{0}%' OR Estado LIKE '%{0}%'", txtBusqueda.Text);

                dgvClientes.DataSource = dv;
                dgvClientes.ClearSelection();

            }
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            DataView dv = datosCli.DefaultView;
            if (chkActivo.Checked)
            {

                dv.RowFilter = "";
                dv.RowFilter = "Estado <> 'Activo'";
                dgvClientes.ClearSelection();
            }

            else
            {
                dv.RowFilter = "Estado = 'Activo'";
                dgvClientes.ClearSelection();
            }
            dgvClientes.DataSource = dv;

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Hide();
        }

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            Perfil PF = new Perfil();
            PF.Show();
            this.Hide();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            Compras CP = new Compras();
            CP.Show();
            this.Hide();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DU = new DeudoresAdmin();
            DU.Show();
            this.Hide();
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            
        }

        private void BtnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BT = new BitacoraAdmin();
            BT.Show();
            this.Hide();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }
    }
}
    