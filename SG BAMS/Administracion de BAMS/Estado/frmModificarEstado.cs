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
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarEstado : Form
    {
        /// <summary>
        /// The identifier estado
        /// </summary>
        int idEstado;
        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarEstado"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="descripcionActual">The descripcion actual.</param>
        public frmModificarEstado(int id, string descripcionActual)
        {
            InitializeComponent();
            this.idEstado = id;
            txtDescri.Text = descripcionActual;

            this.txtDescri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescri_KeyPress);
        }



        /// <summary>
        /// Handles the KeyPress event of the txtDescri control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyPressEventArgs"/> instance containing the event data.</param>
        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {

           ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
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
        /// Handles the Click event of the kryptonButton1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        

      
    }
}
