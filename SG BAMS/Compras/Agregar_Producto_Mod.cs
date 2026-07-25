using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Krypton.Toolkit;
using SG_BAMS.ComprasContratos;
using SG_BAMS.Facturas;
using SG_BAMS.AccesoDatos;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para agregar un producto a una compra (versión modificada).
    /// </summary>
    public partial class Agregar_Producto_Mod : Form
    {
        public string IdCompraActual { get; set; }
        public string IdSeleccionado { get; set; }
        public string NombreSeleccionado { get; set; }
        public int CantidadSeleccionada { get; set; }
        public decimal PrecioSeleccionado { get; set; }

        private ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();
        private int _idProveedor;
        private readonly IComprasRepository comprasRepo;

        private PlaceholderTextBox phCodigo;
        private PlaceholderTextBox phPrecio;
        private PlaceholderComboBox phProductos;

        private readonly ServicioEscaneoBarras _servicioEscaneo = new ServicioEscaneoBarras();

        /// <summary>
        /// Constructor original: usa la implementación real de la dependencia.
        /// </summary>
        public Agregar_Producto_Mod(int idProv) : this(idProv, new ClsCompras()) { }

        /// <summary>
        /// Constructor con inyección de dependencias (DIP).
        /// </summary>
        public Agregar_Producto_Mod(int idProv, IComprasRepository comprasRepo)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._idProveedor = idProv;
            this.comprasRepo = comprasRepo;

            _servicioEscaneo.CodigoEscaneado += (codigo) =>
            {
                txtCodigo.Text = codigo;
                BuscarProductoPorCodigo(codigo);
            };
        }

        private void Agregar_Producto_Mod_Load(object sender, EventArgs e)
        {
            try
            {
                cmbProductos.DataSource = comprasRepo.ObtenerProductosPorProveedor(_idProveedor);
                cmbProductos.DisplayMember = "DisplayFull";
                cmbProductos.ValueMember = "id_producto";

                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProductos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }

            numCantidad.DecimalPlaces = 0;
            numCantidad.ThousandsSeparator = true;

            phCodigo = new PlaceholderTextBox(txtCodigo, "Escanee el producto");
            phPrecio = new PlaceholderTextBox(txtPrecio, "Ingrese un precio válido");
            phProductos = new PlaceholderComboBox(cmbProductos, "Seleccione o escriba un producto");

            txtCodigo.ReadOnly = true;
            txtCodigo.TabStop = false;

            this.ActiveControl = null;
            this.Focus();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (phProductos.IsPlaceholderActive || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            string precioReal = phPrecio.GetRealValue().Trim();
            if (string.IsNullOrWhiteSpace(precioReal) ||
                !decimal.TryParse(precioReal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precioFinal) ||
                precioFinal <= 0)
            {
                MessageBox.Show("El precio debe ser un valor numérico válido mayor a cero.", "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            try
            {
                int idProd = Convert.ToInt32(cmbProductos.SelectedValue);

                if (comprasRepo.ValidarProductoEnCompra(IdCompraActual, idProd))
                {
                    MessageBox.Show("Este producto ya está incluido en la compra.\nModifique la cantidad en la pantalla anterior\n(dando doble click sobre la celda precio o cantidad).",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                comprasRepo.AgregarDetalleACompraExistente(IdCompraActual, idProd, (int)numCantidad.Value, precioFinal);

                IdSeleccionado = idProd.ToString();
                NombreSeleccionado = cmbProductos.Text;
                CantidadSeleccionada = (int)numCantidad.Value;
                PrecioSeleccionado = precioFinal;

                MessageBox.Show("Producto añadido correctamente a la compra.", "SG-BAMS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnProductoNuevo_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto(new ProductoInventario.ProductoRepository(), new ProductoInventario.ComboRepository());
            agregarProducto.Show();
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {

        }

        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (cmbProductos.Focused || numCantidad.Focused || txtPrecio.Focused)
                return base.ProcessCmdKey(ref msg, keyData);

            if (_servicioEscaneo.ProcesarTecla(key))
            {
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BuscarProductoPorCodigo(string codigo)
        {
            try
            {
                DataTable dt = comprasRepo.ObtenerProductosPorProveedor(_idProveedor);

                bool encontrado = false;
                foreach (DataRow row in dt.Rows)
                {
                    if (row["codigo_barra"].ToString().Trim() == codigo.Trim())
                    {
                        cmbProductos.SelectedValue = row["id_producto"];
                        numCantidad.Focus();
                        encontrado = true;
                        break;
                    }
                }

                if (!encontrado)
                {
                    MessageBox.Show($"El código [{codigo}] no está asociado a este proveedor.", "BAMS");
                    txtCodigo.Clear();
                    this.ActiveControl = null;
                    this.Focus();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error en búsqueda: " + ex.Message);
            }
        }

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            this.ActiveControl = null;
            this.Focus();
        }
    }
}