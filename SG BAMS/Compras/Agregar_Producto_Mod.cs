using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Agregar_Producto_Mod : Form
    {

        /// <summary>
        /// Gets or sets the identifier compra actual.
        /// </summary>
        /// <value>
        /// The identifier compra actual.
        /// </value>
        public string IdCompraActual { get; set; }

        /// <summary>
        /// Gets or sets the identifier seleccionado.
        /// </summary>
        /// <value>
        /// The identifier seleccionado.
        /// </value>
        public string IdSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the nombre seleccionado.
        /// </summary>
        /// <value>
        /// The nombre seleccionado.
        /// </value>
        public string NombreSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the cantidad seleccionada.
        /// </summary>
        /// <value>
        /// The cantidad seleccionada.
        /// </value>
        public int CantidadSeleccionada { get; set; }
        /// <summary>
        /// Gets or sets the precio seleccionado.
        /// </summary>
        /// <value>
        /// The precio seleccionado.
        /// </value>
        public decimal PrecioSeleccionado { get; set; }

        /// <summary>
        /// The conexion
        /// </summary>
        private ClsConexion conexion = new ClsConexion();

        /// <summary>
        /// The identifier proveedor
        /// </summary>
        private int _idProveedor;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agregar_Producto_Mod"/> class.
        /// </summary>
        /// <param name="idProv">The identifier prov.</param>
        public Agregar_Producto_Mod(int idProv)
        {
            InitializeComponent();
            this._idProveedor = idProv;
        }

        /// <summary>
        /// Handles the Load event of the Agregar_Producto_Mod control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the kryptonButton3 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the 1 event of the btnCancelar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnProductoNuevo control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnProductoNuevo_Click(object sender, EventArgs e)
        {
            AgregarProducto agregarProducto = new AgregarProducto();
            agregarProducto.Show();
        }

        /// <summary>
        /// Handles the KeyPress event of the txtPrecio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirNumerosYDecimales(sender, e);
        }

        /// <summary>
        /// Handles the Leave event of the txtPrecio control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the KeyPress event of the numCantidad control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void numCantidad_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarSoloNumeros(e);
        }

        /// <summary>
        /// The ultima tecla escaner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        /// <summary>
        /// Processes a command key.
        /// </summary>
        /// <param name="msg">A <see cref="T:System.Windows.Forms.Message" />, passed by reference, that represents the Win32 message to process.</param>
        /// <param name="keyData">One of the <see cref="T:System.Windows.Forms.Keys" /> values that represents the key to process.</param>
        /// <returns>
        ///   <see langword="true" /> if the keystroke was processed and consumed by the control; otherwise, <see langword="false" /> to allow further processing.
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
        /// Buscars the producto por codigo.
        /// </summary>
        /// <param name="codigo">The codigo.</param>
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
        /// Handles the Click event of the btnEscanear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.Focus();
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
        }

    }
}