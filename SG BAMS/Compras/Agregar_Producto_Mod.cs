using System.Data;
using System.Globalization;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Agregar_Producto_Mod : Form
    {

        /// <summary>
        /// Obtiene o establece el identificador de la compra actual.
        /// </summary>
        /// <value>
        /// El identificador de la compra actual.
        /// </value>
        public string IdCompraActual { get; set; }

        /// <summary>
        /// Obtiene o establece el identificador seleccionado.
        /// </summary>
        /// <value>
        /// El identificador seleccionado.
        /// </value>
        public string IdSeleccionado { get; set; }
        /// <summary>
        /// Obtiene o establece el nombre seleccionado.
        /// </summary>
        /// <value>
        /// El nombre seleccionado.
        /// </value>
        public string NombreSeleccionado { get; set; }
        /// <summary>
        /// Obtiene o establece la cantidad seleccionada.
        /// </summary>
        /// <value>
        /// La cantidad seleccionada.
        /// </value>
        public int CantidadSeleccionada { get; set; }
        /// <summary>
        /// Obtiene o establece el precio seleccionado.
        /// </summary>
        /// <value>
        /// El precio seleccionado.
        /// </value>
        public decimal PrecioSeleccionado { get; set; }

        /// <summary>
        /// La conexión
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// Identificador del proveedor
        /// </summary>
        private int _idProveedor;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Agregar_Producto_Mod"/>.
        /// </summary>
        /// <param name="idProv">El identificador del proveedor.</param>
        public Agregar_Producto_Mod(int idProv)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this._idProveedor = idProv;
        }

        /// <summary>
        /// Maneja el evento Load del formulario Agregar_Producto_Mod.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
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
        }


        /// <summary>
        /// Maneja el evento Click del control kryptonButton3.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void kryptonButton3_Click(object sender, EventArgs e)
        {

            if (cmbProductos.SelectedValue == null || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (numCantidad.Value <= 0)
            {
                MessageBox.Show("La cantidad debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                numCantidad.Focus();
                return;
            }

            if (!ClsValidaciones.EsNumeroDecimalValido(txtPrecio, "El precio", out decimal precioFinal))
            {
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

        /// <summary>
        /// Maneja el evento Click del control btnCancelar.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control btnProductoNuevo.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnProductoNuevo_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto();
            agregarProducto.Show();
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtPrecio.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        /// <summary>
        /// Maneja el evento Leave del control txtPrecio.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void txtPrecio_Leave(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtPrecio.Text))
            {
                if (decimal.TryParse(txtPrecio.Text, out decimal valor))
                {
                    txtPrecio.Text = valor.ToString("N2", CultureInfo.InvariantCulture);
                }
            }
        }

        /// <summary>
        /// Maneja el evento KeyPress del control numCantidad.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        /// <summary>
        /// Última tecla detectada del escáner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Procesa una tecla de comando.
        /// </summary>
        /// <param name="msg">Mensaje de Windows que se va a procesar.</param>
        /// <param name="keyData">Tecla que se va a procesar.</param>
        /// <returns>
        /// true si la tecla fue procesada; de lo contrario, false.
        /// </returns>
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
                    if (!string.IsNullOrWhiteSpace(txtCodigo.Text))
                    {
                        BuscarProductoPorCodigo(txtCodigo.Text.Trim());
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Busca el producto por código.
        /// </summary>
        /// <param name="codigo">El código.</param>
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

        /// <summary>
        /// Maneja el evento Click del control btnEscanear.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.Focus();
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
        }

    }
}