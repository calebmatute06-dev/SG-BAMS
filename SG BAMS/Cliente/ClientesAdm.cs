using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace SG_BAMS
{
    public partial class ClientesAdm : Form
    {
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
            DataTable datosCli = await objC.VerClienteTabla();

            if (datosCli != null)
            {
                dgvClientes.DataSource = datosCli;

                dgvClientes.Columns["ID"].HeaderText = "ID del Cliente";
                dgvClientes.Columns["Nombre Completo"].HeaderText = "Nombre Completo";
                dgvClientes.Columns["Teléfono"].HeaderText = "Teléfono";
                dgvClientes.Columns["RTN"].HeaderText = "RTN";




            }
        }

        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            await TablaClientes();

        }

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }
    }
}
    