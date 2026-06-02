using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para ingresar una nueva marca de producto al sistema.
    /// </summary>
    public partial class frmIngresarMarcaProducto : Form
    {
        private PlaceholderTextBox phDescri;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="frmIngresarMarcaProducto"/>.
        /// </summary>
        public frmIngresarMarcaProducto()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
        }

        private void frmIngresarMarcaProducto_Load(object sender, EventArgs e)
        {
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre de la marca");
        }

        private async void btnAgregar_Click(object sender, EventArgs e)
        {
            
            string descripcionReal = phDescri.GetRealValue().Trim();

            
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre de la Marca"))
                    return;
            }

           
            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Marca_producto",
                        columnaNombre: "nombre_marca",
                        nombreCampo: "Tipo de Marca Producto",
                        idExcluir: 0,
                        idColumna: "id_marca_producto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnAgregar.Enabled = false;

                clsMarca objetoMarca = new clsMarca();
                bool exito = await objetoMarca.InsertarMarcaAsync(descripcionReal);

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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}