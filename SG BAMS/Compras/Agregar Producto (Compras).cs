using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para agregar un producto a una compra.
    /// </summary>
    public partial class Agregar_Producto__Compras_ : Form
    {
        /// <summary>Identificador seleccionado.</summary>
        public string IdSeleccionado { get; set; }
        /// <summary>Nombre seleccionado.</summary>
        public string NombreSeleccionado { get; set; }
        /// <summary>Cantidad seleccionada.</summary>
        public int CantidadSeleccionada { get; set; }
        /// <summary>Precio seleccionado.</summary>
        public decimal PrecioSeleccionado { get; set; }

        private int _idProveedor;
        private PlaceholderTextBox phCodigo;
        private PlaceholderTextBox phPrecio;
        private PlaceholderComboBox phProductos;  
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Constructor que recibe el ID del proveedor.
        /// </summary>
        /// <param name="idProv">ID del proveedor.</param>
        public Agregar_Producto__Compras_(int idProv)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this._idProveedor = idProv;
        }

        private void Agregar_Producto__Compras__Load(object sender, EventArgs e)
        {
            LlenarComboProductos();
            numCantidad.DecimalPlaces = 0;
            numCantidad.ThousandsSeparator = true;

           
            phCodigo = new PlaceholderTextBox(txtCodigo, "Código de barras");
            phPrecio = new PlaceholderTextBox(txtPrecio, "0.00");

            
            phProductos = new PlaceholderComboBox(cmbProductos, "Seleccione o escriba el producto");

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private void LlenarComboProductos()
        {
            try
            {
                ClsCompras objCompras = new ClsCompras();
                DataTable dt = objCompras.ObtenerProductosPorProveedor(_idProveedor);

                cmbProductos.DataSource = dt;
                cmbProductos.DisplayMember = "DisplayFull";
                cmbProductos.ValueMember = "id_producto";
                cmbProductos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbProductos.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message);
            }
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            
            if (phProductos.IsPlaceholderActive || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Debe seleccionar un producto de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Cantidad Inválida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            string precioReal = phPrecio.GetRealValue().Trim();
            if (string.IsNullOrWhiteSpace(precioReal) || !decimal.TryParse(precioReal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal precioAux) || precioAux <= 0)
            {
                MessageBox.Show("El precio debe ser un valor numérico válido mayor a cero.", "Precio Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            IdSeleccionado = cmbProductos.SelectedValue.ToString();
            NombreSeleccionado = cmbProductos.Text;
            CantidadSeleccionada = (int)numCantidad.Value;
            PrecioSeleccionado = precioAux;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            using (AgregarProducto frmCrear = new AgregarProducto())
            {
                if (frmCrear.ShowDialog() == DialogResult.OK)
                {
                    LlenarComboProductos();
                    MessageBox.Show("¡Producto registrado! Ya puede seleccionarlo en la lista.");
                }
                else
                {
                    LlenarComboProductos();
                }
            }
        }

        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            string precioReal = phPrecio.GetRealValue().Trim();
            if (!string.IsNullOrWhiteSpace(precioReal))
            {
                if (decimal.TryParse(precioReal, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal valor))
                {
                    txtPrecio.Text = valor.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
        }

        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (cmbProductos.Focused || numCantidad.Focused || txtPrecio.Focused)
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }

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
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
        }

        private void kryptonLabel1_Click(object sender, EventArgs e) { }
        private void kryptonLabel4_Click(object sender, EventArgs e) { }
    }
}