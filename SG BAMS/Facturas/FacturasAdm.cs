using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class FacturasAdm : Form
    {
        DataTable datosFac;
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
            datosFac = await objFac.VerFacturas();

            if (datosFac != null)
            {
                dgvFacturas.DataSource = datosFac;

                dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                dgvFacturas.Columns["Vendedor"].HeaderText = "Vendedor";
                dgvFacturas.Columns["Cliente"].HeaderText = "Cliente";
                dgvFacturas.Columns["RTN Cliente"].HeaderText = "RTN Cliente";
                dgvFacturas.Columns["Método de Pago"].HeaderText = "Metodo de pago";
                dgvFacturas.Columns["ID Método de Pago"].HeaderText = "ID Metodo de pago";
                dgvFacturas.Columns["Fecha"].HeaderText = "Fecha";
                dgvFacturas.Columns["Detalle Venta"].HeaderText = "Detalle Venta";
                dgvFacturas.Columns["Batería Vieja"].HeaderText = "Batería Vieja";
                dgvFacturas.Columns["Total Unidades"].HeaderText = "Total Unidades";
            }
        }

        private async void FacturasAdm_Load(object sender, EventArgs e)
        {
            await CargarFactura();
        }

        private void BtnNueva_Click(object sender, EventArgs e)
        {
            ClienteAgregar frmCA = new ClienteAgregar();
            frmCA.ShowDialog();

            this.Close();
        }

        
        

        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.CurrentRow != null)
            {
                dgvFacturas_CellContentClick(null, null);
            }
        }

        private void dgvFacturas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            int idFacturas, idPago;
            string nombre_Cliente, bateriaVieja;
            DateTime fecha;

            if (dgvFacturas.CurrentRow != null)
            {

                idFacturas = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
                nombre_Cliente = dgvFacturas.CurrentRow.Cells[2].Value.ToString();
                fecha = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[6].Value);
                bateriaVieja = dgvFacturas.CurrentRow.Cells[8].Value.ToString();
                idPago = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[9].Value);

                FacturaVer frmFV = new FacturaVer(idFacturas, nombre_Cliente, fecha, bateriaVieja, idPago);
                frmFV.ShowDialog();
                CargarFactura();
            }
        }
    }
}