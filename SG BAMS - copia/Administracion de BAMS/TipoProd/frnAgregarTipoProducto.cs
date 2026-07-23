using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Interfaz de usuario para el registro de nuevas categorías o tipos de productos en el sistema.
    /// </summary>
    public partial class frnAgregarTipoProducto : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frnAgregarTipoProducto"/>.
        /// </summary>
        public frnAgregarTipoProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el tipo de producto");
        }

        private void frnAgregarTipoProducto_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el tipo de producto");
        }

        private async void btnAgregar_Click_1(object sender, EventArgs e)
        {
            
            string descripcionReal = phDescri.GetRealValue().Trim();

           
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Tipo de Producto"))
                    return;
            }

            if (Regex.IsMatch(descripcionReal, @"(?i)\b(?![yY]\b)[a-zñáéíóú]\b"))
            {
                MessageBox.Show("No se permiten letras aisladas en el nombre (excepto la 'y').",
                                "Formato Inválido", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescri.Focus();
                return;
            }

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Tipo_producto",
                        columnaNombre: "descripcion_producto",
                        nombreCampo: "Tipo de Producto",
                        idExcluir: 0,
                        idColumna: "id_tipo_producto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsTipoProducto objetoTipo = new clsTipoProducto();
                bool exito = await objetoTipo.InsertarTipoProductoAsync(descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Tipo de producto registrado con éxito.", "SG-BAMS",
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}