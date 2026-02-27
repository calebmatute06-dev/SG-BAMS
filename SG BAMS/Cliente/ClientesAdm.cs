using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
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

            }
        }

        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            await TablaClientes();

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
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
            if (dgvClientes.CurrentRow != null)
            {
                dgvClientes_CellContentClick(null, null);
            }
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
            if (datosCli != null)
            {
                DataView dv = datosCli.DefaultView;

                if (chkActivo.Checked)
                {
                    dv.RowFilter = "Estado = 'Activo'";
                }
                else
                {
                    dv.RowFilter = "";
                }

                dgvClientes.DataSource = dv;

            }

        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            Perfil PF = new Perfil();
            PF.Show();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            Compras CP = new Compras();
            CP.Show();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            Deudores DU = new Deudores();
            DU.Show();
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin RA = new ReporteAdmin();
            RA.Show();
        }

        private void BtnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora BT = new Bitacora();
            BT.Show();
        }
    }
}
    