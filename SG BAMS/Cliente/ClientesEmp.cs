using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario de listado y modificación de clientes (perfil Empleado).
    /// Única responsabilidad: coordinar la grilla y la navegación; el acceso a datos
    /// se recibe por inyección y la construcción del filtro se delega en
    /// FiltroClientesService (ver auditoría SOLID, hallazgos CAD01-CAD07, análogos en este formulario).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClientesEmp : Form
    {
        private readonly IClienteRepository clienteRepositorio;
        private readonly FiltroClientesService filtroService = new FiltroClientesService();
        private readonly NavegacionService navegacion = new NavegacionService();

        /// <summary>
        /// Datos de clientes
        /// </summary>
        DataTable datosCli;

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre, apellido, RTN o teléfono...";

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClientesEmp"/>,
        /// recibiendo su dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="clienteRepositorio">Acceso a datos de clientes.</param>
        public ClientesEmp(IClienteRepository clienteRepositorio)
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
        /// Carga la tabla de clientes. La configuración de columnas se hace aparte,
        /// en <see cref="ConfigurarColumnas"/>.
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
        /// Aplica el filtro combinando estado y búsqueda de texto, delegado en
        /// FiltroClientesService.
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

        /// <summary>
        /// Arma un ClienteDTO a partir de la fila actualmente indicada, leyendo por
        /// nombre de columna en vez de por índice fijo.
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

        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    ClienteDTO clienteDTO = ArmarClienteDTODesdeFila(dgvClientes.CurrentRow);

                    ClienteModificar frmMo = new ClienteModificar(clienteRepositorio, clienteDTO);
                    frmMo.ShowDialog();

                    await TablaClientes();
                    dgvClientes.ClearSelection();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar datos del cliente: " + ex.Message);
                }
            }
        }

        private void BtnModificar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Debe seleccionar una fila", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            dgvClientes_CellDoubleClick(null, null);
            dgvClientes.ClearSelection();
        }

        private async void ClientesEmp_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, placeholderTexto);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            btnClientes.Enabled = false;
            btnClientes.BackColor = Color.SkyBlue;
            btnClientes.ForeColor = Color.White;

            await TablaClientes();

            EstiloDataGridView.Aplicar(dgvClientes);
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ClearSelection();

            this.ActiveControl = null;
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalEmp());
        private void btnFacturas_Click(object sender, EventArgs e) => navegacion.IrA(this, new FacturasEmp());
        private void btnInventario_Click(object sender, EventArgs e) => navegacion.IrA(this, new InventarioEmp(new ProductoInventario.ProductoRepository()));
        private void btnDeudores_Click(object sender, EventArgs e) => navegacion.IrA(this, new Deudores_Emp(new DeudaRepository()));

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