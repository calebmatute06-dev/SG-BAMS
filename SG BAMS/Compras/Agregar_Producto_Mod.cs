using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Krypton.Toolkit;

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

        private PlaceholderTextBox phCodigo;
        private PlaceholderTextBox phPrecio;
        private PlaceholderComboBox phProductos;

        private DateTime ultimaTeclaEscaner = DateTime.Now;

        public Agregar_Producto_Mod(int idProv)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._idProveedor = idProv;
        }

        private void Agregar_Producto_Mod_Load(object sender, EventArgs e)
        {
            try
            {
                ClsCompras objCompras = new ClsCompras();
                cmbProductos.DataSource = objCompras.ObtenerProductosPorProveedor(_idProveedor);
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

            this.ActiveControl = null;
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
                ClsCompras objCompras = new ClsCompras();
                int idProd = Convert.ToInt32(cmbProductos.SelectedValue);

                if (objCompras.ValidarProductoEnCompra(IdCompraActual, idProd))
                {
                    MessageBox.Show("Este producto ya está incluido en la compra.\nModifique la cantidad en la pantalla anterior\n(dando doble click sobre la celda precio o cantidad).",
                                    "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                objCompras.AgregarDetalleACompraExistente(IdCompraActual, idProd, (int)numCantidad.Value, precioFinal);

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
            AgregarProducto agregarProducto = new AgregarProducto();
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

            if ((key >= Keys.D0 && key <= Keys.Z) || (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - ultimaTeclaEscaner;
                ultimaTeclaEscaner = DateTime.Now;

                if (intervalo.TotalMilliseconds > 100)
                {
                    txtCodigo.Text = "";
                }

                char c = (char)key;
                txtCodigo.AppendText(c.ToString().ToLower());
                return true;
            }

            if (key == Keys.Enter)
            {
                if (!cmbProductos.Focused && !numCantidad.Focused && !txtPrecio.Focused)
                {
                    string codigoReal = phCodigo.GetRealValue().Trim();
                    if (!string.IsNullOrWhiteSpace(codigoReal))
                    {
                        BuscarProductoPorCodigo(codigoReal);
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BuscarProductoPorCodigo(string codigo)
        {
            try
            {
                ClsCompras objCompras = new ClsCompras();
                DataTable dt = objCompras.ObtenerProductosPorProveedor(_idProveedor);

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
                    txtCodigo.Focus();
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
            txtCodigo.Focus();
        }
    }
}