using SG_BAMS.Cliente;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krypton.Toolkit;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para modificar los datos de un cliente existente en el sistema.
    /// Permite editar nombre, apellido, teléfono, RTN y estado del cliente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteModificar : Form
    {
        /// <summary>
        /// Instancia de la clase de conexión utilizada para operaciones con la base de datos.
        /// </summary>
        ClsConexion objCl = new ClsConexion();

        /// <summary>
        /// Almacena el identificador del estado actual del cliente para preseleccionarlo en el ComboBox.
        /// </summary>
        int idEstadoSelec;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClienteModificar"/> con los datos
        /// actuales del cliente, precargando los campos del formulario y configurando
        /// las validaciones de entrada por teclado.
        /// </summary>
        /// <param name="idCliente">El identificador único del cliente a modificar.</param>
        /// <param name="nombreCliente">El nombre actual del cliente.</param>
        /// <param name="apellidoCliente">El apellido actual del cliente.</param>
        /// <param name="telefonoCliente">El teléfono actual del cliente.</param>
        /// <param name="rtnCliente">El RTN actual del cliente.</param>
        /// <param name="idEstado">El identificador del estado actual del cliente.</param>
        public ClienteModificar(int idCliente, string nombreCliente, string apellidoCliente, string telefonoCliente, string rtnCliente, int idEstado)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtID.Text = idCliente.ToString();
            txtNombre.Text = nombreCliente;
            txtApellido.Text = apellidoCliente;
            txtTelefono.Text = telefonoCliente;
            txtRTN.Text = rtnCliente;
            idEstadoSelec = idEstado;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;
            txtID.ReadOnly = true;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
            txtTelefono.KeyPress += (s, e) =>
                ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        /// <summary>
        /// Inicializa una nueva instancia vacía de la clase <see cref="ClienteModificar"/>.
        /// Utilizada por el diseñador de formularios de Windows Forms.
        /// </summary>
        public ClienteModificar()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnModificar</c>.
        /// Valida todos los campos del formulario y, si son correctos, actualiza
        /// los datos del cliente en la base de datos de forma asíncrona.
        /// Muestra un mensaje de éxito o informa si no hubo cambios detectados.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void BtnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre") ||
                !ClsValidaciones.EsNombrePersonalValido(txtApellido, "Apellido"))
            {
                return;
            }

            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono))
            {
                return;
            }

            if (!ClsValidaciones.ValidarSeleccion(cmbEstado, "el estado del cliente"))
            {
                return;
            }

            string rtn = txtRTN.Text.Trim();
            if (!string.IsNullOrWhiteSpace(rtn) && rtn.ToUpper() != "SIN RTN")
            {
                if (!ClsValidaciones.EsRTNValido(txtRTN))
                {
                    return;
                }
            }
            else
            {
                rtn = "Sin RTN";
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClsModificarCliente objMC = new ClsModificarCliente();

                int filasActualizadas = await objMC.ModificarClientes(
                    Convert.ToInt32(txtID.Text),
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    rtn,
                    Convert.ToInt32(cmbEstado.SelectedValue)
                );

                if (filasActualizadas > 0)
                {
                    MessageBox.Show("Cliente actualizado correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    MessageBox.Show("No se detectaron cambios para actualizar.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al modificar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// Carga de forma asíncrona los estados disponibles del sistema
        /// en el control ComboBox de estado del cliente.
        /// </summary>
        private async Task LlenarComboEstado()
        {
            ClsModificarCliente MC = new ClsModificarCliente();
            try
            {
                DataTable dt = await MC.ObtenerEstados();
                cmbEstado.DataSource = dt;
                cmbEstado.DisplayMember = "descripcion_estado";
                cmbEstado.ValueMember = "id_estado";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al llenar Estados: " + ex.Message);
            }
        }

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClienteModificar</c>.
        /// Carga los estados disponibles en el ComboBox y preselecciona
        /// el estado actual del cliente. Configura el ComboBox en modo solo lectura.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void ClienteModificar_Load(object sender, EventArgs e)
        {
            await LlenarComboEstado();
            cmbEstado.SelectedValue = idEstadoSelec;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            cmbEstado.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnSalir</c>.
        /// Cierra el formulario actual sin guardar cambios.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Maneja el evento KeyPress del campo <c>txtTelefono</c>.
        /// Aplica validación en tiempo real para permitir únicamente
        /// caracteres válidos en un número de teléfono hondureño.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento de teclado.</param>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }
    }
}