using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SG_BAMS.Cliente;
using SG_BAMS.Facturas;

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
            if (!ClsValidaciones.EsNombrePersonalValido(txtNombre.TextBox, "El Nombre") ||
                !ClsValidaciones.EsNombrePersonalValido(txtApellido.TextBox, "El Apellido"))
            {
                return;
            }

            if (!ClsValidaciones.EsTelefonoHondurasValido(txtTelefono.TextBox))
            {
                return;
            }

            string rtn = txtRTN.Text.Trim();

            if (!string.IsNullOrWhiteSpace(rtn))
            {
                if (!ClsValidaciones.EsRTNValido(txtRTN.TextBox))
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
                ClsAgregarClientes objAC = new ClsAgregarClientes();

                int id = await objAC.AgregarClientes(
                    txtNombre.Text.Trim(),
                    txtApellido.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    rtn
                );

                if (id > 0)
                {
                    this.IdClienteGenerado = id;
                    this.NombreDelCliente = $"{txtNombre.Text.Trim()} {txtApellido.Text.Trim()}";

                    using (FacturaAgregarDatos frmFact = new FacturaAgregarDatos(this.NombreDelCliente, this.IdClienteGenerado, rtn))
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
        /// Se ejecuta al cargar el formulario; reservado para inicializaciones futuras.
        /// </summary>
        /// <param name="sender">El objeto que originó el evento.</param>
        /// <param name="e">Los datos del evento.</param>
        private void ClienteAgregar_Load(object sender, EventArgs e) {
            /*ClsMensajeGuia.Activar(txtNombre);
            ClsMensajeGuia.Activar(txtTelefono);
            ClsMensajeGuia.Activar(txtApellido);
            ClsMensajeGuia.Activar(txtRTN);*/

            ClsMensajeGuia.ActivarK(txtNombre);
            ClsMensajeGuia.ActivarK(txtApellido);
            ClsMensajeGuia.ActivarK(txtTelefono);
            ClsMensajeGuia.ActivarK(txtRTN);
            this.ActiveControl = null;
        }
    }
}