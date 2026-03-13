using Krypton.Toolkit;
using System;
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS
{
    public partial class Pago_Deuda : Form
    {
        private ClsDeudas objetoDeudas = new ClsDeudas();
        private string nombreRecibido = "";
        private int idDeudaRecibido = 0;

        public Pago_Deuda()
        {
            InitializeComponent();
            ConfigurarFormulario();

        }

        public Pago_Deuda(string nombre, int idDeuda)
        {
            InitializeComponent();
            this.nombreRecibido = nombre;
            this.idDeudaRecibido = idDeuda;
            ConfigurarFormulario();
        }

        private void ConfigurarFormulario()
        {
            DataTable dt = objetoDeudas.ObtenerDeudoresActivos();

            if (dt != null && dt.Rows.Count > 0)
            {
                cmbDeudores.DataSource = null;
                cmbDeudores.Items.Clear();

                cmbDeudores.ValueMember = "ID";
                cmbDeudores.DisplayMember = "ClienteDetalle";

                cmbDeudores.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDeudores.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbDeudores.DataSource = dt;
                cmbDeudores.SelectedIndex = -1;
            }
        }

        private void Pago_Deuda_Load(object sender, EventArgs e)
        {
            if (idDeudaRecibido > 0 && cmbDeudores.DataSource != null)
            {
                DataTable dt = (DataTable)cmbDeudores.DataSource;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dt.Rows[i]["ID"]) == idDeudaRecibido)
                    {
                        cmbDeudores.SelectedIndex = i; 
                        cmbDeudores.Enabled = false;   
                        return; 
                    }
                }
            }
            else if (!string.IsNullOrEmpty(nombreRecibido))
            {
                int index = cmbDeudores.FindStringExact(nombreRecibido);
                if (index == -1) index = cmbDeudores.FindString(nombreRecibido);
                cmbDeudores.SelectedIndex = index;
            }
        }

        private async void kryptonButton3_Click(object sender, EventArgs e)
        {
            if (cmbDeudores.SelectedValue == null || !decimal.TryParse(txtMonto.Text, out decimal montoPago))
            {
                MessageBox.Show("Por favor, selecciona un deudor y escribe un monto válido.");
                return;
            }

            int idDeudaFinal = Convert.ToInt32(cmbDeudores.SelectedValue);

            decimal saldoPendiente = await objetoDeudas.ObtenerSaldo(idDeudaFinal);

            if (montoPago > saldoPendiente)
            {
                MessageBox.Show($"El monto ingresado ({montoPago:C}) supera el saldo pendiente ({saldoPendiente:C}).",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool ok = await objetoDeudas.InsertarPago(idDeudaFinal, montoPago, DateTime.Now);

            if (ok)
            {
                MessageBox.Show("¡Pago registrado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e) => this.Close();

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
