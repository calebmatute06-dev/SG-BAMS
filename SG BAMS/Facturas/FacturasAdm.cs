using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;

namespace SG_BAMS
{
    public partial class FacturasAdm : Form
    {
        public FacturasAdm()
        {
            InitializeComponent();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private async Task CargarFactura()
        {
            ClsVerFactura objFac = new ClsVerFactura();
            DataTable datosFac = await objFac.VerFacturas();

            if (datosFac != null)
            {
                dgvFacturas.DataSource = datosFac;

                dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                dgvFacturas.Columns["Vendedor"].HeaderText = "Vendedor";
                dgvFacturas.Columns["Cliente"].HeaderText = "Cliente";
                dgvFacturas.Columns["RTN Cliente"].HeaderText = "RTN Cliente";
                dgvFacturas.Columns["Método de Pago"].HeaderText = "Metodo de pago";
                dgvFacturas.Columns["Fecha"].HeaderText = "Fecha";
                dgvFacturas.Columns["Batería Vieja"].HeaderText = "Batería Vieja";
                dgvFacturas.Columns["Total Unidades"].HeaderText = "Total Unidades";

            }


        }

        private async void FacturasAdm_Load(object sender, EventArgs e)
        {
            await CargarFactura();
        }
    }
}
