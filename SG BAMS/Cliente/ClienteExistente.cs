using SG_BAMS.Cliente;
using System;
using System.Data;
using System.Windows.Forms;
using SG_BAMS.Deudores;
using System.Threading.Tasks;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para seleccionar un cliente ya registrado en el sistema
    /// y asignarle una nueva factura.
    /// Solo depende de IClienteRepository (no necesita el catálogo de Estados — ISP).
    /// </summary>
    public partial class ClienteExistente : Form
    {
        private readonly IClienteRepository _repositorio;
        private PlaceholderComboBox phClientes;

        /// <summary>
        /// Crea el formulario recibiendo el repositorio por inyección.
        /// </summary>
        public ClienteExistente(IClienteRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private async Task LlenarComboCliente()
        {
            try
            {
                DataTable dt = await _repositorio.ObtenerClientes();

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

        private async void ClienteExistente_Load(object sender, EventArgs e)
        {
            await LlenarComboCliente();
            cmbClientes.SelectedIndex = -1;

            phClientes = new PlaceholderComboBox(cmbClientes, "Seleccione un cliente");

            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
        }

        private async void BtnAsignar_Click(object sender, EventArgs e)
        {
            if (phClientes.IsPlaceholderActive || cmbClientes.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!ClsValidaciones.ValidarSeleccion(cmbClientes, "la lista de clientes"))
                return;

            try
            {
                if (cmbClientes.SelectedValue != null && int.TryParse(cmbClientes.SelectedValue.ToString(), out int idCliente))
                {
                    string rtn = "Sin RTN";
                    if (cmbClientes.SelectedItem is DataRowView drv)
                    {
                        string rtnValor = drv["rtn_cliente"]?.ToString()?.Trim();
                        if (!string.IsNullOrWhiteSpace(rtnValor) && rtnValor != "Sin RTN")
                            rtn = rtnValor;
                    }

                    ClsDeudas objDeudas = new ClsDeudas();
                    bool tieneDeuda = await objDeudas.ClienteTieneDeudaActiva(idCliente);

                    if (tieneDeuda)
                    {
                        string detalleDeudas = "";

                        try
                        {
                            DataTable dtDeudas = await Task.Run(() => objDeudas.ObtenerDeudasPorCliente(idCliente));

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
                        }
                        catch { }

                        DialogResult respuesta = MessageBox.Show(
                            $"El cliente '{cmbClientes.Text.Trim()}' tiene una deuda activa:\n\n" +
                            $"{detalleDeudas}" +
                            "¿Desea continuar con la factura de todas formas?",
                            "Advertencia de Crédito",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning);

                        if (respuesta == DialogResult.No)
                            return;
                    }

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
                else
                {
                    MessageBox.Show("Por favor, seleccione un cliente válido de la lista desplegable.", "Selección Requerida", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocurrió un error al asignar el cliente: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}