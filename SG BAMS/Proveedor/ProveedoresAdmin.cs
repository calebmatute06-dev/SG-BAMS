using SG_BAMS.Bitacora;
using SG_BAMS.Proveedor.DTO;
using SG_BAMS.Reporte;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS.Proveedor
{
    /// <summary>
    /// Formulario de listado y administración de proveedores. Única responsabilidad:
    /// mostrar la grilla y coordinar la apertura de los formularios de alta/edición,
    /// delegando toda la lógica de datos en IProveedorRepository, IEstadoRepository
    /// e IClasificacionRepository.
    /// </summary>
    public partial class ProveedoresAdmin : Form
    {
        private readonly IProveedorRepository _repositorio;
        private readonly IEstadoRepository _estadoRepositorio;
        private readonly IClasificacionRepository _clasificacionRepositorio;

        /// <summary>
        /// Crea el formulario recibiendo sus dependencias por inyección.
        /// </summary>
        public ProveedoresAdmin(IProveedorRepository repositorio,
                                 IEstadoRepository estadoRepositorio,
                                 IClasificacionRepository clasificacionRepositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            _estadoRepositorio = estadoRepositorio;
            _clasificacionRepositorio = clasificacionRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtBuscar.KeyPress += (s, e) =>
            {
                if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar) &&
                    !char.IsControl(e.KeyChar) && e.KeyChar != '&')
                {
                    e.Handled = true;
                }
            };
            // La carga de datos ocurre solo en el evento Load (ver ProveedoresAdmin_Load).
        }

        private void ProveedoresAdmin_Load(object sender, EventArgs e)
        {
            new PlaceholderTextBox(txtBuscar, "Ingrese un Nombre de Vendedor, Cliente, N.Factura, RTN");
            btnProveedores.Enabled = false;
            btnProveedores.BackColor = Color.SkyBlue;
            btnProveedores.ForeColor = Color.White;

            ConfigurarGrilla();
            CargarDatos();

            ClsMensajeGuia.ActivarK(txtBuscar);
        }

        /// <summary>
        /// Aplica el estilo visual y la configuración de columnas de la grilla.
        /// </summary>
        private void ConfigurarGrilla()
        {
            dgvProveedor.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProveedor.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProveedor.AllowUserToAddRows = false;
            dgvProveedor.ReadOnly = true;

            dgvProveedor.BorderStyle = BorderStyle.None;
            dgvProveedor.BackgroundColor = Color.White;
            dgvProveedor.RowHeadersVisible = false;
            dgvProveedor.EnableHeadersVisualStyles = false;
            dgvProveedor.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProveedor.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProveedor.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProveedor.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProveedor.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProveedor.ColumnHeadersHeight = 28;

            dgvProveedor.DefaultCellStyle.BackColor = Color.White;
            dgvProveedor.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProveedor.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProveedor.DefaultCellStyle.Padding = new Padding(3);
            dgvProveedor.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProveedor.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;
            dgvProveedor.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProveedor.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProveedor.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProveedor.GridColor = Color.LightGray;
            dgvProveedor.RowTemplate.Height = 32;
        }

        /// <summary>
        /// Carga los proveedores en la grilla y oculta las columnas técnicas (IDs).
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                dgvProveedor.DataSource = _repositorio.ObtenerProveedores();

                if (dgvProveedor.Columns.Contains("idProveedor"))
                    dgvProveedor.Columns["idProveedor"].Visible = false;
                if (dgvProveedor.Columns.Contains("idClasificacion"))
                    dgvProveedor.Columns["idClasificacion"].Visible = false;
                if (dgvProveedor.Columns.Contains("idEstado"))
                    dgvProveedor.Columns["idEstado"].Visible = false;

                dgvProveedor.ClearSelection();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtBuscar_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                dgvProveedor.DataSource = _repositorio.Buscar(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnAgregar1_Click(object sender, EventArgs e)
        {
            AgregarProveedores agregar = new AgregarProveedores(_repositorio, _clasificacionRepositorio);
            agregar.ShowDialog();
            CargarDatos();
            dgvProveedor.ClearSelection();
        }

        private void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (dgvProveedor.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            AbrirModificarProveedor(dgvProveedor.CurrentRow);
            dgvProveedor.ClearSelection();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarDatos();
        }

        private void dgvProveedor_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
                AbrirModificarProveedor(dgvProveedor.Rows[e.RowIndex]);
        }

        /// <summary>
        /// Arma el ProveedorDTO a partir de la fila seleccionada y abre
        /// la pantalla de modificación, refrescando la grilla al cerrarse.
        /// </summary>
        private void AbrirModificarProveedor(DataGridViewRow fila)
        {
            if (fila == null)
            {
                MessageBox.Show("Seleccione un proveedor.");
                return;
            }

            ProveedorDTO dto = new ProveedorDTO
            {
                IdProveedor = Convert.ToInt32(fila.Cells["idProveedor"].Value),
                Nombre = fila.Cells["Nombre"].Value.ToString(),
                Contacto = fila.Cells["Contacto"].Value.ToString(),
                Direccion = fila.Cells["Dirección"].Value.ToString(),
                Rtn = fila.Cells["RTN"].Value.ToString(),
                IdEstado = Convert.ToInt32(fila.Cells["idEstado"].Value),
                IdClasificacion = Convert.ToInt32(fila.Cells["idClasificacion"].Value)
            };

            ModificarProveedor frm = new ModificarProveedor(dto, _repositorio, _estadoRepositorio, _clasificacionRepositorio);
            frm.ShowDialog();
            CargarDatos();
        }

        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.ShowDialog();
        }

        private void btnMenu_Click(object sender, EventArgs e) { MenuPrincipalAdm MPA = new MenuPrincipalAdm(); MPA.Show(); this.Hide(); }
        private void btnFacturas_Click(object sender, EventArgs e) { FacturasAdm FA = new FacturasAdm(); FA.Show(); this.Hide(); }
        private void btnCompra_Click(object sender, EventArgs e) { Compras CF = new Compras(); CF.Show(); this.Hide(); }
        private void btnClientes_Click(object sender, EventArgs e) { ClientesAdm CA = new ClientesAdm(); CA.Show(); this.Hide(); }
        private void btnInventario_Click(object sender, EventArgs e) { InventarioAdmin IA = new InventarioAdmin(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository()); IA.Show(); this.Hide(); }
        private void btnDeudores_Click(object sender, EventArgs e) { DeudoresAdmin DA = new DeudoresAdmin(new DeudaRepository()); DA.Show(); this.Hide(); }
        private void btnReportes_Click(object sender, EventArgs e) { ReportesAdmin RA = new ReportesAdmin(); RA.Show(); this.Hide(); }

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
