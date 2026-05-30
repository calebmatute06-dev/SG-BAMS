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
    /// Interfaz de usuario para el registro de nuevas categorías o tipos de productos en el sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frnAgregarTipoProducto : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frnAgregarTipoProducto"/>.
        /// </summary>
        public frnAgregarTipoProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Procesa la inserción de un nuevo tipo de producto tras validar el formato y contenido de la descripción.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
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

            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Tipo_producto",
                    columnaNombre: "descripcion_producto",
                    nombreCampo: "Tipo de Producto",
                    idExcluir: 0,
                    idColumna: "id_tipo_producto"))
            {
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
        /// Cierra el formulario actual sin realizar ninguna acción.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frnAgregarTipoProducto_Load(object sender, EventArgs e)
        {
            
        }
    }
}