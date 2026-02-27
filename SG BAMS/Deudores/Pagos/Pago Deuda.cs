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

                // Asignamos la tabla
                cmbDeudores.DataSource = dt;
                cmbDeudores.SelectedIndex = -1;
            }
        }

        private void Pago_Deuda_Load(object sender, EventArgs e)
        {
            // SI RECIBIMOS UN ID: Buscamos la posición exacta en el DataTable
            if (idDeudaRecibido > 0 && cmbDeudores.DataSource != null)
            {
                DataTable dt = (DataTable)cmbDeudores.DataSource;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Comparamos el ID de la fila con el ID que recibimos del grid
                    if (Convert.ToInt32(dt.Rows[i]["ID"]) == idDeudaRecibido)
                    {
                        cmbDeudores.SelectedIndex = i; // Forzamos la posición exacta
                        cmbDeudores.Enabled = false;   // Bloqueamos para evitar errores
                        return; // Ya lo encontramos, salimos del ciclo
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
            // Verificamos que el SelectedValue sea el correcto
            if (cmbDeudores.SelectedValue != null && decimal.TryParse(txtMonto.Text, out decimal montoPago))
            {
                int idDeudaFinal = Convert.ToInt32(cmbDeudores.SelectedValue);

                bool ok = await objetoDeudas.InsertarPago(idDeudaFinal, montoPago, DateTime.Now);

                if (ok)
                {
                    MessageBox.Show("¡Pago registrado correctamente!", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un deudor y escribe un monto válido.");
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e) => this.Close();

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}