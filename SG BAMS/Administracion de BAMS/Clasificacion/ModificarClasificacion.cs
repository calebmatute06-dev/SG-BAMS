using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    /// <summary>
    /// Formulario para modificar una clasificación de proveedor existente.
    /// </summary>
    public partial class ModificarClasificacion : Form
    {
        private int idSeleccionado;
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia del formulario.
        /// </summary>
        /// <param name="id">ID de la clasificación a modificar.</param>
        /// <param name="descripcionActual">Descripción actual de la clasificación.</param>
        public ModificarClasificacion(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idSeleccionado = id;
            txtDescri.Text = descripcionActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void ModificarClasificacion_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la clasificación");
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
           
            string descripcionReal = phDescri.GetRealValue().Trim();

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Clasificación"))
                    return;
            }

           
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "clasificacion_proveedor",
                        columnaNombre: "clasificacion_proveedor",
                        nombreCampo: "Clasificación",
                        idExcluir: idSeleccionado,
                        idColumna: "id_clasificacion_proveedor"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsClasificacion objetoCla = new clsClasificacion();
                bool exito = await objetoCla.ModificarClasificacionAsync(idSeleccionado, descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Clasificación actualizado correctamente.", "SG-BAMS",
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

        private void label8_Click(object sender, EventArgs e)
        {
        }
    }
}