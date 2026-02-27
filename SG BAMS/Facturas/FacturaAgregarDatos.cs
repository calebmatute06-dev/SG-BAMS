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
            // 1. Validaciones de seguridad
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto antes de facturar.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbPago.SelectedValue == null)
            {
                MessageBox.Show("Por favor seleccione una forma de pago.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                ClsPasarUsuario objPU = new ClsPasarUsuario();
                ClsAgregarFactura objAF = new ClsAgregarFactura();
                ClsAgregarProductos objAP = new ClsAgregarProductos();

                int idUsuario = objPU.IdUsuario();
                int idFormaPago = Convert.ToInt32(cmbPago.SelectedValue);

                // Manejo de baterías viejas para evitar el FormatException
                int.TryParse(TxtBateria.Text.Trim(), out int numBaterias);

                // 2. GUARDAR CABECERA DE FACTURA
                // Esto asigna el ID y permite que el Trigger en SQL se prepare
                int idFactura = await objAF.AgregarFacturas(idUsuario, idCliente, idFormaPago, DateTFecha.SelectionStart, numBaterias);

                if (idFactura > 0)
                {
                    // 3. GUARDAR DETALLE DE PRODUCTOS
                    // Al insertar el primer producto, el Trigger de SQL creará la deuda automáticamente
                    foreach (DataGridViewRow fila in dgvProductos.Rows)
                    {
                        if (fila.IsNewRow) continue;

                        int idProd = Convert.ToInt32(fila.Cells[0].Value);
                        int cantidad = Convert.ToInt32(fila.Cells[2].Value);

                        await objAP.GuardarProductoFactura(idFactura, idProd, cantidad);
                    }

                    MessageBox.Show("Factura guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 4. LÓGICA DE APERTURA DE PAGO (SOLO CRÉDITO)
                    // Verificamos si el texto del combo contiene "Crédito"
                    string formaPagoTexto = cmbPago.Text.ToLower();

                    if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito"))
                    {
                        // Obtenemos el nombre directamente del TextBox del cliente
                        string nombreCliente = TxtCliente.Text.Trim();

                        // Abrimos el formulario pasando el nombre al constructor que configuramos
                        using (Pago_Deuda frmPago = new Pago_Deuda(nombreCliente))
                        {
                            // Lo mostramos como diálogo para que el proceso sea lineal
                            frmPago.ShowDialog();
                        }
                    }

                    // 5. Finalizar y cerrar formulario de factura
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Hubo un error al intentar generar la factura en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error inesperado: " + ex.Message, "Error Crítico", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
