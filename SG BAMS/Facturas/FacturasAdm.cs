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
                dgvFacturas.Columns["ID Método de Pago"].Visible = false;
                dgvFacturas.Columns["Fecha"].HeaderText = "Fecha";
                dgvFacturas.Columns["Detalle Venta"].HeaderText = "Detalle Venta";
                dgvFacturas.Columns["Cant. Baterías Dejadas"].HeaderText = "Batería Vieja";
                dgvFacturas.Columns["Total Unidades"].HeaderText = "Total Unidades";
            }
        }

        private async void FacturasAdm_Load(object sender, EventArgs e)
        {
            await CargarFactura();
        }

        private async void BtnNueva_Click(object sender, EventArgs e)
        {

            using (ClienteAgregar frmCA = new ClienteAgregar())
            {

                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    await CargarFactura();
                }
            }



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
            int idFacturas, idPago, bateriaVieja;
            string nombre_Cliente;
            DateTime fecha;

            if (dgvFacturas.CurrentRow != null)
            {

                idFacturas = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[0].Value);
                nombre_Cliente = dgvFacturas.CurrentRow.Cells[2].Value.ToString();
                fecha = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells[6].Value);
                bateriaVieja = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[8].Value);
                idPago = Convert.ToInt32(dgvFacturas.CurrentRow.Cells[4].Value);

                FacturaVer frmFV = new FacturaVer(idFacturas, nombre_Cliente, fecha, bateriaVieja, idPago);
                frmFV.ShowDialog();



                CargarFactura();
            }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                dv.RowFilter = string.Format("Convert([Factura],'System.String') LIKE '%{0}%' OR [Vendedor] LIKE '%{0}%' OR [Cliente] LIKE '%{0}%' OR [Método de Pago] LIKE '%{0}%' ", txtBusqueda.Text);

                dgvFacturas.DataSource = dv;

            }
        }

        private void dtpInicio_ValueChanged(object sender, EventArgs e)
        {
            dtpFin.Value = dtpInicio.Value.AddDays(7);
            FiltrarPorFecha();

        }
        private void FiltrarPorFecha()
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                DateTime fechaInicio = dtpInicio.Value.Date;
                DateTime fechaFin = dtpFin.Value.Date;

                dv.RowFilter = string.Format(
                    "[Fecha] >= #{0}# AND [Fecha] <= #{1}#",
                    fechaInicio.ToString("MM/dd/yyyy"),
                    fechaFin.ToString("MM/dd/yyyy")
                );

                dgvFacturas.DataSource = dv;
            }
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;
                dtpInicio.Value = DateTime.Today;
                dtpFin.Value = DateTime.Today;



                dv.RowFilter = string.Empty;
                txtBusqueda.Text = "";


                dgvFacturas.DataSource = dv;
            }
        }

        private void BtnPerfil_Click(object sender, EventArgs e)
        {
            Perfil PF = new Perfil();
            PF.ShowDialog();
        }

        private void BtnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.ShowDialog();
        }

        private void BtnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.ShowDialog();
        }

        private void BtnCompras_Click(object sender, EventArgs e)
        {
            Compras CP = new Compras();
            CP.ShowDialog();
        }

        private void BtnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.ShowDialog();
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.ShowDialog();
        }

        private void BtnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.ShowDialog();
        }

        private void BtnDeudores_Click(object sender, EventArgs e)
        {
            Deudores DU = new Deudores();
            DU.ShowDialog();
        }

        private void BtnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin RA = new ReporteAdmin();
            RA.ShowDialog();
        }

        private void BtnBitacora_Click(object sender, EventArgs e)
        {
            Bitacora BT = new Bitacora();
            BT.ShowDialog();
        }
    }

}