using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS
{
    public partial class FacturasEmp : Form
    {
        private DataTable datosFac;

        public FacturasEmp()
        {
            InitializeComponent();
            ConfigurarGrid();
        }

        private void ConfigurarGrid()
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
                MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void FacturasEmp_Load(object sender, EventArgs e)
        {
            await CargarFactura();

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

           
            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

            FiltrarDatos();

            dtpInicio.ValueChanged += (s, ev) => ValidarYFiltrar();
            dtpFin.ValueChanged += (s, ev) => ValidarYFiltrar();
        }

        private void ValidarYFiltrar()
        {
            
            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

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


            string rowFilter = $"[Fecha] >= #{fInicio}# AND [Fecha] < #{fFin}#";


            if (!string.IsNullOrWhiteSpace(texto))
            {
                rowFilter += $" AND (Convert([Factura], 'System.String') LIKE '%{texto}%' OR " +
                             $"[Vendedor] LIKE '%{texto}%' OR " +
                             $"[Cliente] LIKE '%{texto}%' OR " +
                             $"[Método de Pago] LIKE '%{texto}%')";
            }

            try
            {
                dv.RowFilter = rowFilter;
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

                    dtpInicio.Value = DateTime.Today;
                    dtpFin.Value = DateTime.Today;
                    FiltrarPorFecha();
                    dgvFacturas.ClearSelection();
                }
            }
        }

        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.CurrentRow == null)
            {
                MessageBox.Show("Debe seleccionar una fila", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            AbrirDetalle();
        }

        private void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) AbrirDetalle();
        }

        private async void AbrirDetalle()
        {
            var row = dgvFacturas.CurrentRow;
            int idFacturas = Convert.ToInt32(row.Cells[0].Value);
            string nombre_Cliente = row.Cells[2].Value.ToString();
            DateTime fecha = Convert.ToDateTime(row.Cells[6].Value);
            int bateriaVieja = Convert.ToInt32(row.Cells[7].Value);
            int idPago = Convert.ToInt32(row.Cells[10].Value);
            double rebaja = Convert.ToDouble(row.Cells["Rebaja"].Value);

            FacturaVer frmFV = new FacturaVer(idFacturas, nombre_Cliente, fecha, bateriaVieja, idPago, rebaja);
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

        private void FiltrarPorFecha()
        {
            if (datosFac != null)
            {
                DataView dv = datosFac.DefaultView;

                DateTime fechaInicio = dtpInicio.Value.Date;


                DateTime fechaFin = dtpFin.Value.Date.AddDays(1);

                dv.RowFilter = string.Format(System.Globalization.CultureInfo.InvariantCulture,
                    "[Fecha] >= #{0}# AND [Fecha] < #{1}#",
                    fechaInicio.ToString("MM/dd/yyyy"),
                    fechaFin.ToString("MM/dd/yyyy"));

                dgvFacturas.DataSource = dv;
            }
        }

        private void BtnMenu_Click(object sender, EventArgs e) => NavegarA(new MenuPrincipalEmp());
        private void BtnClientes_Click(object sender, EventArgs e) => NavegarA(new ClientesEmp());
        private void BtnInventario_Click(object sender, EventArgs e) => NavegarA(new InventarioEmp());
        private void BtnDeudores_Click(object sender, EventArgs e) => NavegarA(new Deudores_Emp());

        private void BtnNotificaciones_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
            this.Hide();
        }

        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            new Login.Login().Show();
            this.Close();
        }
    }
}