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
    /// Representa la interfaz de usuario para la modificación de una marca de producto existente.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmModificarMarcaProducto : Form
    {
        /// <summary>
        /// Almacena el identificador único de la marca.
        /// </summary>
        private int idMarca;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarMarcaProducto"/>.
        /// </summary>
        /// <param name="id">El identificador de la marca.</param>
        /// <param name="nombreActual">El nombre actual que se cargará en el campo de texto.</param>
        public frmModificarMarcaProducto(int id, string nombreActual)
        {
            InitializeComponent();
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idMarca = id;
            txtDescri.Text = nombreActual;

            // Restricción de entrada para permitir solo caracteres alfanuméricos en tiempo real
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }


        /// <summary>
        /// Maneja el evento de carga del formulario para establecer el foco inicial.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void frmModificarMarcaProducto_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
        }

        /// <summary>
        /// Procesa la actualización de la marca de forma asíncrona tras validar los datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnModificar_Click(object sender, EventArgs e)
        {
            if (!ClsValidaciones.EsAlfanumericoValido(txtDescri, "Nombre de la Marca"))
            {
                return;
            }
            if (!ClsValidaciones.ValidarNombreUnico(
                    control: txtDescri,
                    tabla: "Marca_producto",
                    columnaNombre: "nombre_marca",
                    nombreCampo: "Tipo de Marca Producto",
                    idExcluir: 0,
                    idColumna: "id_marca_producto"))
            {
                return;
            }
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();

                // Intento de modificación en la base de datos
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
        /// Cierra el formulario actual.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Maneja el evento Click del botón secundario para salir.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}