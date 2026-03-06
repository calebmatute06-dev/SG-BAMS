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

        // Constructor para cuando abres el formulario desde cero
        public Pago_Deuda()
        {
            InitializeComponent();
            ConfigurarFormulario();
        }

        // Constructor maestro para cuando haces doble clic en el grid
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
                // IMPORTANTE: Limpiar cualquier rastro previo
                cmbDeudores.DataSource = null;
                cmbDeudores.Items.Clear();

                // Definimos las columnas primero
                cmbDeudores.ValueMember = "ID";
                cmbDeudores.DisplayMember = "ClienteDetalle";

                cmbDeudores.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDeudores.AutoCompleteSource = AutoCompleteSource.ListItems;
                // Asignamos la tabla
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
            // SI SOLO HAY NOMBRE (Búsqueda manual)
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

            // 1. Obtener el saldo actual de la deuda (debes tener este método)
            decimal saldoPendiente = await objetoDeudas.ObtenerSaldo(idDeudaFinal);

            // 2. Validación: el pago no puede superar la deuda
            if (montoPago > saldoPendiente)
            {
                MessageBox.Show($"El monto ingresado ({montoPago:C}) supera el saldo pendiente ({saldoPendiente:C}).",
                                "Error de validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 3. Proceder con el pago
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
