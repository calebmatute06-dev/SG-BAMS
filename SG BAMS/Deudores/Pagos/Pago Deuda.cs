using Krypton.Toolkit;
using System;
using System.Data;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class Pago_Deuda : Form
    {
        /// <summary>
        /// El objeto deudas
        /// </summary>
        private ClsDeudas objetoDeudas = new ClsDeudas();
        /// <summary>
        /// El nombre recibido
        /// </summary>
        private string nombreRecibido = "";
        /// <summary>
        /// El identificador de deuda recibido
        /// </summary>
        private int idDeudaRecibido = 0;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Pago_Deuda"/>.
        /// </summary>
        public Pago_Deuda()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            RegistrarEventos();
            ConfigurarFormulario();
        }

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="Pago_Deuda"/>.
        /// </summary>
        /// <param name="nombre">El nombre.</param>
        /// <param name="idDeuda">El identificador de la deuda.</param>
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
        /// Registra los eventos.
        /// </summary>
        private void RegistrarEventos()
        {
            if (this.txtMonto != null)
            {

                this.txtMonto.KeyPress += (s, e) => ClsValidaciones.PermitirNumerosYDecimales(s, e);
            }
        }

        /// <summary>
        /// Configura el formulario.
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

                cmbDeudores.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbDeudores.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbDeudores.DataSource = dtDeudores;
                cmbDeudores.SelectedIndex = -1;
            }
        }

        /// <summary>
        /// Maneja el evento Load del control Pago_Deuda.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void Pago_Deuda_Load(object sender, EventArgs e)
        {
            if (idDeudaRecibido > 0 && cmbDeudores.DataSource != null)
            {
                DataTable dtDatos = (DataTable)cmbDeudores.DataSource;

                for (int i = 0; i < dtDatos.Rows.Count; i++)
                {
                    if (Convert.ToInt32(dtDatos.Rows[i]["ID"]) == idDeudaRecibido)
                    {
                        cmbDeudores.SelectedIndex = i;
                        cmbDeudores.Enabled = false;
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
        /// Maneja el evento KeyPress del control txtMonto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtMonto_KeyPress(object sender, KeyPressEventArgs e) { }

        /// <summary>
        /// Maneja el evento Click del control btnAceptar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnAceptar_Click(object sender, EventArgs e)
        {
            if (cmbDeudores.SelectedValue == null ||
                !decimal.TryParse(txtMonto.Text, out decimal montoPago) ||
                montoPago <= 0)
            {
                MessageBox.Show("Por favor, selecciona un deudor y escribe un monto válido mayor a cero.",
                                "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idDeudaFinal = Convert.ToInt32(cmbDeudores.SelectedValue);
            decimal saldoPendiente = await objetoDeudas.ObtenerSaldo(idDeudaFinal);

            if (montoPago > saldoPendiente)
            {
                MessageBox.Show($"El monto ingresado ({montoPago:C}) supera el saldo pendiente ({saldoPendiente:C}).",
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
        /// Maneja el evento Click del control btnCancelar.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del control label2.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void label2_Click(object sender, EventArgs e) { }
    }
}