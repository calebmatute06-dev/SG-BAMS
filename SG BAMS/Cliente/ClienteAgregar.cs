using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using SG_BAMS.Facturas;
using Krypton.Toolkit;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para registrar un nuevo cliente en el sistema.
    /// Permite ingresar los datos personales del cliente y redirige
    /// automáticamente al formulario de agregar factura al completar el registro.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteAgregar : Form
    {
        /// <summary>
        /// Obtiene el identificador único generado para el cliente recién registrado.
        /// </summary>
        /// <value>
        /// El ID del cliente generado por la base de datos.
        /// </value>
        public int IdClienteGenerado { get; private set; }

        /// <summary>
        /// Obtiene el nombre completo del cliente recién registrado.
        /// </summary>
        /// <value>
        /// El nombre y apellido del cliente concatenados.
        /// </value>
        public string NombreDelCliente { get; private set; }

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClienteAgregar"/>.
        /// Configura la posición del formulario, las longitudes máximas de los campos
        /// y las validaciones de entrada por teclado.
        /// </summary>
        public ClienteAgregar()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnAgregar</c>.
        /// Valida los campos del formulario, registra el nuevo cliente en la base de datos
        /// y abre el formulario de factura si el registro fue exitoso.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private async void btnAgregar_Click(object sender, EventArgs e)
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

                if (!ClsValidaciones.EsNombrePersonalValido(tempNombre, "El Nombre"))
                    valido = false;
                else if (!ClsValidaciones.EsNombrePersonalValido(tempApellido, "El Apellido"))
                    valido = false;
                else if (!ClsValidaciones.EsTelefonoHondurasValido(tempTelefono))
                    valido = false;
                else if (!string.IsNullOrWhiteSpace(rtnReal) && !ClsValidaciones.EsRTNValido(tempRTN))
                    valido = false;
            }

            if (!valido) return;

            if (string.IsNullOrWhiteSpace(rtnReal))
                rtnReal = "Sin RTN";

            if (rtnReal != "Sin RTN")
            {
                ClsCliente ver = new ClsCliente();
                if (ver.RTNYaExiste(rtnReal))
                {
                    MessageBox.Show("Este RTN ya está registrado para otro cliente.",
                                    "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                ClsCliente objAC = new ClsCliente();

                ClienteDTO clienteDTO = new ClienteDTO
                {
                    Nombre = nombreReal,
                    Apellido = apellidoReal,
                    Telefono = telefonoReal,
                    RTN = rtnReal
                };

                int id = await objAC.AgregarClientes(clienteDTO);

                if (id > 0)
                {
                    this.IdClienteGenerado = id;
                    this.NombreDelCliente = $"{nombreReal} {apellidoReal}";

                    using (FacturaAgregarDatos frmFact = new FacturaAgregarDatos(this.NombreDelCliente, this.IdClienteGenerado, rtnReal))
                    {
                        this.Hide();
                        frmFact.ShowDialog();
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

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

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnExistente</c>.
        /// Abre el formulario de búsqueda de clientes existentes y cierra
        /// el formulario actual si se seleccionó un cliente correctamente.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void BtnExistente_Click(object sender, EventArgs e)
        {
            using (ClienteExistente frmCE = new ClienteExistente())
            {
                if (frmCE.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnSalir</c>.
        /// Cierra el formulario actual sin guardar cambios.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClienteAgregar</c>.
        /// Inicializa los placeholders y los guarda en las variables de instancia.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void ClienteAgregar_Load(object sender, EventArgs e)
        {
            phNombre = new PlaceholderTextBox(txtNombre, "Solo letras y espacios");
            phApellido = new PlaceholderTextBox(txtApellido, "Solo letras y espacios");
            phTelefono = new PlaceholderTextBox(txtTelefono, "Debe comenzar con 2,3,7,8 o 9");
            phRTN = new PlaceholderTextBox(txtRTN, "Ingrese el RTN(opcional)");
        }

        private void txtTelefono_TextChanged(object sender, EventArgs e)
        {
        }
    }
}