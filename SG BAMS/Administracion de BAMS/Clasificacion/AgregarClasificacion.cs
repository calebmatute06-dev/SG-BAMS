using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS.Administracion_de_BAMS.Clasificacion
{
    /// <summary>
    /// Formulario para agregar una nueva clasificación de proveedor.
    /// </summary>
    public partial class AgregarClasificacion : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia del formulario.
        /// </summary>
        public AgregarClasificacion()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la clasificación");
        }

        /// <summary>
        /// Maneja el evento Load del formulario.
        /// </summary>
        private void AgregarClasificacion_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la clasificación");
        }

        /// <summary>
        /// Maneja el evento Click del botón Agregar.
        /// </summary>
        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            // Obtener valor real (sin placeholder)
            string descripcionReal = phDescri.GetRealValue().Trim();

            // Validación con ClsValidaciones usando control temporal
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Clasificación"))
                    return;
            }

            // Validación manual de letras aisladas usando el string real
            if (Regex.IsMatch(descripcionReal, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show("No se permiten letras aisladas en el nombre (excepto la 'y').",
                                "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescri.Focus();
                return;
            }

            // Validación de nombre único usando control temporal
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "clasificacion_proveedor",
                        columnaNombre: "clasificacion_proveedor",
                        nombreCampo: "Clasificación",
                        idExcluir: 0,
                        idColumna: "id_clasificacion_proveedor"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsClasificacion objetoCla = new clsClasificacion();
                bool exito = await objetoCla.InsertarClasificacionAsync(descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Clasificación registrada con éxito.", "SG-BAMS",
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
        /// Maneja el evento Click del botón Salir.
        /// </summary>
        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}