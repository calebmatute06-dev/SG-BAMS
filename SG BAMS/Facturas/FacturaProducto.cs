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

                cmbProductos.DisplayMember = "Nombre Producto";
                cmbProductos.ValueMember = "ID";
                cmbProductos.DataSource = dt;

                
                cmbProductos.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbProductos.AutoCompleteSource = AutoCompleteSource.ListItems;

                
                lblNumero.DataBindings.Clear();
                lblNumero.DataBindings.Add("Text", dt, "Stock");
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

        private void BtnAceptar_Click(object sender, EventArgs e)
        {


          
           
            if (cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

         
            if (ClsValidaciones.CampoVacio(txtCantidad, "Cantidad")) return;

           
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

          
            if (!int.TryParse(lblNumero.Text, out int stock) || cantidad > stock)
            {
                MessageBox.Show($"Stock insuficiente. Solo hay {lblNumero.Text} unidades disponibles.",
                    "Inventario", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            int idProdu = Convert.ToInt32(cmbProductos.SelectedValue);
            foreach (DataGridViewRow fila in FormularioFactura.dgvProductos.Rows)
            {
                if (fila.IsNewRow) continue;
                if (fila.Cells["id_producto"].Value != null &&
                    Convert.ToInt32(fila.Cells["id_producto"].Value) == idProdu)
                {
                    MessageBox.Show("Este producto ya fue agregado a la factura actual. " +
                        "Modifique la cantidad en la tabla si es necesario.",
                        "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            txtCantidad.Clear();
        }
    }
}