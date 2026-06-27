using SG_BAMS.Cliente;
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
    /// </summary>
    public partial class ClienteModificar : Form
    {
        private ClsConexion objCl = new ClsConexion();
        private int idEstadoSelec;

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;
        private PlaceholderComboBox phEstado;

        /// <summary>
        /// Constructor para modificar un cliente existente.
        /// </summary>
        /// <param name="idCliente">ID del cliente.</param>
        /// <param name="nombreCliente">Nombre actual.</param>
        /// <param name="apellidoCliente">Apellido actual.</param>
        /// <param name="telefonoCliente">Teléfono actual.</param>
        /// <param name="rtnCliente">RTN actual.</param>
        /// <param name="idEstado">ID del estado actual.</param>
        public ClienteModificar(int idCliente, string nombreCliente, string apellidoCliente,
            string telefonoCliente, string rtnCliente, int idEstado)
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
            txtTelefono.KeyPress += (s, e) => ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

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

            if (!valido) return;

            if (string.IsNullOrWhiteSpace(rtnReal) || rtnReal.ToUpper() == "SIN RTN")
                rtnReal = "Sin RTN";

            if (rtnReal != "Sin RTN")
            {
                ClsVerCliente ver = new ClsVerCliente();
                if (ver.RTNYaExiste(rtnReal, Convert.ToInt32(txtID.Text)))
                {
                    MessageBox.Show("Este RTN ya está registrado para otro cliente.",
                                    "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClsModificarCliente objMC = new ClsModificarCliente();

                int filasActualizadas = await objMC.ModificarClientes(
                    Convert.ToInt32(txtID.Text),
                    nombreReal,
                    apellidoReal,
                    telefonoReal,
                    rtnReal,
                    Convert.ToInt32(cmbEstado.SelectedValue)
                );

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

        private async void ClienteModificar_Load(object sender, EventArgs e)
        {
            await LlenarComboEstado();
            cmbEstado.SelectedValue = idEstadoSelec;
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