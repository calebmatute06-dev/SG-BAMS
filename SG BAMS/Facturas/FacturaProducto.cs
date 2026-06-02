using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para seleccionar un producto y agregarlo a la factura.
    /// </summary>
    public partial class FacturaProducto : Form
    {
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        private PlaceholderTextBox phCodigo;
        private PlaceholderComboBox phProductos;
        private PlaceholderTextBox phCantidad;

        /// <summary>
        /// Obtiene el stock disponible del producto seleccionado.
        /// </summary>
        public int StockSeleccionado { get; set; }

        /// <summary>
        /// Obtiene el precio de venta del producto seleccionado.
        /// </summary>
        public double PrecioSeleccionado { get; set; }

        /// <summary>
        /// Referencia al formulario padre de factura.
        /// </summary>
        public FacturaAgregarDatos FormularioFactura { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia del formulario.
        /// </summary>
        public FacturaProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

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

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActualizarStock();
            txtCantidad.Clear();
        }

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

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (phProductos.IsPlaceholderActive || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            string cantidadReal = phCantidad.GetRealValue().Trim();

           
            bool cantidadValida;
            int cantidadFinal = 0;
            using (var tempCantidad = new KryptonTextBox())
            {
                tempCantidad.Text = cantidadReal;
                if (ClsValidaciones.CampoVacio(tempCantidad, "Cantidad"))
                    cantidadValida = false;
                else if (!int.TryParse(tempCantidad.Text, out int cant) || cant <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cantidadValida = false;
                }
                else
                {
                    cantidadFinal = cant;
                    cantidadValida = true;
                }
            }

            if (!cantidadValida) return;

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

        private void btnEscanear_Click(object sender, EventArgs e)
        {
            txtCodigo.Clear();
            txtCodigo.StateCommon.Back.Color1 = Color.SkyBlue;
            this.Focus();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}