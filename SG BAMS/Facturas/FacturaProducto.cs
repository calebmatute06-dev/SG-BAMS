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
            int idProd = Convert.ToInt32(cmbProductos.SelectedValue);
            string nombreProd = cmbProductos.Text;
            int cantidadProd = Convert.ToInt32(txtCantidad.Text);

            FormularioFactura.SetProducto(idProd, nombreProd, cantidadProd);

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
