using SG_BAMS.Administracion_de_BAMS;
using SG_BAMS.Administracion_de_BAMS.FormaPago;
using System;
using System.Windows.Forms;

namespace SG_BAMS
{
    /// <summary>
    /// Representa la interfaz de usuario para modificar una forma de pago existente.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsFormaPago directamente.
    /// </summary>
    public partial class frmModificarFormaPago : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int _idFormaPago;
        private PlaceholderTextBox phDescri;

        public frmModificarFormaPago(int id, string descripcionActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _idFormaPago = id;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            txtDescri.Text = descripcionActual;
            txtDescri.KeyPress += (s, e) => ClsValidaciones.PermitirSoloLetras(e);
            phDescri = new PlaceholderTextBox(txtDescri, "Ingrese el nuevo metodo de pago");
        }

        /// <summary>
        /// Constructor de compatibilidad sin repositorio explícito (usa clsFormaPago por defecto).
        /// </summary>
        public frmModificarFormaPago(int id, string descripcionActual)
            : this(id, descripcionActual, new clsFormaPago()) { }

        private void frmModificarFormaPago_Load(object sender, EventArgs e)
        {
            txtDescri.Focus();
            txtDescri.SelectionStart = txtDescri.Text.Length;
        }

        private void pictureBox16_Click(object sender, EventArgs e) { }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string descripcionReal = phDescri.GetRealValue().Trim();

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.EsNombrePersonalValido(temp, "Descripción de Forma de Pago"))
                    return;
            }

            using (var temp = new TextBox { Text = descripcionReal })
            {
                if (!ClsValidaciones.ValidarNombreUnico(
                        control: temp,
                        tabla: "Tipo_Forma_de_pago",
                        columnaNombre: "descripcion_forma_pago",
                        nombreCampo: "Tipo de Forma de Pago",
                        idExcluir: _idFormaPago,
                        idColumna: "id_tipo_forma_pago"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(_idFormaPago, descripcionReal);

                if (exito)
                {
                    MessageBox.Show("Forma de pago actualizada correctamente.", "SG-BAMS",
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

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
