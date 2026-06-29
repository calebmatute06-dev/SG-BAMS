using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para registrar pagos de deudas.
    /// </summary>
    public partial class Pago_Deuda : Form
    {
        private ClsDeudas objetoDeudas = new ClsDeudas();
        private string nombreRecibido = "";
        private int idDeudaRecibido = 0;

        private PlaceholderTextBox phMonto;
        private PlaceholderComboBox phDeudores;

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="Pago_Deuda"/>.
        /// </summary>
        public Pago_Deuda()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            RegistrarEventos();
            ConfigurarFormulario();
        }

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="Pago_Deuda"/> con datos específicos.
        /// </summary>
        /// <param name="nombre">Nombre del deudor.</param>
        /// <param name="idDeuda">Identificador de la deuda.</param>
        public Pago_Deuda(string nombre, int idDeuda)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            RegistrarEventos();
            this.nombreRecibido = nombre;
            this.idDeudaRecibido = idDeuda;
            ConfigurarFormulario();
        }

        /// <summary>
        /// Registra los eventos del formulario.
        /// </summary>
        private void RegistrarEventos()
        {
            if (this.txtMonto != null)
            {
                this.txtMonto.KeyPress += (s, e) => ClsValidaciones.PermitirNumerosYDecimales(s, e);
            }
        }

        /// <summary>
        /// Configura el ComboBox de deudores cargando los datos.
        /// </summary>
        private void ConfigurarFormulario()
        {
            DataTable dtDeudores = objetoDeudas.ObtenerDeudoresActivos();

            if (dtDeudores != null && dtDeudores.Rows.Count > 0)
            {
                cmbDeudores.DataSource = null;
                cmbDeudores.Items.Clear();

                cmbDeudores.ValueMember = "ID";
                cmbDeudores.DisplayMember = "ClienteDetalle";

                cmbDeudores.DropDownStyle = ComboBoxStyle.DropDownList;
                cmbDeudores.AutoCompleteMode = AutoCompleteMode.None;

                if (cmbDeudores is KryptonComboBox kryptonCombo)
                {
                    kryptonCombo.StateCommon.ComboBox.Back.Color1 = Color.SkyBlue;
                    kryptonCombo.StateCommon.ComboBox.Border.Color1 = Color.SkyBlue;
                    kryptonCombo.StateCommon.ComboBox.Content.Color1 = Color.Navy;
                    kryptonCombo.StateCommon.ComboBox.Content.Font = new Font("Segoe UI", 10, FontStyle.Bold);
                }

                cmbDeudores.DataSource = dtDeudores;
                cmbDeudores.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Maneja el evento Load del formulario.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Datos del evento.</param>
        private async void Pago_Deuda_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            phMonto = new PlaceholderTextBox(txtMonto, "Cantidad deseada a pagar");
            phDeudores = new PlaceholderComboBox(cmbDeudores, "Seleccione un nombre");

            if (idDeudaRecibido > 0 && cmbDeudores.DataSource != null)
            {
                DataTable dtDatos = (DataTable)cmbDeudores.DataSource;
                for (int i = 0; i < dtDatos.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDatos.Rows[i]["ID"]) == idDeudaRecibido)
                    {
                        cmbDeudores.SelectedIndex = i;
                        cmbDeudores.Enabled = false;

                        if (cmbDeudores is KryptonComboBox kc)
                        {
                            kc.StateDisabled.ComboBox.Back.Color1 = Color.SkyBlue;
                            kc.StateDisabled.ComboBox.Content.Color1 = Color.Navy;
                        }
                        await CargarProductosDeudor(idDeudaRecibido);
                        return;
                    }
                }
            }
            else if (!string.IsNullOrEmpty(nombreRecibido))
            {
                int indiceEncontrado = cmbDeudores.FindStringExact(nombreRecibido);
                if (indiceEncontrado == -1) indiceEncontrado = cmbDeudores.FindString(nombreRecibido);
                cmbDeudores.SelectedIndex = indiceEncontrado;
            }
        }
        private async Task CargarProductosDeudor(int idDeuda)
        {
            lstProductos.Items.Clear();

            DataTable dtProductos = await Task.Run(() => objetoDeudas.ObtenerProductosPorDeuda(idDeuda));

            if (dtProductos == null || dtProductos.Rows.Count == 0)
            {
                lstProductos.Items.Add("Sin productos registrados.");
                return;
            }

            decimal totalProductos = 0;

            foreach (DataRow row in dtProductos.Rows)
            {
                string producto = row["Producto"].ToString();
                int cantidad = Convert.ToInt32(row["Cantidad"]);
                decimal precio = Convert.ToDecimal(row["PrecioUnitario"]);
                decimal total = Convert.ToDecimal(row["total"]);

                totalProductos += total;

                string linea = $"{cantidad}x {producto,-25}  |  L {precio:N2} c/u    |  Total: L {total:N2}";
                lstProductos.Items.Add(linea);
            }

            decimal saldoPendiente = await objetoDeudas.ObtenerSaldo(idDeuda);
            decimal saldoPagado = totalProductos - saldoPendiente;

            lstProductos.Items.Add($"{"Total de compra:",-35}  L {totalProductos:N2}");
            lstProductos.Items.Add("──────────────────────────────────────────────────────────────────────────────────────────────────────────");
            lstProductos.Items.Add($"{"Saldo pagado:",-35}  L {saldoPagado:N2}");
            lstProductos.Items.Add($"{"Saldo pendiente:",-35}  L {saldoPendiente:N2}");
        }
        /// <summary>
        /// Maneja el evento Click del botón Aceptar.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Datos del evento.</param>
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (phDeudores.IsPlaceholderActive || cmbDeudores.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un deudor válido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeudores.Focus();
                return;
            }

            string montoReal = phMonto.GetRealValue().Trim();

            bool montoValido;
            decimal montoPago = 0;
            using (var tempMonto = new KryptonTextBox())
            {
                tempMonto.Text = montoReal;
                montoValido = ClsValidaciones.EsNumeroDecimalValido(tempMonto, "El monto", out montoPago);
            }

            if (!montoValido) return;

            if (montoPago < 100)
            {
                MessageBox.Show("El monto debe ser mayor a cien.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMonto.Focus();
                return;
            }

            int idDeudaFinal = Convert.ToInt32(cmbDeudores.SelectedValue);
            decimal saldoPendiente = await objetoDeudas.ObtenerSaldo(idDeudaFinal);

            if (montoPago > saldoPendiente)
            {
                MessageBox.Show($"El monto ingresado (L {montoPago:N2}) supera el saldo pendiente (L {saldoPendiente:N2}).",
                                "Error de saldo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool transaccionOk = await objetoDeudas.InsertarPago(idDeudaFinal, montoPago, DateTime.Now);

            if (transaccionOk)
            {
                MessageBox.Show("¡Pago registrado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón Cancelar.
        /// </summary>
        /// <param name="sender">Origen del evento.</param>
        /// <param name="e">Datos del evento.</param>
        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Maneja el evento KeyPress del campo txtMonto (vacío, pero necesario para evitar eventos no deseados).
        /// </summary>
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e) { }

        /// <summary>
        /// Maneja el evento Click del label2 (sin implementación).
        /// </summary>
        private void label2_Click(object sender, EventArgs e) { }
    }
}