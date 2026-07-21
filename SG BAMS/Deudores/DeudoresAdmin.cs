using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Listado y gestión de pagos de deudores (perfil Administrador).
    /// Única responsabilidad: coordinar la grilla y la apertura del formulario de pago,
    /// delegando el acceso a datos en IDeudaRepository (inyectado) y la construcción del
    /// filtro en FiltroDeudoresService (ver auditoría SOLID, hallazgos DAD01-DAD05).
    /// </summary>
    public partial class DeudoresAdmin : Form
    {
        private readonly IDeudaRepository _deudaRepositorio;
        private readonly FiltroDeudoresService _filtroService = new FiltroDeudoresService();
        private readonly NavegacionService _navegacion = new NavegacionService();

        private DataTable dtDeudores;
        private bool isFiltering = false;
        private string placeholderTexto = "Buscar por nombre del cliente...";
        private Color placeholderColor = Color.Gray;
        private Color textoColor = Color.Black;

        /// <summary>
        /// Inicializa una nueva instancia de <see cref="DeudoresAdmin"/>, recibiendo su
        /// dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="deudaRepositorio">Acceso a datos del listado de deudores.</param>
        public DeudoresAdmin(IDeudaRepository deudaRepositorio)
        {
            InitializeComponent();
            _deudaRepositorio = deudaRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            CargarGridDeudores();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;

            txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            txtBuscarNombre.Leave += txtBuscarNombre_Leave;
        }

        private void ConfigurarPlaceholder(TextBox textBox, string placeholder)
        {
            placeholderTexto = placeholder;
            textoColor = textBox.ForeColor;
            placeholderColor = Color.Gray;

            textBox.Text = placeholderTexto;
            textBox.ForeColor = placeholderColor;
        }

        private void txtBuscarNombre_Enter(object sender, EventArgs e)
        {
            if (txtBuscarNombre.Text == placeholderTexto)
            {
                txtBuscarNombre.Text = "";
                txtBuscarNombre.ForeColor = textoColor;
            }
        }

        private void txtBuscarNombre_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscarNombre.Text))
            {
                txtBuscarNombre.Text = placeholderTexto;
                txtBuscarNombre.ForeColor = placeholderColor;
            }
        }

        public void CargarGridDeudores()
        {
            dtDeudores = _deudaRepositorio.ListarDeudores();

            dgvDeudores.DataSource = dtDeudores;
            dgvDeudores.ReadOnly = true;
            dgvDeudores.AllowUserToAddRows = false;
            dgvDeudores.AllowUserToDeleteRows = false;
            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDeudores.MultiSelect = false;

            FiltrarDeudores();
        }

        /// <summary>
        /// Aplica a la grilla el RowFilter construido por FiltroDeudoresService.
        /// La lógica de armado del filtro ya no vive en el formulario (ver hallazgo DAD02).
        /// </summary>
        private void FiltrarDeudores()
        {
            if (dtDeudores == null) return;

            try
            {
                string filtroNombre = txtBuscarNombre.Text?.Trim() ?? "";
                if (filtroNombre == placeholderTexto || txtBuscarNombre.ForeColor == placeholderColor)
                    filtroNombre = "";

                DataView dv = dtDeudores.DefaultView;
                dv.RowFilter = _filtroService.ConstruirRowFilter(filtroNombre);
                dgvDeudores.DataSource = dv;
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            if (isFiltering) return;
            isFiltering = true;
            FiltrarDeudores();
            isFiltering = false;
        }

        private void kryptonButton12_Click(object sender, EventArgs e) => FiltrarDeudores();

        /// <summary>
        /// Decide si corresponde abrir el formulario de pago para la fila seleccionada.
        /// </summary>
        private void ProcesarPagoDeuda(DataRowView fila)
        {
            if (fila == null) return;

            int idDeuda = Convert.ToInt32(fila["ID Deuda"]);
            string nombreCliente = fila["Cliente"].ToString().Trim();
            string estadoDeuda = fila["Estado Deuda"].ToString().Trim();

            if (!ReglaPagoDeudaService.PuedeRegistrarPago(estadoDeuda))
            {
                MessageBox.Show($"La deuda de {nombreCliente} ya no está activa.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            Pago_Deuda pagDe = new Pago_Deuda(new DeudasRepository(), nombreCliente, idDeuda);
            if (pagDe.ShowDialog() == DialogResult.OK)
            {
                CargarGridDeudores();
                txtBuscarNombre.Clear();
            }
        }

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            try
            {
                DataRowView filaSeleccionada = (DataRowView)dgvDeudores.Rows[e.RowIndex].DataBoundItem;
                ProcesarPagoDeuda(filaSeleccionada);
                dgvDeudores.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al intentar procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una fila para pagar la deuda.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                DataRowView filaSeleccionada = dgvDeudores.CurrentRow.DataBoundItem as DataRowView;
                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                    dgvDeudores.ClearSelection();
                }
                else
                {
                    MessageBox.Show("No se pudo obtener la información de la fila seleccionada.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e) => Ayudante_UI.AplicarZoomGlobal(this);

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e) => ClsValidaciones.PermitirSoloLetras(e);

        private void button12_Click(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void kryptonDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvDeudores_DoubleClick(object sender, EventArgs e) { }

        private void DeudoresAdmin_Load(object sender, EventArgs e)
        {
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

            EstiloDataGridView.Aplicar(dgvDeudores);

            ClsMensajeGuia.Activar(txtBuscarNombre);
        }

        private void btnMenu_Click(object sender, EventArgs e) => _navegacion.IrA(this, new MenuPrincipalAdm());
        private void btnFacturas_Click(object sender, EventArgs e) => _navegacion.IrA(this, new FacturasAdm());
        private void btnCompra_Click(object sender, EventArgs e) => _navegacion.IrA(this, new Compras());
        private void btnClientes_Click(object sender, EventArgs e) => _navegacion.IrA(this, new ClientesAdm(new Cliente.ClienteRepository()));
        private void btnInventario_Click(object sender, EventArgs e) => _navegacion.IrA(this, new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()));
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            _navegacion.IrA(this, PA);
        }
        private void btnReportes_Click(object sender, EventArgs e) => _navegacion.IrA(this, new ReportesAdmin());
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
               new BitacoraRepository(),
               new FiltroBitacoraService(),
               new ReporteBitacoraPdfExportador());
            _navegacion.IrA(this, Bi);
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("¿Está seguro que desea cerrar sesión?", "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Login.Login login = new Login.Login();
                login.Show();
                this.Close();
            }
        }

        private void btnPerfil_Click(object sender, EventArgs e) { Perfil perfil = new Perfil(); perfil.ShowDialog(); }
    }
}
