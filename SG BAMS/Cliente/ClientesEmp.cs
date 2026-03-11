using SG_BAMS.Cliente;
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
    public partial class ClientesEmp : Form
    {
        DataTable datosCli;
        public ClientesEmp()
        {
            InitializeComponent();
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            dgvClientes.MultiSelect = false;
        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (datosCli != null)
            {
                DataView dv = datosCli.DefaultView;

                dv.RowFilter = string.Format("Nombre LIKE '%{0}%' OR Apellido LIKE '%{0}%' OR RTN LIKE '%{0}%' OR Teléfono LIKE '%{0}%' OR Estado LIKE '%{0}%'", txtBusqueda.Text);

                dgvClientes.DataSource = dv;

            }
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            DataView dv = datosCli.DefaultView;
            if (chkActivo.Checked)
            {

                dv.RowFilter = "";
                dv.RowFilter = "Estado <> 'Activo'";
            }

            else
            {
                dv.RowFilter = "Estado = 'Activo'";
            }
            dgvClientes.DataSource = dv;
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return; 
            }
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

        private void BtnModificar_Click(object sender, EventArgs e)
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

        private async void ClientesEmp_Load(object sender, EventArgs e)
        {
            await TablaClientes();
            dgvClientes.ClearSelection();
            dgvClientes.ReadOnly = true;
            dgvClientes.AllowUserToOrderColumns = false;
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp MPE = new MenuPrincipalEmp();
            MPE.Show();
            this.Hide();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }



        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
            this.Hide();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Hide();
        }

        private void btnAjustes_Click(object sender, EventArgs e)
        {
            Ajustes ajustes = new Ajustes();
            ajustes.Show();
        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
