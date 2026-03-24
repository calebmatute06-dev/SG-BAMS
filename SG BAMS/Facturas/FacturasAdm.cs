using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Facturas;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS
{
    public partial class FacturasAdm : Form
    {
        private DataTable datosFac;

        public FacturasAdm()
        {
            InitializeComponent();
            ConfigurarInterfazGrid();
        }

        private void ConfigurarInterfazGrid()
        {
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            
            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private async Task CargarFactura()
        {
            try
            {
                ClsVerFactura objFac = new ClsVerFactura();
                datosFac = await objFac.VerFacturas();

                if (datosFac != null)
                {
                    dgvFacturas.DataSource = datosFac;

                    
                    dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                    dgvFacturas.Columns["ID Método de Pago"].Visible = false;
                    dgvFacturas.Columns["Rebaja"].HeaderText = "Rebaja Batería";

                    
                    dgvFacturas.Columns["Rebaja"].DisplayIndex = 8;
                    dgvFacturas.Columns["Batería Vieja"].DisplayIndex = 7;
                    dgvFacturas.Columns["Total Unidades"].DisplayIndex = 9;

                    dgvFacturas.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al sincronizar datos: {ex.Message}", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FacturasAdm_Load(object sender, EventArgs e)
        {
            await CargarFactura();

            
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

            FiltrarDatos();

            
            dtpInicio.ValueChanged += (s, ev) => ValidarYFiltrarFechas();
            dtpFin.ValueChanged += (s, ev) => ValidarYFiltrarFechas();
        }

        private void ValidarYFiltrarFechas()
        {
            if (dtpFin.Value < dtpInicio.Value)
                dtpFin.Value = dtpInicio.Value;

            FiltrarDatos();
        }

        private void FiltrarDatos()
        {
            if (datosFac == null) return;

            DataView dv = datosFac.DefaultView;

            
            string fInicio = dtpInicio.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
            string fFin = dtpFin.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);

            
            string texto = txtBusqueda.Text.Replace("'", "''").Replace("[", "[[]").Replace("]", "[]]");

            
            string query = $"[Fecha] >= #{fInicio}# AND [Fecha] < #{fFin}#";

            if (!string.IsNullOrWhiteSpace(texto))
            {
                query += $" AND (Convert([Factura], 'System.String') LIKE '%{texto}%' OR " +
                         $"[Vendedor] LIKE '%{texto}%' OR " +
                         $"[Cliente] LIKE '%{texto}%' OR " +
                         $"[Método de Pago] LIKE '%{texto}%')";
            }

            try
            {
                dv.RowFilter = query;
                dgvFacturas.DataSource = dv;
                dgvFacturas.ClearSelection();
            }
            catch { }
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e) => FiltrarDatos();

        private async void BtnNueva_Click(object sender, EventArgs e)
        {
            using (ClienteAgregar frmCA = new ClienteAgregar())
            {
                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    await CargarFactura();
                    FiltrarDatos();
                }
            }
        }

        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una factura de la lista.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AbrirDetalleFactura();
        }

        private void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AbrirDetalleFactura();
        }

        private async void AbrirDetalleFactura()
        {
            var row = dgvFacturas.CurrentRow;
            int id = Convert.ToInt32(row.Cells["Factura"].Value);
            string cliente = row.Cells["Cliente"].Value.ToString();
            DateTime fecha = Convert.ToDateTime(row.Cells["Fecha"].Value);
            int batVieja = Convert.ToInt32(row.Cells["Batería Vieja"].Value);
            int idPago = Convert.ToInt32(row.Cells["ID Método de Pago"].Value);
            double rebaja = Convert.ToDouble(row.Cells["Rebaja"].Value);

            FacturaVer frmFV = new FacturaVer(id, cliente, fecha, batVieja, idPago, rebaja);
            frmFV.ShowDialog();

            await CargarFactura();
            FiltrarDatos();
        }

        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Clear();
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            FiltrarDatos();
        }

        

        private void NavegarA(Form formulario)
        {
            formulario.Show();
            this.Close();
        }

        private void btnmenuprincipal_Click(object sender, EventArgs e) => NavegarA(new MenuPrincipalAdm());
        private void btnclientes_Click(object sender, EventArgs e) => NavegarA(new ClientesAdm());
        private void btninventario_Click(object sender, EventArgs e) => NavegarA(new InventarioAdmin());
        private void btnproveedores_Click(object sender, EventArgs e) => NavegarA(new ProveedoresAdmin());
        private void btncompra_Click(object sender, EventArgs e) => NavegarA(new Compras());
        private void btnbitacora_Click(object sender, EventArgs e) => NavegarA(new BitacoraAdmin());
        private void btnReporte_Click(object sender, EventArgs e) => NavegarA(new ReportesAdmin());

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new SG_BAMS.Login.Login().Show();
            this.Close();
        }

        private void btnajustes_Click(object sender, EventArgs e) => new Ajustes().ShowDialog();
        private void btnPerfil_Click(object sender, EventArgs e) => new Perfil().ShowDialog();
    }
}