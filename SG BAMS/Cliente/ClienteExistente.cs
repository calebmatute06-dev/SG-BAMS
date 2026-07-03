using Microsoft.Data.SqlClient;
using SG_BAMS.Cliente;
using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para seleccionar un cliente ya registrado en el sistema
    /// y asignarle una nueva factura.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteExistente : Form
    {
        private PlaceholderComboBox phClientes;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClienteExistente"/>
        /// y centra el formulario en la pantalla.
        /// </summary>
        public ClienteExistente()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        /// <summary>
        /// Carga de forma asíncrona la lista de clientes registrados
        /// en el control ComboBox, configurando el autocompletado.
        /// </summary>
        private async Task LlenarComboCliente()
        {
            ClsCliente objAC = new ClsCliente();
            try
            {
                DataTable dt = await objAC.ObtenerClientes();

                cmbClientes.DisplayMember = "Nombre Completo";
                cmbClientes.ValueMember = "ID";
                cmbClientes.DataSource = dt;

                cmbClientes.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cmbClientes.AutoCompleteSource = AutoCompleteSource.ListItems;
                cmbClientes.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener datos: " + ex.Message, "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClienteExistente</c>.
        /// Carga la lista de clientes en el ComboBox y limpia la selección inicial.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
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
        /// Valida que se haya seleccionado un cliente, recupera su RTN
        /// y abre el formulario de agregar factura asociado al cliente seleccionado.
        /// Si la factura se confirma, cierra el formulario actual con resultado OK.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void BtnAsignar_Click(object sender, EventArgs e)
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

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnSalir</c>.
        /// Cierra el formulario actual sin realizar ninguna acción.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void BtnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}