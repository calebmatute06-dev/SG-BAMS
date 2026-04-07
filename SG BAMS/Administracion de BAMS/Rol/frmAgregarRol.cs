using SG_BAMS.Administracion_de_BAMS.Rol;
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
    public partial class frmAgregarRol : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmAgregarRol"/> class.
        /// </summary>
        public frmAgregarRol()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
        }

        /// <summary>
        /// Handles the Click event of the pictureBox16 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the label1 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void label1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Handles the Click event of the btmAgregar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btmAgregar_Click(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsNombrePersonalValido(txtDescri.TextBox, "Nombre del Rol"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btmAgregar.Enabled = false;

                clsRol objetoRol = new clsRol();

                
                bool exito = await objetoRol.InsertarRolAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Rol registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;

                    
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btmAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the Click event of the btmSalir control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btmSalir_Click(object sender, EventArgs e)
        {
            frmRoles verRoles = new frmRoles();
            verRoles.Show();
            this.Close();
        }
    }
}
