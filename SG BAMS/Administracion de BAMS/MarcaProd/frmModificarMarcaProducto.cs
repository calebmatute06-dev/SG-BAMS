using SG_BAMS.Administracion_de_BAMS.MarcaProd;
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
    public partial class frmModificarMarcaProducto : Form
    {
        /// <summary>
        /// The identifier marca
        /// </summary>
        private int idMarca;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarMarcaProducto"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="nombreActual">The nombre actual.</param>
        public frmModificarMarcaProducto(int id, string nombreActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idMarca = id;
            txtDescri.Text = nombreActual;

            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }


        /// <summary>
        /// Handles the Load event of the frmModificarMarcaProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmModificarMarcaProducto_Load(object sender, EventArgs e)
        {
            
            txtDescri.Focus();
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre de la Marca"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();

                bool exito = await objetoMarca.ModificarMarcaAsync(idMarca, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "SG-BAMS",
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
        /// Handles the Click event of the kryptonButton6 control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}