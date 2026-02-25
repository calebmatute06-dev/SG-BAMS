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
            ClsConexion objCl = new ClsConexion();
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT * FROM vista_productos";


                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    cmbProductos.DisplayMember = "Nombre Producto";
                    cmbProductos.ValueMember = "ID";
                    cmbProductos.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }
            finally
            {
                objCl.Cerrar();
            }
        }

        private async void FacturaProducto_Load(object sender, EventArgs e)
        {
            await LlenarComboProductos();
        }

        private void BtnAceptar_Click(object sender, EventArgs e)
        {
            int idProd = Convert.ToInt32(cmbProductos.SelectedValue);
            string nombreProd = cmbProductos.Text;
            int cantidadProd = Convert.ToInt32(txtCantidad.Text);

            FormularioFactura.SetProducto(idProd, nombreProd, cantidadProd);


            this.Close();

        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
