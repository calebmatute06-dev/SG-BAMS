using SG_BAMS.ProductoInventario;
using System;
using System.Drawing;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Listado de productos del inventario (perfil Empleado, solo lectura).
    /// Única responsabilidad: mostrar y filtrar el listado, delegando el acceso a datos
    /// en IProductoRepository recibido por inyección (ver auditoría SOLID, hallazgos IEM01-IEM03).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class InventarioEmp : Form
    {
        private readonly IProductoRepository productoRepositorio;
        private readonly NavegacionService navegacion = new NavegacionService();

        /// <summary>
        /// Texto del placeholder para el campo de búsqueda
        /// </summary>
        private string placeholderTexto = "Buscar por nombre del producto...";

        /// <summary>
        /// Color del texto placeholder
        /// </summary>
        private Color placeholderColor = Color.Gray;

        /// <summary>
        /// Color del texto normal
        /// </summary>
        private Color textoColor = Color.Black;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InventarioEmp" />,
        /// recibiendo su dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="productoRepositorio">Acceso a datos de productos.</param>
        public InventarioEmp(IProductoRepository productoRepositorio)
        {
            InitializeComponent();
            AdaptadorPantallaCompleta.Habilitar(this);
            this.productoRepositorio = productoRepositorio;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            ConfigurarPlaceholder();
        }

        /// <summary>
        /// Configura el placeholder en el TextBox de búsqueda.
        /// </summary>
        private void ConfigurarPlaceholder()
        {
            textoColor = txtBuscar.ForeColor;
            placeholderColor = Color.Gray;

            txtBuscar.Text = placeholderTexto;
            txtBuscar.ForeColor = placeholderColor;

            txtBuscar.Enter += txtBuscar_Enter;
            txtBuscar.Leave += txtBuscar_Leave;
            txtBuscar.TextChanged += txtBuscar_TextChanged;
        }

        /// <summary>
        /// Maneja el evento Enter del TextBox de búsqueda.
        /// </summary>
        private void txtBuscar_Enter(object sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholderTexto)
            {
                txtBuscar.Text = "";
                txtBuscar.ForeColor = textoColor;
            }
        }

        /// <summary>
        /// Maneja el evento Leave del TextBox de búsqueda.
        /// </summary>
        private void txtBuscar_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                txtBuscar.Text = placeholderTexto;
                txtBuscar.ForeColor = placeholderColor;
            }
        }

        /// <summary>
        /// Maneja el evento Load del control InventarioEmp.
        /// </summary>
        private void InventarioEmp_Load(object sender, EventArgs e)
        {
            btnInventario.Enabled = false;
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.ForeColor = Color.White;

            CargarInventarioCompleto();

            EstiloDataGridView.Aplicar(dgvInventarioEmp);
            dgvInventarioEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvInventarioEmp.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento TextChanged del control txtBuscar.
        /// </summary>
        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            if (txtBuscar.Text == placeholderTexto || txtBuscar.ForeColor == placeholderColor)
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(txtBuscar.Text))
            {
                CargarInventarioCompleto();
                return;
            }

            try
            {
                dgvInventarioEmp.DataSource = productoRepositorio.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        /// <summary>
        /// Carga el inventario completo.
        /// </summary>
        public void CargarInventarioCompleto()
        {
            try
            {
                dgvInventarioEmp.DataSource = productoRepositorio.MostrarProductosCompleto();
                dgvInventarioEmp.ReadOnly = true;
                dgvInventarioEmp.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvInventarioEmp.AllowUserToAddRows = false;
                dgvInventarioEmp.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                if (dgvInventarioEmp.Columns.Contains("Producto"))
                {
                    dgvInventarioEmp.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificacionesAdmin = new NotificacionesAdmin();
            notificacionesAdmin.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento Click del control btnMenu.
        /// </summary>
        private void btnMenu_Click(object sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalEmp());

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        private void btnFacturas_Click(object sender, EventArgs e) => navegacion.IrA(this, new FacturasEmp());

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e) => navegacion.IrA(this, new ClientesEmp(new Cliente.ClienteRepository()));

        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        private void btnDeudores_Click(object sender, EventArgs e) =>
            navegacion.IrA(this, new Deudores_Emp(new DeudaRepository()));

        /// <summary>
        /// Maneja el evento Click del control btnCerrar.
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
        /// Maneja el evento Click del control btnPerfil.
        /// </summary>
        private void btnPerfil_Click(object sender, EventArgs e)
        {
            Perfil perfil = new Perfil();
            perfil.ShowDialog();
        }
    }
}