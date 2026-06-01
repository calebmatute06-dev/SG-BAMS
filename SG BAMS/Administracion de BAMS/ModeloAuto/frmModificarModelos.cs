using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de un modelo de vehículo existente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarModelos : Form
    {
        /// <summary>
        /// Almacena el identificador del modelo seleccionado para su actualización.
        /// </summary>
        private int idModeloSeleccionado;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarModelos"/>.
        /// </summary>
        /// <param name="id">El identificador único del modelo.</param>
        /// <param name="nombreActual">El nombre actual del modelo para mostrar en el campo de edición.</param>
        public frmModificarModelos(int id, string nombreActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idModeloSeleccionado = id;
            txtDescri.Text = nombreActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            // Restricción de entrada para permitir solo caracteres alfanuméricos
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón modificar. Valida la entrada y actualiza los datos de forma asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnModificar_Click_1(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre del Modelo"))
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
                btnModificar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                bool exito = await objetoModelo.ModificarModeloAutoAsync(idModeloSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Cierra el formulario de modificación.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento de carga del formulario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void frmModificarModelos_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
        }
    }
}