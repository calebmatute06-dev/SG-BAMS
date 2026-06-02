using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de una marca de producto existente.
    /// </summary>
    public partial class frmModificarMarcaProducto : Form
    {
        private int idMarca;
        private PlaceholderTextBox phDescri;

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
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void frmModificarMarcaProducto_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre de la marca");
            txtDescri.Focus();
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
           
            string nombreReal = phDescri.GetRealValue().Trim();

           
            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre de la Marca"))
                    return;
            }

           
            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Marca_producto",
                        columnaNombre: "nombre_marca",
                        nombreCampo: "Tipo de Marca Producto",
                        idExcluir: idMarca,
                        idColumna: "id_marca_producto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();
                bool exito = await objetoMarca.ModificarMarcaAsync(idMarca, nombreReal);

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}