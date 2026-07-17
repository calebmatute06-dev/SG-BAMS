using Krypton.Toolkit;
using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using SG_BAMS.Facturas.DTO;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;

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
        private readonly ClsFactura AF = new ClsFactura();

       
        
        public DetalleDTO ProductoSeleccionado { get; private set; }

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
        
            try
            {
                DataTable dt = await AF.ObtenerStockProductos();

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
                        _ = BuscarProductoPorCodigo(codigoReal);
                        return true;
                    }
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private async Task BuscarProductoPorCodigo(string codigo)
        {
            DataRow prod = await AF.ObtenerProductoPorCodigoBarra(codigo);

            if (prod == null)
            {
                MessageBox.Show($"El producto con código [{codigo}] no existe o no tiene stock.", "BAMS");
                txtCodigo.Clear();
                txtCodigo.Focus();
                return;
            }

            int idProd = Convert.ToInt32(prod["id_producto"]);
            cmbProductos.SelectedValue = idProd;
            txtCantidad.Focus();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarProductoSeleccionado())
                return;

            if (!ValidarCantidad(out int cantidad))
                return;

            if (!ValidarStock(cantidad))
                return;

           

            ConfirmarProducto(cantidad);
        }

        private bool ValidarProductoSeleccionado()
        {
            if (phProductos.IsPlaceholderActive || cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show(
                    "Por favor, seleccione un producto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                cmbProductos.Focus();
                return false;
            }

            return true;
        }

        private bool ValidarCantidad(out int cantidadFinal)
        {
            cantidadFinal = 0;

            string cantidadReal = phCantidad.GetRealValue().Trim();

            using (var tempCantidad = new KryptonTextBox())
            {
                tempCantidad.Text = cantidadReal;

                if (ClsValidaciones.CampoVacio(tempCantidad, "Cantidad"))
                    return false;

                if (!int.TryParse(tempCantidad.Text, out cantidadFinal) || cantidadFinal <= 0)
                {
                    MessageBox.Show(
                        "Ingrese una cantidad válida mayor a 0.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    return false;
                }
            }

            return true;
        }
        private bool ValidarStock(int cantidad)
        {
            if (!int.TryParse(lblNumero.Text, out int stock) || cantidad > stock)
            {
                MessageBox.Show(
                    $"Stock insuficiente. Solo hay {lblNumero.Text} unidades disponibles.",
                    "Inventario",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        private void ConfirmarProducto(int cantidad)
        {
            DataRowView filaSeleccionada = (DataRowView)cmbProductos.SelectedItem;

            ProductoSeleccionado = new DetalleDTO
            {
                IdProducto = Convert.ToInt32(cmbProductos.SelectedValue),
                NombreProducto = cmbProductos.Text,
                Cantidad = cantidad,
                Precio = Convert.ToDouble(filaSeleccionada.Row["precio_venta"]),
                Stock = Convert.ToInt32(lblNumero.Text)
            };


            DialogResult = DialogResult.OK;
            Close();
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