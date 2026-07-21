using Krypton.Toolkit;
using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using SG_BAMS.Facturas;
using SG_BAMS.Login;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para registrar un nuevo cliente en el sistema.
    /// Permite ingresar los datos personales del cliente y redirige
    /// automáticamente al formulario de agregar factura al completar el registro.
    /// Única responsabilidad de esta clase: coordinar la interacción con el usuario;
    /// el acceso a datos se recibe por inyección (ver auditoría SOLID, hallazgos CA01-CA03).
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteAgregar : Form
    {
        private readonly IClienteRepository clienteRepositorio;

        /// <summary>
        /// Obtiene el identificador único generado para el cliente recién registrado.
        /// </summary>
        public int IdClienteGenerado { get; private set; }

        /// <summary>
        /// Obtiene el nombre completo del cliente recién registrado.
        /// </summary>
        public string NombreDelCliente { get; private set; }

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="ClienteAgregar"/>,
        /// recibiendo su dependencia de acceso a datos por inyección.
        /// </summary>
        /// <param name="clienteRepositorio">Acceso a datos de clientes.</param>
        public ClienteAgregar(IClienteRepository clienteRepositorio)
        {
            InitializeComponent();
            this.clienteRepositorio = clienteRepositorio;
            this.StartPosition = FormStartPosition.CenterScreen;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtTelefono.KeyPress += txtTelefono_KeyPress;
            txtRTN.KeyPress += txtRTN_KeyPress;
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>btnAgregar</c>.
        /// Orquesta los pasos independientes: validar formato, verificar RTN duplicado
        /// y guardar (ver hallazgo CA02).
        /// </summary>
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarFormulario(out ClienteDTO clienteDTO))
                return;

            if (!await ValidarRTNDuplicado(clienteDTO.RTN, 0))
                return;

            await GuardarClienteYContinuar(clienteDTO);
        }

        /// <summary>
        /// Valida el formato de todos los campos y arma el DTO si son válidos.
        /// </summary>
        private bool ValidarFormulario(out ClienteDTO clienteDTO)
        {
            clienteDTO = null;

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

            if (!valido) return false;

            if (string.IsNullOrWhiteSpace(rtnReal))
                rtnReal = "Sin RTN";

            clienteDTO = new ClienteDTO
            {
                Nombre = nombreReal,
                Apellido = apellidoReal,
                Telefono = telefonoReal,
                RTN = rtnReal
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
        /// Persiste el cliente ya validado y continúa el flujo hacia Agregar Factura.
        /// </summary>
        private async Task GuardarClienteYContinuar(ClienteDTO clienteDTO)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int id = await clienteRepositorio.AgregarClientes(clienteDTO);

                if (id > 0)
                {
                    this.IdClienteGenerado = id;
                    this.NombreDelCliente = $"{clienteDTO.Nombre} {clienteDTO.Apellido}";

                    using (FacturaAgregarDatos frmFact = new FacturaAgregarDatos(this.NombreDelCliente, this.IdClienteGenerado, clienteDTO.RTN))
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
        /// </summary>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        /// <summary>
        /// Maneja el evento Click del botón <c>BtnExistente</c>.
        /// Abre el formulario de búsqueda de clientes existentes, pasándole sus propias
        /// dependencias inyectadas.
        /// </summary>
        private void BtnExistente_Click(object sender, EventArgs e)
        {
            using (ClienteExistente frmCE = new ClienteExistente(new ClienteRepository(), new DeudasRepository()))
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
        /// </summary>
        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Maneja el evento Load del formulario <c>ClienteAgregar</c>.
        /// </summary>
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