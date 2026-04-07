using SG_BAMS.Administracion_de_BAMS.TipoProd;
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
    public partial class frmModificarTipoProducto : Form
    {
        /// <summary>
        /// The identifier seleccionado
        /// </summary>
        private int idSeleccionado;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmModificarTipoProducto"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="descripcionActual">The descripcion actual.</param>
        public frmModificarTipoProducto(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idSeleccionado = id;
            txtDescri.Text = descripcionActual;

            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }


        /// <summary>
        /// Handles the Load event of the frmModificarTipoProducto control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {
           
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        /// <summary>
        /// Handles the Click event of the btnModificar control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Tipo de Producto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                bool exito = await objetoTipo.ModificarTipoProductoAsync(idSeleccionado, txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Tipo de producto actualizado correctamente.", "SG-BAMS",
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
    }
}