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
    /// Representa la ventana para modificar un estado existente en el sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarEstado : Form
    {
        /// <summary>
        /// El identificador del estado.
        /// </summary>
        int idEstado;
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarEstado"/>.
        /// </summary>
        /// <param name="id">El identificador del registro.</param>
        /// <param name="descripcionActual">La descripción actual del estado.</param>
        public frmModificarEstado(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idEstado = id;
            txtDescri.Text = descripcionActual;

            this.txtDescri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescri_KeyPress);
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
        /// Maneja el evento Click del botón modificar de forma asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {

            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Descripción del Estado"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsEstado objetoEstado = new clsEstado();


                bool exito = await objetoEstado.ModificarEstadoAsync(idEstado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Estado actualizado con éxito.", "SG-BAMS",
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
        /// Maneja el evento Click del botón cancelar o salir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }



    }
}