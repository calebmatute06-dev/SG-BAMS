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
    /// Formulario para modificar los datos de un cliente existente.
    /// Recibe dos dependencias específicas en vez de una sola interfaz "gorda" (ISP):
    /// IClienteRepository para el CRUD, e IEstadoClienteRepository únicamente
    /// para llenar el combo de estados.
    /// </summary>
    public partial class ClienteModificar : Form
    {
        private readonly IClienteRepository _repositorio;
        private readonly IEstadoClienteRepository _estadoRepositorio;
        private readonly ClienteDominio _dominio;

        /// <summary>
        /// Datos del cliente a modificar, recibidos desde el listado (ClientesAdm/ClientesEmp).
        /// </summary>
        private readonly ClienteDTO _dto;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phEstado;

        /// <summary>
        /// Constructor para modificar un cliente existente, recibiendo ambos repositorios por inyección.
        /// En la práctica ClienteRepository implementa las dos interfaces, así que en el
        /// sitio de llamada se puede pasar la misma instancia dos veces: cada parámetro
        /// sigue dependiendo únicamente del subconjunto de métodos que realmente usa.
        /// </summary>
        public ClienteModificar(ClienteDTO dto, IClienteRepository repositorio, IEstadoClienteRepository estadoRepositorio)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            _dto = dto;
            _repositorio = repositorio;
            _estadoRepositorio = estadoRepositorio;
            _dominio = new ClienteDominio(repositorio);

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
        /// Constructor sin parámetros, necesario para que el Diseñador de
        /// Visual Studio pueda seguir abriendo el formulario.
        /// </summary>
        public ClienteModificar()
        {
            InitializeComponent();
        }

        private async void BtnModificar_Click(object sender, EventArgs e)
        {
            string nombreReal = phNombre.GetRealValue().Trim();
            string apellidoReal = phApellido.GetRealValue().Trim();
            string telefonoReal = phTelefono.GetRealValue().Trim();
            string rtnReal = phRTN.GetRealValue().Trim();

            var formatoValido = _dominio.ValidarFormato(nombreReal, apellidoReal, telefonoReal, rtnReal);
            if (!formatoValido.EsValido)
            {
                MessageBox.Show(formatoValido.Mensaje, "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (phEstado.IsPlaceholderActive || cmbEstado.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione un estado válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            rtnReal = _dominio.NormalizarRTN(rtnReal);

            try
            {
                var rtnValido = await _dominio.ValidarRTNDuplicado(rtnReal, Convert.ToInt32(txtID.Text));
                if (!rtnValido.EsValido)
                {
                    MessageBox.Show(rtnValido.Mensaje, "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ClienteDTO dtoActualizado = new ClienteDTO
                {
                    IdCliente = Convert.ToInt32(txtID.Text),
                    Nombre = nombreReal,
                    Apellido = apellidoReal,
                    Telefono = telefonoReal,
                    RTN = rtnReal,
                    IdEstado = Convert.ToInt32(cmbEstado.SelectedValue)
                };

                int filasActualizadas = await _repositorio.ModificarClientes(dtoActualizado);

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
                DataTable dt = await _estadoRepositorio.ObtenerEstados();
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
            cmbEstado.SelectedValue = _dto?.IdEstado ?? 0;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;

            phNombre = new PlaceholderTextBox(txtNombre, "Solo letras y espacios");
            phApellido = new PlaceholderTextBox(txtApellido, "Solo letras y espacios");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Debe comenzar con 2,3,7,8 o 9");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese RTN(opcional)");
            phEstado = new PlaceholderComboBox(cmbEstado, "Seleccione un estado");
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }
    }
}