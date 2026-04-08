using SG_BAMS.Administracion_de_BAMS.Estado;
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
    /// Representa la ventana para agregar un nuevo estado al sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarEstado : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarEstado"/>.
        /// </summary>
        public frmAgregarEstado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.txtDescri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescri_KeyPress);

        }

        /// <summary>
        /// Maneja el evento Click del control pictureBox16.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Maneja el evento Click del control btnCerrarSesion.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento KeyPress del control txtDescri.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="KeyPressEventArgs"/> que contiene los datos del evento.</param>
        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {

            ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Maneja el evento Click del control btnAgregar de forma asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri, "Descripción del Estado"))
            {
                return;
            }
            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Estado",
                    columnaNombre: "descripcion_estado",
                    nombreCampo: "Tipo de Estado",
                    idExcluir: 0,
                    idColumna: "id_estado"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsEstado objetoEstado = new clsEstado();


                bool exito = await objetoEstado.InsertarEstadoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Estado registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del control btnSalir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}