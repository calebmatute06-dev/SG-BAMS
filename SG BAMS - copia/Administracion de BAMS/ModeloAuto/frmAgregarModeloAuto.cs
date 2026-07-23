using SG_BAMS.Administracion_de_BAMS.ModeloAuto;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para registrar un nuevo modelo de automóvil en el sistema.
    /// </summary>
    public partial class frmAgregarModeloAuto : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmAgregarModeloAuto"/>.
        /// </summary>
        public frmAgregarModeloAuto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre del modelo de auto");
        }

        private void frmAgregarModeloAuto_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre del modelo de auto");
        }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
           
            string nombreReal = phDescri.GetRealValue().Trim();

            
            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre del Modelo de Auto"))
                    return;
            }

            
            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Modelo_de_auto",
                        columnaNombre: "nombre_modelo_auto",
                        nombreCampo: "Tipo de Modelo de Auto",
                        idExcluir: 0,
                        idColumna: "id_modelo_auto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                if (btnAgregar != null) btnAgregar.Enabled = false;

                clsModeloAuto objetoModelo = new clsModeloAuto();
                bool exito = await objetoModelo.InsertarModeloAutoAsync(nombreReal);

                if (exito)
                {
                    MessageBox.Show("Modelo de auto agregado con éxito.", "SG-BAMS",
                                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error de sistema: " + ex.Message, "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                if (btnAgregar != null) btnAgregar.Enabled = true;
            }
        }

        private void btnSalir_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}