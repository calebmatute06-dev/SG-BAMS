using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.MarcaProd;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para la modificación de una marca de producto existente.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsMarca directamente.
    /// </summary>
    public partial class frmModificarMarcaProducto : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int idMarca;
        private PlaceholderTextBox phDescri;

        public frmModificarMarcaProducto(int id, string nombreActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.idMarca = id;
            txtDescri.Text = nombreActual;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirAlfanumerico(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nombre de la marca");
        }

        /// <summary>
        /// Constructor de compatibilidad sin repositorio explícito (usa clsMarca por defecto).
        /// </summary>
        public frmModificarMarcaProducto(int id, string nombreActual)
            : this(id, nombreActual, new clsMarca()) { }

        private void frmModificarMarcaProducto_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string nombreReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.EsAlfanumericoValido(temp, "Nombre de la Marca"))
                    return;
            }

            using (var temp = new TextBox { Text = nombreReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Marca_producto",
                        columnaNombre: "nombre_marca",
                        nombreCampo: "Tipo de Marca Producto",
                        idExcluir: idMarca,
                        idColumna: "id_marca_producto"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(idMarca, nombreReal);

                if (exito)
                {
                    MessageBox.Show("Marca actualizada correctamente.", "SG-BAMS",
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

        private void kryptonButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
