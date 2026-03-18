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

        public FacturaProducto()
        {
            InitializeComponent();
        }
        public int StockSeleccionado { get; set; }
        public FacturaAgregarDatos FormularioFactura { get; set; }
        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private async Task LlenarComboProductos()
        {
            ClsAgregarProductos AP = new ClsAgregarProductos();

            try
            {
                
                DataTable dt = await AP.ObtenerStockProductos();

               
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
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }

        }

        private async void FacturaProducto_Load(object sender, EventArgs e)
        {
            await LlenarComboProductos();
            cmbProductos.SelectedIndex = -1;
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
           
            if (cmbProductos.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un producto antes de continuar.",
                                "Producto requerido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                cmbProductos.Focus();
                return;
            }

            
            if (ClsValidaciones.CampoVacio(txtCantidad, "Cantidad")) return;

            
            if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad) || cantidad <= 0)
            {
                MessageBox.Show("Ingrese una cantidad válida mayor a 0 (solo números).",
                                "Cantidad inválida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }

            int stock = int.Parse(lblNumero.Text);

           
            if (cantidad > stock)
            {
                MessageBox.Show($"No puede vender más del stock disponible ({stock}).",
                                "Stock insuficiente",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                txtCantidad.Focus();
                return;
            }

            int idProdu = Convert.ToInt32(cmbProductos.SelectedValue);

           
            foreach (DataGridViewRow fila in FormularioFactura.dgvProductos.Rows)
            {
                if (fila.IsNewRow) continue;

                if (fila.Cells[0].Value != null && Convert.ToInt32(fila.Cells[0].Value) == idProdu)
                {
                    MessageBox.Show("Este producto ya está en la factura.",
                                    "Producto ya existente", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }

            string nombreProd = cmbProductos.Text;

            FormularioFactura.SetProducto(idProdu, nombreProd, cantidad);
            this.StockSeleccionado = stock;

            this.DialogResult = DialogResult.OK;
            this.Hide();


        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmbProductos_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }


    }
}
