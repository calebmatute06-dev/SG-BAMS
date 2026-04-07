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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class ClienteAgregar : Form
    {
        /// <summary>
        /// Gets the identifier cliente generado.
        /// </summary>
        /// <value>
        /// The identifier cliente generado.
        /// </value>
        public int IdClienteGenerado { get; private set; }
        /// <summary>
        /// Gets the nombre delete cliente.
        /// </summary>
        /// <value>
        /// The nombre delete cliente.
        /// </value>
        public string NombreDelCliente { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ClienteAgregar"/> class.
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
        /// Handles the Click event of the btnAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the KeyPress event of the txtTelefono control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtTelefono_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.ValidarTelefonoKeyPress(txtTelefono, e);
        }

        /// <summary>
        /// Handles the Click event of the BtnExistente control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the BtnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void BtnSalir_Click(object sender, EventArgs e) => this.Close();

        /// <summary>
        /// Handles the Load event of the ClienteAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void ClienteAgregar_Load(object sender, EventArgs e) { }
    }
}