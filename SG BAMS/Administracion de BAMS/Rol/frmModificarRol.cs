using SG_BAMS.Administracion_de_BAMS.Rol;
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
    public partial class frmModificarRol : Form
    {
        /// <summary>
        /// The identifier rol seleccionado
        /// </summary>
        private int idRolSeleccionado;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarRol"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombreActual">The nombre actual.</param>
        public frmModificarRol(int id, string nombreActual)
        {
            InitializeComponent();
            this.idRolSeleccionado = id;
            txtDescri.Text = nombreActual;

            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Handles the Click event of the btmModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btmModificar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri, "Nombre del Rol"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmModificar.Enabled = false;

                clsRol objetoRol = new clsRol();

                
                bool exito = await objetoRol.ModificarRolAsync(idRolSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol actualizado con éxito.", "SG-BAMS",
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
                btmModificar.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btmSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmSalir_Click(object sender, EventArgs e)
        {
            
            this.Close();
        }
    }
}