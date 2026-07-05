using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClientesEmp : Form
    {
        /// <summary>
        /// Datos de clientes
        /// </summary>
        DataTable datosCli;

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre, apellido, RTN o teléfono...";

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClientesEmp"/>.
        /// </summary>
        public ClientesEmp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;

            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasYNumeros(e);
            txtBusqueda.TextChanged += txtBusqueda_TextChanged;
        }

        /// <summary>
        /// Carga la tabla de clientes.
        /// </summary>
        private async Task TablaClientes()
        {
            ClsCliente objC = new ClsCliente();
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
        /// Aplica el filtro combinando estado y búsqueda de texto.
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
        /// Maneja el evento TextChanged del control txtBusqueda.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
            if (txtBusqueda.Text == placeholderTexto)
                return;

            AplicarFiltro();
        }

        /// <summary>
        /// Maneja el evento CheckedChanged del control chkActivo.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void chkActivo_CheckedChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvClientes.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="DataGridViewCellEventArgs"/> que contiene los datos del evento.</param>
        private async void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e != null && e.RowIndex < 0) return;

            if (dgvClientes.CurrentRow != null)
            {
                try
                {
                    int idCliente = Convert.ToInt32(dgvClientes.CurrentRow.Cells[0].Value);
                    string nombre = dgvClientes.CurrentRow.Cells[1].Value?.ToString() ?? "";
                    string apellido = dgvClientes.CurrentRow.Cells[2].Value?.ToString() ?? "";
                    string telefono = dgvClientes.CurrentRow.Cells[3].Value?.ToString() ?? "";
                    string rtn = dgvClientes.CurrentRow.Cells[4].Value?.ToString() ?? "";
                    int idEstado = Convert.ToInt32(dgvClientes.CurrentRow.Cells[5].Value);

                    ClienteDTO clienteDTO = new ClienteDTO
                    {
                        IdCliente = idCliente,
                        Nombre = nombre,
                        Apellido = apellido,
                        Telefono = telefono,
                        RTN = rtn,
                        IdEstado = idEstado
                    };

                    ClienteModificar frmMo = new ClienteModificar(clienteDTO);
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

        /// <summary>
        /// Maneja el evento Click del control BtnModificar.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

        /// <summary>
        /// Maneja el evento Load del formulario ClientesEmp.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void ClientesEmp_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBusqueda, placeholderTexto);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            btnClientes.Enabled = false;
            btnClientes.BackColor = Color.SkyBlue;
            btnClientes.ForeColor = Color.White;

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
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            new NotificacionesAdmin().Show();
        }

        /// <summary>
        /// Handles the Click event of the btnMenu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp ME = new MenuPrincipalEmp();
            ME.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnFacturas control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnInventario control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnDeudores control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
            this.Hide();
        }

        /// <summary>
        /// Handles the Click event of the btnCerrar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}