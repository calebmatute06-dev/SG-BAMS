using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarModeloAuto : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frmAgregarModeloAuto"/> class.
        /// </summary>
        public frmAgregarModeloAuto()
        {
            InitializeComponent();
            
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Handles the 1 event of the btnAgregar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre del Modelo de Auto"))
            {
                return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;

                
                if (btnAgregar != null) btnAgregar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();

                bool exito = await objetoModelo.InsertarModeloAutoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (btnAgregar != null) btnAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the 1 event of the btnSalir_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}