using Microsoft.Data.SqlClient;
using SG_BAMS.Bitacora;
using SG_BAMS.Cliente;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario de administración de clientes. Permite visualizar, buscar,
    /// filtrar y modificar los clientes registrados en el sistema, así como
    /// navegar hacia otros módulos del panel administrativo.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClientesAdm : Form
    {
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
        /// Inicializa una nueva instancia de la clase <see cref="ClientesAdm"/>.
        /// Configura el modo de selección del DataGridView y las validaciones
        /// de entrada del campo de búsqueda.
        /// </summary>
        public ClientesAdm()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;

            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasYNumeros(e);
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
        }

        /// <summary>
        /// Carga de forma asíncrona los datos de clientes desde la base de datos,
        /// configura los encabezados de columnas del DataGridView, oculta columnas
        /// internas y aplica el filtro activo.
        /// </summary>
        private async Task TablaClientes()
        {
            ClsVerCliente objC = new ClsVerCliente();
            datosCli = await objC.VerClienteTabla();

            if (datosCli != null)
            {
                dgvClientes.DataSource = datosCli;

                dgvClientes.Columns["ID"].HeaderText = "ID Cliente";
                dgvClientes.Columns["Nombre"].HeaderText = "Nombre";
                dgvClientes.Columns["Apellido"].HeaderText = "Apellido";
                dgvClientes.Columns["Teléfono"].HeaderText = "Teléfono";
                dgvClientes.Columns["RTN"].HeaderText = "RTN";
                dgvClientes.Columns["Estado"].HeaderText = "Estado";

                if (dgvClientes.Columns.Contains("ID Estado"))
                    dgvClientes.Columns["ID Estado"].Visible = false;

                AplicarFiltro();

                dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dgvClientes.AllowUserToAddRows = false;
                dgvClientes.ReadOnly = true;
                dgvClientes.ClearSelection();
            }
        }

        /// <summary>
        /// Aplica un filtro combinado al DataGridView según el texto ingresado
        /// en el campo de búsqueda y el estado del checkbox de activos/inactivos.
        /// Filtra por nombre, apellido, RTN y teléfono del cliente.
        /// </summary>
        private void AplicarFiltro()
        {
            if (datosCli == null) return;

            try
            {
                DataView dv = datosCli.DefaultView;

                var condiciones = new List<string>();

                string filtroEstado = chkActivo.Checked ? "Estado <> 'Activo'" : "Estado = 'Activo'";
                condiciones.Add($"({filtroEstado})");

                string textoBusqueda = txtBusqueda.Text?.Trim() ?? "";
                if (textoBusqueda == placeholderTexto)
                {
                    textoBusqueda = "";
                }

                if (!string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    string textoSeguro = textoBusqueda
                        .Replace("'", "''")
                        .Replace("[", "[[]")
                        .Replace("]", "[]]")
                        .Replace("*", "[*]")
                        .Replace("%", "[%]");

                    string filtroTexto = $"(Nombre LIKE '%{textoSeguro}%' OR " +
                                         $"Apellido LIKE '%{textoSeguro}%' OR " +
                                         $"RTN LIKE '%{textoSeguro}%' OR " +
                                         $"Teléfono LIKE '%{textoSeguro}%')";

                    condiciones.Add(filtroTexto);
                }

                string rowFilter = string.Join(" AND ", condiciones);

                dv.RowFilter = rowFilter;
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
        /// Carga la tabla de clientes y aplica el estilo visual del DataGridView,
        /// incluyendo colores, fuentes, bordes y altura de filas.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void ClientesAdm_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, placeholderTexto);

            btnClientes.Enabled = false;
            btnClientes.BackColor = Color.SkyBlue;
            btnClientes.ForeColor = Color.White;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            await TablaClientes();
            dgvClientes.BorderStyle = BorderStyle.None;
            dgvClientes.BackgroundColor = Color.White;
            dgvClientes.RowHeadersVisible = false;
            dgvClientes.EnableHeadersVisualStyles = false;
            dgvClientes.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvClientes.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvClientes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvClientes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvClientes.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvClientes.ColumnHeadersHeight = 28;

            dgvClientes.DefaultCellStyle.BackColor = Color.White;
            dgvClientes.DefaultCellStyle.ForeColor = Color.Navy;
            dgvClientes.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvClientes.DefaultCellStyle.Padding = new Padding(3);
            dgvClientes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvClientes.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvClientes.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvClientes.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvClientes.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvClientes.GridColor = Color.LightGray;
            dgvClientes.RowTemplate.Height = 32;
            dgvClientes.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvClientes.ClearSelection();

            this.ActiveControl = null;
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del DataGridView <c>dgvClientes</c>.
        /// Obtiene los datos del cliente en la fila seleccionada y abre el formulario
        /// de modificación. Recarga la tabla al cerrar el formulario.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento de celda del DataGridView.</param>
        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                    string nombreCliente = dgvClientes.CurrentRow.Cells[1].Value?.ToString() ?? "";
                    string apellidoCliente = dgvClientes.CurrentRow.Cells[2].Value?.ToString() ?? "";
                    string telefonoCliente = dgvClientes.CurrentRow.Cells[3].Value?.ToString() ?? "";
                    string rtnCliente = dgvClientes.CurrentRow.Cells[4].Value?.ToString() ?? "";
                    int idEstado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[5].Value);

                    ClienteModificar frmMo = new ClienteModificar(idCliente, nombreCliente, apellidoCliente, telefonoCliente, rtnCliente, idEstado);

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
        /// Verifica que haya una fila seleccionada en el DataGridView y redirige
        /// al evento de doble clic para abrir el formulario de modificación.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
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

        /// <summary>
        /// Maneja el evento TextChanged del campo <c>txtBusqueda</c>.
        /// Aplica el filtro de búsqueda en tiempo real al modificar el texto ingresado.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == placeholderTexto)
                return;

            AplicarFiltro();
        }

        /// <summary>
        /// Maneja el evento CheckedChanged del control <c>chkActivo</c>.
        /// Actualiza el filtro del DataGridView para mostrar clientes
        /// activos o inactivos según el estado del checkbox.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnNoti</c>.
        /// Abre el formulario de notificaciones del administrador.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnNoti_Click(object sender, EventArgs e) => new NotificacionesAdmin().Show();

        /// <summary>
        /// Maneja el evento CellContentClick del DataGridView <c>dgvClientes</c>.
        /// Reservado para uso futuro; no realiza ninguna acción actualmente.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento de celda del DataGridView.</param>
        private void dgvClientes_CellContentClick(object sender, DataGridViewCellEventArgs e) { }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnMenu</c>.
        /// Navega al menú principal del administrador y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalAdm MPA = new MenuPrincipalAdm();
            MPA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnFacturas</c>.
        /// Navega al módulo de administración de facturas y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasAdm FA = new FacturasAdm();
            FA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnCompra</c>.
        /// Navega al módulo de compras y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnCompra_Click(object sender, EventArgs e)
        {
            Compras CF = new Compras();
            CF.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnInventario</c>.
        /// Navega al módulo de inventario y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioAdmin IA = new InventarioAdmin();
            IA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnProveedores</c>.
        /// Navega al módulo de proveedores y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            ProveedoresAdmin PA = new ProveedoresAdmin();
            PA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnDeudores</c>.
        /// Navega al módulo de deudores y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            DeudoresAdmin DA = new DeudoresAdmin();
            DA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnReportes</c>.
        /// Navega al módulo de reportes y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnReportes_Click(object sender, EventArgs e)
        {
            ReportesAdmin RA = new ReportesAdmin();
            RA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnBitacora</c>.
        /// Navega al módulo de bitácora y oculta el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnBitacora_Click(object sender, EventArgs e)
        {
            BitacoraAdmin BA = new BitacoraAdmin();
            BA.Show();
            this.Hide();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnCerrar</c>.
        /// Cierra la sesión actual, muestra el formulario de inicio de sesión
        /// y cierra el formulario actual.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
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
        /// Maneja el evento Click del botón <c>btnPerfil</c>.
        /// Abre el formulario de perfil del usuario administrador.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}