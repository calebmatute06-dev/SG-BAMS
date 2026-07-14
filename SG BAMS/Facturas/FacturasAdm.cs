using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Facturas;
using SG_BAMS.Facturas.DTO;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Data;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacturasAdm : Form
    {
        /// <summary>
        /// The datos fac
        /// </summary>
        private DataTable datosFac;

        /// <summary>
        /// Texto del placeholder para evitar filtrarlo
        /// </summary>
        private string placeholderText = "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN y Método de Pago";

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturasAdm"/> class.
        /// </summary>
        public FacturasAdm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            ConfigurarGrid();
        }

        /// <summary>
        /// Configurars the grid.
        /// </summary>
        private void ConfigurarGrid()
        {
            try
            {
                dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvFacturas.MultiSelect = false;
                dgvFacturas.AllowUserToAddRows = false;
                dgvFacturas.ReadOnly = true;
                dgvFacturas.AllowUserToOrderColumns = false;

                txtBusqueda.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
                txtBusqueda.TextChanged += txtBusqueda_TextChanged;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo conectar a la base de datos.\n\n" +
                    $"Detalle: {ex.Message}",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Cargars the factura.
        /// </summary>
        private async Task CargarFactura()
        {
            try
            {
                ClsDetalleFactura objFac = new ClsDetalleFactura();
                datosFac = await objFac.VerFacturas();

                if (datosFac != null)
                {
                    dgvFacturas.DataSource = datosFac;

                    dgvFacturas.Columns["Factura"].HeaderText = "N° Factura";
                    dgvFacturas.Columns["ID Método de Pago"].Visible = false;
                    dgvFacturas.Columns["Rebaja"].HeaderText = "Rebaja de Batería Vieja";

                    dgvFacturas.Columns["Rebaja"].DisplayIndex = 8;
                    dgvFacturas.Columns["Batería Vieja"].DisplayIndex = 7;

                    dgvFacturas.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar facturas: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the FacturasAdm control.
        /// </summary>
        private async void FacturasAdm_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, placeholderText);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            btnFacturas.Enabled = false;
            btnFacturas.BackColor = Color.SkyBlue;
            btnFacturas.ForeColor = Color.White;

            try
            {
                await CargarFactura();

                dtpInicio.Value = DateTime.Today;
                dtpFin.Value = DateTime.Today;

                ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

                dtpInicio.ValueChanged += (s, ev) => ValidarYFiltrar();
                dtpFin.ValueChanged += (s, ev) => ValidarYFiltrar();

                ClsMensajeGuia.ActivarK(txtBusqueda);

                dgvFacturas.BorderStyle = BorderStyle.None;
                dgvFacturas.BackgroundColor = Color.White;
                dgvFacturas.RowHeadersVisible = false;
                dgvFacturas.EnableHeadersVisualStyles = false;
                dgvFacturas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

                dgvFacturas.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
                dgvFacturas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
                dgvFacturas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                dgvFacturas.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
                dgvFacturas.ColumnHeadersHeight = 28;

                dgvFacturas.DefaultCellStyle.BackColor = Color.White;
                dgvFacturas.DefaultCellStyle.ForeColor = Color.Navy;
                dgvFacturas.DefaultCellStyle.Font = new Font("Segoe UI", 10);
                dgvFacturas.DefaultCellStyle.Padding = new Padding(3);
                dgvFacturas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
                dgvFacturas.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

                dgvFacturas.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
                dgvFacturas.DefaultCellStyle.SelectionForeColor = Color.White;

                dgvFacturas.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
                dgvFacturas.GridColor = Color.LightGray;
                dgvFacturas.RowTemplate.Height = 32;
                dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvFacturas.ClearSelection();

                FiltrarDatos();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo conectar a la base de datos.\n\n" +
                    $"Detalle: {ex.Message}",
                    "Error de Conexión",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }
        }

        /// <summary>
        /// Validars the y filtrar.
        /// </summary>
        private void ValidarYFiltrar()
        {
            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

            if (dtpFin.Value < dtpInicio.Value)
                dtpFin.Value = dtpInicio.Value;

            FiltrarDatos();
        }

        /// <summary>
        /// Filtrars the datos combinando texto y rango de fechas.
        /// Ambos filtros se aplican simultáneamente con AND.
        /// </summary>
        private void FiltrarDatos()
        {
            if (datosFac == null) return;

            try
            {
                DataView dv = datosFac.DefaultView;

                string texto = txtBusqueda.Text?.Trim() ?? "";

                if (texto == placeholderText)
                {
                    texto = "";
                }

                var condiciones = new List<string>();

                string fInicio = dtpInicio.Value.Date.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string fFin = dtpFin.Value.Date.AddDays(1).ToString("MM/dd/yyyy", CultureInfo.InvariantCulture);
                string filtroFechas = $"[Fecha] >= #{fInicio}# AND [Fecha] < #{fFin}#";
                condiciones.Add($"({filtroFechas})");

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    string textoFiltro = texto
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]");

                    string filtroTexto = $"(Convert([Factura], 'System.String') LIKE '%{textoFiltro}%' OR " +
                                         $"[Vendedor] LIKE '%{textoFiltro}%' OR " +
                                         $"[Cliente] LIKE '%{textoFiltro}%' OR " +
                                         $"[Método de Pago] LIKE '%{textoFiltro}%' OR " +
                                         $"[RTN Cliente] LIKE '%{textoFiltro}%')";

                    condiciones.Add(filtroTexto);
                }

                string rowFilter = string.Join(" AND ", condiciones);

                dv.RowFilter = rowFilter;
                dgvFacturas.DataSource = dv;
                dgvFacturas.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the txtBusqueda control.
        /// </summary>
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == placeholderText)
                return;

            FiltrarDatos();
        }

        /// <summary>
        /// Handles the Click event of the BtnNueva control.
        /// </summary>
        private async void BtnNueva_Click(object sender, EventArgs e)
        {
            using (ClienteAgregar frmCA = new ClienteAgregar())
            {
                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    await CargarFactura();

                    dtpInicio.Value = DateTime.Today;
                    dtpFin.Value = DateTime.Today;
                    FiltrarDatos();
                }
            }
            dgvFacturas.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the BtnVer control.
        /// </summary>
        private void BtnVer_Click(object sender, EventArgs e)
        {
            if (dgvFacturas.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Ninguna fila seleccionada",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvFacturas.CurrentRow != null)
                dgvFacturas_CellDoubleClick(null, null);
            dgvFacturas.ClearSelection();
        }

        /// <summary>
        /// Handles the CellDoubleClick event of the dgvFacturas control.
        /// Arma el FacturaDTO a partir de la fila seleccionada, en vez de
        /// pasar 7 parámetros sueltos al constructor de FacturaVer.
        /// </summary>
        private async void dgvFacturas_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvFacturas.CurrentRow != null)
            {
                int bateriaVieja = 0;
                var valorBateria = dgvFacturas.CurrentRow.Cells["Batería Vieja"].Value?.ToString();
                if (!string.IsNullOrEmpty(valorBateria) && valorBateria != "No dejó")
                {
                    string soloNumero = System.Text.RegularExpressions.Regex.Match(valorBateria, @"\d+").Value;
                    if (!string.IsNullOrEmpty(soloNumero))
                        bateriaVieja = int.Parse(soloNumero);
                }

                string valorCelda = dgvFacturas.CurrentRow.Cells["Rebaja"].Value?.ToString() ?? "0";
                valorCelda = valorCelda.Replace("L.", "").Trim();
                double rebaja = Convert.ToDouble(valorCelda);

                FacturaDTO facturaDTO = new FacturaDTO
                {
                    IdFactura = Convert.ToInt32(dgvFacturas.CurrentRow.Cells["Factura"].Value),
                    NombreCliente = dgvFacturas.CurrentRow.Cells["Cliente"].Value.ToString(),
                    Fecha = Convert.ToDateTime(dgvFacturas.CurrentRow.Cells["Fecha"].Value),
                    IdFormaPago = Convert.ToInt32(dgvFacturas.CurrentRow.Cells["ID Método de Pago"].Value),
                    Vendedor = dgvFacturas.CurrentRow.Cells["Vendedor"].Value.ToString(),
                    CantidadBateriaVieja = bateriaVieja,
                    RebajaBateria = rebaja
                };

                FacturaVer frmFV = new FacturaVer(facturaDTO);
                frmFV.ShowDialog();

                await CargarFactura();
                FiltrarDatos();
                dgvFacturas.ClearSelection();
            }
        }

        /// <summary>
        /// Handles the 1 event of the BtnRefrescar_Click control.
        /// </summary>
        private void BtnRefrescar_Click_1(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            FiltrarDatos();
            dgvFacturas.ClearSelection();
        }

        /// <summary>
        /// Handles the Click event of the btnnotificaciones control.
        /// </summary>
        private void btnnotificaciones_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin Noti = new NotificacionesAdmin();
            Noti.ShowDialog();
        }

        /// <summary>
        /// Handles the KeyPress event of the txtBusqueda control.
        /// </summary>
        private void txtBusqueda_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarBusquedaAlfanumerica(e);
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        private void btnClientes_Click(object sender, EventArgs e)
        {
            ClientesAdm CA = new ClientesAdm();
            CA.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            PA.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            Bi.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            DialogResult resultado = MessageBox.Show(
           "¿Está seguro que desea cerrar sesión?",
           "Confirmación",
           MessageBoxButtons.YesNo,
           MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }
    }
}