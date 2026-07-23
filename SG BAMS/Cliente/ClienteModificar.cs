using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using System;
using System.Data;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para modificar los datos de un cliente existente en el sistema.
    /// Permite editar nombre, apellido, teléfono, RTN y estado del cliente.
    /// Única responsabilidad de esta clase: coordinar la interacción con el usuario;
    /// el acceso a datos se recibe por inyección (ver auditoría SOLID, hallazgos CM01-CM04).
    /// </summary>
    public partial class ClienteModificar : Form
    {
        private readonly IClienteRepository clienteRepositorio;

        /// <summary>
        /// Datos del cliente a modificar, recibidos desde el listado (ClientesAdm/ClientesEmp).
        /// Reemplaza los 6 parámetros sueltos que antes recibía el constructor.
        /// </summary>
        private readonly ClienteDTO dto;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phEstado;

        /// <summary>
        /// Constructor para modificar un cliente existente, recibiendo su dependencia
        /// de acceso a datos por inyección.
        /// </summary>
        /// <param name="clienteRepositorio">Acceso a datos de clientes.</param>
        /// <param name="dto">Datos actuales del cliente seleccionado en el listado.</param>
        public ClienteModificar(IClienteRepository clienteRepositorio, ClienteDTO dto)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            this.clienteRepositorio = clienteRepositorio;
            this.dto = dto;
            txtID.Text = dto.IdCliente.ToString();
            txtNombre.Text = dto.Nombre;
            txtApellido.Text = dto.Apellido;
            txtTelefono.Text = dto.Telefono;
            txtRTN.Text = dto.RTN;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            txtID.ReadOnly = true;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnModificar</c>.
        /// Orquesta los pasos independientes: validar formato, verificar RTN duplicado
        /// y guardar (ver hallazgo CM03).
        /// </summary>
        private async void BtnModificar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(out ClienteDTO dtoActualizado))
                return;

            if (!await ValidarRTNDuplicado(dtoActualizado.RTN, dtoActualizado.IdCliente))
                return;

            await GuardarCambios(dtoActualizado);
        }

        /// <summary>
        /// Valida el formato de todos los campos y arma el DTO actualizado si son válidos.
        /// </summary>
        private bool ValidarFormulario(out ClienteDTO dtoActualizado)
        {
            dtoActualizado = null;

            string nombreReal = phNombre.GetRealValue().Trim();
            string apellidoReal = phApellido.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            bool valido = true;
            using (var tempNombre = new KryptonTextBox())
            using (var tempApellido = new KryptonTextBox())
            using (var tempTelefono = new KryptonTextBox())
            using (var tempRTN = new KryptonTextBox())
            {
                tempNombre.Text = nombreReal;
                tempApellido.Text = apellidoReal;
                tempTelefono.Text = telefonoReal;
                tempRTN.Text = rtnReal;

                if (!ClsValidaciones.EsNombrePersonalValido(tempNombre, "Nombre"))
                    valido = false;
                else if (!ClsValidaciones.EsNombrePersonalValido(tempApellido, "Apellido"))
                    valido = false;
                else if (!ClsValidaciones.EsTelefonoHondurasValido(tempTelefono))
                    valido = false;
                else if (phEstado.IsPlaceholderActive || cmbEstado.SelectedIndex == -1)
                {
                    MessageBox.Show("Seleccione un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    valido = false;
                }
                else if (!string.IsNullOrWhiteSpace(rtnReal) && rtnReal.ToUpper() != "SIN RTN" && !ClsValidaciones.EsRTNValido(tempRTN))
                    valido = false;
            }

            if (!valido) return false;

            if (string.IsNullOrWhiteSpace(rtnReal) || rtnReal.ToUpper() == "SIN RTN")
                rtnReal = "Sin RTN";

            dtoActualizado = new ClienteDTO
            {
                IdCliente = Convert.ToInt32(txtID.Text),
                Nombre = nombreReal,
                Apellido = apellidoReal,
                Telefono = telefonoReal,
                RTN = rtnReal,
                IdEstado = Convert.ToInt32(cmbEstado.SelectedValue)
            };

            return true;
        }

        /// <summary>
        /// Verifica que el RTN ingresado (si lo hay) no esté registrado para otro cliente.
        /// </summary>
        private async Task<bool> ValidarRTNDuplicado(string rtn, int idClienteActual)
        {
            if (rtn == "Sin RTN") return true;

            try
            {
                bool existe = await Task.Run(() => clienteRepositorio.RTNYaExiste(rtn, idClienteActual));
                if (existe)
                {
                    MessageBox.Show("Este RTN ya está registrado para otro cliente.",
                                    "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al verificar el RTN: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        /// <summary>
        /// Persiste los cambios ya validados.
        /// </summary>
        private async Task GuardarCambios(ClienteDTO dtoActualizado)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int filasActualizadas = await clienteRepositorio.ModificarClientes(dtoActualizado);

                if (filasActualizadas > 0)
                {
                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se detectaron cambios para actualizar.", "Información",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private async Task LlenarComboEstado()
        {
            try
            {
                DataTable dt = await clienteRepositorio.ObtenerEstados();
                cmbEstado.DataSource = dt;
                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar Estados: " + ex.Message);
            }
        }

        private async void ClienteModificar_Load(object sender, EventArgs e)
        {
            await LlenarComboEstado();
                cmbEstado.SelectedValue = dto?.IdEstado ?? 0;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            phNombre = new PlaceholderTextBox(txtNombre, "Solo letras y espacios");
            phApellido = new PlaceholderTextBox(txtApellido, "Solo letras y espacios");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Debe comenzar con 2,3,7,8 o 9");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese RTN(opcional)");
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }
    }
}