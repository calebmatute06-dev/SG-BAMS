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
using Microsoft.IdentityModel.Tokens;
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
            TxtBateria.ReadOnly = true;
            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProductos.ClearSelection();
            dgvProductos.Columns["id_producto"].ReadOnly = true;
            dgvProductos.Columns["nombre_producto"].ReadOnly = true;
            dgvProductos.Columns["precio"].ReadOnly = true;
            dgvProductos.Columns["subtotal"].ReadOnly = true;

            dgvProductos.Columns.Add("stock_max", "StockMax");
            dgvProductos.Columns["stock_max"].Visible = false;

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

            if (string.IsNullOrEmpty(TxtBateria.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar un valor en Batería Vieja", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                TxtBateria.Focus();
                return;
            }

            if (!int.TryParse(TxtBateria.Text.Trim(), out int bateria))
            {
                MessageBox.Show("El valor de Batería Vieja debe ser un número.");
                TxtBateria.Focus();
                return;
            }

            if (bateria < 0 || bateria > 100)
            {
                MessageBox.Show("El valor de Batería Vieja debe estar entre 0 y 100.",
                                "Valor fuera de rango",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
                TxtBateria.Focus();
                return;
            }

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
                MessageBox.Show("El total no puede ser menor o igual a 0.",
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

                    string formaPagoTexto = cmbPago.Text.ToLower();

                    if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito"))
                    {
                        string nombreCliente = TxtCliente.Text.Trim();
                        string montoTotal = TxtTotal.Text;
                        DateTime fechaVenta = DateTFecha.SelectionStart;


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
            if (string.IsNullOrEmpty(TxtBateria.Text.Trim()))
            {
                MessageBox.Show("Debe ingresar un valor en Batería Vieja", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
           
                return;
            }

            if (!int.TryParse(TxtBateria.Text.Trim(), out int bateria))
            {
                MessageBox.Show("El valor de Batería Vieja debe ser un número.");
               
                return;
            }

            if (bateria < 0 || bateria > 100)
            {
                MessageBox.Show("El valor de Batería Vieja debe estar entre 0 y 100.",
                                "Valor fuera de rango",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);
            
                return;
            }

            using (FacturaProducto frmProd = new FacturaProducto())
            {
                frmProd.FormularioFactura = this;
                if (frmProd.ShowDialog() == DialogResult.OK)
                {
                    ClsAgregarProductos objAP = new ClsAgregarProductos();
                    double precio = await objAP.ObtenerPrecioProducto(idProducto);
                    dgvProductos.Rows.Add(idProducto, nombresProductos, cantidades, precio, (cantidades * precio), frmProd.StockSeleccionado);
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

        private void BtnEliminar_Click(object sender, EventArgs e)
        {

            if (dgvProductos.SelectedRows.Count > 0)
            {

                DialogResult result = MessageBox.Show("¿Desea quitar este producto de la lista?",
                    "Eliminar Producto", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {

                    foreach (DataGridViewRow row in dgvProductos.SelectedRows)
                    {

                        if (!row.IsNewRow)
                        {
                            dgvProductos.Rows.Remove(row);
                        }
                    }

                    CalcularTotal();
                }
            }

        }

        private void dgvProductos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvProductos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {

            if (dgvProductos.Rows[e.RowIndex].IsNewRow) return;


            if (dgvProductos.Columns[e.ColumnIndex].Name == "cantidad")
            {
                string valorEntrada = e.FormattedValue.ToString().Trim();


                if (string.IsNullOrEmpty(valorEntrada))
                {
                    MessageBox.Show("La cantidad no puede estar vacía.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }


                if (!int.TryParse(valorEntrada, out int nuevaCantidad) || nuevaCantidad <= 0)
                {
                    MessageBox.Show("Ingrese una cantidad válida mayor a 0", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    return;
                }

                if (dgvProductos.Columns.Contains("stock_max"))
                {
                    var celdaStock = dgvProductos.Rows[e.RowIndex].Cells["stock_max"].Value;
                    if (celdaStock != null)
                    {
                        int stockDisponible = Convert.ToInt32(celdaStock);
                        if (nuevaCantidad > stockDisponible)
                        {
                            MessageBox.Show($"No puedes vender {nuevaCantidad}. El stock disponible es {stockDisponible}.",
                                            "Stock Insuficiente", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            e.Cancel = true;
                        }
                    }
                }
            }
        }

        private void dgvProductos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Columns[e.ColumnIndex].Name == "cantidad")
            {
                int cant = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["cantidad"].Value);
                double precio = Convert.ToDouble(dgvProductos.Rows[e.RowIndex].Cells["precio"].Value);

                dgvProductos.Rows[e.RowIndex].Cells["subtotal"].Value = cant * precio;

                CalcularTotal();
            }
        }

        private void btnBateria_Click(object sender, EventArgs e)
        {
            BateriaVieja BV = new BateriaVieja();
            BV.ShowDialog();
           
        }
    }
}