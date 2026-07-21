using SG_BAMS.Facturas;
using SG_BAMS.Facturas.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;

namespace SG_BAMS
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacturasEmp : Form
    {
        /// <summary>
        /// The datos fac
        /// </summary>
        private DataTable datosFac;
        private readonly ClsDetalleFactura detalleFactura;
        private readonly FiltroFacturasService filtroService = new FiltroFacturasService();
        private readonly NavegacionService navegacion = new NavegacionService();

        /// <summary>
        /// Texto del placeholder para evitar filtrarlo
        /// </summary>
        private const string PlaceholderText = "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN";

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturasEmp"/> class.
        /// </summary>

        public FacturasEmp() : this(new ClsDetalleFactura())
        {
        }

        internal FacturasEmp(ClsDetalleFactura detalleFactura)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.detalleFactura = detalleFactura;
            ConfigurarGrid();
        }

        /// <summary>
        /// Configurars the grid.
        /// </summary>
        private void ConfigurarGrid()
        {
            dgvFacturas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvFacturas.MultiSelect = false;
            dgvFacturas.AllowUserToAddRows = false;
            dgvFacturas.ReadOnly = true;
            dgvFacturas.AllowUserToOrderColumns = false;
            dgvFacturas.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.ValidarBusquedaAlfanumerica(e);
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
        }

        /// <summary>
        /// Cargars the factura.
        /// </summary>
        private async Task CargarFactura()
        {
            try
            {
                datosFac = await detalleFactura.VerFacturas();


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
        /// Handles the Load event of the FacturasEmp control.
        /// </summary>
        private async void FacturasEmp_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, PlaceholderText);
            btnFacturas.Enabled = false;
            btnFacturas.BackColor = Color.SkyBlue;
            btnFacturas.ForeColor = Color.White;

            await CargarFactura();

            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;

            ClsValidaciones.ValidarRangoFechas(dtpInicio, dtpFin);

            dtpInicio.ValueChanged += (s, ev) => ValidarYFiltrar();
            dtpFin.ValueChanged += (s, ev) => ValidarYFiltrar();

            EstiloDataGridView.Aplicar(dgvFacturas);

            FiltrarDatos();
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
        /// Filtrars the datos.
        /// Si hay texto de búsqueda, ignora el filtro de fechas y busca en todos los registros.
        /// Si no hay texto, aplica solo el filtro de fechas.
        /// </summary>
        private void FiltrarDatos()
        {
            if (datosFac == null) return;

            try
            {
                string texto = txtBusqueda.Text?.Trim() ?? "";
                if (texto == PlaceholderText) texto = "";

                DataView dv = filtroService.Filtrar(datosFac, texto, dtpInicio.Value, dtpFin.Value);

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
            if (txtBusqueda.Text == PlaceholderText)
                return;

            FiltrarDatos();
        }

        /// <summary>
        /// Handles the Click event of the BtnNueva control.
        /// </summary>
        private async void BtnNueva_Click(object sender, EventArgs e)
        {
            using (ClienteAgregar frmCA = new ClienteAgregar(new Cliente.ClienteRepository()))
            {
                if (frmCA.ShowDialog() == DialogResult.OK)
                {
                    await CargarFactura();

                    dtpInicio.Value = DateTime.Today;
                    dtpFin.Value = DateTime.Today;
                    FiltrarDatos();

                    dgvFacturas.ClearSelection();
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
        /// Handles the Click event of the BtnRefrescar control.
        /// </summary>
        private void BtnRefrescar_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            dtpInicio.Value = DateTime.Today;
            dtpFin.Value = DateTime.Today;
            FiltrarDatos();
            dgvFacturas.ClearSelection();
        }

        

        /// <summary>
        /// Handles the Click event of the BtnNotificaciones control.
        /// </summary>
        private void BtnNotificaciones_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalEmp());

        private void btnClientes_Click(object sender, EventArgs e) => navegacion.IrA(this, new ClientesEmp(new Cliente.ClienteRepository()));

        private void btnInventario_Click(object sender, EventArgs e) => navegacion.IrA(this, new InventarioEmp(new ProductoInventario.ProductoRepository()));

        private void btnDeudores_Click(object sender, EventArgs e) => navegacion.IrA(this, new Deudores_Emp(new DeudaRepository()));

        /// <summary>
        /// Handles the Click event of the btnCerrar control.
        /// </summary>
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

        /// <summary>
        /// Handles the Click event of the btnPerfil control.
        /// </summary>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }
    }
}