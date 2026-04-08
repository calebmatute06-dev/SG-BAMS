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
    /// Representa la interfaz de usuario para la modificación de una categoría o tipo de producto existente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarTipoProducto : Form
    {
        /// <summary>
        /// Almacena el identificador único del tipo de producto seleccionado para su edición.
        /// </summary>
        private int idSeleccionado;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarTipoProducto"/>.
        /// </summary>
        /// <param name="id">El identificador único del tipo de producto.</param>
        /// <param name="descripcionActual">La descripción actual que se cargará en el control de texto.</param>
        public frmModificarTipoProducto(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idSeleccionado = id;
            txtDescri.Text = descripcionActual;

            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Prepara el formulario al cargarse, estableciendo el enfoque y la posición del cursor en el campo de texto.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        /// <summary>
        /// Procesa la actualización del tipo de producto de forma asíncrona tras validar los datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Tipo de Producto"))
            {
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
        /// Cierra el formulario de edición sin aplicar cambios.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}