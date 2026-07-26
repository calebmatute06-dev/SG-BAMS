using SG_BAMS.Administracion_de_BAMS;
using System;
using System.Windows.Forms;
using SG_BAMS.AccesoDatos;
using SG_BAMS.Dominio.AdministracionBAMS;

namespace SG_BAMS
{
    /// <summary>
    /// Formulario para modificar un estado existente en el sistema.
    /// DIP: recibe ICatalogoRepository inyectado, no instancia clsEstado directamente.
    /// SRP: única responsabilidad — capturar y validar datos para actualizar un estado.
    /// </summary>
    public partial class frmModificarEstado : Form
    {
        private readonly ICatalogoRepository _repositorio;
        private readonly int _idEstado;
        private PlaceholderTextBox _phDescri;

        /// <summary>
        /// Constructor que recibe el repositorio por inyección de dependencias.
        /// </summary>
        public frmModificarEstado(int id, string descripcionActual, ICatalogoRepository repositorio)
        {
            InitializeComponent();
            _repositorio = repositorio ?? throw new ArgumentNullException(nameof(repositorio));
            _idEstado = id;

            this.StartPosition = FormStartPosition.CenterScreen;
            this.MaximizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;

            txtDescri.Text = descripcionActual;
            this.txtDescri.KeyPress += txtDescri_KeyPress;

            // Placeholder registrado una sola vez — evita el doble registro del original
            _phDescri = new PlaceholderTextBox(txtDescri, "Ingrese la descripción del estado");
        }

        /// <summary>
        /// Constructor sin parámetros para compatibilidad con código existente.
        /// </summary>
        public frmModificarEstado(int id, string descripcionActual)
            : this(id, descripcionActual, new clsEstado()) { }

        private void txtDescri_KeyPress(object sender, KeyPressEventArgs e)
        {
            ClsValidaciones.PermitirSoloLetras(e);
        }

        private async void btnModificar_Click(object sender, EventArgs e)
        {
            string descripcionReal = _phDescri.GetRealValue().Trim();

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
                        idExcluir: _idEstado,
                        idColumna: "id_estado"))
                    return;
            }

            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnModificar.Enabled = false;

                bool exito = await _repositorio.ModificarAsync(_idEstado, descripcionReal);

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
                MessageBox.Show("Error al actualizar: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnModificar.Enabled = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e) => this.Close();
    }
}