using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Listado y gestión de pagos de deudores (perfil Empleado).
    /// Única responsabilidad: coordinar la grilla y la apertura del formulario de pago,
    /// delegando el acceso a datos en IDeudaRepository (inyectado) y la construcción del
    /// filtro en FiltroDeudoresService (ver auditoría SOLID, hallazgos DEM01-DEM02).
    /// </summary>
    public partial class Deudores_Emp : Form
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
        /// Inicializa una nueva instancia de <see cref="Deudores_Emp"/>, recibiendo su
        /// dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="deudaRepositorio">Acceso a datos del listado de deudores.</param>
        public Deudores_Emp(IDeudaRepository deudaRepositorio)
        {
            InitializeComponent();
            _deudaRepositorio = deudaRepositorio;

            CargarGridDeudores();
            this.StartPosition = FormStartPosition.CenterScreen;

            dgvDeudores.CellDoubleClick += dgvDeudores_CellDoubleClick;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            this.txtBuscarNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            this.txtBuscarNombre.TextChanged += txtBuscarNombre_TextChanged;

            this.txtBuscarNombre.Enter += txtBuscarNombre_Enter;
            this.txtBuscarNombre.Leave += txtBuscarNombre_Leave;

            dgvDeudores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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
            dgvDeudores.MultiSelect = false;

            FiltrarDeudores();
        }

        private void txtBuscarNombre_TextChanged(object sender, EventArgs e)
        {
            if (isFiltering) return;
            isFiltering = true;
            FiltrarDeudores();
            isFiltering = false;
        }

        /// <summary>
        /// Aplica a la grilla el RowFilter construido por FiltroDeudoresService.
        /// La lógica de armado del filtro ya no vive en el formulario (ver hallazgo DEM01).
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
                dgvDeudores.CurrentCell = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al filtrar: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

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

        private void kryptonButton15_Click(object sender, EventArgs e)
        {
            if (dgvDeudores.SelectedRows.Count == 0 || dgvDeudores.CurrentRow == null)
            {
                MessageBox.Show("Por favor, seleccione una fila.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dgvDeudores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                DataRowView filaSeleccionada = null;

                if (dgvDeudores.DataSource is DataView dv)
                {
                    if (e.RowIndex < dv.Count)
                        filaSeleccionada = dv[e.RowIndex];
                }
                else if (dgvDeudores.DataSource is DataTable dt)
                {
                    if (e.RowIndex < dt.Rows.Count)
                        filaSeleccionada = dt.DefaultView[e.RowIndex];
                }
                else if (dgvDeudores.Rows[e.RowIndex].DataBoundItem is DataRowView drv)
                {
                    filaSeleccionada = drv;
                }

                if (filaSeleccionada != null)
                {
                    ProcesarPagoDeuda(filaSeleccionada);
                    dgvDeudores.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al procesar el pago: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Deudores_Shown(object sender, EventArgs e)
        {
            Ayudante_UI.AplicarZoomGlobal(this);
            CargarGridDeudores();
            dgvDeudores.ClearSelection();
            dgvDeudores.CurrentCell = null;
        }

        private void txtBuscarNombre_KeyPress(object sender, KeyPressEventArgs e) => ClsValidaciones.PermitirSoloLetras(e);

        private void btnNoti(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void timer1_Tick(object sender, EventArgs e) { }

        private void Deudores_Emp_Load(object sender, EventArgs e)
        {
            dgvDeudores.ClearSelection();
            ConfigurarPlaceholder(txtBuscarNombre, "Buscar por nombre del cliente...");

            btnDeudores.Enabled = false;
            btnDeudores.BackColor = Color.SkyBlue;
            btnDeudores.ForeColor = Color.White;

            EstiloDataGridView.Aplicar(dgvDeudores);

            ClsMensajeGuia.Activar(txtBuscarNombre);
        }

        private void btnMenu_Click(object sender, EventArgs e) => _navegacion.IrA(this, new MenuPrincipalEmp());
        private void btnFacturas_Click(object sender, EventArgs e) => _navegacion.IrA(this, new FacturasEmp());
        private void btnClientes_Click(object sender, EventArgs e) => _navegacion.IrA(this, new ClientesEmp(new Cliente.ClienteRepository()));
        private void btnInventario_Click(object sender, EventArgs e) => _navegacion.IrA(this, new InventarioEmp(new ProductoInventario.ProductoRepository()));

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
