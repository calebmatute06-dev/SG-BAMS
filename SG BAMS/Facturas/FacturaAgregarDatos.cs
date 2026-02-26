using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using SG_BAMS.Login;
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
    public partial class FacturaAgregarDatos : Form
    {
        int idCliente, idProducto, cantidades;
        string nombresProductos;
        public FacturaAgregarDatos(string cliente, int idCli)
        {
            InitializeComponent();
            TxtCliente.Text = cliente;
            idCliente = idCli;
        }

        public void SetProducto(int idProd, string nombreProd, int cantidadProd)
        {
            idProducto = idProd;
            nombresProductos = nombreProd;
            cantidades = cantidadProd;
        }
        public FacturaAgregarDatos()
        {
            InitializeComponent();


        }


        private async Task LlenarComboPago()
        {
            ClsConexion objCl = new ClsConexion();
            try
            {
                objCl.AbrirConexion();

                string query = "SELECT *  FROM Tipo_Forma_de_pago";


                using (SqlCommand cmd = new SqlCommand(query, objCl.Conectar))
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    DataTable dt = new DataTable();
                    dt.Load(reader);


                    cmbPago.DisplayMember = "descripcion_forma_pago";
                    cmbPago.ValueMember = "id_tipo_forma_pago";
                    cmbPago.DataSource = dt;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox: " + ex.Message);
            }
            finally
            {
                objCl.Cerrar();
                dgvProductos.Rows.Clear();
            }
        }

        private async void FacturaAgregarDatos_Load(object sender, EventArgs e)
        {
            await LlenarComboPago();
            


            dgvProductos.Columns.Add("id_producto", "Código");
            dgvProductos.Columns.Add("nombre_producto", "Nombre");
            dgvProductos.Columns.Add("cantidad", "Cantidad");
            dgvProductos.Columns.Add("precio", "Precio");
            dgvProductos.Columns.Add("subtotal", "Subtotal");

        }

        private void CalcularTotal()
        {
            int bateriaVieja = Convert.ToInt32(TxtBateria.Text);

            double acumulador = 0, rebaja = 0;


            for (int i = 0; i < dgvProductos.Rows.Count; i++)
            {

                if (dgvProductos.Rows[i].Cells["Subtotal"].Value != null)
                {
                    acumulador += Convert.ToDouble(dgvProductos.Rows[i].Cells["Subtotal"].Value);
                }
            }

            if (bateriaVieja == 1)
            {
                rebaja = 500;
            }
            else if (bateriaVieja > 1)
            {
                rebaja = 500 + (bateriaVieja - 1) * 300;    
            }

            double total = acumulador - rebaja;

                TxtTotal.Text = total.ToString();
        }

        private async void BtnAceptar_Click(object sender, EventArgs e)
        {
            ClsPasarUsuario objPU = new ClsPasarUsuario();
            ClsAgregarFactura objAF = new ClsAgregarFactura();
            ClsAgregarProductos objAP = new ClsAgregarProductos();
            int idUsuario = objPU.IdUsuario();

            int idFactura = await objAF.AgregarFacturas(idUsuario, idCliente, Convert.ToInt32(cmbPago.SelectedValue), DateTFecha.SelectionStart, Convert.ToInt32(TxtBateria.Text.Trim()));

            if (idFactura > 0)
            {

                foreach (DataGridViewRow fila in dgvProductos.Rows)
                {
                    if (fila.IsNewRow) continue;

                    int idProd = Convert.ToInt32(fila.Cells[0].Value);
                    int cantidad = Convert.ToInt32(fila.Cells[2].Value);

                    await objAP.GuardarProductoFactura(idFactura, idProd, cantidad);
                }

                MessageBox.Show("Factura y productos agregados correctamente.");

                this.DialogResult = DialogResult.OK;
                this.Close();


            }
            else
            {
                MessageBox.Show("No se pudo agregar la factura.");
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
            dgvProductos.Columns.Clear();
        }

        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (FacturaProducto frmProd = new FacturaProducto())
            {
                frmProd.FormularioFactura = this;

                if (frmProd.ShowDialog() == DialogResult.OK)
                {
                    ClsAgregarProductos objAP = new ClsAgregarProductos();
                    double precio = await objAP.ObtenerPrecioProducto(idProducto);

                    dgvProductos.Rows.Add(idProducto, nombresProductos, cantidades, precio, cantidades * precio);
                    CalcularTotal();
                }
            }
        }

       
    }
}
