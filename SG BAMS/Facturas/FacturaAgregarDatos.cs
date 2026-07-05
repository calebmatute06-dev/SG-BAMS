using Microsoft.Data.SqlClient;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using SG_BAMS.Cliente;
using SG_BAMS.Deudores;
using SG_BAMS.Facturas;
using SG_BAMS.Facturas.DTO;
using SG_BAMS.Login;
using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para agregar una nueva factura con sus productos, descuentos y forma de pago.
    /// </summary>
    public partial class FacturaAgregarDatos : Form
    {
        private int idCliente, cantidades;
        public int idProducto;
        private string nombresProductos, cantidadBateria;
        private double precioBateria;
        private string rtnCliente;

        private static readonly CultureInfo CI = CultureInfo.InvariantCulture;

        private PlaceholderComboBox phPago;
        private PlaceholderTextBox phExento;

        /// <summary>
        /// Acumula los caracteres enviados por el lector de código de barras
        /// hasta que se detecta un Enter que dispara la búsqueda del producto.
        /// </summary>
        private StringBuilder _bufferScanner = new StringBuilder();

        /// <summary>
        /// Registra el momento de la última tecla recibida para distinguir
        /// entre entrada del scanner (menor a 100ms) y escritura manual (mayor a 100ms).
        /// </summary>
        private DateTime _ultimaTecla = DateTime.MinValue;

        public FacturaAgregarDatos(string cliente, int idCli, string rtn = "Sin RTN")
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtCliente.Text = cliente;
            idCliente = idCli;
            rtnCliente = string.IsNullOrWhiteSpace(rtn) ? "Sin RTN" : rtn;
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
            cantidadBateria = "0";
            precioBateria = 0;
            rtnCliente = "Sin RTN";
        }

        private async Task LlenarComboPago()
        {
            ClsFactura AF = new ClsFactura();
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

        private async void FacturaAgregarDatos_Load(object sender, EventArgs e)
        {
            await LlenarComboPago();

            phPago = new PlaceholderComboBox(cmbPago, "Seleccione una forma de pago");
            phExento = new PlaceholderTextBox(txtExento, "0");

            DateTFecha.Enabled = false;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            dgvProductos.Columns.Clear();
            dgvProductos.Columns.Add("id_producto", "Código");
            dgvProductos.Columns.Add("nombre_producto", "Nombre");
            dgvProductos.Columns.Add("cantidad", "Cantidad");
            dgvProductos.Columns.Add("precio", "Precio");
            dgvProductos.Columns.Add("subtotal", "Subtotal");
            dgvProductos.Columns.Add("stock_max", "StockMax");
            dgvProductos.Columns["stock_max"].Visible = false;

            dgvProductos.CellFormatting += (s, ev) =>
            {
                if (ev.RowIndex < 0 || ev.Value == null) return;
                string col = dgvProductos.Columns[ev.ColumnIndex].Name;
                if ((col == "precio" || col == "subtotal") &&
                   decimal.TryParse(ev.Value.ToString(), NumberStyles.Any, CI, out decimal monto))
                {
                    ev.Value = $"L. {monto:N2}";
                    ev.FormattingApplied = true;
                }
            };

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
            dgvProductos.RowsAdded += dgvProductos_RowsAdded;
            dgvProductos.CellClick += dgvProductos_CellClick;

            dgvProductos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private double ParsearMonto(string texto)
        {
            string limpio = texto.Replace("L.", "").Replace(",", "").Trim();
            return double.TryParse(limpio, NumberStyles.Any, CI, out double resultado) ? resultado : 0;
        }

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

            txtSubtotal.Text = $"L. {acumulador.ToString("N2", CI)}";
            txtRebaja.Text = $"L. {rebaja:N2}";
            txtTotal.Text = $"L. {(total < 0 ? 0 : total):N2}";
        }

        private void txtExento_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtExento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
                e.Handled = true;
            if (e.KeyChar == '.' && ((TextBox)sender).Text.Contains('.'))
                e.Handled = true;
        }

        private async Task<bool> CrearDeudaManual(int idFactura, int idCliente, double montoTotal, DateTime fechaVenta)
        {
            ClsRepositorioBaseDatos conexion = new ClsRepositorioBaseDatos();
            try
            {
                conexion.AbrirConexion();
                using (SqlCommand cmd = new SqlCommand("sp_Deuda_CrearOActualizar", conexion.Conectar))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@idFactura", idFactura);
                    cmd.Parameters.AddWithValue("@idCliente", idCliente);
                    cmd.Parameters.AddWithValue("@monto", montoTotal);
                    cmd.Parameters.AddWithValue("@fechaVenta", fechaVenta);
                    await cmd.ExecuteNonQueryAsync();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear/actualizar deuda: " + ex.Message);
                return false;
            }
            finally
            {
                conexion.Cerrar();
            }
        }

        private async void BtnAceptar_Click(object sender, EventArgs e)
        {
            if (ClsValidaciones.CampoVacio(txtCliente, "Cliente")) return;

            if (phPago.IsPlaceholderActive || cmbPago.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione una forma de pago.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (dgvProductos.Rows.Count == 0)
            {
                MessageBox.Show("Debe agregar al menos un producto.", "Factura Vacía", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            double totalFactura = ParsearMonto(txtTotal.Text);
            if (totalFactura < 0)
            {
                MessageBox.Show("El descuento por batería vieja no puede ser mayor al total de la compra.", "Error de Lógica", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string montoExentoTexto = phExento.GetRealValue().Trim();
            double montoExento = 0;
            if (!string.IsNullOrWhiteSpace(montoExentoTexto))
            {
                if (!double.TryParse(montoExentoTexto, NumberStyles.Any, CI, out montoExento) || montoExento < 0)
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
                ClsFactura objAFP = new ClsFactura();

                int idUser = objPU.IdUsuario();
                int idPago = Convert.ToInt32(cmbPago.SelectedValue);
                int.TryParse(txtBateria.Text, out int bat);

                string formaPagoTexto = cmbPago.Text.ToLower();

                if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito") || formaPagoTexto.Contains("efectivo") || formaPagoTexto.Contains("tarjeta"))
                {
                    ClsDeudas objDeudas = new ClsDeudas();
                    bool tieneDeudaActiva = await objDeudas.ClienteTieneDeudaActiva(idCliente);

                    if (tieneDeudaActiva)
                    {
                        DataTable dtDeudas = await Task.Run(() => objDeudas.ObtenerDeudasPorCliente(idCliente));

                        string detalleDeudas = "";
                        if (dtDeudas != null && dtDeudas.Rows.Count > 0)
                        {
                            foreach (DataRow row in dtDeudas.Rows)
                            {
                                int idDeuda = Convert.ToInt32(row["IdDeuda"]);
                                decimal saldo = Convert.ToDecimal(row["Saldo"]);
                                string fechaInicio = Convert.ToDateTime(row["FechaInicio"]).ToString("dd/MM/yyyy");
                                string fechaFin = Convert.ToDateTime(row["FechaFin"]).ToString("dd/MM/yyyy");

                                detalleDeudas += $"• Deuda #{idDeuda}  |  Desde: {fechaInicio}  →  Hasta: {fechaFin}\n";
                                detalleDeudas += $"  Saldo pendiente: L {saldo:N2}\n\n";
                            }
                        }

                        DialogResult respuesta = MessageBox.Show(
                            $"El cliente '{txtCliente.Text.Trim()}' tiene una deuda activa:\n\n" +
                            $"{detalleDeudas}" +
                            "¿Desea generar la factura de todas formas?",
                            "Advertencia de Crédito",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (respuesta == DialogResult.No)
                            return;
                    }
                }

                // --- Armado del DTO con todos los datos de la pantalla ---
                FacturaDTO facturaDTO = new FacturaDTO
                {
                    IdUsuario = idUser,
                    IdCliente = idCliente,
                    IdFormaPago = idPago,
                    Fecha = DateTFecha.Value,
                    CantidadBateriaVieja = bat,
                    RebajaBateria = precioBateria,
                    MontoExento = montoExento,
                    EsGobierno = chkGobierno.Checked,
                    RtnCliente = rtnCliente,
                    NombreCliente = txtCliente.Text
                };

                foreach (DataGridViewRow fila in dgvProductos.Rows)
                {
                    if (fila.IsNewRow) continue;
                    facturaDTO.Detalle.Add(new DetalleDTO
                    {
                        IdProducto = Convert.ToInt32(fila.Cells["id_producto"].Value),
                        NombreProducto = fila.Cells["nombre_producto"].Value.ToString(),
                        Cantidad = Convert.ToInt32(fila.Cells["cantidad"].Value),
                        Precio = Convert.ToDouble(fila.Cells["precio"].Value)
                    });
                }

                // --- Un solo objeto viaja a la capa de datos ---
                int idFactura = await objAFP.AgregarFacturas(facturaDTO);

                if (idFactura > 0)
                {
                    await objAFP.GuardarDetalleFactura(idFactura, facturaDTO.Detalle);

                    DialogResult imprimir = MessageBox.Show(
                        "¿Desea imprimir la factura?", "Imprimir",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (imprimir == DialogResult.Yes)
                    {
                        objAFP.ImprimirFactura(
                            idFactura,
                            facturaDTO.NombreCliente,
                            facturaDTO.Fecha.ToShortDateString(),
                            ParsearMonto(txtSubtotal.Text).ToString("N2", CI),
                            ParsearMonto(txtRebaja.Text).ToString("N2", CI),
                            ParsearMonto(txtTotal.Text).ToString("N2", CI),
                            cmbPago.Text,
                            dgvProductos,
                            SG_BAMS.Login.Login.UsuarioLogueado,
                            facturaDTO.EsGobierno,
                            facturaDTO.MontoExento,
                            facturaDTO.RtnCliente
                        );
                    }

                    if (formaPagoTexto.Contains("crédito") || formaPagoTexto.Contains("credito"))
                    {
                        bool deudaCreada = await CrearDeudaManual(idFactura, facturaDTO.IdCliente, facturaDTO.Total, facturaDTO.Fecha);

                        if (deudaCreada)
                        {
                            MessageBox.Show("Redirigiendo a Deudores...", "SG-BAMS",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                           
                            for (int i = Application.OpenForms.Count - 1; i >= 0; i--)
                            {
                                var frm = Application.OpenForms[i];
                                if (frm is FacturasAdm || frm is FacturasEmp || frm is ClienteAgregar || frm is ClienteExistente)
                                {
                                    frm.Close();
                                }
                            }

                            this.Hide();

                            if (ClsLogin.RolUsuario == 1)
                                new DeudoresAdmin().Show();
                            else
                                new Deudores_Emp().Show();

                            this.Close();
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Error al crear la deuda.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
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

                    dgvProductos.ClearSelection();
                    CalcularTotal();
                    ActualizarEstadoBotonAceptar();
                }
            }
        }

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

                dgvProductos.ClearSelection();
                CalcularTotal();
                ActualizarEstadoBotonAceptar();
            }
        }

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
            double subtotalActual = ParsearMonto(txtSubtotal.Text);
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

        private void ActualizarEstadoBotonAceptar()
        {
            bool tieneProductos = dgvProductos.Rows.Count > 0;
            BtnAceptar.Enabled = tieneProductos;
            btnBateria.Enabled = tieneProductos;
        }

        private void BtnCancelar_Click(object sender, EventArgs e) => this.Close();

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
                if (string.IsNullOrWhiteSpace(txtExento.Text))
                    txtExento.Text = "";
            }
        }

        private void DateTFecha_DateChanged(object sender, DateRangeEventArgs e) { }

        private void label5_Click(object sender, EventArgs e) { }

        private void dgvProductos_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && dgvProductos.Columns[e.ColumnIndex].Name == "cantidad")
            {
                dgvProductos.BeginEdit(true);
            }
        }

        private void dgvProductos_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            if (dgvProductos.Columns.Contains("cantidad"))
            {
                dgvProductos.Rows[e.RowIndex].Cells["cantidad"].Style.BackColor = Color.LightBlue;
            }
        }

        private void txtExento_TextChanged_1(object sender, EventArgs e)
        {
        }

        private async Task BuscarYAgregarProductoPorCodigo(string codigo)
        {
            try
            {
                ClsFactura objAP = new ClsFactura();
                DataRow prod = await objAP.ObtenerProductoPorCodigoBarra(codigo);

                if (prod == null)
                {
                    MessageBox.Show($"El producto con código [{codigo}] no existe o no esta agregado aún.",
                        "BAMS", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idProd = Convert.ToInt32(prod["id_producto"]);
                string nombre = prod["nombre_producto"].ToString();
                double precio = Convert.ToDouble(prod["precio_venta"]);
                int stock = Convert.ToInt32(prod["stock"]);

                foreach (DataGridViewRow row in dgvProductos.Rows)
                {
                    if (row.IsNewRow) continue;
                    if (Convert.ToInt32(row.Cells["id_producto"].Value) == idProd)
                    {
                        MessageBox.Show("Este producto ya fue agregado. Puede cambiar la cantidad directamente en la tabla.",
                            "Producto Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                if (stock <= 0)
                {
                    MessageBox.Show("El producto no tiene stock disponible.",
                        "Sin Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                dgvProductos.Rows.Add(idProd, nombre, 1, precio, precio, stock);
                dgvProductos.ClearSelection();
                dgvProductos.Rows[dgvProductos.Rows.Count - 1].Selected = true;
                CalcularTotal();
                ActualizarEstadoBotonAceptar();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al buscar producto por código: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys key = keyData & Keys.KeyCode;

            if (this.ActiveControl is TextBox || this.ActiveControl is ComboBox)
                return base.ProcessCmdKey(ref msg, keyData);

            if ((key >= Keys.D0 && key <= Keys.D9) ||
                (key >= Keys.A && key <= Keys.Z) ||
                (key >= Keys.NumPad0 && key <= Keys.NumPad9))
            {
                TimeSpan intervalo = DateTime.Now - _ultimaTecla;
                if (intervalo.TotalMilliseconds > 100)
                    _bufferScanner.Clear();

                _ultimaTecla = DateTime.Now;

                string caracter = new KeysConverter().ConvertToString(key);
                _bufferScanner.Append(caracter);
                return true;
            }

            if (key == Keys.Enter)
            {
                _ultimaTecla = DateTime.Now;
                string codigo = _bufferScanner.ToString().Trim();
                _bufferScanner.Clear();

                if (!string.IsNullOrEmpty(codigo))
                    _ = BuscarYAgregarProductoPorCodigo(codigo);

                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}