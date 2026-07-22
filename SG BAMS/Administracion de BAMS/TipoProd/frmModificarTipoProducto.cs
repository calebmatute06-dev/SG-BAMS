using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.TipoProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de una categoría o tipo de producto existente.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsTipoProducto directamente.
    /// </summary>
    public partial class frmModificarTipoProducto : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int idSeleccionado;
        private PlaceholderTextBox phDescri;

        public frmModificarTipoProducto(int id, string descripcionActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idSeleccionado = id;
            txtDescri.Text = descripcionActual;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el tipo de producto");
        }

        /// <summary>
        /// Constructor de compatibilidad sin repositorio explícito (usa clsTipoProducto por defecto).
        /// </summary>
        public frmModificarTipoProducto(int id, string descripcionActual)
            : this(id, descripcionActual, new clsTipoProducto()) { }

        private void frmModificarTipoProducto_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string nombreReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Tipo de Producto"))
                    return;
            }

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Tipo_producto",
                        columnaNombre: "descripcion_producto",
                        nombreCampo: "Tipo de Producto",
                        idExcluir: idSeleccionado,
                        idColumna: "id_tipo_producto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(idSeleccionado, nombreReal);

                if (exito)
                {
                    MessageBox.Show("Tipo de producto actualizado correctamente.", "SG-BAMS",
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
