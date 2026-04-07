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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarModelos : Form
    {
        /// <summary>
        /// The identifier modelo seleccionado
        /// </summary>
        private int idModeloSeleccionado;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarModelos"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombreActual">The nombre actual.</param>
        public frmModificarModelos(int id, string nombreActual)
        {
            InitializeComponent();
            this.idModeloSeleccionado = id;
            txtDescri.Text = nombreActual;


            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Handles the 1 event of the btnModificar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnModificar_Click_1(object sender, EventArgs e)
        {

            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre del Modelo"))
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
        /// Handles the Click event of the btnSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Handles the Load event of the frmModificarModelos control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmModificarModelos_Load(object sender, EventArgs e)
        {

        }
    }
}