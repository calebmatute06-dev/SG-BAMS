using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para registrar pagos de deudas.
    /// Única responsabilidad de esta clase: coordinar la interacción con el usuario.
    /// El acceso a datos se recibe por inyección (IDeudasRepository) y el cálculo del
    /// resumen de la deuda se delega en ResumenPagoService, dejando aquí únicamente
    /// el formateo final (ver auditoría SOLID, hallazgos PGD01-PGD03).
    /// </summary>
    public partial class Pago_Deuda : Form
    {
        private readonly IDeudasRepository deudasRepositorio;
        private readonly ResumenPagoService resumenService;

        private string nombreRecibido = "";
        private int idDeudaRecibido = 0;

        private PlaceholderTextBox phMonto;
        private PlaceholderComboBox phDeudores;

        /// <summary>
        /// Inicializa una nueva instancia del formulario <see cref="Pago_Deuda"/> con datos
        /// específicos, recibiendo su dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="deudasRepositorio">Acceso a datos de pagos/consultas de deudas.</param>
        /// <param name="nombre">Nombre del deudor.</param>
        /// <param name="idDeuda">Identificador de la deuda.</param>
        public Pago_Deuda(IDeudasRepository deudasRepositorio, string nombre = "", int idDeuda = 0)
        {
            InitializeComponent();
            this.deudasRepositorio = deudasRepositorio;
            resumenService = new ResumenPagoService(deudasRepositorio);

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
            DataTable dtDeudores = deudasRepositorio.ObtenerDeudoresActivos();

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

        /// <summary>
        /// Obtiene el resumen ya calculado (ResumenPagoService) y solo se encarga de
        /// darle formato de texto para el ListBox.
        /// </summary>
        private async Task CargarProductosDeudor(int idDeuda)
        {
            lstProductos.Items.Clear();

            ResumenPagoDeudaDTO resumen = await Task.Run(() => resumenService.ObtenerResumen(idDeuda));

            if (resumen.Productos.Count == 0)
            {
                lstProductos.Items.Add("Sin productos registrados.");
                return;
            }

            foreach (var linea in resumen.Productos)
            {
                string texto = $"{linea.Cantidad}x {linea.Producto,-25}  |  L {linea.PrecioUnitario:N2} c/u    |  Total: L {linea.Total:N2}";
                lstProductos.Items.Add(texto);
            }

            lstProductos.Items.Add($"{"Subtotal productos:",-35}     L {resumen.Subtotal:N2}");

            if (resumen.Descuento > 0)
                lstProductos.Items.Add($"{"Descuento batería:",-35}     -L {resumen.Descuento:N2}");

            lstProductos.Items.Add($"{"Total de compra:",-35}       L {resumen.TotalCompra:N2}");

            lstProductos.Items.Add("──────────────────────────────────────────────────────────────────────────────────────────────────────────");

            lstProductos.Items.Add($"{"Saldo pagado:",-35}   L {resumen.SaldoPagado:N2}");
            lstProductos.Items.Add($"{"Saldo pendiente:",-35}  L {resumen.SaldoPendiente:N2}");
        }

        /// <summary>
        /// Maneja el evento Click del botón Aceptar.
        /// Orquesta los pasos independientes: validar selección, validar monto,
        /// validar saldo suficiente y registrar el pago (ver hallazgo PGD02).
        /// </summary>
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (!ValidarDeudorSeleccionado())
                return;

            int idDeudaFinal = Convert.ToInt32(cmbDeudores.SelectedValue);

            DataRow detalle = await Task.Run(() => deudasRepositorio.ObtenerSaldoDetalle(idDeudaFinal));
            decimal saldoPendiente = detalle != null ? Convert.ToDecimal(detalle["SaldoPendiente"]) : 0;

            if (!ValidarMonto(saldoPendiente, out decimal montoPago))
                return;

            if (!ValidarSaldoSuficiente(saldoPendiente, montoPago))
                return;

            await RegistrarPago(idDeudaFinal, montoPago);
        }

        /// <summary>
        /// Valida que se haya seleccionado un deudor válido en el combo.
        /// </summary>
        private bool ValidarDeudorSeleccionado()
        {
            if (phDeudores.IsPlaceholderActive || cmbDeudores.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un deudor válido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeudores.Focus();
                return false;
            }
            return true;
        }

        /// <summary>
        /// Valida el formato del monto ingresado y la regla de negocio de monto mínimo.
        /// </summary>
        private bool ValidarMonto(decimal deuda, out decimal montoPago)
        {
            montoPago = 0;
            string montoReal = phMonto.GetRealValue().Trim();

            bool montoValido;
            using (var tempMonto = new KryptonTextBox())
            {
                tempMonto.Text = montoReal;
                montoValido = ClsValidaciones.EsNumeroDecimalValido(tempMonto, "El monto", out montoPago);
            }

            if (!montoValido) return false;   // Línea duplicada eliminada

            if (deuda > 100)
            {
                if (montoPago < 100)
                {
                    MessageBox.Show("El monto debe ser mayor o igual a cien.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonto.Focus();
                    return false;
                }
            }
            else
            {
                if (montoPago <= 0)
                {
                    MessageBox.Show("El monto debe ser mayor a cero.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMonto.Focus();
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Valida que el monto a pagar no exceda el saldo pendiente actual de la deuda.
        /// </summary>
        private bool ValidarSaldoSuficiente(decimal saldoPendiente, decimal montoPago)
        {
            if (montoPago > saldoPendiente)
            {
                MessageBox.Show($"El monto ingresado (L {montoPago:N2}) supera el saldo pendiente (L {saldoPendiente:N2}).",
                                "Error de saldo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Registra el pago ya validado y cierra el formulario.
        /// </summary>
        private async Task RegistrarPago(int idDeuda, decimal montoPago)
        {
            bool transaccionOk = await deudasRepositorio.InsertarPago(idDeuda, montoPago, DateTime.Now);

            if (transaccionOk)
            {
                MessageBox.Show("¡Pago registrado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("No se pudo registrar el pago. Intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón Cancelar.
        /// </summary>
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