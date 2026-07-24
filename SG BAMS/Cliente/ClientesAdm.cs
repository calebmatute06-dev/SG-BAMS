using SG_BAMS.Bitacora;
using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario de administración de clientes. Permite visualizar, buscar,
    /// filtrar y modificar los clientes registrados en el sistema, así como
    /// navegar hacia otros módulos del panel administrativo.
    /// Única responsabilidad: coordinar la grilla y la navegación; el acceso a datos
    /// se recibe por inyección y la construcción del filtro se delega en
    /// FiltroClientesService (ver auditoría SOLID, hallazgos CAD01-CAD07).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClientesAdm : Form
    {
        private readonly IClienteRepository clienteRepositorio;
        private readonly FiltroClientesService filtroService = new FiltroClientesService();
        private readonly NavegacionService navegacion = new NavegacionService();

        /// <summary>
        /// Almacena los datos de la tabla de clientes obtenidos desde la base de datos,
        /// utilizados como fuente para el filtrado y visualización en el DataGridView.
        /// </summary>
        DataTable datosCli;

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre, apellido, RTN o teléfono...";

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClientesAdm"/>,
        /// recibiendo su dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="clienteRepositorio">Acceso a datos de clientes.</param>
        public ClientesAdm(IClienteRepository clienteRepositorio)
        {
            InitializeComponent();
            this.clienteRepositorio = clienteRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;

            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasYNumeros(e);
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
        }

        /// <summary>
        /// Carga de forma asíncrona los datos de clientes desde la base de datos
        /// y aplica el filtro activo. La configuración de columnas se hace aparte,
        /// en <see cref="ConfigurarColumnas"/> (ver hallazgo CAD03).
        /// </summary>
        private async Task TablaClientes()
        {
            try
            {
                datosCli = await clienteRepositorio.VerClienteTabla();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar clientes: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (datosCli == null) return;

            dgvClientes.DataSource = datosCli;
            ConfigurarColumnas();
            AplicarFiltro();

            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.AllowUserToAddRows = false;
            dgvClientes.ReadOnly = true;
            dgvClientes.ClearSelection();
        }

        /// <summary>
        /// Configura los encabezados y visibilidad de columnas del DataGridView.
        /// </summary>
        private void ConfigurarColumnas()
        {
            dgvClientes.Columns["ID"].HeaderText = "ID Cliente";
            dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
            dgvClientes.Columns["Apellido"].HeaderText = "Apellido";
            dgvClientes.Columns["Teléfono"].HeaderText = "Teléfono";
            dgvClientes.Columns["RTN"].HeaderText = "RTN";
            dgvClientes.Columns["Estado"].HeaderText = "Estado";

            if (dgvClientes.Columns.Contains("ID Estado"))
                dgvClientes.Columns["ID Estado"].Visible = false;
        }

        /// <summary>
        /// Aplica a la grilla el RowFilter construido por FiltroClientesService
        /// (ver hallazgo CAD01).
        /// </summary>
        private void AplicarFiltro()
        {
            if (datosCli == null) return;

            try
            {
                string textoBusqueda = txtBusqueda.Text?.Trim() ?? "";
                if (textoBusqueda == placeholderTexto)
                    textoBusqueda = "";

                DataView dv = datosCli.DefaultView;
                dv.RowFilter = filtroService.ConstruirRowFilter(textoBusqueda, chkActivo.Checked);
                dgvClientes.DataSource = dv;
                dgvClientes.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al aplicar filtro: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClientesAdm</c>.
        /// </summary>
        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, placeholderTexto);

            btnClientes.Enabled = false;
            btnClientes.BackColor = Color.SkyBlue;
            btnClientes.ForeColor = Color.White;
            AdaptadorPantallaCompleta.Habilitar(this);

            await TablaClientes();

            EstiloDataGridView.Aplicar(dgvClientes);
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ClearSelection();

            this.ActiveControl = null;
        }

        /// <summary>
        /// Arma un ClienteDTO a partir de la fila actualmente indicada, leyendo por
        /// nombre de columna en vez de por índice fijo (ver hallazgo CAD05).
        /// </summary>
        private ClienteDTO ArmarClienteDTODesdeFila(DataGridViewRow fila)
        {
            return new ClienteDTO
            {
                IdCliente = Convert.ToInt32(fila.Cells["ID"].Value),
                Nombre = fila.Cells["Nombre"].Value?.ToString() ?? "",
                Apellido = fila.Cells["Apellido"].Value?.ToString() ?? "",
                Telefono = fila.Cells["Teléfono"].Value?.ToString() ?? "",
                RTN = fila.Cells["RTN"].Value?.ToString() ?? "",
                IdEstado = Convert.ToInt32(fila.Cells["ID Estado"].Value)
            };
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del DataGridView <c>dgvClientes</c>.
        /// </summary>
        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    ClienteDTO clienteDTO = ArmarClienteDTODesdeFila(dgvClientes.CurrentRow);
                    ClienteModificar frmMo = new ClienteModificar(clienteRepositorio, clienteDTO);

                    if (frmMo.ShowDialog() == DialogResult.OK || frmMo.DialogResult == DialogResult.Cancel)
                    {
                        await TablaClientes();
                    }
                    dgvClientes.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al procesar la selección: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnModificar</c>.
        /// </summary>
        private void btnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvClientes_CellDoubleClick(null, null);
            dgvClientes.ClearSelection();
        }

        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == placeholderTexto)
                return;

            AplicarFiltro();
        }

        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().ShowDialog();

        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        private void btnMenu_Click(object sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalAdm());
        private void btnFacturas_Click(object sender, EventArgs e) => navegacion.IrA(this, new FacturasAdm());
        private void btnCompra_Click(object sender, EventArgs e) => navegacion.IrA(this, new Compras());
        private void btnInventario_Click(object sender, EventArgs e) => navegacion.IrA(this, new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()));

        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            navegacion.IrA(this, PA);
        }

        private void btnDeudores_Click(object sender, EventArgs e) => navegacion.IrA(this, new DeudoresAdmin(new DeudaRepository()));
        private void btnReportes_Click(object sender, EventArgs e) => navegacion.IrA(this, new ReportesAdmin());

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            var Bi = new BitacoraAdmin(
                new BitacoraRepository(),
                new FiltroBitacoraService(),
                new ReporteBitacoraPdfExportador());
            navegacion.IrA(this, Bi);
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