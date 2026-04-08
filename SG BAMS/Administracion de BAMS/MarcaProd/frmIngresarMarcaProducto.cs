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
    /// Representa la interfaz de usuario para ingresar una nueva marca de producto al sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmIngresarMarcaProducto : Form
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmIngresarMarcaProducto"/>.
        /// </summary>
        public frmIngresarMarcaProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;

            // Restringe la entrada en tiempo real a caracteres alfanuméricos
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        /// <summary>
        /// Maneja el evento Click del botón agregar de forma asíncrona.
        /// Valida la entrada y procede con la inserción en la base de datos.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private async void btnAgregar_Click(object sender, EventArgs e)
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
                btnAgregar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();


                bool exito = await objetoMarca.InsertarMarcaAsync(txtDescri.Text.Trim());

                if (exito)
                {
                    MessageBox.Show("Marca agregada con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error de Sistema",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        /// <summary>
        /// Maneja el evento Click del botón salir para cerrar el formulario.
        /// </summary>
        /// <param name="sender">La fuente del evento.</param>
        /// <param name="e">La instancia de <see cref="EventArgs"/> que contiene los datos del evento.</param>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}