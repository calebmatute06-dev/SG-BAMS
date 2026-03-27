using Microsoft.Data.SqlClient;
using SG_BAMS.Facturas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class FacturaProducto : Form
    {
        public int StockSeleccionado { get; set; }
        public double PrecioSeleccionado { get; set; }
        public FacturaAgregarDatos FormularioFactura { get; set; }
        private DateTime ultimaTeclaEscaner = DateTime.Now;

        public FacturaProducto()
        {
            InitializeComponent();
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
            txtCantidad.KeyPress += (s, ev) => ClsValidaciones.ValidarSoloNumeros(ev);
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

            if (txtCantidad.Focused)
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
                if (!txtCantidad.Focused && !string.IsNullOrWhiteSpace(txtCodigo.Text))
                {
                    BuscarProductoPorCodigo(txtCodigo.Text.Trim());
                    return true;
                }
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void BuscarProductoPorCodigo(string codigo)
        {
            bool encontrado = false;

            foreach (DataRowView fila in cmbProductos.Items)
            {
                if (fila.Row["codigo_barra"].ToString().Trim() == codigo)
                {
                    cmbProductos.SelectedItem = fila;
                    encontrado = true;
                    txtCantidad.Focus();
                    break;
                }
            }

            if (!encontrado)
            {
                MessageBox.Show($"El producto con este código [{codigo}] no tiene stock en el inventario.", "BAMS");
                txtCodigo.Clear();
                txtCodigo.Focus();
            }
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            if (ClsValidaciones.CampoVacio(txtCantidad, "Cantidad")) return;

            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!int.TryParse(lblNumero.Text, out int stock) || cantidad > stock)
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

            FormularioFactura.SetProducto(idProdu, cmbProductos.Text, cantidad);
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