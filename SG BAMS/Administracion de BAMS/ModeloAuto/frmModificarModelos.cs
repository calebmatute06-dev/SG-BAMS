using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de un modelo de vehículo existente.
    /// </summary>
    public partial class frmModificarModelos : Form
    {
        private int idModeloSeleccionado;
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarModelos"/>.
        /// </summary>
        /// <param name="id">El identificador único del modelo.</param>
        /// <param name="nombreActual">El nombre actual del modelo para mostrar en el campo de edición.</param>
        public frmModificarModelos(int id, string nombreActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idModeloSeleccionado = id;
            txtDescri.Text = nombreActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void frmModificarModelos_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre del modelo");
            txtDescri.Focus();
        }

        private async void btnModificar_Click_1(object sender, EventArgs e)
        {
            
            string nombreReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre del Modelo"))
                    return;
            }

            
            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Modelo_de_auto",
                        columnaNombre: "nombre_modelo_auto",
                        nombreCampo: "Tipo de Modelo de Auto",
                        idExcluir: idModeloSeleccionado,
                        idColumna: "id_modelo_auto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();
                bool exito = await objetoModelo.ModificarModeloAutoAsync(idModeloSeleccionado, nombreReal);

                if (exito)
                {
                    MessageBox.Show("Modelo actualizado con éxito.", "SG-BAMS",
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
    }
}