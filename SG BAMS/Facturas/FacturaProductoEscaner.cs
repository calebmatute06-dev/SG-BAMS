using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS.Facturas
{
    public partial class FacturaProductoEscaner : Form
    {

        public int StockSeleccionado { get; set; }
        public double PrecioSeleccionado { get; set; }
        public FacturaAgregarDatos FormularioFactura { get; set; }


        private int _idProductoEncontrado = -1;
        private string _nombreProductoEncontrado = string.Empty;
        private int _stockProductoEncontrado = 0;
        private double _precioProductoEncontrado = 0;

        public FacturaProductoEscaner()
        {
            InitializeComponent();
        }


        private void FacturaProductoEscaner_Load(object sender, EventArgs e)
        {

            txtCantidad.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);


            lblNumero.Text = "0";
            LimpiarEstado();
        }







        private async Task BuscarPorCodigoBarra()
        {
            string codigo = txtEscaner.Text.Trim();

            if (string.IsNullOrWhiteSpace(codigo))
            {
                LimpiarEstado();
                return;
            }

            ClsAgregarProductos ap = new ClsAgregarProductos();
            try
            {
                DataRow fila = await ap.ObtenerProductoPorCodigoBarra(codigo);

                if (fila == null)
                {
                    MessageBox.Show(
                        "No se encontró ningún producto con ese código de barras.",
                        "Producto no encontrado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    LimpiarEstado();
                    txtEscaner.Clear();
                    txtEscaner.Focus();
                    return;
                }


                _idProductoEncontrado = Convert.ToInt32(fila["id_producto"]);
                _nombreProductoEncontrado = fila["nombre_producto"].ToString();
                _stockProductoEncontrado = Convert.ToInt32(fila["stock"]);
                _precioProductoEncontrado = Convert.ToDouble(fila["precio_venta"]);


                lblNumero.Text = _stockProductoEncontrado.ToString();
                txtCantidad.Clear();
                txtCantidad.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al buscar producto: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                LimpiarEstado();
            }
        }








        private void BtnNombre_Click(object sender, EventArgs e)
        {
            using (FacturaProducto FP = new FacturaProducto())
            {
                FP.FormularioFactura = this.FormularioFactura;
                DialogResult resultado = FP.ShowDialog();

                if (resultado == DialogResult.OK)
                {

                    this.PrecioSeleccionado = FP.PrecioSeleccionado;
                    this.StockSeleccionado = FP.StockSeleccionado;
                }

                this.DialogResult = resultado;
                this.Close();
            }


        }

        private void LimpiarEstado()
        {
            _idProductoEncontrado = -1;
            _nombreProductoEncontrado = string.Empty;
            _stockProductoEncontrado = 0;
            _precioProductoEncontrado = 0;

            lblNumero.Text = "0";
            txtCantidad.Clear();
        }

        private void BtnAceptar_Click_1(object sender, EventArgs e)
        {
            if (_idProductoEncontrado == -1)
            {
                MessageBox.Show(
                    "Por favor, escanee un código de barras válido.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtEscaner.Focus();
                return;
            }


            if (ClsValidaciones.CampoVacio(txtCantidad, "Cantidad")) return;

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show(
                    "Ingrese una cantidad válida mayor a 0.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }


            if (!int.TryParse(lblNumero.Text, out int stock) || cantidad > stock)
            {
                MessageBox.Show(
                    $"Stock insuficiente. Solo hay {lblNumero.Text} unidades disponibles.",
                    "Inventario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }


            foreach (DataGridViewRow fila in FormularioFactura.dgvProductos.Rows)
            {
                if (fila.IsNewRow) continue;

                if (fila.Cells["id_producto"].Value != null &&
                    Convert.ToInt32(fila.Cells["id_producto"].Value) == _idProductoEncontrado)
                {
                    MessageBox.Show(
                        "Este producto ya fue agregado a la factura actual. " +
                        "Modifique la cantidad en la tabla si es necesario.",
                        "Producto Duplicado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return;
                }
            }


            FormularioFactura.SetProducto(
                _idProductoEncontrado,
                _nombreProductoEncontrado,
                cantidad);

            this.StockSeleccionado = _stockProductoEncontrado;
            this.PrecioSeleccionado = _precioProductoEncontrado;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }



        private async void txtEscaner_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                await BuscarPorCodigoBarra();
            }
        }

        private async void txtEscaner_Leave(object sender, EventArgs e)
        {

            if (!string.IsNullOrWhiteSpace(txtEscaner.Text))
                await BuscarPorCodigoBarra();
        }

        private void FacturaProductoEscaner_Shown(object sender, EventArgs e)
        {
            txtEscaner.Focus();
        }
    }
}