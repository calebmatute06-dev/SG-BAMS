using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Facturas;
using SG_BAMS.Proveedor;
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
            SG_BAMS.Login.Login log = new SG_BAMS.Login.Login();
            log.Show();
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

        private void btnmenuprincipal_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm Menuad = new MenuPrincipalAdm();
            Menuad.Show();
            this.Hide();
        }

        private void btndeudores_Click(object sender, EventArgs e)
        {
            Deudores Deu = new Deudores();
            Deu.Show();
            this.Hide();
        }

        private void btncompras_Click(object sender, EventArgs e)
        {
            Compras Comp = new Compras();
            Comp.Show();
            this.Hide();
        }

        private void btnclientes_Click(object sender, EventArgs e)
        {
            ClientesAdm Clien = new ClientesAdm();
            Clien.Show();
            this.Hide();
        }

        private void btninventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin inventarioAdmin = new InventarioAdmin();
            inventarioAdmin.Show();
            this.Hide();
        }

        private void btnproveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin Pro = new ProveedoresAdmin();
            Pro.Show();
            this.Hide();
        }

        private void btnReporte_Click(object sender, EventArgs e)
        {
            ReporteAdmin Rep = new ReporteAdmin();
            Rep.Show();
            this.Hide();
        }

        private void btnbitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin bitacora = new BitacoraAdmin();
            bitacora.Show();
            this.Hide();
        }

        private void btnadmin_Click(object sender, EventArgs e)
        {
            Perfil Per = new Perfil();
            Per.Show();
            this.Hide();
        }

        private void btnnotificaciones_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin Noti = new NotificacionesAdmin();
            Noti.Show();

        }

        private void btnajustes_Click(object sender, EventArgs e)
        {
            Ajustes Aju = new Ajustes();
            Aju.Show();
        }

        private void chkHoy_CheckedChanged(object sender, EventArgs e)
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                if (chkHoy.Checked)
                {
                   
                    string hoy = DateTime.Today.ToString("MM/dd/yyyy");

                  
                    dv.RowFilter = string.Format("[Fecha] >= #{0}# AND [Fecha] < #{1}#", hoy,DateTime.Today.AddDays(1).ToString("MM/dd/yyyy"));
                }
                else
                {
                   
                    dv.RowFilter = string.Empty;
                }

                dgvFacturas.DataSource = dv;
            }
        }
    }

}