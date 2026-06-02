using SG_BAMS.Administracion_de_BAMS.Estado;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la ventana para agregar un nuevo estado al sistema.
    /// </summary>
    /// <seealso cref="System.Windows.Forms.Form" />
    public partial class frmAgregarEstado : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarEstado"/>.
        /// </summary>
        public frmAgregarEstado()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.txtDescri.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDescri_KeyPress);
        }

        private void frmAgregarEstado_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la descripción del estado");
        }

        private void pictureBox16_Click(object sender, EventArgs e) { }

        private void btnCerrarSesion_Click(object sender, EventArgs e) => this.Close();

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
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
                        idExcluir: 0,
                        idColumna: "id_estado"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsEstado objetoEstado = new clsEstado();
                bool exito = await objetoEstado.InsertarEstadoAsync(descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Estado registrado correctamente.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al registrar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click(object sender, EventArgs e) => this.Close();
    }
}