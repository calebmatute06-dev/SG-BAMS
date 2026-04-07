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
using System.Text.RegularExpressions; 

namespace SG_BAMS
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frnAgregarTipoProducto : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="frnAgregarTipoProducto"/> class.
        /// </summary>
        public frnAgregarTipoProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Handles the 1 event of the btnAgregar_Click control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Tipo de Producto"))
            {
                return;
            }

            
            if (Regex.IsMatch(txtDescri.Text.Trim(), @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show("No se permiten letras aisladas en el nombre (excepto la 'y').",
                                "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescri.Focus();
                return;
            }

            

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsTipoProducto objetoTipo = new clsTipoProducto();

                bool exito = await objetoTipo.InsertarTipoProductoAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Tipo de producto registrado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
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