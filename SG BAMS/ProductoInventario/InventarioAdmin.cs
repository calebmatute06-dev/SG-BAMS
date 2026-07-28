using SG_BAMS.Bitacora;
using SG_BAMS.ProductoInventario;
using SG_BAMS.ProductoInventario.DTO;
using SG_BAMS.Proveedor;
using SG_BAMS.Reporte;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Listado y administración de productos del inventario (perfil Administrador).
    /// Única responsabilidad: coordinar la grilla y la apertura de los formularios de
    /// alta/edición, delegando el acceso a datos en IProductoRepository / IComboRepository
    /// recibidos por inyección (ver auditoría SOLID, hallazgos IAD01-IAD04).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class InventarioAdmin : Form
    {
        private readonly IProductoRepository productoRepositorio;
        private readonly IComboRepository comboRepositorio;
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
        /// La última tecla del escáner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="InventarioAdmin" />,
        /// recibiendo sus dependencias de acceso a datos por inyección.
        /// </summary>
        /// <param name="productoRepositorio">Acceso a datos de productos.</param>
        /// <param name="comboRepositorio">Acceso a datos de los catálogos de combo.</param>
        public InventarioAdmin(IProductoRepository productoRepositorio, IComboRepository comboRepositorio)
        {
            InitializeComponent();
            AdaptadorPantallaCompleta.Habilitar(this);
            this.productoRepositorio = productoRepositorio;
            this.comboRepositorio = comboRepositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.KeyPreview = true;
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
        /// Carga el inventario completo.
        /// </summary>
        public void CargarInventarioCompleto()
        {
            try
            {
                dgvProductosAdmin.DataSource = productoRepositorio.MostrarProductosCompleto();
                dgvProductosAdmin.ReadOnly = true;
                dgvProductosAdmin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvProductosAdmin.AllowUserToAddRows = false;
                dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                if (dgvProductosAdmin.Columns.Contains("Producto"))
                {
                    dgvProductosAdmin.Columns["Producto"].MinimumWidth = 150;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar el inventario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Load del control InventarioAdmin.
        /// </summary>
        private void InventarioAdmin_Load(object sender, EventArgs e)
        {
            btnInventario.Enabled = false;
            btnInventario.BackColor = Color.SkyBlue;
            btnInventario.ForeColor = Color.White;

            CargarInventarioCompleto();

            EstiloDataGridView.Aplicar(dgvProductosAdmin);
            dgvProductosAdmin.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductosAdmin.ClearSelection();

            dgvProductosAdmin.CellFormatting += dgvProductosAdmin_CellFormatting;
        }

        /// <summary>
        /// Maneja el evento Click del control btnAgregar.
        /// </summary>
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            AgregarProducto frm = new AgregarProducto(productoRepositorio, comboRepositorio);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                CargarInventarioCompleto();
            }

            dgvProductosAdmin.ClearSelection();
        }

        /// <summary>
        /// Maneja el evento Click del control kryptonButton10.
        /// </summary>
        private void kryptonButton10_Click(object sender, EventArgs e)
        {
            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto(productoRepositorio, comboRepositorio, ArmarProductoDTODesdeFila());

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }

                dgvProductosAdmin.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        /// <summary>
        /// Arma un ProductoDTO a partir de la fila actualmente seleccionada en el grid.
        /// Reemplaza el llenado manual de campos sueltos del formulario ModificarProducto.
        /// </summary>
        private ProductoDTO ArmarProductoDTODesdeFila()
        {
            string precio = dgvProductosAdmin.CurrentRow.Cells["Precio Venta"].Value.ToString();

            var dto = new ProductoDTO
            {
                IdProducto = Convert.ToInt32(dgvProductosAdmin.CurrentRow.Cells["ID"].Value),
                Nombre = dgvProductosAdmin.CurrentRow.Cells["Producto"].Value.ToString(),
                Precio = Convert.ToDecimal(precio.Replace("L.", "").Trim()),
                CodigoBarra = dgvProductosAdmin.CurrentRow.Cells["Codigo Barra"].Value.ToString(),
                ProveedorActual = dgvProductosAdmin.CurrentRow.Cells["Proveedor"].Value.ToString(),
                MarcaActual = dgvProductosAdmin.CurrentRow.Cells["Marca"].Value.ToString(),
                TipoActual = dgvProductosAdmin.CurrentRow.Cells["Tipo"].Value.ToString(),
                ModeloActual = dgvProductosAdmin.CurrentRow.Cells["Modelo Auto"].Value.ToString(),
                EstadoActual = dgvProductosAdmin.CurrentRow.Cells["Estado"].Value.ToString()
            };

            if (dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value != DBNull.Value)
            {
                dto.Stock = Convert.ToInt32(dgvProductosAdmin.CurrentRow.Cells["Stock Actual"].Value);
            }

            return dto;
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
                dgvProductosAdmin.DataSource = productoRepositorio.BuscarProductos(txtBuscar.Text.Trim());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar: " + ex.Message, "BAMS");
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnNoti.
        /// </summary>
        private void btnNoti_Click(object sender, EventArgs e)
        {
            NotificacionesAdmin notificaciones = new NotificacionesAdmin();
            notificaciones.ShowDialog();
        }

        /// <summary>
        /// Maneja el evento CellDoubleClick del control dgvProductosAdmin.
        /// </summary>
        private void dgvProductosAdmin_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            if (dgvProductosAdmin.SelectedRows.Count > 0)
            {
                ModificarProducto frmMod = new ModificarProducto(productoRepositorio, comboRepositorio, ArmarProductoDTODesdeFila());

                if (frmMod.ShowDialog() == DialogResult.OK)
                {
                    CargarInventarioCompleto();
                }

                dgvProductosAdmin.ClearSelection();
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una fila para modificar.", "BAMS");
            }
        }

        /// <summary>
        /// Procesa una tecla de comando.
        /// </summary>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if ((key >= Keys.D0 && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - ultimaTeclaEscaner;
                ultimaTeclaEscaner = DateTime.Now;

                if (intervalo.TotalMilliseconds < 50 || !txtBuscar.Focused)
                {
                    txtBuscar.Text = string.Empty;
                    txtBuscar.ForeColor = textoColor;

                    if (!txtBuscar.Focused) txtBuscar.Focus();

                    char c = (char)key;
                    txtBuscar.AppendText(c.ToString().ToLower());

                    return true;
                }
            }

            if (key == Keys.Enter)
            {
                if (txtBuscar.Focused && !string.IsNullOrWhiteSpace(txtBuscar.Text) && txtBuscar.Text != placeholderTexto)
                {
                    dgvProductosAdmin.DataSource = productoRepositorio.BuscarProductos(txtBuscar.Text.Trim());
                    txtBuscar.SelectAll();

                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Maneja el evento Click del control btnMenu.
        /// </summary>
        private void btnMenu_Click(object sender, EventArgs e) => navegacion.IrA(this, new MenuPrincipalAdm());

        /// <summary>
        /// Maneja el evento Click del control btnFacturas.
        /// </summary>
        private void btnFacturas_Click(object sender, EventArgs e) => navegacion.IrA(this, new FacturasAdm());

        /// <summary>
        /// Maneja el evento Click del control btnCompra.
        /// </summary>
        private void btnCompra_Click(object sender, EventArgs e) => navegacion.IrA(this, new Compras());

        /// <summary>
        /// Maneja el evento Click del control btnClientes.
        /// </summary>
        private void btnClientes_Click(object sender, EventArgs e) => navegacion.IrA(this, new ClientesAdm(new Cliente.ClienteRepository()));

        /// <summary>
        /// Maneja el evento Click del control btnProveedores.
        /// </summary>
        private void btnProveedores_Click(object sender, EventArgs e)
        {
            var PA = new ProveedoresAdmin(
                new ProveedorRepository(),
                new EstadoRepository(),
                new ClasificacionRepository());
            navegacion.IrA(this, PA);
        }

        /// <summary>
        /// Maneja el evento Click del control btnDeudores.
        /// </summary>
        private void btnDeudores_Click(object sender, EventArgs e) =>
            navegacion.IrA(this, new DeudoresAdmin(new DeudaRepository()));

        /// <summary>
        /// Maneja el evento Click del control btnReportes.
        /// </summary>
        private void btnReportes_Click(object sender, EventArgs e) => navegacion.IrA(this, new ReportesAdmin());

        /// <summary>
        /// Maneja el evento Click del control btnBitacora.
        /// </summary>
        private void btnBitacora_Click(object sender, EventArgs e) => navegacion.IrA(this, new BitacoraAdmin());

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

        private void dgvProductosAdmin_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvProductosAdmin.Columns[e.ColumnIndex].Name == "Stock Actual" && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out int stock))
                {
                    if (stock < 1)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 192, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkRed;
                    }
                    else if (stock < 10)
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(255, 224, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.Brown;
                    }
                    else
                    {
                        e.CellStyle.BackColor = System.Drawing.Color.FromArgb(192, 255, 192);
                        e.CellStyle.ForeColor = System.Drawing.Color.DarkGreen;
                    }
                }
            }
        }
    }
}