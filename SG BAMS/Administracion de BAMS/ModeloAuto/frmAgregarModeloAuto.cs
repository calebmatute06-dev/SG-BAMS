using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para registrar un nuevo modelo de automóvil en el sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarModeloAuto : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarModeloAuto"/>.
        /// </summary>
        public frmAgregarModeloAuto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Restricción en tiempo real para asegurar que solo se ingresen caracteres alfanuméricos
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón agregar. Realiza la validación de los datos
        /// y ejecuta la inserción de forma asíncrona en la base de datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            // Validar que el campo no esté vacío y cumpla con el formato alfanumérico
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre del Modelo de Auto"))
            {
                return;
            }
            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Modelo_de_auto",
                    columnaNombre: "nombre_modelo_auto",
                    nombreCampo: "Tipo de Modelo de Auto",
                    idExcluir: 0,
                    idColumna: "id_modelo_auto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                // Deshabilitar el botón para evitar múltiples envíos accidentales
                if (btnAgregar != null) btnAgregar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                // Llamada asíncrona a la capa de datos para insertar el nuevo modelo
                bool exito = await objetoModelo.InsertarModeloAutoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Notificar al formulario padre que la operación fue exitosa y cerrar
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (btnAgregar != null) btnAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón salir para cerrar el formulario actual.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}