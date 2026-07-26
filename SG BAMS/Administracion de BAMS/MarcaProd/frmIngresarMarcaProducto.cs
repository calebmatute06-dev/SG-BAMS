using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para ingresar una nueva marca de producto al sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsMarca directamente.
    /// </summary>
    public partial class frmIngresarMarcaProducto : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private PlaceholderTextBox phDescri;

        public frmIngresarMarcaProducto(ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre de la marca");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con el diseñador de WinForms.
        /// </summary>
        public frmIngresarMarcaProducto() : this(new clsMarca()) { }

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

                bool exito = await _repositorio.InsertarAsync(descripcionReal);

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
