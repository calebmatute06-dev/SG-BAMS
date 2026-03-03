using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using SG_BAMS.Login;

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
            ClsAgregarFactura AF = new ClsAgregarFactura();

            try
            {

                DataTable dt = await AF.ObtenerFormasPago();


                cmbPago.DisplayMember = "descripcion_forma_pago";
                cmbPago.ValueMember = "id_tipo_forma_pago";
                cmbPago.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar ComboBox de pagos: " + ex.Message);
            }
            finally
            {

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

            TxtCliente.ReadOnly = true;
            TxtTotal.ReadOnly = true;
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
            TxtTotal.Text = total.ToString() + ",00";
        }
        private bool FacturaTieneProductos()
        {
            return dgvProductos.Rows
                .Cast<DataGridViewRow>()
                .Any(row => !row.IsNewRow);
        }

        private async void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (!FacturaTieneProductos())
            {
                MessageBox.Show("Debe agregar al menos un producto a la factura.",
                                "Factura vacía",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (Convert.ToDecimal(TxtTotal.Text) <= 0)
            {
                MessageBox.Show("El total no puede ser 0.",
                                "Total inválido",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            if (cmbPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago.",
                                "Forma de pago requerida",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                return;
            }

            // Si pasa todas las validaciones se guarda factura

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
                int.TryParse(TxtBateria.Text.Trim(), out int numBaterias);

                int idFactura = await objAF.AgregarFacturas(idUsuario, idCliente, idFormaPago, DateTFecha.SelectionStart, numBaterias);

                if (idFactura > 0)
                {
                    foreach (DataGridViewRow fila in dgvProductos.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        int idProd = Convert.ToInt32(fila.Cells[0].Value);
                        int cantidad = Convert.ToInt32(fila.Cells[2].Value);
                        await objAP.GuardarProductoFactura(idFactura, idProd, cantidad);
                    }

                    MessageBox.Show("Factura guardada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // --- INTEGRACIÓN CON LA NUEVA INTERFAZ ---
                    string formaPagoTexto = cmbPago.Text.ToLower();

                    if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito"))
                    {
                        string nombreCliente = TxtCliente.Text.Trim();
                        string montoTotal = TxtTotal.Text;
                        DateTime fechaVenta = DateTFecha.SelectionStart;

                        // Se usa el nombre de clase exacto que proporcionaste: Modificar_Datos__Deudor_
                        using (Modificar_Datos__Deudor_ frmInfo = new Modificar_Datos__Deudor_(idFactura, nombreCliente, montoTotal, fechaVenta))
                        {
                            frmInfo.ShowDialog();
                        }
                    }

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

        private void DateTFecha_DateChanged(object sender, DateRangeEventArgs e)
        {
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}