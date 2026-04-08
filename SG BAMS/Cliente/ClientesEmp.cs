using SG_BAMS.Cliente;
using System.Data;

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
        /// Inicializa una nueva instancia de la clase <see cref="ClientesEmp"/>.
        /// </summary>
        public ClientesEmp()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            dgvClientes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvClientes.MultiSelect = false;


            txtBusqueda.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetrasYNumeros(e);
        }

        /// <summary>
        /// Carga la tabla de clientes.
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
        /// Aplica el filtro.
        /// </summary>
        private void AplicarFiltro()
        {
            if (datosCli == null) return;

            DataView dv = datosCli.DefaultView;
            string filtroEstado = chkActivo.Checked ? "Estado <> 'Activo'" : "Estado = 'Activo'";

            if (string.IsNullOrWhiteSpace(txtBusqueda.Text))
            {
                dv.RowFilter = filtroEstado;
            }
            else
            {

                string textoSeguro = txtBusqueda.Text
                    .Replace("'", "''")
                    .Replace("[", "[[]")
                    .Replace("]", "[]]")
                    .Replace("*", "[*]")
                    .Replace("%", "[%]");


                dv.RowFilter = string.Format(
                    "({0}) AND (Nombre LIKE '%{1}%' OR Apellido LIKE '%{1}%' OR RTN LIKE '%{1}%' OR Teléfono LIKE '%{1}%')",
                    filtroEstado, textoSeguro);
            }

            dgvClientes.DataSource = dv;
            dgvClientes.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBusqueda.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void txtBusqueda_TextChanged(object sender, EventArgs e)
        {
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

                    ClienteModificar frmMo = new ClienteModificar(idCliente, nombre, apellido, telefono, rtn, idEstado);
                    frmMo.ShowDialog();

                    await TablaClientes();
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
        }

        /// <summary>
        /// Maneja el evento Load del formulario ClientesEmp.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void ClientesEmp_Load(object sender, EventArgs e)
        {

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

        private void btnMenu_Click(object sender, EventArgs e)
        {
            MenuPrincipalEmp ME = new MenuPrincipalEmp();
            ME.Show();
            this.Hide();
        }

        private void btnFacturas_Click(object sender, EventArgs e)
        {
            FacturasEmp FE = new FacturasEmp();
            FE.Show();
            this.Hide();
        }



        private void btnInventario_Click(object sender, EventArgs e)
        {
            InventarioEmp IE = new InventarioEmp();
            IE.Show();
            this.Hide();
        }

        private void btnDeudores_Click(object sender, EventArgs e)
        {
            Deudores_Emp DE = new Deudores_Emp();
            DE.Show();
            this.Hide();
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            Login.Login login = new Login.Login();
            login.Show();
            this.Close();
        }

        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.Show();
        }
    }
}