using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;
using SG_BAMS.Cliente.DTO;
using SG_BAMS.Facturas;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para registrar un nuevo cliente en el sistema.
    /// Solo depende de IClienteRepository (no necesita el catálogo de Estados,
    /// por eso no depende de IEstadoClienteRepository — ISP).
    /// </summary>
    public partial class ClienteAgregar : Form
    {
        private readonly IClienteRepository _repositorio;
        private readonly ClienteDominio _dominio;

        public int IdClienteGenerado { get; private set; }
        public string NombreDelCliente { get; private set; }

        private PlaceholderTextBox phNombre;
        private PlaceholderTextBox phApellido;
        private PlaceholderTextBox phTelefono;
        private PlaceholderTextBox phRTN;

        /// <summary>
        /// Crea el formulario recibiendo el repositorio por inyección.
        /// </summary>
        public ClienteAgregar(IClienteRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio;
            _dominio = new ClienteDominio(repositorio);
            this.StartPosition = FormStartPosition.CenterScreen;

            txtTelefono.MaxLength = 8;
            txtRTN.MaxLength = 14;

            txtNombre.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtApellido.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            txtRTN.KeyPress += (s, e) => ClsValidaciones.ValidarSoloNumeros(e);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
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

            rtnReal = _dominio.NormalizarRTN(rtnReal);

            try
            {
                var rtnValido = await _dominio.ValidarRTNDuplicado(rtnReal);
                if (!rtnValido.EsValido)
                {
                    MessageBox.Show(rtnValido.Mensaje, "RTN Duplicado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                ClienteDTO clienteDTO = new ClienteDTO
                {
                    Nombre = nombreReal,
                    Apellido = apellidoReal,
                    Telefono = telefonoReal,
                    RTN = rtnReal
                };

                int id = await _repositorio.AgregarClientes(clienteDTO);

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

        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        private void BtnExistente_Click(object sender, EventArgs e)
        {
            using (ClienteExistente frmCE = new ClienteExistente(_repositorio))
            {
                if (frmCE.ShowDialog() == DialogResult.OK)
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

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