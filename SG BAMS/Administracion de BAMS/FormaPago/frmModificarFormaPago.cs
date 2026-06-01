using SG_BAMS.Administracion_de_BAMS.FormaPago;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para modificar una forma de pago existente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarFormaPago : Form
    {
        /// <summary>
        /// El identificador único de la forma de pago a modificar.
        /// </summary>
        private int _idFormaPago;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarFormaPago"/>.
        /// </summary>
        /// <param name="id">El identificador de la forma de pago.</param>
        /// <param name="descripcionActual">La descripción actual que se mostrará en el campo de texto.</param>
        public frmModificarFormaPago(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this._idFormaPago = id;
            txtDescri.Text = descripcionActual;

            // Validación en tiempo real para permitir solo letras mientras se escribe
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Maneja el evento Click del control pictureBox16.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void pictureBox16_Click(object sender, EventArgs e)
        {
            // Espacio para lógica adicional de imagen si es necesario
        }

        /// <summary>
        /// Maneja el evento Click del botón modificar de forma asíncrona.
        /// Realiza la validación del campo y actualiza el registro en la base de datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Descripción de Forma de Pago"))
            {
                return;
            }

            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Tipo_Forma_de_pago",
                    columnaNombre: "descripcion_forma_pago",
                    nombreCampo: "Tipo de Forma de Pago",
                    idExcluir: 0,
                    idColumna: "id_tipo_forma_pago"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsFormaPago objetoFP = new clsFormaPago();

                bool exito = await objetoFP.ModificarFormaPagoAsync(_idFormaPago, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Forma de pago actualizada correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón salir para cerrar el formulario actual.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}