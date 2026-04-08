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
    /// Representa la ventana para agregar una nueva forma de pago al sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarFormaPago : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarFormaPago"/>.
        /// </summary>
        public frmAgregarFormaPago()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtdescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón agregar de forma asíncrona.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnAgregar_Click(object sender, EventArgs e)
        {

            if (!ClsValidaciones.EsNombrePersonalValido(txtdescri, "Descripción de Forma de Pago"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsFormaPago objetoFP = new clsFormaPago();


                bool insertado = await objetoFP.InsertarFormaPagoAsync(txtdescri.Text.Trim());

                if (insertado)
                {
                    // Traducido: Forma de pago agregada correctamente.
                    MessageBox.Show("Forma de pago agregada correctamente.", "Éxito",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón salir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click de la etiqueta label5.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}