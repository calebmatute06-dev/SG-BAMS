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

        // Referencias a los placeholders
        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;

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

            
            string originalNombre = txtNombre.Text;
            string originalApellido = txtApellido.Text;
            string originalTelefono = txtTelefono.Text;
            string originalRTN = txtRTN.Text;

            
            txtNombre.Text = nombreReal;
            txtApellido.Text = apellidoReal;
            txtTelefono.Text = telefonoReal;
            txtRTN.Text = rtnReal;

            bool valido = true;

            
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre, "Nombre"))
                valido = false;
            else if (!ClsValidaciones.EsNombrePersonalValido(txtApellido, "Apellido"))
                valido = false;
            else if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono))
                valido = false;
            else if (!ClsValidaciones.ValidarSeleccion(cmbEstado, "el estado del cliente"))
                valido = false;

           
            if (valido && !string.IsNullOrWhiteSpace(rtnReal) && rtnReal.ToUpper() != "SIN RTN")
            {
                if (!ClsValidaciones.EsRTNValido(txtRTN))
                    valido = false;
            }

            
            txtNombre.Text = originalNombre;
            txtApellido.Text = originalApellido;
            txtTelefono.Text = originalTelefono;
            txtRTN.Text = originalRTN;

            if (!valido) return;

            
            if (string.IsNullOrWhiteSpace(rtnReal) || rtnReal.ToUpper() == "SIN RTN")
                rtnReal = "Sin RTN";

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
            phRTN = new PlaceholderTextBox(txtRTN, "14 Digitos numericos minimo");
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }
    }
}