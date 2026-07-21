using SG_BAMS.Cliente;
using SG_BAMS.Facturas;
using System;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para seleccionar un cliente ya registrado en el sistema
    /// y asignarle una nueva factura.
    /// Única responsabilidad de esta clase: coordinar la interacción con el usuario;
    /// el acceso a datos se recibe por inyección (ver auditoría SOLID, hallazgos CE01-CE03).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteExistente : Form
    {
        private readonly IClienteRepository clienteRepositorio;
        private readonly IDeudasRepository deudasRepositorio;

        private PlaceholderComboBox phClientes;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClienteExistente"/>,
        /// recibiendo sus dependencias de acceso a datos por inyección.
        /// </summary>
        /// <param name="clienteRepositorio">Acceso a datos de clientes.</param>
        /// <param name="deudasRepositorio">Acceso a datos de deudas (para advertir de deuda activa).</param>
        public ClienteExistente(IClienteRepository clienteRepositorio, IDeudasRepository deudasRepositorio)
        {
            InitializeComponent();
            this.clienteRepositorio = clienteRepositorio;
            this.deudasRepositorio = deudasRepositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Carga de forma asíncrona la lista de clientes registrados en el ComboBox.
        /// </summary>
        private async Task LlenarComboCliente()
        {
            try
            {
                DataTable dt = await clienteRepositorio.ObtenerClientes();

                cmbClientes.DisplayMember = "Nombre Completo";
                cmbClientes.ValueMember = "ID";
                cmbClientes.DataSource = dt;

                cmbClientes.DropDownStyle = ComboBoxStyle.DropDown;
                cmbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClienteExistente</c>.
        /// </summary>
        private async void ClienteExistente_Load(object sender, EventArgs e)
        {
            await LlenarComboCliente();
            cmbClientes.SelectedIndex = -1;

            phClientes = new PlaceholderComboBox(cmbClientes, "Seleccione un cliente");

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnAsignar</c>.
        /// Orquesta los pasos independientes: validar selección, verificar deuda activa
        /// (con confirmación del usuario si aplica) y abrir el formulario de factura
        /// (ver hallazgo CE03).
        /// </summary>
        private async void BtnAsignar_Click(object sender, EventArgs e)
        {
            if (!ValidarSeleccion(out int idCliente, out string rtn))
                return;

            if (!await ConfirmarSiTieneDeudaActiva(idCliente))
                return;

            AbrirFacturaParaCliente(idCliente, rtn);
        }

        /// <summary>
        /// Valida que se haya seleccionado un cliente válido y extrae su id y RTN.
        /// </summary>
        private bool ValidarSeleccion(out int idCliente, out string rtn)
        {
            idCliente = 0;
            rtn = "Sin RTN";

            if (phClientes.IsPlaceholderActive || cmbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!ClsValidaciones.ValidarSeleccion(cmbClientes, "la lista de clientes"))
                return false;

            if (cmbClientes.SelectedValue == null || !int.TryParse(cmbClientes.SelectedValue.ToString(), out idCliente))
            {
                MessageBox.Show("Por favor, seleccione un cliente válido de la lista desplegable.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbClientes.SelectedItem is DataRowView drv)
            {
                string rtnValor = drv["rtn_cliente"]?.ToString()?.Trim();
                if (!string.IsNullOrWhiteSpace(rtnValor) && rtnValor != "Sin RTN")
                    rtn = rtnValor;
            }

            return true;
        }

        /// <summary>
        /// Si el cliente tiene una deuda activa, muestra el detalle y pide confirmación
        /// antes de continuar. Devuelve false si el usuario decide no continuar.
        /// </summary>
        private async Task<bool> ConfirmarSiTieneDeudaActiva(int idCliente)
        {
            try
            {
                bool tieneDeuda = await deudasRepositorio.ClienteTieneDeudaActiva(idCliente);
                if (!tieneDeuda) return true;

                string detalleDeudas = await Task.Run(() => FormatearDetalleDeudas(idCliente));

                DialogResult respuesta = MessageBox.Show(
                    $"El cliente '{cmbClientes.Text.Trim()}' tiene una deuda activa:\n\n" +
                    $"{detalleDeudas}" +
                    "¿Desea continuar con la factura de todas formas?",
                    "Advertencia de Crédito",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                return respuesta == DialogResult.Yes;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al verificar la deuda del cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Arma el texto de detalle de las deudas activas del cliente.
        /// </summary>
        private string FormatearDetalleDeudas(int idCliente)
        {
            var detalle = new StringBuilder();

            try
            {
                DataTable dtDeudas = deudasRepositorio.ObtenerDeudasPorCliente(idCliente);

                if (dtDeudas != null)
                {
                    foreach (DataRow row in dtDeudas.Rows)
                    {
                        int idDeuda = Convert.ToInt32(row["IdDeuda"]);
                        decimal saldo = Convert.ToDecimal(row["Saldo"]);
                        string fechaInicio = Convert.ToDateTime(row["FechaInicio"]).ToString("dd/MM/yyyy");
                        string fechaFin = Convert.ToDateTime(row["FechaFin"]).ToString("dd/MM/yyyy");

                        detalle.Append($"• Deuda #{idDeuda}  |  Desde: {fechaInicio}  →  Hasta: {fechaFin}\n");
                        detalle.Append($"  Saldo pendiente: L {saldo:N2}\n\n");
                    }
                }
            }
            catch
            {
                // Se degrada a un detalle vacío; el mensaje de advertencia de deuda igual se muestra.
            }

            return detalle.ToString();
        }

        /// <summary>
        /// Abre el formulario de agregar factura para el cliente indicado.
        /// </summary>
        private void AbrirFacturaParaCliente(int idCliente, string rtn)
        {
            try
            {
                using (FacturaAgregarDatos frmFA = new FacturaAgregarDatos(cmbClientes.Text, idCliente, rtn))
                {
                    this.Hide();
                    if (frmFA.ShowDialog() == DialogResult.OK)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        this.Show();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al asignar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnSalir</c>.
        /// </summary>
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}