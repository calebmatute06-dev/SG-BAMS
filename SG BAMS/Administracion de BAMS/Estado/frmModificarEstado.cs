using SG_BAMS.Administracion_de_BAMS.Estado;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la ventana para modificar un estado existente en el sistema.
    /// </summary>
    public partial class frmModificarEstado : Form
    {
        private int idEstado;
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmModificarEstado"/>.
        /// </summary>
        /// <param name="id">Identificador del estado a modificar.</param>
        /// <param name="descripcionActual">Descripción actual del estado.</param>
        public frmModificarEstado(int id, string descripcionActual)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.idEstado = id;
            txtDescri.Text = descripcionActual;
            this.txtDescri.KeyPress += new KeyPressEventHandler(this.txtDescri_KeyPress);
        }

        private void frmModificarEstado_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la descripción del estado");
        }

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            
            string descripcionReal = phDescri.GetRealValue().Trim();

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(temp, "Descripción del Estado"))
                    return;
            }

           
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Estado",
                        columnaNombre: "descripcion_estado",
                        nombreCampo: "Tipo de Estado",
                        idExcluir: idEstado,
                        idColumna: "id_estado"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                clsEstado objetoEstado = new clsEstado();
                bool exito = await objetoEstado.ModificarEstadoAsync(idEstado, descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Estado actualizado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}