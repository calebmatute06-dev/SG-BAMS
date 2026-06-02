using Krypton.Toolkit;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Pago_Deuda : Form
    {
        private ClsDeudas objetoDeudas = new ClsDeudas();
        private string nombreRecibido = "";
        private int idDeudaRecibido = 0;

        // Placeholders
        private PlaceholderTextBox phMonto;
        private PlaceholderComboBox phDeudores;

        public Pago_Deuda()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            RegistrarEventos();
            ConfigurarFormulario();
        }

        public Pago_Deuda(string nombre, int idDeuda)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            RegistrarEventos();
            this.nombreRecibido = nombre;
            this.idDeudaRecibido = idDeuda;
            ConfigurarFormulario();
        }

        private void RegistrarEventos()
        {
            if (this.txtMonto != null)
            {
                this.txtMonto.KeyPress += (s, e) => ClsValidaciones.PermitirNumerosYDecimales(s, e);
            }
        }

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

        private void Pago_Deuda_Load(object sender, EventArgs e)
        {
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            // Inicializar placeholders
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

        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            // Validar selección real en ComboBox (no placeholder)
            if (phDeudores.IsPlaceholderActive || cmbDeudores.SelectedValue == null)
            {
                MessageBox.Show("Por favor, seleccione un deudor válido.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cmbDeudores.Focus();
                return;
            }

            // Obtener valor real del monto (sin placeholder)
            string montoReal = phMonto.GetRealValue().Trim();
            if (string.IsNullOrWhiteSpace(montoReal) ||
                !decimal.TryParse(montoReal, out decimal montoPago) ||
                montoPago <= 0)
            {
                MessageBox.Show("Por favor, escriba un monto válido mayor a cero.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void btnCancelar_Click(object sender, EventArgs e) => this.Close();

        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e) { }
        private void label2_Click(object sender, EventArgs e) { }
    }
}