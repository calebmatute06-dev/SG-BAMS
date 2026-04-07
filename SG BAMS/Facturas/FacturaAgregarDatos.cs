using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Cliente;
using SG_BAMS.Deudores;
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
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class FacturaAgregarDatos : Form
    {
        /// <summary>
        /// The identifier cliente
        /// </summary>
        int idCliente, cantidades;
        /// <summary>
        /// The identifier producto
        /// </summary>
        public int idProducto;
        /// <summary>
        /// The nombres productos
        /// </summary>
        string nombresProductos, cantidadBateria;
        /// <summary>
        /// The precio bateria
        /// </summary>
        double precioBateria;
        /// <summary>
        /// The RTN cliente
        /// </summary>
        string rtnCliente;


        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaAgregarDatos"/> class.
        /// </summary>
        /// <param name="cliente">The cliente.</param>
        /// <param name="idCli">The identifier cli.</param>
        /// <param name="rtn">The RTN.</param>
        public FacturaAgregarDatos(string cliente, int idCli, string rtn = "Sin RTN")
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCliente.Text = cliente;
            idCliente = idCli;
            rtnCliente = string.IsNullOrWhiteSpace(rtn) ? "Sin RTN" : rtn;
        }

        /// <summary>
        /// Sets the producto.
        /// </summary>
        /// <param name="idProd">The identifier product.</param>
        /// <param name="nombreProd">The nombre product.</param>
        /// <param name="cantidadProd">The cantidad product.</param>
        public void SetProducto(int idProd, string nombreProd, int cantidadProd)
        {
            idProducto = idProd;
            nombresProductos = nombreProd;
            cantidades = cantidadProd;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FacturaAgregarDatos"/> class.
        /// </summary>
        public FacturaAgregarDatos()
        {
            InitializeComponent();
            cantidadBateria = "0";
            precioBateria = 0;
            rtnCliente = "Sin RTN";
        }

        /// <summary>
        /// Llenars the combo pago.
        /// </summary>
        private async Task LlenarComboPago()
        {
            ClsAgregarFactura AF = new ClsAgregarFactura();
            try
            {
                DataTable dt = await AF.ObtenerFormasPago();
                cmbPago.DisplayMember = "descripcion_forma_pago";
                cmbPago.ValueMember = "id_tipo_forma_pago";
                cmbPago.DataSource = dt;
                cmbPago.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar pagos: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Load event of the FacturaAgregarDatos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void FacturaAgregarDatos_Load(object sender, EventArgs e)
        {
            await LlenarComboPago();

            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add("id_producto", "Código");
            dgvProductos.Columns.Add("nombre_producto", "Nombre");
            dgvProductos.Columns.Add("cantidad", "Cantidad");
            dgvProductos.Columns.Add("precio", "Precio");
            dgvProductos.Columns.Add("subtotal", "Subtotal");
            dgvProductos.Columns.Add("stock_max", "StockMax");
            dgvProductos.Columns["stock_max"].Visible = false;


            txtCliente.ReadOnly = true;
            txtTotal.ReadOnly = true;
            txtBateria.ReadOnly = true;
            txtRebaja.ReadOnly = true;
            txtSubtotal.ReadOnly = true;

            dgvProductos.AllowUserToAddRows = false;
            dgvProductos.Columns["id_producto"].ReadOnly = true;
            dgvProductos.Columns["nombre_producto"].ReadOnly = true;
            dgvProductos.Columns["nombre_producto"].Width = 200;
            dgvProductos.Columns["precio"].ReadOnly = true;
            dgvProductos.Columns["subtotal"].ReadOnly = true;
            dgvProductos.Columns["Cantidad"].DefaultCellStyle.BackColor = Color.LightBlue;
            dgvProductos.CellClick += dgvProductos_CellClick;
            dgvProductos.SelectionChanged += dgvProductos_SelectionChanged;


            dgvProductos.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dgvProductos.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2;


            btnBateria.Enabled = false;
            ActualizarEstadoBotonAceptar();

            dgvProductos.BorderStyle = BorderStyle.None;
            dgvProductos.BackgroundColor = Color.White;
            dgvProductos.RowHeadersVisible = false;
            dgvProductos.EnableHeadersVisualStyles = false;
            dgvProductos.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgvProductos.ColumnHeadersDefaultCellStyle.BackColor = Color.SkyBlue;
            dgvProductos.ColumnHeadersDefaultCellStyle.ForeColor = Color.Navy;
            dgvProductos.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvProductos.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProductos.ColumnHeadersHeight = 28;

            dgvProductos.DefaultCellStyle.BackColor = Color.White;
            dgvProductos.DefaultCellStyle.ForeColor = Color.Navy;
            dgvProductos.DefaultCellStyle.Font = new Font("Segoe UI", 10);
            dgvProductos.DefaultCellStyle.Padding = new Padding(3);
            dgvProductos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(230, 245, 255);
            dgvProductos.AlternatingRowsDefaultCellStyle.ForeColor = Color.Navy;

            dgvProductos.DefaultCellStyle.SelectionBackColor = Color.DeepSkyBlue;
            dgvProductos.DefaultCellStyle.SelectionForeColor = Color.White;

            dgvProductos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProductos.GridColor = Color.LightGray;
            dgvProductos.RowTemplate.Height = 32;
            dgvProductos.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvProductos.ClearSelection();
        }

        /// <summary>
        /// Calculars the total.
        /// </summary>
        private void CalcularTotal()
        {
            double acumulador = 0;
            foreach (DataGridViewRow row in dgvProductos.Rows)
            {
                if (row.Cells["subtotal"].Value != null)
                    acumulador += Convert.ToDouble(row.Cells["subtotal"].Value);
            }

            double rebaja = precioBateria;
            double total = acumulador - rebaja;

            txtSubtotal.Text = acumulador.ToString("N2");
            txtRebaja.Text = rebaja.ToString("N2");
            txtTotal.Text = (total < 0 ? 0 : total).ToString("N2");
        }

        /// <summary>
        /// Handles the TextChanged event of the txtExento control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void txtExento_TextChanged(object sender, EventArgs e) { }

        /// <summary>
        /// Handles the KeyPress event of the txtExento control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtExento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
                e.Handled = true;
        }

        /// <summary>
        /// Handles the Click event of the BtnAceptar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (ClsValidaciones.CampoVacio(txtCliente, "Cliente")) return;
            if (cmbPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Factura Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double totalFactura = Convert.ToDouble(txtTotal.Text);
            if (totalFactura < 0)
            {
                MessageBox.Show("El descuento por batería vieja no puede ser mayor al total de la compra.", "Error de Lógica", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            double montoExento = 0;
            if (!string.IsNullOrWhiteSpace(txtExento.Text))
            {
                if (!double.TryParse(txtExento.Text, out montoExento) || montoExento < 0)
                {
                    MessageBox.Show("El monto exento ingresado no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (montoExento > totalFactura)
                {
                    MessageBox.Show("El monto exento no puede ser mayor al total de la factura.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                ClsPasarUsuario objPU = new ClsPasarUsuario();
                ClsAgregarFactura objAF = new ClsAgregarFactura();
                ClsAgregarProductos objAP = new ClsAgregarProductos();

                int idUser = objPU.IdUsuario();
                int idPago = Convert.ToInt32(cmbPago.SelectedValue);
                int.TryParse(txtBateria.Text, out int bat);

                int idFactura = await objAF.AgregarFacturas(idUser, idCliente, idPago,
                                    DateTFecha.SelectionStart, bat, precioBateria);

                if (idFactura > 0)
                {
                    foreach (DataGridViewRow fila in dgvProductos.Rows)
                    {
                        if (fila.IsNewRow) continue;
                        int idPr = Convert.ToInt32(fila.Cells["id_producto"].Value);
                        int cant = Convert.ToInt32(fila.Cells["cantidad"].Value);
                        await objAP.GuardarProductoFactura(idFactura, idPr, cant);
                    }

                    DialogResult imprimir = MessageBox.Show(
                        "¿Desea imprimir la factura?", "Imprimir",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (imprimir == DialogResult.Yes)
                    {
                        if (chkNormal.Checked)
                        {
                            objAF.ImprimirFacturaNormal(
                                idFactura,
                                txtCliente.Text,
                                txtTotal.Text,
                                cmbPago.Text,
                                dgvProductos
                            );
                        }
                        else
                        {

                            objAF.ImprimirFactura(
                                idFactura,
                                txtCliente.Text,
                                DateTFecha.SelectionStart.ToShortDateString(),
                                txtSubtotal.Text,
                                txtRebaja.Text,
                                txtTotal.Text,
                                cmbPago.Text,
                                dgvProductos,
                                SG_BAMS.Login.Login.UsuarioLogueado,
                                chkGobierno.Checked,
                                montoExento,
                                rtnCliente
                            );
                        }
                    }

                    string formaPagoTexto = cmbPago.Text.ToLower();

                    if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito"))
                    {
                        string nombreCliente = txtCliente.Text.Trim();
                        string montoTotal = txtTotal.Text;
                        DateTime fechaVenta = DateTFecha.SelectionStart;

                        using (Información_Deudores frmInfo = new Información_Deudores(idFactura, nombreCliente, montoTotal, fechaVenta))
                        {
                            frmInfo.ShowDialog();
                        }
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error crítico al facturar: " + ex.Message);
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void BtnAgregar_Click(object sender, EventArgs e)
        {
            using (FacturaProducto frmProducto = new FacturaProducto())
            {
                frmProducto.FormularioFactura = this;

                if (frmProducto.ShowDialog() == DialogResult.OK)
                {
                    double precio = frmProducto.PrecioSeleccionado;
                    int stock = frmProducto.StockSeleccionado;

                    dgvProductos.Rows.Add(
                        idProducto,
                        nombresProductos,
                        cantidades,
                        precio,
                        (cantidades * precio),
                        stock);

                    CalcularTotal();
                    ActualizarEstadoBotonAceptar();
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the BtnEliminar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvProductos.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProductos.SelectedRows)
                    dgvProductos.Rows.Remove(row);

                if (dgvProductos.Rows.Count == 0)
                {
                    precioBateria = 0;
                    cantidadBateria = "0";
                    txtBateria.Text = "0";
                }

                CalcularTotal();
                ActualizarEstadoBotonAceptar();
            }
        }

        /// <summary>
        /// Handles the CellValidating event of the dgvProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellValidatingEventArgs"/> instance containing the event data.</param>
        private void dgvProductos_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvProductos.Columns[e.ColumnIndex].Name == "cantidad")
            {
                string valor = e.FormattedValue.ToString();
                if (!int.TryParse(valor, out int n) || n <= 0)
                {
                    MessageBox.Show("Cantidad inválida.");
                    e.Cancel = true;
                    return;
                }

                int stock = Convert.ToInt32(dgvProductos.Rows[e.RowIndex].Cells["stock_max"].Value);
                if (n > stock)
                {
                    MessageBox.Show($"Stock insuficiente. Máximo: {stock}");
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// Handles the CellValueChanged event of the dgvProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
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

        /// <summary>
        /// Handles the Click event of the btnBateria control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnBateria_Click(object sender, EventArgs e)
        {
            double subtotalActual = Convert.ToDouble(txtSubtotal.Text);
            using (BateriaVieja BV = new BateriaVieja(subtotalActual))
            {
                if (BV.ShowDialog() == DialogResult.OK)
                {
                    this.precioBateria = BV.TotalDineroBateria;
                    this.cantidadBateria = BV.TotalCantidadBateria;
                    txtBateria.Text = cantidadBateria;
                    CalcularTotal();
                }
            }
        }

        /// <summary>
        /// Actualizars the estado boton aceptar.
        /// </summary>
        private void ActualizarEstadoBotonAceptar()
        {
            bool tieneProductos = dgvProductos.Rows.Count > 0;
            BtnAceptar.Enabled = tieneProductos;
            btnBateria.Enabled = tieneProductos;
        }

        /// <summary>
        /// Handles the Click event of the BtnCancelar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnCancelar_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Handles the CheckedChanged event of the chkGobierno control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkGobierno_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGobierno.Checked)
            {
                txtExento.Enabled = false;
                txtExento.Text = "0";
            }
            else
            {
                txtExento.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the DateChanged event of the DateTFecha control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DateRangeEventArgs"/> instance containing the event data.</param>
        private void DateTFecha_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the label5 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label5_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the CheckedChanged event of the chkNormal control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void chkNormal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNormal.Checked)
            {
                txtExento.ReadOnly = true;
                txtExento.Text = "0";
            }
            else
            {
                txtExento.ReadOnly = false;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgvProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Columns[e.ColumnIndex].Name == "cantidad")
            {
                dgvProductos.BeginEdit(true);
            }
        }

        /// <summary>
        /// Handles the SelectionChanged event of the dgvProductos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void dgvProductos_SelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewCell cell in dgvProductos.SelectedCells)
            {
                if (dgvProductos.Columns[cell.ColumnIndex].Name != "cantidad")
                    cell.Selected = false;
            }
        }
    }
}