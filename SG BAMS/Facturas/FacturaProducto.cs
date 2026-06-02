using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacturaProducto : Form
    {
        /// <summary>
        /// Gets or sets the stock seleccionado.
        /// </summary>
        /// <value>
        /// The stock seleccionado.
        /// </value>
        public int StockSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the precio seleccionado.
        /// </summary>
        /// <value>
        /// The precio seleccionado.
        /// </value>
        public double PrecioSeleccionado { get; set; }
        /// <summary>
        /// Gets or sets the formulario factura.
        /// </summary>
        /// <value>
        /// The formulario factura.
        /// </value>
        public FacturaAgregarDatos FormularioFactura { get; set; }
        /// <summary>
        /// The ultima tecla escaner
        /// </summary>
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        private PlaceholderTextBox phCodigo;
        private PlaceholderComboBox phProductos;
        private PlaceholderTextBox phCantidad;

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaProducto"/> class.
        /// </summary>
        public FacturaProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Llenars the combo productos.
        /// </summary>
        private async Task LlenarComboProductos()
        {
            ClsAgregarProductos ap = new ClsAgregarProductos();
            try
            {
                DataTable dt = await ap.ObtenerStockProductos();

                cmbProductos.DataSource = null;
                cmbProductos.DisplayMember = "NombreCompleto";
                cmbProductos.ValueMember = "ID";
                cmbProductos.DataSource = dt;
                cmbProductos.DropDownStyle = ComboBoxStyle.DropDown;
                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;

                lblNumero.DataBindings.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the FacturaProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void FacturaProducto_Load(object sender, EventArgs e)
        {
            await LlenarComboProductos();
            cmbProductos.SelectedIndex = -1;
            lblNumero.Text = "0";

            phCodigo = new PlaceholderTextBox(txtCodigo, "Código de barras");
            phProductos = new PlaceholderComboBox(cmbProductos, "Seleccione o escriba un producto");
            phCantidad = new PlaceholderTextBox(txtCantidad, "Cantidad");

            txtCantidad.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Actualizars the stock.
        /// </summary>
        private void ActualizarStock()
        {
            if (cmbProductos.SelectedItem != null && cmbProductos.SelectedItem is DataRowView fila)
            {
                lblNumero.Text = fila["Stock"].ToString();
            }
            else
            {
                lblNumero.Text = "0";
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cmbProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStock();
            txtCantidad.Clear();
        }

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

            if (cmbProductos.Focused || txtCantidad.Focused)
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

                string tecla = new KeysConverter().ConvertToString(key);
                txtCodigo.AppendText(tecla);
                return true;
            }

            if (key == Keys.Enter)
            {
                if (!cmbProductos.Focused && !txtCantidad.Focused)
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

        /// <summary>
        /// Buscars the producto por codigo.
        /// </summary>
        /// <param name="codigo">The codigo.</param>
        private void BuscarProductoPorCodigo(string codigo)
        {
            bool encontrado = false;
            string codigoBusqueda = codigo.ToUpper();

            foreach (DataRowView fila in cmbProductos.Items)
            {
                string codFila = fila.Row["codigo_barra"].ToString().Trim().ToUpper();

                if (codFila == codigoBusqueda)
                {
                    cmbProductos.SelectedItem = fila;
                    encontrado = true;
                    txtCantidad.Focus();
                    break;
                }
            }

            if (!encontrado)
            {
                MessageBox.Show($"El producto con código [{codigo}] no existe o no tiene stock.", "BAMS");
                txtCodigo.Clear();
                txtCodigo.Focus();
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnAceptar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (phProductos.IsPlaceholderActive || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            string cantidadReal = phCantidad.GetRealValue().Trim();
            string originalCantidad = txtCantidad.Text;
            txtCantidad.Text = cantidadReal;

            bool valido = true;
            if (ClsValidaciones.CampoVacio(txtCantidad, "Cantidad"))
                valido = false;
            else if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                valido = false;
            }

            txtCantidad.Text = originalCantidad;
            if (!valido) return;

            int cantidadFinal = int.Parse(cantidadReal);

            if (!int.TryParse(lblNumero.Text, out int stock) || cantidadFinal > stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo hay {lblNumero.Text} unidades disponibles.", "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idProdu = Convert.ToInt32(cmbProductos.SelectedValue);
            foreach (DataGridViewRow fila in FormularioFactura.dgvProductos.Rows)
            {
                if (fila.IsNewRow) continue;
                if (fila.Cells["id_producto"].Value != null && Convert.ToInt32(fila.Cells["id_producto"].Value) == idProdu)
                {
                    MessageBox.Show("Este producto ya fue agregado. Modifique la cantidad en la tabla.", "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            DataRowView filaSeleccionada = (DataRowView)cmbProductos.SelectedItem;
            double precio = Convert.ToDouble(filaSeleccionada.Row["precio_venta"]);

            FormularioFactura.SetProducto(idProdu, cmbProductos.Text, cantidadFinal);
            this.StockSeleccionado = stock;
            this.PrecioSeleccionado = precio;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// Handles the Click event of the btnEscanear control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
            this.Focus();
        }

        /// <summary>
        /// Handles the Click event of the BtnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}